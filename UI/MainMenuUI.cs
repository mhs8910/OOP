using System;

namespace SolarManagement
{
    // UI LAYER: first screen. Sign up, sign in, or exit.
    public class MainMenuUI
    {
        private AuthUI _authUI;
        private AdminUI _adminUI;
        private CustomerPortalUI _customerPortalUI;

        public MainMenuUI()
        {
            _authUI = new AuthUI();
            _adminUI = new AdminUI();
            _customerPortalUI = new CustomerPortalUI();
        }

        public void Show()
        {
            bool running = true;

            while (running)
            {
                InputHelper.ClearScreen();
                Console.WriteLine();
                Console.WriteLine("==================================================");
                Console.WriteLine("         SOLAR BUSINESS MANAGEMENT SYSTEM         ");
                Console.WriteLine("==================================================");
                Console.WriteLine("1 - Sign Up");
                Console.WriteLine("2 - Sign In");
                Console.WriteLine("3 - Exit");
                Console.WriteLine("Enter Your Choice:");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    _authUI.ShowSignUp();
                }
                else if (choice == "2")
                {
                    User user = _authUI.ShowSignIn();

                    // Admin and Customer accounts see different menus.
                    if (user != null)
                    {
                        if (user.Role == "Admin")
                        {
                            _adminUI.ShowAdminPanel(user);
                        }
                        else
                        {
                            _customerPortalUI.ShowPortal(user);
                        }
                    }
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Thank you for using Solar Business Management System!");
                    Console.WriteLine("Goodbye.");
                    running = false;
                }
                else
                {
                    Console.WriteLine("Invalid Input! Please enter 1, 2, or 3.");
                    InputHelper.Pause();
                }
            }
        }
    }
}
