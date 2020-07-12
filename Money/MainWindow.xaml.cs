using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DBF;
using System.IO;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Money
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
         //Reports.ItemsSource = Repository db;
        }

        
        Repository db = new Repository(@"data.csv");
        
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
                   
        {
      
        }

        private void MoneyList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newDate = Convert.ToDateTime(dp1.SelectedDate.Value.Date.ToShortDateString());
            var newType = (sbyte)-1;
            var newSum = Convert.ToDouble(sumOp.Text);
            var newAcc = account.Text;
            var newCat = cat.Text;
            var newNot = note.Text;
            db.Add(new Record(newDate, newType, newSum, newAcc, newCat, newNot));
            db.Save();
        }

        private void today_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt = DateTime.Today;
            today.Content = dt.ToString ("dd.MM.yyyy");
        }

       
    }
}
