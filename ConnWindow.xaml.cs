

using QueryDeveloper_WPF.Model;
using System.Windows;
using System.Windows.Controls;

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
            string[] values = {nameBox.Text, descBox.Text, ConnectionString.Text };
            if (CheckedMethod(values)) 
            {
                ConnDb.AddConnection(new ConnModel { Name = nameBox.Text, Description = descBox.Text, ConnectionString = ConnectionString.Text});
                ListConn.Items.Refresh();
            } 
        }


        private void EditConn_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var editItem = e.Row.Item as ConnModel;
            if (!ConnDb.UpdateConnection(editItem!))
                e.Cancel = true;
        }
    }
}
