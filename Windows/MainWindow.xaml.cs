using Microsoft.Win32;
using QueryDeveloper_WPF.Model;
using QueryDeveloper_WPF.ViewModel;
using QueryDeveloper_WPF.Windows.ConnWindows;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Timer = System.Timers.Timer;

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
        Timer _timer;
        

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
            MenuItem clickedItem = sender as MenuItem;
            MenuItem menu = clickedItem.Parent as MenuItem;
            Menu mainMenu = menu.Parent as Menu;
            if (clickedItem.IsChecked == false)
            {
                clickedItem.IsChecked = false;
                ((Timer)mainMenu.Tag)?.Stop();
                return;
            }
            // Снятие галочек со всех других элементов меню
            foreach (var item in menu.Items)
                    if (item is MenuItem menuItem && menuItem != clickedItem)
                        menuItem.IsChecked = false;
            clickedItem.IsChecked = true;
            _timer = new Timer(int.Parse(clickedItem.Header.ToString()) * 1000);
            _timer.Elapsed += (s, args) => Timer_Elapsed(sender, args);
            if (mainMenu.Tag is not null)
            {
                ((Timer)mainMenu.Tag)?.Stop();
                mainMenu.Tag = _timer;
                _timer.Start();
            }
            else
                mainMenu.Tag = _timer;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() => LoadData(sender));
        }

        private void LoadData(object sender)
        {
            MenuItem clickedItem = sender as MenuItem;
            MenuItem menuItem = clickedItem.Parent as MenuItem;
            Menu menu = menuItem.Parent as Menu;
            WrapPanel wrapPanel = menu.Parent as WrapPanel;
            Grid grid = wrapPanel.Parent as Grid;
            DataGrid dataGrid = grid.Children.OfType<DataGrid>().FirstOrDefault();
            DataBaseManager dataBase = new();
            if (dataGrid.Tag is DataBaseViewModel model)
                dataGrid.ItemsSource = dataBase.ExecuteQuery(model.ConnDataBase.ConnectionString, model.UserQuery);
            TextBlock timeBlock = grid.Children.OfType<TextBlock>().FirstOrDefault()!;
            timeBlock.Text = "Время выполнения: " + DateTime.Now.ToLongTimeString();

        }

        private void StartQuery_Click(object sender, RoutedEventArgs e)
        {
            Button startButton = sender as Button;
            WrapPanel wrapPanel = startButton.Parent as WrapPanel;
            startButton.IsEnabled = false;
            Button stopButton = wrapPanel.Children.OfType<Button>().Last() as Button;
            stopButton.IsEnabled = true;
            Grid grid = wrapPanel.Parent as Grid;
            Menu menu = wrapPanel.Children.OfType<Menu>().FirstOrDefault();
            if (menu.Tag is not null)
            {
                ((Timer)menu.Tag).Start();
                return;
            }
            DataGrid dataGrid = grid.Children.OfType<DataGrid>().FirstOrDefault();
            DataBaseManager dataBase = new();
            if (dataGrid.Tag is DataBaseViewModel model)
            {
                dataGrid.ItemsSource = dataBase.ExecuteQuery(model.ConnDataBase.ConnectionString, model.UserQuery);
                TextBlock timeBlock = grid.Children.OfType<TextBlock>().FirstOrDefault()!;
                timeBlock.Text = "Время выполнения: " + DateTime.Now.ToLongTimeString();
            }

        }

        private void StopQuery_Click(object sender, RoutedEventArgs e)
        {
            Button stopButton = sender as Button;
            stopButton.IsEnabled = false;
            WrapPanel wrapPanel = stopButton.Parent as WrapPanel;
            Button startButton = wrapPanel.Children.OfType<Button>().First();
            startButton.IsEnabled = true;
            Grid grid = wrapPanel.Parent as Grid;
            Menu menu = wrapPanel.Children.OfType<Menu>().FirstOrDefault();
            if (menu.Tag is not null)
                ((Timer)menu.Tag).Stop();
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
            {
                Process.Start("C:\\Windows\\System32\\notepad.exe", model.UserQuery.Path);
                return;
            }
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "SQL files (*.sql)|*.sql";
            openFileDialog.Title = "Выберите Sql-файл";
            if (openFileDialog.ShowDialog() == true)
                Process.Start("C:\\Windows\\System32\\notepad.exe", openFileDialog.FileName);
        }
    }
}