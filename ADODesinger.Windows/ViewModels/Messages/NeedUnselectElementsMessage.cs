﻿using ADODesinger.Windows.Models;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesinger.Windows.ViewModels.Messages
{
    public class NeedUnselectElementsMessage : ValueChangedMessage<TimeLineElementModel>
    {
        public NeedUnselectElementsMessage(TimeLineElementModel value) : base(value)
        {
        }
    }
}
