// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the DataService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace CoreTemplates.Services
{
    using System.Collections.Generic;

    using CoreTemplates.Entities;

    /// <summary>
    /// Defines the DataService type.
    /// </summary>
    public class DataService : IDataService<Widget>
    {
        /// <summary>
        /// The connection
        /// </summary>
        private readonly ISQLiteConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataService" /> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public DataService(ISQLiteConnectionFactory factory)
        {
            //// TODO amend database as applicable.
            this.connection = factory.Create("one.sql");

            this.connection.CreateTable<Widget>();
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get
            {
                return this.connection.Table<Widget>().Count();
            }
        }

        /// <summary>
        /// Gets the specified name filter.
        /// </summary>
        /// <param name="nameFilter">The name filter.</param>
        /// <returns>A list of type T.</returns>
        public List<Widget> Get(string nameFilter)
        {
            return this.connection.Table<Widget>()
                              .Where(x => x.Name.Contains(nameFilter))
                              .ToList();
        }

        /// <summary>
        /// Inserts the specified object.
        /// </summary>
        /// <param name="t">The object.</param>
        public void Insert(Widget t)
        {
            this.connection.Insert(t);
        }

        /// <summary>
        /// Updates the specified object.
        /// </summary>
        /// <param name="t">The object.</param>
        public void Update(Widget t)
        {
            this.connection.Update(t);
        }

        /// <summary>
        /// Deletes the specified object.
        /// </summary>
        /// <param name="t">The object.</param>
        public void Delete(Widget t)
        {
            this.connection.Delete(t);
        }
    }
}
