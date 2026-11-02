namespace SolarManagement
{
    // BASE CLASS: data that every person in the system has.
    // Customer and User both INHERIT from this class.
    public class Person
    {
        public string Name;
        public string Phone;
        public string Email;

        public Person() { }

        public Person(string name, string phone, string email)
        {
            Name = name;
            Phone = phone;
            Email = email;
        }
    }
}
