using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace ADODesigner.Models
{
    public partial class Project : ObservableObject
    {
        [ObservableProperty]
        private string path;
        [ObservableProperty]
        private string name;
    }
}
