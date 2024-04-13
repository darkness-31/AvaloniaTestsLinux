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
        var mainView = new MainUserControlViewModel(this);
       
        Router.Navigate.Execute(mainView);
        mainView.ReactiveCommandCreateGroup = ReactiveCommand.Create(() =>
        {
            var groupCreate = new CreateGroupUserControlViewModel(this);
            groupCreate.ReactiveCommandBackRoute = ReactiveCommand.Create(() => { Router.NavigateBack.Execute(); });
            groupCreate.ReactiveCommandCreateTest = ReactiveCommand.Create(() =>
            {
                var testCreate = new CreateTestUserControlViewModel(this);
                testCreate.ReactiveCommandBackRoute = ReactiveCommand.Create(() => { Router.NavigateBack.Execute(); });
                Router.Navigate.Execute(testCreate);
            });
            Router.Navigate.Execute(groupCreate);
        });
    }
}