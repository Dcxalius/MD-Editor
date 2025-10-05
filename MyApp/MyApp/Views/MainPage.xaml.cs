using MyApp.ViewModels;

namespace MyApp.Views;

public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    public MainPage(MainPageViewModel viewModel)
        : this()
    {
        ViewModel = viewModel;
        DataContext = viewModel;
    }

    public MainPageViewModel ViewModel { get; private set; } = null!;
}
