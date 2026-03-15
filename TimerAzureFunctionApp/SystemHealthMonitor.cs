using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TimerAzureFunctionApp;

public class SystemHealthMonitor
{
    private readonly ILogger _logger;

    public SystemHealthMonitor(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<SystemHealthMonitor>();
    }

    [Function("SystemHealthMonitor")]
    public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer)
    {
        try
        {
            _logger.LogInformation("Timer triggered at: {time}", DateTime.UtcNow);

            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation("Next schedule at: {next}",
                    myTimer.ScheduleStatus.Next);
            }

            var memoryUsedMB = Process.GetCurrentProcess().WorkingSet64 / (1024 * 1024);

            _logger.LogInformation("Current memory usage: {memory} MB", memoryUsedMB);


            if (memoryUsedMB > 200)
            {
                _logger.LogWarning("High memory usage detected: {memory} MB", memoryUsedMB);

            }

            await Task.Delay(1000);

            _logger.LogInformation("System health check completed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in SystemHealthMonitor.");
        }
    }
}