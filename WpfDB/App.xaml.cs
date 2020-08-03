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
        /// <summary>
        /// Настройки учета
        /// </summary>
        public static IniValues Settings;
        
        /// <summary>
        /// Путь к файлу с настройками (жестко закодирован)
        /// </summary>
        public static string iniPath = @"settings.ini";
        
        /// <summary>
        /// Если при запуске файл с настройками существует, данные считываются в поле App.Settings
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            if (File.Exists(App.iniPath))
            {
                App.Settings = new IniValues(App.iniPath);
            }
            

        }

    }
}
