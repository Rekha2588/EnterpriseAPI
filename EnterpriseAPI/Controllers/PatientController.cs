using EnterpriseAPI.Contract;
using EnterpriseAPI.Dto;
using EnterpriseAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace EnterpriseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            this._patientService = patientService;
        }

        [HttpGet("GetAllPatients")]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAllPatients()
        {
            var patients = await _patientService.GetAllPatients();
            if(patients != null)
                return Ok(patients);
            else
                return NotFound();
        }

        [HttpGet("GetPatientByPatientId")]
        public async Task<ActionResult<PatientDto>> GetPatientByPatientId(int patientId)
        {
            var patient = await _patientService.GetPatientByPatientId(patientId);
            if (patient != null)
                return Ok(patient);
            else
                return NotFound();
        }

        [HttpGet("GetPatientByLastNameAndDateOfBirth")]
        public async Task<ActionResult<PatientDto>> GetPatientByLastNameAndDateOfBirth(string lastName, string dateOfBirth)
        {
            var patient = await _patientService.GetPatientByLastNameAndDateOfBirth(lastName, dateOfBirth);
            if (patient != null)
                return Ok(patient);
            else
                return NotFound();
        }        

        [HttpPost("AddPatient")]
        public async Task<IActionResult> CreateNewPatient(PatientAddUpdateDto patientDto)
        {
            return await _patientService.CreateNewPatient(patientDto);
        }

        [HttpPut("EditPatient")]
        public async Task<IActionResult> UpdatePatient(int patientId, PatientAddUpdateDto patientDto)
        {
            return await _patientService.UpdatePatient(patientId, patientDto);
        }

        [HttpDelete("DeletePatient")]
        public async Task<IActionResult> DeletePatient(int patientId)
        {
            return await _patientService.DeletePatient(patientId);
        }
    }
}
