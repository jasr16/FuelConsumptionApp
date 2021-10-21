using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FuelConsumptionApp.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FuelConsumptionApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewRecord : ContentPage
    {

        public NewRecord()
        {
            InitializeComponent();
        }

        public async void Button_calculate_Clicked(object sender, EventArgs e)
        {
            bool isVolumeEntryValid = float.TryParse(VolumeEntry.Text, out float volume);
            bool isMileageEntryValid = float.TryParse(MileageEntry.Text, out float mileage);
            if (isMileageEntryValid && isVolumeEntryValid && volume > 0 && mileage > 0)
            {
                TankRecord new_record = new Model.TankRecord()
                {
                    Volume = volume,
                    Mileage = mileage,
                    Consumption = TankRecord.ComputeConsumption(volume, mileage),
                    Date = DateTime.Now,
                };

                bool response = await DisplayAlert("Consumption computed!",
                    $"Your average fuel consumption is: \r\n {new_record.Consumption.ToString("0.00")} l/100 km \r\n \r\n Do you want to save the data?", "Yes", "No");
                if (response)
                {
                    await App.Database.SaveRecordAsync(new_record);
                    await DisplayAlert("The new record was saved and overall was recalculated",
                            $"Your average fuel consumption is: \r\n {TankRecord.CalculateOverall().ToString("0.00")} l/100 km", "OK");
                }
            }
            else
            {
                await DisplayAlert("Invalid input", "Entered data are not in correct format", "OK");
            }
        }

    }
}