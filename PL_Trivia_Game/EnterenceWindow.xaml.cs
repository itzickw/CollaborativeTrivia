using BL;
using PL1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for Enterence.xaml
    /// </summary>
    public partial class EnterenceWindow : Window
    {
        private BlInterface bl;
        public EnterenceWindow(BlInterface bl)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.bl = bl;

        }

        private void EnterenceClick(object sender, RoutedEventArgs e)
        {
            if(!int.TryParse(passw.Password, out int res))
            {
                MessageBox.Show("אנא הכנס לסיסמה מספרים בלבד");
                return;
            }            

            if (bl.UserExist(userName.Text , res))
            {
                BL.Entities.User user = bl.GetUser(userName.Text);                
                new UserWindow(bl, user).Show();
                Close();
                return;
            }
            MessageBox.Show("שם משתמש או סיסמה שגויים");
            return ;
        }
    }
}
