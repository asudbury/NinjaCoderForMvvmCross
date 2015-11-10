// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BindingProxy type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Scorchio.Infrastructure.Wpf
{
    using System.Windows;

    /// <summary>
    ///     Defines the BindingProxy type.
    /// </summary>
    public class BindingProxy : Freezable
    {
        // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
            "Data",
            typeof(object),
            typeof(BindingProxy),
            new UIPropertyMetadata(null));

        /// <summary>
        ///     Gets or sets the data.
        /// </summary>
        public object Data
        {
            get { return this.GetValue(DataProperty); }
            set { this.SetValue(DataProperty, value); }
        }

        /// <summary>
        ///     When implemented in a derived class, creates a new instance of the <see cref="T:System.Windows.Freezable" />
        ///     derived class.
        /// </summary>
        /// <returns>
        ///     The new instance.
        /// </returns>
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }
    }
}