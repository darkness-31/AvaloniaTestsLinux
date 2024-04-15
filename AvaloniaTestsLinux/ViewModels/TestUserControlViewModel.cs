using System.Reactive;
using AvaloniaTestsLinux.Models;
using AvaloniaTestsLinux.ViewModels.Modules;
using ReactiveUI;

namespace AvaloniaTestsLinux.ViewModels;

public class TestUserControlViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, Unit> ReactiveCommandBackRoute { get; set; }
    public ReactiveCommand<Unit, Unit> ReactiveCommandPrevTest { get; set; }
    public ReactiveCommand<Unit, Unit> ReactiveCommandNextTest { get; set; }
    public ReactiveCommand<Unit, Unit> ReactiveCommandCheck { get; set; }

    internal Test Value { get; }
    
    internal TestUserControlViewModel(IScreen screen, Test test) 
        : base(screen)
    {
        Value = test;
    }
}