using RPMFuelDataManager.Library.DataAccess;
using RPMFuelDataManager.Library.Models;

namespace RPMWeeklyFuelPriceMonitor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private readonly IFuelDataExtract _fuelDataExtract;

        public Worker(ILogger<Worker> logger, IConfiguration config, IFuelDataExtract fuelDataExtract)
        {
            _logger = logger;
            _config = config;
            _fuelDataExtract = fuelDataExtract;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                int delayTime = _config.GetValue<int>("delayExuction");
                
                await Task.Delay(delayTime, stoppingToken);
                // after set delay time execute the below
                var listData = _fuelDataExtract.GetFuelDat();
                _fuelDataExtract.SaveFuelData(listData);
                 
            }
        }
    }
}