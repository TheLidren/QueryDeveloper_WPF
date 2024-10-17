namespace QueryDeveloper_WPF.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;
        public bool Status { get; set; } = false;

        public ICollection<UserQuery> UserQueries { get; set; } = null!;


    }
}
