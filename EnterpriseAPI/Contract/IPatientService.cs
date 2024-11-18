using EnterpriseAPI.Dto;
using EnterpriseAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAPI.Contract
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDto>> GetAllPatients();
        Task<PatientDto> GetPatientByPatientId(int patientId);
        Task<PatientDto> GetPatientByLastNameAndDateOfBirth(string lastName, string dateOfBirth);        
        Task<IActionResult> CreateNewPatient(PatientAddUpdateDto patientDto);
        Task<IActionResult> UpdatePatient(int patientId, PatientAddUpdateDto patientDto);
        Task<IActionResult> DeletePatient(int patientId);
    }
}
