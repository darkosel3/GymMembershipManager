namespace GymMembershipManager.Services
{
    public class UserSession
    {
        public string Role { get; private set; } = string.Empty;
        public string Username { get; private set; } = string.Empty;

        public void Set(string username, string role)
        {
            Username = username;
            Role = role;
        }

        public bool IsManager => Role == "Manager";
    }
}