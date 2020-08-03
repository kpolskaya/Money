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
    public struct IniValues
    {
        /// <summary>
        /// Свойство Дата начала учета
        /// </summary>
        public DateTime StartingDate { get; }

        /// <summary>
        /// Свойство Начальный баланс
        /// </summary>
        public double Balance { get; }
        
        /// <summary>
        /// Свойство Счета
        /// </summary>
        public string Accounts { get; }

        /// <summary>
        /// Свойство Категории прихода
        /// </summary>
        public string InCategories { get; }

        /// <summary>
        /// Свойство Категории расхода
        /// </summary>
        public string OutCategories { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="StartingDate">Дата начала учета</param>
        /// <param name="Balance">Начальный баланс</param>
        /// <param name="Accounts">Счета</param>
        /// <param name="InCategories">Категории прихода</param>
        /// <param name="OutCategories">Категории расхода</param>
        public IniValues(DateTime StartingDate, double Balance, string Accounts, string InCategories, string OutCategories)
        {
            this.StartingDate = StartingDate;
            this.Balance = Balance;
            this.Accounts = Accounts;
            this.InCategories = InCategories;
            this.OutCategories = OutCategories;
            
        }

        /// <summary>
        /// Конструктор для загрузки данных из файла
        /// </summary>
        /// <param name="IniPath">Путь к файлу с настройками</param>
        public IniValues(string IniPath)
        {
            this.StartingDate = Convert.ToDateTime($"01.01.{DateTime.Now : yyyy}");
            this.Balance = 0;
            this.Accounts = "не задано";
            this.InCategories = "не задано";
            this.OutCategories ="не задано";

            if(File.Exists(IniPath))
            {
                char[] seps = new char[] {'='};
                using (StreamReader iniStream = new StreamReader(IniPath))
                {

                    while (!iniStream.EndOfStream)
                    {
                        string[] args = iniStream.ReadLine().Split(seps, StringSplitOptions.RemoveEmptyEntries);

                        switch (args[0])
                        {
                            case "balance":
                                this.Balance = Convert.ToDouble(args[1].TrimStart(' '));
                                break;
                            case "date":
                                this.StartingDate = Convert.ToDateTime(args[1].TrimStart(' '));
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

        /// <summary>
        /// Сохраняет настройки в файл
        /// </summary>
        /// <param name="IniPath">Путь к файлу с настройками</param>
        public void Save(string IniPath)
        {
            using (StreamWriter iniFile = new StreamWriter(IniPath, false))
            {
                iniFile.WriteLine($"date={this.StartingDate:dd.MM.yyyy}");
                iniFile.WriteLine($"balance={this.Balance}");                     
                iniFile.WriteLine($"accounts={this.Accounts}");
                iniFile.WriteLine($"inCategories={this.InCategories}");
                iniFile.WriteLine($"outCategories={this.OutCategories}");

            }
        }
    }
}
