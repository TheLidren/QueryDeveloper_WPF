namespace QueryDeveloper_WPF.Model
{
    public class UserQuery
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Path { get; set; } = null!;
        public int? UserId { get; set; }
        public User User { get; set; } = null!;
        public int? ConnDataBaseId { get; set; }
        public ConnDataBase ConnDataBase { get; set; } = null!;
    }

}
