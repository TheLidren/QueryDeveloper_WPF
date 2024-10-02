using QueryDeveloper_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QueryDeveloper_WPF.Commands
{
    public class OpenFormCommand : ICommand
    {
        
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is OpenFormViewModel model)
            {
                ((Window)Activator.CreateInstance(Type.GetType(model.NewWindow)!)!).Show();
                if (model.CloseOldWindow)
                    ((Window)Activator.CreateInstance(Type.GetType(model.OldWindow)!)!).Close();
            }
        }
    }
}
