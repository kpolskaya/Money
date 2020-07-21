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
       
        
        public Window1()
        {
            InitializeComponent();
            opdate = MainWindow.opR.OpDate;
            acc = MainWindow.opR.Account;
            dp1.SelectedDate = opdate;
        }
    }
}
