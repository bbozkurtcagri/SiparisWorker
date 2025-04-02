using SiparisWorker.Services;

namespace SiparisWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var siparisService = scope.ServiceProvider.GetRequiredService<OrderService>();

                try
                {
                    await siparisService.GetOrderListAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Sipariþ listesi alýnýrken hata oluþtu.");
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
