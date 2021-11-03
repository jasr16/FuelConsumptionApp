using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FuelConsumptionApp.Model;
using FuelConsumptionApp.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FuelConsumptionApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Record : ContentPage
    {
        public TankRecord TankRecord{get; set; }
        public Record(TankRecord tr)
        {
            InitializeComponent();
            TankRecord = tr;
            ConsumptionEntry.Text = tr.Consumption.ToString("0.00");
            ConsumptionEntry.IsEnabled = false;
            VolumeEntry.Text = tr.Volume.ToString();
            VolumeEntry.IsEnabled = false;
            MileageEntry.Text = tr.Mileage.ToString();
            MileageEntry.IsEnabled = false;
            DateEntry.Text = tr.Date.ToString();
            DateEntry.IsEnabled = false;
        }

        private void EditItem_Clicked(object sender, EventArgs e)
        {
            VolumeEntry.IsEnabled = true;
            MileageEntry.IsEnabled = true;
            DateEntry.IsEnabled = true;
        }

        private async void SaveItem_Clicked(object sender, EventArgs e)
        {
            bool isVolumeEntryValid = float.TryParse(VolumeEntry.Text, out float volume);
            bool isMileageEntryValid = float.TryParse(MileageEntry.Text, out float mileage);
            if (isMileageEntryValid && isVolumeEntryValid && volume > 0 && mileage > 0)
            {
                TankRecord.Volume = volume;
                TankRecord.Mileage = mileage;
                TankRecord.Consumption = TankRecord.ComputeConsumption(volume, mileage);
                try
                {
                    TankRecord.Date = Convert.ToDateTime(DateEntry.Text);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Invalid date or time!", ex.Message, "OK");
                    return;
                }

                bool response = await DisplayAlert("Consumption computed!",
                    $"Your average fuel consumption is: \r\n {TankRecord.Consumption.ToString("0.00")} l/100 km \r\n \r\n Do you want to save the data?", "Yes", "No");
                if (response)
                {
                    await App.Database.SaveRecordAsync(TankRecord);
                    await DisplayAlert("The new record was saved and overall was recalculated",
                            $"Your average fuel consumption is: \r\n {TankRecord.CalculateOverall().ToString("0.00")} l/100 km", "OK");
                }
            }
            else
            {
                await DisplayAlert("Invalid input", "Entered data are not in correct format", "OK");
            }
        }

        private void DeleteItem_Clicked(object sender, EventArgs e)
        {
            App.Database.DeleteRecordAsync(TankRecord);
        }
    }
}