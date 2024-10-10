

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
        private DataBaseManager ConnDb;

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

        public ConnWindow()
        {
            InitializeComponent();
            ConnDb = new DataBaseManager();
            ListConn.ItemsSource = ConnDb.GetListConn();
        }

        private void AddConn_Click(object sender, RoutedEventArgs e)
        {
            string[] values = {nameBox.Text, descBox.Text, ConnectionString.Text };
            if (CheckedMethod(values)) 
            {
                ConnDb.AddConnection(new ConnDataBase { Name = nameBox.Text, Description = descBox.Text, ConnectionString = ConnectionString.Text});
                ListConn.Items.Refresh();
            } 
        }


        private void EditConn_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var editItem = e.Row.Item as ConnDataBase;
            if (!ConnDb.UpdateConnection(editItem!))
                e.Cancel = true;
            else
            {

            }
        }
    }
}
