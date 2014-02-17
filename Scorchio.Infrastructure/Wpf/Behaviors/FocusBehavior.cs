// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the FocusBehavior type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf.Behaviors
{
    using System.Windows;
    using System.Windows.Controls;

    using System.Windows.Interactivity;

    public class FocusBehavior : Behavior<Control>
    {
        /// <summary>
        /// Called when [attached].
        /// </summary>
        protected override void OnAttached()
        {
            AssociatedObject.GotFocus += (sender, args) => IsFocused = true;
            AssociatedObject.LostFocus += (sender, a) => IsFocused = false;

            AssociatedObject.Loaded += (o, a) =>
            {
                if (HasInitialFocus || IsFocused)
                {
                    AssociatedObject.Focus();
                }
            };

            base.OnAttached();
        }

        /// <summary>
        /// The is focused property
        /// </summary>
        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.Register(
                "IsFocused",
                typeof(bool),
                typeof(FocusBehavior),
                new PropertyMetadata(false,
                    (d, e) =>
                    {
                        if ((bool)e.NewValue) ((FocusBehavior)d).AssociatedObject.Focus();
                    }));

        /// <summary>
        /// Gets or sets a value indicating whether [is focused].
        /// </summary>
        public bool IsFocused
        {
            get { return (bool)GetValue(IsFocusedProperty); }
            set { SetValue(IsFocusedProperty, value); }
        }

        /// <summary>
        /// The has initial focus property.
        /// </summary>
        public static readonly DependencyProperty HasInitialFocusProperty =
            DependencyProperty.Register(
                "HasInitialFocus",
                typeof(bool),
                typeof(FocusBehavior),
                new PropertyMetadata(false, null));

        /// <summary>
        /// Gets or sets a value indicating whether [has initial focus].
        /// </summary>
        public bool HasInitialFocus
        {
            get { return (bool)GetValue(HasInitialFocusProperty); }
            set { SetValue(HasInitialFocusProperty, value); }
        }
    }
}
