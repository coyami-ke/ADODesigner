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

            if ((NumberTimeLine % 2) == 0) canvas.Background = new SolidColorBrush(Color.FromRgb(16, 16, 19));
            else canvas.Background = new SolidColorBrush(Color.FromRgb(18, 18, 21));

            EditorView.Editor.TimeLines[NumberTimeLine].CollectionChanged += TimeLine_CollectionChanged;
            EditorView.Editor.AddKeyFrame("AZAZA");
        }

        private void TimeLine_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            canvas.Children.Clear();
            for (int i = 0; i < EditorView.Editor.TimeLines[NumberTimeLine].Count ; i++)
            {
                KeyFrameControl keyFrameControl = new();
                keyFrameControl.Key = EditorView.Editor.TimeLines[NumberTimeLine][i].Key;
                canvas.Children.Add(keyFrameControl);
            }
        }
    }
}
