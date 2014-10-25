// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the XamarinFormsProjectFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Factories
{
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Scorchio.Infrastructure.Extensions;
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the XamarinFormsProjectFactory type. 
    /// </summary>
    public class XamarinFormsProjectFactory : IXamarinFormsProjectFactory
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
        /// The nuget commands service.
        /// </summary>
        private readonly INugetCommandsService nugetCommandsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="XamarinFormsProjectFactory"/> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="nugetCommandsService">The nuget commands service.</param>
        public XamarinFormsProjectFactory(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            INugetCommandsService nugetCommandsService)
        {
            this.visualStudioService = visualStudioService;
            this.settingsService = settingsService;
            this.nugetCommandsService = nugetCommandsService;
        }

        /// <summary>
        /// Gets the allowed projects.
        /// </summary>
        /// <returns>
        /// The allowed projects.
        /// </returns>
        public IEnumerable<ProjectTemplateInfo> GetAllowedProjects()
        {
            List<ProjectTemplateInfo> projectInfos = new List<ProjectTemplateInfo>();

            if (this.visualStudioService.CoreProjectService == null)
            {
                projectInfos.Add(this.GetCoreProject());
            }

            if (this.visualStudioService.FormsProjectService == null)
            {
                projectInfos.Add(this.GetFormsProject());
            }

            if (this.visualStudioService.DroidProjectService == null)
            {
                projectInfos.Add(this.GetDroidProject());
            }

            if (this.visualStudioService.iOSProjectService == null)
            {
                projectInfos.Add(this.GetiOSProject());
            }

            if (this.visualStudioService.WindowsPhoneProjectService == null)
            {
                projectInfos.Add(this.GetWindowsPhoneProject());
            }

            return projectInfos;
        }

        /// <summary>
        /// Gets the xamarim forms core project.
        /// </summary>
        /// <returns>A core project.</returns>
        internal ProjectTemplateInfo GetCoreProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.Core.GetDescription() + " (Profile " + this.settingsService.PCLProfile + ")",
                ProjectSuffix = ProjectSuffix.Core.GetDescription(),
                TemplateName = XamarinFormsProjectTemplate.Core.GetDescription(),
                PreSelected = true,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossCoreCommands()
            };
        }

        /// <summary>
        /// Gets the xamarin forms project.
        /// </summary>
        /// <returns>A xamarin forms project.</returns>
        internal ProjectTemplateInfo GetFormsProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.XamarinForms.GetDescription(),
                ProjectSuffix = ProjectSuffix.Forms.GetDescription(),
                TemplateName = XamarinFormsProjectTemplate.Forms.GetDescription(),
                PreSelected = true,
                NugetCommands = this.nugetCommandsService.GetXamarinFormsCommands()
            };
        }


        /// <summary>
        /// Gets the droid project.
        /// </summary>
        /// <returns>A android project.</returns>
        internal ProjectTemplateInfo GetDroidProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = "Android",
                ProjectSuffix = ProjectSuffix.Droid.GetDescription(),
                TemplateName = XamarinFormsProjectTemplate.Droid.GetDescription(),
                PreSelected = true,
                ReferenceXamarinFormsProject = true,
                NugetCommands = this.nugetCommandsService.GetXamarinFormsCommands()
            };
        }

        /// <summary>
        /// Gets the ios project.
        /// </summary>
        /// <returns>An iOs project.</returns>
        internal ProjectTemplateInfo GetiOSProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.iOS.GetDescription() + " " + this.settingsService.iOSBuildVersion,
                ProjectSuffix = ProjectSuffix.iOS.GetDescription(),
                TemplateName = XamarinFormsProjectTemplate.iOS.GetDescription(),
                PreSelected = true,
                NugetCommands = this.nugetCommandsService.GetXamarinFormsCommands()
            };
        }

        /// <summary>
        /// Gets the windows phone project.
        /// </summary>
        /// <returns>A windows phone project.</returns>
        internal ProjectTemplateInfo GetWindowsPhoneProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.WindowsPhone.GetDescription() + " " + this.settingsService.WindowsPhoneBuildVersion,
                ProjectSuffix = ProjectSuffix.WindowsPhone.GetDescription(),
                TemplateName = XamarinFormsProjectTemplate.WindowsPhone.GetDescription(),
                ReferenceXamarinFormsProject = true,
                PreSelected = true,
                NugetCommands = this.nugetCommandsService.GetXamarinFormsCommands()
            };
        }
    }
}

