using ADODesigner.Animations;
using ADODesigner.Models;
using ADODesigner.ViewModels;
using ADODesigner.Views.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static EditorVM Editor { get; set; }
        public EditorView()
        {
            Editor = new();
            Editor.PropertyChanged += Editor_PropertyChanged;
            DataContextChanged += EditorView_DataContextChanged;

            

            InitializeComponent();

            panelTimeLines.Children.Add(new TimeLine());

            BallsAnimation ballsAnimation = new(new BallsAnimationArgs() { FirstPosition = new(0, 0), SecondPosition = new(10, 10) });
            ballsAnimation.CreateAnimation();

            MessageBox.Show(Editor.TimeLines[0][10].PositionOffset.X.ToString());

            //MessageBox.Show(Editor.FindKeyFrame("BallsAnimation" + "Balls1" + "1").PositionOffset.X.ToString());
        }

        private void EditorView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Editor = (EditorVM)DataContext;
        }

        private void Editor_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            DataContext = Editor;
        }
    }
}
