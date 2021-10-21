using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using SQLite;
using FuelConsumptionApp.Model;

namespace FuelConsumptionApp.Data
{
    public class TankRecordDatabase
    {
        readonly SQLiteAsyncConnection database;

        public TankRecordDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<TankRecord>().Wait();
        }

        public Task<List<TankRecord>> GetRecordsAsync()
        {
            return database.Table<TankRecord>().ToListAsync();
        }

        public Task<TankRecord> GetRecordAsync(int id)
        {
            return database.Table<TankRecord>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveRecordAsync(TankRecord tr)
        {
            if (tr.ID != 0)
            {
                return database.UpdateAsync(tr);
            }
            else
            {
                return database.InsertAsync(tr);
            }
        }

        public Task<int> DeleteRecordAsync(TankRecord tr)
        {
            return database.DeleteAsync(tr);
        }

    }
}
