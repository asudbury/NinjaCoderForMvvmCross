// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the TodoService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace $rootnamespace$.Services
{
    using DataServices;
    using Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the TodoService type.
    /// </summary>
    public class TodoService : ITodoService
    {
        /// <summary>
        /// The todo sqlite data service.
        /// </summary>
        private readonly ITodoSqliteDataService todoSqliteDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoService" /> class.
        /// </summary>
        /// <param name="todoSqliteDataService">The todo sqlite data service.</param>
        public TodoService(ITodoSqliteDataService todoSqliteDataService)
        {
            this.todoSqliteDataService = todoSqliteDataService;
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TodoItem> GetItems()
        {
            return this.todoSqliteDataService.GetItems();
        }

        /// <summary>
        /// Gets the items not done.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TodoItem> GetItemsNotDone()
        {
            return this.todoSqliteDataService.GetItemsNotDone();
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public TodoItem GetItem(int id)
        {
            return this.todoSqliteDataService.GetItem(id);
        }

        /// <summary>
        /// Saves the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public int SaveItem(TodoItem item)
        {
            return this.todoSqliteDataService.SaveItem(item);
        }

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public int DeleteItem(int id)
        {
            return this.todoSqliteDataService.DeleteItem(id);
        }
    }
}
