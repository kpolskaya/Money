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
        private BindingList<Record> _todoDataList;

        public MainWindow()
        {
            InitializeComponent();

        }
        //private void buttonRead_Click(object sender, RoutedEventArgs e)// чтение из файла
        //{
        //    txbTextFile.Text = File.ReadAllText(txb.Text);
        //}
        private void buttonRead_Click(object sender, RoutedEventArgs e)// запись в файл
        {
            File.WriteAllText(txb.Text, txbTextFile.Text);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
                   
        {
            _todoDataList = new BindingList<Record>()
            {
                new Record(DateTime.Today, 1, 1000, "cash", "home", "lalala"),
           
                new Record(DateTime.Today, 0, 5000, "card", "pets", "blablabla") 
            };
           
            MoneyList.ItemsSource = _todoDataList;
            
        }
        
    }
}
