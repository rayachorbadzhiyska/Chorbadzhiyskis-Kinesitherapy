using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ChorbadzhiyskiKinesitherapy.Models
{
    public class PatientViewModel
    {
        [BsonId]
        public string Id { get; set; }

        [BsonRequired]
        [Required]
        public string Name { get; set; }

        [BsonRequired]
        [Required]
        public string MobileNumber { get; set; }

        [BsonRequired]
        [Required]
        public string EGN { get; set; }

        public string? Address { get; set; }

        public string? Diagnose { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? Birthday { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? FirstAppointment { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? LastAppointment { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public List<DateTime>? Appointments { get; set; }

        public string? Complaints { get; set; }

        public string? Notes { get; set; }

        public PatientViewModel()
        {
            Id = Guid.NewGuid().ToString();
            Name = string.Empty;
            MobileNumber = string.Empty;
            EGN = string.Empty;
            Address = string.Empty;
            Diagnose = string.Empty;
            Birthday = DateTime.Today;
            FirstAppointment = DateTime.Today;
            LastAppointment = DateTime.Today;
            Appointments = new List<DateTime>();
            Complaints = string.Empty;
            Notes = string.Empty;
        }
    }
}
