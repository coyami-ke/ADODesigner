using ADODesigner.Windows.Models.Localization;
using ADODesinger.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Windows.ViewModels
{
    public partial class GuideViewModel : ObservableObject
    {
        [ObservableProperty]
        private ADODesignerLocalization localization = App.Localization;
    }
}
