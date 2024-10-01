using QueryDeveloper_WPF.Commands;
using QueryDeveloper_WPF.Extensions;
using QueryDeveloper_WPF.Model;
using System.Windows;

namespace QueryDeveloper_WPF
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        AppDbContext _appDbContext;
        LoginWindow _loginWindow;
        public RegisterWindow()
        {
            InitializeComponent();
            _appDbContext = new AppDbContext();
            _loginWindow = new LoginWindow();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) => AdaptivePanelClass.SizeChanged(sender, e);

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            User user = new()
            {
                Name = nameBox.Text,
                Surname = surnameBox.Text,
                Login = loginBox.Text,
                Password = passBox.Password.ToSHA256String()
            };
            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();
            MessageBox.Show("УЗ создана. Сообщите администратору для активации УЗ");
            _loginWindow.Show();
            this.Close();
        }
    }
}
