// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the FrameworkSelectorViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddProjects
{
    using MahApps.Metro;
    using NinjaCoder.MvvmCross.Constants;
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using Scorchio.Infrastructure.Services;
    using Scorchio.Infrastructure.Wpf;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using UserControls.AddProjects;

    /// <summary>
    /// Defines the FrameworkSelectorViewModel type.
    /// </summary>
    public class FrameworkSelectorViewModel : BaseWizardStepViewModel
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The message box service.
        /// </summary>
        private readonly IMessageBoxService messageBoxService;

        /// <summary>
        /// The MVVM cross option.
        /// </summary>
        private bool mvvmCross = true;

        /// <summary>
        /// The xamarin forms option.
        /// </summary>
        private bool xamarinForms;

        /// <summary>
        /// The mvvmcrossxamarin forms.
        /// </summary>
        private bool mvvmcrossxamarinForms;

        /// <summary>
        /// The enable xamarin forms.
        /// </summary>
        private bool enableXamarinForms;

        /// <summary>
        /// The enable MVVM cross and xamarin forms.
        /// </summary>
        private bool enableMvvmCrossAndXamarinForms;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkSelectorViewModel"/> class.
        /// </summary>
        public FrameworkSelectorViewModel(
            ISettingsService settingsService,
            IMessageBoxService messageBoxService)
        {
            this.settingsService = settingsService;
            this.messageBoxService = messageBoxService;

            this.EnableXamarinForms = this.settingsService.EnableXamarinForms;
            this.EnableMvvmCrossAndXamarinForms = this.settingsService.EnableMvvmCrossAndXamarinForms;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [MVVM cross].
        /// </summary>
        public bool MvvmCross
        {
            get { return this.mvvmCross; }
            set { this.SetProperty(ref this.mvvmCross, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [xamarin forms].
        /// </summary>
        public bool XamarinForms
        {
            get{ return this.xamarinForms; }
            set { this.SetProperty(ref this.xamarinForms, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [MVVM cross xamarin forms].
        /// </summary>
        public bool MvvmCrossXamarinForms
        {
            get { return this.mvvmcrossxamarinForms; }
            set { this.SetProperty(ref this.mvvmcrossxamarinForms, value); }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether [enable xamarin forms].
        /// </summary>
        public bool EnableXamarinForms
        {
            get { return this.enableXamarinForms; }
            set { this.SetProperty(ref this.enableXamarinForms, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable MVVM cross and xamarin forms].
        /// </summary>
        public bool EnableMvvmCrossAndXamarinForms
        {
            get { return this.enableMvvmCrossAndXamarinForms; }
            set { this.SetProperty(ref this.enableMvvmCrossAndXamarinForms, value); }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public override string DisplayName
        {
            get { return "Framework"; }
        }

        /// <summary>
        /// For when yous need to save some values that can't be directly bound to UI elements.
        /// Not called when moving previous (see WizardViewModel.MoveToNextStep).
        /// </summary>
        /// <returns>
        /// An object that may modify the route
        /// </returns>
        public override RouteModifier OnNext()
        {
            //// if we are mvvmcross then we don't want the xamarin forms steps
            if (this.mvvmCross)
            {
                return new RouteModifier 
                {
                    ExcludeViewTypes = new List<Type> { typeof(LayoutSelectorControl), typeof(PageSelectorControl) },
                };
            }
             
            return new RouteModifier();
        }

        /// <summary>
        /// Determines whether this instance [can move to next page].
        /// </summary>
        /// <returns></returns>
        public override bool CanMoveToNextPage()
        {
            if (this.mvvmCross)
            {
                this.settingsService.FrameworkType = FrameworkType.MvvmCross;
            }

            else if (this.XamarinForms)
            {
                this.settingsService.FrameworkType = FrameworkType.XamarinForms;
            }

            else if (this.MvvmCrossXamarinForms)
            {
                this.messageBoxService.Show(
                    "This option is not currently available - Will be available in a future release.",
                    Settings.ApplicationName,
                    true,
                    Theme.Light, 
                    this.settingsService.ThemeColor);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets the change profile page command.
        /// </summary>
        public ICommand ChangeProfilePageCommand
        {
            get { return new RelayCommand(this.ChangeProfilePage); }
        }

        /// <summary>
        /// Changes the profile page.
        /// </summary>
        internal void ChangeProfilePage()
        {
                this.messageBoxService.Show(
                    "This option is not currently available - Will be available in a future release.",
                    Settings.ApplicationName,
                    true,
                    Theme.Light, 
                    this.settingsService.ThemeColor);
        }
    }
}
