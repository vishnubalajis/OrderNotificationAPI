using Microsoft.AspNetCore.Mvc;
using OrderNotificationAPI.Services;

namespace OrderNotificationAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly SNSService _snsService;

    public OrderController(SNSService snsService)
    {
        _snsService = snsService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder()
    {
        var order = new
        {
            OrderId = Guid.NewGuid(),
            ProductName = "Millet Health Mix",
            Quantity = 2,
            Amount = 500,
            Status = "OrderCreated"
        };

        var messageId = await _snsService.PublishMessage(
            System.Text.Json.JsonSerializer.Serialize(order)
        );

        return Ok(new
        {
            Message = "Order notification published successfully",
            Order = order,
            SNSMessageId = messageId
        });
    }
}