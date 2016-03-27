// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NugetPackagesFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Factories
{
    using Entities;
    using Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using System.Collections.Generic;
    using UserControls.AddNugetPackages;
    using UserControls.AddProjects;
    using ViewModels.AddNugetPackages;
    using ViewModels.AddProjects;

    /// <summary>
    ///  Defines the NugetPackagesFactory type.
    /// </summary>
    public class NugetPackagesFactory : INugetPackagesFactory
    {
        /// <summary>
        /// The resolver service.
        /// </summary>
        private readonly IResolverService resolverService;

        /// <summary>
        /// The register service.
        /// </summary>
        private readonly IRegisterService registerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NugetPackagesFactory" /> class.
        /// </summary>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="registerService">The register service.</param>
        public NugetPackagesFactory(
            IResolverService resolverService,
            IRegisterService registerService)
        {
            this.resolverService = resolverService;
            this.registerService = registerService;
        }

        /// <summary>
        /// Gets the wizards steps.
        /// </summary>
        /// <returns>
        /// The wizard steps.
        /// </returns>
        public List<WizardStepViewModel> GetWizardsSteps()
        {
            List<WizardStepViewModel> wizardSteps = new List<WizardStepViewModel>
            {
                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<ApplicationOptionsViewModel>(),
                    ViewType = typeof(ApplicationOptionsControl)
                },
                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<NinjaCoderOptionsViewModel>(),
                    ViewType = typeof(NinjaCoderOptionsControl),
                },
                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<ApplicationSamplesOptionsViewModel>(),
                    ViewType = typeof(ApplicationSamplesOptionsControl),
                },
                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<NugetPackagesViewModel>(),
                    ViewType = typeof(NugetPackagesControl)
                },
                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<XamarinFormsLabsViewModel>(),
                    ViewType = typeof(XamarinFormsLabsControl)
                },
                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<NugetPackagesFinishedViewModel>(),
                    ViewType = typeof(NugetPackagesFinishedControl)
                }
            };

            return wizardSteps;
        }

        /// <summary>
        /// Registers the wizard data.
        /// </summary>
        public void RegisterWizardData()
        {
            WizardData wizardData = new WizardData
            {
                WindowTitle = "Add Nuget Packages",
                WindowHeight = 600,
                WindowWidth = 680,
                WizardSteps = this.GetWizardsSteps()
            };

            this.registerService.Register<IWizardData>(wizardData);
        }
    }
}
