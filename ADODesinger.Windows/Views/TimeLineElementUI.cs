using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Media;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Media.SpeechRecognition;
using Windows.UI.Notifications;
using System.ComponentModel;
using ADODesigner.Models;
using ADODesigner.Animations;

namespace ADODesinger.Windows.Views
{
    public class TimeLineElementUI : FrameworkElement
    {
        public static readonly DependencyProperty SelectedBrushProperty;
        public static readonly DependencyProperty UnselectedBrushProperty;

        public static readonly DependencyProperty TextProperty;

        public static readonly DependencyProperty IsSelectedProperty;

        public static readonly DependencyProperty EaseProperty;

        static TimeLineElementUI()
        {
            FrameworkPropertyMetadata selectBrushMeta = new(Brushes.Transparent, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender);
            SelectedBrushProperty = DependencyProperty.Register("SelectedBrush", typeof(Brush), typeof(TimeLineElementUI), selectBrushMeta);
            FrameworkPropertyMetadata unselectBrushMeta = new(Brushes.Transparent, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender);
            UnselectedBrushProperty = DependencyProperty.Register("UnselectedBrush", typeof(Brush), typeof(TimeLineElementUI), unselectBrushMeta);

            FrameworkPropertyMetadata textMeta = new("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender);
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TimeLineElementUI));

            FrameworkPropertyMetadata boolMeta = new(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender);
            IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(TimeLineElementUI), boolMeta);

            FrameworkPropertyMetadata easeMeta = new(Ease.Linear, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender);
            EaseProperty = DependencyProperty.Register("Ease", typeof(Ease), typeof(TimeLineElementUI));
        }
        [Bindable(true, BindingDirection.TwoWay)]
        public Brush SelectedBrush
        {
            get { return (Brush)GetValue(SelectedBrushProperty); }
            set
            { 
                SetValue(SelectedBrushProperty, value);
            }
        }
        [Bindable(true, BindingDirection.TwoWay)]
        public Brush UnselectedBrush
        {
            get { return (Brush)GetValue(UnselectedBrushProperty); }
            set 
            { 
                SetValue(UnselectedBrushProperty, value);
            }
        }
        [Bindable(true, BindingDirection.TwoWay)]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set 
            { 
                SetValue(TextProperty, value);
            }
        }
        [Bindable(true, BindingDirection.TwoWay)]
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set 
            { 
                SetValue(IsSelectedProperty, value);
            }
        }
        [Bindable(true, BindingDirection.TwoWay)]
        public Ease Ease
        {
            get { return (Ease)GetValue(EaseProperty); }
            set { SetValue(EaseProperty, value); }
        }
        protected override void OnRender(System.Windows.Media.DrawingContext dc)
        {
            Brush selection = UnselectedBrush;
            if (IsSelected) selection = SelectedBrush;

            Rect rect = new();
            rect.Height = Height;
            rect.Width = Width;
            dc.DrawRoundedRectangle(selection, new Pen(), rect, 0, 0);
            Rect rect1 = new Rect();
            rect1.Height = Height;
            rect1.Width = 20;    
            dc.DrawRoundedRectangle(selection, new Pen(), rect1, 0, 0);

            SolidColorBrush lineColor = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 110,});

            Point lastPoint = new(0, this.Height);
            for (int i = 0; i < 100; i++)
            {
                float tx = MathFunctions.Normalize(i, 0, 100);
                float x = ((float)this.Width) * tx;

                float ty = 1 - EasingFunctions.ApplyFunction(this.Ease, MathFunctions.Normalize(i, 0, 100));
                float y = ((float)this.Height) * ty;

                dc.DrawLine(new Pen(lineColor, 3), lastPoint, new Point(x, y));

                lastPoint = new Point(x, y);
            }
            dc.DrawText(new(Text, System.Globalization.CultureInfo.CurrentCulture, 0, new(""), 15, new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 220})), new Point(10, 10));
        }
    }
}
