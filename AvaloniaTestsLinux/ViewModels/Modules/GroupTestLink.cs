using AvaloniaTestsLinux.Models;

namespace AvaloniaTestsLinux.ViewModels.Modules;

public class GroupTestLink
{
    internal GroupTest Value { get; }
    
    internal string WithPercent { get; private set; } 
    
    internal GroupTestLink(GroupTest value)
    {
        Value = value;
        WithPercent = value.ComplatePercent + "%";
    }
}