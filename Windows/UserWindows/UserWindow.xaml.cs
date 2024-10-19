using Microsoft.EntityFrameworkCore;
using QueryDeveloper_WPF.Commands;
using QueryDeveloper_WPF.Extensions;
using QueryDeveloper_WPF.Model;
using System.Windows;

namespace QueryDeveloper_WPF
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private User CurrentUser;
        private AppDbContext _appDbContext;

        public UserWindow(User user)
        {
            InitializeComponent();
            CurrentUser = user;
            this.Resources["CurrentUser"] = CurrentUser;
            _appDbContext = new AppDbContext();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) => AdaptivePanelClass.SizeChanged(sender, e);

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            User user = _appDbContext.Users.Find(CurrentUser.Id)!;
            user = new() { Id = CurrentUser.Id, 
                Name = nameBox.Text.Trim(), 
                Surname = surnameBox.Text.Trim(),
                Login = loginBox.Text.Trim(),
                Password = passBox.Password.ToSHA256String()};
            _appDbContext.Entry(user).State = EntityState.Modified;
            MessageBox.Show("Пользователь обновлён");
        }
    }
}
