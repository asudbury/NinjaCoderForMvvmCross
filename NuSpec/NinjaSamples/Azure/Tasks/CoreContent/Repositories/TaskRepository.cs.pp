// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the TaskRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace $rootnamespace$.Repositories
{
    using Entities;

    /// <summary>
    /// Defines the TaskRepository type.
    /// </summary>
    public class TaskRepository : ITaskRepository
    {
        /// <summary>
        /// Gets or sets the selected task.
        /// </summary>
        public TaskItem SelectedTask { get; set; }
    }
}