using ADODesigner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Animations
{
    public interface IBaseAnimation
    {
        public (KeyFrame[], Decoration[]) CreateAnimation();
    }
}
