// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the SqliteConnectionServiceDroid type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

[assembly: Dependency (typeof (SqliteConnectionServiceDroid))]

namespace $rootnamespace$.DependencyServices
{
    using SQLite.Net;

    /// <summary>
    /// Defines the SqliteConnectionServiceDroid type.
    /// </summary>
    public class SqliteConnectionServiceDroid ISqliteConnectionService
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        public SQLiteConnection GetConnection()
        {
            string sqliteFilename = "TodoSQLite.db3";
            string documentsPath = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal); 
            string path = Path.Combine(documentsPath, sqliteFilename);

            if (!File.Exists(path))
            {
                var s = Forms.Context.Resources.OpenRawResource(Resource.Raw.TodoSQLite);  

                FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                this.ReadWriteStream(s, writeStream);
            }

            SQLiteConnection connection = new SQLite.SQLiteConnection(path);

            return connection;
        }
        
        /// <summary>
        /// helper method to get the database out of /raw/ and into the user filesystem
        /// </summary>
        private void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }
    }
}
