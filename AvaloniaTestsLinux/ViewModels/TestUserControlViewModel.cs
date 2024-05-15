using System.Reactive;
using System.Text;
using AvaloniaTestsLinux.Models;
using AvaloniaTestsLinux.Models.Utils;
using AvaloniaTestsLinux.ViewModels.Modules;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;

namespace AvaloniaTestsLinux.ViewModels;

public class TestUserControlViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, Unit> ReactiveCommandBackRoute { get; set; }
    public ReactiveCommand<Unit, Unit> ReactiveCommandCheck { get; set; }

    internal Test Value { get; }
    
    internal TestUserControlViewModel(IScreen screen, Test test) 
        : base(screen)
    {
        Value = test;

        ReactiveCommandCheck = ReactiveCommand.Create(ReactiveCommandCheck_Method);
    }

    internal void ReactiveCommandCheck_Method()
    {
        var script = Encoding.Default.GetString(Value.Script);
        if (ShellHelper.Bash(script) == 0)
        {
            MessageBoxManager.GetMessageBoxStandard("Успешно", "Задание выполнено", ButtonEnum.Ok, Icon.Plus);
            Value.Complete();
        }
        else
        {
            MessageBoxManager.GetMessageBoxStandard("Внимание", "Задание не выполнено!", ButtonEnum.Ok, Icon.Info);
        }
    }
}