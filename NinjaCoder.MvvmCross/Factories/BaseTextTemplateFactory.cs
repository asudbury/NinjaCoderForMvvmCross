// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseTextTemplateFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using NinjaCoder.MvvmCross.Entities;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services.Interfaces;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the BaseTextTemplateFactory type.
    /// </summary>
    public abstract class BaseTextTemplateFactory
    {
        /// <summary>
        /// Gets the text template information.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="name">The name.</param>
        /// <param name="directory">The directory.</param>
        /// <param name="projectSuffix">The project suffix.</param>
        /// <param name="baseDictionary">The base dictionary.</param>
        /// <returns></returns>
        protected TextTemplateInfo GetTextTemplateInfo(
            IProjectService projectService,
            string templateName,
            string name,
            string directory,
            ProjectSuffix projectSuffix,
            Dictionary<string, string> baseDictionary)
        {
            string nameSpace = this.GetNameSpace(projectService, directory);
            string className = this.GetClassName(name, projectSuffix);
            string fileName = this.GetFileName(name, projectSuffix);

            return new TextTemplateInfo
                       {
                           TemplateName = templateName,
                           ProjectFolder = directory,
                           ProjectSuffix = projectSuffix.GetDescription(),
                           FileName = fileName,
                           Tokens = this.GetDictionary(baseDictionary, nameSpace, className)
                       };
        }

        /// <summary>
        /// Gets the type of the project.
        /// </summary>
        /// <param name="projectSuffix">The project suffix.</param>
        /// <returns></returns>
        protected string GetProjectType(ProjectSuffix projectSuffix)
        {
            return projectSuffix.GetDescription().Replace(".", string.Empty);
        }

        /// <summary>
        /// Gets the name space.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="directory">The directory.</param>
        /// <returns></returns>
        protected string GetNameSpace(
            IProjectService projectService, 
            string directory)
        {
            return projectService.Name + "." + directory;
        }

        /// <summary>
        /// Gets the name of the class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="projectSuffix">The project suffix.</param>
        /// <returns></returns>
        protected string GetClassName(
            string name,
            ProjectSuffix projectSuffix)
        {
            return name + this.GetProjectType(projectSuffix);
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="projectSuffix">The project suffix.</param>
        /// <returns></returns>
        protected string GetFileName(
            string name, 
            ProjectSuffix projectSuffix)
        {
            if (name.StartsWith("I") || 
                projectSuffix == ProjectSuffix.XamarinForms)
            {
                return name + ".cs";
            }

            return name + this.GetProjectType(projectSuffix) + ".cs";
        }

        /// <summary>
        /// Gets the dictionary.
        /// </summary>
        /// <param name="baseDictionary">The base dictionary.</param>
        /// <param name="nameSpace">The name space.</param>
        /// <param name="className">Name of the class.</param>
        /// <returns></returns>
        internal Dictionary<string, string> GetDictionary(
            Dictionary<string, string> baseDictionary,
            string nameSpace,
            string className)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>(baseDictionary);

            dictionary["NameSpace"] = nameSpace;
            dictionary["ClassName"] = className;

            return dictionary;
        }
    }
}
