 // --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the XamarinFormsViewsWizard type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.TemplateWizards.ViewModelsAndViews
{
    using EnvDTE;
    using NinjaCoder.MvvmCross.TemplateWizards.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    /// <summary>
    ///  Defines the ViewModelsAndViewsWizard type.
    /// </summary>
    public class XamarinFormsViewsWizard : BaseWizard
    {
        /// <summary>
        /// The project items.
        /// </summary>
        private List<ProjectItem> projectItems;
 
        /// <summary>
        /// Runs custom wizard logic at the beginning of a template wizard run.
        /// </summary>
        protected override void OnRunStarted()
        {
            TraceService.WriteHeader("XamarinFormsViewsWizard::OnRunStarted");

            this.projectItems = new List<ProjectItem>();
        }

        /// <summary>
        /// Called when [should add project item].
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>True or false.</returns>
        protected override bool OnShouldAddProjectItem(string filePath)
        {
            TraceService.WriteLine("XamarinFormsViewsWizard::OnShouldAddProjectItem path=" + filePath);

            return true;
        }

        /// <summary>
        /// Called when [project item finished generating].
        /// </summary>
        /// <param name="projectItem">The project item.</param>
        protected override void OnProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            TraceService.WriteLine("XamarinFormsViewsWizard::OnProjectItemFinishedGenerating projectItem=" + projectItem.Name);

            this.projectItems.Add(projectItem);

            this.UpdateFile(projectItem);
        }

        /// <summary>
        /// Called when [run finished].
        /// </summary>
        protected override void OnRunFinished()
        {
            TraceService.WriteLine("XamarinFormsViewsWizard::OnRunFinished");
        }

        /// <summary>
        /// Updates the file.
        /// </summary>
        /// <param name="projectItem">The project item.</param>
        internal void UpdateFile(ProjectItem projectItem)
        {
            TraceService.WriteLine("XamarinFormsViewsWizard::UpdateFile");

            string xamarinFormsViews = this.SettingsService.XamarinFormsViews;

            if (string.IsNullOrEmpty(xamarinFormsViews) == false)
            {
                TraceService.WriteLine(xamarinFormsViews);

                using (StringReader stringReader = new StringReader(xamarinFormsViews))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<View>));

                    List<View> views = (List<View>)serializer.Deserialize(stringReader);

                    if (views != null)
                    {
                        foreach (View view in views
                            .Where(view => view.Name == projectItem.Name.Replace("View.xaml", string.Empty)))
                        {
                            string pageType = view.PageType.Replace(" ", string.Empty);

                            projectItem.ReplaceText("ContentPage", pageType);
                            projectItem.ReplaceText("AbsoluteLayout", view.LayoutType.Replace(" ", string.Empty));

                            projectItem.ReplaceText("Forms.Core", "Core");

                            if (this.SettingsService.BindContextInXamlForXamarinForms)
                            {
                                TraceService.WriteLine("XamarinFormsViewsWizard::UpdateFile Update Xaml");

                                string text = string.Format("<{0}.BindingContext>{1}\t\t<viewModels:{2} />{1}\t</{0}.BindingContext>", 
                                    pageType,
                                    Environment.NewLine,
                                    view.Name + "ViewModel");

                                projectItem.ReplaceText("<!-- BindingContextPlaceHolder -->", text);
                                projectItem.ReplaceText("<!-- BindingPlaceHolder -->",  "<Label Text='{Binding SampleText}' VerticalOptions='Center' HorizontalOptions='Center'/>");
                            }

                            else
                            {
                                TraceService.WriteLine("XamarinFormsViewsWizard::UpdateFile Remove placeholder");

                                projectItem.ReplaceText("<!-- BindingContextPlaceHolder -->", string.Empty);
                                projectItem.ReplaceText("<!-- BindingPlaceHolder -->", string.Empty);
                            }
                        }
                    }
                }
            }
        }
    }
}