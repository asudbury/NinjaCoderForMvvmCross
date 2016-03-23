﻿// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseTextTemplateFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using Entities;
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
        /// Gets the name of the class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="projectSuffix">The project suffix.</param>
        /// <param name="useProjectSuffix">if set to <c>true</c> [use project suffix].</param>
        /// <returns></returns>
        internal string GetClassName(
            string name,
            ProjectSuffix projectSuffix,
            bool useProjectSuffix)
        {
            if (useProjectSuffix)
            {
                return name + this.GetProjectType(projectSuffix);
            }

            return name;
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="projectSuffix">The project suffix.</param>
        /// <param name="useProjectSuffix">if set to <c>true</c> [use project suffix].</param>
        /// <returns></returns>
        internal string GetFileName(
            string name,
            ProjectSuffix projectSuffix,
            bool useProjectSuffix)
        {
            if (useProjectSuffix == false)
            {
                return name + ".cs";
            }

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
        /// <param name="platForm">The plat form.</param>
        /// <returns></returns>
        internal Dictionary<string, string> GetDictionary(
            Dictionary<string, string> baseDictionary,
            string nameSpace,
            string className,
            string platForm)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>(baseDictionary);

            dictionary["NameSpace"] = nameSpace;
            dictionary["ClassName"] = className;
            dictionary["Platform"] = platForm;

            return dictionary;
        }

        /// <summary>
        /// Gets the text template information.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="name">The name.</param>
        /// <param name="directory">The directory.</param>
        /// <param name="projectSuffix">The project suffix.</param>
        /// <param name="baseDictionary">The base dictionary.</param>
        /// <param name="useProjectSuffix">if set to <c>true</c> [use project suffix].</param>
        /// <param name="projectSuffixOverride">The project suffix override.</param>
        /// <returns>A TextTemplateInfo.</returns>
        protected TextTemplateInfo GetTextTemplateInfo(
            IProjectService projectService,
            string templateName,
            string name,
            string directory,
            ProjectSuffix projectSuffix,
            Dictionary<string, string> baseDictionary,
            bool useProjectSuffix,
            string projectSuffixOverride = "")
        {
            string projectSuffixString = projectSuffix.GetDescription();

            if (projectSuffixOverride != string.Empty)
            {
                projectSuffixString = projectSuffixOverride;
            }

            string nameSpace = this.GetNameSpace(projectService, directory);
            string className = this.GetClassName(name, projectSuffix, useProjectSuffix);
            string fileName = this.GetFileName(name, projectSuffix, useProjectSuffix);

            return new TextTemplateInfo
                       {
                           TemplateName = templateName,
                           ProjectFolder = directory,
                           ProjectSuffix = projectSuffix.GetDescription(),
                           FileName = fileName,
                           Tokens = this.GetDictionary(baseDictionary, nameSpace, className, projectSuffixString)
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
    }
}
