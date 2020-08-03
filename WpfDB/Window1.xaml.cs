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
       
        /// <summary>
        /// Дата редактируемой операции
        /// </summary>
        DateTime opdate;

        /// <summary>
        /// Список счетов для редактирования операции
        /// </summary>
        string[] accs = MainWindow.accs;

        /// <summary>
        /// Список расходных категорий для редактирования операции
        /// </summary>
        string[] catsE = MainWindow.catsE;

        /// <summary>
        /// Список приходных категорий для редактирования операции
        /// </summary>
        string[] catsI = MainWindow.catsI;

                        
        /// <summary>
        /// окно редактирования записи
        /// </summary>
        public Window1()
        {
            InitializeComponent();
            opdate = MainWindow.opR.OpDate;
            dp1.SelectedDate = opdate;
            sumR.Text = Convert.ToString(MainWindow.opR.OpSum);
            catR.SelectedItem = MainWindow.opR.Category;
            
            if (MainWindow.opR.OpType == 1)         // выбор категории в зависимости от типа операции (приход/расход)
            {
                catR.ItemsSource = catsI;
            }
            else
            {
                catR.ItemsSource = catsE;
            }
            noteR.Text = MainWindow.opR.Note;
            accR.ItemsSource = accs;
            accR.SelectedItem = MainWindow.opR.Account;
           
        }
        /// <summary>
        /// Удаляет предыдущую редакцию записи и записывает новую
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.db.Delete(MainWindow.opR.RecNumber);
            MainWindow.db.Add(new Record(Convert.ToDateTime(dp1.SelectedDate.Value.Date.ToShortDateString()), (MainWindow.opR.OpType),
                Convert.ToDouble(sumR.Text), accR.Text, catR.Text, noteR.Text));
            //sumR.Text = "0";
            //accR.Text = "";
            //catR.Text = "";
            //noteR.Text = "";
            this.Close();

        }
    }
}
