using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Text;
using System.IO;
using Microsoft.Win32;
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
        static char[] comma = new char[] { ',' };
        public static Repository db;
        public static Record opR = new Record(); // запись для редактирования
        public static string[] accs; 
        public static string[] catsE; 
        public static string[] catsI;
        public static string balance;
        string[] all = new string[] {""};
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        // public static string balance = String.Format("{0 : 0.00}", db.Balance);
        sbyte t;
        string a;
        string c;
        public MainWindow()
        {
            InitializeComponent();
            db = new Repository(@"data.csv");
            MessageBox.Show($"База загружена из файла {db.DbPath}. Количество записей: {db.Count}.");
            
            if (App.Settings.Accounts == null)
            {
                Window2 window = new Window2();
                window.ShowDialog();
            }
            accs = App.Settings.Accounts.Split(comma, StringSplitOptions.RemoveEmptyEntries);
            catsE = App.Settings.OutCategories.Split(comma, StringSplitOptions.RemoveEmptyEntries);
            catsI = App.Settings.InCategories.Split(comma, StringSplitOptions.RemoveEmptyEntries);

            accE.ItemsSource = accs;
            accI.ItemsSource = accs;
            accR.ItemsSource = all.Concat(accs);
            catE.ItemsSource = catsE;
            catI.ItemsSource = catsI;
            catR.ItemsSource = all.Concat(catsE.Concat(catsI));
            dp1E.SelectedDate = DateTime.Today;
            dp2R.SelectedDate = DateTime.Today;
            dp1R.SelectedDate = db.StartingDate;
          
            balance = String.Format("{0 : 0.00}", db.Balance);
            this.DataContext = db;         
        }

          
        private void sumOp_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox ctrl = sender as TextBox;
            e.Handled = ",0123456789".IndexOf(e.Text) < 0;//только цифры и ,
            ctrl.MaxLength = 9;//длина текста в текстбоксе
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            db.Save();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (dp1E.SelectedDate == null || sumE.Text == "" || accE.Text == "" || catE.Text == "")
            {
                MessageBox.Show($"Не все поля заполнены");
            }
            else
            {
                db.Add(new Record(Convert.ToDateTime(dp1E.SelectedDate.Value.Date.ToShortDateString()), (sbyte)(-1),
                    Convert.ToDouble(sumE.Text), accE.Text, catE.Text, noteE.Text));
                sumE.Text = " ";
                accE.Text = "";
                catE.Text = "";
                noteE.Text = "";
                Record[] lastRecords = db.FilteredList(new Template(db.LastSavingTime, DateTime.Now,(sbyte)(-1)));
                listViewE.ItemsSource = lastRecords;
                listViewE.Items.Refresh();
                saldo.Text = db.Balance.ToString("0.00");
                saldoI.Text = db.Balance.ToString("0.00");
            }

            }

            private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (dp1I.SelectedDate == null || sumI.Text == "" || accI.Text == "" || catI.Text == "")
            {
                MessageBox.Show($"Не все поля заполнены");
            }
            else
            {
                db.Add(new Record(Convert.ToDateTime(dp1I.SelectedDate.Value.Date.ToShortDateString()), (sbyte)(1),
               Convert.ToDouble(sumI.Text), accI.Text, catI.Text, noteI.Text));
                sumE.Text = " ";
                accI.Text = "";
                catI.Text = "";
                noteI.Text = "";
                Record[] lastRecords = db.FilteredList(new Template(db.LastSavingTime, DateTime.Now, (sbyte)(1)));
                listViewI.ItemsSource = lastRecords;
                listViewI.Items.Refresh();
                saldoI.Text = db.Balance.ToString("0.00");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        { 
            if (typeR.Text == "приход")
            {
                t = 1;
            }
            else if (typeR.Text == "расход")
            {
                t = -1;
            }
            else t = 0;

          
            if (accR.Text == null)
            {
                a = "";
            } else a = accR.Text;

           
            if(catR.Text == null )
            { 
                c = ""; }
            else
            c = catR.Text;

            Record[] lastRecords = db.FilteredList(new Template(Convert.ToDateTime(dp1R.SelectedDate.Value.Date.ToShortDateString()), 
                Convert.ToDateTime(dp2R.SelectedDate.Value.Date.ToShortDateString()),t,a,c));
            listViewR.ItemsSource = lastRecords;
            listViewR.Items.Refresh();
            saldoR.Text = db.Balance.ToString("0.00");
        }

        private void lvUsersColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                listViewR.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            listViewR.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }

        public class SortAdorner : Adorner
        {
            private static Geometry ascGeometry =
                Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

            private static Geometry descGeometry =
                Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

            public ListSortDirection Direction { get; private set; }

            public SortAdorner(UIElement element, ListSortDirection dir)
                : base(element)
            {
                this.Direction = dir;
            }

            protected override void OnRender(DrawingContext drawingContext)
            {
                base.OnRender(drawingContext);

                if (AdornedElement.RenderSize.Width < 20)
                    return;

                TranslateTransform transform = new TranslateTransform
                    (
                        AdornedElement.RenderSize.Width - 10,
                        (AdornedElement.RenderSize.Height - 5) / 2
                    );
                drawingContext.PushTransform(transform);

                Geometry geometry = ascGeometry;
                if (this.Direction == ListSortDirection.Descending)
                    geometry = descGeometry;
                drawingContext.DrawGeometry(Brushes.Black, null, geometry);

                drawingContext.Pop();
            }
        }

      
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
          
        }

        private void TextBlock_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as TextBlock).DataContext;
            opR = (Record)item;
            if (item != null)
            {
                Window1 taskWindow = new Window1();
                taskWindow.Show();

            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
           SaveFileDialog openFileDialog = new SaveFileDialog();
            if (openFileDialog.ShowDialog() == true)
            
            db.Save(openFileDialog.FileName, new Template(Convert.ToDateTime(dp1R.SelectedDate.Value.Date.ToShortDateString()),
                Convert.ToDateTime(dp2R.SelectedDate.Value.Date.ToShortDateString()), t, a, c));
            MessageBox.Show($"Данные сохранены в {openFileDialog.FileName}");
        }
    }
}
