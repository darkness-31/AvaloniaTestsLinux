using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using AvaloniaEdit.Document;
using AvaloniaTestsLinux.Models;
using AvaloniaTestsLinux.ViewModels.Modules;
using ReactiveUI;

namespace AvaloniaTestsLinux.ViewModels;

public class CreateTestUserControlViewModel : ViewModelBase
{
    internal string Name { get; set; } = string.Empty;
    internal string Description { get; set; } = string.Empty;

    internal TextDocument EditorTest { get; set; } = new ();
    
    public ReactiveCommand<Unit, Unit> ReactiveCommandBackRoute { get; set; }
    public ReactiveCommand<Unit, Unit> ButtonAddTestReactiveCommand { get; set; }

    internal CreateTestUserControlViewModel(IScreen screen, TestVM test)
        : base(screen)
    {
        Name = test.Name;
        Description = test.Description;
        EditorTest.Text = test.Script;

        ReactiveCommandBackRoute = ReactiveCommand.Create(RoutingBack);
        ButtonAddTestReactiveCommand = ReactiveCommand.Create(() =>
        {
            test.Name = Name;
            test.Description = Description;
            test.Script = EditorTest.Text;
            
            RoutingBack();
        });
    }
    
    internal CreateTestUserControlViewModel(IScreen screen, ObservableCollection<TestVM> tests)
        : base(screen)
    {
        EditorTest.Text = "#!/bin/bash";
        ReactiveCommandBackRoute = ReactiveCommand.Create(RoutingBack);
        ButtonAddTestReactiveCommand = ReactiveCommand.Create(() =>
        {
            tests.Add(new TestVM()
            {
                Name = Name,
                Description = Description,
                Script = EditorTest.Text,
                GlobalScreen = screen,
                GlobalRoutingState = this.GlobalRoutingState
            });
            RoutingBack();
        });
    }
}