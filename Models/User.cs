namespace SolarManagement
{
    // A login account. Inherits Name, Phone, Email from Person.
    // Role is either "Admin" or "Customer".
    public class User : Person
    {
        public string Username;
        public string Password;
        public string Role;

        public User() { }

        public User(string name, string phone, string email,
                    string username, string password, string role)
            : base(name, phone, email)
        {
            Username = username;
            Password = password;
            Role = role;
        }

        public static string ToString(User user)
        {
            return user.Name + "|" + user.Phone + "|" + user.Email + "|" + user.Username + "|" + user.Password + "|" + user.Role;
        }
    }
}
