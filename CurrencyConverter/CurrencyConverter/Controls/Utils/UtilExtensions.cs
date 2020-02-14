using System.Windows;

namespace CurrencyConverter.Controls.Utils
{
    public class UtilExtensions
    {
        public static bool GetAnimationAllowed(DependencyObject obj)
        {
            return (bool)obj.GetValue(AnimationAllowedProperty);
        }

        public static void SetAnimationAllowed(DependencyObject obj, bool value)
        {
            obj.SetValue(AnimationAllowedProperty, value);
        }

        // Using a DependencyProperty as the backing store for Enabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationAllowedProperty =
            DependencyProperty.RegisterAttached("AnimationAllowed", typeof(bool), typeof(UtilExtensions), new UIPropertyMetadata(false));

    }
}