using System;
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
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Note);
            string Note = Intent.GetStringExtra("note");
            TextView Notetext = FindViewById<TextView>(Resource.Id.Notetext);
            Notetext.Text = Note;
        }
    }
}