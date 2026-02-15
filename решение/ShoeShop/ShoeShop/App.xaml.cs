using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ShoeShop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            EventManager.RegisterClassHandler(typeof(Window),
                Window.LoadedEvent,
                new RoutedEventHandler((sender, args) =>
                {
                    if (sender is Window window && window.Icon == null)
                    {
                        window.Icon = BitmapFrame.Create(new Uri("Resources/Icon.ico", UriKind.Relative));
                    }
                }));
        }
    }

}
