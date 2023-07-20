using APIMCC.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIMCC.Models
{
    [Table("tb_m_employees")]
    public class Employee : BaseProp
    {
        [Column("nik",TypeName ="nchar(6)")]
        public string NIK { get; set; }
        
        [Column("first_name", TypeName ="nvarchar(100)")]
        public string FirstName { get; set; }

        [Column("last_name", TypeName ="nvarchar(100)")]
        public string? LastName { get; set; }

        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Column("gender")]
        public GenderLevel Gender { get; set; }

        [Column("hire_date")]
        public DateTime HireDate { get; set; }

        [Column("email", TypeName ="nvarchar(100)")]
        public string Email { get; set; }

        [Column("phone_number", TypeName ="nvarchar(20)")]
        public string PhoneNumber { get; set; }

        //Cardinalities
        public ICollection<Booking>? Bookings { get; set; }
        public Account? Account { get; set; }
        public Education? Education { get; set; }

    }
}
