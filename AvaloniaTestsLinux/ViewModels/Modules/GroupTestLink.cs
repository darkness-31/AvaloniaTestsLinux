using System.Linq;
using System.Reactive;
using AvaloniaEdit.Utils;
using AvaloniaTestsLinux.Models;
using ReactiveUI;

namespace AvaloniaTestsLinux.ViewModels.Modules;

public class GroupTestLink
{
    internal GroupTest Value { get; }
    
    internal string WithPercent { get; private set; }
    public ReactiveCommand<Unit, Unit> ReactiveCommandGroupMain { get; set; }


    internal GroupTestLink(GroupTest value)
    {
        Value = value;
        WithPercent = value.ComplatePercent + "%";
    }

    internal void Render()
    {
        Value.Tests.Clear();
        Value.Tests.AddRange(Test.GetCollection(Value.Id));
    }
}