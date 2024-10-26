using BL;
using BL.Entities;
using System;
using System.Collections;
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
    /// Interaction logic for UserQuestionaryWindow.xaml
    /// </summary>
    public partial class UserQuestionaryWindow : Window
    {
        private BlInterface bl;
        private User User;
        private string Category;
        private int Level;
        private Questionary questionary;

        public UserQuestionaryWindow(BL.BlInterface bl, string category, User user)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.bl = bl;
            this.User = user;
            Category = category;
            categoryLabel.Content = category;
            new Menu(this, mainGrid, bl, user);
            UpdateWindow();
        }

        private void UpdateWindow()
        {
            User = bl.GetUser(User.Name);
            Level = User.States.FirstOrDefault(s => s.Category == Category).Level;
            levelFilter.ItemsSource = Enumerable.Range(0, Level + 1).ToList();
            levelFilter.SelectedIndex = Level;
            QuestionsDisplay();
        }

        private void QuestionsDisplay()
        {
            questionsDisplay.Children.Clear();
            questionary = bl.GetQuestionary(Category, Level);
            foreach (Question q in questionary.Questions)
            {
                StackPanel stack = new StackPanel() { Tag = q.Id, Orientation = Orientation.Vertical, Style = Desines.GetStackStyle(), Width = 350, Height = 150, Background = (Brush)new BrushConverter().ConvertFromString(bl.GetLevelColorsList()[Level]), HorizontalAlignment = HorizontalAlignment.Center };
                Label label = new Label() { Content = q.QuestionContent, Style = Desines.GetGeneralSet(), Tag = q.AnswerContent, FontWeight = FontWeights.UltraBold };
                stack.Children.Add(label);
                if (User.States.FirstOrDefault(s => s.Category == Category).AnswerdQuestion.Any(x => x.Id == q.Id))
                {
                    Label label1 = new Label() { Content = "ענית נכון", Style = Desines.GetGeneralSet() };
                    Label label2 = new Label() { Content = q.AnswerContent, Style = Desines.GetGeneralSet() };
                    stack.Children.Add(label1);
                    stack.Children.Add(label2);
                }

                else
                {
                    // יצירת Grid להצבת התשובות והכפתור בשורה אחת מתחת לשאלה
                    Grid grid = new Grid() { Tag = q.Id, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Center };
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });

                    // הצגת תשובות אפשריות אם השאלה סגורה
                    if (q.IsClosedQuestion)
                    {
                        StackPanel optionalAnswersStack = GetOptionalAnswersStack(q.OptionalAnswers, q.AnswerContent);
                        Grid.SetColumn(optionalAnswersStack, 0);
                        grid.Children.Add(optionalAnswersStack);
                    }
                    else
                    {
                        // במידה והשאלה פתוחה - תיבת טקסט
                        TextBox textBox = new TextBox() { Width = 100, Height = 20, Style = Desines.GetGeneralSet() };
                        Grid.SetColumn(textBox, 0);
                        grid.Children.Add(textBox);
                    }

                    // כפתור הכניסת תשובה בצד שמאל
                    Button button = new Button() { Content = "הכנס\rתשובה", Height = 60, Style = Desines.GetGeneralSet(), Tag = q.AnswerContent, Width = 60 };
                    button.Click += AnswerQuestionClick;
                    Grid.SetColumn(button, 1);
                    grid.Children.Add(button);

                    // הוספת ה-Grid ל-StackPanel הראשי
                    stack.Children.Add(grid);
                }

                Border border = new Border() { BorderBrush = Brushes.Black, BorderThickness = new Thickness(2), Child = stack, Margin = new Thickness(5), CornerRadius = new CornerRadius(5), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
                questionsDisplay.Children.Add(border);
            }

        }

        private StackPanel GetOptionalAnswersStack(List<Answer> optionalAnswers, string answerContent)
        {
            var allAnswers = optionalAnswers.Select(a => a.AnswerContent).ToList();
            allAnswers.Add(answerContent);

            Random random = new Random();
            allAnswers = allAnswers.OrderBy(x => random.Next()).ToList();

            StackPanel stack = new StackPanel() { Style = Desines.GetStackStyle(), HorizontalAlignment = HorizontalAlignment.Center };
            foreach (var answer in allAnswers)
            {
                RadioButton radioButton = new RadioButton() { Margin = new Thickness(1), HorizontalAlignment = HorizontalAlignment.Stretch };

                // יצירת TextBlock עם TextWrapping 
                TextBlock textBlock = new TextBlock()
                {
                    Text = answer,
                    TextWrapping = TextWrapping.Wrap, // מאפשר שבירת שורות בטקסט ארוך
                    MaxWidth = 300 // או כל רוחב שתתאים בהתאם לחלון שלך
                };

                radioButton.Content = textBlock;
                stack.Children.Add(radioButton);
            }

            return stack;
        }


        private void AnswerQuestionClick(object sender, RoutedEventArgs e)
        {
            // קבלת הכפתור ששיגר את האירוע
            Button button = (Button)sender;

            // קבלת ה-StackPanel שבו הכפתור נמצא (ה-Parent של הכפתור)
            Grid grid = button.Parent as Grid;
            string answer = GetAnswerFromStack(grid);
            string userAnswer = GetUserAnswerFormStack(grid);
            if (answer == userAnswer)
            {
                bl.UpdateUserQuestionState(User.Name, (int)grid.Tag);
                int level = Level;
                UpdateWindow();
                MessageBox.Show("כל הכבוד! תשובה נכונה");
                if (Level == level + 1)
                    MessageBox.Show("כל הכבוד! עברת שלב");
                return;
            }

            UpdateWindow();
            MessageBox.Show("תשובה לא נכונה. נסה שוב");
        }

        private string GetUserAnswerFormStack(Grid grid)
        {
            foreach (var child in grid.Children)
            {
                if (child is TextBox textBox) // אם השאלה היא פתוחה                
                    return textBox.Text;

                else if (child is StackPanel innerStack) // אם השאלה היא סגורה
                {
                    foreach (var radioChild in innerStack.Children)
                        if (radioChild is RadioButton radioButton && radioButton.IsChecked == true)
                            return ((TextBlock)radioButton.Content).Text;
                }
            }
            return null;
        }

        private string GetAnswerFromStack(Grid grid)
        {
            foreach (var child in grid.Children)
                if (child is Button button)
                    return button.Tag.ToString();

            return null;
        }

        Style style = new Style() { };

        private void LevelSelected(object sender, SelectionChangedEventArgs e)
        {
            if (levelFilter.SelectedItem != null)
            {
                Level = levelFilter.SelectedIndex;
                QuestionsDisplay();
            }
        }
    }
}
