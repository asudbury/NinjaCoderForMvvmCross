// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the SqliteConnectionServiceDroid type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using $rootnamespace$.DependencyServices;

[assembly: Xamarin.Forms.Dependency (typeof (SqliteConnectionServiceDroid))]

namespace $rootnamespace$.DependencyServices
{
    using System;
    using System.IO;
    using $rootnamespace$.Core.Services;
    using SQLite.Net;
    using SQLite.Net.Platform.XamarinAndroid;

    /// <summary>
    /// Defines the SqliteConnectionServiceDroid type.
    /// </summary>
    public class SqliteConnectionServiceDroid : ISqliteConnectionService
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        public SQLiteConnection GetConnection()
        {
            string sqliteFilename = "TodoSQLite.db3";
            string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); 
            string path = Path.Combine(documentsPath, sqliteFilename);

            SQLitePlatformAndroid sqLitePlatformAndroid = new SQLitePlatformAndroid();

            SQLiteConnection connection = new SQLiteConnection(sqLitePlatformAndroid, path);

            return connection;
        }
    }
}