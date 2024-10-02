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
using ADODesigner.Windows.ViewModels.Messages;
using WPFColorLib;
using ADODesigner.Windows.Helpers;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;

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
                blockDuration.Text = App.Localization.TimeLineElements.GetLocalization("Duration");
                TextBox boxDuration = CreateBaseTextBox();
                boxDuration.Text = element.Duration.ToString(System.Globalization.CultureInfo.InvariantCulture);
                boxDuration.LostFocus += (sender, e) =>
                {
                    WeakReferenceMessenger.Default.Send(new TimeLineElementChangedMessage(element));
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
                    string? nameLocalization = App.Localization.TimeLineElements.GetLocalization(att.LocalizationProperty);
                    if (nameLocalization is not null)
                    {
                        name = nameLocalization;
                    }
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
                        WeakReferenceMessenger.Default.Send(new TimeLineElementChangedMessage(element));
                        prop.SetValue(obj, box.Text);
                    };
                    Border border;
                    if (att is not null && att.IsColor)
                    {
                        Button button = new()
                        {
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Center,
                            Height = 35,
                            Width = 35,
                            Content = "...",
                            Margin = new(312.5f, 0, 0, 0),
                        };
                        button.Click += (sender, e) =>
                        {
                            Color color;
                            try
                            {
                                color = HexColorConverter.HexToColor(str);
                            }
                            catch (Exception ex)
                            {
                                color = Color.FromArgb(255, 255, 255, 255); 
                            }

                            WPFColorLib.SelectColorDlg dialog = new(color);
                            var result = dialog.ShowDialog();
                            if (result.HasValue && result.Value)
                            {
                                WeakReferenceMessenger.Default.Send(new TimeLineElementChangedMessage(element));
                                Color selectedColor = dialog.SelectedColor;
                                box.Text = HexColorConverter.ColorToHex(selectedColor);
                                str = HexColorConverter.ColorToHex(selectedColor);
                            }
                        };
                        border = CreateBorder(new UIElement[] { block, box, button, });
                    }
                    else if (att is not null && att.IsImage)
                    {
                        Button button = new()
                        {
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Center,
                            Height = 35,
                            Width = 35,
                            Content = "...",
                            Margin = new(312.5f, 0, 0, 0),
                        };
                        button.Click += (sender, e) =>
                        {
                            CommonOpenFileDialog dialog = new("Select Image");
                            dialog.Filters.Add(new("PNG", ".png"));
                            dialog.Filters.Add(new("JPG", ".jpg"));
                            dialog.Filters.Add(new("JPEG", ".jpeg"));
                            var result = dialog.ShowDialog();
                            if (result == CommonFileDialogResult.Ok)
                            {
                                WeakReferenceMessenger.Default.Send(new TimeLineElementChangedMessage(element));
                                box.Text = Path.GetFileName(dialog.FileName);
                            }
                        };
                        border = CreateBorder(new UIElement[] { block, box, button, });
                    }
                    else
                    {
                        border = CreateBorder(new UIElement[] { block, box });
                    }
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
                        WeakReferenceMessenger.Default.Send(new TimeLineElementChangedMessage(element));
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
                        WeakReferenceMessenger.Default.Send(new TimeLineElementChangedMessage(element));
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
                        WeakReferenceMessenger.Default.Send(new TimeLineElementChangedMessage(element));
                        Vector2 result = new(NCalcHelper.GetFloatFromExperession(boxX.Text), NCalcHelper.GetFloatFromExperession(boxY.Text));
                        boxX.Text = result.X.ToString(System.Globalization.CultureInfo.InvariantCulture);
                        boxY.Text = result.Y.ToString(System.Globalization.CultureInfo.InvariantCulture);
                        prop.SetValue(obj, result);
                        
                    };
                    boxY.LostFocus += (sender, e) => 
                    {
                        WeakReferenceMessenger.Default.Send(new TimeLineElementChangedMessage(element));
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
                        WeakReferenceMessenger.Default.Send(new TimeLineElementChangedMessage(element));
                        prop.SetValue(obj, box.IsChecked);
                    };
                    box.Unchecked += (sender, e) =>
                    {
                        WeakReferenceMessenger.Default.Send(new TimeLineElementChangedMessage(element));
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

                    ComboBox comboBox = CreateBaseEaseComboBox();
                    comboBox.SelectedItem = ease;

                    comboBox.SelectionChanged += (sender, e) =>
                    {
                        if (comboBox.SelectedItem is Ease selectedEase)
                        {
                            prop.SetValue(obj, selectedEase);
                            WeakReferenceMessenger.Default.Send(new TimeLineElementChangedMessage(element));
                        }
                    };

                    Border border = CreateBorder(new UIElement[] { block, comboBox });
                    panel.Children.Add(border);
                }
            }
            return panel;
        }
        public static ComboBox CreateBaseEaseComboBox()
        {
            ComboBox comboBox = new ComboBox()
            {
                Margin = new(150, 0, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 16,
                ItemsSource = Enum.GetValues(typeof(Ease)).Cast<Ease>(),
                Width = 200,
                Height = 40,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            return comboBox;
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
