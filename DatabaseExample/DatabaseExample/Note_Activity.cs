﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DatabaseExample
{
    [Activity(Label = "Note_Activity")]
    public class Note_Activity : Activity
    {
        DatabaseService databaseService = new DatabaseService();
        Stock note = new Stock();
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Note);
            string Note = Intent.GetStringExtra("symbol");
            TextView Notetext = FindViewById<TextView>(Resource.Id.Notetext);
            Notetext.Text = Note;
            var save_note = FindViewById<Button>(Resource.Id.save_note);
            var delete_note = FindViewById<Button>(Resource.Id.delete_note);

            save_note.Click += save_Click;
            delete_note.Click += Delete_Click;

        }

        private void save_Click(object sender, EventArgs e)
        {

            //Mingi asja pärast UptateNote ja DeleteNote ei tööta 
            //äkki saate aru mis siin valesti on

            var note_text = FindViewById<TextView>(Resource.Id.Notetext);
            //int debug = int.Parse(Intent.GetStringExtra("id"));
            note.Id = int.Parse(Intent.GetStringExtra("id"));
            note.Symbol = note_text.Text;
            databaseService.UpdateNote(note);

        }

        private void Delete_Click(object sender, EventArgs e)
        {

            
            var note_text = FindViewById<TextView>(Resource.Id.Notetext);
            note.Id = int.Parse(Intent.GetStringExtra("id"));
            note.Symbol = Intent.GetStringExtra("symbol");
            databaseService.RemoveNote(note);
            Finish();
        }
    }
}