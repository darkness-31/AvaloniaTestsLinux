using System.Collections.ObjectModel;
using AvaloniaTestsLinux.ViewModels.Modules;
using ReactiveUI;

namespace AvaloniaTestsLinux.ViewModels;

public class CreateGroupUserControlViewModel : ViewModelBase
{
    internal string Name { get; set; }
    public ObservableCollection<TestLink> TestLinkCollection { get; }

    internal CreateGroupUserControlViewModel(IScreen screen) 
        : base(screen)
    {
        
    }
}