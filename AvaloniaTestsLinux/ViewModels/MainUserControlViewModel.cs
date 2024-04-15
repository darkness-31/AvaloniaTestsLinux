using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Rendering;
using AvaloniaTestsLinux.Models;
using AvaloniaTestsLinux.ViewModels.Modules;
using DynamicData;
using ReactiveUI;

namespace AvaloniaTestsLinux.ViewModels
{
    public class MainUserControlViewModel : ViewModelBase
    {
        public ObservableCollection<GroupTestLink> ItemsControlGroups { get; } = new();
        public ReactiveCommand<Unit, Unit> ReactiveCommandCreateGroup { get; set;  }

        internal MainUserControlViewModel(IScreen screen)
            : base(screen)
        {
            Render();
        }

        internal void Render()
        {
            ItemsControlGroups.Clear();
            ItemsControlGroups.AddRange(GroupTest.GetCollection().Select(x => new GroupTestLink(x)));
        }
    }
}
