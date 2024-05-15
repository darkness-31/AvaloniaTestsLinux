using System.Collections.ObjectModel;
using System.Reactive;
using System.Security.Cryptography;
using System.Text;
using AvaloniaTestsLinux.Models;
using AvaloniaTestsLinux.ViewModels.Modules;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;

namespace AvaloniaTestsLinux.ViewModels;

public class CreateGroupUserControlViewModel : ViewModelBase
{
    internal string Name { get; set; }
    public ReactiveCommand<Unit, Unit> ReactiveCommandBackRoute { get; set; }
    public ReactiveCommand<Unit, Unit> ReactiveCommandCreateTest { get; set; }
    public ReactiveCommand<Unit, Unit> ReactiveCommandCreate { get; set; }
    public ObservableCollection<TestVM> TestCollection { get; } = new();

    internal CreateGroupUserControlViewModel(IScreen screen) 
        : base(screen)
    {
        ReactiveCommandBackRoute = ReactiveCommand.Create(RoutingBack);
        
        ReactiveCommandCreateTest = ReactiveCommand.Create(() =>
        {
            GlobalRoutingState.Navigate.Execute(new CreateTestUserControlViewModel(screen, TestCollection)
            {
                GlobalRoutingState = this.GlobalRoutingState
            });
        });

        ReactiveCommandCreate = ReactiveCommand.Create(() =>
        {
            try
            {
                var group = GroupTest.Create(Name);
                foreach (TestVM test in TestCollection)
                {
                    Test.Create(group.Id, 
                                test.Name,
                                test.Description,
                                Entity.Get(Handbook.Entity.NameEnum.complate_state, 0), 
                                Encoding.UTF8.GetBytes(test.Script));
                }
                RoutingBack();
            }
            catch
            {
                MessageBoxManager.GetMessageBoxStandard("Ошибка", "Ошибка в группе");
            }
        });
    }
}