using System;

namespace Лаба_1_ПИС
{
    class FuelPrice
    {
        public string FuelType;
        public DateTime Date;
        public decimal Price;

        public void Print()
        {
            Console.WriteLine("Тип топлива: " + FuelType);
            Console.WriteLine("Дата:        " + Date.ToString("yyyy.MM.dd"));
            Console.WriteLine("Цена:        " + Price.ToString("F2"));
        }

        public string ShortInfo()
        {
            return $"{FuelType} | {Date:yyyy.MM.dd} | {Price:F2}";
        }
    }
}
