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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DBF;

namespace WpfDB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Repository db;
        
        public MainWindow()
        {
            InitializeComponent();
            db = new Repository(@"data.csv");
            MessageBox.Show($"База загружена из файла {db.DbPath}. Количество записей: {db.Count}.");
            
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            db.Add(new Record(Convert.ToDateTime(date.Text), Convert.ToSByte((optype.Text == "Доход") ? 1 : -1), Convert.ToDouble(sum.Text),acc.Text, cat.Text, note.Text));
            sum.Text = "";
            acc.Text = "";
            cat.Text = "";
            note.Text = "";
            Record[] lastRecords = db.FilteredList(new Template(db.LastSavingTime, DateTime.Now));
            listView.ItemsSource = lastRecords;
            listView.Items.Refresh();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            db.Save();
        }
    }
}
