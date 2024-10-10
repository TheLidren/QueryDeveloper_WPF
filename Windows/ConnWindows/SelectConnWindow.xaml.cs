using Microsoft.Win32;
using QueryDeveloper_WPF.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

namespace QueryDeveloper_WPF.Windows.ConnWindows
{
    /// <summary>
    /// Логика взаимодействия для SelectConnWindow.xaml
    /// </summary>
    public partial class SelectConnWindow : Window
    {
        AppDbContext AppDbContext = new();
        string query;
        DataGrid _dataGrid;
        List<ConnDataBase> _listConnections;
        DataBaseManager _connectionDb;


        public SelectConnWindow(DataGrid dataGrid)
        {
            InitializeComponent();
            _dataGrid = dataGrid;
            _listConnections =  AppDbContext.Connections.ToList();
            for (int i = 0; i < _listConnections.Count; i++) 
            {
                RadioButton radioButton = new()
                {
                    Name = _listConnections[i].Name,
                    Content = _listConnections[i].Name + ": " + _listConnections[i].Description,
                    Margin = new Thickness(5)
                };
                radioButton.Checked += SelectConn_Checked;
                ListConnPanel.Children.Add(radioButton);
            }
            _connectionDb = new DataBaseManager();
        }

        private void SelectConn_Checked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "SQL files (*.sql)|*.sql";
            if (openFileDialog.ShowDialog() == true)
            {
                string FilePath = openFileDialog.FileName;
                query = File.ReadAllText(FilePath);
            }
            RadioButton radioButton = (RadioButton)sender;
            ConnDataBase connDataBase = _listConnections.Where(x => x.Name == radioButton.Name).First();
            
            _dataGrid.ItemsSource = _connectionDb.ExecuteQuery(query, connDataBase.ConnectionString);
            this.DialogResult = true;
            this.Close();
            //this.Close();
        }
    }
}
