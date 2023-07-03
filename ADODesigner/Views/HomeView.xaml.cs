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

namespace ADODesigner.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : Window
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void RowDefinition_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void closeWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
