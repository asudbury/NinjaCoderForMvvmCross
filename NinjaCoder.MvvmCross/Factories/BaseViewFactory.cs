// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the BaseViewFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Factories
{
    using Entities;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Services.Interfaces;
    using System.Collections.Generic;

    /// <summary>
    ///     Defines the BaseViewFactory type.
    /// </summary>
    public abstract class BaseViewFactory
    {
        /// <summary>
        ///     The view model suffix.
        /// </summary>
        protected const string ViewModelSuffix = "ViewModel";

        /// <summary>
        ///     The text transformation service
        /// </summary>
        private ITextTransformationService textTransformationService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseViewFactory" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        protected BaseViewFactory(ISettingsService settingsService, IVisualStudioService visualStudioService)
        {
            this.SettingsService = settingsService;
            this.VisualStudioService = visualStudioService;
        }

        /// <summary>
        /// Gets the settings service.
        /// </summary>
        protected ISettingsService SettingsService { get; }

        /// <summary>
        ///  Gets the visual studio service.
        /// </summary>
        protected IVisualStudioService VisualStudioService { get; }

        /// <summary>
        ///     Gets the text transformation service.
        /// </summary>
        /// <returns>The Text Transformation Service.</returns>
        protected ITextTransformationService GetTextTransformationService()
        {
            return this.textTransformationService ?? (this.textTransformationService = new TextTransformationService());
        }

        /// <summary>
        ///     Gets the embedded resource file operation.
        /// </summary>
        /// <param name="platForm">The platform.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>A FileOperation.</returns>
        protected FileOperation GetEmbeddedResourceFileOperation(string platForm, string fileName)
        {
            return this.GetFileOperation(platForm, fileName, "3");
        }

        /// <summary>
        ///     Gets the embedded resource file operation.
        /// </summary>
        /// <param name="platForm">The platform.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>A FileOperation.</returns>
        protected FileOperation GetPageFileOperation(string platForm, string fileName)
        {
            var operation = "4";

            if (platForm == ProjectSuffix.Wpf.GetDescription())
            {
                operation = "5";
            }

            return this.GetFileOperation(platForm, fileName, operation);
        }

        /// <summary>
        ///     Gets the compile file operation.
        /// </summary>
        /// <param name="platForm">The platform.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>A FileOperation.</returns>
        protected FileOperation GetCompileFileOperation(string platForm, string fileName)
        {
            return this.GetFileOperation(platForm, fileName, "1");
        }

        /// <summary>
        ///     Gets the file operation.
        /// </summary>
        /// <param name="platForm">The platform.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="operation">The operation.</param>
        /// <returns>A FileOperation.</returns>
        internal FileOperation GetFileOperation(string platForm, string fileName, string operation)
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

        /// <summary>
        ///     Gets the code behind text template information.
        /// </summary>
        /// <param name="projectSuffix">The project suffix.</param>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="viewTemplateName">Name of the view template.</param>
        /// <param name="tokens">The tokens.</param>
        /// <returns>A TextTemplateInfo.</returns>
        protected TextTemplateInfo GetCodeBehindTextTemplateInfo(
            string projectSuffix,
            string viewName,
            string viewTemplateName,
            Dictionary<string, string> tokens)
        {
            var templateDirectory = this.SettingsService.ItemTemplatesDirectory;

            if (projectSuffix == ProjectSuffix.XamarinForms.GetDescription())
            {
                templateDirectory += "\\XamarinForms";
            }

            var textTemplateInfo = new TextTemplateInfo
            {
                ProjectSuffix = projectSuffix,
                ProjectFolder = "Views",
                Tokens = tokens,
                ShortTemplateName = viewTemplateName,
                TemplateName =
                                               templateDirectory + "\\" + "BlankViewCodeBehind.t4"
            };

            var textTransformationRequest = new TextTransformationRequest
            {
                SourceFile = textTemplateInfo.TemplateName,
                Parameters = textTemplateInfo.Tokens,
                RemoveFileHeaders =
                                                        this.SettingsService
                                                        .RemoveDefaultFileHeaders,
                RemoveXmlComments =
                                                        this.SettingsService
                                                        .RemoveDefaultComments,
                RemoveThisPointer =
                                                        this.SettingsService.RemoveThisPointer
            };

            var textTransformation = this.GetTextTransformationService().Transform(textTransformationRequest);

            textTemplateInfo.TextOutput = textTransformation.Output;
            textTemplateInfo.FileName = viewName + "." + textTransformation.FileExtension;

            return textTemplateInfo;
        }

        /// <summary>
        ///     Gets the view template.
        /// </summary>
        /// <param name="frameworkType">Type of the framework.</param>
        /// <param name="type">The type.</param>
        /// <param name="pageType">Type of the page.</param>
        /// <returns>The View Template.</returns>
        protected string GetViewTemplate(FrameworkType frameworkType, string type, string pageType)
        {
            if (frameworkType == FrameworkType.XamarinForms)
            {
                return "BlankView.t4";
            }

            return string.Format("{0}View.t4", pageType);
        }

        /// <summary>
        ///     Gets the base dictionary.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="projectSuffix">The project suffix.</param>
        /// <returns>A Dictionary.</returns>
        protected Dictionary<string, string> GetBaseDictionary(string viewName, string projectSuffix)
        {
            return new Dictionary<string, string>
                       {
                           { "ClassName", viewName },
                           {
                               "ProjectName",
                               this.VisualStudioService.GetProjectServiceBySuffix(projectSuffix)
                               .Name
                           },
                           {
                               "NameSpace",
                               this.VisualStudioService.GetProjectServiceBySuffix(projectSuffix)
                                   .Name + ".Views"
                           }
                       };
        }
    }
}