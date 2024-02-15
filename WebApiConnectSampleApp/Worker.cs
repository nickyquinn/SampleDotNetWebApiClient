using WebApiConnectLib;

namespace WebApiConnectSampleApp;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IWebApiBuilder _api;

    public Worker(ILogger<Worker> logger, IWebApiBuilder api)
    {
        _logger = logger;
        _api = api;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}