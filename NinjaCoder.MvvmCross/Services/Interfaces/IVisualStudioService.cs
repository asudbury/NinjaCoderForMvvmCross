// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IVisualStudioService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using System.Collections.Generic;

    using EnvDTE;

    using EnvDTE80;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    /// Defines the IVisualStudioService type.
    /// </summary>
    public interface IVisualStudioService
    {
        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        DTE2 DTE2 { get; set; }

        /// <summary>
        /// Gets a value indicating whether [allow droid project].
        /// </summary>
        bool AllowDroidProject { get; }

        /// <summary>
        /// Gets a value indicating whether [allow iOS project].
        /// </summary>
        bool AllowiOSProject { get; }

        /// <summary>
        /// Gets a value indicating whether [allow windows phone project].
        /// </summary>
        bool AllowWindowsPhoneProject { get; }

        /// <summary>
        /// Gets a value indicating whether [allow windows store project].
        /// </summary>
        bool AllowWindowsStoreProject { get; }

        /// <summary>
        /// Gets a value indicating whether [allow WPF project].
        /// </summary>
        bool AllowWpfProject { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is MVVM cross solution.
        /// </summary>
        bool IsMvvmCrossSolution { get; }

        /// <summary>
        /// Gets the core project.
        /// </summary>
        Project CoreProject { get; }

        /// <summary>
        /// Gets the core project service.
        /// </summary>
        IProjectService CoreProjectService { get; }
        
        /// <summary>
        /// Gets the core tests project.
        /// </summary>
        Project CoreTestsProject { get;  }

        /// <summary>
        /// Gets the core tests project service.
        /// </summary>
        IProjectService CoreTestsProjectService { get; }

        /// <summary>
        /// Gets the droid project.
        /// </summary>
        Project DroidProject { get; }

        /// <summary>
        /// Gets the droid project service.
        /// </summary>
        IProjectService DroidProjectService { get; }

        /// <summary>
        /// Gets the i OS project.
        /// </summary>
        Project iOSProject { get; }

        /// <summary>
        /// Gets the iOS project service.
        /// </summary>
        IProjectService iOSProjectService { get; }

        /// <summary>
        /// Gets the windows phone project.
        /// </summary>
        Project WindowsPhoneProject { get; }

        /// <summary>
        /// Gets the windows phone project service.
        /// </summary>
        IProjectService WindowsPhoneProjectService { get; }

        /// <summary>
        /// Gets the windows store project.
        /// </summary>
        Project WindowsStoreProject { get; }

        /// <summary>
        /// Gets the windows store project service.
        /// </summary>
        IProjectService WindowsStoreProjectService { get; }

        /// <summary>
        /// Gets the WPF project.
        /// </summary>
        Project WpfProject { get; }

        /// <summary>
        /// Gets the WPF project service.
        /// </summary>
        IProjectService WpfProjectService { get; }

        /// <summary>
        /// Gets the allowed project templates.
        /// </summary>
        List<ProjectTemplateInfo> AllowedProjectTemplates { get; }

        /// <summary>
        /// Gets the allowed item templates.
        /// </summary>
        List<ItemTemplateInfo> AllowedItemTemplates { get; }

        /// <summary>
        /// Gets the folder template infos.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="destinationFolder">The destination folder.</param>
        /// <returns>
        /// The template Infos.
        /// </returns>
        List<ItemTemplateInfo> GetFolderTemplateInfos(
            string path,
            string destinationFolder);
    }
}
