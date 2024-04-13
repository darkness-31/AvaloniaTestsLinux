using AvaloniaTestsLinux.Models;

namespace AvaloniaTestsLinux.ViewModels.Modules;

public class TestLink
{
    internal Test Value { get; }

    internal TestLink(Test value)
    {
        Value = value;
    }
}