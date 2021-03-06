﻿#pragma checksum "..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "55A22AF30C12EE6FAD17625703976532FAFD0860"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.Controls;
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
using WpfApplication3;


namespace WpfApplication3 {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox userbox;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox passbox;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock FPblock;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CAblock;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApplication3;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
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
            this.userbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 34 "..\..\MainWindow.xaml"
            this.userbox.GotFocus += new System.Windows.RoutedEventHandler(this.UserBoxFocus);
            
            #line default
            #line hidden
            
            #line 34 "..\..\MainWindow.xaml"
            this.userbox.LostFocus += new System.Windows.RoutedEventHandler(this.UserBoxLost);
            
            #line default
            #line hidden
            return;
            case 2:
            this.passbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 36 "..\..\MainWindow.xaml"
            this.passbox.GotFocus += new System.Windows.RoutedEventHandler(this.PassBoxFocus);
            
            #line default
            #line hidden
            
            #line 36 "..\..\MainWindow.xaml"
            this.passbox.LostFocus += new System.Windows.RoutedEventHandler(this.PassBoxLost);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 39 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.ButtonColorBlack);
            
            #line default
            #line hidden
            
            #line 39 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.ButtonColorWhite);
            
            #line default
            #line hidden
            return;
            case 4:
            this.FPblock = ((System.Windows.Controls.TextBlock)(target));
            
            #line 47 "..\..\MainWindow.xaml"
            this.FPblock.MouseEnter += new System.Windows.Input.MouseEventHandler(this.FPChangeTextWhite);
            
            #line default
            #line hidden
            
            #line 47 "..\..\MainWindow.xaml"
            this.FPblock.MouseLeave += new System.Windows.Input.MouseEventHandler(this.FPChangeTextBlack);
            
            #line default
            #line hidden
            
            #line 47 "..\..\MainWindow.xaml"
            this.FPblock.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.OpenForgotPassWindow);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CAblock = ((System.Windows.Controls.TextBlock)(target));
            
            #line 48 "..\..\MainWindow.xaml"
            this.CAblock.MouseEnter += new System.Windows.Input.MouseEventHandler(this.CAChangeTextWhite);
            
            #line default
            #line hidden
            
            #line 48 "..\..\MainWindow.xaml"
            this.CAblock.MouseLeave += new System.Windows.Input.MouseEventHandler(this.CAChangeTextBlack);
            
            #line default
            #line hidden
            
            #line 48 "..\..\MainWindow.xaml"
            this.CAblock.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.OpenCreateAccountWindow);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

