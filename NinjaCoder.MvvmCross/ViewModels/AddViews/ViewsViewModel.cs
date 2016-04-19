// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddViews
{
    using Constants;
    using Entities;
    using Extensions;
    using Factories.Interfaces;
    using MahApps.Metro;
    using Scorchio.Infrastructure.Entities;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.Infrastructure.Services;
    using Scorchio.Infrastructure.Wpf;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Input;

    using Scorchio.VisualStudio.Services;

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
        /// The choose framework command.
        /// </summary>
        private RelayParameterCommand<object> chooseFrameworkCommand;

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
        /// The frameworks.
        /// </summary>
        private IEnumerable<ImageItemWithDescription> frameworks;

        /// <summary>
        /// The MVVM cross ios view types.
        /// </summary>
        private IEnumerable<string> mvvmCrossiOSViewTypes;

        /// <summary>
        /// The selected MVVM cross ios view type
        /// </summary>
        private string selectedMvvmCrossiOSViewType;

        /// <summary>
        /// The allow framework selection.
        /// </summary>
        private bool allowFrameworkSelection;

        /// <summary>
        /// The show frameworks.
        /// </summary>
        private bool showFrameworks;

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
        private bool showXfPages;

        /// <summary>
        /// The show layouts.
        /// </summary>
        private bool showLayouts;

        /// <summary>
        /// The show MVVM cross view types.
        /// </summary>
        private bool showMvvmCrossViewTypes;

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
        /// <param name="viewModelAndViewsFactory">The view model and views factory.</param>
        public ViewsViewModel(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IFrameworkFactory frameworkFactory,
            IXamarinPageFactory pageFactory,
            IXamarinLayoutFactory layoutFactory,
            IMessageBoxService messageBoxService,
            IMvvmCrossViewFactory mvvmCrossViewFactory,
            IViewModelAndViewsFactory viewModelAndViewsFactory)
        {
            this.views = new ObservableCollection<View>();

            this.visualStudioService = visualStudioService;
            this.settingsService = settingsService;
            this.frameworkFactory = frameworkFactory;
            this.Pages = pageFactory.Pages;
            this.Layouts = layoutFactory.Layouts;
            this.messageBoxService = messageBoxService;
            this.MvxViews = mvvmCrossViewFactory.Views;

            this.frameworks = frameworkFactory.AllowedFrameworks;

            this.allowFrameworkSelection = this.Frameworks.Count() > 1;

            this.DisplayGrid();

            this.MvvmCrossiOSViewTypes = viewModelAndViewsFactory.GetAvailableMvvmCrossiOSViewTypes();
            this.SelectedMvvmCrossiOSViewType = this.settingsService.SelectedMvvmCrossiOSViewType;
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
        /// Gets a value indicating whether [allow framework selection].
        /// </summary>
        public bool AllowFrameworkSelection
        {
            get { return this.allowFrameworkSelection; }
        }

        /// <summary>
        /// Gets the Add command.
        /// </summary>
        public ICommand AddCommand
        {
            get { return this.addCommand ?? (this.addCommand = new RelayCommand(this.Add)); }
        }

        /// <summary>
        /// Gets the choose framework command.
        /// </summary>
        public ICommand ChooseFrameworkCommand
        {
            get { return this.chooseFrameworkCommand ?? (this.chooseFrameworkCommand = new RelayParameterCommand<object>(this.ChooseFramework)); }
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
        /// Gets the xamarin pages help command.
        /// </summary>
        public ICommand XamarinPagesHelpCommand
        {
            get { return new RelayParameterCommand<object>(this.DisplayPagesWebPage); }
        }

        /// <summary>
        /// Gets the xamarin layouts help command.
        /// </summary>
        public ICommand XamarinLayoutsHelpCommand
        {
            get { return new RelayParameterCommand<object>(this.DisplayLayoutsWebPage); }
        }
        
        /// <summary>
        /// Gets the frameworks.
        /// </summary>
        public IEnumerable<ImageItemWithDescription> Frameworks
        {
            get { return this.frameworks; }
        }
 
        /// <summary>
        /// Gets the pages types.
        /// </summary>
        public IEnumerable<ImageItemWithDescription> Pages { get; }

        /// <summary>
        /// Gets the MVX views.
        /// </summary>
        public IEnumerable<ImageItemWithDescription> MvxViews { get; }

        /// <summary>
        /// Gets the layout types.
        /// </summary>
        public IEnumerable<ImageItemWithDescription> Layouts { get; }

        /// <summary>
        /// Gets or sets a value indicating whether [show grid].
        /// </summary>
        public bool ShowGrid
        {
            get { return this.showGrid; }
            set { this.SetProperty(ref this.showGrid, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show frameworks].
        /// </summary>
        public bool ShowFrameworks
        {
            get { return this.showFrameworks; }
            set { this.SetProperty(ref this.showFrameworks, value); }
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
        public bool ShowXfPages
        {
            get { return this.showXfPages; }
            set { this.SetProperty(ref this.showXfPages, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show layouts].
        /// </summary>
        public bool ShowLayouts
        {
            get { return this.showLayouts; }
            set { this.SetProperty(ref this.showLayouts, value); }
        }

        /// <summary>
        /// Gets or sets the MVVM cross ios view types.
        /// </summary>
        public IEnumerable<string> MvvmCrossiOSViewTypes
        {
            get { return this.mvvmCrossiOSViewTypes; }
            set { this.SetProperty(ref this.mvvmCrossiOSViewTypes, value); }
        }

        /// <summary>
        /// Gets or sets the type of the selected view.
        /// </summary>
        public string SelectedMvvmCrossiOSViewType
        {
            get { return this.selectedMvvmCrossiOSViewType; }
            set { this.SetProperty(ref this.selectedMvvmCrossiOSViewType, value); }
        }

        /// <summary>
        /// Gets the UI help page command.
        /// </summary>
        public ICommand UIHelpPageCommand
        {
            get { return new RelayCommand(this.DisplayUIHelpPage); }
        }

        /// <summary>
        /// Gets a value indicating whether [show MVVM cross view types].
        /// </summary>
        public bool ShowMvvmCrossViewTypes
        {
            get { return this.showMvvmCrossViewTypes; }
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

                if (this.showXfPages)
                {
                    imageItemWithDescription = this.GetImageItemWithDescription(thisView, this.Pages);

                    if (imageItemWithDescription != null)
                    {
                        thisView.PageType = imageItemWithDescription.Name;

                        //// tabbed pages and carousel pages have to have content pages as children.
                        if (thisView.PageType == XamarinFormsPage.TabbedPage.GetDescription() ||
                            thisView.PageType == XamarinFormsPage.CarouselPage.GetDescription())
                        {
                            thisView.LayoutType = XamarinFormsPage.ContentPage.GetDescription();
                        }
                    } 
                }
                else if (this.showMvxPages)
                {
                    imageItemWithDescription = this.GetImageItemWithDescription(thisView, this.MvxViews);

                    if (imageItemWithDescription != null)
                    {
                        thisView.PageType = imageItemWithDescription.Name;
                    }   
                }
                else if (this.showLayouts)
                {
                    imageItemWithDescription = this.GetImageItemWithDescription(thisView, this.Layouts);

                    if (imageItemWithDescription != null)
                    {
                        thisView.LayoutType = imageItemWithDescription.Name;
                    } 
                }
                else if (this.showFrameworks)
                {
                    imageItemWithDescription = this.GetImageItemWithDescription(thisView, this.Frameworks);

                    if (imageItemWithDescription != null)
                    {
                        if (thisView.Framework != imageItemWithDescription.Name)
                        {
                            if (imageItemWithDescription.Name == FrameworkType.XamarinForms.GetDescription())
                            {
                                thisView.PageType = "Content Page";
                                thisView.LayoutType = "Content View";
                            }
                            else
                            {
                                thisView.LayoutType = "Not Applicable";
                                thisView.PageType = "SampleData";
                            }
                        }

                        thisView.Framework = imageItemWithDescription.Name;
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
            this.frameworks = this.frameworkFactory.AllowedFrameworks;

            this.allowFrameworkSelection = this.frameworks.Count() > 1;

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

            this.showMvvmCrossViewTypes = false;

            if (this.visualStudioService.SolutionAlreadyCreated == false)
            {
                if (this.settingsService.FrameworkType.IsMvvmCrossSolutionType())
                {
                    if (this.settingsService.AddiOSProject)
                    {
                        this.showMvvmCrossViewTypes = true;
                    }
                }
            }
        }

        /// <summary>
        /// For when yous need to save some values that can't be directly bound to UI elements.
        /// Not called when moving previous (see WizardViewModel.MoveToNextStep).
        /// </summary>
        /// <returns>
        /// An object that may modify the route
        /// </returns>
        public override RouteModifier OnNext()
        {
            this.settingsService.SelectedMvvmCrossiOSViewType = this.SelectedMvvmCrossiOSViewType;
            TraceService.WriteDebugLine("ViewsViewModel::OnNext SelectedMvvmCrossiOSViewType=" + this.settingsService.SelectedMvvmCrossiOSViewType);
            return base.OnNext();
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
                view.PageType = this.settingsService.DefaultViewType;
            }
            
            this.Views.Add(view);
        }

        /// <summary>
        /// Chooses the framework.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ChooseFramework(object parameter)
        {
            this.currentView = this.views.FirstOrDefault(x => x.Name == parameter.ToString());

            if (this.currentView != null)
            {
                this.DisplayFrameworks();

                ImageItemWithDescription imageItemWithDescription = this.Frameworks.FirstOrDefault(x => x.Name == this.currentView.Framework);

                if (imageItemWithDescription != null)
                {
                    imageItemWithDescription.Selected = true;
                }
           }
        }

        /// <summary>
        /// Chooses the type of the page.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
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

                if (this.currentView.PageType == XamarinFormsPage.TabbedPage.GetDescription() ||
                    this.currentView.PageType == XamarinFormsPage.CarouselPage.GetDescription())
                {
                    this.currentView.LayoutType = "Content Page";
                }

                this.DisplayPages(this.currentView.Framework);
            }
        }

        /// <summary>
        /// Chooses the type of the layout.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ChooseLayoutType(object parameter)
        {
            this.currentView = this.views.FirstOrDefault(x => x.Name == parameter.ToString());

            if (this.currentView != null)
            {
                if (this.currentView.PageType == XamarinFormsPage.TabbedPage.GetDescription() || 
                    this.currentView.PageType == XamarinFormsPage.CarouselPage.GetDescription())
                {
                    this.messageBoxService.Show(
                          "Tabbed Pages and Carousel Pages can only have Content Layouts.",
                          Settings.ApplicationName);
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
            this.ShowFrameworks = false;
            this.ShowMvxPages = false;
            this.ShowXfPages = false;
            this.ShowLayouts = false;
        }

        /// <summary>
        /// Displays the frameworks.
        /// </summary>
        internal void DisplayFrameworks()
        {
            this.ShowGrid = false;
            this.ShowFrameworks = true;
            this.ShowMvxPages = false;
            this.ShowXfPages = false;
            this.ShowLayouts = false;
        }

        /// <summary>
        /// Displays the pages.
        /// </summary>
        /// <param name="framework">The framework.</param>
        internal void DisplayPages(string framework)
        {
            this.ShowGrid = false;
            this.ShowFrameworks = false;

            //// work out if we are mvvmcross or xamarin forms

            if (framework == "MvvmCross")
            {
                this.ShowMvxPages = true;
            }
            else
            {
                this.ShowXfPages = true;
            }

            this.ShowLayouts = false;
        }

        /// <summary>
        /// Displays the layouts.
        /// </summary>
        internal void DisplayLayouts()
        {
            this.ShowGrid = false;
            this.ShowFrameworks = false;
            this.ShowMvxPages = false;
            this.ShowXfPages = false;
            this.ShowLayouts = true;
        }
        
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns>The name of the viewmodel and views.</returns>
        internal string GetName()
        {
            string name;

            string[] viewNames = { "Main", "Second", "Third", "Fourth", "Fifth", "Sixth", "Seventh", "Eighth", "Ninth", "Tenth" };

            if (this.Views.Count < 10)
            {
                name = viewNames[this.Views.Count()];
            }
            else
            {
                name = "No" + (this.Views.Count + 1).ToString(CultureInfo.InvariantCulture);
            }

            //// make sure the view name is unique!
            if (this.views.FirstOrDefault(x => x.Name == name) != null)
            {
                name += "a";
            }

            return name;
        }

        /// <summary>
        /// Gets the image item with description.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="items">The items.</param>
        /// <returns>An ImageItemWithDescription.</returns>
        internal ImageItemWithDescription GetImageItemWithDescription(
            View view,
            IEnumerable<ImageItemWithDescription> items)
        {
            ImageItemWithDescription imageItemWithDescription = items.FirstOrDefault(x => x.Selected);

            //// bug im my code or WPF - doesnt seem to uncheck radio buttons in a group !!
            IEnumerable<ImageItemWithDescription> multiupleItems = items.Where(x => x.Selected);

            ImageItemWithDescription[] imageItemWithDescriptions = multiupleItems as ImageItemWithDescription[] ?? multiupleItems.ToArray();

            if (imageItemWithDescriptions.Count() == 2)
            {
                ImageItemWithDescription firstItem = imageItemWithDescriptions.ToList()[0];

                if (this.showLayouts)
                {
                    if (view.LayoutType != firstItem.Name)
                    {
                        imageItemWithDescription = firstItem;
                    }

                    ImageItemWithDescription secondItem = imageItemWithDescriptions.ToList()[1];

                    if (view.LayoutType != secondItem.Name)
                    {
                        imageItemWithDescription = secondItem;
                    }                    
                }
                else if (this.showFrameworks)
                {
                    if (view.Framework != firstItem.Name)
                    {
                        imageItemWithDescription = firstItem;
                    }

                    ImageItemWithDescription secondItem = imageItemWithDescriptions.ToList()[1];

                    if (view.Framework != secondItem.Name)
                    {
                        imageItemWithDescription = secondItem;
                    } 
                }
                else 
                {
                    if (view.PageType != firstItem.Name)
                    {
                        imageItemWithDescription = firstItem;
                    }

                    ImageItemWithDescription secondItem = imageItemWithDescriptions.ToList()[1];

                    if (view.PageType != secondItem.Name)
                    {
                        imageItemWithDescription = secondItem;
                    }                    
                }
            }

            return imageItemWithDescription;
        }

        /// <summary>
        /// Displays the pages web page.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        internal void DisplayPagesWebPage(object parameter)
        {
            Process.Start(this.settingsService.XamarinPagesHelp);
        }

        /// <summary>
        /// Displays the layouts web page.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        internal void DisplayLayoutsWebPage(object parameter)
        {
            Process.Start(this.settingsService.XamarinLayoutsHelp);
        }

        /// <summary>
        /// Displays the UI help page.
        /// </summary>
        internal void DisplayUIHelpPage()
        {
            Process.Start(this.settingsService.MvvmCrossiOSViewWebPage);
        }
    }
}
