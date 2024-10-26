using BL;
using BL.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
//using System.Reflection.Emit;
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
    /// Interaction logic for QuestionaryEditWindow.xaml
    /// </summary>
    public partial class QuestionaryEditWindow : Window
    {
        private BlInterface bl;
        string category;        
        Questionary questionary;
        Question question;
        public QuestionaryEditWindow(BlInterface bl, string c, User user)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.bl = bl;
            category = c;
            title.Content = c;
            levelFilter.ItemsSource = Enumerable.Range(0, bl.GetLevelColorsList().Count()).ToList();            
            questionsListView.Items.Clear();
            new Menu(this, mainGrid, bl, user);
        }        
        private void LevelFilterChanged(object sender, SelectionChangedEventArgs e)
        {
            if(levelFilter.SelectedItem != null)
            {
                title.Background = (Brush)new BrushConverter().ConvertFromString(bl.GetLevelColorsList()[(int)levelFilter.SelectedItem]);
                DisplayQuestionary();
            }
        }

        private void DisplayQuestionary()
        {
            questionary = bl.GetQuestionary(category, (int)levelFilter.SelectedItem);
            questionsListView.ItemsSource = questionary?.Questions;
        }

        private void QuestionSelected(object sender, SelectionChangedEventArgs e)
        {
            // קוד לפתיחת חלון עריכה עבור השאלה שנבחרה
            if (questionsListView.SelectedItem is Question selectedQuestion)
            {
                question = questionary.Questions.First(x => x.Id == selectedQuestion.Id);                
                editQuestionGrid.Visibility = Visibility.Visible;
                questionContent.Text = question.QuestionContent;
                answerContent.Text = question.AnswerContent;
                optionalAnswers.Children.Clear();
                if (question.IsClosedQuestion)
                    OptionalAnswersDisplay();                                
            }
        }

        private void OptionalAnswersDisplay()
        {            
            foreach (Answer a in question.OptionalAnswers)
            {
                StackPanel stack = new StackPanel();
                stack.Tag = question.OptionalAnswers.IndexOf(a).ToString();
                Label label = new Label() { Content = "תשובה אפשרית" };
                TextBox textBox = new TextBox() { Text = a.AnswerContent };
                stack.Children.Add(label);
                stack.Children.Add(textBox);
                optionalAnswers.Children.Add(stack);
            }
        }

        private void QuestionUpdate(object sender, RoutedEventArgs e)
        {
            question.QuestionContent = questionContent.Text;
            question.AnswerContent = answerContent.Text;
            if (question.IsClosedQuestion)
            {
                foreach (StackPanel stack in optionalAnswers.Children)
                    foreach (var child in stack.Children)
                        if (child is TextBox t)
                            question.OptionalAnswers[int.Parse(stack.Tag.ToString())].AnswerContent = t.Text;
            }

            bl.UpdateQuestion(question);
            editQuestionGrid.Visibility = Visibility.Hidden;
            DisplayQuestionary();
        }

        private void DeleteQuestionClick(object sender, RoutedEventArgs e)
        {
            ConfirmDeleteQuestionButton.Visibility = Visibility.Visible;
        }

        private void ConfirmDeleteQuestionClick(object sender, RoutedEventArgs e)
        {
            bl.DeleteQuestion(question.Id);
            editQuestionGrid.Visibility = Visibility.Hidden;
            DisplayQuestionary();
        }

        private void AddingQuestionClick(object sender, RoutedEventArgs e)
        {
            if (levelFilter.SelectedItem != null)
            {
                //addingQuestionGrid.Visibility = Visibility.Visible;
                var addingWindow = new AddingNewQuestionWindow(bl, category, (int)levelFilter.SelectedItem);
                addingWindow.ShowDialog();
                DisplayQuestionary();
            }
        }               
    }
}

