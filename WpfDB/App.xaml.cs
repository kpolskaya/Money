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
        public static string iniPath = @"settings.ini";
        protected override void OnStartup(StartupEventArgs e)
        {
            if (File.Exists(App.iniPath))
            {
                App.Settings = new IniValues(App.iniPath);
            }
            else
            {

                Window2 window = new Window2();
                window.ShowDialog();
                //    DateTime date = Convert.ToDateTime("02.01.2020");
                //    double balance = 9999.99;
                //    string accs = ",карта,наличные,кредит";
                //    string catsI = ",зарплата,подработка,проценты,воровство,подарки и находки,кредиты,прочее,";
                //    string catsE = ",продукты,дом,автомобиль,коммунальные платежи,животные,отдых,кредиты,инвестиции,одежда,воровство,благотворительность,прочее";


            }

        }

    }
}
