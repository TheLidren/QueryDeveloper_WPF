using QueryDeveloper_WPF.Model;

namespace QueryDeveloper_WPF.ViewModel
{
    public class DataBaseViewModel
    {
        public UserQuery UserQuery { get; set; } = null!;
        public ConnDataBase ConnDataBase { get; set; } = null!;
    }
}
