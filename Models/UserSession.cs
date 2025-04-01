namespace InsuranceProviderManagementSystem.Models
{
    public class UserSession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LogoutTime { get; set; }
        public TimeSpan Duration => LogoutTime - LoginTime;
    }

}
