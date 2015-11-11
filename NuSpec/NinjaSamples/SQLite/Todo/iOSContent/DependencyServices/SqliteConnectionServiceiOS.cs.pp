// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the SqliteConnectionServiceiOS type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using $rootnamespace$.DependencyServices;

[assembly: Xamarin.Forms.Dependency (typeof (SqliteConnectionServiceiOS))]

namespace $rootnamespace$.DependencyServices
{
    using System;
    using System.IO;
    using $rootnamespace$.Core.Services;
    using SQLite.Net;
    using SQLite.Net.Platform.XamarinIOS;

    /// <summary>
    /// Defines the SqliteConnectionServiceiOS type.
    /// </summary>
    public class SqliteConnectionServiceiOS : ISqliteConnectionService
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

            SQLitePlatformIOS sqLitePlatformIos = new SQLitePlatformIOS();

            SQLiteConnection connection = new SQLiteConnection(sqLitePlatformIos, path);

            return connection;
        }
    }
}