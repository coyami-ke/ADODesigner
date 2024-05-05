using ADODesigner.Reflection;
using ADODesinger.Windows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Runtime.CompilerServices;
using NCalc;
using System.Numerics;
using System.Xml.Linq;
using ADODesinger.Windows.ViewModels.Messages;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Documents;
using ADODesigner.Models;
using System.Windows.Media.Effects;

namespace ADODesinger.Windows.Helpers
{
    public class PropertiesWindow
    {
        public static StackPanel FromTimeLineElement(TimeLineElementModel element)
        {
            StackPanel panel = new();

            if (element.IsSupportDuration)
            {
                TextBlock blockDuration = CreateBaseTextBlock();
                blockDuration.Text = "Duration";
                TextBox boxDuration = CreateBaseTextBox();
                boxDuration.Text = element.Duration.ToString(System.Globalization.CultureInfo.InvariantCulture);
                boxDuration.LostFocus += (sender, e) =>
                {
                    WeakReferenceMessenger.Default.Send(new NeedAddToBuffer(null));
                    float result = NCalcHelper.GetFloatFromExperession(boxDuration.Text);
                    boxDuration.Text = result.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    element.Duration = result;
                };
                Border borderDuration = CreateBorder(new UIElement[] { blockDuration, boxDuration, });
                panel.Children.Add(borderDuration);
            }

            object? obj = element.GetEditableObject();
            if (obj is null) return panel;

            Type type = obj.GetType();

            foreach (var prop in type.GetProperties())
            {
                UsageWindowPropertiesAttribute? att = prop.GetCustomAttribute<UsageWindowPropertiesAttribute>();
                string name = "";
                string nameSwitch = "";
                if (att is not null)
                {
                    if (att.AddToWindowProperties == false) continue;
                    if (!string.IsNullOrEmpty(att.Name)) name = att.Name;
                }
                else name = prop.Name;

                if (prop.GetValue(obj) is string str)
                {
                    TextBlock block = CreateBaseTextBlock();
                    block.Text = name;
                    TextBox box = CreateBaseTextBox();
                    box.Text = str;
                    box.LostFocus += (sender, e) =>
                    {
                        WeakReferenceMessenger.Default.Send(new NeedAddToBuffer(null));
                        prop.SetValue(obj, box.Text);
                    };
                    Border border = CreateBorder(new UIElement[] { block, box,});
                    panel.Children.Add(border);
                }
                else if (prop.GetValue(obj) is int int32)
                {
                    TextBlock block = CreateBaseTextBlock();
                    block.Text = name;
                    TextBox box = CreateBaseTextBox();
                    box.Text = int32.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    box.LostFocus += (sender, e) =>
                    {
                        WeakReferenceMessenger.Default.Send(new NeedAddToBuffer(null));
                        int result = Convert.ToInt32(NCalcHelper.GetFloatFromExperession(box.Text));
                        box.Text = result.ToString(System.Globalization.CultureInfo.InvariantCulture);
                        prop.SetValue(obj, result);
                        
                    };
                    Border border = CreateBorder(new UIElement[] { block, box, });
                    panel.Children.Add(border);
                }
                else if (prop.GetValue(obj) is float flt)
                {
                    TextBlock block = CreateBaseTextBlock();
                    block.Text = name;
                    TextBox box = CreateBaseTextBox();
                    box.Text = flt.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    box.LostFocus += (sender, e) =>
                    {
                        WeakReferenceMessenger.Default.Send(new NeedAddToBuffer(null));
                        float result = NCalcHelper.GetFloatFromExperession(box.Text);
                        box.Text = result.ToString(System.Globalization.CultureInfo.InvariantCulture);
                        prop.SetValue(obj, result);
                        
                    };
                    Border border = CreateBorder(new UIElement[] { block, box, });
                    panel.Children.Add(border);
                }
                else if (prop.GetValue(obj) is Vector2 vec2)
                {
                    TextBlock block = CreateBaseTextBlock();
                    block.Text = name;
                    TextBox boxX = CreateBaseTextBox();
                    boxX.Width = 80;
                    boxX.Text = vec2.X.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    TextBox boxY = CreateBaseTextBox();
                    boxY.Text = vec2.Y.ToString(System.Globalization.CultureInfo.InvariantCulture);
                    boxY.Width = 80;
                    boxY.Margin = new(270, 0, 0 , 0);
                    boxX.LostFocus += (sender, e) =>
                    {
                        WeakReferenceMessenger.Default.Send(new NeedAddToBuffer(null));
                        Vector2 result = new(NCalcHelper.GetFloatFromExperession(boxX.Text), NCalcHelper.GetFloatFromExperession(boxY.Text));
                        boxX.Text = result.X.ToString(System.Globalization.CultureInfo.InvariantCulture);
                        boxY.Text = result.Y.ToString(System.Globalization.CultureInfo.InvariantCulture);
                        prop.SetValue(obj, result);
                        
                    };
                    boxY.LostFocus += (sender, e) => 
                    {
                        WeakReferenceMessenger.Default.Send(new NeedAddToBuffer(null));
                        Vector2 result = new(NCalcHelper.GetFloatFromExperession(boxX.Text), NCalcHelper.GetFloatFromExperession(boxY.Text));
                        boxX.Text = result.X.ToString(System.Globalization.CultureInfo.InvariantCulture);
                        boxY.Text = result.Y.ToString(System.Globalization.CultureInfo.InvariantCulture);
                        prop.SetValue(obj, result);
                    };

                    Border border = CreateBorder(new UIElement[] { block, boxX, boxY });
                    panel.Children.Add(border);
                }
                else if (prop.GetValue(obj) is bool bl)
                {
                    CheckBox box = new()
                    {
                        IsChecked = bl,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        Content = name,
                        Foreground = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 255, }),
                        Opacity = 0.85f,
                    };
                    box.Checked += (sender, e) =>
                    {
                        WeakReferenceMessenger.Default.Send(new NeedAddToBuffer(null));
                        prop.SetValue(obj, box.IsChecked);
                    };
                    box.Unchecked += (sender, e) =>
                    {
                        WeakReferenceMessenger.Default.Send(new NeedAddToBuffer(null));
                        prop.SetValue(obj, box.IsChecked);
                    };
                    Border border = CreateBorder(new UIElement[] { box });
                    if (att is not null && att.CanDisableProperties) border.Background = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 8 });
                    panel.Children.Add(border);
                }
                else if (prop.GetValue(obj) is Ease ease)
                {
                    TextBlock block = CreateBaseTextBlock();
                    block.Text = name;
                    TextBox box = CreateBaseTextBox();
                    box.Text = ease.ToString();
                    box.LostFocus += (sender, e) =>
                    {
                        WeakReferenceMessenger.Default.Send(new NeedAddToBuffer(null));
                        object? result;
                        if (Enum.TryParse(typeof(Ease), box.Text, true, out result))
                        {
                            Ease conv = (Ease)result;
                            prop.SetValue(obj, result);
                            box.Text = result.ToString();
                        }
                    };
                    Border border = CreateBorder(new UIElement[] { block, box, });
                    panel.Children.Add(border);
                }
            }
            return panel;
        }

        public static TextBox CreateBaseTextBox()
        {
            TextBox box = new()
            {
                Margin = new(150, 0, 0, 0),
                Width = 200,
                Height = 40,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 16,
                Opacity = 0.85f,
                Foreground = Brushes.White,
            };
            return box;
        }
        public static TextBlock CreateBaseTextBlock()
        {
            TextBlock block = new()
            {
                Foreground = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 255, }),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 16,
                Margin = new(10, 0, 0, 0),
                Opacity = 0.85f,
            };
            return block;
        }
        public static Border CreateBorder(UIElement[] elements)
        {
            Grid grid = new Grid();
            foreach (var e in elements)
            {
                grid.Children.Add(e);
            }
            Border border = new()
            {
                Background = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 4, }),
                BorderBrush = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 8, }),
                Height = 40,
                Width = 400,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                CornerRadius = new(5),
                BorderThickness = new(0.75f),
            };
            border.Child = grid;
            return border;
        }
    }
}
