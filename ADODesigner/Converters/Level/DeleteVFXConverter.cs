using ADODesigner.Converters.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Converters.Level
{
    public class DeleteVFXConverter : ILevelConverter
    {
        public CustomLevel Convert(CustomLevel value)
        {
            CustomLevelParser parser = new(value);
            Twirl[] twirls = parser.GetEvents<Twirl>("Twirl");
            SetSpeed[] rabbits = parser.GetEvents<SetSpeed>("SetSpeed");
            parser.ADOFAILevel.Decorations.Clear();
            parser.ADOFAILevel.Actions.Clear();
            parser.AddEvents(twirls);
            parser.AddEvents(rabbits);
            return parser.ADOFAILevel;
        }
    }
}
