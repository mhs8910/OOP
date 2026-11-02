using System;
using System.Collections.Generic;

namespace SolarManagement
{
    // UI LAYER: admin screens for inventory.
    public class InventoryUI
    {
        private ProductService _productService;

        public InventoryUI()
        {
            _productService = new ProductService();
        }

        public void ShowInventoryMenu()
        {
            bool running = true;

            while (running)
            {
                InputHelper.ClearScreen();
                Console.WriteLine();
                Console.WriteLine("=============== INVENTORY MANAGEMENT ===============");
                Console.WriteLine("1 - Add Product");
                Console.WriteLine("2 - View All Products");
                Console.WriteLine("3 - Back to Admin Menu");
                Console.WriteLine("Enter Your Choice:");

                string choice = Console.ReadLine();

                if (choice == "1") AddProductUI();
                else if (choice == "2") ViewProductsUI();
                else if (choice == "3") running = false;
                else
                {
                    Console.WriteLine("Invalid Input! Please enter 1, 2, or 3.");
                    InputHelper.Pause();
                }
            }
        }

        private void AddProductUI()
        {
            InputHelper.ClearScreen();
            Console.WriteLine();
            Console.WriteLine("========== ADD NEW PRODUCT ==========");

            string name = InputHelper.ReadText("Enter Product Name:");
            string description = InputHelper.ReadText("Enter Product Description:");
            int quantity = InputHelper.ReadPositiveNumber("Enter Product Quantity (min 1):",
                                                          "Quantity must be at least 1.");
            int price = InputHelper.ReadPositiveNumber("Enter Product Price (PKR, min 1):",
                                                       "Price must be greater than 0.");

            Product product = new Product(name, description, quantity, price);

            Console.WriteLine();
            if (_productService.AddProduct(product))
            {
                Console.WriteLine("Product Added Successfully!");
            }
            else
            {
                Console.WriteLine("A product with this name may already exist, or inventory is full.");
            }

            InputHelper.Pause();
        }

        private void ViewProductsUI()
        {
            InputHelper.ClearScreen();
            Console.WriteLine();
            Console.WriteLine("================ INVENTORY ================");

            List<Product> products = _productService.GetAllProducts();

            if (products.Count == 0)
            {
                Console.WriteLine("No products in inventory.");
            }

            for (int i = 0; i < products.Count; i++)
            {
                Product p = products[i];
                Console.WriteLine("Product #" + (i + 1));
                Console.WriteLine("Name        : " + p.Name);
                Console.WriteLine("Description : " + p.Description);
                Console.WriteLine("Quantity    : " + p.Quantity);
                Console.WriteLine("Price       : " + p.Price + " PKR");
                Console.WriteLine("--------------------------------------------------");
            }

            // Quick overview: products with quantity above 20
            Console.WriteLine();
            Console.WriteLine("Products with quantity > 20:");
            List<Product> bigStock = _productService.GetProductsAbove(20);

            if (bigStock.Count == 0)
            {
                Console.WriteLine("  None.");
            }
            for (int i = 0; i < bigStock.Count; i++)
            {
                Console.WriteLine("  - " + bigStock[i].Name + " (Qty: " + bigStock[i].Quantity + ")");
            }

            InputHelper.Pause();
        }
    }
}
