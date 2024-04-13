using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaTestsLinux.ViewModels;

namespace AvaloniaTestsLinux;

public partial class MainUserControl : ReactiveUserControl<MainUserControlViewModel>
{
    public MainUserControl()
    {
        InitializeComponent();
    }
}