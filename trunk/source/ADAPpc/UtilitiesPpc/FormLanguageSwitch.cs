using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;

namespace UtilitiesPpc
{
    public class FormLanguageSwitchSingleton
    {

        /// <summary>
        ///     Static read-only instance.
        /// </summary>
        protected static readonly FormLanguageSwitchSingleton m_instance = new FormLanguageSwitchSingleton();

        /// <summary>
        ///     Hidden constructor called during static member <c>m_instance</c> 
        ///     initialization.
        /// </summary>
        protected FormLanguageSwitchSingleton() { }

        /// <summary>
        ///     Gets the only instance of the object.
        /// </summary>
        public static FormLanguageSwitchSingleton Instance
        {
            get { return m_instance; }
        }

        /// <summary>
        ///     Changes the current culture used by the Resource Manager to look 
        ///     up culture-specific resources at run time.
        /// </summary>
        /// <param name="cultureInfoIdentifier">
        ///     A <c>CultureInfo</c> object that will be applied to the 
        ///     appName thread.
        /// </param>
        public void ChangeCurrentCulture(System.Globalization.CultureInfo cultureInfo)
        {
            m_cultureInfo = cultureInfo;
        }

        public bool IsCultureSupported(string cultureName)
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(this.GetType());
            CultureInfo newCulture = new CultureInfo(cultureName);
            try
            {
                object newValue = resources.GetObject("", newCulture);
            }
            catch (PlatformNotSupportedException ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
            catch
            {
            }

            return true;
        }

        /// <summary>
        ///     Changes the language of the <c>Form</c> object provided to the 
        ///     currently selected language.
        /// </summary>
        /// <param name="form">
        ///     <c>Form</c> object to apply changes to.
        /// </param>
        public void ChangeLanguage(System.Windows.Forms.Form form)
        {
            ChangeLanguage(form, m_cultureInfo);
        }

        /// <summary>
        ///     Changes the language of the <c>Form</c> object provided and all 
        ///     its MDI children (in the case of MDI UI) to the currently 
        ///     selected language.
        /// </summary>
        /// <param name="form">
        ///     <c>Form</c> object to apply changes to.
        /// </param>
        /// <param name="cultureInfo">
        ///     <c>CultureInfo</c> to which language has to be changed.
        /// </param>
        public void ChangeLanguage(System.Windows.Forms.Form form, System.Globalization.CultureInfo cultureInfo)
        {
            m_cultureInfo = cultureInfo;
            ChangeFormLanguage(form);
        }

        /// <summary>
        ///     Changes <c>Text</c> properties associated with following 
        ///     controls: <c>AxHost</c>, <c>ButtonBase</c>, <c>GroupBox</c>, 
        ///     <c>Label</c>, <c>ScrollableControl</c>, <c>StatusBar</c>, 
        ///     <c>TabControl</c>, <c>ToolBar</c>. Method is made virtual so it 
        ///     can be overriden in derived class to redefine types.
        /// </summary>
        /// <param name="parent">
        ///     <c>Control</c> object.
        /// </param>
        /// <param name="resources">
        ///     <c>ResourceManager</c> object.
        /// </param>
        protected virtual void ReloadTextForSelectedControls(System.Windows.Forms.Control control, System.Resources.ResourceManager resources)
        {
            if (control is System.Windows.Forms.ButtonBase ||
                control is System.Windows.Forms.Label ||
                control is System.Windows.Forms.ScrollableControl ||
                control is System.Windows.Forms.StatusBar ||
                control is System.Windows.Forms.TabControl ||
                control is System.Windows.Forms.ToolBar)
            {
                control.Text = (string)GetSafeValue(resources, control.Name + ".Text", control.Text);
            }
        }

        /// <summary>
        ///     Reloads properties common to all controls (except the <c>Text</c>
        ///     property).
        /// </summary>
        /// <param name="control">
        ///     <c>Control</c> object for which resources should be reloaded.
        /// </param>
        /// <param name="resources">
        ///     <c>ResourceManager</c> object.
        /// </param>
        protected virtual void ReloadControlCommonProperties(System.Windows.Forms.Control control, System.Resources.ResourceManager resources)
        {
            SetProperty(control, "AccessibleDescription", resources);
            SetProperty(control, "AccessibleName", resources);
            SetProperty(control, "BackgroundImage", resources);
            SetProperty(control, "Font", resources);
            SetProperty(control, "ImeMode", resources);
            SetProperty(control, "RightToLeft", resources);
            SetProperty(control, "Size", resources);
            // following properties are not changed for the form
            if (!(control is System.Windows.Forms.Form))
            {
                SetProperty(control, "Anchor", resources);
                SetProperty(control, "Dock", resources);
                SetProperty(control, "Enabled", resources);
                SetProperty(control, "Location", resources);
                SetProperty(control, "TabIndex", resources);
                SetProperty(control, "Visible", resources);
            }
            if (control is System.Windows.Forms.ScrollableControl)
            {
                ReloadScrollableControlProperties((System.Windows.Forms.ScrollableControl)control, resources);
                if (control is System.Windows.Forms.Form)
                {
                    ReloadFormProperties((System.Windows.Forms.Form)control, resources);
                }
            }
        }

        /// <summary>
        ///     Reloads properties specific to some controls.
        /// </summary>
        /// <param name="control">
        ///     <c>Control</c> object for which resources should be reloaded.
        /// </param>
        /// <param name="resources">
        ///     <c>ResourceManager</c> object.
        /// </param>
        protected virtual void ReloadControlSpecificProperties(System.Windows.Forms.Control control, System.Resources.ResourceManager resources)
        {
            // ImageIndex property for ButtonBase, Label, TabPage, ToolBarButton, TreeNode, TreeView
            SetProperty(control, "ImageIndex", resources);
            // ToolTipText property for StatusBar, TabPage, ToolBarButton
            SetProperty(control, "ToolTipText", resources);
            // IntegralHeight property for ComboBox, ListBox
            SetProperty(control, "IntegralHeight", resources);
            // ItemHeight property for ListBox, ComboBox, TreeView
            SetProperty(control, "ItemHeight", resources);
            // MaxDropDownItems property for ComboBox
            SetProperty(control, "MaxDropDownItems", resources);
            // MaxLength property for ComboBox, RichTextBox, TextBoxBase
            SetProperty(control, "MaxLength", resources);
            // Appearance property for CheckBox, RadioButton, TabControl, ToolBar
            SetProperty(control, "Appearance", resources);
            // CheckAlign property for CheckBox and RadioBox
            SetProperty(control, "CheckAlign", resources);
            // FlatStyle property for ButtonBase, GroupBox and Label
            SetProperty(control, "FlatStyle", resources);
            // ImageAlign property for ButtonBase, Image and Label
            SetProperty(control, "ImageAlign", resources);
            // Indent property for TreeView
            SetProperty(control, "Indent", resources);
            // Multiline property for RichTextBox, TabControl, TextBoxBase
            SetProperty(control, "Multiline", resources);
            // BulletIndent property for RichTextBox
            SetProperty(control, "BulletIndent", resources);
            // RightMargin property for RichTextBox
            SetProperty(control, "RightMargin", resources);
            // ScrollBars property for RichTextBox, TextBox
            SetProperty(control, "ScrollBars", resources);
            // WordWrap property for TextBoxBase
            SetProperty(control, "WordWrap", resources);
            // ZoomFactor property for RichTextBox
            SetProperty(control, "ZoomFactor", resources);
            // ButtonSize property for ToolBar
            SetProperty(control, "ButtonSize", resources);
            // ButtonSize property for ToolBar
            SetProperty(control, "DropDownArrows", resources);
            // ShowToolTips property for TabControl, ToolBar
            SetProperty(control, "ShowToolTips", resources);
            // Wrappable property for ToolBar
            SetProperty(control, "Wrappable", resources);
            // AutoSize property for Label, RichTextBox, ToolBar, TrackBar
            SetProperty(control, "AutoSize", resources);
        }

        /// <summary>
        ///     Scans controls that are not contained by <c>Controls</c> 
        ///     collection, like <c>MenuItem</c>s, <c>StatusBarPanel</c>s
        ///     and <c>ColumnHeader</c>s.
        /// </summary>
        /// <param name="form">
        ///     <c>ContainerControl</c> object to scan.
        /// </param>
        /// <param name="resources">
        ///     <c>ResourceManager</c> used to get localized resources.
        /// </param>
        protected virtual void ScanNonControls(System.Windows.Forms.ContainerControl containerControl, System.Resources.ResourceManager resources)
        {
            FieldInfo[] fieldInfo = containerControl.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            for (int i = 0; i < fieldInfo.Length; i++)
            {
                object obj = fieldInfo[i].GetValue(containerControl);
                string fieldName = fieldInfo[i].Name;
                if (obj is System.Windows.Forms.MenuItem)
                {
                    System.Windows.Forms.MenuItem menuItem = (System.Windows.Forms.MenuItem)obj;
                    menuItem.Text = (string)GetSafeValue(resources, fieldName + ".Text", menuItem.Text);
                }
                if (obj is System.Windows.Forms.ColumnHeader)
                {
                    System.Windows.Forms.ColumnHeader header = (System.Windows.Forms.ColumnHeader)obj;
                    header.Text = (string)(GetSafeValue(resources, fieldName + ".Text", header.Text));
                    header.TextAlign = (System.Windows.Forms.HorizontalAlignment)(GetSafeValue(resources, fieldName + ".TextAlign", header.TextAlign));
                    header.Width = (int)(GetSafeValue(resources, fieldName + ".Width", header.Width));
                }
                if (obj is System.Windows.Forms.ToolBarButton)
                {
                    System.Windows.Forms.ToolBarButton button = (System.Windows.Forms.ToolBarButton)obj;
                    button.Enabled = (bool)(GetSafeValue(resources, fieldName + ".Enabled", button.Enabled));
                    button.ImageIndex = (int)(GetSafeValue(resources, fieldName + ".ImageIndex", button.ImageIndex));
                    button.ToolTipText = (string)(GetSafeValue(resources, fieldName + ".ToolTipText", button.ToolTipText));
                    button.Visible = (bool)(GetSafeValue(resources, fieldName + ".Visible", button.Visible));
                }
            }
        }

        /// <summary>
        ///   Gets resource value. If resource for new culture does not exists, leaves the current.
        /// </summary>
        /// <param name="resources"></param>
        /// <param name="name"></param>
        /// <param name="currentValue"></param>
        /// <returns></returns>
        private object GetSafeValue(System.Resources.ResourceManager resources, string name, object currentValue)
        {
            object newValue = resources.GetObject(name, m_cultureInfo);
            if (newValue == null)
            {
                //Debug.WriteLine(string.Format("Resource for {0} not found, using current value.", name));
                return currentValue;
            }
            return newValue;
        }

        /// <summary>
        ///     Reloads items in following controls: <c>ComboBox</c>, 
        ///     <c>ListBox</c>, <c>DomainUpDown</c>. Method is made virtual so 
        ///     it can be overriden in derived class to redefine types.
        /// </summary>
        /// <param name="parent">
        ///     <c>Control</c> object.
        /// </param>
        /// <param name="resources">
        ///     <c>ResourceManager</c> object.
        /// </param>
        protected virtual void ReloadListItems(System.Windows.Forms.Control control, System.Resources.ResourceManager resources)
        {
            if (control is System.Windows.Forms.ComboBox)
                ReloadComboBoxItems((System.Windows.Forms.ComboBox)control, resources);
            else if (control is System.Windows.Forms.ListBox)
                ReloadListBoxItems((System.Windows.Forms.ListBox)control, resources);
            else if (control is System.Windows.Forms.DomainUpDown)
                ReloadUpDownItems((System.Windows.Forms.DomainUpDown)control, resources);
        }

        /// <summary>
        ///     Changes the language of the form.
        /// </summary>
        /// <param name="form">
        ///     <c>Form</c> object to apply changes to. 
        /// </param>
        private void ChangeFormLanguage(System.Windows.Forms.Form form)
        {
            form.SuspendLayout();
            Cursor.Current = Cursors.WaitCursor;
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(form.GetType());
            // change main form resources
            form.Text = (string)(GetSafeValue(resources, "$this.Text", form.Text));
            ReloadControlCommonProperties(form, resources);

            // change text of all containing controls
            RecurControls(form, resources);
            // change the text of menus
            ScanNonControls(form, resources);

            Cursor.Current = Cursors.Default;

            form.ResumeLayout();
        }


        /// <summary>
        ///     Recurs <c>Controls</c> members of the control to change 
        ///     corresponding texts.
        /// </summary>
        /// <param name="parent">
        ///     Parent <c>Control</c> object.
        /// </param>
        /// <param name="resources">
        ///     <c>ResourceManager</c> object.
        /// </param>
        private void RecurControls(System.Windows.Forms.Control parent, System.Resources.ResourceManager resources)
        {
            foreach (Control control in parent.Controls)
            {
                control.SuspendLayout();
                ReloadControlCommonProperties(control, resources);
                ReloadControlSpecificProperties(control, resources);

                if (control is System.Windows.Forms.UserControl)
                    RecurUserControl((System.Windows.Forms.UserControl)control);
                else
                {
                    ReloadTextForSelectedControls(control, resources);
                    ReloadListItems(control, resources);
                    if (control is System.Windows.Forms.TreeView)
                        ReloadTreeViewNodes((System.Windows.Forms.TreeView)control, resources);
                    if (control.Controls.Count > 0)
                        RecurControls(control, resources);
                }
                control.ResumeLayout();
            }
        }

        /// <summary>
        ///     Reloads resources specific to the <c>Form</c> type.
        /// </summary>
        /// <param name="form">
        ///     <c>Form</c> object to apply changes to.
        /// </param>
        /// <param name="resources">
        ///     <c>ResourceManager</c> object.
        /// </param>
        private void ReloadFormProperties(System.Windows.Forms.Form form, System.Resources.ResourceManager resources)
        {
            SetProperty(form, "AutoScaleBaseSize", resources);
            SetProperty(form, "Icon", resources);
            SetProperty(form, "MaximumSize", resources);
            SetProperty(form, "MinimumSize", resources);
        }

        /// <summary>
        ///     Reloads resources specific to the <c>ScrollableControl</c> type.
        /// </summary>
        /// <param name="control">
        ///     <c>Control</c> object for which resources should be reloaded.
        /// </param>
        /// <param name="resources">
        ///     <c>ResourceManager</c> object.
        /// </param>
        private void ReloadScrollableControlProperties(System.Windows.Forms.ScrollableControl control, System.Resources.ResourceManager resources)
        {
            SetProperty(control, "AutoScroll", resources);
            SetProperty(control, "AutoScrollMargin", resources);
            SetProperty(control, "AutoScrollMinSize", resources);
        }

        /// <summary>
        ///     Reloads resources for a property.
        /// </summary>
        /// <param name="control">
        ///     <c>Control</c> object for which resources should be reloaded.
        /// </param>
        /// <param name="propertyName">
        ///     Name of the property to reload.
        /// </param>
        /// <param name="resources">
        ///     <c>ResourceManager</c> object.
        /// </param>
        private void SetProperty(System.Windows.Forms.Control control, string propertyName, System.Resources.ResourceManager resources)
        {
            try
            {
                PropertyInfo propertyInfo = control.GetType().GetProperty(propertyName);
                if (propertyInfo != null)
                {
                    string controlName = control.Name;
                    if (control is System.Windows.Forms.Form)
                        controlName = "$this";
                    object resObject = resources.GetObject(controlName + "." + propertyName, m_cultureInfo);
                    if (resObject != null)
                    {
                        //propertyInfo.SetValue(control, Convert.ChangeType(resObject, propertyInfo.PropertyType, null), null);
                        propertyInfo.SetValue(control, resObject, null);
                    }
                }
            }
            catch (AmbiguousMatchException e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        /// <summary>
        ///     Recurs <c>UserControl</c> to change.
        /// </summary>
        /// <param name="parent">
        ///     <c>UserControl</c> object to scan.
        /// </param>
        private void RecurUserControl(System.Windows.Forms.UserControl userControl)
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(userControl.GetType());

            RecurControls(userControl, resources);

            // addition suggested by Piotr Sielski:
            ScanNonControls(userControl, resources);
        }

        /// <summary>
        ///     Reloads items in the <c>ListBox</c>. If items are not sorted, 
        ///     selections are kept.
        /// </summary>
        /// <param name="listBox">
        ///     <c>ListBox</c> to localize.
        /// </param>
        /// <param name="resources">
        ///     <c>ResourceManager</c> object.
        /// </param>
        private void ReloadListBoxItems(System.Windows.Forms.ListBox listBox, System.Resources.ResourceManager resources)
        {
            if (listBox.Items.Count > 0)
            {
                int selectedItems = listBox.SelectedIndex;
                ReloadItems(listBox.Name, listBox.Items, listBox.Items.Count, resources);
                listBox.SelectedIndex = selectedItems;
            }
        }

        /// <summary>
        ///     Reloads items in the <c>ComboBox</c>. If items are not sorted, 
        ///     selection is kept.
        /// </summary>
        /// <param name="listBox">
        ///     <c>ComboBox</c> to localize.
        /// </param>
        /// <param name="resources">
        ///     <c>ResourceManager</c> object.
        /// </param>
        private void ReloadComboBoxItems(System.Windows.Forms.ComboBox comboBox, System.Resources.ResourceManager resources)
        {
            if (comboBox.Items.Count > 0)
            {
                int selectedIndex = comboBox.SelectedIndex;
                ReloadItems(comboBox.Name, comboBox.Items, comboBox.Items.Count, resources);
                comboBox.SelectedIndex = selectedIndex;
            }
        }

        /// <summary>
        ///     Reloads items in the <c>DomainUpDown</c> control. If items are 
        ///     not sorted, selection is kept.
        /// </summary>
        /// <param name="listBox">
        ///     <c>DomainUpDown</c> to localize.
        /// </param>
        /// <param name="resources">
        ///     <c>ResourceManager</c> object.
        /// </param>
        private void ReloadUpDownItems(System.Windows.Forms.DomainUpDown domainUpDown, System.Resources.ResourceManager resources)
        {
            if (domainUpDown.Items.Count > 0)
            {
                int selectedIndex = domainUpDown.SelectedIndex;
                ReloadItems(domainUpDown.Name, domainUpDown.Items, domainUpDown.Items.Count, resources);
                domainUpDown.SelectedIndex = selectedIndex;
            }
        }

        /// <summary>
        ///     Reloads content of a <c>TreeView</c>.
        /// </summary>
        /// <param name="treeView">
        ///     <c>TreeView</c> control to reload.
        /// </param>
        /// <param name="resources">
        ///     <c>ResourceManager</c> object.
        /// </param>
        private void ReloadTreeViewNodes(System.Windows.Forms.TreeView treeView, System.Resources.ResourceManager resources)
        {
            if (treeView.Nodes.Count > 0)
            {
                string resourceName = treeView.Name + ".Nodes";
                TreeNode[] newNodes = new TreeNode[treeView.Nodes.Count];
                newNodes[0] = (System.Windows.Forms.TreeNode)resources.GetObject(resourceName, m_cultureInfo);
                // VS2002 generates node resource names with additional ".Nodes" string
                if (newNodes[0] == null)
                {
                    resourceName += ".Nodes";
                    newNodes[0] = (System.Windows.Forms.TreeNode)resources.GetObject(resourceName, m_cultureInfo);
                }
                Debug.Assert(newNodes[0] != null);
                for (int i = 1; i < treeView.Nodes.Count; i++)
                {
                    newNodes[i] = (System.Windows.Forms.TreeNode)resources.GetObject(resourceName + i.ToString(), m_cultureInfo);
                }
                treeView.Nodes.Clear();
                treeView.Nodes.AddRange(newNodes);
            }
        }

        /// <summary>
        ///     Clears all items in the <c>IList</c> and reloads the list with
        ///     items according to language settings.
        /// </summary>
        /// <param name="controlName">
        ///     Name of the control.
        /// </param>
        /// <param name="list">
        ///     <c>IList</c> with items to change.
        /// </param>
        /// <param name="itemsNumber">
        ///     Number of items.
        /// </param>
        /// <param name="resources">
        ///     <c>ResourceManager</c> object.
        /// </param>
        private void ReloadItems(string controlName, IList list, int itemsNumber, System.Resources.ResourceManager resources)
        {
            string resourceName = controlName + ".Items";
            object obj = resources.GetString(resourceName, m_cultureInfo);
            // VS2002 generates item resource name with additional ".Items" string
            if (obj == null)
            {
                resourceName += ".Items";
                obj = resources.GetString(resourceName, m_cultureInfo);
            }
            if (obj != null)
            {
                list.Clear();
                Debug.Assert(obj != null);
                list.Add(obj);
                for (int i = 1; i < itemsNumber; i++)
                    list.Add(resources.GetString(resourceName + i, m_cultureInfo));
            }
        }

        /// <summary>
        ///     <c>CultureInfo</c> used by Resource Manager.
        /// </summary>
        private System.Globalization.CultureInfo m_cultureInfo;
    }
}
