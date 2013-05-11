// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the OptionsPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Presenters
{
    using System;

    using Microsoft.Win32;

    using NinjaCoder.MvvmCross.Views.Interfaces;

    /// <summary>
    ///    Defines the OptionsPresenter type.
    /// </summary>
    public class OptionsPresenter
    {
        /// <summary>
        /// The view.
        /// </summary>
        private readonly IOptionsView view;

        /// <summary>
        /// Gets the log file registry setting.
        /// </summary>
        private string LogFileRegistrySetting
        {
            get
            {
                return @"SOFTWARE\Scorchio Limited\Ninja Coder for MvvmCross\LogToFile";
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsPresenter" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public OptionsPresenter(IOptionsView view)
        {
            this.view = view;    
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        public void LoadSettings()
        {
            string value = (string)Registry.CurrentUser.GetValue(this.LogFileRegistrySetting, "N");

            this.view.LogToFile = value != "N";

            this.view.LogFilePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\ninja-coder-for-mvvmcross.log";
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings()
        {
            string value = "N";

            if (this.view.LogToFile)
            {
                value = "Y";
            }

            Registry.CurrentUser.SetValue(this.LogFileRegistrySetting, value);
        }
    }
}
