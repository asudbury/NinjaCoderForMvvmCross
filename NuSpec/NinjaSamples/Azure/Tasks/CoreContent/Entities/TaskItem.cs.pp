// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the TaskItem type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace $rootnamespace$.Entities
{
    using Newtonsoft.Json;

    /// <summary>
    /// Defines the TaskItem type.
    /// </summary>
    public class TaskItem
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [JsonProperty(PropertyName = "id")] 
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty(PropertyName = "text")] 
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        [JsonProperty(PropertyName = "notes")] 
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TaskItem"/> is complete.
        /// </summary>
        [JsonProperty(PropertyName = "complete")] 
        public bool Complete { get; set; }
    }
}
