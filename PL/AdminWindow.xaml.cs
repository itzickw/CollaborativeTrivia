using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private BlInterface bl;
        private BL.Entities.User user;

        public AdminWindow(BlInterface bl, BL.Entities.User user)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.bl = bl;
            this.user = user;
            new Menu(this, mainGrid, bl, user);
            CreateLevelsColorButtons();
            CreateCategoriesButtons();
        }

        private void CreateLevelsColorButtons()
        {
            levelsColor.Children.Clear();
            int level = 0;
            foreach (string color in bl.GetLevelColorsList())
            {

                Button button = new Button
                {
                    Content = "שלב " + level,
                    Margin = new Thickness(5),
                    //Style = Desines.GetRoundButtonStyle(),                    
                };

                button.Background = (Brush)new BrushConverter().ConvertFromString(color);
                levelsColor.Children.Add(button);
                level++;
            }
        }

        private void CreateCategoriesButtons()
        {
            CategoriesButtons.Children.Clear();

            if (user.UserCategories.Count > 0)
            {
                foreach (string c in user.UserCategories)
                {
                    Button button = new Button
                    {
                        Content = c,
                        Margin = new Thickness(5),
                        Style = Desines.GetRoundButtonStyle()
                    };

                    button.Click += QuestionaryEdit;
                    CategoriesButtons.Children.Add(button);
                }
            }

            else  CategoriesButtons.Children.Add(new System.Windows.Controls.Label() { Content = "עדיין לא יצרת שום קטגוריה" });
        }

        private void QuestionaryEdit(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            new QuestionaryEditWindow(bl, (string)button.Content, user).Show();
            Close();
            return;
        }

        private void AddCategoryClick(object sender, RoutedEventArgs e)
        {
            bl.AddCategory(CategoryName.Text, user.Name);
            CategoryName.Text = string.Empty;
            user = bl.GetUser(user.Name);
            CreateCategoriesButtons();
        }

        private void DefineLevelClick(object sender, RoutedEventArgs e)
        {
            try
            {
                BrushConverter converter = new BrushConverter();
                Brush brush = (Brush)converter.ConvertFromString(LevelColor.Text);

                if (int.TryParse(LevelNumber.Text, out int level) && level <= bl.GetLevelColorsList().Count)
                {
                    bl.DefineLevelColor(level, LevelColor.Text);
                    LevelNumber.Text = string.Empty;
                    LevelColor.Text = string.Empty;
                    CreateLevelsColorButtons();
                }
                else MessageBox.Show("אנא אכנס מספר שלב חוקי");
            }
            catch
            {
                MessageBox.Show("אנא אכנס שם צבע חוקי");
            }


        }
    }
}
