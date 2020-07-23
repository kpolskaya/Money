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
        protected override void OnStartup(StartupEventArgs e)
        {
            string iniPath = @"settings.ini";
            
            iniValues Settings;
            if (File.Exists(iniPath))
            { 
                Settings = new iniValues(@"settings.ini");
            }

            else
            {
                DateTime date = Convert.ToDateTime("02.01.2020");
                double balance = 9999.99;
                string accounts = "карта, наличные, кредит";
                string inCats = "зарплата, подработка, проценты, благотворительность, воровство, подарки/находки, кредиты, прочее";
                string outCats = "продукты, дом, коммунальные, животные, отдых, кредиты, инвестиции, одежда, прочее";
                Settings = new iniValues(date, balance, accounts, inCats, outCats);
                Settings.Save(@"settings.ini");
                MessageBox.Show($@"Сформирован файл начальных настроек учета по адресу: {Directory.GetCurrentDirectory ()}\{iniPath}");
                
            }

        }

    }
}
