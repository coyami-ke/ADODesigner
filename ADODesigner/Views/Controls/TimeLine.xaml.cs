using ADODesigner.Models;
using Gstc.Collections.ObservableLists;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADODesigner.Views.Controls
{
    /// <summary>
    /// Interaction logic for TimeLine.xaml
    /// </summary>
    public partial class TimeLine : UserControl
    {
        public int NumberTimeLine { get; set; } = 0;
        public TimeLine()
        {
            InitializeComponent();

            if ((NumberTimeLine % 2) == 0) canvas.Background = new SolidColorBrush(Color.FromRgb(18, 19, 21));
            else canvas.Background = new SolidColorBrush(Color.FromRgb(19, 20, 22));

            EditorView.Editor.TimeLines[NumberTimeLine].CollectionChanged += TimeLine_CollectionChanged;
        }

        private void TimeLine_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            canvas.Children.Clear();
            for (int i = 0; i < EditorView.Editor.TimeLines[NumberTimeLine].Count ; i++)
            {
                KeyFrameControl keyFrameControl = new();
                keyFrameControl.Key = EditorView.Editor.TimeLines[NumberTimeLine][i].Key;
                keyFrameControl.PART_textBlockTag.Text = EditorView.Editor.TimeLines[NumberTimeLine][i].Tag;
                keyFrameControl.Margin = new((EditorView.Editor.TimeLines[NumberTimeLine][i].AngleOffset / 180) + EditorView.Editor.TimeLines[NumberTimeLine][i].Floor * 50, 0, 0, 0);
                if (EditorView.Editor.TimeLines[NumberTimeLine][i].IsSelected) keyFrameControl.Opacity = 1;
                else keyFrameControl.Opacity = 0.8;
                canvas.Children.Add(keyFrameControl);
            }
        }
    }
}
