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
    using NinjaCoder.MvvmCross.UserControls.AddViews;
    using Scorchio.Infrastructure.Services;
    using Scorchio.Infrastructure.Wpf;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    using PluginsControl = NinjaCoder.MvvmCross.UserControls.AddPlugins.PluginsControl;

    /// <summary>
    /// Defines the FrameworkSelectorViewModel type.
    /// </summary>
    public class FrameworkSelectorViewModel : BaseWizardStepViewModel
    {
        /// <summary>
        /// The visual studio service.
        /// </summary>
        private readonly IVisualStudioService visualStudioService;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The message box service.
        /// </summary>
        private readonly IMessageBoxService messageBoxService;

        /// <summary>
        /// No framework.
        /// </summary>
        private bool noFramework;

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
        /// Initializes a new instance of the <see cref="FrameworkSelectorViewModel"/> class.
        /// </summary>
        public FrameworkSelectorViewModel(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService)
        {
            this.visualStudioService = visualStudioService;
            this.settingsService = settingsService;
            this.messageBoxService = messageBoxService;

            switch (this.settingsService.FrameworkType)
            {
                case FrameworkType.NoFramework:
                    this.NoFramework = true;
                    break;

                case FrameworkType.XamarinForms:
                    this.XamarinForms = true;
                    break;

                case FrameworkType.MvvmCrossAndXamarinForms:
                    this.MvvmCrossXamarinForms = true;
                    break;

                default:
                    this.MvvmCross = true;
                    break;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [solution already created].
        /// </summary>
        public bool AllowFrameWorkSelection
        {
            get { return !this.visualStudioService.SolutionAlreadyCreated; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [no framework].
        /// </summary>
        public bool NoFramework
        {
            get { return this.noFramework; }
            set { this.SetProperty(ref this.noFramework, value); }
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
            //// if no framework we cant setup up the viewmodels and views.
            if (this.noFramework)
            {
                return new RouteModifier
                {
                    ExcludeViewTypes = new List<Type> { typeof(ViewsControl), typeof(PluginsControl) } 
                };
            }

            if (this.xamarinForms)
            {
                return new RouteModifier
                {
                    ExcludeViewTypes = new List<Type> { typeof(PluginsControl) }
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
            if (this.noFramework)
            {
                this.settingsService.FrameworkType = FrameworkType.NoFramework;
            }

            else if (this.mvvmCross)
            {
                this.settingsService.FrameworkType = FrameworkType.MvvmCross;
            }

            else if (this.XamarinForms)
            {
                this.settingsService.FrameworkType = FrameworkType.XamarinForms;
            }

            else if (this.MvvmCrossXamarinForms)
            {
                this.settingsService.FrameworkType = FrameworkType.MvvmCrossAndXamarinForms;
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
