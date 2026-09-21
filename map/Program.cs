using System;
using System.IO;

namespace Лаба_1_ПИС
{
    class Program
    {
        static void Main()
        {
            //Читаем файл
            Console.Write("Файл: ");
            string path = Console.ReadLine();

            string[] lines = File.ReadAllLines(path);

            int rows = lines.Length;
            int cols = lines[0].Length;

            //Переносим в двумерный массив
            char[,] map = new char[rows, cols];
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    map[r, c] = lines[r][c];
                }
            }

            //Спрашиваем координаты
            Console.Write("x (столбец): ");
            int x = int.Parse(Console.ReadLine());

            Console.Write("y (строка): ");
            int y = int.Parse(Console.ReadLine());

            //Если # — меняем на *
            if (map[y, x] == '#')
            {
                map[y, x] = '*';
                Console.WriteLine("Заменено: # на *");
            }
            else
            {
                Console.WriteLine("Там не #.");
            }

            //Сохраняем обратно
            for (int r = 0; r < rows; r++)
            {
                char[] row = new char[cols];
                for (int c = 0; c < cols; c++)
                {
                    row[c] = map[r, c];
                }
                lines[r] = new string(row);
            }

            File.WriteAllLines(path, lines);
            Console.WriteLine("Сохранено.");
        }
    }
}