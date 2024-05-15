using System.Reactive;
using AvaloniaTestsLinux.Models;
using ReactiveUI;

namespace AvaloniaTestsLinux.ViewModels.Modules;

public class TestVM
{
    internal Test Value { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Script { get; set; }
    
    internal RoutingState GlobalRoutingState { get; set; }
    internal IScreen GlobalScreen { get; set; }
    
    public ReactiveCommand<Unit, Unit> ReactiveCommandTestUserControl { get; set; }
    public ReactiveCommand<Unit, Unit> TestEditReactiveCommmand { get; }

    internal TestVM()
    {
        TestEditReactiveCommmand = ReactiveCommand.Create(() =>
        {
            GlobalRoutingState.Navigate.Execute(new CreateTestUserControlViewModel(GlobalScreen, this)
            {
                GlobalRoutingState = this.GlobalRoutingState
            });
        });
    }
    
    internal TestVM(Test value) : this()
    {
        Value = value;
    }
}