using System;
using System.Collections.Generic;

namespace SolarManagement
{
    // UI LAYER: admin screens for orders.
    public class OrderUI
    {
        private OrderService _orderService;

        public OrderUI()
        {
            _orderService = new OrderService();
        }

        public void ShowOrderMenu()
        {
            bool running = true;

            while (running)
            {
                InputHelper.ClearScreen();
                Console.WriteLine();
                Console.WriteLine("=============== ORDER MANAGEMENT ===============");
                Console.WriteLine("1 - Create Order");
                Console.WriteLine("2 - View All Orders");
                Console.WriteLine("3 - Back to Admin Menu");
                Console.WriteLine("Enter Your Choice:");

                string choice = Console.ReadLine();

                if (choice == "1") CreateOrderUI();
                else if (choice == "2") ViewOrdersUI();
                else if (choice == "3") running = false;
                else
                {
                    Console.WriteLine("Invalid Input! Please enter 1, 2, or 3.");
                    InputHelper.Pause();
                }
            }
        }

        private void CreateOrderUI()
        {
            InputHelper.ClearScreen();
            Console.WriteLine();
            Console.WriteLine("========== CREATE ORDER ==========");

            string name = InputHelper.ReadText("Enter Customer Name:");
            string phone = InputHelper.ReadPhone("Enter Customer Phone (11 digits):");
            string productName = InputHelper.ReadText("Enter Product Name (exactly as in inventory):");
            int quantity = InputHelper.ReadPositiveNumber("Enter Quantity (min 1):",
                                                          "Quantity must be at least 1.");

            // Total price and status are filled in by the service.
            Order order = new Order(name, phone, productName, quantity, 0, "");

            string result = _orderService.CreateOrder(order);

            Console.WriteLine();
            if (result == "Success")
            {
                Console.WriteLine("Order Created Successfully!");
                Console.WriteLine("Total Price : " + order.TotalPrice + " PKR");
                Console.WriteLine("Status      : " + order.Status);
            }
            else if (result == "NotFound")
            {
                Console.WriteLine("Product not found in inventory!");
            }
            else if (result == "LowStock")
            {
                Console.WriteLine("Insufficient stock! Available: " +
                                  _orderService.GetAvailableStock(productName));
            }
            else if (result == "InvalidTotal")
            {
                Console.WriteLine("Order total is too large. Reduce the quantity or check the product price.");
            }
            else
            {
                Console.WriteLine("Order storage is full! Cannot create more orders.");
            }

            InputHelper.Pause();
        }

        private void ViewOrdersUI()
        {
            InputHelper.ClearScreen();
            Console.WriteLine();
            Console.WriteLine("================ ORDERS ================");

            List<Order> orders = _orderService.GetAllOrders();

            if (orders.Count == 0)
            {
                Console.WriteLine("No orders placed yet.");
            }

            for (int i = 0; i < orders.Count; i++)
            {
                Order o = orders[i];
                Console.WriteLine("Order #" + (i + 1));
                Console.WriteLine("Customer Name : " + o.CustomerName);
                Console.WriteLine("Phone         : " + o.CustomerPhone);
                Console.WriteLine("Product       : " + o.ProductName);
                Console.WriteLine("Quantity      : " + o.Quantity);
                Console.WriteLine("Total Price   : " + o.TotalPrice + " PKR");
                Console.WriteLine("Status        : " + o.Status);
                Console.WriteLine("--------------------------------------------------");
            }

            InputHelper.Pause();
        }
    }
}
