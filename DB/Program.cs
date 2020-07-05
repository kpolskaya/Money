using System;
using System.Xml.Linq;

namespace DB
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"data.csv";
            Repository db = new Repository(path);
            Console.WriteLine($"Файл {path} открыт успешно.");
            db.Save();
            Console.WriteLine($"База выгружена. Файл {path} записан успешно.");
            Console.ReadKey();
        }
    }
}
