using System.IO;
using OnePiece.App.Droid.Providers;
using OnePiece.App.LocalData;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteProvider))]
namespace OnePiece.App.Droid.Providers
{
    public class SQLiteProvider : ISQLiteProvider
    {
        private string GetDatabasePath(string databaseName)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
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