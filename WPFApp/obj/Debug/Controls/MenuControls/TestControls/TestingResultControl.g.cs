﻿#pragma checksum "..\..\..\..\..\Controls\MenuControls\TestControls\TestingResultControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "46B3C5B75F3336FE319A9C4E24A02959B3FBBAA09586AEE22F0D0AF1ECDCBC46"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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
using WPFApp.Controls.MenuControls.TestControls;


namespace WPFApp.Controls.MenuControls.TestControls {
    
    
    /// <summary>
    /// TestingResultControl
    /// </summary>
    public partial class TestingResultControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\..\..\Controls\MenuControls\TestControls\TestingResultControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CtrlTitle;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\..\Controls\MenuControls\TestControls\TestingResultControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CtrlTime;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\..\Controls\MenuControls\TestControls\TestingResultControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CtrlMark;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\..\Controls\MenuControls\TestControls\TestingResultControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label CtrlAttempts;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\..\Controls\MenuControls\TestControls\TestingResultControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CtrlReTesting;
        
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
            System.Uri resourceLocater = new System.Uri("/WPFApp;component/controls/menucontrols/testcontrols/testingresultcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Controls\MenuControls\TestControls\TestingResultControl.xaml"
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
            this.CtrlTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.CtrlTime = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.CtrlMark = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            
            #line 26 "..\..\..\..\..\Controls\MenuControls\TestControls\TestingResultControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CtrlBackToMenu_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CtrlAttempts = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.CtrlReTesting = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\..\..\Controls\MenuControls\TestControls\TestingResultControl.xaml"
            this.CtrlReTesting.Click += new System.Windows.RoutedEventHandler(this.CtrlReTesting_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
