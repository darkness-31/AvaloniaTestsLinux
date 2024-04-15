using System.Collections.ObjectModel;
using System.Reactive;
using System.Security.Cryptography;
using AvaloniaTestsLinux.Models;
using AvaloniaTestsLinux.ViewModels.Modules;
using ReactiveUI;

namespace AvaloniaTestsLinux.ViewModels;

public class CreateGroupUserControlViewModel : ViewModelBase
{
    internal string Name { get; set; }

    internal ObservableCollection<TempTest> TestTempCollection { get; } = new();

    public ReactiveCommand<Unit, Unit> ReactiveCommandBackRoute { get; set; }
    public ReactiveCommand<Unit, Unit> ReactiveCommandCreateTest { get; set; }
    public ReactiveCommand<Unit, Unit> ReactiveCommandCreate { get; set; }

    internal CreateGroupUserControlViewModel(IScreen screen) 
        : base(screen)
    {
        
    }
}