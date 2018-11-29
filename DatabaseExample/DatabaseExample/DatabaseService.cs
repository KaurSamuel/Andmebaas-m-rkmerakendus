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
            
        }

        public void RemoveNote(Stock note)
        {
            db.Delete(note);
        }

        public void UpdateNote(Stock note)
        {
            //ütleb siin et db väärtuseks on null aga samas AddStock töötab
           
            db.Update(note);
        }

        public void AddStock(Stock note)
        {
            db.Insert(note);
        }

        public TableQuery<Stock> GetAllStocks()
        {
            var table = db.Table<Stock>();
            return table;
        }
    }
}