using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Windows.Models
{
    public partial class BookmarkTimeLine : ObservableObject
    {
        [ObservableProperty]
        private int floor;
    }
}
