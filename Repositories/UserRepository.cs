using System.Collections.Generic;
using System.IO;

namespace SolarManagement
{
    // DATA ACCESS LAYER: login accounts (users.txt).
    public class UserRepository : BaseRepository
    {
        public UserRepository() : base("users.txt")
        {
            // First run: create the file with the default admin account.
            if (!File.Exists(FilePath))
            {
                Save(new User("Administrator", "03000000000", "admin@solar.com",
                              "admin", "123", "Admin"));
            }
        }

        public void Save(User user)
        {
            AppendLine(user.ToString());
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            List<string> lines = ReadLines();

            for (int i = 0; i < lines.Count; i++)
            {
                string[] parts = lines[i].Split('|');
                if (parts.Length == 6)
                {
                    users.Add(new User(parts[0], parts[1], parts[2], parts[3], parts[4], parts[5]));
                }
            }
            return users;
        }
    }
}
