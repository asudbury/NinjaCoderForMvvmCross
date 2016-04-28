// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the DependencyServiceViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddDependencyServices
{
    using Factories.Interfaces;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.Infrastructure.Services;
    using Scorchio.Infrastructure.Wpf;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Scorchio.VisualStudio.Entities;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Input;

    /// <summary>
    ///  Defines the DependencyServiceViewModel type.
    /// </summary>
    public class DependencyServiceViewModel : BaseWizardStepViewModel
    {
        /// <summary>
        /// The message box service.
        /// </summary>
        private readonly IMessageBoxService messageBoxService;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The caching service.
        /// </summary>
        private readonly ICachingService cachingService;

        /// <summary>
        /// The dependency services factory.
        /// </summary>
        private readonly IDependencyServicesFactory dependencyServicesFactory;

        /// <summary>
        /// The name.
        /// </summary>
        private string name;

        /// <summary>
        /// The append service to name.
        /// </summary>
        private bool appendServiceToName;

        /// <summary>
        /// The method comment.
        /// </summary>
        private string methodComment;

        /// <summary>
        /// The method return type.
        /// </summary>
        private string methodReturnType;

        /// <summary>
        /// The method name.
        /// </summary>
        private string methodName;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyServiceViewModel" /> class.
        /// </summary>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="cachingService">The caching service.</param>
        /// <param name="dependencyServicesFactory">The dependency services factory.</param>
        public DependencyServiceViewModel(
            IMessageBoxService messageBoxService,
            ISettingsService settingsService,
            ICachingService cachingService,
            IDependencyServicesFactory dependencyServicesFactory)
        {
            this.messageBoxService = messageBoxService;
            this.settingsService = settingsService;
            this.cachingService = cachingService;
            this.dependencyServicesFactory = dependencyServicesFactory;
            this.Init();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get { return this.name?.CaptialiseFirstCharacter(); }
            set { this.SetProperty(ref this.name, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [append service to name].
        /// </summary>
        public bool AppendServiceToName
        {
            get { return this.appendServiceToName; }
            set { this.SetProperty(ref this.appendServiceToName, value); }
        }

        /// <summary>
        /// Gets or sets the method comment.
        /// </summary>
        public string MethodComment
        {
            get { return this.methodComment.CaptialiseFirstCharacter(); }
            set { this.SetProperty(ref this.methodComment, value); }
        }

        /// <summary>
        /// Gets or sets the type of the method return.
        /// </summary>
        public string MethodReturnType
        {
            get { return this.methodReturnType; }
            set { this.SetProperty(ref this.methodReturnType, value); }
        }

        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        public string MethodName
        {
            get { return this.methodName.CaptialiseFirstCharacter(); }
            set { this.SetProperty(ref this.methodName, value); }
        }

        /// <summary>
        /// Gets the name of the requested.
        /// </summary>
        public string RequestedName
        {
            get
            {
                string requestedName = this.name.CaptialiseFirstCharacter();

                if (this.appendServiceToName)
                {
                    return requestedName + "Service";
                }

                return requestedName;
            }
        }

        /// <summary>
        /// Gets the dependency services web site command.
        /// </summary>
        public ICommand DependencyServicesWebSiteCommand
        {
            get { return new RelayCommand(this.DisplayWebPage); }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public override string DisplayName
        {
            get { return "Dependency Service"; }
        }

        /// <summary>
        /// Determines whether this instance [can move to next page].
        /// </summary>
        /// <returns>True or false.</returns>
        public override bool CanMoveToNextPage()
        {
            if (string.IsNullOrEmpty(this.name) ||
                string.IsNullOrEmpty(this.methodReturnType) || 
                string.IsNullOrEmpty(this.methodName))
            {
                this.messageBoxService.Show(
                    "Please enter all the required fields",
                    Constants.Settings.ApplicationName);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Called when [save].
        /// </summary>
        public override void OnSave()
        {
            base.OnSave();

            this.settingsService.AutomaticallyAddServicetoDependency = this.appendServiceToName;

            IEnumerable<TextTemplateInfo> templateInfos = this.dependencyServicesFactory.GetTextTemplates(
                this.RequestedName,
                this.MethodComment,
                this.MethodReturnType,
                this.MethodName,
                this.settingsService.DependencyDirectory);

            string message = string.Empty;

            foreach (TextTemplateInfo textTemplateInfo in templateInfos)
            {
                message += textTemplateInfo.ProjectFolder + @"\" + textTemplateInfo.FileName + " will be added to the  " + textTemplateInfo.ProjectSuffix + " project.\r\n\r\n";
            }

            this.cachingService.Messages["DependencyServicesFinishMessage"] = message;
        }

        /// <summary>
        /// Displays the web page.
        /// </summary>
        internal void DisplayWebPage()
        {
            Process.Start(this.settingsService.DependencyServicesWebPage);
        }

        /// <summary>
        /// Called when [initialize].
        /// </summary>
        internal void Init()
        {
            this.AppendServiceToName = this.settingsService.AutomaticallyAddServicetoDependency;
        }
    }
}
