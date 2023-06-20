using ADODesigner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.ViewModels.Buffer
{
    public class BufferStates <T>
    {
        public Stack<T> Buffer { get; private set; }
        public BufferStates()
        {
            Buffer = new Stack<T>();
        }
    }
}
