// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the SqliteConnectionServiceiOS type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

[assembly: Dependency (typeof (SqliteConnectionServiceiOS))]

namespace $rootnamespace$.DependencyServices
{
    using SQLite.Net;

    /// <summary>
    /// Defines the SqliteConnectionServiceiOS type.
    /// </summary>
    public class SqliteConnectionServiceiOS ISqliteConnectionService
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        public SQLiteConnection GetConnection()
        {
            string sqliteFilename = "TodoSQLite.db3";
            string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); 
            string libraryPath = Path.Combine (documentsPath, "..", "Library"); 
            string path = Path.Combine(libraryPath, sqliteFilename);

            if (!File.Exists (path)) 
            {
                File.Copy (sqliteFilename, path);
            }

            SQLiteConnection connection = new SQLite.SQLiteConnection(path);

            return connection;
        }
    }
}
