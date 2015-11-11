// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the SqliteConnectionServiceWindowsPhone type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using $rootnamespace$.DependencyServices;

[assembly: Xamarin.Forms.Dependency (typeof (SqliteConnectionServiceWindowsPhone))]

namespace  $rootnamespace$.DependencyServices
{
    using System.IO;
    using Windows.Storage;
    using $rootnamespace$.Core.Services;
    using SQLite.Net;

    /// <summary>
    /// Defines the SqliteConnectionServiceWindowsPhone type.
    /// </summary>
    public class SqliteConnectionServiceWindowsPhone //: ISqliteConnectionService
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        /*public SQLiteConnection GetConnection()
        {
            string sqliteFilename = "TodoSQLite.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);

            SQLiteConnection connection = new SQLite.SQLiteConnection(path);

            return connection;
        }*/
    }
}