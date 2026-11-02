using System.Collections.Generic;

namespace SolarManagement
{
    // BUSINESS LOGIC LAYER: sign up and sign in rules.
    public class AuthService
    {
        private UserRepository _repository;

        public AuthService()
        {
            _repository = new UserRepository();
        }

        // Returns "" if the account was created, otherwise the error message.
        // Everyone who signs up gets the role "Customer".
        public string SignUp(User user)
        {
            if (!Validator.IsValidPhone(user.Phone))
            {
                return "Phone number must be exactly 11 digits.";
            }

            if (user.Password.Length < 3)
            {
                return "Password must be at least 3 characters long.";
            }

            List<User> users = _repository.GetAll();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Username == user.Username)
                {
                    return "This username is already taken.";
                }
            }

            user.Role = "Customer";
            _repository.Save(user);
            return "";
        }

        // Returns the matching User, or null if username/password are wrong.
        public User SignIn(string username, string password)
        {
            List<User> users = _repository.GetAll();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Username == username && users[i].Password == password)
                {
                    return users[i];
                }
            }
            return null;
        }
    }
}
