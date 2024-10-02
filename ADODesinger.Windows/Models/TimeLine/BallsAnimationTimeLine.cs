using ADODesigner.Converters;
using ADODesigner.Models;
using ADODesinger.Windows.ViewModels.Messages;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media;
using ADODesigner.Animations;
using ADODesinger.Windows.Helpers;

namespace ADODesinger.Windows.Models.TimeLine
{
    public class BallsAnimationTimeLine : TimeLineElementModel
    {

        public BallsAnimation Animation { get; set; } = new();

        public BallsAnimationTimeLine()
        {
            this.SelectedColor = new SolidColorBrush(new Color() { R = 234, G = 172, B = 89, A = 255 });
            this.UnselectedColor = new SolidColorBrush(new Color() { R = 160, G = 114, B = 53, A = 255 });
            this.Duration = 1;
            this.Floor = 1;
            this.Text = "Balls Animation";
            this.SaveID = "ballsAnimations";
            this.Select();
        }

        public override JsonObject[] GetJsonActions()
        {
            this.UpdateKeyFrame();
            this.Animation.Floor = this.Floor;
            this.Animation.Duration = this.Duration;
            List<JsonObject> actions = new();
            foreach (var k in this.Animation.Animate())
            {
                JsonNode? node = JsonSerializer.SerializeToNode(KeyFrameConverter.Convert(k), ADODesignerFile.GetDefaultOptions());
                if (node is null) continue;
                actions.Add(node.AsObject());
            }
            return actions.ToArray();
        }

        public override JsonObject[] GetJsonDecorations()
        {
            return Array.Empty<JsonObject>();
        }
        public override TimeLineElementModel CloneElement()
        {
            BallsAnimation cloneObject = (BallsAnimation)this.Animation.Clone();

            BallsAnimationTimeLine clone = new();
            clone.Duration = this.Duration;
            clone.Floor = this.Floor;
            clone.NumberTimeLine = this.NumberTimeLine;
            clone.Animation = cloneObject;
            return clone;
        }
        private void UpdateKeyFrame()
        {
            this.Ease = this.Animation.Ease;
        }
        public override object? GetEditableObject()
        {
            return this.Animation;
        }
        public override string GetText()
        {
            return $"{this.Animation.Tag} ({this.Animation.AngleOffset.ToString()}) {this.Animation.Ease}";
        }
        public override Ease GetEase()
        {
            return this.Animation.Ease;
        }
    }
}
