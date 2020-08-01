using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DBF;

namespace WpfDB
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
       
        public Window2()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            DateTime date = Convert.ToDateTime(Convert.ToDateTime(bdate.SelectedDate.Value.Date.ToShortDateString()));
            double balance = Convert.ToDouble(bsaldo.Text);
            string accs ="";
            string catsI ="";
            string catsE ="";


            //string x = (string)acclist.Items[1];
            //

            for (int i = 1; i < 11; i++)
            {
                string x = (acclist.Items.GetItemAt(i).ToString());
                if (x != $"System.Windows.Controls.ListBoxItem")
                {
                    accs = accs + x.Substring(37) +',';
                }
                
            }

            for (int i = 1; i < 11; i++)
            {
                string x = (catsElist.Items.GetItemAt(i).ToString());
                if (x != $"System.Windows.Controls.ListBoxItem")
                {
                    catsE = catsE + x.Substring(37) + ',';
                    
                }
                
            }

            for (int i = 1; i < 11; i++)
            {
                string x = (catsIlist.Items.GetItemAt(i).ToString());
                if (x != $"System.Windows.Controls.ListBoxItem")
                {
                    catsI = catsI + x.Substring(37) + ',';
                   
                }
                
            }

            App.Settings = new IniValues(date, balance, accs, catsI, catsE);
            App.Settings.Save(App.iniPath);
            MessageBox.Show($@"Сформирован файл начальных настроек учета по адресу: {Directory.GetCurrentDirectory()}\{App.iniPath}");

            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Now;
            double balance = 10000;
            string accs = "кошелек, узелок. правый карман, левый карман,";
            string catsI = "зарплата,пособие,паперть,";
            string catsE = "еда,лекарства,питомцы,остальное,";

            App.Settings = new IniValues(date, balance, accs, catsI, catsE);
            App.Settings.Save(App.iniPath);
            MessageBox.Show($@"Сформирован файл начальных настроек учета по адресу: {Directory.GetCurrentDirectory()}\{App.iniPath}");

            this.Close();
        }

       
    }
}
