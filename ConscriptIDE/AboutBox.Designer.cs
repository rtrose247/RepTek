namespace Conscript.IDE
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.m_pnlLayout = new System.Windows.Forms.TableLayoutPanel();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.m_lblProductName = new System.Windows.Forms.Label();
            this.m_lblVersion = new System.Windows.Forms.Label();
            this.m_lblCopyright = new System.Windows.Forms.Label();
            this.m_txtDescription = new System.Windows.Forms.TextBox();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_pnlLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // m_pnlLayout
            // 
            this.m_pnlLayout.ColumnCount = 2;
            this.m_pnlLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.m_pnlLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67F));
            this.m_pnlLayout.Controls.Add(this.logoPictureBox, 0, 0);
            this.m_pnlLayout.Controls.Add(this.m_lblProductName, 1, 0);
            this.m_pnlLayout.Controls.Add(this.m_lblVersion, 1, 1);
            this.m_pnlLayout.Controls.Add(this.m_lblCopyright, 1, 2);
            this.m_pnlLayout.Controls.Add(this.m_txtDescription, 1, 4);
            this.m_pnlLayout.Controls.Add(this.m_btnOk, 1, 5);
            this.m_pnlLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_pnlLayout.Location = new System.Drawing.Point(9, 9);
            this.m_pnlLayout.Name = "m_pnlLayout";
            this.m_pnlLayout.RowCount = 6;
            this.m_pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.m_pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.m_pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.m_pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.m_pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.m_pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.m_pnlLayout.Size = new System.Drawing.Size(417, 265);
            this.m_pnlLayout.TabIndex = 0;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logoPictureBox.Image = global::Conscript.IDE.Properties.Resources.About;
            this.logoPictureBox.Location = new System.Drawing.Point(0, 0);
            this.logoPictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.logoPictureBox.Name = "logoPictureBox";
            this.m_pnlLayout.SetRowSpan(this.logoPictureBox, 6);
            this.logoPictureBox.Size = new System.Drawing.Size(137, 265);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPictureBox.TabIndex = 12;
            this.logoPictureBox.TabStop = false;
            // 
            // m_lblProductName
            // 
            this.m_lblProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblProductName.Location = new System.Drawing.Point(143, 0);
            this.m_lblProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.m_lblProductName.MaximumSize = new System.Drawing.Size(0, 17);
            this.m_lblProductName.Name = "m_lblProductName";
            this.m_lblProductName.Size = new System.Drawing.Size(271, 17);
            this.m_lblProductName.TabIndex = 19;
            this.m_lblProductName.Text = "Product Name";
            this.m_lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblVersion
            // 
            this.m_lblVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblVersion.Location = new System.Drawing.Point(143, 26);
            this.m_lblVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.m_lblVersion.MaximumSize = new System.Drawing.Size(0, 17);
            this.m_lblVersion.Name = "m_lblVersion";
            this.m_lblVersion.Size = new System.Drawing.Size(271, 17);
            this.m_lblVersion.TabIndex = 0;
            this.m_lblVersion.Text = "Version";
            this.m_lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblCopyright
            // 
            this.m_lblCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblCopyright.Location = new System.Drawing.Point(143, 52);
            this.m_lblCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.m_lblCopyright.MaximumSize = new System.Drawing.Size(0, 17);
            this.m_lblCopyright.Name = "m_lblCopyright";
            this.m_lblCopyright.Size = new System.Drawing.Size(271, 17);
            this.m_lblCopyright.TabIndex = 21;
            this.m_lblCopyright.Text = "Copyright";
            this.m_lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtDescription
            // 
            this.m_txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtDescription.Location = new System.Drawing.Point(143, 107);
            this.m_txtDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.m_txtDescription.Multiline = true;
            this.m_txtDescription.Name = "m_txtDescription";
            this.m_txtDescription.ReadOnly = true;
            this.m_txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_txtDescription.Size = new System.Drawing.Size(271, 126);
            this.m_txtDescription.TabIndex = 23;
            this.m_txtDescription.TabStop = false;
            this.m_txtDescription.Text = "Description";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnOk.Location = new System.Drawing.Point(339, 239);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 24;
            this.m_btnOk.Text = "&OK";
            // 
            // AboutBox
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnOk;
            this.ClientSize = new System.Drawing.Size(435, 283);
            this.Controls.Add(this.m_pnlLayout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AboutBox";
            this.m_pnlLayout.ResumeLayout(false);
            this.m_pnlLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel m_pnlLayout;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label m_lblProductName;
        private System.Windows.Forms.Label m_lblVersion;
        private System.Windows.Forms.Label m_lblCopyright;
        private System.Windows.Forms.TextBox m_txtDescription;
        private System.Windows.Forms.Button m_btnOk;
    }
}
