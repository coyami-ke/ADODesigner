using ADODesigner.Converters;
using ADODesigner.Models;
using ADODesinger.Windows.Helpers;
using ADODesinger.Windows.ViewModels.Messages;
using AutoMapper;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ADODesinger.Windows.Models.TimeLine
{
    public class KeyFrameTimeLine : TimeLineElementModel
    {
        public KeyFrame KeyFrame { get; set; } = new();

        public KeyFrameTimeLine()
        {
            this.SelectedColor = new SolidColorBrush(new Color() { R = 125, G = 200, B = 255, A = 255});
            this.UnselectedColor = new SolidColorBrush(new Color() { R = 78, G = 122, B = 194, A = 255 });
            this.Duration = 1;
            this.Floor = 1;
            this.Text = "Key Frame";
            this.SaveID = "keyFrames";
            this.Select();
        }

        public override JsonObject[] GetJsonActions()
        {
            this.UpdateKeyFrame();
            this.KeyFrame.Floor = this.Floor;
            this.KeyFrame.Duration = this.Duration;
            JsonNode? node = JsonSerializer.SerializeToNode(KeyFrameConverter.Convert(this.KeyFrame), ADODesignerFile.GetDefaultOptions());
            if (node is null) return Array.Empty<JsonObject>();
            return new JsonObject[] { node.AsObject(), };
        }

        public override JsonObject[] GetJsonDecorations()
        {
            return Array.Empty<JsonObject>();
        }
        public override TimeLineElementModel CloneElement()
        {
            KeyFrame cloneObject = (KeyFrame)this.KeyFrame.Clone();

            KeyFrameTimeLine clone = new();
            clone.Duration = this.Duration;
            clone.Floor = this.Floor;
            clone.NumberTimeLine = this.NumberTimeLine;
            clone.KeyFrame = cloneObject;
            return clone;
        }
        private void UpdateKeyFrame()
        {
            this.Ease = this.KeyFrame.Ease;
            this.Text = $"{this.KeyFrame.Tag} ({this.KeyFrame.AngleOffset.ToString()}) {this.KeyFrame.Ease}";
        }
        public override object? GetEditableObject()
        {
            return this.KeyFrame;
        }
        public override string GetText()
        {
            return $"{this.KeyFrame.Tag} ({this.KeyFrame.AngleOffset.ToString()}) {this.KeyFrame.Ease}";
        }
        public override Ease GetEase()
        {
            return this.KeyFrame.Ease;
        }
    }
}
