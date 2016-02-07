// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CustomRendererViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddCustomRenderers
{
    using MahApps.Metro;
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.Infrastructure.Services;
    using Scorchio.Infrastructure.Translators;
    using Scorchio.Infrastructure.Wpf;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Input;

    /// <summary>
    ///  Defines the CustomRendererViewModel type.
    /// </summary>
    public class CustomRendererViewModel : BaseWizardStepViewModel
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
        /// The translator.
        /// </summary>
        private readonly ITranslator<string, CustomRenderers> translator;

        /// <summary>
        /// The caching service.
        /// </summary>
        private readonly ICachingService cachingService;

        /// <summary>
        /// The custom renderer factory.
        /// </summary>
        private readonly ICustomRendererFactory customRendererFactory;

        /// <summary>
        /// The name.
        /// </summary>
        private string name;

        /// <summary>
        /// The directory.
        /// </summary>
        private string directory;

        /// <summary>
        /// The append renderer to name.
        /// </summary>
        private bool appendRendererToName;

        /// <summary>
        /// The custom renderers.
        /// </summary>
        private CustomRenderers customRenderers;

        /// <summary>
        /// The custom renderer group.
        /// </summary>
        private CustomerRendererGroup customRendererGroup;

        /// <summary>
        /// The custom renderer groups.
        /// </summary>
        private List<string> customRendererGroups;

        /// <summary>
        /// The selected custom renderer group.
        /// </summary>
        private string selectedCustomRendererGroup;

        /// <summary>
        /// The custom renderer items.
        /// </summary>
        private List<string> customRendererItems;

        /// <summary>
        /// The selected custom renderer item.
        /// </summary>
        private string selectedCustomRendererItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRendererViewModel" /> class.
        /// </summary>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="translator">The translator.</param>
        /// <param name="cachingService">The caching service.</param>
        /// <param name="customRendererFactory">The custom renderer factory.</param>
        public CustomRendererViewModel(
            IMessageBoxService messageBoxService,
            ISettingsService settingsService,
            ITranslator<string, CustomRenderers> translator,
            ICachingService cachingService,
            ICustomRendererFactory customRendererFactory
            )
        {
            this.messageBoxService = messageBoxService;
            this.settingsService = settingsService;
            this.translator = translator;
            this.cachingService = cachingService;
            this.customRendererFactory = customRendererFactory;
            this.Init();
        }

        /// <summary>
        /// Called when [initialize].
        /// </summary>
        internal void Init()
        {
            this.AppendRendererToName = this.settingsService.AutomaticallyAddRenderer;
            this.Directory = this.settingsService.CustomRendererDirectory;
            this.customRenderers = this.translator.Translate(this.settingsService.XamarinFormsCustomRenderersUri);
            this.CustomRendererGroups = this.customRenderers.Groups.Select(customerRendererGroup => customerRendererGroup.Name).ToList();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get
            {
                if (this.name != null)
                {
                    return this.name.CaptialiseFirstCharacter();
                }

                return null;
            }
            set
            {
                this.SetProperty(ref this.name, value);
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether [append renderer to name].
        /// </summary>
        public bool AppendRendererToName
        {
            get { return this.appendRendererToName; }
            set { this.SetProperty(ref this.appendRendererToName, value); }
        }

        /// <summary>
        /// Gets or sets the directory.
        /// </summary>
        public string Directory
        {
            get { return this.directory; }
            set { this.SetProperty(ref this.directory, value); }
        }

        /// <summary>
        /// Gets or sets the custom renderer groups.
        /// </summary>
        public List<string> CustomRendererGroups
        {
            get { return this.customRendererGroups; }
            set { this.SetProperty(ref this.customRendererGroups, value); }
        }

        /// <summary>
        /// Gets or sets the selected custom renderer group.
        /// </summary>
        public string SelectedCustomRendererGroup
        {
            get
            {
                return this.selectedCustomRendererGroup;
            }

            set
            {
                this.SetProperty(ref this.selectedCustomRendererGroup, value);

                this.customRendererGroup = this.customRenderers.Groups.FirstOrDefault(x => x.Name == this.selectedCustomRendererGroup);

                if (this.customRendererGroup != null)
                {
                    this.CustomRendererItems = this.customRendererGroup.Renderers.OrderBy(y => y.Name).Select(x => x.Name).ToList();
                }
            }
        }

        /// <summary>
        /// Gets or sets the custom renderer items.
        /// </summary>
        public List<string> CustomRendererItems
        {
            get { return this.customRendererItems; }
            set { this.SetProperty(ref this.customRendererItems, value); }
        }

        /// <summary>
        /// Gets or sets the selected custom renderer item.
        /// </summary>
        public string SelectedCustomRendererItem
        {
            get { return this.selectedCustomRendererItem; }
            set { this.SetProperty(ref this.selectedCustomRendererItem, value); }
        }

        /// <summary>
        /// Gets the code block.
        /// </summary>
        public string CodeBlock
        {
            get
            {
                CustomerRenderer customerRenderer = this.customRendererGroup.Renderers.FirstOrDefault(x => x.Name == this.SelectedCustomRendererItem);

                string codeBlock = this.customRendererGroup.CodeBlock;

                if (customerRenderer != null)
                {
                    if (string.IsNullOrEmpty(customerRenderer.CodeBlock) == false)
                    {
                        codeBlock = customerRenderer.CodeBlock;
                    }
                }

                if (codeBlock.StartsWith("\n"))
                {
                    codeBlock = codeBlock.Substring(1);
                }

                return codeBlock;
            }
        }

        /// <summary>
        /// Gets the custom renderers web site command.
        /// </summary>
        public ICommand CustomRenderersWebSiteCommand
        {
            get { return new RelayCommand(this.DisplayWebPage); }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public override string DisplayName
        {
            get { return "Custom Renderer"; }
        }

        /// <summary>
        /// Displays the web page.
        /// </summary>
        internal void DisplayWebPage()
        {
            Process.Start(this.customRenderers.HelpLink);
        }

        /// <summary>
        /// Gets the name of the requested.
        /// </summary>
        public string RequestedName
        {
            get
            {
                string requestedName = this.name.CaptialiseFirstCharacter();

                if (this.appendRendererToName)
                {
                    return requestedName + "Renderer";
                }

                return requestedName;
            }
        }

        /// <summary>
        /// Determines whether this instance [can move to next page].
        /// </summary>
        /// <returns></returns>
        public override bool CanMoveToNextPage()
        {
            if (string.IsNullOrEmpty(this.name) ||
                string.IsNullOrEmpty(this.selectedCustomRendererItem))
            {
                this.messageBoxService.Show(
                    "Please enter all the required fields",
                    Constants.Settings.ApplicationName,
                    this.settingsService.BetaTesting,
                    Theme.Light,
                    this.settingsService.ThemeColor);

                return false;
            }

            this.settingsService.AutomaticallyAddRenderer = this.appendRendererToName;
            this.settingsService.CustomRendererDirectory = this.directory;

            IEnumerable<TextTemplateInfo> templateInfos = this.customRendererFactory.GetTextTemplates(
                    this.RequestedName,
                    this.directory,
                    this.selectedCustomRendererItem,
                    string.Empty);

            string message = string.Empty;

            foreach (TextTemplateInfo textTemplateInfo in templateInfos)
            {
                message += textTemplateInfo.ProjectFolder + @"\" + textTemplateInfo.FileName + " will be added to the  " + textTemplateInfo.ProjectSuffix + " project.\r\n\r\n";
            }

            this.cachingService.Messages["CustomRendererFinishMessage"] = message;
            return true;
        }
    }
}
