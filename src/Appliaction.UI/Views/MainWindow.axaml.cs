using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Ursa.Controls;
namespace Appliaction.UI.Views;

public partial class MainWindow : UrsaWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }
    protected override async Task<bool> CanClose()
    {
        var result = await MessageBox.ShowOverlayAsync("Are you sure you want to exit?\n您确定要退出吗？", "Exit", button: MessageBoxButton.YesNo);
        return result == MessageBoxResult.Yes;
    }
}