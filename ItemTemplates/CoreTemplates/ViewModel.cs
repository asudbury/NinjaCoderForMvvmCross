// -----------------------------------------------------------------------
// <summary>
//   Defines the $safeitemrootname$ type.
// </summary>
// -----------------------------------------------------------------------

namespace CoreTemplates
{
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    /// <summary>
    ///   Defines the $safeitemrootname$ type.
    /// </summary>
    public class ViewModel : MvxViewModel
    {
        /// <summary>
        /// Backing field for my property.
        /// </summary>
        private string myProperty;

        /// <summary>
        /// Gets the hello.
        /// </summary>
        public string Hello
        {
            get { return "Hello MvvmCross from the Ninja Coder!"; }
        }

        //// An example of a Dependency property.
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
        //// Note the viewModel inside of ShowViewModel needs to change!
        /// <summary>
        /// Gets My Command.
        /// </summary>
        public ICommand MyCommand
        {
            get { return new MvxCommand(() => this.ShowViewModel<$safeitemrootname$>()); }
        }
    }
}
