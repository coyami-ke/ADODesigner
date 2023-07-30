using CommunityToolkit.Mvvm.ComponentModel;
using Gstc.Collections.ObservableLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Models
{
    public partial class ExtendedKeyFrame : ObservableObject
    {
        [ObservableProperty]
        private KeyFrame keyFrame = new();
        [ObservableProperty]
        private ObservableList<int> numberFrames = new();
    }
}
