namespace ConscriptDemo
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.m_lblSource = new System.Windows.Forms.Label();
            this.m_txtSource = new System.Windows.Forms.TextBox();
            this.m_lblBuildOutput = new System.Windows.Forms.Label();
            this.m_txtBuildOutput = new System.Windows.Forms.TextBox();
            this.m_lblScriptOutput = new System.Windows.Forms.Label();
            this.m_txtScriptOutput = new System.Windows.Forms.TextBox();
            this.m_btnRunScript = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_lblSource
            // 
            this.m_lblSource.AutoSize = true;
            this.m_lblSource.Location = new System.Drawing.Point(13, 13);
            this.m_lblSource.Name = "m_lblSource";
            this.m_lblSource.Size = new System.Drawing.Size(41, 13);
            this.m_lblSource.TabIndex = 0;
            this.m_lblSource.Text = "Source";
            // 
            // m_txtSource
            // 
            this.m_txtSource.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtSource.Location = new System.Drawing.Point(16, 30);
            this.m_txtSource.Multiline = true;
            this.m_txtSource.Name = "m_txtSource";
            this.m_txtSource.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_txtSource.Size = new System.Drawing.Size(380, 532);
            this.m_txtSource.TabIndex = 1;
            this.m_txtSource.WordWrap = false;
            // 
            // m_lblBuildOutput
            // 
            this.m_lblBuildOutput.AutoSize = true;
            this.m_lblBuildOutput.Location = new System.Drawing.Point(399, 13);
            this.m_lblBuildOutput.Name = "m_lblBuildOutput";
            this.m_lblBuildOutput.Size = new System.Drawing.Size(65, 13);
            this.m_lblBuildOutput.TabIndex = 2;
            this.m_lblBuildOutput.Text = "Build Output";
            // 
            // m_txtBuildOutput
            // 
            this.m_txtBuildOutput.Location = new System.Drawing.Point(402, 29);
            this.m_txtBuildOutput.Multiline = true;
            this.m_txtBuildOutput.Name = "m_txtBuildOutput";
            this.m_txtBuildOutput.ReadOnly = true;
            this.m_txtBuildOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_txtBuildOutput.Size = new System.Drawing.Size(380, 150);
            this.m_txtBuildOutput.TabIndex = 3;
            this.m_txtBuildOutput.WordWrap = false;
            // 
            // m_lblScriptOutput
            // 
            this.m_lblScriptOutput.AutoSize = true;
            this.m_lblScriptOutput.Location = new System.Drawing.Point(402, 200);
            this.m_lblScriptOutput.Name = "m_lblScriptOutput";
            this.m_lblScriptOutput.Size = new System.Drawing.Size(69, 13);
            this.m_lblScriptOutput.TabIndex = 4;
            this.m_lblScriptOutput.Text = "Script Output";
            // 
            // m_txtScriptOutput
            // 
            this.m_txtScriptOutput.Location = new System.Drawing.Point(402, 216);
            this.m_txtScriptOutput.Multiline = true;
            this.m_txtScriptOutput.Name = "m_txtScriptOutput";
            this.m_txtScriptOutput.ReadOnly = true;
            this.m_txtScriptOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_txtScriptOutput.Size = new System.Drawing.Size(380, 317);
            this.m_txtScriptOutput.TabIndex = 5;
            this.m_txtScriptOutput.WordWrap = false;
            // 
            // m_btnRunScript
            // 
            this.m_btnRunScript.Location = new System.Drawing.Point(706, 539);
            this.m_btnRunScript.Name = "m_btnRunScript";
            this.m_btnRunScript.Size = new System.Drawing.Size(75, 23);
            this.m_btnRunScript.TabIndex = 6;
            this.m_btnRunScript.Text = "Run Script";
            this.m_btnRunScript.UseVisualStyleBackColor = true;
            this.m_btnRunScript.Click += new System.EventHandler(this.m_btnRunScript_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 574);
            this.Controls.Add(this.m_btnRunScript);
            this.Controls.Add(this.m_txtScriptOutput);
            this.Controls.Add(this.m_lblScriptOutput);
            this.Controls.Add(this.m_txtBuildOutput);
            this.Controls.Add(this.m_lblBuildOutput);
            this.Controls.Add(this.m_txtSource);
            this.Controls.Add(this.m_lblSource);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Conscript Demo Application";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblSource;
        private System.Windows.Forms.TextBox m_txtSource;
        private System.Windows.Forms.Label m_lblBuildOutput;
        private System.Windows.Forms.TextBox m_txtBuildOutput;
        private System.Windows.Forms.Label m_lblScriptOutput;
        private System.Windows.Forms.TextBox m_txtScriptOutput;
        private System.Windows.Forms.Button m_btnRunScript;
    }
}

