using System;
using System.Collections.Generic;

namespace Лаба_1_ПИС
{
    class Program
    {
        static List<FuelPrice> records = new List<FuelPrice>();

        static void Main()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 — Добавить запись");
                Console.WriteLine("2 — Показать все записи");
                Console.WriteLine("3 — Удалить запись");
                Console.WriteLine("4 — Выход");

                string choice = Console.ReadLine();

                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddRecord();
                        break;
                    case "2":
                        ShowAll();
                        break;
                    case "3":
                        DeleteRecord();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Нет такого пункта. Попробуй снова.");
                        break;
                }
            }
        }

        static void AddRecord()
        {
            FuelPrice fp = new FuelPrice();

            Console.Write("Тип топлива: ");
            fp.FuelType = Console.ReadLine();

            Console.Write("Дата (гггг.мм.дд): ");
            fp.Date = DateTime.ParseExact(Console.ReadLine(), "yyyy.MM.dd", null);
            Console.Write("Цена: ");
            fp.Price = decimal.Parse(Console.ReadLine().Replace('.', ','));

            records.Add(fp);
            Console.WriteLine("Запись добавлена.");
        }

        static void ShowAll()
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

        static void DeleteRecord()
        {
            if (records.Count == 0)
            {
                Console.WriteLine("Список пуст — нечего удалять.");
                return;
            }

            ShowAll();
            Console.Write("Введите номер записи для удаления: ");
            int index = int.Parse(Console.ReadLine()) - 1;

            if (index < 0 || index >= records.Count)
            {
                Console.WriteLine("Нет записи с таким номером.");
                return;
            }

            records.RemoveAt(index);
            Console.WriteLine("Запись удалена.");
        }
    }
}