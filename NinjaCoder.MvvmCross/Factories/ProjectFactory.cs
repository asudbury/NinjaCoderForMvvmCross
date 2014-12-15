// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using Interfaces;
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.UserControls.AddViews;
    using NinjaCoder.MvvmCross.ViewModels.AddNugetPackages;
    using NinjaCoder.MvvmCross.ViewModels.AddViews;

    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;

    using Services.Interfaces;
    using System.Collections.Generic;
    using UserControls.AddProjects;
    using ViewModels.AddProjects;

    using PluginsControl = NinjaCoder.MvvmCross.UserControls.AddPlugins.PluginsControl;
    using ProjectsFinishedControl = NinjaCoder.MvvmCross.UserControls.AddProjects.ProjectsFinishedControl;

    /// <summary>
    ///  Defines the ProjectFactory type.
    /// </summary>
    public class ProjectFactory : IProjectFactory
    {
        /// <summary>
        /// The resolver service.
        /// </summary>
        private readonly IResolverService resolverService;

        /// <summary>
        /// The register service
        /// </summary>
        private readonly IRegisterService registerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectFactory" /> class.
        /// </summary>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="registerService">The register service.</param>
        public ProjectFactory(
            IResolverService resolverService,
            IRegisterService registerService)
        {
            TraceService.WriteLine("ProjectFactory::Constructor");

            this.resolverService = resolverService;
            this.registerService = registerService;
        }

        /// <summary>
        /// Gets the allowed projects.
        /// </summary>
        /// <param name="frameworkType">Type of the framework.</param>
        /// <returns>
        /// The allowed projects.
        /// </returns>
        public IEnumerable<ProjectTemplateInfo> GetAllowedProjects(FrameworkType frameworkType)
        {
            TraceService.WriteLine("ProjectFactory::GetAllowedProjects");

            switch (frameworkType)
            {
                case FrameworkType.NoFramework:
                    return this.resolverService.Resolve<INoFrameworkProjectFactory>().GetAllowedProjects();

                case FrameworkType.MvvmCross:
                    return this.resolverService.Resolve<IMvvmCrossProjectFactory>().GetAllowedProjects();

                case FrameworkType.XamarinForms:
                    return this.resolverService.Resolve<XamarinFormsProjectFactory>().GetAllowedProjects();

                default:
                    return this.resolverService.Resolve<MvvmCrossAndXamarinFormsProjectFactory>().GetAllowedProjects();
            }
        }

        /// <summary>
        /// Gets the wizards steps.
        /// </summary>
        /// <returns>
        /// The wizard steps.
        /// </returns>
        public List<WizardStepViewModel> GetWizardsSteps()
        {
            TraceService.WriteLine("ProjectFactory::GetWizardsSteps");

            List<WizardStepViewModel> wizardSteps = new List<WizardStepViewModel>
            {
                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<BuildOptionsViewModel>(),
                    ViewType = typeof (BuildOptionsControl)
                },
                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<FrameworkSelectorViewModel>(),
                    ViewType = typeof (FrameworkSelectorControl)
                },
                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<ProjectsViewModel>(),
                    ViewType = typeof (ProjectsControl)
                },
                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<ViewsViewModel>(),
                    ViewType = typeof (ViewsControl)
                },
                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<PluginsViewModel>(),
                    ViewType = typeof (PluginsControl)
                },
                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<NugetPackagesViewModel>(),
                    ViewType = typeof (NugetPackagesControl)
                },
                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<XamarinFormsLabsViewModel>(),
                    ViewType = typeof (XamarinFormsLabsControl)
                },
                new WizardStepViewModel
                {
                    ViewModel = this.resolverService.Resolve<ProjectsFinishedViewModel>(),
                    ViewType = typeof (ProjectsFinishedControl)
                }
            };

            return wizardSteps;
        }

        /// <summary>
        /// Gets the wizard data.
        /// </summary>
        /// <returns></returns>
        public void RegisterWizardData()
        {
            TraceService.WriteLine("ProjectFactory::RegisterWizardData");

            WizardData wizardData = new WizardData
            {
                WindowTitle = "Add Projects",
                WindowHeight = 600,
                WindowWidth = 680,
                WizardSteps = this.GetWizardsSteps()
            };

            this.registerService.Register<IWizardData>(wizardData);
        }
    }
}
