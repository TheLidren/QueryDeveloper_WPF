using QueryDeveloper_WPF.Commands;
using QueryDeveloper_WPF.Extensions;
using QueryDeveloper_WPF.Model;
using System.Windows;

namespace QueryDeveloper_WPF
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        AppDbContext _appDbContext;
        public LoginWindow()
        {
            InitializeComponent();
            _appDbContext = new AppDbContext();
            this.Resources["OpenForm"] = (title: "QueryDeveloper_WPF.RegisterWindow", closeWindow: true);
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            User? user = _appDbContext.Users.Where(u => u.Login == loginBox.Text && u.Password == passBox.Password.ToSHA256String()).FirstOrDefault();
            if (user != null && user.Status)
            {
                MainWindow mainWindow = new(user);
                mainWindow.Show();
                this.Close();
            }
            else MessageBox.Show("Ошибка авторизации");


        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) => AdaptivePanelClass.SizeChanged(sender, e);

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new();
            registerWindow.Show();
        }

    }
}
