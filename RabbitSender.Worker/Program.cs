using RabbitSender.Infrastructure;
using RabbitSender.Infrastructure.Settings;
using RabbitSender.Worker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddInfrastructure(context.Configuration);
        services.Configure<RabbitMQSettings>(context.Configuration.GetSection("RabbitMQ"));
        services.AddHostedService<EmailConsumerWorker>();
    })
    .Build();

await host.RunAsync();