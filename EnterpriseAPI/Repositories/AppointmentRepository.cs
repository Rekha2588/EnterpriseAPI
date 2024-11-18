using EnterpriseAPI.Context;
using EnterpriseAPI.Contract;
using EnterpriseAPI.Enum;
using EnterpriseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseAPI.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly TakeCareDBContext _dbContext;
        public AppointmentRepository(TakeCareDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Appointment> GetAppointmentByAppointmentId(int appointmentId)
        {
            return await _dbContext.Appointments.FindAsync(appointmentId);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentByPatientId(int patientId)
        {
            return await _dbContext.Appointments.Where(a => a.PatientId == patientId && a.Status == (short)AppointmentStatus.Confirmed).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentByLastNameAndDateOfBirth(string lastName, string dateOfBirth)
        {
            return await _dbContext.Appointments.Include(p => p.Patient).AsAsyncEnumerable()
                .Where(a => a.Patient.LastName.ToLower() == lastName.ToLower() &&
                a.Patient.DateOfBirth.ToString("yyyy-MM-dd") == dateOfBirth &&
                a.Status == (short)AppointmentStatus.Confirmed).ToListAsync();
        }

		public async Task<IEnumerable<Appointment>> GetAppointmentsByDate(string date)
		{
			return await _dbContext.Appointments.Include(p => p.Patient).AsAsyncEnumerable()
				.Where(a => a.StartTime.ToString("yyyy-MM-dd") == date &&
				a.Status == (short)AppointmentStatus.Confirmed).ToListAsync();
		}

		public async Task<IActionResult> CreateAppointment(Appointment appointment)
        {
            if (appointment != null)
            {
                _dbContext.Appointments.Add(appointment);
                await _dbContext.SaveChangesAsync();
                return new ObjectResult(appointment) { StatusCode = StatusCodes.Status201Created };
            }
            return new ObjectResult(appointment) { StatusCode = StatusCodes.Status400BadRequest };
        }

        public async Task<IActionResult> RescheduleAppointment(int appointmentId, Appointment appointment)
        {
            var existingAppointment = await _dbContext.Appointments.FindAsync(appointmentId);
            if (existingAppointment != null)
            {
                existingAppointment.StartTime = appointment.StartTime;
                existingAppointment.EndTime = appointment.EndTime;
                existingAppointment.Status = (short)AppointmentStatus.Confirmed;

                _dbContext.Entry(existingAppointment).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return new ObjectResult(existingAppointment) { StatusCode = StatusCodes.Status200OK };
            }
            return new ObjectResult(existingAppointment) { StatusCode = StatusCodes.Status404NotFound };
        }

        public async Task<IActionResult> CancelAppointment(int appointmentId)
        {
            var appointment = await _dbContext.Appointments.FindAsync(appointmentId);
            if (appointment != null)
            {
                appointment.Status = (short)AppointmentStatus.Cancelled;
                _dbContext.Entry(appointment).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return new ObjectResult(appointment) { StatusCode = StatusCodes.Status200OK };
            }
            return new ObjectResult(appointment) { StatusCode = StatusCodes.Status404NotFound };
        }
    }
}
