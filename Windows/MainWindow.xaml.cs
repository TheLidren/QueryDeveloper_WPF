
using Microsoft.Win32;
using QueryDeveloper_WPF.Model;
using QueryDeveloper_WPF.ViewModel;
using QueryDeveloper_WPF.Windows.ConnWindows;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace QueryDeveloper_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        DataBaseManager ConnDB = new();
        AppDbContext AppDbContext = new AppDbContext();
        User CurrentUser;
        string query;

        public MainWindow(User user)
        {
            InitializeComponent();
            CurrentUser = user;
        }
        public MainWindow()
        {
            InitializeComponent();
            CurrentUser = AppDbContext.Users.First();
            this.Resources["LogoutKey"] = new OpenFormViewModel { NewWindow = "QueryDeveloper_WPF.LoginWindow", OldWindow = this, CloseOldWindow = true };
            LogOutButton.Command = (ICommand)this.Resources["OpenFormCommand"];
            LogOutButton.CommandParameter = this.Resources["LogoutKey"];
            //SelectConnWindow connWindow = new();
            //Type type = connWindow.GetType();
            //MessageBox.Show(type.FullName);
        }

        private void OpenQuery_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            ContextMenu menu = menuItem.Parent as ContextMenu;
            DataGrid dataGrid = menu.PlacementTarget as DataGrid;
            //data.ItemsSource = selectConnWindow()            
            SelectConnWindow selectConnWindow = new(dataGrid);
            selectConnWindow.ShowDialog();
        }

        private void CloseQuery_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartQuery_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StopQuery_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenUser_Click(object sender, RoutedEventArgs e)
        {
            UserWindow userWindow = new(CurrentUser);
            userWindow.Show();

        }
    }
}