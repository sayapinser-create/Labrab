using System;
using System.Collections.Generic;

namespace ПИС2
{
    public class Menu
    {
        public class Item
        {
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
            public Action ActionToRun { get; set; }

            public Item(string name, string description, Action actionToRun)
            {
                Name = name;
                Description = description;
                ActionToRun = actionToRun;
            }
        }

        private List<Item> items = new List<Item>();

        public void Add(string name, string description, Action action)
        {
            items.Add(new Item(name, description, action));
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("МЕНЮ");

                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine($"{i + 1} — {items[i].Name} ({items[i].Description})");
                }

                Console.Write("Выбор: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                if (!int.TryParse(choice, out int id))
                {
                    Console.WriteLine("Введите число.");
                    continue;
                }

                if (id == items.Count) break;

                if (id < 1 || id > items.Count)
                {
                    Console.WriteLine("Нет такого пункта.");
                    continue;
                }

                Item selected = items[id - 1];

                if (selected.ActionToRun != null)
                    selected.ActionToRun.Invoke();
                else
                    Console.WriteLine("У этого пункта нет действия.");
            }
        }
    }
}