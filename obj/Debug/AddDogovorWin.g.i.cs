﻿#pragma checksum "..\..\AddDogovorWin.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9AD835BE31F4CD563C0D42EB3A356D9C008887DA7A7BD2DE81C750AAD67CD07C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

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
using Фотостудия;


namespace Фотостудия {
    
    
    /// <summary>
    /// AddDogovorWin
    /// </summary>
    public partial class AddDogovorWin : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\AddDogovorWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_Clients;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\AddDogovorWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_Photog;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\AddDogovorWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_Usluga;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\AddDogovorWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rb1;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\AddDogovorWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rb2;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\AddDogovorWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_Location;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\AddDogovorWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox newAddress;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\AddDogovorWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbDateStart;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\AddDogovorWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbDateEnd;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\AddDogovorWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_Status;
        
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
            System.Uri resourceLocater = new System.Uri("/Фотостудия;component/adddogovorwin.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddDogovorWin.xaml"
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
            this.cb_Clients = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.cb_Photog = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.cb_Usluga = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.rb1 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 29 "..\..\AddDogovorWin.xaml"
            this.rb1.Checked += new System.Windows.RoutedEventHandler(this.rb1_Checked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.rb2 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 30 "..\..\AddDogovorWin.xaml"
            this.rb2.Checked += new System.Windows.RoutedEventHandler(this.rb2_Checked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.cb_Location = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.newAddress = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.tbDateStart = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.tbDateEnd = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.cb_Status = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 11:
            
            #line 42 "..\..\AddDogovorWin.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.bt_SaveClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
