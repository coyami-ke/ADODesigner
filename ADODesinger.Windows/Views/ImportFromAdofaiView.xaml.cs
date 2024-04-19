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

namespace ADODesinger.Windows.Views
{
    /// <summary>
    /// Interaction logic for ImportFromAdofaiView.xaml
    /// </summary>
    public partial class ImportFromAdofaiView : Window
    {
        public ImportFromAdofaiView()
        {
            InitializeComponent();
        }

        private void BorderCloseWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void BorderMoveWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
