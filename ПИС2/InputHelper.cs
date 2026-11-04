using System;
using System.Globalization;

namespace ПИС2
{
    public static class InputHelper
    {
        public static string ReadString(string prompt)
        {
            Console.Write(prompt);
            string s = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(s))
            {
                Console.WriteLine("Ошибка ввода: пустая строка.");
                return "";
            }

            return s;
        }

        public static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine();

                if (int.TryParse(s, out int v))
                    return v;

                Console.WriteLine($"Ошибка ввода: «{s}» не целое число. Повторите.");
            }
        }

        public static decimal ReadDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine()?.Replace(',', '.');

                if (decimal.TryParse(s, NumberStyles.Any,
                        CultureInfo.InvariantCulture, out decimal v))
                    return v;

                Console.WriteLine($"Ошибка ввода: «{s}» не число. Повторите.");
            }
        }

        public static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine()?.Replace(',', '.');

                if (double.TryParse(s, NumberStyles.Any,
                        CultureInfo.InvariantCulture, out double v))
                    return v;

                Console.WriteLine($"Ошибка ввода: «{s}» не число. Повторите.");
            }
        }

        public static DateTime ReadDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine();

                if (DateTime.TryParseExact(s, "yyyy.MM.dd",
                        CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out DateTime d))
                    return d;

                Console.WriteLine($"Ошибка ввода: «{s}» не дата гггг.мм.дд. Повторите.");
            }
        }

        public static bool ReadBool(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + " (y/n): ");
                string s = Console.ReadLine()?.Trim().ToLower();

                if (s == "y" || s == "yes" || s == "1" || s == "д") return true;
                if (s == "n" || s == "no" || s == "0" || s == "н") return false;

                Console.WriteLine($"Ошибка ввода: «{s}» не да/нет. Повторите.");
            }
        }
    }
}