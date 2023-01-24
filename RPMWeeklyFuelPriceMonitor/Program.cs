using RPMFuelDataManager.Library.DataAccess;
using RPMWeeklyFuelPriceMonitor;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
        services.AddSingleton<IFuelDataExtract, FuelDataExtract>();
        
    })
    .Build();

await host.RunAsync();
