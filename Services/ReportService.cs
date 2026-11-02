using System.Collections.Generic;

namespace SolarManagement
{
    // BUSINESS LOGIC LAYER: numbers for the business report.
    public class ReportService
    {
        private CustomerService _customerService;
        private ProductService _productService;
        private OrderService _orderService;

        public ReportService()
        {
            _customerService = new CustomerService();
            _productService = new ProductService();
            _orderService = new OrderService();
        }

        public int GetTotalCustomers()
        {
            return _customerService.GetAllCustomers().Count;
        }

        public int GetTotalProducts()
        {
            return _productService.GetAllProducts().Count;
        }

        public int GetTotalOrders()
        {
            return _orderService.GetAllOrders().Count;
        }

        // Revenue = sum of all customer project prices.
        public int GetTotalRevenue()
        {
            int total = 0;
            List<Customer> customers = _customerService.GetAllCustomers();
            for (int i = 0; i < customers.Count; i++)
            {
                total = total + customers[i].ProjectPrice;
            }
            return total;
        }

        // Products with quantity 5 or less.
        public List<Product> GetLowStockProducts()
        {
            return _productService.GetLowStockProducts(5);
        }
    }
}
