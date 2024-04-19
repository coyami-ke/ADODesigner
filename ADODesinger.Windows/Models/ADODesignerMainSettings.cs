using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesinger.Windows.Models
{
    public partial class ADODesignerMainSettings : ObservableObject
    {
        [ObservableProperty]
        private string? lastOpenedProject;
    }
}
