// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.ViewModels
{
	using System.ComponentModel;

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
    }
}
