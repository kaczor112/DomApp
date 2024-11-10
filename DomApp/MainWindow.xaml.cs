using MainComponents;
using Model;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DomApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ApplicationSettings.CheckRefresh();

            InitializeComponent();

            RunLoop = true;
            _ = Task.Run(RefreshLoop);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sender is Slider slider)
            {
                _ = SendRequest(slider.Name, e.NewValue == 1);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            RunLoop = false;
        }
    }
}