using System;
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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string acc;
        DateTime opdate;
        string[] accs = new string[] { "карта", "наличные", "кредит" };
        string[] cats = new string[] { "продукты", "дом", "коммунальные платежи", "животные", "отдых", "погашение кредита", "инвестиции", "одежда и обувь", "прочее" };
        string[] catsP = new string[] { "зарплата", "подработка", "проценты от инвестиций", "благотворительность", "воровство", "подарки и находки", "прочее" };
        string[] all = new string[] { "" };

        public Window1()
        {
            InitializeComponent();
            opdate = MainWindow.opR.OpDate;
            acc = MainWindow.opR.Account;
            dp1.SelectedDate = opdate;
            sumR.Text = Convert.ToString(MainWindow.opR.OpSum);
            catR.SelectedItem = MainWindow.opR.Category;
           // Cat.Text = MainWindow.opR.Category;
            noteR.Text =  MainWindow.opR.Note;
            //acc.ItemsSource = accs;
            //accP.ItemsSource = accs;
            accR.ItemsSource = accs;
            if (MainWindow.opR.OpType == 1)
            {
                catR.ItemsSource = catsP;
            }
            else
            {
                catR.ItemsSource = cats;
            }
            
            accR.SelectedItem = MainWindow.opR.Account;
            //catP.ItemsSource = catsP;
            //catR.ItemsSource = all.Concat(cats.Concat(catsP));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.db.Delete(MainWindow.opR.RecNumber);
            MainWindow.db.Add(new Record(Convert.ToDateTime(dp1.SelectedDate.Value.Date.ToShortDateString()), (MainWindow.opR.OpType),
                Convert.ToDouble(sumR.Text), accR.Text, catR.Text, noteR.Text));
            sumR.Text = "0";
            accR.Text = "";
            catR.Text = "";
            noteR.Text = "";
            this.Close();

        }
    }
}
