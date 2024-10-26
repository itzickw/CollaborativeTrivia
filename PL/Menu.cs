using PL;
using PL1;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

public class Menu
{
    private BL.BlInterface BlInterface;
    private BL.Entities.User user;
    private Window window;
    private Grid grid;

    Button exitButton = new Button() { Content = "יציאה", Height = 20, Width = 100 };
    Button categoryEditButton = new Button() { Content = "עריכת קטגוריות", Height = 20, Width = 150 };
    Button questionarySelectionButton = new Button() { Content = "בחירת שאלון", Height = 20, Width = 150 };

    public Menu(Window window, Grid grid, BL.BlInterface blInterface, BL.Entities.User user)
    {
        BlInterface = blInterface;
        this.user = user;
        this.window = window;
        this.grid = grid;

        // הצמדת האירועים לכפתורים
        exitButton.Click += ExitClick;
        categoryEditButton.Click += CategoryEditClick;
        questionarySelectionButton.Click += QuestionarySelectionClick;

        // יצירת Grid עם שתי שורות
        Grid menuGrid = new Grid() { HorizontalAlignment = HorizontalAlignment.Right };
        menuGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });  // שורה עבור הלייבל
        menuGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });  // שורה עבור הכפתורים

        Label label = new Label() { Content = "תפריט ניווט", HorizontalAlignment = HorizontalAlignment.Center };
        menuGrid.Children.Add(label);
        Grid.SetRow(label, 0);  // מיקום הלייבל בשורה 0

        // יצירת StackPanel עבור הכפתורים עם יישור לימין
        StackPanel buttonPanel = new StackPanel() { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };

        // הוספת הכפתורים ל-StackPanel
        buttonPanel.Children.Add(exitButton);
        buttonPanel.Children.Add(categoryEditButton);
        buttonPanel.Children.Add(questionarySelectionButton);

        // הוספת ה-StackPanel ל-Grid בשורה 1
        menuGrid.Children.Add(buttonPanel);
        Grid.SetRow(buttonPanel, 1);  // מיקום הכפתורים בשורה 1

        // הוספת ה-Grid לתוך Border ולאחר מכן הוספה ל-Grid הראשי
        Border border = new Border() { Child = menuGrid, BorderThickness = new Thickness(2), Margin = new Thickness(3), BorderBrush = Brushes.Black, Width = 400};
        grid.Children.Add(border);
        Grid.SetRow(border, 0);

        BlInterface = blInterface;
        this.user = user;
    }

    private void ExitClick(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        window.Close();
    }

    private void CategoryEditClick(object sender, RoutedEventArgs e)
    {
        // פתיחת חלון עריכת קטגוריות
        new AdminWindow(BlInterface, user).Show();
        window.Close();
    }

    private void QuestionarySelectionClick(object sender, RoutedEventArgs e)
    {
        // פתיחת חלון בחירת שאלון
        new UserWindow(BlInterface, user).Show();
        window.Close();
    }
}
