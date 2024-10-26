using BL;
using PL;
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

namespace PL1
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        BlInterface bl;
        public RegistrationWindow(BlInterface bl)
        {
            InitializeComponent();
            this.bl = bl;
        }

        private void RegisteredClick(object sender, RoutedEventArgs e)
        {
            string name = userName.Text;

            if (!int.TryParse(passw.Text, out int result) || !int.TryParse(checkingPassw.Text, out int result1))
            {
                MessageBox.Show("אנא הכנס לסיסמה, מספרים בלבד");
                return;
            }
            int password = int.Parse(passw.Text);
            int password2 = int.Parse(checkingPassw.Text);
            if (bl.UserNameExist(name))
            {
                MessageBox.Show("שם משתמש כבר קיים");
                return;
            }
            if(password != password2)
            {
                MessageBox.Show("הסיסמאות לא תואמות");
                return;
            }

            BL.Entities.User user = new BL.Entities.User()
            {
                Name = name,
                Password = password,
                States = null
            };
            bl.AddUser(user);
            new UserWindow(bl, user).Show();
            Close();
        }
    }
}
