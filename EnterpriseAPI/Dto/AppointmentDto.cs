
using EnterpriseAPI.Enum;

namespace EnterpriseAPI.Dto
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }        
        public AppointmentStatus Status { get; set; }        
        public string StartTime { get; set; }        
        public string EndTime { get; set; }        
        public int PatientId { get; set; }
    }
}
