using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace wpf_gastosPessoais.Views
{
    public static class Watermark
    {

        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.RegisterAttached(
            "Watermark", typeof(object), typeof(Watermark), 
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnWatermarkChanged)));

        public static object GetWatermark(Control control)
        {
            return control.GetValue(WatermarkProperty);
        }

        public static  void SetWatermark(Control control, object value)
        {
            control.SetValue(WatermarkProperty, value);
        }

        private static void OnWatermarkChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            Control control = (Control)dependencyObject;
            control.Loaded += ControlLoaded;
            if(dependencyObject is TextBox)
            {
                control.GotFocus += ControlGotFocus;
                control.LostFocus += ControlLoaded;
            }
        }

        private static void ControlGotFocus(object sender, RoutedEventArgs e)
        {
            Control control = (Control)sender;
            if (ShouldShowWatermark(control))
            {
                RemoveWatermark(control);
            }
        }

        private static void ControlLoaded(object sender, RoutedEventArgs e)
        {
            Control control = (Control)sender;
            if (ShouldShowWatermark(control))
            {
                ShowWatermark(control);
            }
        }

        private static bool ShouldShowWatermark(Control control)
        {
            DependencyProperty dependencyProperty = GetDependencyProperty(control);
            if (dependencyProperty == null) return true;
            return control.GetValue(dependencyProperty).Equals("");
        }

        private static void ShowWatermark(Control control)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(control);
            layer.Add(new WatermarkAdorner(control, GetWatermark(control)));
        }

        private static void RemoveWatermark(UIElement control)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(control);
            Adorner[] adorners = layer.GetAdorners(control);
            if (adorners == null) return;
            foreach (Adorner adorner in adorners)
            {
                if(adorner is WatermarkAdorner)
                {
                    adorner.Visibility = Visibility.Collapsed;
                    layer.Remove(adorner);
                }
            }
        }

        private static DependencyProperty GetDependencyProperty(Control control)
        {
            if (control is TextBoxBase)
                return TextBox.TextProperty;
            return null;
        }

    }
}
