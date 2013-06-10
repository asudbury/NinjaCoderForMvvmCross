// ---------------------- ----------------------------------------------------------------------------------------------
// <summary>
//    Defines the ConvertersController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using System.Collections.Generic;

    using EnvDTE;

    using NinjaCoder.MvvmCross.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.Views;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;

    /// <summary>
    /// Defines the ConvertersController type.
    /// </summary>
    public class ConvertersController : BaseController
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertersController"/> class.
        /// </summary>
        public ConvertersController()
            : this(new SettingsService())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertersController" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public ConvertersController(ISettingsService settingsService)
            : base(new VisualStudioService(), new ReadMeService(), new SettingsService())
        {
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            this.AddTraceHeader("ConvertersController::Run");

            if (this.VisualStudioService.IsMvvmCrossSolution)
            {
                string templatesPath = this.settingsService.ConvertersTemplatesPath;

                List<ItemTemplateInfo> itemTemplateInfos = this.VisualStudioService.GetFolderTemplateInfos(templatesPath, "Converters");

                ItemTemplatesForm form = new ItemTemplatesForm(itemTemplateInfos);

                form.ShowDialog();

                if (form.Continue)
                {
                    this.WriteStatusBarMessage("Ninja Coder is running....");

                    Project project = this.VisualStudioService.CoreProject;

                    if (project != null)
                    {
                        List<string> messages = new List<string>();

                        foreach (ItemTemplateInfo templateInfo in form.RequiredTemplates)
                        {
                            TraceService.WriteLine("MvvmCrossController::AddConverters adding from template path " + templatesPath + " template=" + templateInfo.FileName);

                            string fileName = templateInfo.FriendlyName + ".cs";

                            project.AddToFolderFromTemplate("Converters", templateInfo.FileName, fileName, false);

                            ProjectItem projectItem = project.GetProjectItem(fileName);

                            //// if we find the project item replace the text in it.
                            if (projectItem != null)
                            {
                                TextSelection textSelection = projectItem.DTE.ActiveDocument.Selection;
                                textSelection.SelectAll();
                                textSelection.ReplacePattern("MvvmCross." + templateInfo.FriendlyName, project.Name);
                                projectItem.Save();
                            }

                            messages.Add(@"Converters\" + fileName + " added to project " + project.Name + ".");
                        }

                        //// close any open documents.
                        this.VisualStudioService.DTE2.CloseDocuments();

                        //// now collapse the solution!
                        this.VisualStudioService.DTE2.CollapseSolution();

                        //// show the readme.
                        this.ShowReadMe("Add Converters", messages);

                        this.WriteStatusBarMessage("Ninja Coder has completed the adding of the converters.");
                    }
                    else
                    {
                        TraceService.WriteError("MvvmCrossController::AddConverters cannot find Core project");
                    }
                }
            }
            else
            {
                this.ShowNotMvvmCrossSolutionMessage();
            }
        }
    }
}
