namespace APIMCC.Models
{
    public class AccountRoles : Date
    {
        public Guid Guid { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

    }
}
