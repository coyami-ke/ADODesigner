using ADODesigner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Cmd
{
    public interface IADODesignerCommand
    {
        public string Command { get; set; }
        public string Description { get; set; }
        public (KeyFrame[], Decoration[]) Run();
    }
}
