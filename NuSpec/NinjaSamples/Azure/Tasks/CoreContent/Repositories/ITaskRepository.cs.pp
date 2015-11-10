// --------------------------------------------------------------------------------------------------------------------
//  Defines the ITaskRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace $rootnamespace$.Repositories
{
    using Entities;

    /// <summary>
    /// Defines the ITaskRepository type.
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Gets or sets the selected task.
        /// </summary>
        TaskItem SelectedTask { get; set; }
    }
}