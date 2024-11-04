using System.ComponentModel.DataAnnotations;

namespace EnterpriseAPI.Dto
{
    public class PatientDto
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }        
        public string LastName { get; set; }        
        public string DateOfBirth { get; set; }        
        public char Gender { get; set; }        
        public string ContactNumber { get; set; }        
        public string EmailAddress { get; set; }        
        public string Address { get; set; }        
        public string City { get; set; }        
        public string Province { get; set; }
        public string? Zipcode { get; set; }
    }
}
