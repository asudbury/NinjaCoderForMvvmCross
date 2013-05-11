// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConvertersPresenter.cs" company="Eon Uk Limited">
//   (C) 2013 Eon Uk Limited
// </copyright>
// <summary>
//    Defines the ConvertersPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Presenters
{
    using System.IO;

    using NinjaCoder.MvvmCross.Views.Interfaces;

    /// <summary>
    ///  Defines the ConvertersPresenter type.
    /// </summary>
    public class ConvertersPresenter
    {
        /// <summary>
        /// The view.
        /// </summary>
        private readonly IConvertersView view;

        /// <summary>
        /// The paths
        /// </summary>
        private string[] paths;
 
        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertersPresenter" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public ConvertersPresenter(IConvertersView view)
        {
            this.view = view;
        }

        /// <summary>
        /// Gets or sets the templates path.
        /// </summary>
        public string TemplatesPath { get; set; }

        /// <summary>
        /// Loads the templates.
        /// </summary>
        public void LoadTemplates()
        {
            this.paths = Directory.GetFiles(this.TemplatesPath);

            foreach (string path in this.paths)
            {
                FileInfo fileInfo = new FileInfo(path);

                string name = Path.GetFileNameWithoutExtension(fileInfo.Name);

                this.view.AddTemplate(name);
            }
        }

        /// <summary>
        /// Gets the name of the path from file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The template.</returns>
        public string GetPathFromFileName(string fileName)
        {
            foreach (string path in this.paths)
            {
                FileInfo fileInfo = new FileInfo(path);

                if (Path.GetFileNameWithoutExtension(fileInfo.Name) == 
                    Path.GetFileNameWithoutExtension(fileName))
                {
                    return path;
                }
            }

            return string.Empty;
        }
    }
}
