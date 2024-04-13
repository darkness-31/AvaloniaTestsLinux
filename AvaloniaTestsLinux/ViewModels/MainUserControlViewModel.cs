using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using AvaloniaTestsLinux.Models;
using AvaloniaTestsLinux.ViewModels.Modules;
using ReactiveUI;

namespace AvaloniaTestsLinux.ViewModels
{
    public class MainUserControlViewModel : ViewModelBase
    {
        public ObservableCollection<GroupTestLink> ItemsControlGroups { get; } 
            = new ObservableCollection<GroupTestLink>();

        public ReactiveCommand<Unit, Unit> ReactiveCommandCreateGroup { get; set;  }

        internal MainUserControlViewModel(IScreen screen)
            : base(screen)
        {
            ItemsControlGroups = new ObservableCollection<GroupTestLink>(GroupTest.GetCollection()
                                                                                  .Select(x => new GroupTestLink(x)));
        }

    }
}
