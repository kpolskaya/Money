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

            for (int i = 0; i <= db.Count; i++)
            {
                string[] content = db.ToText(i);
                foreach (var item in content)
                {
                    Console.Write($"{item,14}");
                }
                Console.WriteLine();
            }

            Console.Write("Какую запись вы хотите удалить? Введите номер:");
            int num = Convert.ToInt32(Console.ReadLine());
            db.Delete(num);
            Console.WriteLine();

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
