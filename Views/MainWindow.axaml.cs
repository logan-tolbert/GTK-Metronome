using Avalonia.Controls;

using MetronomeApp.ViewModels;

namespace MetronomeApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    protected override void OnClosing(WindowClosingEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            vm.Dispose();
        }
        base.OnClosing(e);
    }
}