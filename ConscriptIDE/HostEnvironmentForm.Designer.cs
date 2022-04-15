namespace Conscript.IDE
{
    partial class HostEnvironmentForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HostEnvironmentForm));
            this.m_lblHostFunctions = new System.Windows.Forms.Label();
            this.m_dgvHostFunctions = new System.Windows.Forms.DataGridView();
            this.Function = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Module = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Assembly = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_btnRegisterModule = new System.Windows.Forms.Button();
            this.m_btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.m_dgvHostFunctions)).BeginInit();
            this.SuspendLayout();
            // 
            // m_lblHostFunctions
            // 
            this.m_lblHostFunctions.AutoSize = true;
            this.m_lblHostFunctions.Location = new System.Drawing.Point(12, 14);
            this.m_lblHostFunctions.Name = "m_lblHostFunctions";
            this.m_lblHostFunctions.Size = new System.Drawing.Size(78, 13);
            this.m_lblHostFunctions.TabIndex = 0;
            this.m_lblHostFunctions.Text = "Host Functions";
            // 
            // m_dgvHostFunctions
            // 
            this.m_dgvHostFunctions.AllowUserToAddRows = false;
            this.m_dgvHostFunctions.AllowUserToDeleteRows = false;
            this.m_dgvHostFunctions.AllowUserToResizeColumns = false;
            this.m_dgvHostFunctions.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.m_dgvHostFunctions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.m_dgvHostFunctions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dgvHostFunctions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Function,
            this.Module,
            this.Assembly});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.m_dgvHostFunctions.DefaultCellStyle = dataGridViewCellStyle2;
            this.m_dgvHostFunctions.Location = new System.Drawing.Point(12, 30);
            this.m_dgvHostFunctions.Name = "m_dgvHostFunctions";
            this.m_dgvHostFunctions.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.m_dgvHostFunctions.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.m_dgvHostFunctions.RowHeadersVisible = false;
            this.m_dgvHostFunctions.Size = new System.Drawing.Size(610, 200);
            this.m_dgvHostFunctions.TabIndex = 1;
            // 
            // Function
            // 
            this.Function.HeaderText = "Function";
            this.Function.Name = "Function";
            this.Function.ReadOnly = true;
            this.Function.Width = 200;
            // 
            // Module
            // 
            this.Module.HeaderText = "Module";
            this.Module.Name = "Module";
            this.Module.ReadOnly = true;
            // 
            // Assembly
            // 
            this.Assembly.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Assembly.HeaderText = "Assembly";
            this.Assembly.Name = "Assembly";
            this.Assembly.ReadOnly = true;
            // 
            // m_btnRegisterModule
            // 
            this.m_btnRegisterModule.Location = new System.Drawing.Point(431, 259);
            this.m_btnRegisterModule.Name = "m_btnRegisterModule";
            this.m_btnRegisterModule.Size = new System.Drawing.Size(110, 23);
            this.m_btnRegisterModule.TabIndex = 2;
            this.m_btnRegisterModule.Text = "Register Module...";
            this.m_btnRegisterModule.UseVisualStyleBackColor = true;
            this.m_btnRegisterModule.Click += new System.EventHandler(this.m_btnRegisterModule_Click);
            // 
            // m_btnClose
            // 
            this.m_btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnClose.Location = new System.Drawing.Point(547, 259);
            this.m_btnClose.Name = "m_btnClose";
            this.m_btnClose.Size = new System.Drawing.Size(75, 23);
            this.m_btnClose.TabIndex = 3;
            this.m_btnClose.Text = "Close";
            this.m_btnClose.UseVisualStyleBackColor = true;
            // 
            // HostEnvironmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnClose;
            this.ClientSize = new System.Drawing.Size(634, 294);
            this.Controls.Add(this.m_btnClose);
            this.Controls.Add(this.m_btnRegisterModule);
            this.Controls.Add(this.m_dgvHostFunctions);
            this.Controls.Add(this.m_lblHostFunctions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HostEnvironmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Host Environment";
            this.Load += new System.EventHandler(this.HostEnvironmentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_dgvHostFunctions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblHostFunctions;
        private System.Windows.Forms.DataGridView m_dgvHostFunctions;
        private System.Windows.Forms.DataGridViewTextBoxColumn Function;
        private System.Windows.Forms.DataGridViewTextBoxColumn Module;
        private System.Windows.Forms.DataGridViewTextBoxColumn Assembly;
        private System.Windows.Forms.Button m_btnRegisterModule;
        private System.Windows.Forms.Button m_btnClose;
    }
}