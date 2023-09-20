using ChorbadzhiyskiKinesitherapy.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChorbadzhiyskiKinesitherapy.Services
{
    public class PatientsService
    {
        private readonly IMongoCollection<PatientViewModel> _patientsCollection;

        public PatientsService(
            IOptions<PatientsDatabaseSettings> patientsDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                patientsDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                patientsDatabaseSettings.Value.DatabaseName);

            _patientsCollection = mongoDatabase.GetCollection<PatientViewModel>(
                patientsDatabaseSettings.Value.PatientsCollectionName);
        }

        public async Task<List<PatientViewModel>> GetAsync()
        {
            var patientsData = await _patientsCollection.Find(_ => true).ToListAsync();
            return patientsData;
        }

        public async Task<PatientViewModel?> GetAsync(Guid id)
        {
            var patient = await _patientsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return patient;
        }

        public async Task CreateAsync(PatientViewModel newPatient)
        {
            await _patientsCollection.InsertOneAsync(newPatient);
        }

        public async Task UpdateAsync(Guid id, PatientViewModel updatedPatient)
        {
            var result = await _patientsCollection.ReplaceOneAsync(x => x.Id == id, updatedPatient);

            if (!result.IsAcknowledged)
            {
                throw new Exception($"Could not edit patient with ID {id}");
            }
        }

        public async Task RemoveAsync(Guid id)
        {
            var result = await _patientsCollection.DeleteOneAsync(x => x.Id == id);

            if (!result.IsAcknowledged)
            {
                throw new Exception($"Could not delete patient with ID {id}");
            }
        }
    }
}
