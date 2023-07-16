using ADODesigner.Models;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable 1591
namespace ADODesigner.ViewModels.Buffer
{
    /// <summary>
    /// Buffer for storing states.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BufferStates <T>
    {
        /// <summary>
        /// Stack.
        /// </summary>
        public Stack<T> Buffer { get; private set; }
        public BufferStates()
        {
            Buffer = new Stack<T>();
        }
    }
}
