// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TokensTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators
{
    using Interfaces;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Services.Interfaces;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the TokensTranslator type.
    /// </summary>
    public class TokensTranslator : ITokensTranslator
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
        /// Initializes a new instance of the <see cref="TokensTranslator"/> class.
        /// </summary>
        public TokensTranslator(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService)
        {
            this.visualStudioService = visualStudioService;
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Translates the object.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <returns>
        /// The translated object.
        /// </returns>
        public Dictionary<string, string> Translate(IProjectService projectService)
        {
            Dictionary<string, string> tokens = new Dictionary<string, string>
            {
                {
                    "NameSpace", this.GetNameSpace(projectService.Name)
                },
                {
                     "CoreProject", this.settingsService.CoreProjectSuffix.Substring(1)
                },
                { 
                     "FormsProject", this.settingsService.XamarinFormsProjectSuffix.Substring(1)
                }
            };

            return tokens;
        }

        /// <summary>
        /// Gets the name space.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <returns>The namespace.</returns>
        internal string GetNameSpace(string projectName)
        {
            IProjectService projectService = this.visualStudioService.GetProjectServiceBySuffix(projectName);

            return projectService != null ? projectService.Name : string.Empty;
        }
    }
}
