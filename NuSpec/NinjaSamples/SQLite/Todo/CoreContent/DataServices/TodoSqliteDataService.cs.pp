// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the TodoSqliteDataService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace $rootnamespace$.DataServices
{
    using Entities;
    using System.Collections.Generic;
    using System.Linq;
    using Services;
    using SQLite.Net;

    /// <summary>
    ///  Defines the TodoDataService type.
    /// </summary>
    public class TodoSqliteDataService : ITodoSqliteDataService
    {
        /// <summary>
        /// The done select statement.
        /// </summary>
        private const string DoneSelectStatement = "SELECT * FROM [TodoItem] WHERE [Complete] = 0";

        /// <summary>
        /// The Locker.
        /// </summary>
        private static readonly object Locker = new object();

        /// <summary>
        /// The connection.
        /// </summary>
        private readonly SQLiteConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoSqliteDataService"/> class.
        /// </summary>
        /// <param name="sqliteConnectionService">The sqlite connection service.</param>
        public TodoSqliteDataService(ISqliteConnectionService sqliteConnectionService)
        {
            this.connection = sqliteConnectionService.GetConnection();
            this.connection.CreateTable<TodoItem>();
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TodoItem> GetItems()
        {
            lock (Locker)
            {
                return (from i in this.connection.Table<TodoItem>() select i).ToList();
            }
        }

        /// <summary>
        /// Gets the items not done.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TodoItem> GetItemsNotDone()
        {
            lock (Locker)
            {
                return this.connection.Query<TodoItem>(DoneSelectStatement);
            }
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public TodoItem GetItem(int id)
        {
            lock (Locker)
            {
                return this.connection.Table<TodoItem>().FirstOrDefault(x => x.Id == id);
            }
        }

        /// <summary>
        /// Saves the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public int SaveItem(TodoItem item)
        {
            lock (Locker)
            {
                if (item.Id != 0)
                {
                    this.connection.Update(item);
                    return item.Id;
                }

                return this.connection.Insert(item);
            }
        }

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public int DeleteItem(int id)
        {
            lock (Locker)
            {
                return this.connection.Delete<TodoItem>(id);
            }
        }
    }
}
