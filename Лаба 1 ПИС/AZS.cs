using System;
using System.Collections.Generic;

namespace Лаба_1_ПИС
{
    class AZS
    {
        private List<FuelPrice> records = new List<FuelPrice>();

        public int Count
        {
            get { return records.Count; }
        }

        public void Add(FuelPrice fp)
        {
            records.Add(fp);
        }

        public void ShowAll()
        {
            if (records.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            Console.WriteLine("Все записи:");
            for (int i = 0; i < records.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {records[i].ShortInfo()}");
            }
        }

        public bool DeleteAt(int number)
        {
            int index = number - 1;

            if (index < 0 || index >= records.Count)
                return false;

            records.RemoveAt(index);
            return true;
        }

        public FuelPrice GetAt(int number)
        {
            int index = number - 1;
            if (index < 0 || index >= records.Count)
                return null;
            return records[index];
        }
    }
}
