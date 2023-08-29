using ADODesigner.Animations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Gstc.Collections.ObservableLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.GUI.ViewModels
{
    public partial class AddBallsAnimationVM : ObservableObject
    {
        [ObservableProperty]
        private ObservableList<BallsAnimationArgs> animations = new();
        [RelayCommand]
        private void AddAnimation()
        {
        }
    }
}
