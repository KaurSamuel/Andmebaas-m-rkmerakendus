using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace DatabaseExample
{
    public class DatabaseService
    {
        SQLiteConnection db;

        public void CreateDatabase()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "mydatabase.db3");
            db = new SQLiteConnection(dbPath);            
        }

        public void CreateTableWithData()
        {
            db.CreateTable<Stock>();
            if (db.Table<Stock>().Count() == 0)
            {
                var newStock = new Stock();
                newStock.Symbol = "Test note";
                db.Insert(newStock);
            }
        }

        public void AddStock(string name)
        {
            var newStock = new Stock();
            newStock.Symbol = name;
            db.Insert(newStock);
        }

        public TableQuery<Stock> GetAllStocks()
        {
            var table = db.Table<Stock>();
            return table;
        }
    }
}