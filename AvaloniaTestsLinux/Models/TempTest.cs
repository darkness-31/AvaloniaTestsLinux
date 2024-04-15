using System.Reactive;
using ReactiveUI;

namespace AvaloniaTestsLinux.Models;

public class TempTest
{
    internal int Id { get; set; }
    internal string Name { get; set; }
    internal string Description { get; set; }
    internal byte[] Script { get; set; }
    public ReactiveCommand<Unit, Unit> TestEditReactiveCommmand { get; set; }
}