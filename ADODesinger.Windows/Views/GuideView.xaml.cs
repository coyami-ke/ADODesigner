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
    /// <summary>
    /// Interaction logic for GuideView.xaml
    /// </summary>
    public partial class GuideView : Window
    {
        public GuideView()
        {
            InitializeComponent();
        }
        private void BorderCloseWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void BorderMaxWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Normal) WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

        private void BorderMinWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BorderMoveWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
