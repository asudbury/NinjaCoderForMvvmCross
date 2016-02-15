// --------------------------------------------------------------------------------------------------------------------
// <summary>
// 	Defines the BaseViewFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using System.Collections.Generic;

    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Scorchio.Infrastructure.Extensions;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the BaseViewFactory type.
    /// </summary>
    public abstract class BaseViewFactory
    {
        /// <summary>
        /// The view model suffix.
        /// </summary>
        protected const string ViewModelSuffix = "ViewModel";

        /// <summary>
        /// The settings service
        /// </summary>
        protected ISettingsService SettingsService { get; private set; }

        /// <summary>
        /// Gets or sets the visual studio service.
        /// </summary>
        protected IVisualStudioService VisualStudioService { get; private set; }

        /// <summary>
        /// The text transformation service
        /// </summary>
        private ITextTransformationService textTransformationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewFactory" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        protected BaseViewFactory(
            ISettingsService settingsService,
            IVisualStudioService visualStudioService)
        {
            this.SettingsService = settingsService;
            this.VisualStudioService = visualStudioService;
        }

        /// <summary>
        /// Gets the text transformation service.
        /// </summary>
        /// <returns></returns>
        protected ITextTransformationService GetTextTransformationService()
        {
            return this.textTransformationService ?? (this.textTransformationService = new TextTransformationService());
        }

        /// <summary>
        /// Gets the embedded resource file operation.
        /// </summary>
        /// <param name="platForm">The platform.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        protected FileOperation GetEmbeddedResourceFileOperation(
            string platForm,
            string fileName)
        {
            return this.GetFileOperation(platForm, fileName, "3");
        }

        /// <summary>
        /// Gets the embedded resource file operation.
        /// </summary>
        /// <param name="platForm">The platform.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        protected FileOperation GetPageFileOperation(
            string platForm,
            string fileName)
        {
            string operation = "4";

            if (platForm == ProjectSuffix.Wpf.GetDescription())
            {
                operation = "5";
            }
            return this.GetFileOperation(platForm, fileName, operation);
        }

        /// <summary>
        /// Gets the compile file operation.
        /// </summary>
        /// <param name="platForm">The platform.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        protected FileOperation GetCompileFileOperation(
            string platForm,
            string fileName)
        {
            return this.GetFileOperation(platForm, fileName, "1");
        }

        /// <summary>
        /// Gets the code behind text template information.
        /// </summary>
        /// <param name="projectSuffix">The project suffix.</param>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="viewTemplateName">Name of the view template.</param>
        /// <param name="tokens">The tokens.</param>
        /// <returns></returns>
        protected TextTemplateInfo GetCodeBehindTextTemplateInfo(
            string projectSuffix,
            string viewName,
            string viewTemplateName,
            Dictionary<string, string> tokens)
        {
            TextTemplateInfo textTemplateInfo = new TextTemplateInfo
            {
                ProjectSuffix = projectSuffix,
                ProjectFolder = "Views",
                Tokens = tokens,
                ShortTemplateName = viewTemplateName,
                TemplateName = this.SettingsService.ItemTemplatesDirectory + "\\" + "BlankViewCodeBehind.t4",
            };

            TextTransformation textTransformation = this.GetTextTransformationService().Transform(
                                           textTemplateInfo.TemplateName,
                                           textTemplateInfo.Tokens);

            textTemplateInfo.TextOutput = textTransformation.Output;
            textTemplateInfo.FileName = viewName + "." + textTransformation.FileExtension;

            return textTemplateInfo;
        }

        /// <summary>
        /// Gets the view template.
        /// </summary>
        /// <param name="frameworkType">Type of the framework.</param>
        /// <param name="type">The type.</param>
        /// <param name="pageType">Type of the page.</param>
        /// <returns>
        /// The View Template.
        /// </returns>
        protected string GetViewTemplate(
            FrameworkType frameworkType,
            string type,
            string pageType)
        {
            if (frameworkType == FrameworkType.XamarinForms)
            {
                return "BlankView.t4";
            }

            return string.Format(
                "{0}View.t4",
                pageType);
        }

        /// <summary>
        /// Gets the base dictionary.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="projectSuffix">The project suffix.</param>
        /// <returns></returns>
        protected Dictionary<string, string> GetBaseDictionary(
            string viewName,
            string projectSuffix)
        {
            return new Dictionary<string, string>
                        {
                            { "ClassName", viewName },
                            { "ProjectName",  this.VisualStudioService.GetProjectServiceBySuffix(projectSuffix).Name },
                            { "NameSpace",  this.VisualStudioService.GetProjectServiceBySuffix(projectSuffix).Name + ".Views"}
                        };
        }

        /// <summary>
        /// Gets the file operation.
        /// </summary>
        /// <param name="platForm">The platform.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="operation">The operation.</param>
        /// <returns></returns>
        internal FileOperation GetFileOperation(
            string platForm,
            string fileName,
            string operation)
        {
            return new FileOperation
            {
                PlatForm = platForm,
                CommandType = "Properties",
                Directory = "Views",
                File = fileName,
                From = "BuildAction",
                To = operation
            };
        }
    }
}
