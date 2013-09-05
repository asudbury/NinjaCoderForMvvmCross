// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ServicesService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using EnvDTE;
    using Interfaces;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Translators;

    /// <summary>
    ///  Defines the ServicesService type.
    /// </summary>
    public class ServicesService : BaseCodeService, IServicesService
    {
        /// <summary>
        /// The template being added.
        /// </summary>
        private string templateName = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicesService" /> class.
        /// </summary>
        /// <param name="translator">The translator.</param>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="snippetService">The snippet service.</param>
        public ServicesService(
            ITranslator<string, CodeConfig> translator,
            IFileSystem fileSystem, 
            ISettingsService settingsService,
            ISnippetService snippetService)
            : base(translator, fileSystem, settingsService, snippetService)
        {
            TraceService.WriteLine("ServicesService::Constructor");
        }

        /// <summary>
        /// Adds the services.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="createUnitTests">if set to <c>true</c> [create unit tests].</param>
        /// <returns> The messages.</returns>
        public List<string> AddServices(
            IVisualStudioService visualStudioService,
            IEnumerable<ItemTemplateInfo> itemTemplateInfos,
            string viewModelName,
            bool createUnitTests)
        {
            TraceService.WriteLine("ServicesService::AddServices");
            
            IProjectService coreProjectService = visualStudioService.CoreProjectService;

            ProjectItemsEvents cSharpProjectItemsEvents = visualStudioService.DTEService.GetCSharpProjectItemsEvents();
            cSharpProjectItemsEvents.ItemAdded += this.ProjectItemsEventsItemAdded;

            foreach (ItemTemplateInfo itemTemplateInfo in itemTemplateInfos)
            {
                this.templateName = itemTemplateInfo.FriendlyName;
                
                this.AddService(coreProjectService, this.SettingsService.ConfigPath, itemTemplateInfo);
            }

            //// do we want to implement in a view model?
            if (string.IsNullOrEmpty(viewModelName) == false)
            {
                IProjectItemService testProjectItemService = null;
                IProjectItemService projectItemService = coreProjectService.GetProjectItem(viewModelName);

                if (projectItemService.ProjectItem != null)
                {
                    foreach (ItemTemplateInfo itemTemplateInfo in itemTemplateInfos)
                    {
                        testProjectItemService = this.CreateSnippet(
                            visualStudioService,
                            viewModelName,
                            this.SettingsService.CodeSnippetsPath + @"\Services",
                            createUnitTests,
                            coreProjectService,
                            projectItemService,
                            itemTemplateInfo);
                    }

                    projectItemService.FixUsingStatements();

                    //// also only do once for the unit test file.
                    if (createUnitTests && testProjectItemService != null)
                    {
                        testProjectItemService.FixUsingStatements();
                    }
                }

                //// remove event handler.
                cSharpProjectItemsEvents.ItemAdded -= this.ProjectItemsEventsItemAdded;
            }

            return this.Messages;
        }

        /// <summary>
        /// Adds the service.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="templatesPath">The templates path.</param>
        /// <param name="templateInfo">The template info.</param>
        internal void AddService(
            IProjectService projectService,
            string templatesPath,
            ItemTemplateInfo templateInfo)
        {
            TraceService.WriteLine("ServicesService::AddServices adding from template path " + templatesPath + " template=" + templateInfo.FileName);

            string fileName = templateInfo.FriendlyName + ".cs";

            projectService.AddToFolderFromTemplate("Services", templateInfo.FileName, fileName, false);

            string configFile = string.Format(@"{0}\Services\Config.{1}.xml", this.SettingsService.ConfigPath, templateInfo.FriendlyName);

            if (this.FileSystem.File.Exists(configFile))
            {
                CodeConfig codeConfig = this.Translator.Translate(configFile);

                if (codeConfig != null)
                {
                    string sourceDirectory = this.SettingsService.CorePluginsPath;

                    foreach (string reference in codeConfig.References)
                    {
                        projectService.AddReference(
                            "Lib",
                            projectService.GetProjectPath() + @"\Lib\" + reference, 
                            sourceDirectory + @"\" + reference,
                            this.SettingsService.IncludeLibFolderInProjects);
                    }
                }
            }
        }

        /// <summary>
        /// Project Item added event handler.
        /// </summary>
        /// <param name="projectItem">The project item.</param>
        internal void ProjectItemsEventsItemAdded(ProjectItem projectItem)
        {
            string message = string.Format(
                "ServicesService::ProjectItemsEventsItemAdded file={0}", 
                projectItem.Name);

            TraceService.WriteLine(message);

            if (projectItem.IsCSharpFile())
            {
                this.Messages.Add(projectItem.GetFolder() + @"\" + projectItem.Name + " added to project " + projectItem.ContainingProject.Name + ".");
            
                //// now we want to amend some of the namespaces!
                //// TODO: this should really be done in the template!
                projectItem.ReplaceText(
                    "MvvmCross." + this.templateName, 
                    projectItem.ContainingProject.Name);

                if (this.SettingsService.RemoveDefaultComments)
                {
                    projectItem.RemoveComments();
                }

                if (this.SettingsService.RemoveDefaultFileHeaders)
                {
                    projectItem.RemoveHeader();
                }
            }
        }

        /// <summary>
        /// Creates the snippet.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="codeSnippetsPath">The code snippets path.</param>
        /// <param name="createUnitTests">if set to <c>true</c> [create unit tests].</param>
        /// <param name="coreProjectService">The core project service.</param>
        /// <param name="projectItemService">The project item service.</param>
        /// <param name="itemTemplateInfo">The item template info.</param>
        /// <returns>The project Item service.</returns>
        internal IProjectItemService CreateSnippet(
            IVisualStudioService visualStudioService,
            string viewModelName,
            string codeSnippetsPath,
            bool createUnitTests,
            IProjectService coreProjectService,
            IProjectItemService projectItemService,
            ItemTemplateInfo itemTemplateInfo)
        {
            TraceService.WriteLine("ServicesService::CreateSnippet service=" + itemTemplateInfo.FriendlyName + " viewModelName=" + viewModelName);

            string snippetPath = string.Format(@"{0}\Services.{1}.xml", codeSnippetsPath, itemTemplateInfo.FriendlyName);
            IProjectItemService testProjectItemService = null;

            CodeSnippet codeSnippet = this.SnippetService.GetSnippet(snippetPath);

            if (codeSnippet != null)
            {
                projectItemService.ImplementCodeSnippet(codeSnippet);

                this.Messages.Add(itemTemplateInfo.FriendlyName + " service code added to " + viewModelName + ".cs in project " + coreProjectService.Name + ".");

                //// do we need to implement any unit tests?
                if (createUnitTests)
                {
                    string testSnippetPath = string.Format(@"{0}\Services.{1}.Tests.xml", codeSnippetsPath, itemTemplateInfo.FriendlyName);

                    IProjectItemService itemService = this.CreateUnitTests(
                            visualStudioService.CoreTestsProjectService,
                            testSnippetPath,
                            viewModelName,
                            itemTemplateInfo.FriendlyName,
                            string.Empty);

                    //// if we actually create some units tests save the pointer to do
                    //// the sort and remove of the using statements later!
                    if (itemService != null)
                    {
                        testProjectItemService = itemService;
                    }
                }
            }

            return testProjectItemService;
        }
    }
}
