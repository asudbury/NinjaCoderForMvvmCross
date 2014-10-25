// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewModelViewsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels
{
    using Constants;
    using Factories.Interfaces;

    using Scorchio.Infrastructure.Extensions;
    using Scorchio.Infrastructure.Services;
    using Scorchio.Infrastructure.Wpf.ViewModels;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Defines the ViewModelViewsViewModel type.
    /// </summary>
    internal class ViewModelViewsViewModel : NinjaBaseViewModel
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
        /// The message box service.
        /// </summary>
        private readonly IMessageBoxService messageBoxService;
        
        /// <summary>
        /// The view model and views factory
        /// </summary>
        private readonly IViewModelAndViewsFactory viewModelAndViewsFactory;

        /// <summary>
        /// The allowed views.
        /// </summary>
        private readonly ObservableCollection<SelectableItemViewModel<ItemTemplateInfo>> allowedViews;

        /// <summary>
        /// The view model name.
        /// </summary>
        private string viewModelName = "ViewModel";

        /// <summary>
        /// The view model initiated from.
        /// </summary>
        private string viewModelInitiatedFrom;

        /// <summary>
        /// The view model navigate to.
        /// </summary>
        private string viewModelNavigateTo;

        /// <summary>
        /// The include unit tests.
        /// </summary>
        private bool includeUnitTests = true;

        /// <summary>
        /// The view model names.
        /// </summary>
        private IEnumerable<string> viewModelNames;

        /// <summary>
        /// The view model name is focused.
        /// </summary>
        private bool viewModelNameIsFocused;

        /// <summary>
        /// The view types.
        /// </summary>
        private readonly IEnumerable<string> viewTypes;

        /// <summary>
        /// The selected view type.
        /// </summary>
        private string selectedViewType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelViewsViewModel" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="viewModelAndViewsFactory">The view model and views factory.</param>
        public ViewModelViewsViewModel(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            IViewModelAndViewsFactory viewModelAndViewsFactory)
            : base(settingsService)
        {
            this.visualStudioService = visualStudioService;
            this.settingsService = settingsService;
            this.messageBoxService = messageBoxService;
            this.viewModelAndViewsFactory = viewModelAndViewsFactory;

            this.allowedViews = new ObservableCollection<SelectableItemViewModel<ItemTemplateInfo>>();

            foreach (SelectableItemViewModel<ItemTemplateInfo> view in
                this.viewModelAndViewsFactory.AllowedUIViews
                .Select(itemTemplateInfo => new SelectableItemViewModel<ItemTemplateInfo>(itemTemplateInfo)))
            {
                view.IsSelected = true;
                this.allowedViews.Add(view);
            }

            this.viewTypes = viewModelAndViewsFactory.GetAvailableViewTypes();
            this.selectedViewType = this.settingsService.SelectedViewType;
        }

        /// <summary>
        /// Gets or sets the name of the view model.
        /// </summary>
        public string ViewModelName
        {
            get { return this.viewModelName.CaptialiseFirstCharacter(); }
            set { this.SetProperty(ref this.viewModelName, value.CaptialiseFirstCharacter()); }
        }

        /// <summary>
        /// Gets or sets the view model initiated from.
        /// </summary>
        public string ViewModelInitiatedFrom
        {
            get { return this.viewModelInitiatedFrom; }
            set { this.SetProperty(ref this.viewModelInitiatedFrom, value); }
        }

        /// <summary>
        /// Gets or sets the view model to navigate to.
        /// </summary>
        public string ViewModelToNavigateTo
        {
            get { return this.viewModelNavigateTo; }
            set { this.SetProperty(ref this.viewModelNavigateTo, value); }
        }

        /// <summary>
        /// Gets or sets the type of the selected view.
        /// </summary>
        public string SelectedViewType
        {
            get { return this.selectedViewType; }
            set { this.SetProperty(ref this.selectedViewType, value); }
        }

        /// <summary>
        /// Gets the type of the view.
        /// </summary>
        public IEnumerable<string> ViewTypes
        {
            get { return this.viewTypes; }
        }

        /// <summary>
        /// Gets the allowed views.
        /// </summary>
        public IEnumerable<SelectableItemViewModel<ItemTemplateInfo>> AllowedViews
        {
            get { return this.allowedViews; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [include unit tests].
        /// </summary>
        public bool IncludeUnitTests
        {
            get { return this.includeUnitTests; }
            set { this.SetProperty(ref this.includeUnitTests, value); }
        }

        /// <summary>
        /// Gets the view model names.
        /// </summary>
        public IEnumerable<string> ViewModelNames
        {
            get { return this.viewModelNames ?? (this.viewModelNames = this.visualStudioService.GetPublicViewModelNames()); }
        }

        /// <summary>
        /// Gets the required templates.
        /// </summary>
        public IEnumerable<ItemTemplateInfo> RequiredTemplates
        {
            get
            {
                List<ItemTemplateInfo> requiredUIViews = this.AllowedViews
                    .Where(v => v.IsSelected)
                    .Select(view => view.Item).ToList();

                return this.viewModelAndViewsFactory.GetRequiredViewModelAndViews(
                    null,
                    this.ViewModelName,
                    requiredUIViews,
                    this.IncludeUnitTests);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [view model name is focused].
        /// </summary>
        public bool ViewModelNameIsFocused
        {
            get { return this.viewModelNameIsFocused; }
            set { this.SetProperty(ref this.viewModelNameIsFocused, value); }
        }

        /// <summary>
        /// Called when ok button pressed.
        /// </summary>
        public override void OnOk()
        {
            if (this.DoesViewModelAlreadyExist())
            {
                this.messageBoxService.Show(
                    Settings.ViewModelExists,
                    Settings.ApplicationName,
                    true,
                    this.CurrentTheme,
                    this.settingsService.ThemeColor);

                this.ViewModelNameIsFocused = true;
            }
            else
            {
                this.settingsService.SelectedViewType = this.selectedViewType ?? "SampleData";
                this.settingsService.SelectedViewPrefix = this.ViewModelName.Replace("ViewModel", string.Empty);
                base.OnOk();
            }
        }

        /// <summary>
        /// Doeses the view model already exist.
        /// </summary>
        /// <returns>True or false.</returns>
        internal bool DoesViewModelAlreadyExist()
        {
            IProjectService projectService = this.visualStudioService.CoreProjectService;

            if (projectService != null)
            {
                IEnumerable<string> files = projectService.GetFolderItems("ViewModels", false);

                if (files.Contains(this.viewModelName))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
