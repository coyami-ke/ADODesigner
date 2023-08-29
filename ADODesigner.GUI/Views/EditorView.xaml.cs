using ADODesigner.GUI.Models;
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
#nullable disable
namespace ADODesigner.Views
{
    public partial class EditorView : Window
    {
        public EditorView()
        {
            InitializeComponent();
        }
        private void closeWindowButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }
        public void RunAnimation()
        {
            EditorVM editor = DataContext as EditorVM;
            for (int i = 0; i < editor.KeyFrames; i++)
            {

            }
            PART_Preview.Children.Add();
        }
    }
}
