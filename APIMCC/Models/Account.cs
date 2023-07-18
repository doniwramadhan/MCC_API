namespace APIMCC.Models
{
    public class Account : Date
    {
        public Guid Guid { get; set; }
        public string Password { get; set; }
        public int OTP { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
