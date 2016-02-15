// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MvvmCrossViewFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using NinjaCoder.MvvmCross.Constants;
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using Scorchio.Infrastructure.Entities;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Defines the MvvmCrossViewFactory type.
    /// </summary>
    public class MvvmCrossViewFactory : BaseViewFactory, IMvvmCrossViewFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MvvmCrossViewFactory" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        public MvvmCrossViewFactory(
            ISettingsService settingsService,
            IVisualStudioService visualStudioService)
            :base(
            settingsService,
            visualStudioService)
        {
        }

        /// <summary>
        /// Gets the views.
        /// </summary>
        public ObservableCollection<ImageItemWithDescription> Views
        {
            get
            {
                ObservableCollection<ImageItemWithDescription> pages = new ObservableCollection<ImageItemWithDescription>
                {
                    new ImageItemWithDescription  
                        {
                            ImageUrl = this.GetUrlPath("ContentPage.png"),
                            Name = "Blank",
                            Description = "Blank Views",
                            Selected = true
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl = this.GetUrlPath("MasterDetailPage.png"),
                            Description = "Views with sample data",
                            Name = "SampleData"
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl = this.GetUrlPath("NavigationPage.png"),
                            Description = "Web View to host html",
                            Name = "Web"
                        }
                };

                return pages;
            }
        }
        
        /// <summary>
        /// Gets the MVVM cross view.
        /// </summary>
        /// <param name="itemTemplateInfo">The item template information.</param>
        /// <param name="tokens">The tokens.</param>
        /// <param name="viewTemplateName">Name of the view template.</param>
        /// <param name="viewName">Name of the view.</param>
        /// <returns></returns>
        public TextTemplateInfo GetMvvmCrossView(
            ItemTemplateInfo itemTemplateInfo,
            Dictionary<string, string> tokens,
            string viewTemplateName,
            string viewName)
        {
            TraceService.WriteLine("MvvmCrossViewFactory::GetMvvmCrossView viewTemplateName=" + viewTemplateName);

            TextTemplateInfo textTemplateInfo = new TextTemplateInfo
            {
                ProjectSuffix = itemTemplateInfo.ProjectSuffix,
                ProjectFolder = "Views",
                Tokens = tokens,
                ShortTemplateName = viewTemplateName,
                TemplateName = this.SettingsService.ItemTemplatesDirectory + "\\" + itemTemplateInfo.ProjectSuffix.Substring(1) + "\\" + viewTemplateName
            };

            TextTransformation textTransformation = this.GetTextTransformationService()
                .Transform(textTemplateInfo.TemplateName, textTemplateInfo.Tokens);

            textTemplateInfo.TextOutput = textTransformation.Output;
            textTemplateInfo.FileName = viewName + "." + textTransformation.FileExtension;

            if (itemTemplateInfo.ProjectSuffix == ProjectSuffix.iOS.GetDescription() ||
                itemTemplateInfo.ProjectSuffix == ProjectSuffix.Droid.GetDescription())
            {
                textTemplateInfo.FileOperations.Add(
                    this.GetCompileFileOperation(textTemplateInfo.ProjectSuffix, textTemplateInfo.FileName));
            }

            else
            {
                textTemplateInfo.FileOperations.Add(
                    this.GetPageFileOperation(textTemplateInfo.ProjectSuffix, textTemplateInfo.FileName));
            }
            
            //// now handle xib and story board options!

            if (viewTemplateName.Contains("XibSampleDataView") ||
                viewTemplateName.Contains("StoryBoardSampleDataView"))
            {
                viewTemplateName = viewTemplateName.ToLower().Contains("xib") ? "SampleDataViewXib.t4" : "SampleDataViewStoryBoard.t4";

                TraceService.WriteLine("Adding ChildTextTemplate ViewTemplateName=" + viewTemplateName);

                TextTemplateInfo childTextTemplateInfo = new TextTemplateInfo
                {
                    ProjectSuffix = itemTemplateInfo.ProjectSuffix,
                    ProjectFolder = "Views",
                    Tokens = tokens,
                    ShortTemplateName = viewTemplateName,
                    TemplateName = this.SettingsService.ItemTemplatesDirectory + "\\" + itemTemplateInfo.ProjectSuffix.Substring(1) + "\\" + viewTemplateName
                };

                textTransformation = this.GetTextTransformationService()
                    .Transform(childTextTemplateInfo.TemplateName, textTemplateInfo.Tokens);

                childTextTemplateInfo.TextOutput = textTransformation.Output;
                childTextTemplateInfo.FileName = viewName + "." + textTransformation.FileExtension;
                
                textTemplateInfo.ChildItems.Add(childTextTemplateInfo);
            }

            return textTemplateInfo;
        }

        /// <summary>
        /// Gets the MVVM cross view.
        /// </summary>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="itemTemplateInfo">The item template information.</param>
        /// <param name="view">The view.</param>
        /// <returns></returns>
        public  TextTemplateInfo GetMvvmCrossView(
            string viewModelName,
            ItemTemplateInfo itemTemplateInfo,
            View view)
        {
            TraceService.WriteLine("MvvmCrossViewFactory::GetMvvmCrossView");

            string viewName = viewModelName.Remove(viewModelName.Length - ViewModelSuffix.Length) + "View";

            string viewTemplateName = this.GetViewTemplate(
                    FrameworkType.MvvmCross.GetValueFromDescription<FrameworkType>(view.Framework),
                    itemTemplateInfo.ProjectSuffix,
                    view.PageType);

            TraceService.WriteLine("ViewTemplateName=" + viewTemplateName);

            if (itemTemplateInfo.ProjectSuffix == ProjectSuffix.iOS.GetDescription()
                && viewTemplateName == "SampleDataView.t4")
            {
                viewTemplateName = this.SettingsService.SelectedMvvmCrossiOSSampleDataViewType
                                   + viewTemplateName;
            }

            Dictionary<string, string> tokens = this.GetBaseDictionary(
                viewName,
                itemTemplateInfo.ProjectSuffix);

            TextTemplateInfo textTemplateInfo = this.GetMvvmCrossView(
                itemTemplateInfo,
                tokens,
                viewTemplateName,
                viewName);

            if (itemTemplateInfo.ProjectSuffix == ProjectSuffix.WindowsPhone.GetDescription() ||
                itemTemplateInfo.ProjectSuffix == ProjectSuffix.Wpf.GetDescription())
            {
                TextTemplateInfo childTextTemplateInfo =
                    this.GetCodeBehindTextTemplateInfo(
                        itemTemplateInfo.ProjectSuffix,
                        viewName,
                        viewTemplateName,
                        tokens);

                textTemplateInfo.ChildItems.Add(childTextTemplateInfo);
            }

            else if (itemTemplateInfo.ProjectSuffix == ProjectSuffix.Droid.GetDescription())
            {
                this.GetDroidMvvmCrossViewLayout(textTemplateInfo, tokens, viewName);
            }

            return textTemplateInfo;
        }
        /// <summary>
        /// Gets the droid MVVM cross view layout.
        /// </summary>
        /// <param name="textTemplateInfo">The text template information.</param>
        /// <param name="tokens">The tokens.</param>
        /// <param name="viewName">Name of the view.</param>
        internal void GetDroidMvvmCrossViewLayout(
            TextTemplateInfo textTemplateInfo,
            Dictionary<string, string> tokens,
            string viewName)
        {
            string viewTemplateName = textTemplateInfo.ShortTemplateName.Replace(".t4", ".axml.t4");

            TextTemplateInfo childTextTemplateInfo = new TextTemplateInfo
            {
                ProjectSuffix = ProjectSuffix.Droid.GetDescription(),
                ProjectFolder = "Resources\\Layout",
                Tokens = tokens,
                ShortTemplateName = viewTemplateName,
                TemplateName = this.SettingsService.ItemTemplatesDirectory + "\\Droid\\" + viewTemplateName
            };

            TextTransformation textTransformation = this.GetTextTransformationService().Transform(childTextTemplateInfo.TemplateName, childTextTemplateInfo.Tokens);

            childTextTemplateInfo.TextOutput = textTransformation.Output;
            childTextTemplateInfo.FileName = viewName + "." + textTransformation.FileExtension;

            textTemplateInfo.ChildItems.Add(childTextTemplateInfo);
        }

        /// <summary>
        /// Gets the URL path.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>the url of the image.</returns>
        internal string GetUrlPath(string image)
        {
            return string.Format("{0}/{1}", Settings.XamarinResourcePath, image);
        }
    }
}
