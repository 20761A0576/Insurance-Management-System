using InsuranceProviderManagementSystem.Models;

namespace InsuranceProviderManagementSystem.Services
{
    public class UserService
    {
        private readonly int _dailyUsageLimitInMinutes;
        public InsuranceProviderManagementSystemContext context=new InsuranceProviderManagementSystemContext();

        public UserService()
        {
            _dailyUsageLimitInMinutes = 4;
            //_context = context;
        }

        public bool ValidateUser(string username, string password)
        {
            // Fetch user from DB and validate password (use hashed password in production)
            // For simplicity, assuming a user exists with credentials "user"/"password"
            var user = context.UserDetails.FirstOrDefault(u => u.UserId == username && u.Password == password);
            var ids = context.UserSessionLogs.FirstOrDefault(user => user.UserId == username);
            if (ids == null)
            {
                UserSessionLog userSession = new UserSessionLog()
                {
                    UserId = username,
                    LoginTime = DateTime.Now,
                    Duration = 0
                };
                context.UserSessionLogs.Add(userSession);
                context.SaveChanges();
            }
            else
            {
                ids.LoginTime = DateTime.Now;
                context.SaveChanges();
            }
            if(user == null)
            {
                return false;
            }
            return true;
        }

        public bool CanAccessToday(string userId)
        {
            // Get user sessions for today
            //var todaySessions = GetSessionsForToday(userId);
            //var totalTimeToday = todaySessions.Sum(session => session.Duration.TotalMinutes);
            int? totalTimeToday = context.UserSessionLogs
                               .Where(l => l.UserId == userId)
                               .Select(l => l.Duration)
                               .Max();

            return totalTimeToday < _dailyUsageLimitInMinutes;
        }

        private List<UserSession> GetSessionsForToday(int userId)
        {
            // Fetch sessions for today from database for the user
            return new List<UserSession>(); // Replace with actual DB call
        }
        
        public void CreateSession(int userId)
        {
            // Create new session record with login time
        }

        public void EndSession(int userId)
        {
            // Set logout time for the latest session
        }
    }


}
