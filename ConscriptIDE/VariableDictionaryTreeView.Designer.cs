namespace Conscript.IDE
{
    partial class VariableDictionaryTreeView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // VariableDictionaryTreeView
            // 
            this.LineColor = System.Drawing.Color.Black;
            this.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.VariableDictionaryTreeView_NodeMouseDoubleClick);
            this.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.VariableDictionaryTreeView_DrawNode);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.VariableDictionaryTreeView_KeyUp);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
