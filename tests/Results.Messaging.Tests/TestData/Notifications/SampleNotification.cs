using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toarnbeike.Results.Messaging.Notifications;

namespace Toarnbeike.Results.Messaging.Tests.TestData.Notifications;

public record SampleNotification(string Payload) : NotificationBase("TestSender");