using System;
using System.Globalization;

namespace ПИС2
{
    public class FuelRecord
    {
        public string Kind { get; set; } = "";
        public string FuelType { get; set; } = "";
        public DateTime Date { get; set; }
        public decimal Price { get; set; }

        public string StationName { get; set; } = "";
        public int Column { get; set; }

        public double Volume { get; set; }
        public bool IsPaid { get; set; }

        public string ShortInfo()
        {
            if (Kind == "АЗС")
                return $"АЗС: {StationName}, кол. {Column} | {FuelType} | {Date:yyyy.MM.dd} | {Price:F2}";

            if (Kind == "Поставка")
                return $"Поставка: {FuelType} | {Date:yyyy.MM.dd} | {Volume:F1} л | {(IsPaid ? "оплачено" : "не оплачено")}";

            return $"Цена: {FuelType} | {Date:yyyy.MM.dd} | {Price:F2}";
        }

        public void Print()
        {
            Console.WriteLine("Тип записи:  " + Kind);
            Console.WriteLine("Тип топлива: " + FuelType);
            Console.WriteLine("Дата:        " + Date.ToString("yyyy.MM.dd"));
            Console.WriteLine("Цена:        " + Price.ToString("F2"));

            if (Kind == "АЗС")
            {
                Console.WriteLine("АЗС:         " + StationName);
                Console.WriteLine("Колонка:     " + Column);
            }
            else if (Kind == "Поставка")
            {
                Console.WriteLine("Объём, л:    " + Volume.ToString("F1"));
                Console.WriteLine("Оплачено:    " + (IsPaid ? "да" : "нет"));
            }
        }

        public string ToLine()
        {
            if (Kind == "АЗС")
                return $"STATION;{FuelType};{Date:yyyy.MM.dd};" +
                       $"{Price.ToString(CultureInfo.InvariantCulture)};" +
                       $"{StationName};{Column}";

            if (Kind == "Поставка")
                return $"DELIVERY;{FuelType};{Date:yyyy.MM.dd};" +
                       $"{Price.ToString(CultureInfo.InvariantCulture)};" +
                       $"{Volume.ToString(CultureInfo.InvariantCulture)};" +
                       $"{(IsPaid ? 1 : 0)}";

            return $"PRICE;{FuelType};{Date:yyyy.MM.dd};" +
                   $"{Price.ToString(CultureInfo.InvariantCulture)}";
        }

        public static FuelRecord FromLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                throw new FormatException("Строка пустая.");

            string[] p = line.Split(';');

            if (p.Length == 0)
                throw new FormatException("Строка не содержит полей.");

            string type = p[0].Trim().ToUpperInvariant();
            FuelRecord r = new FuelRecord();

            switch (type)
            {
                case "PRICE":
                    if (p.Length != 4)
                        throw new FormatException(
                            $"PRICE: ожидалось 4 поля, получено {p.Length}.");

                    r.Kind = "Цена";
                    r.FuelType = p[1];
                    r.Date = ParseDate(p[2]);
                    r.Price = ParseDecimal(p[3], "Цена");
                    break;

                case "STATION":
                    if (p.Length != 6)
                        throw new FormatException(
                            $"STATION: ожидалось 6 полей, получено {p.Length}.");

                    r.Kind = "АЗС";
                    r.FuelType = p[1];
                    r.Date = ParseDate(p[2]);
                    r.Price = ParseDecimal(p[3], "Цена");
                    r.StationName = p[4];
                    r.Column = ParseInt(p[5], "Колонка");
                    break;

                case "DELIVERY":
                    if (p.Length != 6)
                        throw new FormatException(
                            $"DELIVERY: ожидалось 6 полей, получено {p.Length}.");

                    r.Kind = "Поставка";
                    r.FuelType = p[1];
                    r.Date = ParseDate(p[2]);
                    r.Price = ParseDecimal(p[3], "Цена");
                    r.Volume = ParseDouble(p[4], "Объём");
                    r.IsPaid = ParseBool01(p[5]);
                    break;

                default:
                    throw new FormatException("Неизвестный тип строки: " + p[0]);
            }

            return r;
        }

        private static DateTime ParseDate(string s)
        {
            if (DateTime.TryParseExact(s, "yyyy.MM.dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out DateTime d))
                return d;

            throw new FormatException($"Дата «{s}» не в формате гггг.мм.дд.");
        }

        private static decimal ParseDecimal(string s, string field)
        {
            if (decimal.TryParse(s, NumberStyles.Any,
                    CultureInfo.InvariantCulture, out decimal v))
                return v;

            throw new FormatException($"{field}: «{s}» не число.");
        }

        private static double ParseDouble(string s, string field)
        {
            if (double.TryParse(s, NumberStyles.Any,
                    CultureInfo.InvariantCulture, out double v))
                return v;

            throw new FormatException($"{field}: «{s}» не число.");
        }

        private static int ParseInt(string s, string field)
        {
            if (int.TryParse(s, NumberStyles.Integer,
                    CultureInfo.InvariantCulture, out int v))
                return v;

            throw new FormatException($"{field}: «{s}» не целое число.");
        }

        private static bool ParseBool01(string s)
        {
            string v = s.Trim();
            if (v == "1") return true;
            if (v == "0") return false;

            throw new FormatException($"Оплачено: ожидалось 0 или 1, получено «{s}».");
        }
    }
}