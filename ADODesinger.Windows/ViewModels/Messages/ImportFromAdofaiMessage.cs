using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesinger.Windows.ViewModels.Messages
{
    public class ImportFromAdofaiMessage : ValueChangedMessage<ImportFromAdofaiViewModel>
    {
        public ImportFromAdofaiMessage(ImportFromAdofaiViewModel value) : base(value)
        {
        }
    }
}
