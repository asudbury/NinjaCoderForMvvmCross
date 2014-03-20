// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the DirectoryPickerViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf.ViewModels
{
    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    using Scorchio.Infrastructure.Services;

    /// <summary>
    ///  Defines the DirectoryPickerViewModel type.
    /// </summary>
    public class DirectoryPickerViewModel : BaseViewModel
    {
        /// <summary>
        /// The folder browser dialog service.
        /// </summary>
        private readonly IFolderBrowserDialogService folderBrowserDialogService;

        /// <summary>
        /// The label binding key.
        /// </summary>
        private string labelBindingKey;

        /// <summary>
        /// The open binding key.
        /// </summary>
        private string openBindingKey;

        /// <summary>
        /// The directory.
        /// </summary>
        private string directory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryPickerViewModel" /> class.
        /// </summary>
        /// <param name="folderBrowserDialogService">The folder browser dialog service.</param>
        public DirectoryPickerViewModel(IFolderBrowserDialogService folderBrowserDialogService)
        {
            this.folderBrowserDialogService = folderBrowserDialogService;
        }

        /// <summary>
        /// Gets or sets the label binding key.
        /// </summary>
        public string LabelBindingKey
        {
            get { return this.labelBindingKey; }
            set { this.SetProperty(ref this.labelBindingKey, value); }
        }

        /// <summary>
        /// Gets or sets the open binding key.
        /// </summary>
        public string OpenBindingKey
        {
            get { return this.openBindingKey; }
            set { this.SetProperty(ref this.openBindingKey, value); }
        }

        /// <summary>
        /// Gets or sets the directory.
        /// </summary>
        public string Directory
        {
            get { return this.directory; }
            set { this.SetProperty(ref this.directory, value); }
        }

        /// <summary>
        /// Gets the browser folders command.
        /// </summary>
        public ICommand BrowserFoldersCommand
        {
            get { return new RelayCommand(this.BrowserFolders); }
        }

        /// <summary>
        /// Gets the open folder command.
        /// </summary>
        public ICommand OpenFolderCommand
        {
            get { return new RelayCommand(this.OpenFolder); }
        }

        /// <summary>
        /// Browsers the folders.
        /// </summary>
        public void BrowserFolders()
        {
            this.folderBrowserDialogService.RootFolder = Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialogService.ShowNewFolderButton = true;

            bool? returnValue = this.folderBrowserDialogService.ShowDialog();

            if (returnValue.HasValue &&
                returnValue == true)
            {
                this.Directory = this.folderBrowserDialogService.SelectedPath;
            }
        }

        /// <summary>
        /// Opens the folder.
        /// </summary>
        public void OpenFolder()
        {
            Process.Start(this.Directory); 
        }
    }
}
