using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using AvaloniaEdit.Utils;
using AvaloniaTestsLinux.Models;
using AvaloniaTestsLinux.ViewModels.Modules;
using ReactiveUI;

namespace AvaloniaTestsLinux.ViewModels;

public class GroupUserControlViewModel : ViewModelBase
{
    internal GroupTest Value { get; set; }
    
    internal ReactiveCommand<Unit, Unit> ReactiveCommandCreateTest { get; set; }
    internal ReactiveCommand<Unit, Unit> ReactiveCommandBackRoute { get; set; }
    internal ReactiveCommand<Unit, Unit> ReactiveCommandTest { get; set; }
    
    internal ObservableCollection<TestLink> Tests { get; set; } = new();

    internal GroupUserControlViewModel(IScreen screen, GroupTest group) 
        : base(screen)
    {
        Value = group;
        Tests.AddRange(Value.Tests.Select(x => new TestLink(x)));
    }
}