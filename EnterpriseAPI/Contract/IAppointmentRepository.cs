using EnterpriseAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAPI.Contract
{
    public interface IAppointmentRepository
    {
        Task<Appointment> GetAppointmentByAppointmentId(int appointmentId);
        Task<IEnumerable<Appointment>> GetAppointmentByPatientId(int patientId);
        Task<IEnumerable<Appointment>> GetAppointmentByLastNameAndDateOfBirth(string lastName, string dateOfBirth);
        Task<IActionResult> CreateAppointment(Appointment appointment);
        Task<IActionResult> RescheduleAppointment(int appointmentId, Appointment appointment);
        Task<IActionResult> CancelAppointment(int appointmentId);
    }
}
