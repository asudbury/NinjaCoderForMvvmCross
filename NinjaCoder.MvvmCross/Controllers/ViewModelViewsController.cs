// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewModelViewsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using System.Collections.Generic;
    using EnvDTE80;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Services;
    using Views;

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
            TraceService.WriteLine("ViewModelAndViewsController::Constructor");
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            this.AddTraceHeader("ViewModelAndViewsController", "Run");

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

                    //// we now need to amend code in the unit test file that references FirstViewModel to this ViewModel
                    this.UpdateUnitTestFile(form.ViewModelName);

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

        /// <summary>
        /// Updates the unit test file.
        /// </summary>
        /// <param name="viewModelName">Name of the view model.</param>
        internal void UpdateUnitTestFile(string viewModelName)
        {
            TraceService.WriteLine("ViewModelAndViewsController::UpdateUnitTestFile ViewModelName=" + viewModelName);

            IProjectService testProjectService = this.VisualStudioService.CoreTestsProjectService;

            if (testProjectService != null)
            {
                IProjectItemService projectItemService = testProjectService.GetProjectItem("Test" + viewModelName);

                if (projectItemService != null)
                {
                    projectItemService.ReplaceText("CoreTemplate.", "Core.");
                    projectItemService.ReplaceText("FirstViewModel", viewModelName);
                    projectItemService.ReplaceText("firstViewModel",  viewModelName.Substring(0, 1).ToLower() + viewModelName.Substring(1));
                }
            }
        }
    }
}
