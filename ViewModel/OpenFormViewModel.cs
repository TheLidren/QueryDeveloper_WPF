using QueryDeveloper_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryDeveloper_WPF.ViewModel
{
    public class OpenFormViewModel
    {
        public string NewWindow { get; set; } = null!;
        public string OldWindow { get; set; } = null!;
        public bool CloseOldWindow { get; set; } = false;
    }
}
