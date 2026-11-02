using System.Collections.Generic;

namespace SolarManagement
{
    // BUSINESS LOGIC LAYER: rules for inventory.
    public class ProductService
    {
        private ProductRepository _repository;

        public ProductService()
        {
            _repository = new ProductRepository();
        }

        // Returns false if the inventory is full (1000 products).
        public bool AddProduct(Product product)
        {
            if (_repository.GetAll().Count >= 1000)
            {
                return false;
            }
            _repository.Save(product);
            return true;
        }

        public List<Product> GetAllProducts()
        {
            return _repository.GetAll();
        }

        // Returns the product with this exact name, or null.
        public Product FindProductByName(string name)
        {
            List<Product> products = _repository.GetAll();
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Name == name)
                {
                    return products[i];
                }
            }
            return null;
        }

        // Products with quantity greater than the limit.
        public List<Product> GetProductsAbove(int limit)
        {
            List<Product> result = new List<Product>();
            List<Product> products = _repository.GetAll();
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Quantity > limit)
                {
                    result.Add(products[i]);
                }
            }
            return result;
        }

        // Products with quantity less than or equal to the limit.
        public List<Product> GetLowStockProducts(int limit)
        {
            List<Product> result = new List<Product>();
            List<Product> products = _repository.GetAll();
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Quantity <= limit)
                {
                    result.Add(products[i]);
                }
            }
            return result;
        }

        // Reduces the stock of one product and saves the inventory file.
        public void ReduceStock(string productName, int quantity)
        {
            List<Product> products = _repository.GetAll();
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Name == productName)
                {
                    products[i].Quantity = products[i].Quantity - quantity;
                }
            }
            _repository.SaveAll(products);
        }
    }
}
