using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toarnbeike.Results.MinimalApi.DependencyInjection;
using Toarnbeike.Results.MinimalApi.Mapping;
using Toarnbeike.Results.MinimalApi.Mapping.Failures;

namespace Toarnbeike.Results.MinimalApi.Tests.DependencyInjection;

public class ResultMappingBuilderTests
{
    private readonly IServiceCollection _services = new ServiceCollection();

    private IServiceProvider Build(Action<ResultMappingBuilder>? configure = null)
    {
        _services.Clear();
        _services.AddResultMapping(configure);
        return _services.BuildServiceProvider();
    }

    [Fact]
    public void AddResultMapping_Should_RegisterDefaultMappers()
    {
        var provider = Build();

        provider.GetService<IResultMapper>().ShouldBeOfType<ResultMapper>();
        
        var mappers = provider.GetServices<IFailureResultMapper>().ToList();

        mappers.ShouldContain(x => x is AggregateFailureResultMapper);
        mappers.ShouldContain(x => x is ExceptionFailureResultMapper);
        mappers.ShouldContain(x => x is ValidationFailureResultMapper);
        mappers.ShouldContain(x => x is ValidationFailuresResultMapper);
    }

    [Fact]
    public void AddResultMapping_Should_RegisterDefaultFallbackMapper()
    {
        var provider = Build();

        provider.GetService<IFallbackFailureResultMapper>().ShouldBeOfType<FallbackFailureResultMapper>();
    }

    [Fact]
    public void AddMapper_Should_RegisterCustomMapper()
    {
        var provider = Build(cfg => cfg.AddMapper<CustomMapper>());

        var mappers = provider.GetServices<IFailureResultMapper>();

        mappers.ShouldContain(x => x is CustomMapper);
    }


    [Fact]
    public void UseFallback_Should_ReplaceFallbackMapper()
    {
        var provider = Build(cfg => cfg.UseFallback<CustomFallbackMapper>());

        var fallback = provider.GetService<IFallbackFailureResultMapper>();

        fallback.ShouldBeOfType<CustomFallbackMapper>();
    }

    [Fact]
    public void AddMappersFromAssembly_Should_RegisterAllMappersFromAssembly()
    {
        var provider = Build(cfg =>
            cfg.AddMappersFromAssembly(typeof(CustomMapper).Assembly));

        var mappers = provider.GetServices<IFailureResultMapper>().ToList();

        mappers.ShouldContain(x => x.GetType() == typeof(CustomMapper));
        mappers.ShouldContain(x => x.GetType() == typeof(SecondCustomMapper));
    }

    [Fact]
    public void AddMappersFromAssemblyContaining_Should_RegisterAllMappersFromAssembly()
    {
        var provider = Build(cfg =>
            cfg.AddMappersFromAssemblyContaining<CustomMapper>());

        var mappers = provider.GetServices<IFailureResultMapper>().ToList();

        mappers.ShouldContain(x => x.GetType() == typeof(CustomMapper));
        mappers.ShouldContain(x => x.GetType() == typeof(SecondCustomMapper));
    }

    private sealed class CustomMapper : FailureResultMapper<Failure>
    {
        public override ProblemDetails Map(Failure failure) =>
            throw new NotImplementedException();
    }

    private sealed class SecondCustomMapper : FailureResultMapper<Failure>
    {
        public override ProblemDetails Map(Failure failure) =>
            throw new NotImplementedException();
    }

    private sealed class CustomFallbackMapper : FailureResultMapper<Failure>, IFallbackFailureResultMapper
    {
        public override ProblemDetails Map(Failure failure) =>
            throw new NotImplementedException();
    }
}
