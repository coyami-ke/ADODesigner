using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Reflection
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UsageWindowPropertiesAttribute : Attribute
    {
        public bool AddToWindowProperties { get; set; } = true;
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public bool CanDisableProperties { get; set; } = false;
        public bool IsColor { get; set; } = false;
        public bool IsImage { get; set; } = false;
        public string? LocalizationProperty { get; set; } = null;
    }
}
