﻿using ADODesigner.ViewModels;
using ADODesigner.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ADODesigner.Core.API
{
    public static class EditorAPI
    {
        public enum MenuCategory
        {
            File,
            Edit,
            Project,
        }
        internal static EditorView Editor { get; set; }
        static EditorAPI()
        {
            Editor = new();
            Editor.DataContext = new EditorVM();
        }
        #region KeyBindings
        public static void AddHotKeyToTimeLine(Action action, Key key, ModifierKeys modifiers)
        {
            KeyGesture keyGesture = new KeyGesture(key, modifiers);
            InputBinding inputBinding = new(new RelayCommand(action), keyGesture);
            Editor.PART_timeLine.InputBindings.Add(inputBinding);
        }
        public static void AddHotKeyToDesigner(Action action, Key key, ModifierKeys modifiers)
        {
            KeyGesture keyGesture = new KeyGesture(key, modifiers);
            InputBinding inputBinding = new(new RelayCommand(action), keyGesture);
            Editor.PART_timeLine.InputBindings.Add(inputBinding);
        }
        public static void AddGlobalHotKey(Action action, Key key, ModifierKeys modifiers)
        {
            KeyGesture keyGesture = new KeyGesture(key, modifiers);
            InputBinding inputBinding = new(new RelayCommand(action), keyGesture);
            Editor.InputBindings.Add(inputBinding);
        }
        public static void AddGlobalHotKeyWithMenuItem(Action action, Key key, ModifierKeys modifiers, MenuItem menuItem, MenuCategory category)
        {
            KeyGesture keyGesture = new KeyGesture(key, modifiers);
            InputBinding inputBinding = new(new RelayCommand(action), keyGesture);
            Editor.InputBindings.Add(inputBinding);

            MenuItem item = menuItem;
            item.InputGestureText = keyGesture.DisplayString;
            item.Command = new RelayCommand(action);

            if (category == MenuCategory.File) Editor.PART_menuFile.Items.Add(item);
            if (category == MenuCategory.Edit) Editor.PART_menuEdit.Items.Add(item);
            if (category == MenuCategory.Project) Editor.PART_menuProject.Items.Add(item);
        }
        #endregion
    }
}