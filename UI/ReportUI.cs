using System;
using System.Collections.Generic;

namespace SolarManagement
{
    // UI LAYER: business report screen.
    public class ReportUI
    {
        private ReportService _reportService;

        public ReportUI()
        {
            _reportService = new ReportService();
        }

        public void ShowReports()
        {
            Console.WriteLine();
            Console.WriteLine("=============== BUSINESS REPORTS ===============");
            Console.WriteLine("Total Customers : " + _reportService.GetTotalCustomers());
            Console.WriteLine("Total Products  : " + _reportService.GetTotalProducts());
            Console.WriteLine("Total Orders    : " + _reportService.GetTotalOrders());
            Console.WriteLine("Total Revenue   : " + _reportService.GetTotalRevenue() + " PKR");

            Console.WriteLine();
            Console.WriteLine("---- Low Stock Alert (Qty <= 5) ----");

            List<Product> lowStock = _reportService.GetLowStockProducts();

            if (lowStock.Count == 0)
            {
                Console.WriteLine("  All products have sufficient stock.");
            }
            for (int i = 0; i < lowStock.Count; i++)
            {
                Console.WriteLine("  WARNING: " + lowStock[i].Name + " | Qty: " + lowStock[i].Quantity);
            }

            InputHelper.Pause();
        }
    }
}
