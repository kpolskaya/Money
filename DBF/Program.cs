using System;
using System.IO;
using System.Text;


namespace DBF
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"data.csv";
            Repository db = new Repository(path);
            Console.WriteLine($"Файл {path} открыт успешно.");

                      
                for (int i = 0; i < db.Count; i++)
                {
                    string[] content = db.ToText(i);
                    foreach (var item in content)
                    {
                        Console.Write($"{item,18}");
                    }
                    Console.WriteLine();
                }
            do
            { 
                Console.Write("Какую запись вы хотите удалить? Введите номер:");
                int num = Convert.ToInt32(Console.ReadLine());
                db.Delete(num);
                Console.WriteLine();

                Console.WriteLine("Давайте отфильтруем!");
                Console.Write("Начальная дата: ");
                DateTime startDate = Convert.ToDateTime(Console.ReadLine());

                Console.Write("Конечная дата: ");
                DateTime endDate = Convert.ToDateTime(Console.ReadLine());

                Console.Write("Тип операции (-1, 1 или 0 если все): ");
                sbyte type = Convert.ToSByte(Console.ReadLine());

                Console.Write("счет или  <Enter> если все: ");
                string acc = Console.ReadLine();

                Console.Write("категория или  <Enter> если все: ");
                string cat = Console.ReadLine();

                Template filter = new Template(startDate, endDate, type, acc, cat);
                int[] selected = db.Select(filter);

                for (int i = 0; i < selected.Length; i++)
                {
                    string[] content = db.ToText(selected[i]);
                    if (content == null) continue;
                    foreach (var item in content)
                    {
                        Console.Write($"{item,18}");
                    }
                    Console.WriteLine();
                }

            } while (true);

            Console.WriteLine("Введите новую запись:");
            Console.Write("Дата операции (dd.mm.yyyy):");
            DateTime newDate = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Вид операции (доход: 1 / расход: -1):");
            sbyte newType = Convert.ToSByte(Console.ReadLine());

            Console.Write("Сумма:");
            double newSum = Convert.ToDouble(Console.ReadLine());

            Console.Write("Счет:");
            string newAcc = (Console.ReadLine());

            Console.Write("Категория дохода/расхода:");
            string newCat = (Console.ReadLine());

            Console.Write("Примечание:");
            string newNot = (Console.ReadLine());

            db.Add(new Record(newDate, newType, newSum, newAcc, newCat, newNot));


            db.Save();
            Console.WriteLine($"База выгружена. Файл {path} записан успешно.");
            Console.ReadKey();
        }
    }
}
