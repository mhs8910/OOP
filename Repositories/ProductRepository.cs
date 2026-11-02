using System.Collections.Generic;
using System.IO;

namespace SolarManagement
{
    // DATA ACCESS LAYER: inventory (products.txt).
    public class ProductRepository : BaseRepository
    {
        public ProductRepository() : base("products.txt")
        {
            // First run: create the file with the 5 sample products.
            if (!File.Exists(FilePath))
            {
                SaveAll(GetDefaultProducts());
            }
        }

        public void Save(Product product)
        {
            AppendLine(product.ToString());
        }

        public void SaveAll(List<Product> products)
        {
            List<string> lines = new List<string>();
            for (int i = 0; i < products.Count; i++)
            {
                lines.Add(products[i].ToString());
            }
            WriteLines(lines);
        }

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            List<string> lines = ReadLines();

            for (int i = 0; i < lines.Count; i++)
            {
                string[] parts = lines[i].Split('|');
                if (parts.Length == 4 && int.TryParse(parts[2], out int value2) && int.TryParse(parts[3], out int value3))
                {
                    products.Add(new Product(parts[0], parts[1], value2, value3));
                }
            }
            return products;
        }

        private List<Product> GetDefaultProducts()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product("Longi Solar Panel 550W", "High efficiency mono-crystalline solar panel", 50, 550));
            products.Add(new Product("Huawei Inverter 5KW", "Hybrid inverter for residential system", 10, 5000));
            products.Add(new Product("Lithium Battery 100Ah", "Deep cycle energy storage battery", 20, 10000));
            products.Add(new Product("Mounting Structure Kit", "Aluminum rooftop mounting system", 100, 15000));
            products.Add(new Product("DC Solar Cable 6mm", "UV resistant solar cable", 25, 200));
            return products;
        }
    }
}
