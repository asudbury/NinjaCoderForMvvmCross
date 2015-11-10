// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the ITaskService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace $rootnamespace$.Services
{
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the ITaskService type.
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// Gets the task asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<TaskItem> GetTaskAsync(string id);

        /// <summary>
        /// Gets the tasks asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<TaskItem>> GetTasksAsync();

        /// <summary>
        /// Saves the task asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        Task SaveTaskAsync(TaskItem item);

        /// <summary>
        /// Deletes the task asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        Task DeleteTaskAsync(TaskItem item);
    }
}
