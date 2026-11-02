using System;

namespace SolarManagement
{
    // UI LAYER: the admin panel (only for accounts with Role = "Admin").
    public class AdminUI
    {
        private CustomerUI _customerUI;
        private InventoryUI _inventoryUI;
        private OrderUI _orderUI;
        private CalculatorUI _calculatorUI;
        private ReportUI _reportUI;

        public AdminUI()
        {
            _customerUI = new CustomerUI();
            _inventoryUI = new InventoryUI();
            _orderUI = new OrderUI();
            _calculatorUI = new CalculatorUI();
            _reportUI = new ReportUI();
        }

        public void ShowAdminPanel(User admin)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("=============== ADMIN PANEL ===============");
                Console.WriteLine("Logged in as: " + admin.Name);
                Console.WriteLine("1 - Customer Management");
                Console.WriteLine("2 - Inventory Management");
                Console.WriteLine("3 - Order Management");
                Console.WriteLine("4 - Solar Setup Calculator");
                Console.WriteLine("5 - Reports");
                Console.WriteLine("6 - Logout");
                Console.WriteLine("Enter Your Choice:");

                string choice = Console.ReadLine();

                if (choice == "1") _customerUI.ShowCustomerMenu();
                else if (choice == "2") _inventoryUI.ShowInventoryMenu();
                else if (choice == "3") _orderUI.ShowOrderMenu();
                else if (choice == "4") _calculatorUI.ShowCalculator();
                else if (choice == "5") _reportUI.ShowReports();
                else if (choice == "6") running = false;
                else
                {
                    Console.WriteLine("Invalid Input! Please enter a number between 1 and 6.");
                    InputHelper.Pause();
                }
            }
        }
    }
}
