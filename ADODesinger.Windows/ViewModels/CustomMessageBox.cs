using ADODesigner.Windows.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Windows.ViewModels
{
    public static class CustomMessageBox
    {
        public static CustomMessageBoxResult Show(string content, string title, CustomMessageBoxButton buttons)
        {
            CustomMessageBoxView view = new()
            {
                Title = title,
            };
            view.MessageBoxTitle.Text = title;
            view.MessageBoxContent.Text = content;

            view.SetButtons(buttons);

            view.ShowDialog();

            return view.Result;
        }
    }
}
