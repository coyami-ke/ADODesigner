using ADODesigner.Models;
using ADODesinger.Windows.ViewModels.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ADODesinger.Windows.Models
{
    public abstract partial class TimeLineElementModel : ObservableObject
    {
        [ObservableProperty]
        [property: JsonIgnore]
        private string text = "";
        [ObservableProperty]
        [property: JsonIgnore]
        private Brush unselectedColor = Brushes.DarkGray;
        [ObservableProperty]
        [property: JsonIgnore]
        private Brush selectedColor = Brushes.LightGray;
        [ObservableProperty]
        private float duration;
        [ObservableProperty]
        private int floor;
        [ObservableProperty]
        private int numberTimeLine;
        [ObservableProperty]
        [property: JsonIgnore]
        private bool isSelected;
        [ObservableProperty]
        [property: JsonIgnore]
        private string saveID = "unknown";
        [ObservableProperty]
        [property: JsonIgnore]
        private Thickness margin = new();
        [ObservableProperty]
        private Ease ease;
        [ObservableProperty]
        private bool isSupportDuration = true;

        [JsonConstructor]
        public TimeLineElementModel()
        {
        }
        public abstract JsonObject[] GetJsonActions();
        public abstract JsonObject[] GetJsonDecorations();
        public abstract TimeLineElementModel CloneElement();

        [RelayCommand]
        [property: JsonIgnore]
        public void Select()
        {
            IsSelected = true;
            WeakReferenceMessenger.Default.Send(new NeedChangePropertiesWindow(this));
        }
        [RelayCommand]
        [property: JsonIgnore]
        public void Unselect()
        {
            IsSelected = false;
            WeakReferenceMessenger.Default.Send(new NeedChangePropertiesWindow(this));
        }
        [RelayCommand]
        [property: JsonIgnore]
        public void UnselectWithoutUpdateProperties()
        {
            IsSelected = false;
        }

        [RelayCommand]
        [property: JsonIgnore]
        public void SelectWithShift()
        {
            this.Select();
        }
        [RelayCommand]
        [property: JsonIgnore]
        public void SelectWithoutShift()
        {
            WeakReferenceMessenger.Default.Send(new NeedUnselectElementsMessage(this));
            this.Select();
        }
        public virtual object? GetEditableObject()
        {
            return null;
        }
        public virtual string GetText() { return ""; }
        public virtual Ease GetEase() { return Ease.Linear; }
    }
}
