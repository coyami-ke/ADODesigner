using ADODesigner.Models;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.ViewModels.Messengers
{
    public class CreateProjectMessenger : ValueChangedMessage<Project>
    {
        public CreateProjectMessenger(Project value) : base(value)
        {
        }
    }
}
