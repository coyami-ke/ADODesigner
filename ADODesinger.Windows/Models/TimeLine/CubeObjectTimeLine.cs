using ADODesigner.Converters;
using ADODesigner.Models;
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
using ADODesigner.Animations;

namespace ADODesigner.Windows.Models.TimeLine
{
    public class CubeObjectTimeLine : TimeLineElementModel
    {
        public CubeObjectAnimation Animation { get; set; } = new();

        public CubeObjectTimeLine()
        {
            this.IsSupportDuration = false;
            this.SelectedColor = new SolidColorBrush(new Color() { R = 196, G = 153, B = 243, A = 255 });
            this.UnselectedColor = new SolidColorBrush(new Color() { R = 115, G = 96, B = 223, A = 255 });
            this.Duration = 1;
            this.Floor = 1;
            this.Text = "Cube object";
            this.SaveID = "cubeObjects";
            this.Select();
        }

        public override JsonObject[] GetJsonActions()
        {
            return Array.Empty<JsonObject>();
        }

        public override JsonObject[] GetJsonDecorations()
        {
            this.UpdateKeyFrame();
            this.Animation.Floor = this.Floor;
            List<JsonObject> actions = new();
            foreach (var d in this.Animation.Animate())
            {
                JsonNode? node = JsonSerializer.SerializeToNode(DecorationConverter.Convert(d), ADODesignerFile.GetDefaultOptions());
                if (node is null) continue;
                actions.Add(node.AsObject());
            }
            return actions.ToArray();
        }
        public override TimeLineElementModel CloneElement()
        {
            CubeObjectAnimation cloneObject = (CubeObjectAnimation)this.Animation.Clone();

            CubeObjectTimeLine clone = new();
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
        protected override void OnSelected()
        {
            UpdateKeyFrame();
        }
        protected override void OnUnselected()
        {
            UpdateKeyFrame();
        }
        public override object? GetEditableObject()
        {
            return this.Animation;
        }
    }
}
