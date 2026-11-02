using System;
using System.Collections.Generic;
using System.Transactions;

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
                InputHelper.ClearScreen();
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
            InputHelper.ClearScreen();
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
                Console.WriteLine("A customer with this phone number may already exist, or customer storage is full.");
            }

            InputHelper.Pause();
        }

        private void ViewCustomersUI()
        {
            InputHelper.ClearScreen();
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
            InputHelper.ClearScreen();
            List<Customer> customers = _customerService.GetAllCustomers();

            Console.WriteLine();
            Console.WriteLine("========== SEARCH CUSTOMER ==========");

            Console.WriteLine("How do you want to search Customers");
            Console.WriteLine("1-By Name");
            Console.WriteLine("2-By System Capacity");
            Console.WriteLine("3-By System Type");
            Console.WriteLine();
            Console.Write("Enter your choice : ");

            int input = int.Parse(Console.ReadLine());

            if (input == 1)
            {
                InputHelper.ClearScreen();
                Console.WriteLine("Enter the name of the customer : ");
                string name = "";
                Console.WriteLine(name);
                _customerService.FindCustomerByName(name);

            }
            if (input == 2)
            {
                InputHelper.ClearScreen();
                Console.WriteLine("Enter the maximum System Capacity : ");
                int SystemCapacitymaximum = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the minimum System Capacity : ");
                int SystemCapacityminimum = int.Parse(Console.ReadLine());
                Customer c=_customerService.FindCustomerBySystemCapacity(SystemCapacitymaximum, SystemCapacityminimum);
                if (customers.Count == 0)
                {
                    Console.WriteLine("No customer found");
                }
                for (int i = 0; i < customers.Count; i++)
                {
                    c = customers[i];

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
                InputHelper.ClearScreen();

            }

            if (input == 3)
            {
                InputHelper.ClearScreen();
                Console.WriteLine("Enter the System Type (Write Residential, Commercial or Industrial for respective type) : ");
                string SystemType = Console.ReadLine();

                Customer c = _customerService.FindCustomerBySystemType(SystemType);
                if (customers.Count == 0)
                {
                    Console.WriteLine("No customer found");
                }
                for (int i = 0; i < customers.Count; i++)
                {
                    c = customers[i];

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
                InputHelper.ClearScreen();
            }


        }

        private void DeleteCustomerUI()
        {
            InputHelper.ClearScreen();
            Console.WriteLine();
            Console.WriteLine("========== DELETE CUSTOMER ==========");

            string phone = InputHelper.ReadPhone("Enter Customer Phone Number to Delete (11 digits):");

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
            InputHelper.ClearScreen();
            _customerService.ExportCustomers();

            Console.WriteLine();
            Console.WriteLine("Customer list exported successfully to Customer_List.txt");
            InputHelper.Pause();
        }
    }
}
