
using Microsoft.Win32;
using QueryDeveloper_WPF.Model;
using QueryDeveloper_WPF.ViewModel;
using QueryDeveloper_WPF.Windows.ConnWindows;
using System.Diagnostics;
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
            Grid grid = dataGrid.Parent as Grid;
            WrapPanel wrapPanel = grid.Children.OfType<WrapPanel>().FirstOrDefault();
            TextBlock nameBlock = wrapPanel.Children.OfType<TextBlock>().FirstOrDefault()!;
            TextBlock timeBlock = grid.Children.OfType<TextBlock>().FirstOrDefault()!;
            SelectConnWindow selectConnWindow = new(dataGrid, CurrentUser, nameBlock, timeBlock);
            selectConnWindow.ShowDialog();
        }

        private void CloseQuery_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            ContextMenu menu = menuItem.Parent as ContextMenu;
            DataGrid? dataGrid = menu.PlacementTarget as DataGrid;
            Grid grid = dataGrid.Parent as Grid;
            if (dataGrid.Tag is DataBaseViewModel model)
            {
                AppDbContext.Quires.Remove(model.UserQuery);
                AppDbContext.SaveChanges();
            }
            dataGrid.ItemsSource = null;
            TextBlock timeBlock = grid.Children.OfType<TextBlock>().FirstOrDefault()!;
            timeBlock.Text = "Последнее время: ";
            WrapPanel wrapPanel = grid.Children.OfType<WrapPanel>().FirstOrDefault();
            TextBlock nameBlock = wrapPanel.Children.OfType<TextBlock>().FirstOrDefault()!;
            nameBlock.Text = "Название запроса";

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartQuery_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            WrapPanel wrapPanel = button.Parent as WrapPanel;
            Grid grid = wrapPanel.Parent as Grid;
            DataGrid dataGrid = grid.Children.OfType<DataGrid>().FirstOrDefault();
            DataBaseManager dataBase = new();
            if (dataGrid.Tag is DataBaseViewModel model)
                dataGrid.ItemsSource = dataBase.ExecuteQuery(model.ConnDataBase.ConnectionString, model.UserQuery);
            TextBlock timeBlock = grid.Children.OfType<TextBlock>().FirstOrDefault()!;
            timeBlock.Text = "Время выполнения: " + DateTime.Now.ToLongTimeString();

        }

        private void StopQuery_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenUser_Click(object sender, RoutedEventArgs e)
        {
            UserWindow userWindow = new(CurrentUser);
            userWindow.Show();

        }

        private void EditQuery_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            ContextMenu menu = menuItem.Parent as ContextMenu;
            DataGrid? dataGrid = menu.PlacementTarget as DataGrid;
            Grid grid = dataGrid.Parent as Grid;
            if (dataGrid.Tag is DataBaseViewModel model)
                Process.Start("C:\\Windows\\System32\\notepad.exe", model.UserQuery.Path);

        }
    }
}