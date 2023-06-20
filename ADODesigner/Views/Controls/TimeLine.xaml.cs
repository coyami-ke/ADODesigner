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
        public List<KeyFrameControl> KeyFrameControls { get; set; } = new();
        public int NumberTimeLine { get; set; } = 0;
        public TimeLine()
        {

            for (int i = 0; EditorView.Editor.TimeLines[NumberTimeLine].Count < 0; i++)
            {
                KeyFrameControl keyFrameControl = new();
                keyFrameControl.Key = EditorView.Editor.TimeLines[NumberTimeLine][i].Key;
                KeyFrameControls.Add(keyFrameControl);

                EditorView.Editor.TimeLines[NumberTimeLine].PropertyChanged += (sender, e) =>
                {
                    for (int s = 0; KeyFrameControls.Count < 0; s++)
                    {
                        if (EditorView.Editor.TimeLines[NumberTimeLine][i].Key == KeyFrameControls[s].Key)
                        {

                        }
                    }
                };
            }

            InitializeComponent();
        }
    }
}
