using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using AvaloniaTestsLinux.Models;
using AvaloniaTestsLinux.ViewModels.Modules;
using DynamicData;
using ReactiveUI;

namespace AvaloniaTestsLinux.ViewModels
{
    public class MainUserControlViewModel : ViewModelBase
    {
        public ObservableCollection<GroupTestVM> ItemsControlGroups { get; } = new();
        public ReactiveCommand<Unit, Unit> ReactiveCommandCreateGroup { get; set;  }

        internal MainUserControlViewModel(IScreen screen)
            : base(screen)
        {
            ReactiveCommandCreateGroup = ReactiveCommand.Create(() =>
            {
                GlobalRoutingState.Navigate.Execute(new CreateGroupUserControlViewModel(screen)
                {
                    GlobalRoutingState = this.GlobalRoutingState
                });
            });

            GroupTest.GroupTestCreateEvent += group => ItemsControlGroups.Add(new GroupTestVM(group)); 
            
            Render();
        }

        internal void Render()
        {
            ItemsControlGroups.Clear();
            ItemsControlGroups.AddRange(GroupTest.GetCollection().Select(x => new GroupTestVM(x)));
        }
    }
}
