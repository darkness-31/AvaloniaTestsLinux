using System.Linq;
using System.Text;
using AvaloniaTestsLinux.Models;
using AvaloniaTestsLinux.ViewModels.Modules;
using MsBox.Avalonia;
using ReactiveUI;

namespace AvaloniaTestsLinux.ViewModels;

public class MainWindowViewModel : ReactiveObject, IScreen
{
    public RoutingState Router { get; } = new RoutingState();

    public MainWindowViewModel()
    {
        var mainView = new MainUserControlViewModel(this);

        void Render()
        {
            mainView.Render();
            foreach (GroupTestLink item in mainView.ItemsControlGroups)
            {
                item.ReactiveCommandGroupMain = ReactiveCommand.Create(() =>
                {
                    var uc = new GroupUserControlViewModel(this, item.Value)
                    {
                        ReactiveCommandBackRoute = ReactiveCommand.Create(() => { Router.NavigateBack.Execute(); }),
                        ReactiveCommandCreateTest = ReactiveCommand.Create(() =>
                        {
                            var uc = new CreateTestUserControlViewModel(this);
                            uc.ReactiveCommandBackRoute = ReactiveCommand.Create(() => { Router.NavigateBack.Execute(); });
                            uc.ButtonAddTestReactiveCommand = ReactiveCommand.Create(() =>
                            {
                                Test.Create(item.Value.Id,
                                    uc.Name,
                                    uc.Description,
                                    Entity.Get(Entity.eName.complate_state, 0),
                                    Encoding.Default.GetBytes(uc.EditorTest.Text));
                                Router.NavigateBack.Execute();
                                Render();
                            });
                            item.Render();
                            Router.Navigate.Execute(uc);
                        })
                    };

                    foreach (var testLink in uc.Tests)
                    {
                        testLink.ReactiveCommandTestUserControl = ReactiveCommand.Create(() =>
                        {
                            var tuc = new TestUserControlViewModel(this, testLink.Value);
                            tuc.ReactiveCommandBackRoute = ReactiveCommand.Create(() => { Router.NavigateBack.Execute(); });
                            Router.Navigate.Execute(tuc);
                        });
                    }
                    Router.Navigate.Execute(uc);
                });
            }
            
        }
        
        Router.Navigate.Execute(mainView);
        mainView.ReactiveCommandCreateGroup = ReactiveCommand.Create(() =>
        {
            var groupCreate = new CreateGroupUserControlViewModel(this);
            groupCreate.ReactiveCommandBackRoute = ReactiveCommand.Create(() =>
            {
                Router.NavigateBack.Execute();
                Render();
            });
            groupCreate.ReactiveCommandCreateTest = ReactiveCommand.Create(() =>
            {
                var testCreate = new CreateTestUserControlViewModel(this);
                testCreate.ReactiveCommandBackRoute = ReactiveCommand.Create(() => { Router.NavigateBack.Execute(); });
                testCreate.ButtonAddTestReactiveCommand = ReactiveCommand.Create(() =>
                {
                    if (groupCreate.TestTempCollection.Any(x => x.Name == testCreate.Name))
                    {
                        MessageBoxManager.GetMessageBoxStandard("Внимание", "Имя теста уже существует");
                        return;
                    }

                    var tmpTest = new TempTest()
                    {
                        Id = groupCreate.TestTempCollection.Count,
                        Name = testCreate.Name ?? string.Empty,
                        Description = testCreate.Description ?? string.Empty,
                        Script = Encoding.Default.GetBytes(testCreate.EditorTest.Text)
                    };
                    groupCreate.TestTempCollection.Add(tmpTest);
                    Router.NavigateBack.Execute();
                });
                Router.Navigate.Execute(testCreate);
            });
            groupCreate.ReactiveCommandCreate = ReactiveCommand.Create(() =>
            {
                var group = GroupTest.Create(groupCreate.Name);
                foreach (TempTest item in groupCreate.TestTempCollection)
                {
                    Test.Create(group.Id, 
                                item.Name, 
                                item.Description, 
                                Entity.Get(Entity.eName.complate_state, 0)!,
                                item.Script);
                }
                Render();
                Router.NavigateBack.Execute();
            });
            Router.Navigate.Execute(groupCreate);
        });
        
        Render();
    }
}