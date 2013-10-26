// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseSqliteDataService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MvvmCross.SqliteDataService.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Cirrious.MvvmCross.Community.Plugins.Sqlite;

    /// <summary>
    /// Defines the BaseSqliteDataService type.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    public abstract class BaseSqliteDataService<T> : IBaseSqliteDataService<T> 
        where T : new()
    {
        /// <summary>
        /// The database name.
        /// </summary>
        private const string DatabaseName = "my.db";

        /// <summary>
        /// The connection
        /// </summary>
        private readonly ISQLiteConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSqliteDataService{T}"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        protected BaseSqliteDataService(ISQLiteConnectionFactory factory)
        {
            //// Note:- This will actually open the database if it already
            //// exists - the method name should really be CreateOpen or something!
            this.connection = factory.Create(DatabaseName);

            //// Note:- This will actually open the table if it already
            //// exists - the method name should really be CreateOpen or something!
            this.connection.CreateTable<T>();
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get { return this.connection.Table<T>().Count(); }
        }

        /// <summary>
        /// Gets all the items.
        /// </summary>
        /// <returns>All the items.</returns>
        public List<T> GetAll()
        {
            return this.connection.Table<T>().ToList();
        }

        /// <summary>
        /// Inserts the specified t.
        /// </summary>
        /// <param name="t">The t.</param>
        public void Insert(T t)
        {
            this.connection.Insert(t);
        }

        /// <summary>
        /// Updates the specified t.
        /// </summary>
        /// <param name="t">The t.</param>
        public void Update(T t)
        {
            this.connection.Update(t);
        }

        /// <summary>
        /// Deletes the specified t.
        /// </summary>
        /// <param name="t">The t.</param>
        public void Delete(T t)
        {
            this.connection.Delete(t);
        }
    }
}
