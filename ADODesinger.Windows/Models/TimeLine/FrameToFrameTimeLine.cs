using ADODesigner.Animations;
using ADODesigner.Converters;
using ADODesinger.Windows.Models;
using ADODesinger.Windows.Models.TimeLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ADODesigner.Windows.Models.TimeLine
{
    public class FrameToFrameTimeLine : TimeLineElementModel
    {
        public FrameToFrameAnimation Animation { get; set; } = new();

        public FrameToFrameTimeLine()
        {
            this.SelectedColor = new SolidColorBrush(new Color() { R = 206, G = 222, B = 189, A = 255 });
            this.UnselectedColor = new SolidColorBrush(new Color() { R = 148, G = 169, B = 122, A = 255 });
            this.Duration = 0;
            this.Floor = 1;
            this.Text = "Frame To Frame Animation";
            this.SaveID = "frameToFrameAnimations";
            this.Select();
            this.IsSupportDuration = false;
        }

        public override JsonObject[] GetJsonActions()
        {
            this.UpdateKeyFrame();
            this.Animation.Floor = this.Floor;
            List<JsonObject> actions = new();
            foreach (var k in this.Animation.Animate().Item1)
            {
                JsonNode? node = JsonSerializer.SerializeToNode(KeyFrameConverter.Convert(k), ADODesignerFile.GetDefaultOptions());
                if (node is null) continue;
                actions.Add(node.AsObject());
            }
            if (this.Animation.Animate().Item2 is not null)
            {
                JsonNode? repeat = JsonSerializer.SerializeToNode(this.Animation.Animate().Item2, ADODesignerFile.GetDefaultOptions());
                if (repeat is not null) actions.Add(repeat.AsObject());
            }
            return actions.ToArray();
        }

        public override JsonObject[] GetJsonDecorations()
        {
            return Array.Empty<JsonObject>();
        }
        public override TimeLineElementModel CloneElement()
        {
            FrameToFrameAnimation cloneObject = (FrameToFrameAnimation)this.Animation.Clone();

            FrameToFrameTimeLine clone = new();
            clone.Duration = this.Duration;
            clone.Floor = this.Floor;
            clone.NumberTimeLine = this.NumberTimeLine;
            clone.Animation = cloneObject;
            return clone;
        }
        private void UpdateKeyFrame()
        {
            this.Text = this.Animation.Tag;
        }
        public override object? GetEditableObject()
        {
            return this.Animation;
        }
        public override string GetText()
        {
            return this.Animation.Tag;
        }
    }
}
