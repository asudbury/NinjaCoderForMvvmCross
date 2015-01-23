// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.ViewModels
{
	using System.ComponentModel;
	using System.Runtime.CompilerServices;

    /// <summary>
    /// Defines the BaseViewModel type.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
	{
        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
		protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
			{
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
        }

		/// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="backingStore">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        protected void SetProperty<T>(
            ref T backingStore,
            T value,
            [CallerMemberName] string propertyName = null)
        {
            if (Equals(backingStore, value))
            {
                return;
            }

            backingStore = value;

            this.OnPropertyChanged(propertyName);
        }
    }
}
