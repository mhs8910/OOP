namespace SolarManagement
{
    // A solar customer record. Inherits Name, Phone, Email from Person.
    public class Customer : Person
    {
        public string Address;
        public string SystemType;        // Residential / Commercial / Industrial
        public string InverterCapacity;  // e.g. 5KW
        public string SolarPanels;       // panel model name
        public int SystemCapacity;       // in KW
        public int ProjectPrice;         // in PKR

        public Customer() { }

        public Customer(string name, string phone, string email, string address,
                        string systemType, string inverterCapacity, string solarPanels,
                        int systemCapacity, int projectPrice)
            : base(name, phone, email)
        {
            Address = address;
            SystemType = systemType;
            InverterCapacity = inverterCapacity;
            SolarPanels = solarPanels;
            SystemCapacity = systemCapacity;
            ProjectPrice = projectPrice;
        }

        // One line of the file.
        public static string ToString(Customer customer)
        {
            return customer.Name + "|" + customer.Phone + "|" + customer.Email + "|" + customer.Address + "|" + customer.SystemType + "|" +
                   customer.InverterCapacity + "|" + customer.SolarPanels + "|" + customer.SystemCapacity + "|" + customer.ProjectPrice;
        }
    }
}
