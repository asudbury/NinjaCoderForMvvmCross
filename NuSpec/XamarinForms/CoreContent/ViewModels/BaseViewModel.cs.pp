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
        /// The title
        /// </summary>
        private string title;

        /// <summary>
        /// The is busy indicator.
        /// </summary>
        private bool isBusy;

        /// <summary>
        /// The is not busy indicator.
        /// </summary>
        private bool isNotBusy = true;

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }
            set
            {
                this.SetProperty(ref this.isBusy, value);
                this.IsNotBusy = !this.isBusy;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is not busy.
        /// </summary>
        public bool IsNotBusy
        {
            get { return this.isNotBusy; }
            private set { this.SetProperty(ref this.isNotBusy, value); }
        }

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
