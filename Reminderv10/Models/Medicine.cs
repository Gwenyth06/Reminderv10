using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using MongoDbGenericRepository.Attributes;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace Reminderv10.Models
{
    
    public class Medicine
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("MedicineName")]
        public string MedicineName { get; set; }

        public int MedicineDose { get; set; }

        public bool IsComplete { get; set; }
        public int Time { get; set; }
        [BindProperty]
        public int[] Days { get; set; }
        public string? OwnerID { get; set; }
    }
}
