using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ChorbadzhiyskiKinesitherapy.Models
{
    public class PatientViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }

        [BsonRequired]
        [Required]
        public string Name { get; set; }

        [BsonRequired]
        [Required]
        public string MobileNumber { get; set; }

        public string? Address { get; set; }

        public string? Diagnose { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime? FirstAppointment { get; set; }

        public DateTime? LastAppointment { get; set; }

        public List<DateTime>? Appointments { get; set; }

        public string? Complaints { get; set; }

        public PatientViewModel()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
            MobileNumber = string.Empty;
            Address = string.Empty;
            Diagnose = string.Empty;
            Birthday = DateTime.Today;
            FirstAppointment = DateTime.Today;
            LastAppointment = DateTime.Today;
            Appointments = new List<DateTime>();
            Complaints = string.Empty;
        }
    }
}
