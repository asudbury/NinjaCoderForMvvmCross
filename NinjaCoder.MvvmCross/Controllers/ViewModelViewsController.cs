// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewModelViewsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using System.Collections.Generic;

    using EnvDTE80;

    using NinjaCoder.MvvmCross.Services;
    using NinjaCoder.MvvmCross.Views;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;

    /// <summary>
    /// Defines the ViewModelViewsController type.
    /// </summary>
    public class ViewModelViewsController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelViewsController"/> class.
        /// </summary>
        public ViewModelViewsController()
            : base(new VisualStudioService(), new ReadMeService(), new SettingsService())
        {
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            this.AddTraceHeader("ViewModelAndViewsController::Run");

            if (this.VisualStudioService.IsMvvmCrossSolution)
            {
                List<ItemTemplateInfo> templateInfos = this.VisualStudioService.AllowedItemTemplates;

                ViewModelOptionsView form = new ViewModelOptionsView(templateInfos);

                form.ShowDialog();

                if (form.Continue)
                {
                    this.VisualStudioService.DTE2.WriteStatusBarMessage("Ninja Coder is running....");

                    Solution2 solution = this.VisualStudioService.DTE2.GetSolution() as Solution2;

                    List<string> messages = solution.AddItemTemplateToProjects(form.Presenter.GetRequiredItemTemplates(), true);

                    //// close any open documents.
                    this.VisualStudioService.DTE2.CloseDocuments();

                    //// now collapse the solution!
                    this.VisualStudioService.DTE2.CollapseSolution();

                    //// show the readme.
                    this.ShowReadMe("Add ViewModel and Views", messages);

                    this.WriteStatusBarMessage("Ninja Coder has completed the adding of the viewmodel and views.");
                }
            }
            else
            {
                this.ShowNotMvvmCrossSolutionMessage();
            }
        }
    }
}
