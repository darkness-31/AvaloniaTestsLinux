using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaTestsLinux.ViewModels;

namespace AvaloniaTestsLinux.Views.UserControls;

public partial class CreateTestUserControl : ReactiveUserControl<CreateTestUserControlViewModel>
{
    public CreateTestUserControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}