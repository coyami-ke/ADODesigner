using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ADODesinger.Windows.Models.TimeLine
{
    public partial class ChartTimeLineModel : ObservableObject
    {
        [ObservableProperty]
        private string textAngle = "-1";
        [ObservableProperty]
        private int number = -1;
        [ObservableProperty]
        private Thickness marginLine;
        [ObservableProperty]
        private Thickness marginText;
        [ObservableProperty]
        private Thickness marginTextAngle;
    }
}
