using ReactiveUI;

namespace AvaloniaTestsLinux.ViewModels;

public class CreateTestUserControlViewModel : ViewModelBase
{
    internal string Name { get; set; }
    internal string Description { get; set; }
    internal string Script { get; set; }
    
    internal CreateTestUserControlViewModel(IScreen screen) 
        : base(screen)
    {
        
    }
}