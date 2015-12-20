// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the TextTemplatingService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///  Defines the TextTemplatingService type.
    /// </summary>
    public class TextTemplatingService : BaseService, ITextTemplatingService
    {
        /// <summary>
        /// The visual studio service.
        /// </summary>
        private readonly IVisualStudioService visualStudioService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextTemplatingService" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        public TextTemplatingService(IVisualStudioService visualStudioService)
        {
            TraceService.WriteLine("TextTemplatingService::Constructor");

            this.visualStudioService = visualStudioService;
        }

        /// <summary>
        /// Adds the text templates.
        /// </summary>
        /// <param name="statusBarMessage">The status bar message.</param>
        /// <param name="textTemplates">The text templates.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<string> AddTextTemplates(
           string statusBarMessage,
           IEnumerable<TextTemplateInfo> textTemplates)
        {
            TraceService.WriteLine("TextTemplatingService::AddTextTemplates");

            this.Messages = new List<string>();

            this.visualStudioService.WriteStatusBarMessage(statusBarMessage);

            foreach (TextTemplateInfo textTemplateInfo in textTemplates)
            {
                TraceService.WriteLine("TextTemplatingService::AddTextTemplates textTemplate=" + textTemplateInfo.FileName);
                    
                IProjectService projectService = this.visualStudioService.GetProjectServiceBySuffix(textTemplateInfo.ProjectSuffix);

                if (projectService != null)
                {
                    this.visualStudioService.WriteStatusBarMessage(statusBarMessage + "for " + projectService.Name);

                    ITextTransformationService textTransformationService = this.visualStudioService.GetTextTransformationService();

                    textTemplateInfo.TextOutput = textTransformationService.Transform(
                        this.visualStudioService.UseSimpleTextTemplatingEngine,
                        textTemplateInfo.TemplateName, 
                        textTemplateInfo.Tokens);

                    if (textTemplateInfo.TextOutput != string.Empty &&
                        textTransformationService.T4CallBack.ErrorMessages.Any() == false)
                    {
                        //// add file to the solution
                        string message = projectService.AddTextTemplate(textTemplateInfo);
                        this.Messages.Add(message);
                    }

                    else
                    {
                        foreach (string errorMessage in textTransformationService.T4CallBack.ErrorMessages)
                        {
                            this.Messages.Add("T4 Error " + errorMessage);
                        }
                    }
                }
            }

            return this.Messages;
        }
    }
}
