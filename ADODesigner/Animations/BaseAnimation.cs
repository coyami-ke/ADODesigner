using ADODesigner.Models;
using ADODesigner.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Animations
{
    /// <summary>
    /// Base class for creating procedural animation.
    /// </summary>
    /// <typeparam name="T">Type of arguments</typeparam>
    public abstract class BaseAnimation <T>
    {
        /// <summary>
        /// Arguments for customizing the animation.
        /// </summary>
        public T Args { get; set; }
        /// <summary>
        /// Basic constructor
        /// </summary>
        /// <param name="args">Animation settings</param>
        public BaseAnimation(T args)
        {
            Args = args;
        }
        /// <summary>
        /// Using the arguments, create an animation.
        /// </summary>
        public abstract void CreateAnimation();
    }
}
