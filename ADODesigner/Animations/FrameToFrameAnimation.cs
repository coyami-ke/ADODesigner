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
        [UsageWindowProperties(LocalizationProperty = "Tag")]
        public string Tag { get; set; } = "";
        [UsageWindowProperties(LocalizationProperty = "Image")]
        public string ImageName { get; set; } = "";
        [UsageWindowProperties(LocalizationProperty = "FileExtension")]
        public string FileExtension { get; set; } = "png";
        [UsageWindowProperties(LocalizationProperty = "AngleOffset")]
        public float AngleOffset { get; set; }
        [UsageWindowProperties(LocalizationProperty = "AddableAngle")]
        public float CornerAnglePerDeco { get; set; } = 90;
        [UsageWindowProperties(LocalizationProperty = "CountFrames")]
        public int CountFrames { get; set; } = 0;
        [UsageWindowProperties(AddToWindowProperties = false)]
        public int Floor { get; set; } = 1;
        [UsageWindowProperties(LocalizationProperty = "Repetitions")]
        public int Repetitions { get; set; } = 0;
        [UsageWindowProperties(LocalizationProperty = "EventTag")]
        public string EventTag { get; set; } = "";
        public (KeyFrame[], RepeatEvents?) Animate()
        {
            List<KeyFrame> frames = new();
            RepeatEvents? repeatEvent = null;
            for (int i = 0; i < this.CountFrames; i++)
            {
                string image;
                if (ImageName.Contains("{n}"))
                {
                    image = ImageName.Replace("{n}", (i + 1).ToString());
                }
                else image = $"{ImageName}{i + 1}.{FileExtension}";

                KeyFrame frame = new()
                {
                    UsePositionOffset = false,
                    UseImage = true,
                    AngleOffset = i * CornerAnglePerDeco,
                    Image = image + $".{FileExtension}",
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
