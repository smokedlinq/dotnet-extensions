﻿using Microsoft.ApplicationInsights.DataContracts;

namespace Microsoft.Extensions.Logging;

public interface ILoggerTelemetry : ILogger
{
    void LogEvent(EventTelemetry telemetry);
    void LogEvent(string eventName, IDictionary<string, string>? properties = null, IDictionary<string, double>? metrics = null);
    void LogAvailability(AvailabilityTelemetry telemetry);
    void LogAvailability(
        string name,
        DateTimeOffset timeStamp,
        TimeSpan duration,
        string runLocation,
        bool success,
        string? message = null,
        IDictionary<string, string>? properties = null,
        IDictionary<string, double>? metrics = null);
}
