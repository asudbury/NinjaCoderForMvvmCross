// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the TaskService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace $rootnamespace$.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;
    using DataServices;

    /// <summary>
    /// Defines the TaskService type.
    /// </summary>
    public class TaskService : ITaskService
    {
        /// <summary>
        /// The task azure data service.
        /// </summary>
        private readonly ITaskAzureDataService taskAzureDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskService"/> class.
        /// </summary>
        /// <param name="taskAzureDataService">The task azure data service.</param>
        public TaskService(ITaskAzureDataService taskAzureDataService)
        {
            this.taskAzureDataService = taskAzureDataService;
        }

        /// <summary>
        /// Gets the task asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Task<TaskItem> GetTaskAsync(string id)
        {
            return this.taskAzureDataService.GetTaskAsync(id);
        }

        /// <summary>
        /// Gets the tasks asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<List<TaskItem>> GetTasksAsync()
        {
            return this.taskAzureDataService.GetTasksAsync();
        }

        /// <summary>
        /// Saves the task asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public Task SaveTaskAsync(TaskItem item)
        {
            return this.taskAzureDataService.SaveTaskAsync(item);
        }

        /// <summary>
        /// Deletes the task asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public Task DeleteTaskAsync(TaskItem item)
        {
            return this.taskAzureDataService.DeleteTaskAsync(item);
        }
    }
}
