using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FuelConsumptionApp.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FuelConsumptionApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Record : ContentPage
    {
        public Record(TankRecord tr)
        {
            InitializeComponent();
            ConsumptionEntry.Text = tr.Consumption.ToString("0.00");
            ConsumptionEntry.IsEnabled = false;
            VolumeEntry.Text = tr.Volume.ToString();
            VolumeEntry.IsEnabled = false;
            MileageEntry.Text = tr.Mileage.ToString();
            MileageEntry.IsEnabled = false;
            DateEntry.Text = tr.Date.ToString();
            DateEntry.IsEnabled = false;
        }

        private void ButtonEdit_Clicked(object sender, EventArgs e)
        {
            VolumeEntry.IsEnabled = true;
            MileageEntry.IsEnabled = true;
            DateEntry.IsEnabled = true;
        }
    }
}