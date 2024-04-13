using AvaloniaTestsLinux.Models;
using AvaloniaTestsLinux.Models.Utils;
using AvaloniaTestsLinux.ViewModels;
using ReactiveUI;

namespace AvaloniaTestsLinux.ViewModels;

public class MainWindowViewModel : ReactiveObject, IScreen
{
    public RoutingState Router { get; } = new RoutingState();

    public MainWindowViewModel()
    {
        Router.Navigate.Execute(new MainUserControlViewModel(this));
    }
}