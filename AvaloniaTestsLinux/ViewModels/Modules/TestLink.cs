using System.Reactive;
using AvaloniaTestsLinux.Models;
using ReactiveUI;

namespace AvaloniaTestsLinux.ViewModels.Modules;

public class TestLink
{
    internal Test Value { get; }
    public ReactiveCommand<Unit, Unit> ReactiveCommandTestUserControl { get; set; }

    internal TestLink(Test value)
    {
        Value = value;
    }
}