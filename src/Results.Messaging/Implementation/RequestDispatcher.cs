using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using Toarnbeike.Results.Messaging.Pipeline;
using Toarnbeike.Results.Messaging.Requests;

namespace Toarnbeike.Results.Messaging.Implementation;

/// <inheritdoc cref="IRequestDispatcher" />
internal sealed class RequestDispatcher(IServiceProvider provider) : IRequestDispatcher
{
    private static readonly ConcurrentDictionary<Type, Func<object, IServiceProvider, CancellationToken, Task<object>>> _cache = new();

    public async Task<TResponse> DispatchAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        where TResponse : IResult
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var requestType = request.GetType();

        var executorDelegate = _cache.GetOrAdd(requestType, _ => CreateExecutorDelegate<TResponse>(requestType));
        var result = await executorDelegate(request, provider, cancellationToken);

        return (TResponse)result;
    }

    private static Func<object, IServiceProvider, CancellationToken, Task<object>> CreateExecutorDelegate<TResponse>(Type requestType)
        where TResponse : IResult
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
        var behaviourType = typeof(IPipelineBehaviour<,>).MakeGenericType(requestType, typeof(TResponse));
        var executorType = typeof(RequestPipelineExecutor<,>).MakeGenericType(requestType, typeof(TResponse));

        // Parameters for the lambda
        var requestParam = Expression.Parameter(typeof(object), "request");
        var providerParam = Expression.Parameter(typeof(IServiceProvider), "provider");
        var ctParam = Expression.Parameter(typeof(CancellationToken), "ct");

        // Cast request
        var castedRequest = Expression.Convert(requestParam, requestType);

        // provider.GetServices<IPipelineBehaviour<TRequest,TResponse>>()
        var getBehavioursCall = Expression.Call(
            typeof(ServiceProviderServiceExtensions),
            nameof(ServiceProviderServiceExtensions.GetServices),
            [behaviourType],
            providerParam);

        // provider.GetRequiredService<IRequestHandler<TRequest,TResponse>>()
        var getHandlerCall = Expression.Call(
            typeof(ServiceProviderServiceExtensions),
            nameof(ServiceProviderServiceExtensions.GetRequiredService),
            [handlerType],
            providerParam);

        // new RequestPipelineExecutor<TRequest,TResponse>(behaviours, handler)
        var executorCtor = executorType.GetConstructor([typeof(IEnumerable<>).MakeGenericType(behaviourType), handlerType
        ])!;
        var executorVar = Expression.Variable(executorType, "executor");
        var assignExecutor = Expression.Assign(
            executorVar,
            Expression.New(executorCtor, getBehavioursCall, getHandlerCall));

        // executor.ExecuteAsync(request, ct)
        var executeMethod = executorType.GetMethod("ExecuteAsync")!;

        var executeCall = Expression.Call(
            executorVar,
            executeMethod,
            castedRequest,
            ctParam);

        // Wrap Task<TResponse> -> Task<object>
        var wrapCall = Expression.Call(
            typeof(RequestDispatcher),
            nameof(WrapTask),
            [typeof(TResponse)],
            executeCall);

        // Full block
        var body = Expression.Block(
            [executorVar],
            assignExecutor,
            wrapCall);

        return Expression.Lambda<Func<object, IServiceProvider, CancellationToken, Task<object>>>(
            body, requestParam, providerParam, ctParam).Compile();
    }

    private static async Task<object> WrapTask<T>(Task<T> task) => (await task)!;
}