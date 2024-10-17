using QueryDeveloper_WPF.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryDeveloper_WPF.ViewModel
{
    public class DataBaseViewModel
    {
        public UserQuery UserQuery { get; set; } = null!;
        public ConnDataBase ConnDataBase { get; set; } = null!;
    }
}
