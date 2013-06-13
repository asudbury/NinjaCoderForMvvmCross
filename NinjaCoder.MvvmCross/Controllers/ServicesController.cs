// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ServicesController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;

    using EnvDTE;

    using NinjaCoder.MvvmCross.Services;
    using NinjaCoder.MvvmCross.Views;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;

    /// <summary>
    /// Defines the ServicesController type.
    /// </summary>
    public class ServicesController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServicesController"/> class.
        /// </summary>
        public ServicesController()
            : base(new VisualStudioService(), new ReadMeService(), new SettingsService())
        {
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            this.AddTraceHeader("ServicesController::Run");

            if (this.VisualStudioService.IsMvvmCrossSolution)
            {
                string templatesPath = this.SettingsService.ServicesTemplatesPath;

                List<ItemTemplateInfo> itemTemplateInfos = this.VisualStudioService.GetFolderTemplateInfos(templatesPath, "Services");

                ServicesForm form = new ServicesForm(this.GetViewModelNames(), itemTemplateInfos);

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
                            TraceService.WriteLine("MvvmCrossController::AddServices adding from template path " + templatesPath + " template=" + templateInfo.FileName);

                            string fileName = templateInfo.FriendlyName + ".cs";

                            project.AddToFolderFromTemplate("Services", templateInfo.FileName, fileName, false);

                            //// do we need to add any using statements??
                            string configFile = string.Format(@"{0}\Services\Services.{1}.Config.xml", this.SettingsService.CodeSnippetsPath, templateInfo.FriendlyName);

                            if (File.Exists(configFile))
                            {
                                string line;

                                string sourceDirectory = this.SettingsService.CorePluginsPath;

                                StreamReader streamReader = new StreamReader(configFile);

                                while ((line = streamReader.ReadLine()) != null)
                                {
                                    project.AddReference("Lib", project.GetProjectPath() + @"\Lib\" + line, sourceDirectory + @"\" + line);
                                }

                                streamReader.Close();
                            }

                            messages.Add(@"Services\" + fileName + " added to project " + project.Name + ".");
                        }

                        //// close any open documents.
                        this.VisualStudioService.DTE2.CloseDocuments();

                        //// now collapse the solution!
                        this.VisualStudioService.DTE2.CollapseSolution();

                        //// show the readme.
                        this.ShowReadMe("Add Services", messages);

                        this.WriteStatusBarMessage("Ninja Coder has completed the adding of the services.");
                    }
                    else
                    {
                        TraceService.WriteError("MvvmCrossController::AddServices Cannot find Core project");
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
