using QueryDeveloper_WPF.Model;
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

namespace QueryDeveloper_WPF
{
    /// <summary>
    /// Логика взаимодействия для ConnWindow.xaml
    /// </summary>
    public partial class ConnWindow : Window
    {
        private ConnectionDB ConnDb;

        static bool CheckedMethod(string[] values)
        {
            foreach (string s in values)
                if (string.IsNullOrEmpty(s))
                {
                    MessageBox.Show("Заполните все поля");
                    return false;
                }
            return true;
        }

        public ConnWindow(ConnectionDB db)
        {
            InitializeComponent();
            ConnDb = db;
            ListConn.ItemsSource = ConnDb.GetModels();
        }

        private void AddConn_Click(object sender, RoutedEventArgs e)
        {
            string[] values = {Name.Text, Description.Text, ConnectionString.Text };
            if (CheckedMethod(values)) 
            {
                ConnDb.AddConnection(new ConnModel { Name = Name.Text, Description = Description.Text, ConnectionString = ConnectionString.Text});
                ListConn.Items.Refresh();
            } 
        }
    }
}
