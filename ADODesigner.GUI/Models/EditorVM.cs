using ADODesigner.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Gstc.Collections.ObservableLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.GUI.Models
{
    public partial class EditorVM : ObservableObject
    {
        [ObservableProperty]
        private ObservableList<KeyFrame> keyFrames = new();
        [ObservableProperty]
        private ObservableList<Decoration> decorations = new();
        private Buffer<ObservableList<KeyFrame>> keyFramesBuffer = new();
        private Buffer<ObservableList<Decoration>> decorationsBuffer = new();
    }
}
