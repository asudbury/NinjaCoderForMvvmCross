// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IVisualStudioService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using EnvDTE;
    using EnvDTE80;
    using NinjaCoder.MvvmCross.Entities;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services.Interfaces;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the IVisualStudioService type.
    /// </summary>
    public interface IVisualStudioService
    {
        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        DTE2 DTE2 { get; set; }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        IEnumerable<Project> Projects { get; }

        /// <summary>
        /// Gets the DTE service.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        IDTEService DTEService { get; }

        /// <summary>
        /// Gets the solution service.
        /// </summary>
        ISolutionService SolutionService { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is MVVM cross solution.
        /// </summary>
        bool IsMvvmCrossSolution { get; }

        /// <summary>
        /// Gets the core project service.
        /// </summary>
        IProjectService CoreProjectService { get; }

        /// <summary>
        /// Gets the core tests project service.
        /// </summary>
        IProjectService CoreTestsProjectService { get; }

        /// <summary>
        /// Gets the droid project service.
        /// </summary>
        IProjectService DroidProjectService { get; }

        /// <summary>
        /// Gets the droid tests project service.
        /// </summary>
        IProjectService DroidTestsProjectService { get; }

        /// <summary>
        /// Gets the iOS project service.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        IProjectService iOSProjectService { get; }

        /// <summary>
        /// Gets the ios tests project service.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        IProjectService iOSTestsProjectService { get; }

        /// <summary>
        /// Gets the windows phone project service.
        /// </summary>
        IProjectService WindowsPhoneProjectService { get; }

        /// <summary>
        /// Gets the windows phone tests project service.
        /// </summary>
        IProjectService WindowsPhoneTestsProjectService { get; }

        /// <summary>
        /// Gets the WPF project service.
        /// </summary>
        IProjectService WpfProjectService { get; }

        /// <summary>
        /// Gets the WPF tests project service.
        /// </summary>
        IProjectService WpfTestsProjectService { get; }

        /// <summary>
        /// Gets the xamarin forms project service.
        /// </summary>
        IProjectService XamarinFormsProjectService { get; }

        /// <summary>
        /// Gets the xamarin forms tests project service.
        /// </summary>
        IProjectService XamarinFormsTestsProjectService { get; }

        /// <summary>
        /// Gets the folder template infos.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The template Infos.</returns>
        List<ItemTemplateInfo> GetFolderTemplateInfos(string path);
        
        /// <summary>
        /// Writes the status bar message.
        /// </summary>
        /// <param name="message">The message.</param>
        void WriteStatusBarMessage(string message);

        /// <summary>
        /// Tidy the code up.
        /// </summary>
        /// <param name="removeHeader">if set to <c>true</c> [remove header].</param>
        /// <param name="removeComments">if set to <c>true</c> [remove comments].</param>
        /// <param name="removeThisPointer">if set to <c>true</c> [remove this pointer].</param>
        void CodeTidyUp(
            bool removeHeader, 
            bool removeComments,
            bool removeThisPointer);

        /// <summary>
        /// Gets the default name of the project.
        /// </summary>
        /// <returns>The default project name.</returns>
        string GetDefaultProjectName();

        /// <summary>
        /// Gets the project service by suffix.
        /// </summary>
        /// <param name="suffix">The suffix.</param>
        /// <returns>The project service.</returns>
        IProjectService GetProjectServiceBySuffix(string suffix);

        /// <summary>
        /// Gets the project services by suffix.
        /// </summary>
        /// <param name="suffix">The suffix.</param>
        /// <returns>A collection of project services.</returns>
        IEnumerable<IProjectService> GetProjectServicesBySuffix(string suffix);

        /// <summary>
        /// Gets the public view model names.
        /// </summary>
        /// <returns>The public view model names.</returns>
        IEnumerable<string> GetPublicViewModelNames();

        /// <summary>
        /// Gets the type of the framework.
        /// </summary>
        /// <returns>The framework type.</returns>
        FrameworkType GetFrameworkType();

        /// <summary>
        /// Gets a value indicating whether [solution already created].
        /// </summary>
        bool SolutionAlreadyCreated { get; }

        /// <summary>
        /// Gets the text transformation service.
        /// </summary>
        /// <returns></returns>
        ITextTransformationService GetTextTransformationService();
    }
}
