using BL;
using PL1;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BlInterface bl = BlFactory.GetBlInterface();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegisteredUserClick(object sender, RoutedEventArgs e)
        {
            new EnterenceWindow(bl).Show(); 
            Close();
        }

        private void NewUserClick(object sender, RoutedEventArgs e)
        {
            new RegistrationWindow(bl).Show();
            Close();
        }
    }
}