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
    /// Interaction logic for AddingNewQuestionWindow.xaml
    /// </summary>
    public partial class AddingNewQuestionWindow : Window
    {
        private BlInterface bl;
        string Category; 
        int Level;
        Question question = null!;

        public AddingNewQuestionWindow(BlInterface bl, string category, int level)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.bl = bl;
            this.Category = category;
            this.Level = level;
        }

        private void TypeQuestionSelected(object sender, SelectionChangedEventArgs e)
        {
            if (questionType.SelectedItem != null)
            {
                if (questionType.SelectedIndex == 1)
                {
                    addingButton.Visibility = Visibility.Visible;
                    optionalAnswersNumberLabel.Visibility = Visibility.Hidden;
                    optionalAnswersNumber.Visibility = Visibility.Hidden;
                    optionalAnswersNumberGrid.Visibility = Visibility.Hidden;
                    question.IsClosedQuestion = false;
                }

                if (questionType.SelectedIndex == 0)
                {
                    addingButton.Visibility = Visibility.Hidden;
                    optionalAnswersNumberLabel.Visibility = Visibility.Visible;
                    optionalAnswersNumber.Visibility = Visibility.Visible;
                    question.IsClosedQuestion = true;
                }
            }
        }

        private void AnswersNumSelected(object sender, SelectionChangedEventArgs e)
        {
            if (questionType.SelectedItem != null)
            {
                optionalAnswersNumberGrid.Visibility = Visibility.Visible;
                optionalAnswersNumberGrid.Children.Clear();
                for (int i = 0; i <= optionalAnswersNumber.SelectedIndex; i++)
                {
                    Label label = new Label() { Content = "הכנס תשובה אפשרית " + (i + 1).ToString() };
                    TextBox textBox = new TextBox();
                    optionalAnswersNumberGrid.Children.Add(label);
                    optionalAnswersNumberGrid.Children.Add(textBox);
                }
                addingButton.Visibility = Visibility.Visible;
            }
        }

        private void AddingQuestionClick(object sender, RoutedEventArgs e)
        {
            question = new Question
            {
                Category = Category,
                Level = Level,
                QuestionContent = questionContent.Text,
                AnswerContent = answerContent.Text
            };

            if (question.IsClosedQuestion)
            {
                question.OptionalAnswers = new List<Answer>();
                foreach (var child in optionalAnswersNumberGrid.Children)
                {
                    if (child is TextBox textBox)
                    {
                        question.OptionalAnswers.Add(new Answer()
                        {
                            AnswerContent = textBox.Text
                        });
                    }
                }
            }
            bl.AddQuestion(question);
            Close();
        }
    }
}
