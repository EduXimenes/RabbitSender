namespace RabbitSender.Infrastructure.Settings;

public class RabbitMQSettings
{
    public string HostName { get; set; } = "localhost";
    public string QueueName { get; set; } = "email_queue";
    public string UserName { get; set; } = "admin";
    public string Password { get; set; } = "admin";
}
