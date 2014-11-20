// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddProjects
{
    using MahApps.Metro;
    using NinjaCoder.MvvmCross.Constants;
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using Scorchio.Infrastructure.Entities;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.Infrastructure.Services;
    using Scorchio.Infrastructure.Wpf;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Input;

    /// <summary>
    /// Defines the ViewsViewModel type.
    /// </summary>
    public class ViewsViewModel : BaseWizardStepViewModel
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
        /// The framework factory.
        /// </summary>
        private readonly IFrameworkFactory frameworkFactory;

        /// <summary>
        /// The message box service.
        /// </summary>
        private readonly IMessageBoxService messageBoxService;

        /// <summary>
        /// The views.
        /// </summary>
        private ObservableCollection<View> views;

        /// <summary>
        /// The current view.
        /// </summary>
        private View currentView;

        /// <summary>
        /// The add command.
        /// </summary>
        private RelayCommand addCommand;

        /// <summary>
        /// The layout types.
        /// </summary>
        private readonly IEnumerable<ImageItemWithDescription> layoutTypes;

        /// <summary>
        /// The frameworks.
        /// </summary>
        private IEnumerable<string> frameworks;

        /// <summary>
        /// The choose page type command.
        /// </summary>
        private RelayParameterCommand<object> choosePageTypeCommand;

        /// <summary>
        /// The choose layout type command.
        /// </summary>
        private RelayParameterCommand<object> chooseLayoutTypeCommand;

        /// <summary>
        /// The delete view command.
        /// </summary>
        private RelayParameterCommand<object> deleteViewCommand;

        /// <summary>
        /// The page types.
        /// </summary>
        private readonly IEnumerable<ImageItemWithDescription> pageTypes;

        /// <summary>
        /// The MVX views.
        /// </summary>
        private readonly IEnumerable<ImageItemWithDescription> mvxViews;

        /// <summary>
        /// The show grid.
        /// </summary>
        private bool showGrid;

        /// <summary>
        /// The show mvx pages.
        /// </summary>
        private bool showMvxPages;
        
        /// <summary>
        /// The show XF Pages.
        /// </summary>
        private bool showXFPages;

        /// <summary>
        /// The show layouts.
        /// </summary>
        private bool showLayouts;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewsViewModel" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="frameworkFactory">The framework factory.</param>
        /// <param name="pageFactory">The page factory.</param>
        /// <param name="layoutFactory">The layout factory.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="mvvmCrossViewFactory">The MVVM cross view factory.</param>
        public ViewsViewModel(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IFrameworkFactory frameworkFactory,
            IXamarinPageFactory pageFactory,
            IXamarinLayoutFactory layoutFactory,
            IMessageBoxService messageBoxService,
            IMvvmCrossViewFactory mvvmCrossViewFactory)
        {
            this.views = new ObservableCollection<View>();

            this.visualStudioService = visualStudioService;
            this.settingsService = settingsService;
            this.frameworkFactory = frameworkFactory;
            this.pageTypes = pageFactory.Pages;
            this.layoutTypes = layoutFactory.Layouts;
            this.messageBoxService = messageBoxService;
            this.mvxViews = mvvmCrossViewFactory.Views;

            this.Frameworks = frameworkFactory.AllowedFrameworks;

            this.DisplayGrid();
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public override string DisplayName
        {
            get { return "ViewModels and Views"; }
        }

        /// <summary>
        /// Gets or sets the views.
        /// </summary>
        public ObservableCollection<View> Views
        {
            get { return this.views; }
            set { this.SetProperty(ref this.views, value); }
        }

        /// <summary>
        /// Gets the frameworks.
        /// </summary>
        public IEnumerable<string> Frameworks
        {
            get {return this.frameworks; }
            set { this.SetProperty(ref this.frameworks, value); }
        } 

        /// <summary>
        /// Gets the Add command.
        /// </summary>
        public ICommand AddCommand
        {
            get { return this.addCommand ?? (this.addCommand = new RelayCommand(this.Add)); }
        }
        
        /// <summary>
        /// Gets the choose page type command.
        /// </summary>
        public ICommand ChoosePageTypeCommand
        {
            get { return this.choosePageTypeCommand ?? (this.choosePageTypeCommand = new RelayParameterCommand<object>(this.ChoosePageType)); }
        }

        /// <summary>
        /// Gets the choose layout type command command.
        /// </summary>
        public ICommand ChooseLayoutTypeCommand
        {
            get { return this.chooseLayoutTypeCommand ?? (this.chooseLayoutTypeCommand = new RelayParameterCommand<object>(this.ChooseLayoutType)); }
        }

        /// <summary>
        /// Gets the delete view command.
        /// </summary>
        public ICommand DeleteViewCommand
        {
            get { return this.deleteViewCommand ?? (this.deleteViewCommand = new RelayParameterCommand<object>(this.DeleteView)); }
        }
        /// <summary>
        /// Gets the pages types.
        /// </summary>
        public IEnumerable<ImageItemWithDescription> Pages
        {
            get { return this.pageTypes; }
        }

        /// <summary>
        /// Gets the MVX views.
        /// </summary>
        public IEnumerable<ImageItemWithDescription > MvxViews
        {
            get { return this.mvxViews; }
        }

        /// <summary>
        /// Gets the layout types.
        /// </summary>
        public IEnumerable<ImageItemWithDescription> Layouts
        {
            get { return this.layoutTypes; }
        }

        /// <summary>
        /// Gets a value indicating whether [show grid].
        /// </summary>
        public bool ShowGrid
        {
            get { return this.showGrid; }
            set { this.SetProperty(ref this.showGrid, value); }
        }


        /// <summary>
        /// Gets or sets a value indicating whether [show MVX pages].
        /// </summary>
        public bool ShowMvxPages
        {
            get { return this.showMvxPages; }
            set { this.SetProperty(ref this.showMvxPages, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show xf pages].
        /// </summary>
        public bool ShowXFPages
        {
            get { return this.showXFPages; }
            set { this.SetProperty(ref this.showXFPages, value); }
        }

        /// <summary>
        /// Gets a value indicating whether [show layouts].
        /// </summary>
        public bool ShowLayouts
        {
            get { return this.showLayouts; }
            set { this.SetProperty(ref this.showLayouts, value); }
        }

        /// <summary>
        /// Determines whether this instance [can move to next page].
        /// </summary>
        public override bool CanMoveToNextPage()
        {
            if (this.showGrid)
            {
                return true;
            }

            ObservableCollection<View> currentViews = this.views;

            this.Views = null;

            View thisView = currentViews.FirstOrDefault(x => x.Name == this.currentView.Name);

            if (thisView != null)
            {
                ImageItemWithDescription imageItemWithDescription;

                if (this.showXFPages)
                {
                    imageItemWithDescription = this.Pages.FirstOrDefault(x => x.Selected);

                    //// bug im my code or WPF - doesnt seem to uncheck radio buttons in a group !!
                    IEnumerable<ImageItemWithDescription> items = this.Pages.Where(x => x.Selected);

                    ImageItemWithDescription[] imageItemWithDescriptions = items as ImageItemWithDescription[] ?? items.ToArray();

                    if (imageItemWithDescriptions.Count() == 2)
                    {
                        ImageItemWithDescription firstItem = imageItemWithDescriptions.ToList()[0];

                        if (thisView.PageType != firstItem.Name)
                        {
                            imageItemWithDescription = firstItem;
                        }

                        ImageItemWithDescription secondItem = imageItemWithDescriptions.ToList()[1];

                        if (thisView.PageType != secondItem.Name)
                        {
                            imageItemWithDescription = secondItem;
                        }
                    }

                    if (imageItemWithDescription != null)
                    {
                        thisView.PageType = imageItemWithDescription.Name;
                    }
                }

                else if (this.showMvxPages)
                {
                    imageItemWithDescription = this.MvxViews.FirstOrDefault(x => x.Selected);

                    //// bug im my code or WPF - doesnt seem to uncheck radio buttons in a group !!
                    IEnumerable<ImageItemWithDescription> items = this.MvxViews.Where(x => x.Selected);

                    ImageItemWithDescription[] imageItemWithDescriptions = items as ImageItemWithDescription[] ?? items.ToArray();

                    if (imageItemWithDescriptions.Count() == 2)
                    {
                        ImageItemWithDescription firstItem = imageItemWithDescriptions.ToList()[0];

                        if (thisView.PageType != firstItem.Name)
                        {
                            imageItemWithDescription = firstItem;
                        }

                        ImageItemWithDescription secondItem = imageItemWithDescriptions.ToList()[1];

                        if (thisView.PageType != secondItem.Name)
                        {
                            imageItemWithDescription = secondItem;
                        }
                    }

                    if (imageItemWithDescription != null)
                    {
                        thisView.PageType = imageItemWithDescription.Name;
                    }   
                }

                else if (this.showLayouts)
                {
                    imageItemWithDescription = this.Layouts.FirstOrDefault(x => x.Selected);

                    //// bug im my code or WPF - doesnt seem to uncheck radio buttons in a group !!
                    IEnumerable<ImageItemWithDescription> items = this.Layouts.Where(x => x.Selected);

                    ImageItemWithDescription[] imageItemWithDescriptions = items as ImageItemWithDescription[] ?? items.ToArray();

                    if (imageItemWithDescriptions.Count() == 2)
                    {
                        ImageItemWithDescription firstItem = imageItemWithDescriptions.ToList()[0];

                        if (thisView.PageType != firstItem.Name)
                        {
                            imageItemWithDescription = firstItem;
                        }

                        ImageItemWithDescription secondItem = imageItemWithDescriptions.ToList()[1];

                        if (thisView.PageType != secondItem.Name)
                        {
                            imageItemWithDescription = secondItem;
                        }
                    }

                    if (imageItemWithDescription != null)
                    {
                        thisView.LayoutType = imageItemWithDescription.Name;
                    }
                }
            }

            this.Views = currentViews;

            this.DisplayGrid();

            return false;
        }

        /// <summary>
        /// Determines whether this instance [can move to previous page].
        /// </summary>
        public override bool CanMoveToPreviousPage()
        {
            if (this.showGrid)
            {
                return true;
            }

            this.DisplayGrid();

            return false;
        }

        /// <summary>
        /// Called when [initialize].
        /// </summary>
        public override void OnInitialize()
        {
            this.Frameworks = frameworkFactory.AllowedFrameworks;

            if (!this.views.Any())
            {
                ///// lets work out which viewmodels already exist and add them to the screen
                IEnumerable<string> currentViewModels = this.visualStudioService.GetPublicViewModelNames();

                if (currentViewModels != null)
                {
                    foreach (View view in currentViewModels.Select(viewModel => new View
                    {
                        Name = viewModel.Replace("ViewModel", string.Empty),
                        Existing = true,
                        AllowDeletion = false
                    }))
                    {
                        this.Views.Add(view);
                    }
                }

                else
                {
                    this.Add(); 
                }
            }
        }

        /// <summary>
        /// Adds this instance.
        /// </summary>
        public void Add()
        {
            View view = new View
            {
                Name = this.GetName(), 
                AllowDeletion = true
            };

            //// dont allow the main viewmodel to be deleted.
            if (view.Name == "Main")
            {
                view.AllowDeletion = false;
            }

            if (this.settingsService.FrameworkType == FrameworkType.XamarinForms ||
                this.settingsService.FrameworkType == FrameworkType.MvvmCrossAndXamarinForms)
            {
                view.Framework = FrameworkType.XamarinForms.GetDescription();
                view.PageType = "Content Page";
                view.LayoutType = "Content View";
            }

            else
            {
                view.Framework = FrameworkType.MvvmCross.GetDescription();
                view.LayoutType = "Not Applicable";
                view.PageType = "SampleData";
            }
            
            this.Views.Add(view);
        }

        /// <summary>
        /// Chooses the type of the page.
        /// </summary>
        public void ChoosePageType(object parameter)
        {
            this.currentView = this.views.FirstOrDefault(x => x.Name == parameter.ToString());
            
            if (this.currentView != null)
            {
                ImageItemWithDescription imageItemWithDescription = this.Pages.FirstOrDefault(x => x.Name == this.currentView.PageType);

                if (imageItemWithDescription != null)
                {
                    imageItemWithDescription.Selected = true;
                }

                else
                {
                    imageItemWithDescription = this.MvxViews.FirstOrDefault(x => x.Name == this.currentView.PageType);

                    if (imageItemWithDescription != null)
                    {
                        imageItemWithDescription.Selected = true;
                    }                    
                }

                this.DisplayPages(this.currentView.Framework);
            }
        }

        /// <summary>
        /// Chooses the type of the layout.
        /// </summary>
        public void ChooseLayoutType(object parameter)
        {
            this.currentView = this.views.FirstOrDefault(x => x.Name == parameter.ToString());

            if (this.currentView != null)
            {
                if (this.currentView.Framework == FrameworkType.MvvmCross.GetDescription())
                {
                    this.messageBoxService.Show(
                        "Not applicable to MvvmCross views.",
                        Settings.ApplicationName,
                        true,
                        Theme.Light,
                        this.settingsService.ThemeColor);
                    return;
                }
            }

            this.DisplayLayouts();

            ImageItemWithDescription imageItemWithDescription = this.Layouts.FirstOrDefault(x => x.Name == this.currentView.LayoutType);

            if (imageItemWithDescription != null)
            {
                imageItemWithDescription.Selected = true;
            }
        }

        /// <summary>
        /// Deletes the view.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void DeleteView(object parameter)
        {
            View view = this.views.FirstOrDefault(x => x.Name == parameter.ToString());

            if (view != null)
            {
                this.views.Remove(view);
            }
        }

        /// <summary>
        /// Displays the grid.
        /// </summary>
        internal void DisplayGrid()
        {
            this.ShowGrid = true;
            this.ShowMvxPages = false;
            this.ShowXFPages = false;
            this.ShowLayouts = false;
        }

        /// <summary>
        /// Displays the pages.
        /// </summary>
        /// <param name="framework">The framework.</param>
        internal void DisplayPages(string framework)
        {
            this.ShowGrid = false;

            //// work out if we are mvvmcross or xamarin forms

            if (framework == "MvvmCross")
            {
                this.ShowMvxPages = true;
            }

            else
            {
                this.ShowXFPages = true;
            }

            this.ShowLayouts = false;
        }

        /// <summary>
        /// Displays the layouts.
        /// </summary>
        internal void DisplayLayouts()
        {
            this.ShowGrid = false;
            this.showMvxPages = false;
            this.showXFPages = false;
            this.ShowLayouts = true;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns>The name of the viewmodel and views.</returns>
        internal string GetName()
        {
            string[] viewNames = { "Main", "Second", "Third", "Fourth", "Fifth", "Sixth", "Seventh", "Eighth", "Nineth", "Tenth" };

            if (this.Views.Count < 10)
            {
                return viewNames[this.Views.Count()];
            }

            return "No" + (this.Views.Count+1).ToString(CultureInfo.InvariantCulture);
        }
    }
}
