// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SqliteSampleData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MvvmCross.SqliteDataService.Entities
{
    using Cirrious.MvvmCross.Community.Plugins.Sqlite;

    /// <summary>
    ///  Defines the SqliteSampleData type.
    /// </summary>
    public class SqliteSampleData
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
