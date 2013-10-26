// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SampleDataService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MvvmCross.SqliteDataService.Services
{
    using Cirrious.MvvmCross.Community.Plugins.Sqlite;

    using Entities;

    /// <summary>
    /// Defines the SampleDataService type.
    /// </summary>
    public class SampleDataService : BaseSqliteDataService<SqliteSampleData>, ISampleDataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleDataService"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public SampleDataService(ISQLiteConnectionFactory factory)
            : base(factory)
        {
        }
    }
}
