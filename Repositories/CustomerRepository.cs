using System.Collections.Generic;
using System.IO;

namespace SolarManagement
{
    // DATA ACCESS LAYER: customer records (customers.txt).
    public class CustomerRepository : BaseRepository
    {
        public CustomerRepository() : base("customers.txt")
        {
            // First run: create the file with the 3 sample customers.
            if (!File.Exists(FilePath))
            {
                SaveAll(GetDefaultCustomers());
            }
        }

        public void Save(Customer customer)
        {
            AppendLine(customer.ToString());
        }

        public void SaveAll(List<Customer> customers)
        {
            List<string> lines = new List<string>();
            for (int i = 0; i < customers.Count; i++)
            {
                lines.Add(customers[i].ToString());
            }
            WriteLines(lines);
        }

        public List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();
            List<string> lines = ReadLines();

            for (int i = 0; i < lines.Count; i++)
            {
                string[] parts = lines[i].Split('|');
                if (parts.Length == 9)
                {
                    Customer customer = new Customer(parts[0], parts[1], parts[2], parts[3],
                        parts[4], parts[5], parts[6], int.Parse(parts[7]), int.Parse(parts[8]));
                    customers.Add(customer);
                }
            }
            return customers;
        }

        // Writes a nicely formatted report to Customer_List.txt.
        public void ExportToTxt(List<Customer> customers)
        {
            using (StreamWriter writer = new StreamWriter("Customer_List.txt", false))
            {
                writer.WriteLine("==========================================================");
                writer.WriteLine("              SOLAR BUSINESS CUSTOMER LIST                ");
                writer.WriteLine("==========================================================");
                writer.WriteLine();

                for (int i = 0; i < customers.Count; i++)
                {
                    Customer c = customers[i];
                    writer.WriteLine("Customer #" + (i + 1));
                    writer.WriteLine("Name            : " + c.Name);
                    writer.WriteLine("Phone           : " + c.Phone);
                    writer.WriteLine("Email           : " + c.Email);
                    writer.WriteLine("Address         : " + c.Address);
                    writer.WriteLine("System Type     : " + c.SystemType);
                    writer.WriteLine("System Capacity : " + c.SystemCapacity + " KW");
                    writer.WriteLine("Inverter        : " + c.InverterCapacity);
                    writer.WriteLine("Solar Panels    : " + c.SolarPanels);
                    writer.WriteLine("Project Price   : " + c.ProjectPrice + " PKR");
                    writer.WriteLine("----------------------------------------------------------");
                }
            }
        }

        // Sample data from the original C++ program.
        private List<Customer> GetDefaultCustomers()
        {
            List<Customer> customers = new List<Customer>();
            customers.Add(new Customer("Ali Khan", "03001234567", "ali.khan@gmail.com",
                "Lahore, Johar Town", "Residential", "5KW", "Longi Solar Panel 550W", 5, 155000));
            customers.Add(new Customer("Usman Tariq", "03114567890", "usman.tariq@yahoo.com",
                "Faisalabad, Madina Town", "Commercial", "10KW", "Trina Solar Panel 450W", 10, 310000));
            customers.Add(new Customer("Hassan Raza", "03331234567", "hassan.raza@gmail.com",
                "Sheikhupura, City Center", "Industrial", "20KW", "Jinko Solar Panel 400W", 20, 620000));
            return customers;
        }
    }
}
