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

        private SQLiteConnection mConnection;

        private Database()
        {
            mConnection = new SQLiteConnection(@"Data Source=./Database/WebDB.db");
            mConnection.Open();
        }

        public void Dispose()
        {
            mConnection.Close();
        }

        public bool Insert(string query)
        {
            Debug.Assert(!string.IsNullOrEmpty(query));

            SQLiteCommand cmd = new SQLiteCommand(query, mConnection);

            int result = cmd.ExecuteNonQuery();

            return result > 0;
        }

        public SQLiteDataReader? Select(string query)
        {
            Debug.Assert(!string.IsNullOrEmpty(query));

            SQLiteCommand cmd = new SQLiteCommand(query, mConnection);

            return cmd.ExecuteReader();
        }
    }
}
