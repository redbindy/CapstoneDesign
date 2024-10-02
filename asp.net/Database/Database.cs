using System.Data.SQLite;
using System.Diagnostics;

namespace Capstone.Database
{
    public class Database : IDisposable
    {
        private static Database? sInstance = null;
        public static Database Instance
        {
            get
            {
                if (sInstance == null)
                {
                    sInstance = new Database();
                }

                return sInstance;
            }
        }

        private SQLiteConnection connection;

        private Database()
        {
            connection = new SQLiteConnection(@"Data Source=./WebDB.db");
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public SQLiteDataReader? Select(string columns, string from)
        {
            Debug.Assert(!string.IsNullOrEmpty(columns));
            Debug.Assert(!string.IsNullOrEmpty(from));

            string query = $"select {columns} from {from}";

            SQLiteCommand cmd = new SQLiteCommand(query, connection);

            return cmd.ExecuteReader();
        }
    }
}
