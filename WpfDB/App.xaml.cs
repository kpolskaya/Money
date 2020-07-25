using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DBF;

namespace WpfDB
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IniValues Settings;
        protected override void OnStartup(StartupEventArgs e)
        {
            string iniPath = @"settings.ini";
            
            
            if (File.Exists(iniPath))
            { 
                Settings = new IniValues(@"settings.ini");
            }

            else
            {
                DateTime date = Convert.ToDateTime("02.01.2020");
                double balance = 9999.99;
                string accounts = "карта,наличные,кредит";
                string inCats = "зарплата,подработка,проценты,воровство,подарки и находки,кредиты,прочее";
                string outCats = "продукты,дом,автомобиль,коммунальные платежи,животные,отдых,кредиты,инвестиции,одежда,воровство,благотворительность,прочее";
                Settings = new IniValues(date, balance, accounts, inCats, outCats);
                Settings.Save(@"settings.ini");
                MessageBox.Show($@"Сформирован файл начальных настроек учета по адресу: {Directory.GetCurrentDirectory ()}\{iniPath}");
                
            }
                       
        }

    }
}
