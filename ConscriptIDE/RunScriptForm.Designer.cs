namespace Conscript.IDE
{
    partial class RunScriptForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunScriptForm));
            this.m_lblFunction = new System.Windows.Forms.Label();
            this.m_cmbFunction = new System.Windows.Forms.ComboBox();
            this.m_lblParameters = new System.Windows.Forms.Label();
            this.m_dgvParameters = new System.Windows.Forms.DataGridView();
            this.ParameterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParameterValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_btnOk = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_erpDialog = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.m_dgvParameters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_erpDialog)).BeginInit();
            this.SuspendLayout();
            // 
            // m_lblFunction
            // 
            this.m_lblFunction.AutoSize = true;
            this.m_lblFunction.Location = new System.Drawing.Point(12, 16);
            this.m_lblFunction.Name = "m_lblFunction";
            this.m_lblFunction.Size = new System.Drawing.Size(48, 13);
            this.m_lblFunction.TabIndex = 0;
            this.m_lblFunction.Text = "Function";
            // 
            // m_cmbFunction
            // 
            this.m_cmbFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_cmbFunction.FormattingEnabled = true;
            this.m_cmbFunction.Location = new System.Drawing.Point(79, 12);
            this.m_cmbFunction.Name = "m_cmbFunction";
            this.m_cmbFunction.Size = new System.Drawing.Size(293, 21);
            this.m_cmbFunction.TabIndex = 1;
            this.m_cmbFunction.SelectedIndexChanged += new System.EventHandler(this.m_cmbFunction_SelectedIndexChanged);
            // 
            // m_lblParameters
            // 
            this.m_lblParameters.AutoSize = true;
            this.m_lblParameters.Location = new System.Drawing.Point(12, 39);
            this.m_lblParameters.Name = "m_lblParameters";
            this.m_lblParameters.Size = new System.Drawing.Size(60, 13);
            this.m_lblParameters.TabIndex = 2;
            this.m_lblParameters.Text = "Parameters";
            // 
            // m_dgvParameters
            // 
            this.m_dgvParameters.AllowUserToAddRows = false;
            this.m_dgvParameters.AllowUserToDeleteRows = false;
            this.m_dgvParameters.AllowUserToResizeColumns = false;
            this.m_dgvParameters.AllowUserToResizeRows = false;
            this.m_dgvParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dgvParameters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParameterName,
            this.ParameterValue});
            this.m_dgvParameters.Location = new System.Drawing.Point(79, 39);
            this.m_dgvParameters.Name = "m_dgvParameters";
            this.m_dgvParameters.RowHeadersVisible = false;
            this.m_dgvParameters.Size = new System.Drawing.Size(293, 160);
            this.m_dgvParameters.TabIndex = 3;
            this.m_dgvParameters.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_dgvParameters_CellValidated);
            this.m_dgvParameters.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.m_dgvParameters_CellValidating);
            // 
            // ParameterName
            // 
            this.ParameterName.Frozen = true;
            this.ParameterName.HeaderText = "Name";
            this.ParameterName.Name = "ParameterName";
            this.ParameterName.ReadOnly = true;
            // 
            // ParameterValue
            // 
            this.ParameterValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ParameterValue.HeaderText = "Value";
            this.ParameterValue.Name = "ParameterValue";
            // 
            // m_btnOk
            // 
            this.m_btnOk.Location = new System.Drawing.Point(215, 229);
            this.m_btnOk.Name = "m_btnOk";
            this.m_btnOk.Size = new System.Drawing.Size(75, 23);
            this.m_btnOk.TabIndex = 4;
            this.m_btnOk.Text = "OK";
            this.m_btnOk.UseVisualStyleBackColor = true;
            this.m_btnOk.Click += new System.EventHandler(this.m_btnOk_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Location = new System.Drawing.Point(296, 229);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
            this.m_btnCancel.TabIndex = 5;
            this.m_btnCancel.Text = "Cancel";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // m_erpDialog
            // 
            this.m_erpDialog.ContainerControl = this;
            // 
            // RunScriptForm
            // 
            this.AcceptButton = this.m_btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 264);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOk);
            this.Controls.Add(this.m_dgvParameters);
            this.Controls.Add(this.m_lblParameters);
            this.Controls.Add(this.m_cmbFunction);
            this.Controls.Add(this.m_lblFunction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RunScriptForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Run Script";
            this.Load += new System.EventHandler(this.RunScriptForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.m_dgvParameters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_erpDialog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblFunction;
        private System.Windows.Forms.ComboBox m_cmbFunction;
        private System.Windows.Forms.Label m_lblParameters;
        private System.Windows.Forms.DataGridView m_dgvParameters;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParameterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParameterValue;
        private System.Windows.Forms.Button m_btnOk;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.ErrorProvider m_erpDialog;
    }
}