 // --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the WebRequestServiceWizard type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.TemplateWizards.WebRequestService
{
    using System.Globalization;
    using System.Windows.Forms;
    using EnvDTE;
    using Extensions;
    using Services;

    /// <summary>
    ///  Defines the WebRequestServiceWizard type.
    /// </summary>
    public class WebRequestServiceWizard : BaseWizard
    {
        /// <summary>
        /// The entity name.
        /// </summary>
        private string entityName = string.Empty;

        /// <summary>
        /// The dialog result.
        /// </summary>
        private DialogResult dialogResult = DialogResult.None;

        /// <summary>
        /// Runs custom wizard logic at the beginning of a template wizard run.
        /// </summary>
        public override void OnRunStarted()
        {
            TraceService.WriteLine("WebRequestServiceWizard::OnRunStarted");

            WebRequestServiceView view = new WebRequestServiceView(this.SettingsService.DisplayLogo);
            view.ShowDialog();

            this.dialogResult = view.DialogResult;

            if (this.dialogResult == DialogResult.OK)
            {
                this.entityName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(view.EntityName);

                this.ReplacementsDictionary.Add("$webRequestEntityName$", this.entityName);

                this.AddGlobal("MockWebRequestService", "mock" + this.entityName + "WebRequestService");
                this.AddGlobal("InterfaceWebRequest", "I" + this.entityName + "WebRequestService");
                this.AddGlobal("webRequestInstance", this.entityName.LowerCaseFirstCharacter() + "WebRequestService");
                this.AddGlobal("WebRequestService", this.entityName + "WebRequestService");

                this.AddGlobal("SampleWebRequestService", this.entityName + "WebRequestService");
                
                this.AddGlobal("SampleWebRequestData", this.entityName);
                this.AddGlobal("WebRequestSampleData", this.entityName);

                this.AddGlobal("SampleWebRequestDataInstance", this.entityName.LowerCaseFirstCharacter());
                this.AddGlobal("SampleWebRequestTranslator", this.entityName + "Translator");
            }
        }

        /// <summary>
        /// Called when [should add project item].
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>True or false.</returns>
        public override bool OnShouldAddProjectItem(string filePath)
        {
            TraceService.WriteLine("WebRequestServiceWizard::OnShouldAddProjectItem path=" + filePath);

            if (this.dialogResult == DialogResult.OK)
            {
                //// if the file already exists don't try and add it again!
                return !this.DoesFileExist(filePath);
            }

            //// if the user hasn't pressed the ok button then don't add items.
            return false;
        }

        /// <summary>
        /// Called when [project item finished generating].
        /// </summary>
        /// <param name="projectItem">The project item.</param>
        public override void OnProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            string message = "WebRequestServiceWizard::OnProjectItemFinishedGenerating projectItem=" + projectItem.Name;

            TraceService.WriteLine(message);

            projectItem.ReplaceText("SampleWebRequestService", this.entityName + "WebRequestService");
            projectItem.ReplaceText("SampleWebRequestTranslator", this.entityName + "Translator");
            projectItem.ReplaceText("WebRequestSampleData", this.entityName);
                            
            ////projectItem.SortAndRemoveUsingStatements();
        }
    }
}
