using Microsoft.AspNetCore.Mvc;
using RabbitSender.Application.DTOs;
using RabbitSender.Infrastructure.Messaging;

namespace RabbitSender.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly RabbitMQEmailPublisher _publisher;

    public EmailController(RabbitMQEmailPublisher publisher)
    {
        _publisher = publisher;
    }

    [HttpPost]
    public IActionResult SendEmail([FromBody] SendEmailRequest request)
    {
        _publisher.Publish(request);
        return Accepted("Email queued for sending.");
    }
}
