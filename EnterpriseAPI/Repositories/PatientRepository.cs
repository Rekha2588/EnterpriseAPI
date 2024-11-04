using EnterpriseAPI.Context;
using EnterpriseAPI.Contract;
using EnterpriseAPI.Dto;
using EnterpriseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace EnterpriseAPI.Repositories
{
    public class PatientRepository: IPatientRepository
    {
        private readonly TakeCareDBContext _dbContext;
        public PatientRepository(TakeCareDBContext dbContext) 
        { 
            this._dbContext = dbContext;
        }
        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            if (_dbContext.Patients == null)
                return null;
            return await _dbContext.Patients.ToListAsync();
        }

        public async Task<Patient> GetPatientByPatientId(int patientId)
        {
            return await _dbContext.Patients.FindAsync(patientId);
        }

        public async Task<IEnumerable<Patient>> GetPatientByLastNameAndDateOfBirth(string lastName, string dateOfBirth)
        {
            return await _dbContext.Patients.AsAsyncEnumerable().Where(p => p.LastName.ToLower() == lastName.ToLower() && p.DateOfBirth.ToString("yyyy-MM-dd") == dateOfBirth).ToListAsync();
        }

        public async Task<IActionResult> CreateNewPatient(Patient patient)
        {
            if (patient != null)
            {
                _dbContext.Patients.Add(patient);
                await _dbContext.SaveChangesAsync();
                return new ObjectResult(patient) { StatusCode = StatusCodes.Status201Created };
            }
            return new ObjectResult(patient) { StatusCode = StatusCodes.Status400BadRequest };
        }

        public async Task<IActionResult> UpdatePatient(int patientId, Patient patient)
        {
            var existingPatient = await _dbContext.Patients.FindAsync(patientId);
            if (existingPatient != null)
            {
                existingPatient.FirstName = patient.FirstName ?? existingPatient.FirstName;
                existingPatient.LastName = patient.LastName ?? existingPatient.LastName;
                existingPatient.DateOfBirth = patient.DateOfBirth;
                existingPatient.Gender = patient.Gender;
                existingPatient.ContactNumber = patient.ContactNumber ?? existingPatient.ContactNumber;
                existingPatient.EmailAddress = patient.EmailAddress ?? existingPatient.EmailAddress;
                existingPatient.Address = patient.Address ?? existingPatient.Address;
                existingPatient.City = patient.City ?? existingPatient.City;
                existingPatient.Province = patient.Province ?? existingPatient.Province;
                existingPatient.Zipcode = patient.Zipcode ?? existingPatient.Zipcode;

                _dbContext.Entry(existingPatient).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return new ObjectResult(existingPatient) { StatusCode = StatusCodes.Status200OK };
            }
            return new ObjectResult(existingPatient) { StatusCode = StatusCodes.Status404NotFound };
        }
        
        public async Task<IActionResult> DeletePatient(int patientId)
        {
            var patient = await _dbContext.Patients.FindAsync(patientId);
            if (patient != null)
            {
                _dbContext.Patients.Remove(patient);
                await _dbContext.SaveChangesAsync();
                return new ObjectResult(patient) { StatusCode = StatusCodes.Status200OK };
            }
            return new ObjectResult(patient) { StatusCode = StatusCodes.Status404NotFound };
        }
    }
}
