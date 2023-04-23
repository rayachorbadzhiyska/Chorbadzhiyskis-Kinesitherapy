namespace ChorbadzhiyskiKinesitherapy.Models
{
	public class PatientsDatabaseSettings
	{
		public string ConnectionString { get; set; } = null!;

		public string DatabaseName { get; set; } = null!;

		public string PatientsCollectionName { get; set; } = null!;
	}
}
