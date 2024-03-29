﻿using Autodesk.Windows;
using System.Windows;
using System.Windows.Interop;


namespace Reivt_LinkMultiple.App.Views
{
    public partial class DebugWindow : Window
    {
        public DebugWindow(string debugText)
        {
            InitializeComponent();

            WindowInteropHelper helper = new WindowInteropHelper(this);
            helper.Owner = ComponentManager.ApplicationWindow;

            text_box.Text = debugText;
        }
    }
}
