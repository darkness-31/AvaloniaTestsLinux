using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaTestsLinux.ViewModels;

namespace AvaloniaTestsLinux.Views.UserControls;

public partial class GroupUserControl : ReactiveUserControl<GroupUserControlViewModel>
{
    public GroupUserControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}