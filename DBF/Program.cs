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
            string iniPath = @"settings.ini";
            IniValues Settings;
            if (File.Exists(iniPath))
                Settings = new IniValues(@"settings.ini");
            else
            {
                DateTime date = Convert.ToDateTime("02.01.2020");
                double balance = 9999.99;
                string accounts = "наличные,карта,другое";
                string inCats = "зарплата,подарки,находки,украдено,кредиты,неизвестно";
                string outCats = "продукты,дом,машина,животные,развлечения,инвестиции,электроника,обучение,украдено,потери,комиссии,налоги,здоровье,кредиты,неизвестно";
                Settings = new IniValues(date, balance, accounts, inCats, outCats);
                Settings.Save(@"settings.ini");
            }
            
            

            Repository db = new Repository(path);
            double newBalance = 9999.99;
            double income = 0;
            double outflow = 0;

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

            Record[] lastRecords = db.FilteredList(new Template(Convert.ToDateTime("01.01.0001"), Convert.ToDateTime("31.07.2022"), (sbyte)0, "", ""));
            db.Save();

            foreach (var item in lastRecords)
            {
                if (item.OpType > 0)
                    income += item.OpSum;
                else
                    outflow += item.OpSum;

                newBalance += item.Sum;
                Console.WriteLine("{0};{1};{2};{3};{4};{5};{6};{7}",
                                        item.RecNumber,
                                        item.CrDate,
                                        item.OpDate,
                                        item.OpType,
                                        item.Sum,
                                        item.Account,
                                        item.Category,
                                        item.Note);
            }

            Console.WriteLine($"initial balance = {Settings.Balance}; total income = {income}; total outflow = {outflow}");
            Console.WriteLine($"Final balance = {db.Balance}, but counted balance is {newBalance} what makes a difference of {db.Balance - newBalance : 0.00}");
            Console.WriteLine($"Starting date is {db.StartingDate}");
            Console.WriteLine($"Last saving at {db.LastSavingTime}");

            //do
            //{ 
            //    Console.Write("Какую запись вы хотите удалить? Введите номер:");
            //    int num = Convert.ToInt32(Console.ReadLine());
            //    db.Delete(num);
            //    Console.WriteLine();

            //    Console.WriteLine("Давайте отфильтруем!");
            //    Console.Write("Начальная дата: ");
            //    DateTime startDate = Convert.ToDateTime(Console.ReadLine());

            //    Console.Write("Конечная дата: ");
            //    DateTime endDate = Convert.ToDateTime(Console.ReadLine());

            //    Console.Write("Тип операции (-1, 1 или 0 если все): ");
            //    sbyte type = Convert.ToSByte(Console.ReadLine());

            //    Console.Write("счет или  <Enter> если все: ");
            //    string acc = Console.ReadLine();

            //    Console.Write("категория или  <Enter> если все: ");
            //    string cat = Console.ReadLine();

            //    //Template filter = new Template(startDate, endDate, type, acc, cat);
            //    //int[] selected = db.Select(filter);

            //    Record[] lastRecords = db.FilteredList(new Template(DateTime.Now, DateTime.Now, (sbyte)0, "", ""));

            //    //for (int i = 0; i < selected.Length; i++)
            //    //{
            //    //    string[] content = db.ToText(selected[i]);
            //    //    if (content == null) continue;
            //    //    foreach (var item in content)
            //    //    {
            //    //        Console.Write($"{item,18}");
            //    //    }
            //    //    Console.WriteLine();
            //    //}




            //} while (true);

            //Console.WriteLine("Введите новую запись:");
            //Console.Write("Дата операции (dd.mm.yyyy):");
            //DateTime newDate = Convert.ToDateTime(Console.ReadLine());

            //Console.Write("Вид операции (доход: 1 / расход: -1):");
            //sbyte newType = Convert.ToSByte(Console.ReadLine());

            //Console.Write("Сумма:");
            //double newSum = Convert.ToDouble(Console.ReadLine());

            //Console.Write("Счет:");
            //string newAcc = (Console.ReadLine());

            //Console.Write("Категория дохода/расхода:");
            //string newCat = (Console.ReadLine());

            //Console.Write("Примечание:");
            //string newNot = (Console.ReadLine());

            //db.Add(new Record(newDate, newType, newSum, newAcc, newCat, newNot));


            //db.Save();
            //Console.WriteLine($"База выгружена. Файл {path} записан успешно.");
            Console.ReadKey();
        }
    }
}
