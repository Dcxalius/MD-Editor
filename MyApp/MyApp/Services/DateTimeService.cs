namespace MyApp.Services;

public sealed class DateTimeService : IDateTimeService
{
    public DateTimeOffset Now => DateTimeOffset.Now;
}
