namespace Conscript.IDE
{
    partial class ByteCodeListBox
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
            // ByteCodeListBox
            // 
            this.Size = new System.Drawing.Size(120, 95);
            this.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ByteCodeListBox_DrawItem);
            this.DoubleClick += new System.EventHandler(this.ByteCodeListBox_DoubleClick);
            this.SelectedIndexChanged += new System.EventHandler(this.ByteCodeListBox_SelectedIndexChanged);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
