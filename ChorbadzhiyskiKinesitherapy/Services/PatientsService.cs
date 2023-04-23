using ChorbadzhiyskiKinesitherapy.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChorbadzhiyskiKinesitherapy.Services
{
	public class PatientsService
	{
		private readonly IMongoCollection<Patient> _patientsCollection;

		public PatientsService(
			IOptions<PatientsDatabaseSettings> patientsDatabaseSettings)
		{
			var mongoClient = new MongoClient(
				patientsDatabaseSettings.Value.ConnectionString);

			var mongoDatabase = mongoClient.GetDatabase(
				patientsDatabaseSettings.Value.DatabaseName);

			_patientsCollection = mongoDatabase.GetCollection<Patient>(
				patientsDatabaseSettings.Value.PatientsCollectionName);
		}

		public async Task<List<Patient>> GetAsync() =>
			await _patientsCollection.Find(_ => true).ToListAsync();

		public async Task<Patient?> GetAsync(string id) =>
			await _patientsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

		public async Task CreateAsync(Patient newPatient) =>
			await _patientsCollection.InsertOneAsync(newPatient);

		public async Task UpdateAsync(string id, Patient updatedPatient) =>
			await _patientsCollection.ReplaceOneAsync(x => x.Id == id, updatedPatient);

		public async Task RemoveAsync(string id) =>
			await _patientsCollection.DeleteOneAsync(x => x.Id == id);
	}
}
