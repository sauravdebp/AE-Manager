﻿#pragma checksum "..\..\..\UserControls\OrderFormUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D3F3B4E650F330E238EAAA009BC9345A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Manager.UserControls;
using Manager.Utilities.Converters;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Manager.UserControls {
    
    
    /// <summary>
    /// OrderFormUserControl
    /// </summary>
    public partial class OrderFormUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 61 "..\..\..\UserControls\OrderFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox combo_ItemsList;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\UserControls\OrderFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox text_Qty;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\UserControls\OrderFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox text_Rate;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\UserControls\OrderFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button but_AddOrderItem;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Manager;component/usercontrols/orderformusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\OrderFormUserControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.combo_ItemsList = ((System.Windows.Controls.ComboBox)(target));
            
            #line 63 "..\..\..\UserControls\OrderFormUserControl.xaml"
            this.combo_ItemsList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.combo_ItemsList_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 64 "..\..\..\UserControls\OrderFormUserControl.xaml"
            this.combo_ItemsList.KeyDown += new System.Windows.Input.KeyEventHandler(this.combo_ItemsList_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.text_Qty = ((System.Windows.Controls.TextBox)(target));
            
            #line 76 "..\..\..\UserControls\OrderFormUserControl.xaml"
            this.text_Qty.KeyDown += new System.Windows.Input.KeyEventHandler(this.text_Qty_KeyDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.text_Rate = ((System.Windows.Controls.TextBox)(target));
            
            #line 82 "..\..\..\UserControls\OrderFormUserControl.xaml"
            this.text_Rate.KeyDown += new System.Windows.Input.KeyEventHandler(this.text_Rate_KeyDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.but_AddOrderItem = ((System.Windows.Controls.Button)(target));
            
            #line 103 "..\..\..\UserControls\OrderFormUserControl.xaml"
            this.but_AddOrderItem.Click += new System.Windows.RoutedEventHandler(this.but_AddOrderItem_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

