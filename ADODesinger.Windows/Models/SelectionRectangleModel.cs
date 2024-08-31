using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ADODesigner.Windows.Models
{
    public partial class SelectionRectangleModel : ObservableObject
    {
        [ObservableProperty]
        private bool isMouseDown;
        [ObservableProperty]
        private Point mouseDownPosition;
        [ObservableProperty]
        private double height;
        [ObservableProperty]
        private double width;
        [ObservableProperty]
        private double canvasLeft;
        [ObservableProperty]
        private double canvasTop;
        [ObservableProperty]
        private Visibility visibility;
    }
}
