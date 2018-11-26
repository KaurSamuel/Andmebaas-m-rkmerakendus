using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System.IO;

namespace DatabaseExample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var stockListView = FindViewById<ListView>(Resource.Id.listView1);
            var addStockEditText = FindViewById<EditText>(Resource.Id.editText1);
            var addButton = FindViewById<Button>(Resource.Id.button1);

            var databaseService = new DatabaseService();
            databaseService.CreateDatabase();
            databaseService.CreateTableWithData();
            var stocks = databaseService.GetAllStocks();

            stockListView.Adapter = new CustomAdapter(this, stocks.ToList());

            addButton.Click += delegate
            {
                var stockName = addStockEditText.Text;
                databaseService.AddStock(stockName);

                stocks = databaseService.GetAllStocks();
                stockListView.Adapter = new CustomAdapter(this, stocks.ToList());
            };
        }
    }
}