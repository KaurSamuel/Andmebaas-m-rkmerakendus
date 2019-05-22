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

namespace Notepad
{
    class DatabaseService
    {
        SQLiteConnection db;
        public static List<Note> NotesList { get; set; }

        public DatabaseService()
        {
            CreateDatabase();
            NotesList = GetAllNotes().ToList();
        }
        public void CreateDatabase()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "db.db3");
            db = new SQLiteConnection(dbPath);
            db.CreateTable<Note>();


            db.CreateTable<Note>();
            if (db.Table<Note>().Count() == 0)
            {
                Note testNote = new Note()
                {
                    Content = "Tap to edit"
                };
                db.Insert(testNote);
            }
        }

        public void AddNote(Note note)
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "db.db3");
            SQLiteConnection db = new SQLiteConnection(dbPath);
            db.Insert(note);
        }

        public void SaveNote(Note note)
        {
            db.Update(note);
        }

        public void DeleteNote(Note note)
        {
            db.Delete(note);
        }

        public List<Note> GetAllNotes()
        {
            var table = db.Table<Note>();
            return table.ToList();
        }
    }
}