using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ADODesinger.Windows.Models.TimeLine
{
    public partial class VisualTimeLineModel : ObservableObject
    {
        [ObservableProperty]
        private Brush? borderBrush;
        [ObservableProperty]
        private float height;
        [ObservableProperty]
        private float width;
        [ObservableProperty]
        private Thickness margin;
    }
}
