using AutoMapper;
using EnterpriseAPI.Context;
using EnterpriseAPI.Contract;
using EnterpriseAPI.Dto;
using EnterpriseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace EnterpriseAPI.Services
{
    public class PatientService: IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        public PatientService(IPatientRepository patientRepository, IMapper mapper)
        {
            this._patientRepository = patientRepository;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatients()
        {            
            var patients = await _patientRepository.GetAllPatients();
            return _mapper.Map<IEnumerable<PatientDto>>(patients);
        }

        public async Task<PatientDto> GetPatientByPatientId(int patientId)
        {
            var patient = await _patientRepository.GetPatientByPatientId(patientId);  
            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<IEnumerable<PatientDto>> GetPatientByLastNameAndDateOfBirth(string lastName, string dateOfBirth)
        {
            var patients = await _patientRepository.GetPatientByLastNameAndDateOfBirth(lastName, dateOfBirth);
            return _mapper.Map<IEnumerable<PatientDto>>(patients);
        }        

        public async Task<IActionResult> CreateNewPatient(PatientAddUpdateDto patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);
            return await _patientRepository.CreateNewPatient(patient);             
        }

        public async Task<IActionResult> UpdatePatient(int patientId, PatientAddUpdateDto patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);
            return await _patientRepository.UpdatePatient(patientId, patient);            
        }

        public async Task<IActionResult> DeletePatient(int patientId)
        {
            return await _patientRepository.DeletePatient(patientId);
        }
    }
}
