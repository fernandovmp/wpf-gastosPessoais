using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace wpf_gastosPessoais.Views
{
    public class WatermarkAdorner : Adorner
    {

        public WatermarkAdorner(UIElement adornedElement, object watermark) : base(adornedElement)
        {
            IsHitTestVisible = false;
            presenter = new ContentPresenter();
            presenter.Opacity = 0.7;
            presenter.Content = watermark;
            presenter.Margin = new Thickness(Control.Margin.Left + Control.Padding.Left, 
                Control.Margin.Top + Control.Padding.Top, 0, 0);
        }

        private readonly ContentPresenter presenter;

        private Control Control
        {
            get { return (Control)this.AdornedElement; }
        }

        protected override Visual GetVisualChild(int index)
        {
            return presenter;
        }

        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            presenter.Measure(Control.RenderSize);
            return Control.RenderSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            presenter.Arrange(new Rect(finalSize));
            return finalSize;
        }
    }

}

