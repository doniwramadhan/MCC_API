using System.ComponentModel.DataAnnotations.Schema;

namespace APIMCC.Models
{
    [Table("tb_m_roles")]
    public class Role : BaseProp
    {
        [Column("name", TypeName ="nvarchar(100)")]
        public string Name { get; set; }

        //Cardinalities
        public ICollection<AccountRole>? AccountRoles { get; set; }
    }
}
