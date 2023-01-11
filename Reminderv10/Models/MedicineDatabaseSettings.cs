namespace Reminderv10.Models
{
    public class MedicineDatabaseSettings : IMedicineDatabaseSettings
    {
        public string MedicinesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMedicineDatabaseSettings
    {
        string MedicinesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}