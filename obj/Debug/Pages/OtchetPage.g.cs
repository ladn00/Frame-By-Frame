﻿#pragma checksum "..\..\..\Pages\OtchetPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E49FC91F78856CA6719F2D4C91F4425A35D308E53B77AF831CC569AB94EF1E16"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignExtensions.Controls;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using Microsoft.Windows.Themes;
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
using Фотостудия.Pages;


namespace Фотостудия.Pages {
    
    
    /// <summary>
    /// OtchetPage
    /// </summary>
    public partial class OtchetPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 228 "..\..\..\Pages\OtchetPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb1;
        
        #line default
        #line hidden
        
        
        #line 246 "..\..\..\Pages\OtchetPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rb1;
        
        #line default
        #line hidden
        
        
        #line 247 "..\..\..\Pages\OtchetPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rb2;
        
        #line default
        #line hidden
        
        
        #line 248 "..\..\..\Pages\OtchetPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb2;
        
        #line default
        #line hidden
        
        
        #line 262 "..\..\..\Pages\OtchetPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel sp1;
        
        #line default
        #line hidden
        
        
        #line 266 "..\..\..\Pages\OtchetPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker date1;
        
        #line default
        #line hidden
        
        
        #line 272 "..\..\..\Pages\OtchetPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker date2;
        
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
            System.Uri resourceLocater = new System.Uri("/Фотостудия;component/pages/otchetpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\OtchetPage.xaml"
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
            
            #line 213 "..\..\..\Pages\OtchetPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.bt_excel);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 216 "..\..\..\Pages\OtchetPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.bt_word);
            
            #line default
            #line hidden
            return;
            case 3:
            this.cb1 = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.rb1 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 246 "..\..\..\Pages\OtchetPage.xaml"
            this.rb1.Checked += new System.Windows.RoutedEventHandler(this.rb1_Checked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.rb2 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 247 "..\..\..\Pages\OtchetPage.xaml"
            this.rb2.Checked += new System.Windows.RoutedEventHandler(this.rb2_Chcked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.cb2 = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.sp1 = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 8:
            this.date1 = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 9:
            this.date2 = ((System.Windows.Controls.DatePicker)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

