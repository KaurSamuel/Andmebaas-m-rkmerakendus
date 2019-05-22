using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using Android.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;

namespace Notepad
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme", Icon ="@drawable/app_icon")]
    public class MainActivity : Activity
    {
        DatabaseService db=new DatabaseService();
        EditText editText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.main_activity);
            //Todo: this
            //AppCenter.Start("3866317d-4cee-4f6e-ad03-a5e364eb47d9", typeof(Analytics), typeof(Crashes),typeof(Distribute));
            editText = FindViewById<EditText>(Resource.Id.textInputEditText1);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar1);
            SetActionBar(toolbar);
            ActionBar.Title = "Notes App";
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.note_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Note note = new Note();
            DatabaseService database = new DatabaseService();
            switch (item.TitleFormatted.ToString())
            {
                case "addNote":
                    Note @new = new Note();
                    @new.Content = "Tap to edit";
                    database.AddNote(@new);
                    Toast.MakeText(Application.Context, "New note created!", ToastLength.Short).Show();
                    break;

                case "saveNote":
                    if(NoteFragment.editText==null)
                    {
                        Toast.MakeText(Application.Context, "You must have a note selected", ToastLength.Short).Show();
                        break;
                    }
                    else if(NoteFragment.editText!=null)
                    {
                        note.Id = DatabaseService.NotesList[NoteFragment.StatNoteId].Id;
                        note.Content = NoteFragment.editText.Text;
                        database.SaveNote(note);
                        Toast.MakeText(Application.Context, "Note saved", ToastLength.Short).Show();
                        break;
                    }
                    break;
                    
                case "deleteNote":
                    if (NoteFragment.editText == null)
                    {
                        Toast.MakeText(Application.Context, "You must have a note selected", ToastLength.Short).Show();
                        break;
                    }
                    else if (NoteFragment.editText != null)
                    {
                        database.DeleteNote(note);
                        Toast.MakeText(Application.Context, "Note deleted", ToastLength.Short).Show();
                        break;
                    }
                    break;
            }
            this.Recreate();
            return base.OnOptionsItemSelected(item);
        }
    }
}