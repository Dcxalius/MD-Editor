using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyApp.Services;

namespace MyApp.ViewModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    private readonly IDateTimeService _dateTimeService;
    private readonly IMessageService _messageService;
    private string _greeting = string.Empty;
    private DateTimeOffset _currentTime;

    public MainPageViewModel(IDateTimeService dateTimeService, IMessageService messageService)
    {
        _dateTimeService = dateTimeService;
        _messageService = messageService;
        Refresh();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Greeting
    {
        get => _greeting;
        private set => SetProperty(ref _greeting, value);
    }

    public DateTimeOffset CurrentTime
    {
        get => _currentTime;
        private set => SetProperty(ref _currentTime, value);
    }

    public void Refresh()
    {
        var now = _dateTimeService.Now;
        CurrentTime = now;
        Greeting = _messageService.GetWelcomeMessage(now);
    }

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected void OnPropertyChanged(string? propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
