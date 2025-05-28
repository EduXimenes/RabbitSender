using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitSender.Application.DTOs;
using RabbitSender.Infrastructure.Persistence;
using RabbitSender.Infrastructure.Settings;
using System.Text;
using System.Text.Json;

namespace RabbitSender.Worker;

public class EmailConsumerWorker : BackgroundService
{
    private readonly RabbitMQSettings _settings;
    private readonly ILogger<EmailConsumerWorker> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public EmailConsumerWorker(
        IOptions<RabbitMQSettings> options,
        ILogger<EmailConsumerWorker> logger,
        IServiceScopeFactory scopeFactory)
    {
        _settings = options.Value;
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _settings.HostName,
            UserName = _settings.UserName,
            Password = _settings.Password
        };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(queue: _settings.QueueName,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var messageJson = Encoding.UTF8.GetString(body);

            var email = JsonSerializer.Deserialize<SendEmailRequest>(messageJson);

            _logger.LogInformation($"[Worker] Simulating email to: {email.To}");

            // Simulating the email sending
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            db.EmailMessages.Add(new Domain.Entities.EmailMessage
            {
                To = email.To,
                Subject = email.Subject,
                Body = email.Body,
                Status = "Sent",
                CreatedAt = DateTime.UtcNow
            });

            await db.SaveChangesAsync();
        };

        channel.BasicConsume(queue: _settings.QueueName,
                             autoAck: true,
                             consumer: consumer);

        return Task.CompletedTask;
    }
}
