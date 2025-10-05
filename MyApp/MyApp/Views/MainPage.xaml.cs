using MyApp.ViewModels;

namespace MyApp.Views;

public partial class MainPage
{
    public MainPage(MainPageViewModel viewModel)
    {
        ViewModel = viewModel;
    }

    public MainPageViewModel ViewModel { get; }
}
