using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Cmd
{
    public interface IAnimationConfig<T>
    {
        public T Config { get; set; }
        public string ConfigFileName { get; set; }
    }
}
