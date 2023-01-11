using Reminderv10.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Reminderv10.Services
{
    public class MedicineService
    {
        private readonly IMongoCollection<Medicine> _medicines;

        public MedicineService(IMedicineDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _medicines = database.GetCollection<Medicine>(settings.MedicinesCollectionName);
        }

        public List<Medicine> Get(string id) =>
            _medicines.Find(medicine => medicine.OwnerID == id).ToList();

        // public Medicine Get(string id) =>
        //   _medicines.Find<Medicine>().FirstOrDefault();

        public Medicine Create(Medicine medicine)
        {
            _medicines.InsertOne(medicine);
            return medicine;
        }

        public void Update(string id, Medicine medicineIn) =>
            _medicines.ReplaceOne(medicine => medicine.Id == id, medicineIn);

        public void Remove(Medicine medicineIn) =>
            _medicines.DeleteOne(medicine => medicine.Id == medicineIn.Id);

        public void Remove(string id) =>
            _medicines.DeleteOne(medicine => medicine.Id == id);
    }
}