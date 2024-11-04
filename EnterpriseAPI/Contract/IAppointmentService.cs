using EnterpriseAPI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAPI.Contract
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> GetAppointmentByAppointmentId(int appointmentId);
        Task<IEnumerable<AppointmentDto>> GetAppointmentByPatientId(int patientId);
        Task<IEnumerable<AppointmentDto>> GetAppointmentByLastNameAndDateOfBirth(string lastName, string dateOfBirth);
        Task<IActionResult> CreateAppointment(AppointmentAddUpdateDto appointmentDto);
        Task<IActionResult> RescheduleAppointment(int appointmentId, AppointmentAddUpdateDto appointmentDto);
        Task<IActionResult> CancelAppointment(int appointmentId);
    }
}
