using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using Conscript.Runtime;

namespace Conscript.IDE
{
    internal partial class VariableDictionaryTreeView
        : TreeView
    {
        #region Private Classes

        private class VariableInfo
        {
            public String m_strContainer;
            public object m_objectContainer;
            public object m_objectKey;
            public object m_objectValue;
        }

        #endregion

        #region Private Variables

        private VariableDictionary m_variableDictionary;
        private Panel m_panel;
        private Label m_lblValue;
        private TextBox m_txtValue;

        #endregion

        #region Private Methods

        private String ToString(object objectValue)
        {
            if (objectValue == null)
                return "NULL";
            else if (objectValue.GetType() == typeof(String))
                return "\"" + objectValue + "\"";
            else
                return objectValue.ToString();
        }

        private void AddNode(
            String strIdentifier, object objectValue)
        {
            String strText = strIdentifier + " = "
                + ToString(objectValue);

            TreeNode treeNodeRoot = Nodes[0];
            TreeNode treeNode = treeNodeRoot.Nodes.Add(strText);

            VariableInfo variableInfo = new VariableInfo();
            variableInfo.m_strContainer = null;
            variableInfo.m_objectContainer = m_variableDictionary;
            variableInfo.m_objectKey = strIdentifier;
            variableInfo.m_objectValue = objectValue;
            treeNode.Tag = variableInfo;
            if (objectValue != null && objectValue.GetType() == typeof(AssociativeArray))
            {
                AssociativeArray associativeArray
                    = (AssociativeArray) objectValue;
                foreach (object objectKey in associativeArray.Keys)
                {
                    object objectValueChild = associativeArray[objectKey];
                    AddNode(treeNode, objectKey, objectValueChild);
                }        
            }
        }

        private void AddNode(TreeNode treeNodeParent,
            object objectKey, object objectValue)
        {
            VariableInfo variableInfoParent = (VariableInfo) treeNodeParent.Tag;
            VariableInfo variableInfo = new VariableInfo();

            if (variableInfoParent.m_strContainer == null)
                variableInfo.m_strContainer = variableInfoParent.m_objectKey
                    + "[" + ToString(objectKey) + "]";
            else
                variableInfo.m_strContainer = variableInfoParent.m_strContainer
                    + "[" + ToString(objectKey) + "]";
            variableInfo.m_objectContainer = variableInfoParent.m_objectValue;
            variableInfo.m_objectKey = objectKey;
            variableInfo.m_objectValue = objectValue;

            String strText = variableInfo.m_strContainer
                + " = " + ToString(objectValue);

            TreeNode treeNode = treeNodeParent.Nodes.Add(strText);

            treeNode.Tag = variableInfo;

            Type typeObject = objectValue.GetType();
            if (typeObject == typeof(AssociativeArray))
            {
                AssociativeArray associativeArray
                    = (AssociativeArray)objectValue;
                foreach (object objectKeyChild in associativeArray.Keys)
                    AddNode(treeNode, objectKeyChild,
                        associativeArray[objectKeyChild]);
            }
            
        }

        private bool IsIdentifier(String strIdentifier)
        {
            if (strIdentifier.Length == 0) return false;
            if (strIdentifier[0] != '_' && !Char.IsLetter(strIdentifier[0])) return false;
            for (int iIndex = 1; iIndex < strIdentifier.Length; iIndex++)
            {
                char ch = strIdentifier[iIndex];
                if (ch != '_' && !Char.IsLetterOrDigit(ch))
                    return false;
            }
            return true;
        }

        private object ParseValue(String strValue)
        {
            if (strValue.Trim().ToUpper() == "TRUE")
                return true;

            if (strValue.Trim().ToUpper() == "FALSE")
                return false;

            if (strValue.Trim() == "{}")
                return new AssociativeArray();

            if (strValue.Trim().ToUpper() == "NULL")
                return null;

            try { return int.Parse(strValue); }
            catch (Exception) { }

            try { return float.Parse(strValue); }
            catch (Exception) { }

            return strValue;
        }

        private void CreateVariable(TreeNode treeNodeParent)
        {
            if (treeNodeParent.Tag != null)
            {
                VariableInfo variableInfoParent
                    = (VariableInfo)treeNodeParent.Tag;

                object objectValueParent = variableInfoParent.m_objectValue;
                if (objectValueParent == null
                    || objectValueParent.GetType() != typeof(AssociativeArray))
                {
                    MessageBox.Show(this,
                        "Cannot add array element to a native value.",
                        "New Variable", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
               
            TreeNode treeNodeNew = new TreeNode("(enter new variable name)");
            treeNodeParent.Nodes.Add(treeNodeNew);
            treeNodeNew.TreeView.LabelEdit = true;
            SelectedNode = treeNodeNew;
            treeNodeNew.BeginEdit();            
        }

        private void UpdateVariable()
        {
            // do nothing if tag cleared
            if (m_txtValue.Tag == null) return;

            TreeNode treeNode = (TreeNode)m_txtValue.Tag;

            if (treeNode.Tag == null) return;
            VariableInfo variableInfo = (VariableInfo)treeNode.Tag;

            String strValue = variableInfo.m_objectValue == null
                ? "NULL" : variableInfo.m_objectValue.ToString();
            if (m_txtValue.Text == strValue)
            {
                m_txtValue.Tag = null;
                m_panel.Hide();
                return;
            }

            object objectValue = ParseValue(m_txtValue.Text);
            variableInfo.m_objectValue = objectValue;

            treeNode.Nodes.Clear();

            object objectContainer = variableInfo.m_objectContainer;
            if (objectContainer.GetType() == typeof(VariableDictionary))
            {
                VariableDictionary variableDictionary
                    = (VariableDictionary)objectContainer;
                variableDictionary[variableInfo.m_objectKey.ToString()] = objectValue;
                treeNode.Text = variableInfo.m_objectKey + " = "
                    + ToString(objectValue);
            }
            else
            {
                AssociativeArray associativeArray
                    = (AssociativeArray)objectContainer;
                associativeArray[variableInfo.m_objectKey] = objectValue;
                treeNode.Text = variableInfo.m_strContainer + " = "
                    + ToString(objectValue);
            }


            m_txtValue.Tag = null;
            m_panel.Hide();
        }

        private void DeleteVariable(TreeNode treeNode)
        {
            if (treeNode.Tag == null) return;
            VariableInfo variableInfo = (VariableInfo)treeNode.Tag;

            object objectContainer = variableInfo.m_objectContainer;
            if (objectContainer.GetType() == typeof(VariableDictionary))
                m_variableDictionary.Remove(
                    variableInfo.m_objectKey.ToString());
            else
            {
                AssociativeArray associativeArray
                    = (AssociativeArray)objectContainer;
                associativeArray.Remove(variableInfo.m_objectKey);
            }

            // select sibling or parent node
            if (treeNode.NextNode != null)
                SelectedNode = treeNode.NextNode;
            else if (treeNode.PrevNode != null)
                SelectedNode = treeNode.PrevNode;
            else
                SelectedNode = treeNode.Parent;

            // remove this node
            treeNode.Remove();
        }

        private void OnVariableLostFocus(object objectSender, EventArgs eventArgs)
        {
            UpdateVariable();
        }

        private void OnVariableKeyUp(object objectSender, KeyEventArgs keyEventArgs)
        {
            // ESC pressed - cancel edit
            if (keyEventArgs.KeyCode == Keys.Escape)
            {
                m_txtValue.Tag = null;
                m_panel.Hide();
                return;
            }

            if (keyEventArgs.KeyCode == Keys.Return)
                UpdateVariable();
        }

        private void OnVariableNameEdit(object objectSender, NodeLabelEditEventArgs nodeLabelEditEventArgs)
        {
            TreeNode treeNodeNew = nodeLabelEditEventArgs.Node;

            String strIdentifier = nodeLabelEditEventArgs.Label;

            TreeNode treeNodeParent = nodeLabelEditEventArgs.Node.Parent;
            if (treeNodeParent.Tag == null)
            {
                // it's a variable in the dictionary
                if (strIdentifier == null || !IsIdentifier(strIdentifier))
                {
                    nodeLabelEditEventArgs.CancelEdit = true;
                    MessageBox.Show(this, "Invalid identifier format.",
                        "New Variable", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    treeNodeNew.BeginEdit();
                    return;
                }

                // ensure not already declared
                if (m_variableDictionary.IsDeclared(strIdentifier))
                {
                    nodeLabelEditEventArgs.CancelEdit = true;
                    MessageBox.Show(this,
                        "Identifier '" + strIdentifier
                        + "' already declared in this scope or a more public scope.",
                        "New Variable", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    treeNodeNew.BeginEdit();
                    return;
                }

                m_variableDictionary[strIdentifier] = null;
                VariableInfo variableInfo = new VariableInfo();
                variableInfo.m_strContainer = null;
                variableInfo.m_objectContainer = m_variableDictionary;
                variableInfo.m_objectKey = strIdentifier;
                variableInfo.m_objectValue = null;
                treeNodeNew.Tag = variableInfo;

            }
            else
            {
                // it's an array element
                VariableInfo variableInfoParent
                    = (VariableInfo) treeNodeParent.Tag; 

                AssociativeArray associativeArray
                    = (AssociativeArray) variableInfoParent.m_objectValue;

                object objectKey = ParseValue(strIdentifier);

                if (associativeArray.ContainsKey(objectKey))
                {
                    nodeLabelEditEventArgs.CancelEdit = true;
                    MessageBox.Show(this,
                        "Duplicate array index.",
                        "New Variable", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    treeNodeNew.BeginEdit();
                    return;
                }

                associativeArray[objectKey] = null;
                VariableInfo variableInfo = new VariableInfo();
                if (variableInfoParent.m_strContainer == null)
                    variableInfo.m_strContainer = variableInfoParent.m_objectKey
                        + "[" + ToString(objectKey) + "]";
                else
                    variableInfo.m_strContainer = variableInfoParent.m_strContainer
                        + "[" + ToString(objectKey) + "]";
                variableInfo.m_objectContainer = associativeArray;
                variableInfo.m_objectKey = objectKey;
                variableInfo.m_objectValue = null;
                treeNodeNew.Tag = variableInfo;
            }

            treeNodeNew.EndEdit(false);
            LabelEdit = false;
            VariableDictionary = m_variableDictionary;
        }

        private void OnContextMenuClick(object objectSender, EventArgs eventArgs)
        {
            if (SelectedNode == null) return;
            MenuItem menuItem = (MenuItem)objectSender;
            if (menuItem.Index == 0)
                CreateVariable(SelectedNode);
            else if (menuItem.Index == 1)
                DeleteVariable(SelectedNode);
        }

        private void VariableDictionaryTreeView_KeyUp(object objectSender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.KeyCode == Keys.Delete
                && SelectedNode != null)
                DeleteVariable(SelectedNode);
        }

        private void VariableDictionaryTreeView_NodeMouseDoubleClick(
            object objectSender,
            TreeNodeMouseClickEventArgs treeNodeMouseClickEventArgs)
        {
            if (treeNodeMouseClickEventArgs.Button != MouseButtons.Left)
                return;

            TreeNode treeNode = treeNodeMouseClickEventArgs.Node;

            if (treeNode.Tag == null) return;
            VariableInfo variableInfo = (VariableInfo)treeNode.Tag;

            Rectangle rectBounds = treeNode.Bounds;

            m_panel.Bounds = rectBounds;
            m_panel.Left -= 1;
            m_panel.Width += 8;

            m_lblValue.Text = variableInfo.m_strContainer == null
                ? variableInfo.m_objectKey.ToString() + " = "
                : variableInfo.m_strContainer + " = ";
            m_lblValue.AutoSize = true;
            m_lblValue.SetBounds(0, 0, m_lblValue.Width, m_lblValue.Height);

            m_txtValue.Tag = treeNode;
            m_txtValue.Text = variableInfo.m_objectValue == null ? "NULL" : variableInfo.m_objectValue.ToString();
            m_txtValue.LostFocus += new EventHandler(OnVariableLostFocus);
            m_txtValue.KeyUp += new KeyEventHandler(OnVariableKeyUp);
            m_txtValue.SetBounds(m_lblValue.Width, 0, rectBounds.Width + 16 - m_lblValue.Width, rectBounds.Height);

            m_panel.Show();
            m_txtValue.Focus();
            m_txtValue.Select(0, m_txtValue.Text.Length);
        }

        private void VariableDictionaryTreeView_DrawNode(object objectSender,
            DrawTreeNodeEventArgs drawTreeNodeEventArgs)
        {
            TreeNode treeNode = drawTreeNodeEventArgs.Node;
            Graphics graphics = drawTreeNodeEventArgs.Graphics;
            bool bSelected = treeNode == SelectedNode;
            Brush brushBack = bSelected ? Brushes.SkyBlue : Brushes.White;
            Brush brushFont = bSelected ? Brushes.White : Brushes.Black; ;
            graphics.FillRectangle(brushBack, drawTreeNodeEventArgs.Bounds);
            graphics.DrawString(treeNode.Text, Font, brushFont,
                drawTreeNodeEventArgs.Bounds.X,
                drawTreeNodeEventArgs.Bounds.Y);
        }

        #endregion

        #region Protected Methods

        protected override void OnPaint(PaintEventArgs paintEventArgs)
        {
            if (m_variableDictionary == null)
            {
                Graphics graphics = paintEventArgs.Graphics;
                graphics.DrawString("(dictionary not set)", Font,
                    Brushes.Black, paintEventArgs.ClipRectangle);
            }
            else
                base.OnPaint(paintEventArgs);
        }

        #endregion

        #region Public Methods

        public VariableDictionaryTreeView()
        {
            InitializeComponent();

            DrawMode = TreeViewDrawMode.OwnerDrawText;
            BorderStyle = BorderStyle.None;
            m_variableDictionary = null;
            m_lblValue = new Label();
            m_txtValue = new TextBox();
            m_txtValue.BorderStyle = BorderStyle.None;
            m_panel = new Panel();
            m_panel.Controls.Add(m_lblValue);
            m_panel.Controls.Add(m_txtValue);
            m_panel.Hide();
            Controls.Add(m_panel);

            // variable name editing
            AfterLabelEdit
                += new NodeLabelEditEventHandler(OnVariableNameEdit);

            // context menu
            ContextMenu contextMenu = new ContextMenu();
            MenuItem menuItemNew
                = new MenuItem("New Variable", OnContextMenuClick);
            menuItemNew.Visible = false;
            contextMenu.MenuItems.Add(menuItemNew);
            MenuItem menuItemDelete
                = new MenuItem("Delete Variable", OnContextMenuClick);
            contextMenu.MenuItems.Add(menuItemDelete);
            menuItemDelete.Visible = false;
            ContextMenu = contextMenu;
        }

        public VariableDictionary VariableDictionary
        {
            get { return m_variableDictionary; }
            set
            {
                m_variableDictionary = value;
                if (m_variableDictionary == null)
                {
                    Nodes.Clear();
                    return;
                }

                BeginUpdate();
                Nodes.Clear();
                TreeNode treeNodeRoot = new TreeNode(
                    m_variableDictionary.Scope.ToString() + " Scope");
                Nodes.Add(treeNodeRoot);
                foreach (String strIdentifier in m_variableDictionary.Identifiers)
                    AddNode(strIdentifier,
                        m_variableDictionary[strIdentifier]);
                ContextMenu.MenuItems[0].Visible = true;
                ContextMenu.MenuItems[1].Visible = true;
                EndUpdate();
            }
        }

        #endregion
    }
}
