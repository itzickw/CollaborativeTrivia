using BL;
using BL.Entities;
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
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        BlInterface bl;
        private User user;

        public UserWindow(BlInterface bl, User user)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.bl = bl;
            this.user = user;
            Menu menu = new Menu(this, mainGrid, bl, user);
            CreateCategoriesButtons();
        }

        private void CreateCategoriesButtons()
        {
            foreach (string c in bl.GetCategoriesList())
            {
                Button button = new Button
                {
                    Content = c,                    
                    Margin = new Thickness(5),
                    Style = Desines.GetRoundButtonStyle()
                };

                button.Click += QuestionaryDoing;
                categoriesButtons.Children.Add(button);
            }
        }

        private void QuestionaryDoing(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            new UserQuestionaryWindow(bl, (string)button.Content, user).Show();
            Close();
            return;
        }
    }
}
