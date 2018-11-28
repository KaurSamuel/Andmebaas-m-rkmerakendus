using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System.Collections.Generic;
using System.IO;

namespace DatabaseExample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public List<string> AllNotes = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var NotesListView = FindViewById<ListView>(Resource.Id.listView1);
            var addButton = FindViewById<Button>(Resource.Id.Add_Note);

            var databaseService = new DatabaseService();
            databaseService.CreateDatabase();
            databaseService.CreateTableWithData();
            var stocks = databaseService.GetAllStocks();
            

            NotesListView.ItemClick += NotesListView_Click;

            NotesListView.Adapter = new CustomAdapter(this, stocks.ToList());

            addButton.Click += delegate
            {
                //databaseService.AddStock("New Note");
                Stock note = new Stock();
                note.Symbol = "new note";
                databaseService.AddStock(note);
                stocks = databaseService.GetAllStocks();
                NotesListView.Adapter = new CustomAdapter(this, stocks.ToList());
            };
        }

        private void NotesListView_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
            
         //    var commentActivity = new Intent(this, typeof(Comments));
         //    Value.Comments = new List<Comment>();
         //    Value.Comments.Add(All_Post[e.Position].Comment);

         //    StartActivity(commentActivity);
            var NoteActivity = new Intent(this, typeof(Note_Activity));
            var databaseService = new DatabaseService();
            databaseService.CreateDatabase();
            var AllNotes = databaseService.GetAllStocks().ToList();
            NoteActivity.PutExtra("symbol", AllNotes[e.Position].Symbol.ToString());
            NoteActivity.PutExtra("id", AllNotes[e.Position].Id.ToString());
            StartActivity(NoteActivity);
        }

    }
}