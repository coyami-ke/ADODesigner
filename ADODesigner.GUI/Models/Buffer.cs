using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.GUI.Models
{
    public class Buffer <T>
    {
        private Stack<T> values = new Stack<T>();
        public void Add(T value)
        {
            values.Push(value);
        }
        public T Take()
        {
            return values.Pop();
        }
    }
}
