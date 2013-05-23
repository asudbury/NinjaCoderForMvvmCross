// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the FirstViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoreTemplate.ViewModels
{
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    /// <summary>
    /// Define the FirstViewModel type.
    /// </summary>
    public class FirstViewModel : BaseViewModel
    {
        /// <summary>
        /// Backing field for my property.
        /// </summary>
        private string myProperty = "Hello MvvmCross from the Ninja Coder!";

        /// <summary>
        /// Gets or sets my property.
        /// </summary>
        public string MyProperty
        {
            get
            {
                return this.myProperty;
            }

            set
            {
                this.myProperty = value;
                this.RaisePropertyChanged(() => this.MyProperty);
            }
        }

        //// An example of a command and how to navigate to another view model
        //// Note the ViewModel inside of ShowViewModel needs to change!
        /// <summary>
        /// Gets My Command.
        /// </summary>
        public ICommand MyCommand
        {
            get { return new MvxCommand(() => this.ShowViewModel<$safeitemrootname$>()); }
        }
    }
}
