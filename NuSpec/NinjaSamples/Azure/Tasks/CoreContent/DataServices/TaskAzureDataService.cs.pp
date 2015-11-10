// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the TaskAzureDataService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace $rootnamespace$.DataServices
{
    using Entities;
    using Microsoft.WindowsAzure.MobileServices;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the TaskAzureDataService type.
    /// </summary>
    public class TaskAzureDataService : ITaskAzureDataService
    {
        /// <summary>
        /// The task table.
        /// </summary>
        private readonly IMobileServiceTable<TaskItem> taskTable;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskAzureDataService" /> class.
        /// </summary>
        /// <param name="taskTable">The task table.</param>
        public TaskAzureDataService(IMobileServiceTable<TaskItem> taskTable)
        {        
            this.taskTable = taskTable; 
        }

        /// <summary>
        /// Gets the task asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<TaskItem> GetTaskAsync(string id)
        {
            return await this.taskTable.LookupAsync(id);
        }

        /// <summary>
        /// Gets the tasks asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<TaskItem>> GetTasksAsync()
        {
            return new List<TaskItem>(await this.taskTable.ReadAsync());
        }

        /// <summary>
        /// Saves the task asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public async Task SaveTaskAsync(TaskItem item)
        {
            if (item.Id == null)
            {
                await this.taskTable.InsertAsync(item);
            }

            else
            {
                await this.taskTable.UpdateAsync(item);
            }
        }

        /// <summary>
        /// Deletes the task asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public async Task DeleteTaskAsync(TaskItem item)
        {
            await this.taskTable.DeleteAsync(item);
        }
    }
}