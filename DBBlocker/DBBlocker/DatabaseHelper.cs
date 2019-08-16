using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace DBBlocker
{
    class DatabaseHelper
    {
        protected static SQLiteConnection sandBoxDB;

        protected static void InitDB()
        {
            sandBoxDB = new SQLiteConnection("Data Source= sandbox.db;Version=3;New=True;Compress=True");
            try
            {
                sandBoxDB.Open();
            }
            catch(Exception ex)
            {
                throw ex;       
            }
            //CreateTestTables();
        }

        public static void RunSQL(string query)
        {
            InitDB();
            SQLiteCommand toRun;
            toRun = sandBoxDB.CreateCommand();
            toRun.CommandText = query;
            toRun.ExecuteNonQuery();
        }

        public static void RunReaderSQL(string query)
        {
            InitDB();
            SQLiteCommand toRun;
            SQLiteDataReader reader;
            toRun = sandBoxDB.CreateCommand();
            toRun.CommandText = query;
            reader = toRun.ExecuteReader();
            while (reader.Read())
            {
                //the data from the reader must be displayed. Data binding is the best solution.
            }
            sandBoxDB.Close();
        }

        // This method is purely a test. It will not be used.

        static void CreateTestTables()
        {

            SQLiteCommand toRun;
            string Createsql = "CREATE TABLE Test1 (tcol1 VARCHAR(20), tcol2 INT)";
            string Createsql1 = "CREATE TABLE Test2 (tcol1 VARCHAR(20), tcol2 INT)";
            toRun = sandBoxDB.CreateCommand();
            toRun.CommandText = Createsql;
            toRun.ExecuteNonQuery();
            toRun.CommandText = Createsql1;
            toRun.ExecuteNonQuery();

        }
    }
}
