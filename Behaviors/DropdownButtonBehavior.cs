using System.Windows.Controls;
using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace wpf_gastosPessoais.Behaviors
{
    public class DropdownButtonBehavior : Behavior<Button>
    {
        private bool isContextMenuOpen;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AddHandler(Button.ClickEvent, new RoutedEventHandler(AssociatedObjectClick), true);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(AssociatedObjectClick));
        }

        private void AssociatedObjectClick(object sender, RoutedEventArgs args)
        {
            Button button = (Button)sender;
            if(button != null && button.ContextMenu != null && !isContextMenuOpen)
            {
                button.ContextMenu.AddHandler(ContextMenu.ClosedEvent, new RoutedEventHandler(ContextMenuClosed), true);
                button.ContextMenu.PlacementTarget = button;
                button.ContextMenu.IsOpen = true;
                isContextMenuOpen = true;
            }
        }

        private void ContextMenuClosed(object sender, RoutedEventArgs args)
        {
            isContextMenuOpen = false;
            ContextMenu menu = (ContextMenu)sender;
            if(menu != null)
            {
                menu.RemoveHandler(ContextMenu.ClosedEvent, new RoutedEventHandler(ContextMenuClosed));
            }
        }
    }
}
