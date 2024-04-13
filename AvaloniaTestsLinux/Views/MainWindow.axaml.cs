using Avalonia.Controls;
using AvaloniaTestsLinux.Models.Utils;

namespace AvaloniaTestsLinux.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        SQLite.Open();
        this.Closing += (s, e) 
            => SQLite.Close();
        InitializeComponent();
    }
}