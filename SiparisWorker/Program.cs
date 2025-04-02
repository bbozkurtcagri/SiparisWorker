using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SiparisWorker;
using SiparisWorker.Services;

Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddMemoryCache();
        services.AddHttpClient<TokenService>();
        services.AddHttpClient<OrderService>();
        services.AddSingleton<TokenService>();
        services.AddSingleton<OrderService>();
        services.AddHostedService<Worker>();
    })
    .Build()
    .Run();
