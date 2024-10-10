using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryDeveloper_WPF.Model
{
    public class ConnDataBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;

    }
}
