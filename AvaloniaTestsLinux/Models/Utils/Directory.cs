using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;

namespace AvaloniaTestsLinux.Models.Utils;

public static class DirectoryScripts
{
    private static readonly FilePickerFileType[] FilePickers = new[]
    {
        new FilePickerFileType("All Script")
        {
            Patterns = new[] { "*.sh", "*.bash" },
            MimeTypes = new[] { "scripts/*" }
        }
    };
    
    internal static async Task<IStorageFile?> DoOpenFilePickerAsync()
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
            desktop.MainWindow?.StorageProvider is not { } provider)
            throw new NullReferenceException("Отсутствует проводник по-умолчанию");

        var files = await provider.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = "Open Text File",
            AllowMultiple = false,
            FileTypeFilter = FilePickers
        });

        return files?.Count >= 1 ? files[0] : null;
    }

    private static async Task<IStorageFile?> DoSaveFilePickerAsync()
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
            desktop.MainWindow?.StorageProvider is not { } provider)
            throw new NullReferenceException("Отсутствует проводник по-умолчанию");

        return await provider.SaveFilePickerAsync(new FilePickerSaveOptions()
        {
            Title = "Save Text File",
            FileTypeChoices = FilePickers
        });
    }
}