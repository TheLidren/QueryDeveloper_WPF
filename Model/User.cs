using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryDeveloper_WPF.Model
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
