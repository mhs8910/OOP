using System.Collections.Generic;

namespace SolarManagement
{
    // BUSINESS LOGIC LAYER: rules for orders.
    public class OrderService
    {
        private OrderRepository _orderRepository;
        private ProductService _productService;

        public OrderService()
        {
            _orderRepository = new OrderRepository();
            _productService = new ProductService();
        }

        // Checks stock, deducts it, sets total price and "Pending" status, then saves.
        // Returns one of: "Success", "Full", "NotFound", "LowStock"
        public string CreateOrder(Order order)
        {
            if (_orderRepository.GetAll().Count >= 1000)
            {
                return "Full";
            }

            Product product = _productService.FindProductByName(order.ProductName);
            if (product == null)
            {
                return "NotFound";
            }

            if (product.Quantity < order.Quantity)
            {
                return "LowStock";
            }

            _productService.ReduceStock(product.Name, order.Quantity);

            order.TotalPrice = order.Quantity * product.Price;
            order.Status = "Pending";

            _orderRepository.Save(order);
            return "Success";
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }

        // Orders placed with this phone number.
        public List<Order> GetOrdersByPhone(string phone)
        {
            List<Order> result = new List<Order>();
            List<Order> orders = _orderRepository.GetAll();
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].CustomerPhone == phone)
                {
                    result.Add(orders[i]);
                }
            }
            return result;
        }

        // Stock left for a product (0 if the product does not exist).
        public int GetAvailableStock(string productName)
        {
            Product product = _productService.FindProductByName(productName);
            if (product == null)
            {
                return 0;
            }
            return product.Quantity;
        }
    }
}
