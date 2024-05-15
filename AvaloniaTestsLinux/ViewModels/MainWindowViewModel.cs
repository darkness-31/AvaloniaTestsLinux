using System.Linq;
using System.Text;
using AvaloniaTestsLinux.Models;
using AvaloniaTestsLinux.ViewModels.Modules;
using MsBox.Avalonia;
using ReactiveUI;

namespace AvaloniaTestsLinux.ViewModels;

public class MainWindowViewModel : ReactiveObject, IScreen
{
    public RoutingState Router { get; } = new RoutingState();

    public MainWindowViewModel()
    {
        Router.Navigate.Execute(new MainUserControlViewModel(this)
        {
            GlobalRoutingState = Router
        });
    }
}