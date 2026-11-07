using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace OrderNotificationAPI.Services;

public class SNSService
{
    private readonly IAmazonSimpleNotificationService _snsClient;
    private readonly IConfiguration _configuration;

    public SNSService(
        IAmazonSimpleNotificationService snsClient,
        IConfiguration configuration)
    {
        _snsClient = snsClient;
        _configuration = configuration;
    }

    public async Task<string> PublishMessage(string message)
    {
        var topicArn = _configuration["SNS:TopicArn"];

        var request = new PublishRequest
        {
            TopicArn = topicArn,
            Message = message,
            Subject = "New Order Notification"
        };

        var response = await _snsClient.PublishAsync(request);

        return response.MessageId;
    }
}