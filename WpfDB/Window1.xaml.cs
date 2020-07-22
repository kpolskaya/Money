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
            Sum.Text = Convert.ToString(MainWindow.opR.OpSum);
            Cat.SelectedItem = MainWindow.opR.Category;
           // Cat.Text = MainWindow.opR.Category;
            Note.Text =  MainWindow.opR.Note;
            //acc.ItemsSource = accs;
            //accP.ItemsSource = accs;
            accR.ItemsSource = accs;
            Cat.ItemsSource = all.Concat(cats.Concat(catsP));
            accR.SelectedItem = MainWindow.opR.Account;
            //catP.ItemsSource = catsP;
            //catR.ItemsSource = all.Concat(cats.Concat(catsP));

        }
    }
}
