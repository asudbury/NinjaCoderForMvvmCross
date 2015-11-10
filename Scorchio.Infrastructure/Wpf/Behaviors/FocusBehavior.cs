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

    /// <summary>
    ///  Defines the FocusBehavior type.
    /// </summary>
    public class FocusBehavior : Behavior<Control>
    {
        /// <summary>
        /// Called when [attached].
        /// </summary>
        protected override void OnAttached()
        {
            this.AssociatedObject.GotFocus += (sender, args) => this.IsFocused = true;
            this.AssociatedObject.LostFocus += (sender, a) => this.IsFocused = false;

            this.AssociatedObject.Loaded += (o, a) =>
            {
                if (this.HasInitialFocus || this.IsFocused)
                {
                    this.AssociatedObject.Focus();
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
                        if ((bool)e.NewValue)((FocusBehavior)d).AssociatedObject.Focus();
                    }));

        /// <summary>
        /// Gets or sets a value indicating whether [is focused].
        /// </summary>
        public bool IsFocused
        {
            get { return (bool)this.GetValue(IsFocusedProperty); }
            set { this.SetValue(IsFocusedProperty, value); }
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
            get { return (bool)this.GetValue(HasInitialFocusProperty); }
            set { this.SetValue(HasInitialFocusProperty, value); }
        }
    }
}
