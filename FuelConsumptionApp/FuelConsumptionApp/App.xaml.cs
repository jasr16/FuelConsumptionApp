using System;
using System.IO;
using FuelConsumptionApp.Data;
using FuelConsumptionApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FuelConsumptionApp
{
    public partial class App : Application
    {
        static TankRecordDatabase database;

        // Create the database connection as a singleton.
        public static TankRecordDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new TankRecordDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "TankRecords.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
