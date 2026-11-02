using System;

namespace SolarManagement
{
    // UI LAYER: sign up and sign in screens.
    public class AuthUI
    {
        private AuthService _authService;

        public AuthUI()
        {
            _authService = new AuthService();
        }

        public void ShowSignUp()
        {
            Console.WriteLine();
            Console.WriteLine("=============== SIGN UP ===============");

            string name = InputHelper.ReadText("Enter Your Full Name:");
            string phone = InputHelper.ReadPhone("Enter Your Phone (11 digits):");
            string email = InputHelper.ReadText("Enter Your Email:");
            string username = InputHelper.ReadText("Choose a Username:");
            string password = InputHelper.ReadText("Choose a Password (min 3 characters):");

            // The service decides the Role ("Customer"), so we send an empty one.
            User user = new User(name, phone, email, username, password, "");

            string error = _authService.SignUp(user);

            Console.WriteLine();
            if (error == "")
            {
                Console.WriteLine("Account created successfully! You can now sign in.");
            }
            else
            {
                Console.WriteLine("Sign up failed: " + error);
            }

            InputHelper.Pause();
        }

        // Gives the user 3 attempts. Returns the User, or null if all attempts fail.
        public User ShowSignIn()
        {
            Console.WriteLine();
            Console.WriteLine("=============== SIGN IN ===============");

            int attempts = 0;
            while (attempts < 3)
            {
                string username = InputHelper.ReadText("Enter Username:");
                string password = InputHelper.ReadText("Enter Password:");

                User user = _authService.SignIn(username, password);

                if (user != null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Welcome, " + user.Name + "!");
                    return user;
                }

                attempts++;
                Console.WriteLine("Invalid Username or Password! Attempts left: " + (3 - attempts));
            }

            Console.WriteLine("Too many failed attempts. Returning to the main menu.");
            InputHelper.Pause();
            return null;
        }
    }
}
