// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the TodoItem type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace $rootnamespace$.Entities
{
    using SQLite.Net.Attributes;

    /// <summary>
    /// Defines the TodoItem type.
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TodoItem"/> is complete.
        /// </summary>
        public bool Complete { get; set; }
    }
}
