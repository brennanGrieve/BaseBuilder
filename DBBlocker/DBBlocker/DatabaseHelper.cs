using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Data;

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
                Application.Current.Resources["Output"] = "Command successful.";
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
                DataGrid grid = (DataGrid)LogicalTreeHelper.FindLogicalNode(Application.Current.MainWindow, "OutputView");
            try
            {
                DataTable table = new DataTable();
                reader = toRun.ExecuteReader();
                table.Load(reader);
                grid.ItemsSource = table.DefaultView;     
                Application.Current.Resources["Output"] = "";
                Application.Current.Resources["DataGridEnabled"] = true;
                Application.Current.Resources["GridVis"] = Visibility.Visible;
            }
            catch (Exception ex)
            {
                    Application.Current.Resources["Output"] = ex.Message;
                    Application.Current.Resources["DataGridEnabled"] = false;
                    Application.Current.Resources["GridVis"] = Visibility.Hidden;
            }
            sandBoxDB.Close();
            }
    }
}
