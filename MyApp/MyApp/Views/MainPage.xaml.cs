using Microsoft.UI.Xaml.Controls;
using MyApp.ViewModels;

namespace MyApp.Views;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
    }

    public MainPage(MainPageViewModel viewModel)
        : this()
    {
        InitializeComponent();
        ViewModel = viewModel;
        DataContext = viewModel;
    }

    public MainPageViewModel ViewModel { get; }
}
