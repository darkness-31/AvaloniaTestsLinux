using System;
using System.Reactive;
using AvaloniaEdit.Document;
using ReactiveUI;

namespace AvaloniaTestsLinux.ViewModels;

public class CreateTestUserControlViewModel : ViewModelBase
{
    internal string Name { get; set; }
    internal string Description { get; set; }

    internal TextDocument EditorTest { get; set; }
        = new TextDocument();
    
    public ReactiveCommand<Unit, Unit> ReactiveCommandBackRoute { get; set; }
    public ReactiveCommand<Unit, Unit> ButtonAddTestReactiveCommand { get; set; }

    internal CreateTestUserControlViewModel(IScreen screen)
        : base(screen)
    {
        EditorTest.Text = "#!/bin/bash";
    }
}