using System;
using System.Collections.Generic;
using System.IO;
using ПИС2;

namespace ПИС2
{
    class Program
    {
        static List<FuelRecord> records = new List<FuelRecord>();

        static void Main()
        {
            Menu menu = new Menu();

            menu.Add("Добавить запись", "Добавляет топливо", AddRecord);
            menu.Add("Показать все", "Выводит список", ShowAll);
            menu.Add("Удалить запись", "Удаляет запись", DeleteRecord);
            menu.Add("Выход", "Завершает программу", null);
            menu.Run();

            Console.WriteLine("Программа завершена.");
        }

        static void AddRecord()
        {
            Console.WriteLine("1 - Обычная цена, 2 - Цена на АЗС, 3 - Поставка");
            Console.Write("Тип: ");
            string t = Console.ReadLine();

            FuelRecord r = new FuelRecord();
            r.FuelType = InputHelper.ReadString("Тип топлива: ");
            r.Date = InputHelper.ReadDate("Дата (гггг.мм.дд): ");
            r.Price = InputHelper.ReadDecimal("Цена: ");

            if (t == "1")
            {
                r.Kind = "Цена";
            }
            else if (t == "2")
            {
                r.Kind = "АЗС";
                r.StationName = InputHelper.ReadString("Название АЗС: ");
                r.Column = InputHelper.ReadInt("Номер колонки: ");
            }
            else if (t == "3")
            {
                r.Kind = "Поставка";
                r.Volume = InputHelper.ReadDouble("Объём, л: ");
                r.IsPaid = InputHelper.ReadBool("Оплачено?");
            }
            else
            {
                Console.WriteLine("Неизвестный тип.");
                return;
            }

            records.Add(r);
            Logger.LogData($"ADD; {r.ToLine()}");
            Console.WriteLine("Запись добавлена.");
        }

        static void ShowAll()
        {
            if (records.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            for (int i = 0; i < records.Count; i++)
                Console.WriteLine($"{i + 1}. {records[i].ShortInfo()}");
        }

        static void DeleteRecord()
        {
            if (records.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            ShowAll();
            int n = InputHelper.ReadInt("Номер для удаления: ");

            if (n < 1 || n > records.Count)
            {
                Console.WriteLine("Нет такой записи.");
                return;
            }

            records.RemoveAt(n - 1);
            Console.WriteLine("Удалено.");
        }

        static void SaveToFile()
        {
            string path = InputHelper.ReadString("Путь к файлу: ");
            try
            {
                var lines = new List<string>();
                foreach (FuelRecord r in records)
                    lines.Add(r.ToLine());

                File.WriteAllLines(path, lines);
                Console.WriteLine("Файл сохранён.");
            }
            catch
            {
                Console.WriteLine("Ошибка записи.");
            }
        }
    }
}