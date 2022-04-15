namespace Conscript.IDE
{
    partial class BuildSettingsForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuildSettingsForm));
            this.m_chkDebugMode = new System.Windows.Forms.CheckBox();
            this.m_chkOptimiseCode = new System.Windows.Forms.CheckBox();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_chkDebugMode
            // 
            this.m_chkDebugMode.AutoSize = true;
            this.m_chkDebugMode.Location = new System.Drawing.Point(13, 13);
            this.m_chkDebugMode.Name = "m_chkDebugMode";
            this.m_chkDebugMode.Size = new System.Drawing.Size(159, 17);
            this.m_chkDebugMode.TabIndex = 0;
            this.m_chkDebugMode.Text = "Generate &debug instructions";
            this.m_chkDebugMode.UseVisualStyleBackColor = true;
            // 
            // m_chkOptimiseCode
            // 
            this.m_chkOptimiseCode.AutoSize = true;
            this.m_chkOptimiseCode.Location = new System.Drawing.Point(13, 37);
            this.m_chkOptimiseCode.Name = "m_chkOptimiseCode";
            this.m_chkOptimiseCode.Size = new System.Drawing.Size(93, 17);
            this.m_chkOptimiseCode.TabIndex = 1;
            this.m_chkOptimiseCode.Text = "&Optimise code";
            this.m_chkOptimiseCode.UseVisualStyleBackColor = true;
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(116, 79);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 2;
            this.m_btnOk.Text = "OK";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(197, 79);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 3;
            this.m_btnCancel.Text = "&Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // BuildSettingsForm
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(284, 114);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_chkOptimiseCode);
            this.Controls.Add(this.m_chkDebugMode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BuildSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Build Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox m_chkDebugMode;
        private System.Windows.Forms.CheckBox m_chkOptimiseCode;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Button m_btnCancel;

    }
}