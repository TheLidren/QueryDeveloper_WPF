using Microsoft.Win32;
using QueryDeveloper_WPF.Model;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace QueryDeveloper_WPF.Windows.ConnWindows
{
    /// <summary>
    /// Логика взаимодействия для SelectConnWindow.xaml
    /// </summary>
    public partial class SelectConnWindow : Window
    {
        AppDbContext AppDbContext = new();
        (string filePath, string fileName) queryFile;
        DataGrid _dataGrid;
        TextBlock _nameBlock, _timeBlock;
        List<ConnDataBase> _listConnections;
        DataBaseManager _connectionDb;
        User _currentUser;

        public SelectConnWindow(DataGrid dataGrid, User currentUser, TextBlock nameBlock, TextBlock timeBlock)
        {
            InitializeComponent();
            _dataGrid = dataGrid;
            _nameBlock = nameBlock;
            _timeBlock = timeBlock;
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
            _currentUser = currentUser;
        }

        private void SelectConn_Checked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "SQL files (*.sql)|*.sql";
            openFileDialog.Title = "Выберите Sql-файл";
            if (openFileDialog.ShowDialog() == true)
                queryFile = (openFileDialog.FileName, openFileDialog.SafeFileName);
            RadioButton radioButton = (RadioButton)sender;
            ConnDataBase connDataBase = _listConnections.Where(x => x.Name == radioButton.Name).First();

            (_dataGrid.ItemsSource, _dataGrid.Tag) = _connectionDb.OpenQuery(queryFile, connDataBase, _currentUser);
            if (_dataGrid.ItemsSource is null && _dataGrid.Tag is null)
            {
                radioButton.IsChecked = false;
                return;
            }
            _nameBlock.Text = queryFile.fileName;
            _timeBlock.Text = "Время выполнения: " + DateTime.Now.ToLongTimeString();
            this.DialogResult = true;
            this.Close();
        }
    }
}
