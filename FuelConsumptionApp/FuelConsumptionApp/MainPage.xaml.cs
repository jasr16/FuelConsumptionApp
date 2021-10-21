using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FuelConsumptionApp
{
    public partial class MainPage : ContentPage
    {
        ViewCell lastCell;
        public MainPage()
        {
            InitializeComponent();
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();

            OverallLabel.Text = Model.TankRecord.CalculateOverall().ToString("0.00") + " l/100 km";
            listView.ItemsSource = await App.Database.GetRecordsAsync();
        }


        public async void Button_add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewRecord { });
        }

        public void Button_delete_Clicked(object sender, EventArgs e)
        {
            foreach (Model.TankRecord tr in App.Database.GetRecordsAsync().Result)
            {
                App.Database.DeleteRecordAsync(tr);
            }
            OnAppearing();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            bool response = await DisplayAlert("Delete", "Do you want to delete this record?", "Yes", "No");
            if (response)
            {
                Model.TankRecord recordToBeDeleted = (Model.TankRecord)listView.SelectedItem;
                await App.Database.DeleteRecordAsync(recordToBeDeleted);
            }
            OnAppearing();
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            if (lastCell != null)
                lastCell.View.BackgroundColor = Color.Transparent;
            var viewCell = (ViewCell)sender;
            if (viewCell.View != null)
            {
                viewCell.View.BackgroundColor = Color.FromHex("E39696");
                lastCell = viewCell;
            }
        }
    }
}

