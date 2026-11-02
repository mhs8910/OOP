using System;

namespace SolarManagement
{
    // UI LAYER: admin solar setup calculator.
    public class CalculatorUI
    {
        private SolarCalculatorService _calculator;

        public CalculatorUI()
        {
            _calculator = new SolarCalculatorService();
        }

        public void ShowCalculator()
        {
            InputHelper.ClearScreen();
            Console.WriteLine();
            Console.WriteLine("========== SOLAR SETUP CALCULATOR ==========");

            int capacity = InputHelper.ReadCapacity("Enter Solar System Capacity (1-100 KW):");

            Console.WriteLine();
            Console.WriteLine("---- Calculation Results ----");
            Console.WriteLine("Panels Needed    : " + _calculator.CalculatePanels(capacity));
            Console.WriteLine("Inverters Needed : " + _calculator.CalculateInverters(capacity));
            Console.WriteLine("Batteries Needed : " + _calculator.CalculateBatteries(capacity));
            Console.WriteLine("Estimated Price  : " + _calculator.CalculatePrice(capacity) + " PKR");

            InputHelper.Pause();
        }
    }
}
