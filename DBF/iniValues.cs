using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DBF
{
    /// <summary>
    /// Структура для начальных параметров ведения учета
    /// </summary>
    public class iniValues
    {
        public DateTime StartingDate { get; }

        public double Balance { get; }

        public string Accounts { get; }

        public string InCategories { get; }

        public string OutCategories { get; }

        //public string IniPath { get; }

        public iniValues(DateTime StartingDate, double Balance, string Accounts, string InCategories, string OutCategories)
        {
            this.StartingDate = StartingDate;
            this.Balance = Balance;
            this.Accounts = Accounts;
            this.InCategories = InCategories;
            this.OutCategories = OutCategories;
            //this.IniPath = IniPath;

        }

        public iniValues(string IniPath)
        {
            this.StartingDate = Convert.ToDateTime($"01.01.{DateTime.Now : yyyy}");
            this.Balance = 0;
            this.Accounts = "";
            this.InCategories = "";
            this.OutCategories = "";

            if(File.Exists(IniPath))
            {
                char[] seps = new char[] {' ', '=' };
                using (StreamReader iniStream = new StreamReader(IniPath))
                {

                    while (!iniStream.EndOfStream)
                    {
                        string[] args = iniStream.ReadLine().Split(seps, System.StringSplitOptions.RemoveEmptyEntries);

                        switch (args[0])
                        {
                            case "balance":
                                this.Balance = Convert.ToDouble(args[1]);
                                break;
                            case "date":
                                this.StartingDate = Convert.ToDateTime(args[1]);
                                break;
                            case "accounts":
                                this.Accounts = args[1];
                                break;
                            case "inCategories":
                                this.InCategories = args[1];
                                break;
                            case "outCategories":
                                this.OutCategories = args[1];
                                break;
                            default:
                                break;
                        }

                    }
                }
            }
        }

        public void Save(string IniPath)
        {
            using (StreamWriter iniFile = new StreamWriter(IniPath, false))
            {
                iniFile.WriteLine($"date = {this.StartingDate: dd.MM.yyyy}");
                iniFile.WriteLine($"balance = {this.Balance}");                     // нужно проверить формат записи, чтобы потом не было проблем с парсингом обратно!
                iniFile.WriteLine($"accounts = {this.Accounts}");
                iniFile.WriteLine($"inCategories = {this.InCategories}");
                iniFile.WriteLine($"outCategories = {this.OutCategories}");

            }
        }
    }
}
