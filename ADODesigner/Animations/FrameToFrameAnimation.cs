using ADODesigner.Converters.Events;
using ADODesigner.Models;
using ADODesigner.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Animations
{
    public class FrameToFrameAnimation : ICloneable
    {
        public string Tag { get; set; } = "";
        public string ImageName { get; set; } = "";
        public string FileExtension { get; set; } = "png";
        public float AngleOffset { get; set; }
        [UsageWindowProperties(Name = "Addable Angle")]
        public float CornerAnglePerDeco { get; set; } = 90;
        public int CountFrames { get; set; } = 0;
        [UsageWindowProperties(AddToWindowProperties = false)]
        public int Floor { get; set; } = 1;
        public int Repetitions { get; set; } = 0;
        public string EventTag { get; set; } = "";
        public (KeyFrame[], RepeatEvents?) Animate()
        {
            List<KeyFrame> frames = new();
            RepeatEvents? repeatEvent = null;
            for (int i = 0; i < this.CountFrames; i++)
            {
                KeyFrame frame = new()
                {
                    UsePositionOffset = false,
                    UseImage = true,
                    AngleOffset = i * CornerAnglePerDeco,
                    Image = $"{ImageName}{i + 1}.{FileExtension}",
                    EventTag = EventTag,
                    Tag = Tag,
                    Floor = Floor,
                    Duration = 0,
                };
                frames.Add(frame);
            }
            if (Repetitions > 0)
            {
                RepeatEvents repeat = new()
                {
                    Repetitions = Repetitions,
                    Floor = Floor,
                    Interval = CountFrames * CornerAnglePerDeco / 180,
                    Tag = EventTag,
                };
                repeatEvent = repeat;
            }
            return (frames.ToArray(), repeatEvent);
        }

        public object Clone()
        {
            return (FrameToFrameAnimation)MemberwiseClone();
        }
    }
}
