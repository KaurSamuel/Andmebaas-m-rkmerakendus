using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Notepad
{
    public class NoteFragment : Fragment
    {
        DatabaseService DatabaseService=new DatabaseService();
        public int NoteId => Arguments.GetInt("current_note_id", 0);
        public static EditText editText { get; set; }
        public static int StatNoteId { get; set; }

        public static NoteFragment NewInstance(int noteId)
        {
            var bundle = new Bundle();
            bundle.PutInt("current_note_id", noteId);
            return new NoteFragment { Arguments = bundle };
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (container == null)
            {
                return null;
            }

            var notes = DatabaseService.GetAllNotes();

            List<string> notesList = DatabaseService.NotesList.Select(x => x.Content).ToList();

            var editText2 = Activity.FindViewById<EditText>(Resource.Id.textInputEditText1);
            editText = editText2;
            try
            {
                editText.Text = notesList[NoteId];
            }
            catch (Exception)
            {
                editText.Text = notesList[0];
            }

            return null;
        }
    }
}