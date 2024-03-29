﻿using System.Runtime.CompilerServices;
using Extensions.Microsoft.Logging.ApplicationInsights;
using Microsoft.Extensions.Logging;

[assembly: InternalsVisibleTo("Extensions.Microsoft.Logging.ApplicationInsights.Tests")]

namespace Microsoft.Extensions.DependencyInjection;

public static class LoggerTelemetrySerivceCollectionExtensions
{
    public static IServiceCollection AddLoggerTelemetry(this IServiceCollection services)
    {
        return services
            .AddTransient(typeof(ILoggerTelemetry<>), typeof(LoggerTelemetry<>))
            .AddTransient(typeof(ILoggerTelemetryFactory), typeof(LoggerTelemetryFactory));
    }
}
