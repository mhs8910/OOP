using System;
using System.Collections.Generic;

namespace SolarManagement
{
    // UI LAYER: admin screens for customer records.
    public class CustomerUI
    {
        private CustomerService _customerService;

        public CustomerUI()
        {
            _customerService = new CustomerService();
        }

        public void ShowCustomerMenu()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("=============== CUSTOMER MANAGEMENT ===============");
                Console.WriteLine("1 - Add Customer");
                Console.WriteLine("2 - View All Customers");
                Console.WriteLine("3 - Search Customer");
                Console.WriteLine("4 - Delete Customer");
                Console.WriteLine("5 - Export Customers to TXT File");
                Console.WriteLine("6 - Back to Admin Menu");
                Console.WriteLine("Enter Your Choice:");

                string choice = Console.ReadLine();

                if (choice == "1") AddCustomerUI();
                else if (choice == "2") ViewCustomersUI();
                else if (choice == "3") SearchCustomerUI();
                else if (choice == "4") DeleteCustomerUI();
                else if (choice == "5") ExportCustomersUI();
                else if (choice == "6") running = false;
                else
                {
                    Console.WriteLine("Invalid Input! Please enter a number between 1 and 6.");
                    InputHelper.Pause();
                }
            }
        }

        private void AddCustomerUI()
        {
            Console.WriteLine();
            Console.WriteLine("========== ADD NEW CUSTOMER ==========");

            string name = InputHelper.ReadText("Enter Customer Name:");
            string email = InputHelper.ReadText("Enter Customer Email:");
            string phone = InputHelper.ReadPhone("Enter Customer Phone (11 digits):");
            string address = InputHelper.ReadText("Enter Customer Address:");
            string systemType = InputHelper.ReadText("Enter System Type (Residential/Commercial/Industrial):");
            string inverter = InputHelper.ReadText("Enter Inverter Capacity (e.g. 5KW):");
            string panels = InputHelper.ReadText("Enter Solar Panels Model:");
            int capacity = InputHelper.ReadCapacity("Enter Solar System Capacity (1-100 KW):");

            // Price is 0 for now; the service calculates it.
            Customer customer = new Customer(name, phone, email, address,
                                             systemType, inverter, panels, capacity, 0);

            Console.WriteLine();
            if (_customerService.AddCustomer(customer))
            {
                Console.WriteLine("Customer Added Successfully!");
                Console.WriteLine("Estimated Project Price: " + customer.ProjectPrice + " PKR");
            }
            else
            {
                Console.WriteLine("Customer storage is full! Cannot add more customers.");
            }

            InputHelper.Pause();
        }

        private void ViewCustomersUI()
        {
            Console.WriteLine();
            Console.WriteLine("================ CUSTOMER LIST ================");

            List<Customer> customers = _customerService.GetAllCustomers();

            if (customers.Count == 0)
            {
                Console.WriteLine("No customers found.");
            }

            for (int i = 0; i < customers.Count; i++)
            {
                Customer c = customers[i];
                Console.WriteLine("Customer #" + (i + 1));
                Console.WriteLine("Name            : " + c.Name);
                Console.WriteLine("Phone           : " + c.Phone);
                Console.WriteLine("Email           : " + c.Email);
                Console.WriteLine("Address         : " + c.Address);
                Console.WriteLine("System Type     : " + c.SystemType);
                Console.WriteLine("System Capacity : " + c.SystemCapacity + " KW");
                Console.WriteLine("Inverter        : " + c.InverterCapacity);
                Console.WriteLine("Solar Panels    : " + c.SolarPanels);
                Console.WriteLine("Project Price   : " + c.ProjectPrice + " PKR");
                Console.WriteLine("--------------------------------------------------");
            }

            InputHelper.Pause();
        }

        private void SearchCustomerUI()
        {
            Console.WriteLine();
            Console.WriteLine("========== SEARCH CUSTOMER ==========");

            string phone = InputHelper.ReadText("Enter Customer Phone Number:");
            Customer c = _customerService.FindCustomerByPhone(phone);

            Console.WriteLine();
            if (c == null)
            {
                Console.WriteLine("Customer not found!");
            }
            else
            {
                Console.WriteLine("Customer Found!");
                Console.WriteLine("Name            : " + c.Name);
                Console.WriteLine("Phone           : " + c.Phone);
                Console.WriteLine("Email           : " + c.Email);
                Console.WriteLine("Address         : " + c.Address);
                Console.WriteLine("System Type     : " + c.SystemType);
                Console.WriteLine("System Capacity : " + c.SystemCapacity + " KW");
                Console.WriteLine("Project Price   : " + c.ProjectPrice + " PKR");
            }

            InputHelper.Pause();
        }

        private void DeleteCustomerUI()
        {
            Console.WriteLine();
            Console.WriteLine("========== DELETE CUSTOMER ==========");

            string phone = InputHelper.ReadText("Enter Customer Phone Number to Delete:");

            Console.WriteLine();
            if (_customerService.DeleteCustomerByPhone(phone))
            {
                Console.WriteLine("Customer Deleted Successfully!");
            }
            else
            {
                Console.WriteLine("Customer not found!");
            }

            InputHelper.Pause();
        }

        private void ExportCustomersUI()
        {
            _customerService.ExportCustomers();

            Console.WriteLine();
            Console.WriteLine("Customer list exported successfully to Customer_List.txt");
            InputHelper.Pause();
        }
    }
}
