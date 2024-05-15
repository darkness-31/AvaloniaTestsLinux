using ReactiveUI;
using System;

namespace AvaloniaTestsLinux.ViewModels;

public class ViewModelBase : ReactiveObject, IRoutableViewModel
{
    public string? UrlPathSegment { get; } = Guid.NewGuid().ToString(); 
    public IScreen HostScreen { get; }
    
    public RoutingState GlobalRoutingState { get; set; }

    internal ViewModelBase(IScreen screen) => HostScreen = screen;

    internal void RoutingBack()
    {
        GlobalRoutingState?.NavigateBack.Execute();
    }
}