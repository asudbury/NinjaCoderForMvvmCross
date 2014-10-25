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
        /// <param name="settingsService">The settings service.</param>
        /// <param name="frameworkFactory">The framework factory.</param>
        /// <param name="pageFactory">The page factory.</param>
        /// <param name="layoutFactory">The layout factory.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="mvvmCrossViewFactory">The MVVM cross view factory.</param>
        public ViewsViewModel(
            ISettingsService settingsService,
            IFrameworkFactory frameworkFactory,
            IXamarinPageFactory pageFactory,
            IXamarinLayoutFactory layoutFactory,
            IMessageBoxService messageBoxService,
            IMvvmCrossViewFactory mvvmCrossViewFactory)
        {
            this.views = new ObservableCollection<View>();

            this.settingsService = settingsService;
            this.frameworkFactory = frameworkFactory;
            this.pageTypes = pageFactory.Pages;
            this.layoutTypes = layoutFactory.Layouts;
            this.messageBoxService = messageBoxService;
            this.mvxViews = mvvmCrossViewFactory.Views;

            this.Frameworks = frameworkFactory.AllowedFrameworks;

            this.DisplayGrid();

            this.Add();
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
                if (this.showXFPages)
                {
                    ImageItemWithDescription imageItemWithDescription = this.Pages.FirstOrDefault(x => x.Selected);

                    if (imageItemWithDescription != null)
                    {
                        thisView.PageType = imageItemWithDescription.Name;
                    }
                }

                else if (this.showMvxPages)
                {
                    ImageItemWithDescription imageItemWithDescription = this.MvxViews.FirstOrDefault(x => x.Selected);

                    if (imageItemWithDescription != null)
                    {
                        thisView.PageType = imageItemWithDescription.Name;
                    }   
                }

                else if (this.showLayouts)
                {
                    ImageItemWithDescription imageItemWithDescription = this.Layouts.FirstOrDefault(x => x.Selected);

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
        }

        /// <summary>
        /// Adds this instance.
        /// </summary>
        public void Add()
        {
            View view = new View
                            {
                                Name = this.GetName(), 
                                PageType = "Content Page", 
                                LayoutType = "Content View"
                            };

            if (this.settingsService.FrameworkType == FrameworkType.XamarinForms ||
                this.settingsService.FrameworkType == FrameworkType.MvvmCrossAndXamarinForms)
            {
                view.Framework = FrameworkType.XamarinForms.GetDescription();
            }

            else
            {
                view.Framework = FrameworkType.MvvmCross.GetDescription();
                view.PageType = this.settingsService.SelectedViewType;
                view.LayoutType = "Not Applicable";
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
                ImageItemWithDescription ImageItemWithDescription = this.Pages.FirstOrDefault(x => x.Name == this.currentView.PageType);

                if (ImageItemWithDescription != null)
                {
                    ImageItemWithDescription.Selected = true;
                }

                else
                {
                    ImageItemWithDescription = this.MvxViews.FirstOrDefault(x => x.Name == this.currentView.PageType);

                    if (ImageItemWithDescription != null)
                    {
                        ImageItemWithDescription.Selected = true;
                    }                    
                }
            }

            this.DisplayPages(this.currentView.Framework);
        }

        /// <summary>
        /// Chooses the type of the layout.
        /// </summary>
        public void ChooseLayoutType(object parameter)
        {
            this.messageBoxService.Show(
                "Not currently available - Will be available in a future release.",
                Settings.ApplicationName,
                true,
                Theme.Light,
                this.settingsService.ThemeColor);

            return;
            this.currentView = this.views.FirstOrDefault(x => x.Name == parameter.ToString());

            if (this.currentView != null)
            {
                ImageItemWithDescription ImageItemWithDescription = this.Layouts.FirstOrDefault(x => x.Name == this.currentView.LayoutType);

                if (ImageItemWithDescription != null)
                {
                    ImageItemWithDescription.Selected = true;
                }
            }

            this.DisplayLayouts();
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
                if (view.Name == "Main")
                {
                    this.messageBoxService.Show(
                        "'Main' View Model and Views cannot be deleted.", 
                        Settings.ApplicationName, 
                        true, 
                        Theme.Light,
                        this.settingsService.ThemeColor);
                }
                else
                {
                    this.views.Remove(view);
                }
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
