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
    public class NotesListFragment : ListFragment
    {
        int selectedNoteId;
        bool showingTwoFragments;

        public NotesListFragment()
        {
            // Being explicit about the requirement for a default constructor.
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            DatabaseService databaseService = new DatabaseService();
            databaseService.CreateDatabase();
            List<Note> notes = databaseService.GetAllNotes();
            List<string> notesList = new List<string>();
            foreach (var note in notes)
            {
                notesList.Add(note.Content);
            }

            base.OnActivityCreated(savedInstanceState);

            ListAdapter = new ArrayAdapter<String>(Activity, Android.Resource.Layout.SimpleListItemActivated1, notesList);

            if (savedInstanceState != null)
            {
                selectedNoteId = savedInstanceState.GetInt("current_note_id", 0);
            }

            var noteContainer = Activity.FindViewById(Resource.Id.notes_container);
            showingTwoFragments = noteContainer != null &&
                                  noteContainer.Visibility == ViewStates.Visible;
            if (showingTwoFragments)
            {
                ListView.ChoiceMode = ChoiceMode.Single;
                ShowNote(selectedNoteId);
            }
        }

        public override void OnResume()
        {
            DatabaseService databaseService = new DatabaseService();
            databaseService.CreateDatabase();
            List<Note> notes = databaseService.GetAllNotes();
            List<string> notesList = new List<string>();
            foreach (var note in notes)
            {
                notesList.Add(note.Content);
            }

            base.OnResume();

            ListAdapter = new ArrayAdapter<String>(Activity, Android.Resource.Layout.SimpleListItemActivated1, notesList);

            //might help with viewing notes in landscape mode somehow
            //reasoning: selected note on the left in landscape mode is highlighted with this code 
            var noteContainer = Activity.FindViewById(Resource.Id.notes_container);
            showingTwoFragments = noteContainer != null &&
                                  noteContainer.Visibility == ViewStates.Visible;
            if (showingTwoFragments)
            {
                ListView.ChoiceMode = ChoiceMode.Single;
                ShowNote(selectedNoteId);
            }
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt("current_note_id", selectedNoteId);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            ShowNote(position);
        }

        void ShowNote(int noteId)
        {
            selectedNoteId = noteId;
            if (showingTwoFragments)
            {
                ListView.SetItemChecked(selectedNoteId, true);

                NoteFragment noteFragment = FragmentManager.FindFragmentById(Resource.Id.notes_container) as NoteFragment;

                if (noteFragment == null || noteFragment.NoteId != noteId)
                {
                    var container = Activity.FindViewById(Resource.Id.notes_container);
                    var quoteFrag = NoteFragment.NewInstance(selectedNoteId);

                    FragmentTransaction ft = FragmentManager.BeginTransaction();
                    ft.Replace(Resource.Id.notes_container, quoteFrag);
                    ft.Commit();
                }
            }
            else
            {
                var intent = new Intent(Activity, typeof(NoteActivity));
                intent.PutExtra("current_play_id", noteId);
                StartActivity(intent);
            }
        }
    }
}