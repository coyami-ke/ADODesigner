using ADODesigner.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace ADODesigner.ViewModels
{
    public partial class KeyFrameControlVM : ObservableObject
    {
        [ObservableProperty]
        private KeyFrame keyFrame;

        [RelayCommand]
        private void ChangeSelection()
        {
            KeyFrame.IsSelected = !KeyFrame.IsSelected;
        }
    }
}
