using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Controls;
using System.Windows;

namespace DBBlocker
{
    abstract class DatabaseHelper
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
            try
            {
                toRun.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Application.Current.Resources["Output"] = ex.Message;
            }
        }

            public static void RunReaderSQL(string query)
            {
                InitDB();
                SQLiteCommand toRun;
                SQLiteDataReader reader;
                toRun = sandBoxDB.CreateCommand();
                toRun.CommandText = query;
                try
                {
                    reader = toRun.ExecuteReader();
                    while (reader.Read())
                    {
                        //the data from the reader must be displayed. Data binding is the best solution.
                    }
                }
                catch(Exception ex)
                {
                    Application.Current.Resources["Output"] = ex.Message;

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
