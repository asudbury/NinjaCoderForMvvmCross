// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the ISqliteConnectionService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace $rootnamespace$.Services
{
    using SQLite.Net;
    
    /// <summary>
    /// Defines the ISqliteConnectionService
    /// </summary>
    public interface ISqliteConnectionService
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        SQLiteConnection GetConnection();
    }
}