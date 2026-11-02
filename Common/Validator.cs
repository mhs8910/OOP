namespace SolarManagement
{
    // Static helper: validation rules (no object needed).
    public static class Validator
    {
        // Phone must be exactly 11 characters and all digits.
        public static bool IsValidPhone(string phone)
        {
            if (phone == null || phone.Length != 11)
            {
                return false;
            }

            for (int i = 0; i < phone.Length; i++)
            {
                if (phone[i] < '0' || phone[i] > '9')
                {
                    return false;
                }
            }
            return true;
        }

        // Capacity must be between 1 and 100 KW.
        public static bool IsValidCapacity(int capacity)
        {
            return capacity >= 1 && capacity <= 100;
        }

        // True if the text is a whole number (digits only, max 9 digits).
        public static bool IsNumber(string text)
        {
            if (text == null || text.Length == 0 || text.Length > 9)
            {
                return false;
            }

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] < '0' || text[i] > '9')
                {
                    return false;
                }
            }
            return true;
        }

        // The '|' symbol separates values inside our files,
        // so it is not allowed inside typed text.
        public static bool HasSeparator(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '|')
                {
                    return true;
                }
            }
            return false;
        }
    }
}
