using EnterpriseAPI.Dto;
using EnterpriseAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAPI.Contract
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatients();
        Task<Patient> GetPatientByPatientId(int patientId);
        Task<IEnumerable<Patient>> GetPatientByLastNameAndDateOfBirth(string lastName, string dateOfBirth);
        Task<IActionResult> CreateNewPatient(Patient patient);
        Task<IActionResult> UpdatePatient(int patientId, Patient patient);
        Task<IActionResult> DeletePatient(int patientId);
    }
}
