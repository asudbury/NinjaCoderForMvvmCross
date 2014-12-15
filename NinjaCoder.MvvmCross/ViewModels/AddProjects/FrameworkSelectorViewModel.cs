// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the FrameworkSelectorViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddProjects
{
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.UserControls.AddViews;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using System;
    using System.Collections.Generic;

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
        /// No framework.
        /// </summary>
        private bool noFramework;

        /// <summary>
        /// The MVVM cross option.
        /// </summary>
        private bool mvvmCross;

        /// <summary>
        /// The xamarin forms option.
        /// </summary>
        private bool xamarinForms;

        /// <summary>
        /// The mvvmcrossxamarin forms.
        /// </summary>
        private bool mvvmcrossxamarinForms;

        /// <summary>
        /// The MVVM cross label.
        /// </summary>
        private string mvvmCrossLabel;

        /// <summary>
        /// The xamarin forms label.
        /// </summary>
        private string xamarinFormsLabel;

        /// <summary>
        /// The MVVM cross and xamarin forms label.
        /// </summary>
        private string mvvmCrossAndXamarinFormsLabel;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkSelectorViewModel"/> class.
        /// </summary>
        public FrameworkSelectorViewModel(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService)
        {
            this.visualStudioService = visualStudioService;
            this.settingsService = settingsService;

            this.Init();
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
        /// Gets or sets the MVVM cross label.
        /// </summary>
        public string MvvmCrossLabel
        {
            get { return this.mvvmCrossLabel; }
            set { this.SetProperty(ref this.mvvmCrossLabel, value); }
        }

        /// <summary>
        /// Gets or sets the xamarin forms label.
        /// </summary>
        public string XamarinFormsLabel
        {
            get { return this.xamarinFormsLabel; }
            set { this.SetProperty(ref this.xamarinFormsLabel, value); }
        }

        /// <summary>
        /// Gets the MVVM cross and xamarin forms label.
        /// </summary>
        /// <value>
        /// The MVVM cross and xamarin forms label.
        /// </value>
        public string MvvmCrossAndXamarinFormsLabel
        {
            get { return this.mvvmCrossAndXamarinFormsLabel; }
            set { this.SetProperty(ref this.mvvmCrossAndXamarinFormsLabel, value); }
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
                    ExcludeViewTypes = new List<Type>
                        {
                            typeof(ViewsControl), 
                            typeof(PluginsControl)
                        } 
                };
            }

            if (this.xamarinForms)
            {
                return new RouteModifier
                {
                    ExcludeViewTypes = new List<Type>
                        {
                            typeof(PluginsControl)
                        }
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
            bool canMove = false;

            if (this.noFramework)
            {
                this.settingsService.FrameworkType = FrameworkType.NoFramework;
                canMove = true;
            }

            else if (this.mvvmCross)
            {
                this.settingsService.FrameworkType = FrameworkType.MvvmCross;
                canMove = true;
            }

            else if (this.XamarinForms)
            {
                this.settingsService.FrameworkType = FrameworkType.XamarinForms;
                canMove = true;
            }

            else if (this.MvvmCrossXamarinForms)
            {
                this.settingsService.FrameworkType = FrameworkType.MvvmCrossAndXamarinForms;
                canMove = true;
            }

            return canMove;
        }

        /// <summary>
        /// Called when [initialize].
        /// </summary>
        public override void OnInitialize()
        {
            this.Init();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        internal void Init()
        {
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

            string label = "MvvmCross";

            if (this.settingsService.UsePreReleaseMvvmCrossNugetPackages)
            {
                label += " (Pre Release)";
            }

            this.MvvmCrossLabel = label;

            label = "Xamarin Forms";

            if (this.settingsService.UsePreReleaseXamarinFormsNugetPackages)
            {
                label += " (Pre Release)";
            }

            this.XamarinFormsLabel = label;

            label = "MvvmCross and Xamarin Forms";

            if (this.settingsService.UsePreReleaseMvvmCrossNugetPackages ||
                this.settingsService.UsePreReleaseXamarinFormsNugetPackages)
            {
                ////label += " (Pre Release)";
            }

            label += " (Available in a future release)";

            this.MvvmCrossAndXamarinFormsLabel = label;
        }
    }
}
