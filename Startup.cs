using AzureKafkaTriggerFun.MessageQueueClient;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AzureKafkaTriggerFun.Startup))]
namespace AzureKafkaTriggerFun
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IMessageQueue, MessageQueue>();
            builder.Services.AddOptions<FuncConfig>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("Config").Bind(settings);
                });
        }
    }
}