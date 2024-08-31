using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Windows.Models
{
    public partial class TimeLineTileNavigation : ObservableObject
    {
        [ObservableProperty]
        private float firstPositionX;
        [ObservableProperty]
        private float firstPositionY;
        [ObservableProperty]
        private float secondPositionX;
        [ObservableProperty] 
        private float secondPositionY;
        [ObservableProperty]
        private float opacity;
    }
}
