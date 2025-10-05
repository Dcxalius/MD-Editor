namespace MyApp.Services;

public interface IMessageService
{
    string GetWelcomeMessage(DateTimeOffset currentTime);
}
