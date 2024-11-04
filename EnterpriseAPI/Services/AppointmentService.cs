using AutoMapper;
using EnterpriseAPI.Contract;
using EnterpriseAPI.Dto;
using EnterpriseAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            this._appointmentRepository = appointmentRepository;
            this._mapper = mapper;
        }

        public async Task<AppointmentDto> GetAppointmentByAppointmentId(int appointmentId)
        {
            var appointment = await _appointmentRepository.GetAppointmentByAppointmentId(appointmentId);
            return _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentByPatientId(int patientId)
        {
            var appointment = await _appointmentRepository.GetAppointmentByPatientId(patientId);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointment);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentByLastNameAndDateOfBirth(string lastName, string dateOfBirth)
        {
            var appointments = await _appointmentRepository.GetAppointmentByLastNameAndDateOfBirth(lastName, dateOfBirth);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task<IActionResult> CreateAppointment(AppointmentAddUpdateDto appointmentDto)
        {
            var appointment = _mapper.Map<Appointment>(appointmentDto);
            return await _appointmentRepository.CreateAppointment(appointment);
        }

        public async Task<IActionResult> RescheduleAppointment(int appointmentId, AppointmentAddUpdateDto appointmentDto)
        {
            var appointment = _mapper.Map<Appointment>(appointmentDto);
            return await _appointmentRepository.RescheduleAppointment(appointmentId, appointment);
        }

        public async Task<IActionResult> CancelAppointment(int appointmentId)
        {
            return await _appointmentRepository.CancelAppointment(appointmentId);
        }
    }
}
