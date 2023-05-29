using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace ADODesigner.Models
{
    public class ConfigurationModel
    {
        public ObservableCollection<Project> Projects { get; set; }
    }
}
