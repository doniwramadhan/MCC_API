using System.ComponentModel.DataAnnotations.Schema;

namespace APIMCC.Models
{
    [Table("tb_tr_account_roles")]
    public class AccountRole : BaseProp
    {
        [Column("account_guid")]
        public Guid AccountGuid { get; set; }
       
        [Column("role_guid")]
        public Guid RoleGuid { get; set; }

        //Cardinalities

        public Account? Account { get; set; }
        public Role? Role { get; set; }
    }
}
