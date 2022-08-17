namespace BackgroundTaskFirstTest.Api;

public class MyBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MyBackgroundService> _logger;

    public MyBackgroundService(IServiceProvider serviceProvider, ILogger<MyBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                _logger.LogInformation("ExecuteAsync... {DateTime}", DateTime.UtcNow);
                var scopedService = scope.ServiceProvider.GetRequiredService<IScopedService>();
                scopedService.Write();
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StartAsync.... {DateTime}", DateTime.UtcNow);
        return base.StartAsync(cancellationToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("StopAsync.... {DateTime}", DateTime.UtcNow);
        return base.StopAsync(cancellationToken);
    }
}