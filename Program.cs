using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Moonman.KeyVault;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration((context, config) =>
    {
        string? secretKey = System.Environment.GetEnvironmentVariable("");
        if (secretKey != null)
        {
            string? secretValue = KeyVaultSecretAggregtor.GetSecretValue(secretKey);
            if (secretValue != null)
            {
                config.AddInMemoryCollection([new KeyValuePair<string, string?>("MoonBusConnectionString", secretValue)]);
            }

        }

    })
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
