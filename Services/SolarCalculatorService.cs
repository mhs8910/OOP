namespace SolarManagement
{
    // BUSINESS LOGIC LAYER: solar size and price calculations.
    public class SolarCalculatorService
    {
        // 1 panel = 550W, so panels = ceil(capacityKW / 0.55)
        public int CalculatePanels(int capacityKW)
        {
            return (capacityKW * 100 + 54) / 55;
        }

        // 1 inverter = 5KW, so inverters = ceil(capacityKW / 5)
        public int CalculateInverters(int capacityKW)
        {
            return (capacityKW + 4) / 5;
        }

        // 1 battery per KW
        public int CalculateBatteries(int capacityKW)
        {
            return capacityKW;
        }

        // Panel = 550 PKR, Inverter = 5000 PKR, Battery = 10000 PKR
        public int CalculatePrice(int capacityKW)
        {
            return (CalculatePanels(capacityKW) * 550) +
                   (CalculateInverters(capacityKW) * 5000) +
                   (CalculateBatteries(capacityKW) * 10000);
        }
    }
}
