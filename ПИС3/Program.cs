using System;

namespace ПИС3;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Igra igra = new Igra();
        igra.Nachat();

        while (true)
        {
            Pokazat(igra);
            Console.Write("> ");
            string? vvod = Console.ReadLine()?.Trim().ToLower();
            if (vvod == null) break;

            if (vvod == "q") break;

            else if (vvod == "d")
                igra.Vzyat();

            else if (vvod == "w")
            {
                if (igra.Sbros.Count == 0) { Console.WriteLine("Сброс пуст."); continue; }
                Console.Write("в столбец 1-7: ");
                int col = Convert.ToInt32(Console.ReadLine()) - 1;
                if (!igra.SbrosVStolbec(col)) Console.WriteLine("Не подходит.");
            }

            else if (vvod == "f")
            {
                Console.Write("откуда (w = сброс, t = столбец): ");
                string? otkuda = Console.ReadLine()?.Trim().ToLower();

                if (otkuda == "w")
                {
                    if (!igra.VDom(0, -1)) Console.WriteLine("Не подходит.");
                }
                else if (otkuda == "t")
                {
                    Console.Write("столбец 1-7: ");
                    int col = Convert.ToInt32(Console.ReadLine()) - 1;
                    if (!igra.VDom(1, col)) Console.WriteLine("Не подходит.");
                }
                else Console.WriteLine("Не понял откуда.");
            }

            else if (vvod == "t")
            {
                Console.Write("из столбца 1-7: ");
                int a = Convert.ToInt32(Console.ReadLine()) - 1;
                Console.Write("в столбец 1-7: ");
                int b = Convert.ToInt32(Console.ReadLine()) - 1;
                if (!igra.StolbecVStolbec(a, b)) Console.WriteLine("Не подходит.");
            }

            else Console.WriteLine("Неизвестная команда.");

            if (igra.VsegoVDomah() == 52)
            {
                Pokazat(igra);
                Console.WriteLine("Собрано! Победа.");
                break;
            }
        }
    }

    static void Pokazat(Igra igra)
    {
        Console.WriteLine();
        Console.Write("Дома: ");
        for (int i = 0; i < 4; i++)
        {
            if (igra.Doma[i].Count == 0) Console.Write("[..] ");
            else Console.Write("[" + igra.Doma[i][igra.Doma[i].Count - 1] + "] ");
        }
        Console.WriteLine();

        Console.Write("Колода: " + igra.Koloda.Count + "   Сброс: ");
        int n = Math.Min(3, igra.Sbros.Count);
        for (int i = igra.Sbros.Count - n; i < igra.Sbros.Count; i++)
            Console.Write("[" + igra.Sbros[i] + "] ");
        Console.WriteLine();
        Console.WriteLine();

        int max = 0;
        foreach (var s in igra.Stolbcy)
            if (s.Count > max) max = s.Count;

        for (int r = 0; r < max; r++)
        {
            for (int c = 0; c < 7; c++)
            {
                if (r < igra.Stolbcy[c].Count)
                    Console.Write(igra.Stolbcy[c][r].ToString().PadRight(5));
                else
                    Console.Write("     ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();

        Console.WriteLine("Управление:");
        Console.WriteLine("  d - взять из колоды (или перевернуть сброс)");
        Console.WriteLine("  w - сброс -> столбец");
        Console.WriteLine("  f - в дом (w = из сброса, t = из столбца)");
        Console.WriteLine("  t - столбец -> столбец (переносится одна карта)");
        Console.WriteLine("  q - выход");
        Console.WriteLine();
    }
}