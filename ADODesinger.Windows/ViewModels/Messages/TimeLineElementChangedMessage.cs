using ADODesinger.Windows.Models;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Windows.ViewModels.Messages
{
    public class TimeLineElementChangedMessage : ValueChangedMessage<TimeLineElementModel>
    {
        public TimeLineElementChangedMessage(TimeLineElementModel value) : base(value)
        {

        }
    }
}
