namespace APIMCC.Models
{
    public class Employee : Date
    {
        public Guid Guid { get; set; }
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
        public DateTime HireDare { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
}
