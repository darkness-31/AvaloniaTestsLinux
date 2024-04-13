using System.Collections.ObjectModel;
using System.Reactive;
using AvaloniaTestsLinux.ViewModels.Modules;
using ReactiveUI;

namespace AvaloniaTestsLinux.ViewModels;

public class CreateGroupUserControlViewModel : ViewModelBase
{
    internal string Name { get; set; }

    public ObservableCollection<TestLink> TestLinkCollection { get; }
        = new ObservableCollection<TestLink>();
    public ReactiveCommand<Unit, Unit> ReactiveCommandBackRoute { get; set; }
    public ReactiveCommand<Unit, Unit> ReactiveCommandCreateTest { get; set; }

    internal CreateGroupUserControlViewModel(IScreen screen) 
        : base(screen)
    {
        
    }
}