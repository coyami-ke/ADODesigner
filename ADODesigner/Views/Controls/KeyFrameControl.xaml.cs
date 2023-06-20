using ADODesigner.Models;
using ADODesigner.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADODesigner.Views.Controls
{
    /// <summary>
    /// Interaction logic for KeyFrameControl.xaml
    /// </summary>
    public partial class KeyFrameControl : UserControl
    {
        public string Key { get; set; } = String.Empty;
        public KeyFrameControl()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < EditorView.Editor.TimeLines.Count; i++)
            {
                for (int s = 0; s < EditorView.Editor.TimeLines[i].Count; s++)
                {
                    if (EditorView.Editor.TimeLines[i][s].Key == Key)
                    {
                        EditorView.Editor.TimeLines[i][s].IsSelected = !EditorView.Editor.TimeLines[i][s].IsSelected;
                        if (EditorView.Editor.TimeLines[i][s].IsSelected)
                            Background = new SolidColorBrush(new System.Windows.Media.Color() { R = 189 }) ;
                        
                    }
                }
            }
        }
    }
}
