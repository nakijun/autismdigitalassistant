﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.42
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SymbolManager.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SymbolManager.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} name can not be empty!.
        /// </summary>
        internal static string CannotBeEmptyMessage {
            get {
                return ResourceManager.GetString("CannotBeEmptyMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Confirm Delete.
        /// </summary>
        internal static string ConfirmDeletionCaption {
            get {
                return ResourceManager.GetString("ConfirmDeletionCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Are you sure you want to delete &apos;{0}&apos;?.
        /// </summary>
        internal static string ConfirmDeletionMessage {
            get {
                return ResourceManager.GetString("ConfirmDeletionMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error.
        /// </summary>
        internal static string ErrorCaption {
            get {
                return ResourceManager.GetString("ErrorCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exported categories: {0}, symbols: {1}.
        /// </summary>
        internal static string ExportingSymbolsStatus {
            get {
                return ResourceManager.GetString("ExportingSymbolsStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to All Supported Graphics(BMP;WMF;PNG;JPG)|*.bmp;*.wmf;*png;*.jpg|All files|*.*&quot;.
        /// </summary>
        internal static string ImageFileDialogFilter {
            get {
                return ResourceManager.GetString("ImageFileDialogFilter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Image has not been set!.
        /// </summary>
        internal static string ImageNotSetMessage {
            get {
                return ResourceManager.GetString("ImageNotSetMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Imported from: {0}.
        /// </summary>
        internal static string ImportedFrom {
            get {
                return ResourceManager.GetString("ImportedFrom", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Imported categories: {0}, symbols: {1}.
        /// </summary>
        internal static string ImportingSymbolsStatus {
            get {
                return ResourceManager.GetString("ImportingSymbolsStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ready.
        /// </summary>
        internal static string Ready {
            get {
                return ResourceManager.GetString("Ready", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Refreshing category list ....
        /// </summary>
        internal static string RefreshCategoryListView {
            get {
                return ResourceManager.GetString("RefreshCategoryListView", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Refreshing symbol list ....
        /// </summary>
        internal static string RefreshSymbolListView {
            get {
                return ResourceManager.GetString("RefreshSymbolListView", resourceCulture);
            }
        }
    }
}