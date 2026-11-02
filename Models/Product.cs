namespace SolarManagement
{
    // One inventory item.
    public class Product
    {
        public string Name;
        public string Description;
        public int Quantity;
        public int Price;   // in PKR

        public Product() { }

        public Product(string name, string description, int quantity, int price)
        {
            Name = name;
            Description = description;
            Quantity = quantity;
            Price = price;
        }

        public override string ToString()
        {
            return Name + "|" + Description + "|" + Quantity + "|" + Price;
        }
    }
}
