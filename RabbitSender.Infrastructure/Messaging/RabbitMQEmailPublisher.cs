using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitSender.Application.DTOs;
using RabbitSender.Infrastructure.Settings;
using RabbitMQ.Client;

namespace RabbitSender.Infrastructure.Messaging;

public class RabbitMQEmailPublisher
{
    private readonly RabbitMQSettings _settings;

    public RabbitMQEmailPublisher(IOptions<RabbitMQSettings> options)
    {
        _settings = options.Value;
    }

    public void Publish(SendEmailRequest request)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _settings.HostName,
            UserName = _settings.UserName,
            Password = _settings.Password
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: _settings.QueueName,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var json = JsonSerializer.Serialize(request);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(exchange: "",
                             routingKey: _settings.QueueName,
                             basicProperties: null,
                             body: body);
    }
}
