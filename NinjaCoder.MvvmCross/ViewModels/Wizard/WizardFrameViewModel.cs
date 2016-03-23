// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the WizardFrameViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.Wizard
{
    using Entities;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using System;
    using System.Linq;

    /// <summary>
    ///  Defines the WizardFrameViewModel type.
    /// </summary>
    internal class WizardFrameViewModel : NinjaBaseViewModel
    {
        /// <summary>
        /// The wizard data.
        /// </summary>
        private readonly IWizardData wizardData;

        /// <summary>
        /// The ninja wizard view model.
        /// </summary>
        private NinjaWizardViewModel ninjaWizardViewModel;

        /// <summary>
        /// The window title.
        /// </summary>
        private string windowTitle;

        /// <summary>
        /// The window height.
        /// </summary>
        private double windowHeight;

        /// <summary>
        /// The window width.
        /// </summary>
        private double windowWidth;

        /// <summary>
        /// Initializes a new instance of the <see cref="WizardFrameViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="wizardData">The wizard data.</param>
        public WizardFrameViewModel(
            ISettingsService settingsService,
            IWizardData wizardData)
            : base(settingsService)
        {
            TraceService.WriteLine("WizardFrameViewModel::Constructor Start");

            this.wizardData = wizardData;

            this.WindowTitle = wizardData.WindowTitle;
            this.WindowHeight = wizardData.WindowHeight;
            this.WindowWidth = wizardData.WindowWidth;

            this.NinjaWizardViewModel = new NinjaWizardViewModel(wizardData.WizardSteps);

            this.NinjaWizardViewModel.OnCancel += this.OnWizardCancel;
            this.NinjaWizardViewModel.OnFinish += this.OnWizardFinish;

            TraceService.WriteLine("WizardFrameViewModel::Constructor End");
        }

        /// <summary>
        /// Gets or sets the wizard view model.
        /// </summary>
        public NinjaWizardViewModel NinjaWizardViewModel
        {
            get { return this.ninjaWizardViewModel; } 
            set { this.SetProperty(ref this.ninjaWizardViewModel, value); }
        }

        /// <summary>
        /// Gets or sets the window title.
        /// </summary>
        public string WindowTitle
        {
            get { return this.wizardData.WindowTitle; }
            set { this.SetProperty(ref this.windowTitle, value); }
        }
        
        /// <summary>
        /// Gets or sets the height of the window.
        /// </summary>
        public double WindowHeight
        {
            get { return this.wizardData.WindowHeight; }
            set { this.SetProperty(ref this.windowHeight, value); }
        }

        /// <summary>
        /// Gets or sets the width of the window.
        /// </summary>
        public double WindowWidth
        {
            get { return this.windowWidth; }
            set { this.SetProperty(ref this.windowWidth, value); }
        }

        /// <summary>
        /// Gets the wizard step view model.
        /// </summary>
        /// <param name="name">The name.</param>
        public WizardStepViewModel GetWizardStepViewModel(string name)
        {
            return this.NinjaWizardViewModel.Steps
                    .FirstOrDefault(x => x.ViewModel.ToString().Contains(name));
        }

        /// <summary>
        /// Projectses the wizard view model on on cancel.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnWizardCancel(object sender, EventArgs eventArgs)
        {
            this.OnCancel();
        }

        /// <summary>
        /// Projectses the wizard view model on finish.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnWizardFinish(object sender, EventArgs e)
        {
            this.OnOk();
        }
    }
}
