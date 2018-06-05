using System;
using System.IO;
using OnePiece.App.iOS.Providers;
using OnePiece.App.LocalData;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteProvider))]
namespace OnePiece.App.iOS.Providers
{
    public class SQLiteProvider : ISQLiteProvider
    {
        private string GetDatabasePath(string databaseName)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            //var libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(documentsPath, databaseName);
            return path;
        }

        /// <summary>
        /// Get SQlite connection
        /// </summary>
        /// <param name="databaseName">If database name contains "/", assume it is a full database path</param>
        /// <returns></returns>
        public SQLiteConnection GetConnection(string databaseName)
        {
            SQLiteConnection conn;
            if (databaseName.Contains("/"))
            {
                conn = new SQLiteConnection(databaseName);
            }
            else
            {
                var path = GetDatabasePath(databaseName);
                conn = new SQLiteConnection(path);
            }

            return conn;
        }
    }
}