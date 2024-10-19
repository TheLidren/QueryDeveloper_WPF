using System.Windows;

namespace QueryDeveloper_WPF.ViewModel
{
    public class OpenFormViewModel
    {
        public string NewWindow { get; set; } = null!;
        public Window OldWindow { get; set; } = null!;
        public bool CloseOldWindow { get; set; } = false;
    }
}
