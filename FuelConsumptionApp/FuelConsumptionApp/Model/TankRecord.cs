using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace FuelConsumptionApp.Model
{
    public class TankRecord
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public float Volume { get; set; }
        public float Mileage { get; set; }
        public float Consumption { get; set; }
        public DateTime Date { get; set; }

        public string RecordToString()
        {
            string rec_string = Volume.ToString("0.0") + ";" + Mileage.ToString("0.0") + ";" + Consumption.ToString("0.0") + ";" + Date + ";";
            return rec_string;
        }

        public static List<TankRecord> LoadRecords()
        {
            List<TankRecord> records = App.Database.GetRecordsAsync().Result;
            return records;
        }


        public static float ComputeConsumption(float volume, float mileage)
        {
            float consumption = mileage != 0 ? (volume / mileage * 100) : 0;
            return consumption;
        }

        public static double CalculateOverall()
        {
            float overall;
            List<TankRecord> records = LoadRecords();
            if (!records.Any())
            {
                return 0;
            }
            float[] mileages = new float[records.Count];
            float[] consumptions = new float[records.Count];
            for (int i = 0; i < records.Count; i++)
            {
                mileages[i] = records[i].Mileage;
                consumptions[i] = records[i].Consumption;
            }

            if (mileages.Sum() > 0)
            {
                overall = Math.CalculateInnerProduct(mileages, consumptions) / mileages.Sum();
                return overall;
            }
            else
            {
                return 0;
            }

        }
    }

}
