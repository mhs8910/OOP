using System;
using System.Collections.Generic;

namespace SolarManagement
{
    // UI LAYER: menu for signed-in customers (Role = "Customer").
    public class CustomerPortalUI
    {
        private SolarCalculatorService _calculator;
        private OrderService _orderService;

        public CustomerPortalUI()
        {
            _calculator = new SolarCalculatorService();
            _orderService = new OrderService();
        }

        public void ShowPortal(User user)
        {
            bool running = true;

            while (running)
            {
                InputHelper.ClearScreen();
                Console.WriteLine();
                Console.WriteLine("=============== CUSTOMER PORTAL ===============");
                Console.WriteLine("Logged in as: " + user.Name);
                Console.WriteLine("1 - Solar Price Estimator");
                Console.WriteLine("2 - View My Orders");
                Console.WriteLine("3 - Logout");
                Console.WriteLine("Enter Your Choice:");

                string choice = Console.ReadLine();

                if (choice == "1") ShowEstimator(user);
                else if (choice == "2") ShowMyOrders(user);
                else if (choice == "3") running = false;
                else
                {
                    Console.WriteLine("Invalid Input! Please enter 1, 2, or 3.");
                    InputHelper.Pause();
                }
            }
        }

        private void ShowEstimator(User user)
        {
            InputHelper.ClearScreen();
            Console.WriteLine();
            Console.WriteLine("========== PRICE ESTIMATOR ==========");

            int capacity = InputHelper.ReadCapacity("Enter Required Solar Capacity (1-100 KW):");

            Console.WriteLine();
            Console.WriteLine("Hello, " + user.Name + "!");
            Console.WriteLine("Panels Needed    : " + _calculator.CalculatePanels(capacity));
            Console.WriteLine("Inverters Needed : " + _calculator.CalculateInverters(capacity));
            Console.WriteLine("Batteries Needed : " + _calculator.CalculateBatteries(capacity));
            Console.WriteLine("Estimated Project Price for " + capacity + " KW: " +
                              _calculator.CalculatePrice(capacity) + " PKR");

            InputHelper.Pause();
        }

        // Shows only the orders placed with the customer's own phone number.
        private void ShowMyOrders(User user)
        {
            InputHelper.ClearScreen();
            Console.WriteLine();
            Console.WriteLine("================ MY ORDERS ================");

            List<Order> orders = _orderService.GetOrdersByPhone(user.Phone);

            if (orders.Count == 0)
            {
                Console.WriteLine("You have no orders yet.");
            }

            for (int i = 0; i < orders.Count; i++)
            {
                Order o = orders[i];
                Console.WriteLine("Order #" + (i + 1));
                Console.WriteLine("Product     : " + o.ProductName);
                Console.WriteLine("Quantity    : " + o.Quantity);
                Console.WriteLine("Total Price : " + o.TotalPrice + " PKR");
                Console.WriteLine("Status      : " + o.Status);
                Console.WriteLine("--------------------------------------------------");
            }

            InputHelper.Pause();
        }
    }
}
