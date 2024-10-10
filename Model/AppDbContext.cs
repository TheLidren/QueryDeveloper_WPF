namespace QueryDeveloper_WPF.Model
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserQuery> Quires { get; set; }
        public DbSet<ConnDataBase> Connections { get; set; }


        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;port=3306;user id=root;password=root;database=WpfQueryDB;Persist Security Info=True");
        }

    }
}
