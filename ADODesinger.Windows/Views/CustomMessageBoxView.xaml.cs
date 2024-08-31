using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ADODesigner.Windows.Views
{
    public enum CustomMessageBoxButton
    {
        YesNo,
        Ok,
        YesNoCancel,
    }
    public enum CustomMessageBoxResult
    {
        None,
        Yes,
        No,
        Ok,
        Cancel
    }
    /// <summary>
    /// Interaction logic for CustomMessageBoxView.xaml
    /// </summary>
    public partial class CustomMessageBoxView : Window
    {
        public CustomMessageBoxView()
        {
            InitializeComponent();
        }

        public CustomMessageBoxResult Result { get; private set; } = CustomMessageBoxResult.None;

        public void SetButtons(CustomMessageBoxButton buttons)
        {
            if (buttons == CustomMessageBoxButton.YesNo || buttons == CustomMessageBoxButton.YesNoCancel)
            {
                this.YesButton.Visibility = Visibility.Visible;
                this.NoButton.Visibility = Visibility.Visible;

                this.YesButton.Click += (e, sender) =>
                {
                    this.Result = CustomMessageBoxResult.Yes;
                    this.Close();
                };
                this.NoButton.Click += (e, sender) =>
                {
                    this.Result = CustomMessageBoxResult.No;
                    this.Close();
                };

                if (buttons == CustomMessageBoxButton.YesNoCancel)
                {
                    this.CancelButton.Visibility = Visibility.Visible;
                    this.CancelButton.Click += (e, sender) =>
                    {
                        this.Result = CustomMessageBoxResult.Cancel;
                        this.Close();
                    };
                }
            }
            else if (buttons == CustomMessageBoxButton.Ok)
            {
                this.OKButton.Visibility = Visibility.Visible;
                this.OKButton.Click += (e, sender) => 
                {
                    this.Result = CustomMessageBoxResult.Ok;
                    this.Close();
                };
            }
        }

        private void BorderMoveWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
