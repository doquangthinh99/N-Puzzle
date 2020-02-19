﻿#pragma checksum "..\..\PlayImageWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6C7C08189555A751B89D003D97FF82258CD9DD37E9D870176CFC09F3AAF03ED6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using N_Puzzle;
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


namespace N_Puzzle {
    
    
    /// <summary>
    /// PlayImageWindow
    /// </summary>
    public partial class PlayImageWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\PlayImageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas ImageCanvas;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\PlayImageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu MainMenu;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\PlayImageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem NewGameItem;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\PlayImageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem PlayItem;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\PlayImageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem SaveItem;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\PlayImageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem LoadItem;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\PlayImageWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas previewCanvas;
        
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
            System.Uri resourceLocater = new System.Uri("/N-Puzzle;component/playimagewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PlayImageWindow.xaml"
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
            
            #line 8 "..\..\PlayImageWindow.xaml"
            ((N_Puzzle.PlayImageWindow)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.Window_MouseMove);
            
            #line default
            #line hidden
            
            #line 8 "..\..\PlayImageWindow.xaml"
            ((N_Puzzle.PlayImageWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ImageCanvas = ((System.Windows.Controls.Canvas)(target));
            return;
            case 3:
            this.MainMenu = ((System.Windows.Controls.Menu)(target));
            return;
            case 4:
            this.NewGameItem = ((System.Windows.Controls.MenuItem)(target));
            
            #line 13 "..\..\PlayImageWindow.xaml"
            this.NewGameItem.Click += new System.Windows.RoutedEventHandler(this.NewGame_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PlayItem = ((System.Windows.Controls.MenuItem)(target));
            
            #line 14 "..\..\PlayImageWindow.xaml"
            this.PlayItem.Click += new System.Windows.RoutedEventHandler(this.PlayItem_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SaveItem = ((System.Windows.Controls.MenuItem)(target));
            
            #line 15 "..\..\PlayImageWindow.xaml"
            this.SaveItem.Click += new System.Windows.RoutedEventHandler(this.SaveItem_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.LoadItem = ((System.Windows.Controls.MenuItem)(target));
            
            #line 16 "..\..\PlayImageWindow.xaml"
            this.LoadItem.Click += new System.Windows.RoutedEventHandler(this.LoadItem_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.previewCanvas = ((System.Windows.Controls.Canvas)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

