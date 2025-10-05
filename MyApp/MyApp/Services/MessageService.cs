namespace MyApp.Services;

public sealed class MessageService : IMessageService
{
    public string GetWelcomeMessage(DateTimeOffset currentTime)
    {
        var greeting = currentTime.Hour switch
        {
            >= 5 and < 12 => "Good morning",
            >= 12 and < 18 => "Good afternoon",
            >= 18 and < 22 => "Good evening",
            _ => "Hello"
        };

        return $"{greeting}! Welcome to MyApp.";
    }
}
