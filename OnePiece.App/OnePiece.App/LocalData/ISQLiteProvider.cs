using SQLite;

namespace OnePiece.App.LocalData
{
    public interface ISQLiteProvider
    {
        /// <summary>
        /// Get SQlite connection
        /// </summary>
        /// <param name="databaseName">If database name contains "/", assume it is a full database path</param>
        /// <returns></returns>
        SQLiteConnection GetConnection(string databaseName);
    }
}
