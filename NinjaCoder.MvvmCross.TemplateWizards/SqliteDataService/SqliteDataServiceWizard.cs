 // --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SqliteDataServiceWizard type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.TemplateWizards.SqliteDataService
{
    using System.Globalization;
    using System.Windows.Forms;

    using EnvDTE;

    using Extensions;
    using Services;

    /// <summary>
    ///  Defines the SqliteDataServiceWizard type.
    /// </summary>
    public class SqliteDataServiceWizard : BaseWizard
    {
        /// <summary>
        /// The entity name.
        /// </summary>
        private string entityName = string.Empty;

        /// <summary>
        /// The dialog result.
        /// </summary>
        private DialogResult dialogResult = DialogResult.None;

        /// <summary>
        /// Runs custom wizard logic at the beginning of a template wizard run.
        /// </summary>
        public override void OnRunStarted()
        {
            TraceService.WriteLine("SqliteDataServiceWizard::OnRunStarted");

            SqlDataServiceView view = new SqlDataServiceView(this.SettingsService.DisplayLogo);
            view.ShowDialog();

            this.dialogResult = view.DialogResult;

            if (this.dialogResult == DialogResult.OK)
            {
                this.entityName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(view.EntityName);

                this.ReplacementsDictionary.Add("$sqliteEntityName$", this.entityName);

                this.AddGlobal("MockSqliteDataService", "mock" + this.entityName + "DataService");
                this.AddGlobal("InterfaceSqlite", "I" + this.entityName + "DataService");
                this.AddGlobal("sqliteInstance", this.entityName.LowerCaseFirstCharacter() + "DataService");
                this.AddGlobal("SqliteDataService", this.entityName + "DataService");
                this.AddGlobal("SqliteData", this.entityName);
            }
        }

        /// <summary>
        /// Called when [should add project item].
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>True or false.</returns>
        public override bool OnShouldAddProjectItem(string filePath)
        {
            TraceService.WriteLine("SqliteDataServiceWizard::OnShouldAddProjectItem path=" + filePath);

            if (this.dialogResult == DialogResult.OK)
            {
                //// if the file already exists don't try and add it again!
                return !this.DoesFileExist(filePath);
            }

            //// if the user hasn't pressed the ok button then don't add items.
            return false;
        }

        /// <summary>
        /// Called when [project item finished generating].
        /// </summary>
        /// <param name="projectItem">The project item.</param>
        public override void OnProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            TraceService.WriteLine("SqliteDataServiceWizard::OnProjectItemFinishedGenerating");

            projectItem.ReplaceText("SampleDataService", this.entityName + "DataService");
        }
    }
}
