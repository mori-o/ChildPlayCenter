using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Navigation;

namespace ChildPlayCenter;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        NavigationWindow mainWindow = new NavigationWindow
        {
            Title = "Play Center",
            Width = 800,
            Height = 450,
            WindowStartupLocation = WindowStartupLocation.CenterScreen
        };
        mainWindow.Navigate(new Uri("Pages/LoginPage.xaml", UriKind.Relative));
        mainWindow.Show();
    }
}

