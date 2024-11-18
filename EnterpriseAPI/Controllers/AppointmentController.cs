using EnterpriseAPI.Contract;
using EnterpriseAPI.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            this._appointmentService = appointmentService;
        }

        [HttpGet("GetAppointmentByAppointmentId")]
        public async Task<ActionResult<AppointmentDto>> GetAppointmentByAppointmentId(int appointmentId)
        {
            var appointment = await _appointmentService.GetAppointmentByAppointmentId(appointmentId);
            if (appointment != null)
                return Ok(appointment);
            else
                return NotFound();
        }

        [HttpGet("GetAppointmentByPatientId")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentByPatientId(int patientId)
        {
            var appointments = await _appointmentService.GetAppointmentByPatientId(patientId);
            if (appointments != null)
                return Ok(appointments);
            else
                return NotFound();
        }

        [HttpGet("GetAppointmentByLastNameAndDateOfBirth")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentByLastNameAndDateOfBirth(string lastName, string dateOfBirth)
        {
            var appointments = await _appointmentService.GetAppointmentByLastNameAndDateOfBirth(lastName, dateOfBirth);
            if (appointments != null)
                return Ok(appointments);
            else
                return NotFound();
        }

		[HttpGet("GetAppointmentsByDate")]
		public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsByDate(string date)
		{
			var appointments = await _appointmentService.GetAppointmentsByDate(date);
			if (appointments != null)
				return Ok(appointments);
			else
				return NotFound();
		}

		[HttpPost("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment(AppointmentAddUpdateDto appointmentDto)
        {
            return await _appointmentService.CreateAppointment(appointmentDto);
        }

        [HttpPut("RescheduleAppointment")]
        public async Task<IActionResult> RescheduleAppointment(int appointmentId, AppointmentAddUpdateDto appointmentDto)
        {
            return await _appointmentService.RescheduleAppointment(appointmentId, appointmentDto);
        }

        [HttpDelete("CancelAppointment")]
        public async Task<IActionResult> CancelAppointment(int appointmentId)
        {
            return await _appointmentService.CancelAppointment(appointmentId);
        }
    }
}
