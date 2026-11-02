using System.Collections.Generic;

namespace SolarManagement
{
    // DATA ACCESS LAYER: orders (orders.txt).
    public class OrderRepository : BaseRepository
    {
        public OrderRepository() : base("orders.txt")
        {
        }

        public void Save(Order order)
        {
            AppendLine(order.ToString());
        }

        public List<Order> GetAll()
        {
            List<Order> orders = new List<Order>();
            List<string> lines = ReadLines();

            for (int i = 0; i < lines.Count; i++)
            {
                string[] parts = lines[i].Split('|');
                if (parts.Length == 6 && int.TryParse(parts[3], out int value3) && int.TryParse(parts[4], out int value4))
                {
                    orders.Add(new Order(parts[0], parts[1], parts[2],
                                         value3, value4, parts[5]));
                }
            }
            return orders;
        }
    }
}
