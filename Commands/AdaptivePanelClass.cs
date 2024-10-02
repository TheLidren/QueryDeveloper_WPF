using System.Windows;
using System.Windows.Controls;

namespace QueryDeveloper_WPF.Commands
{
    public static class AdaptivePanelClass
    {
        internal static void SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Window? formWindow = sender as Window;
            if (e.WidthChanged && formWindow != null)
                ((StackPanel)formWindow.FindName("AdaptiveStackPanel")).Width = formWindow.ActualWidth / 2;
        }
    }
}
