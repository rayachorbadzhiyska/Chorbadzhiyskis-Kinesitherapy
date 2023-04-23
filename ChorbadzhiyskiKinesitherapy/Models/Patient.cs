using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ChorbadzhiyskiKinesitherapy.Models
{
	public class Patient
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public string MobileNumber { get; set; } = string.Empty;

		public string Address { get; set; } = string.Empty;

		public decimal Price { get; set; }

		public DateTime Birthday { get; set; }

		public DateTime FirstAppointment { get; set; }

		public List<DateTime> Appointments { get; set; }

		public List<string> Complaints { get; set; } 
	}
}
