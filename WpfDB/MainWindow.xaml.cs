using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Reflection.Emit;
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
        public static Record opR = new Record(); // запись для редактирования
        string[] accs = new string[] { "карта", "наличные", "кредит" };
        string[] cats = new string[] { "продукты", "дом", "коммунальные платежи","животные","отдых","погашение кредита","инвестиции","одежда и обувь","прочее" };
        string[] catsP = new string[] { "зарплата", "подработка", "проценты от инвестиций", "благотворительность", "воровство", "подарки и находки", "прочее" };
        string[] all = new string[] {""};
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;

        public MainWindow()
        {
            InitializeComponent();
            db = new Repository(@"data.csv");
            MessageBox.Show($"База загружена из файла {db.DbPath}. Количество записей: {db.Count}.");
            acc.ItemsSource = accs;
            accP.ItemsSource = accs;
            accR.ItemsSource = accs;
            cat.ItemsSource = cats;
            catP.ItemsSource = catsP;
            catR.ItemsSource = all.Concat(cats.Concat(catsP));
            dp2R.SelectedDate = DateTime.Today;
            dp1R.SelectedDate = db.StartingDate;
           
        }

       
        //private void Add_Click(object sender, RoutedEventArgs e)
        //{
        //    db.Add(new Record(Convert.ToDateTime(date.Text), Convert.ToSByte((optype.Text == "Доход") ? 1 : -1), Convert.ToDouble(sum.Text),acc.Text, cat.Text, note.Text));
        //    sum.Text = "";
        //    acc.Text = "";
        //    cat.Text = "";
        //    note.Text = "";
        //    Record[] lastRecords = db.FilteredList(new Template(db.LastSavingTime, DateTime.Now));
        //    listView.ItemsSource = lastRecords;
        //    listView.Items.Refresh();
        //}

        //private void Save_Click(object sender, RoutedEventArgs e)
        //{
        //    db.Save();
        //}

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
            db.Add(new Record(Convert.ToDateTime(dp1.SelectedDate.Value.Date.ToShortDateString()), (sbyte)(-1),
                Convert.ToDouble(sum.Text), acc.Text, cat.Text, note.Text));
            sum.Text = "0";
            acc.Text = "";
            cat.Text = "";
            note.Text = "";
            Record[] lastRecords = db.FilteredList(new Template(db.LastSavingTime, DateTime.Now,(sbyte)(-1)));
            listView.ItemsSource = lastRecords;
            listView.Items.Refresh();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            db.Add(new Record(Convert.ToDateTime(dp1P.SelectedDate.Value.Date.ToShortDateString()), (sbyte)(1),
               Convert.ToDouble(sumP.Text), accP.Text, catP.Text, noteP.Text));
            sum.Text = "0";
            acc.Text = "";
            cat.Text = "";
            note.Text = "";
            Record[] lastRecords = db.FilteredList(new Template(db.LastSavingTime, DateTime.Now, (sbyte)(1)));
            listViewP.ItemsSource = lastRecords;
            listViewP.Items.Refresh();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            sbyte t;
            if (typeR.Text == "приход")
            {
                t = 1;
            }
            else if (typeR.Text == "расход")
            {
                t = -1;
            }
            else t = 0;

            string a;
            if (accR.Text == null)
            {
                a = "";
            } else a = accR.Text;

            string c;
            if(catR.Text == null )
            { 
                c = ""; }
            else
            c = catR.Text;

            Record[] lastRecords = db.FilteredList(new Template(Convert.ToDateTime(dp1R.SelectedDate.Value.Date.ToShortDateString()), 
                Convert.ToDateTime(dp2R.SelectedDate.Value.Date.ToShortDateString()),t,a,c));
            listViewR.ItemsSource = lastRecords;
            listViewR.Items.Refresh();
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

        public void listViewR_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            opR = (Record)item;
            if (item != null)
            {
                Window1 taskWindow = new Window1();
                taskWindow.Show();
               
            }
        }

    }
}
