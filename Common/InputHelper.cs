using System;

namespace SolarManagement
{
    // Static helper: safe reading from the keyboard.
    public static class InputHelper
    {
        // Keeps asking until the user types something (not empty, no '|').
        public static string ReadText(string message)
        {
            Console.WriteLine(message);
            string text = Console.ReadLine();
            if (text == null)
            {
                text = "";
            }

            while (text == "" || Validator.HasSeparator(text))
            {
                Console.WriteLine("  Input cannot be empty or contain the | symbol.");
                Console.WriteLine(message);
                text = Console.ReadLine();
                if (text == null)
                {
                    text = "";
                }
            }
            return text;
        }

        // Keeps asking until the user types a whole number.
        public static int ReadNumber(string message)
        {
            Console.WriteLine(message);
            string text = Console.ReadLine();

            while (!Validator.IsNumber(text))
            {
                Console.WriteLine("  Invalid input. Please enter a whole number.");
                Console.WriteLine(message);
                text = Console.ReadLine();
            }
            return int.Parse(text);
        }

        // Keeps asking until the number is at least 1.
        public static int ReadPositiveNumber(string message, string errorMessage)
        {
            int value = ReadNumber(message);
            while (value < 1)
            {
                Console.WriteLine("  " + errorMessage);
                value = ReadNumber(message);
            }
            return value;
        }

        // Keeps asking until the phone number is valid.
        public static string ReadPhone(string message)
        {
            string phone = ReadText(message);
            while (!Validator.IsValidPhone(phone))
            {
                Console.WriteLine("  Invalid phone number. It must be exactly 11 digits.");
                phone = ReadText(message);
            }
            return phone;
        }

        // Keeps asking until the capacity is between 1 and 100.
        public static int ReadCapacity(string message)
        {
            int capacity = ReadNumber(message);
            while (!Validator.IsValidCapacity(capacity))
            {
                Console.WriteLine("  Invalid capacity. Enter a value between 1 and 100.");
                capacity = ReadNumber(message);
            }
            return capacity;
        }

        // Waits for the user to press Enter.
        public static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
