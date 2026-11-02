namespace SolarManagement
{
    // One customer order.
    public class Order
    {
        public string CustomerName;
        public string CustomerPhone;
        public string ProductName;
        public int Quantity;
        public int TotalPrice;
        public string Status;

        public Order() { }

        public Order(string customerName, string customerPhone, string productName,
                     int quantity, int totalPrice, string status)
        {
            CustomerName = customerName;
            CustomerPhone = customerPhone;
            ProductName = productName;
            Quantity = quantity;
            TotalPrice = totalPrice;
            Status = status;
        }

        public static string ToString(Order order)
        {
            return order.CustomerName + "|" + order.CustomerPhone + "|" + order.ProductName + "|" +
                   order.Quantity + "|" + order.TotalPrice + "|" + order.Status;
        }
    }
}
