using System;
using System.Data.SqlTypes;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaEdit;
using AvaloniaTestsLinux.Models;
using AvaloniaTestsLinux.ViewModels;
using AvaloniaTestsLinux.Models.Utils;

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
    
    private async void Button_OpenOnClick(object? sender, RoutedEventArgs e)
    {
        var storage = await Dialogs.DoOpenFilePickerAsync();
        await using var file = File.Open(storage.Path.AbsolutePath, FileMode.OpenOrCreate);
        var buffer = new byte[file.Length];
        await file.ReadAsync(buffer);
        this.Find<TextEditor>("Editor")!.Text = Encoding.Default.GetString(buffer);
    }

    private async void Button_SaveOnClick(object? sender, RoutedEventArgs e)
    {
        var but = this.Find<TextEditor>("Editor");
        var storage = await Dialogs.DoSaveFilePickerAsync();
        if (storage == null) return;
        var buffer = Encoding.Default.GetBytes(but!.Text);
        await using var file = File.Open(storage.Path.AbsolutePath, FileMode.OpenOrCreate);
        await file.WriteAsync(buffer);
    }
}