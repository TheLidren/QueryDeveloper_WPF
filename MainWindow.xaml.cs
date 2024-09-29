
using Microsoft.Win32;
using System.Windows;

namespace QueryDeveloper_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConnectionDB ConnDB = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenWindow_Click(object sender, RoutedEventArgs e)
        {
            ConnWindow connWindow = new(ConnDB);
            connWindow.Show();
        }

        private void OpenQuery_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                MessageBox.Show("File Open");
        }

        private void CloseQuery_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}