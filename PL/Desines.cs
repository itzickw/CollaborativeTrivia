using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PL1
{
    internal class Desines
    {
        public static Style GetRoundButtonStyle()
        {
            Style roundButtonStyle = new Style(typeof(Button));

            // עיגול הקצוות של הכפתור
            roundButtonStyle.Setters.Add(new Setter(Button.WidthProperty, 100.0));
            roundButtonStyle.Setters.Add(new Setter(Button.HeightProperty, 100.0));
            roundButtonStyle.Setters.Add(new Setter(Button.BorderBrushProperty, Brushes.Black));
            roundButtonStyle.Setters.Add(new Setter(Button.BorderThicknessProperty, new Thickness(2)));

            // קביעת תבנית של הכפתור
            ControlTemplate template = new ControlTemplate(typeof(Button));
            FrameworkElementFactory border = new FrameworkElementFactory(typeof(Border));
            border.SetValue(Border.CornerRadiusProperty, new CornerRadius(50));
            border.SetValue(Border.BackgroundProperty, Brushes.LightBlue);

            FrameworkElementFactory contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenter.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            contentPresenter.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);

            border.AppendChild(contentPresenter);
            template.VisualTree = border;
            roundButtonStyle.Setters.Add(new Setter(Button.TemplateProperty, template));

            return roundButtonStyle;
        }

        public static Style GetGeneralSet()
        {
            Style style = new Style(typeof(Control));
            style.Setters.Add(new Setter(Control.HorizontalAlignmentProperty, HorizontalAlignment.Center));
            style.Setters.Add(new Setter(Control.VerticalAlignmentProperty, VerticalAlignment.Center));
            style.Setters.Add(new Setter(Control.MarginProperty, new Thickness(5)));            
            return style;
        }

        public static Style GetStackStyle()
        {
            Style style = new Style(typeof(StackPanel));
            style.Setters.Add(new Setter(Control.HorizontalAlignmentProperty, HorizontalAlignment.Center));
            style.Setters.Add(new Setter(Control.VerticalAlignmentProperty, VerticalAlignment.Center));
            style.Setters.Add(new Setter(Control.MarginProperty, new Thickness(5)));            
            return style;
        }
    }
}
