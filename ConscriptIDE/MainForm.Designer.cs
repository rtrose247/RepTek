namespace Conscript.IDE
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.m_spcHorizontal = new System.Windows.Forms.SplitContainer();
            this.m_spcVertical = new System.Windows.Forms.SplitContainer();
            this.m_grpScripts = new System.Windows.Forms.GroupBox();
            this.m_tbcScripts = new System.Windows.Forms.TabControl();
            this.m_cmsScript = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_tmiScriptSave = new System.Windows.Forms.ToolStripMenuItem();
            this.m_tmiScriptClose = new System.Windows.Forms.ToolStripMenuItem();
            this.m_grpVirtualMachine = new System.Windows.Forms.GroupBox();
            this.m_tbcVirtualMachine = new System.Windows.Forms.TabControl();
            this.m_tbpByteCode = new System.Windows.Forms.TabPage();
            this.m_lsbByteCode = new Conscript.IDE.ByteCodeListBox(this.components);
            this.m_tbpScopeGlobal = new System.Windows.Forms.TabPage();
            this.m_vdtGlobal = new Conscript.IDE.VariableDictionaryTreeView();
            this.m_tbpScopeScript = new System.Windows.Forms.TabPage();
            this.m_vdtScript = new Conscript.IDE.VariableDictionaryTreeView();
            this.m_tbpScopeLocal = new System.Windows.Forms.TabPage();
            this.m_vdtLocal = new Conscript.IDE.VariableDictionaryTreeView();
            this.m_tbpCallStack = new System.Windows.Forms.TabPage();
            this.m_lsbCallStack = new System.Windows.Forms.ListBox();
            this.m_tbpParameterStack = new System.Windows.Forms.TabPage();
            this.m_lsbParameterStack = new System.Windows.Forms.ListBox();
            this.m_tbpLocks = new System.Windows.Forms.TabPage();
            this.m_lsbLocks = new System.Windows.Forms.ListBox();
            this.m_grpOutput = new System.Windows.Forms.GroupBox();
            this.m_stsStatus = new System.Windows.Forms.StatusStrip();
            this.m_tslMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_tslLineNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_tslCharNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_txtOutput = new System.Windows.Forms.TextBox();
            this.m_mnsMain = new System.Windows.Forms.MenuStrip();
            this.m_mniFile = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniFileClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mniFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniEditUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniEditRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mniEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniEditDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mniEditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniBuild = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniBuildScript = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniBuildRebuild = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniBuildSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniBuildHostEnvironment = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniDebugStart = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniDebugRun = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniDebugBreak = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniDebugStop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mniDebugStepInto = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniDebugStepOver = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniDebugStepOut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.m_mniDebugToggleBreakpoint = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniDebugDeleteAllBreakpoints = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.m_mniHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.m_tmrDebugger = new System.Windows.Forms.Timer(this.components);
            this.m_spcHorizontal.Panel1.SuspendLayout();
            this.m_spcHorizontal.Panel2.SuspendLayout();
            this.m_spcHorizontal.SuspendLayout();
            this.m_spcVertical.Panel1.SuspendLayout();
            this.m_spcVertical.Panel2.SuspendLayout();
            this.m_spcVertical.SuspendLayout();
            this.m_grpScripts.SuspendLayout();
            this.m_cmsScript.SuspendLayout();
            this.m_grpVirtualMachine.SuspendLayout();
            this.m_tbcVirtualMachine.SuspendLayout();
            this.m_tbpByteCode.SuspendLayout();
            this.m_tbpScopeGlobal.SuspendLayout();
            this.m_tbpScopeScript.SuspendLayout();
            this.m_tbpScopeLocal.SuspendLayout();
            this.m_tbpCallStack.SuspendLayout();
            this.m_tbpParameterStack.SuspendLayout();
            this.m_tbpLocks.SuspendLayout();
            this.m_grpOutput.SuspendLayout();
            this.m_stsStatus.SuspendLayout();
            this.m_mnsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_spcHorizontal
            // 
            this.m_spcHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_spcHorizontal.Location = new System.Drawing.Point(0, 24);
            this.m_spcHorizontal.Name = "m_spcHorizontal";
            this.m_spcHorizontal.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_spcHorizontal.Panel1
            // 
            this.m_spcHorizontal.Panel1.Controls.Add(this.m_spcVertical);
            // 
            // m_spcHorizontal.Panel2
            // 
            this.m_spcHorizontal.Panel2.Controls.Add(this.m_grpOutput);
            this.m_spcHorizontal.Size = new System.Drawing.Size(784, 540);
            this.m_spcHorizontal.SplitterDistance = 430;
            this.m_spcHorizontal.TabIndex = 1;
            // 
            // m_spcVertical
            // 
            this.m_spcVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_spcVertical.Location = new System.Drawing.Point(0, 0);
            this.m_spcVertical.Name = "m_spcVertical";
            // 
            // m_spcVertical.Panel1
            // 
            this.m_spcVertical.Panel1.Controls.Add(this.m_grpScripts);
            // 
            // m_spcVertical.Panel2
            // 
            this.m_spcVertical.Panel2.Controls.Add(this.m_grpVirtualMachine);
            this.m_spcVertical.Size = new System.Drawing.Size(784, 430);
            this.m_spcVertical.SplitterDistance = 500;
            this.m_spcVertical.TabIndex = 3;
            // 
            // m_grpScripts
            // 
            this.m_grpScripts.Controls.Add(this.m_tbcScripts);
            this.m_grpScripts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_grpScripts.Location = new System.Drawing.Point(0, 0);
            this.m_grpScripts.Name = "m_grpScripts";
            this.m_grpScripts.Size = new System.Drawing.Size(500, 430);
            this.m_grpScripts.TabIndex = 2;
            this.m_grpScripts.TabStop = false;
            this.m_grpScripts.Text = "Scripts";
            // 
            // m_tbcScripts
            // 
            this.m_tbcScripts.ContextMenuStrip = this.m_cmsScript;
            this.m_tbcScripts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tbcScripts.HotTrack = true;
            this.m_tbcScripts.Location = new System.Drawing.Point(3, 16);
            this.m_tbcScripts.Name = "m_tbcScripts";
            this.m_tbcScripts.SelectedIndex = 0;
            this.m_tbcScripts.Size = new System.Drawing.Size(494, 411);
            this.m_tbcScripts.TabIndex = 1;
            this.m_tbcScripts.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_tbcScripts_MouseDown);
            this.m_tbcScripts.SelectedIndexChanged += new System.EventHandler(this.m_tbcScripts_SelectedIndexChanged);
            // 
            // m_cmsScript
            // 
            this.m_cmsScript.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tmiScriptSave,
            this.m_tmiScriptClose});
            this.m_cmsScript.Name = "m_cmsScript";
            this.m_cmsScript.Size = new System.Drawing.Size(104, 48);
            // 
            // m_tmiScriptSave
            // 
            this.m_tmiScriptSave.Image = global::Conscript.IDE.Properties.Resources.FileSave;
            this.m_tmiScriptSave.Name = "m_tmiScriptSave";
            this.m_tmiScriptSave.Size = new System.Drawing.Size(103, 22);
            this.m_tmiScriptSave.Text = "Save";
            this.m_tmiScriptSave.Click += new System.EventHandler(this.m_mniFileSave_Click);
            // 
            // m_tmiScriptClose
            // 
            this.m_tmiScriptClose.Name = "m_tmiScriptClose";
            this.m_tmiScriptClose.Size = new System.Drawing.Size(103, 22);
            this.m_tmiScriptClose.Text = "Close";
            this.m_tmiScriptClose.Click += new System.EventHandler(this.m_mniFileClose_Click);
            // 
            // m_grpVirtualMachine
            // 
            this.m_grpVirtualMachine.Controls.Add(this.m_tbcVirtualMachine);
            this.m_grpVirtualMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_grpVirtualMachine.Location = new System.Drawing.Point(0, 0);
            this.m_grpVirtualMachine.Name = "m_grpVirtualMachine";
            this.m_grpVirtualMachine.Size = new System.Drawing.Size(280, 430);
            this.m_grpVirtualMachine.TabIndex = 0;
            this.m_grpVirtualMachine.TabStop = false;
            this.m_grpVirtualMachine.Text = "Virtual Machine";
            // 
            // m_tbcVirtualMachine
            // 
            this.m_tbcVirtualMachine.Controls.Add(this.m_tbpByteCode);
            this.m_tbcVirtualMachine.Controls.Add(this.m_tbpScopeGlobal);
            this.m_tbcVirtualMachine.Controls.Add(this.m_tbpScopeScript);
            this.m_tbcVirtualMachine.Controls.Add(this.m_tbpScopeLocal);
            this.m_tbcVirtualMachine.Controls.Add(this.m_tbpCallStack);
            this.m_tbcVirtualMachine.Controls.Add(this.m_tbpParameterStack);
            this.m_tbcVirtualMachine.Controls.Add(this.m_tbpLocks);
            this.m_tbcVirtualMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_tbcVirtualMachine.Location = new System.Drawing.Point(3, 16);
            this.m_tbcVirtualMachine.Name = "m_tbcVirtualMachine";
            this.m_tbcVirtualMachine.SelectedIndex = 0;
            this.m_tbcVirtualMachine.Size = new System.Drawing.Size(274, 411);
            this.m_tbcVirtualMachine.TabIndex = 0;
            // 
            // m_tbpByteCode
            // 
            this.m_tbpByteCode.Controls.Add(this.m_lsbByteCode);
            this.m_tbpByteCode.Location = new System.Drawing.Point(4, 22);
            this.m_tbpByteCode.Name = "m_tbpByteCode";
            this.m_tbpByteCode.Size = new System.Drawing.Size(266, 385);
            this.m_tbpByteCode.TabIndex = 4;
            this.m_tbpByteCode.Text = "Byte Code";
            this.m_tbpByteCode.UseVisualStyleBackColor = true;
            // 
            // m_lsbByteCode
            // 
            this.m_lsbByteCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_lsbByteCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsbByteCode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.m_lsbByteCode.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lsbByteCode.FormattingEnabled = true;
            this.m_lsbByteCode.IntegralHeight = false;
            this.m_lsbByteCode.ItemHeight = 16;
            this.m_lsbByteCode.Location = new System.Drawing.Point(0, 0);
            this.m_lsbByteCode.Name = "m_lsbByteCode";
            this.m_lsbByteCode.NextInstruction = 0;
            this.m_lsbByteCode.Size = new System.Drawing.Size(266, 385);
            this.m_lsbByteCode.TabIndex = 1;
            // 
            // m_tbpScopeGlobal
            // 
            this.m_tbpScopeGlobal.Controls.Add(this.m_vdtGlobal);
            this.m_tbpScopeGlobal.Location = new System.Drawing.Point(4, 22);
            this.m_tbpScopeGlobal.Name = "m_tbpScopeGlobal";
            this.m_tbpScopeGlobal.Padding = new System.Windows.Forms.Padding(3);
            this.m_tbpScopeGlobal.Size = new System.Drawing.Size(266, 385);
            this.m_tbpScopeGlobal.TabIndex = 0;
            this.m_tbpScopeGlobal.Text = "Global Scope";
            this.m_tbpScopeGlobal.UseVisualStyleBackColor = true;
            // 
            // m_vdtGlobal
            // 
            this.m_vdtGlobal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_vdtGlobal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_vdtGlobal.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.m_vdtGlobal.Location = new System.Drawing.Point(3, 3);
            this.m_vdtGlobal.Name = "m_vdtGlobal";
            this.m_vdtGlobal.Size = new System.Drawing.Size(260, 379);
            this.m_vdtGlobal.TabIndex = 0;
            this.m_vdtGlobal.VariableDictionary = null;
            // 
            // m_tbpScopeScript
            // 
            this.m_tbpScopeScript.Controls.Add(this.m_vdtScript);
            this.m_tbpScopeScript.Location = new System.Drawing.Point(4, 22);
            this.m_tbpScopeScript.Name = "m_tbpScopeScript";
            this.m_tbpScopeScript.Padding = new System.Windows.Forms.Padding(3);
            this.m_tbpScopeScript.Size = new System.Drawing.Size(266, 385);
            this.m_tbpScopeScript.TabIndex = 1;
            this.m_tbpScopeScript.Text = "Script Scope";
            this.m_tbpScopeScript.UseVisualStyleBackColor = true;
            // 
            // m_vdtScript
            // 
            this.m_vdtScript.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_vdtScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_vdtScript.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.m_vdtScript.Location = new System.Drawing.Point(3, 3);
            this.m_vdtScript.Name = "m_vdtScript";
            this.m_vdtScript.Size = new System.Drawing.Size(260, 379);
            this.m_vdtScript.TabIndex = 0;
            this.m_vdtScript.VariableDictionary = null;
            // 
            // m_tbpScopeLocal
            // 
            this.m_tbpScopeLocal.Controls.Add(this.m_vdtLocal);
            this.m_tbpScopeLocal.Location = new System.Drawing.Point(4, 22);
            this.m_tbpScopeLocal.Name = "m_tbpScopeLocal";
            this.m_tbpScopeLocal.Size = new System.Drawing.Size(266, 385);
            this.m_tbpScopeLocal.TabIndex = 2;
            this.m_tbpScopeLocal.Text = "Local Scope";
            this.m_tbpScopeLocal.UseVisualStyleBackColor = true;
            // 
            // m_vdtLocal
            // 
            this.m_vdtLocal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_vdtLocal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_vdtLocal.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.m_vdtLocal.Location = new System.Drawing.Point(0, 0);
            this.m_vdtLocal.Name = "m_vdtLocal";
            this.m_vdtLocal.Size = new System.Drawing.Size(266, 385);
            this.m_vdtLocal.TabIndex = 0;
            this.m_vdtLocal.VariableDictionary = null;
            // 
            // m_tbpCallStack
            // 
            this.m_tbpCallStack.Controls.Add(this.m_lsbCallStack);
            this.m_tbpCallStack.Location = new System.Drawing.Point(4, 22);
            this.m_tbpCallStack.Name = "m_tbpCallStack";
            this.m_tbpCallStack.Size = new System.Drawing.Size(266, 385);
            this.m_tbpCallStack.TabIndex = 6;
            this.m_tbpCallStack.Text = "Call Stack";
            this.m_tbpCallStack.UseVisualStyleBackColor = true;
            // 
            // m_lsbCallStack
            // 
            this.m_lsbCallStack.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_lsbCallStack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsbCallStack.FormattingEnabled = true;
            this.m_lsbCallStack.IntegralHeight = false;
            this.m_lsbCallStack.Location = new System.Drawing.Point(0, 0);
            this.m_lsbCallStack.Name = "m_lsbCallStack";
            this.m_lsbCallStack.Size = new System.Drawing.Size(266, 385);
            this.m_lsbCallStack.TabIndex = 0;
            // 
            // m_tbpParameterStack
            // 
            this.m_tbpParameterStack.Controls.Add(this.m_lsbParameterStack);
            this.m_tbpParameterStack.Location = new System.Drawing.Point(4, 22);
            this.m_tbpParameterStack.Name = "m_tbpParameterStack";
            this.m_tbpParameterStack.Size = new System.Drawing.Size(266, 385);
            this.m_tbpParameterStack.TabIndex = 5;
            this.m_tbpParameterStack.Text = "Parameter Stack";
            this.m_tbpParameterStack.UseVisualStyleBackColor = true;
            // 
            // m_lsbParameterStack
            // 
            this.m_lsbParameterStack.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_lsbParameterStack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsbParameterStack.FormattingEnabled = true;
            this.m_lsbParameterStack.IntegralHeight = false;
            this.m_lsbParameterStack.Location = new System.Drawing.Point(0, 0);
            this.m_lsbParameterStack.Name = "m_lsbParameterStack";
            this.m_lsbParameterStack.Size = new System.Drawing.Size(266, 385);
            this.m_lsbParameterStack.TabIndex = 0;
            // 
            // m_tbpLocks
            // 
            this.m_tbpLocks.Controls.Add(this.m_lsbLocks);
            this.m_tbpLocks.Location = new System.Drawing.Point(4, 22);
            this.m_tbpLocks.Name = "m_tbpLocks";
            this.m_tbpLocks.Size = new System.Drawing.Size(266, 385);
            this.m_tbpLocks.TabIndex = 3;
            this.m_tbpLocks.Text = "Locks";
            this.m_tbpLocks.UseVisualStyleBackColor = true;
            // 
            // m_lsbLocks
            // 
            this.m_lsbLocks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_lsbLocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lsbLocks.FormattingEnabled = true;
            this.m_lsbLocks.IntegralHeight = false;
            this.m_lsbLocks.Location = new System.Drawing.Point(0, 0);
            this.m_lsbLocks.Name = "m_lsbLocks";
            this.m_lsbLocks.Size = new System.Drawing.Size(266, 385);
            this.m_lsbLocks.TabIndex = 0;
            // 
            // m_grpOutput
            // 
            this.m_grpOutput.Controls.Add(this.m_stsStatus);
            this.m_grpOutput.Controls.Add(this.m_txtOutput);
            this.m_grpOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_grpOutput.Location = new System.Drawing.Point(0, 0);
            this.m_grpOutput.Name = "m_grpOutput";
            this.m_grpOutput.Size = new System.Drawing.Size(784, 106);
            this.m_grpOutput.TabIndex = 2;
            this.m_grpOutput.TabStop = false;
            this.m_grpOutput.Text = "Output";
            // 
            // m_stsStatus
            // 
            this.m_stsStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_tslMessage,
            this.m_tslLineNumber,
            this.m_tslCharNumber});
            this.m_stsStatus.Location = new System.Drawing.Point(3, 81);
            this.m_stsStatus.Name = "m_stsStatus";
            this.m_stsStatus.Size = new System.Drawing.Size(778, 22);
            this.m_stsStatus.TabIndex = 2;
            this.m_stsStatus.Text = "Status Strip";
            // 
            // m_tslMessage
            // 
            this.m_tslMessage.AutoSize = false;
            this.m_tslMessage.Name = "m_tslMessage";
            this.m_tslMessage.Size = new System.Drawing.Size(635, 17);
            this.m_tslMessage.Spring = true;
            this.m_tslMessage.Text = "Ready";
            this.m_tslMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_tslLineNumber
            // 
            this.m_tslLineNumber.AutoSize = false;
            this.m_tslLineNumber.Name = "m_tslLineNumber";
            this.m_tslLineNumber.Size = new System.Drawing.Size(64, 17);
            this.m_tslLineNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_tslCharNumber
            // 
            this.m_tslCharNumber.AutoSize = false;
            this.m_tslCharNumber.Name = "m_tslCharNumber";
            this.m_tslCharNumber.Size = new System.Drawing.Size(64, 17);
            this.m_tslCharNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_txtOutput
            // 
            this.m_txtOutput.AcceptsReturn = true;
            this.m_txtOutput.AcceptsTab = true;
            this.m_txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_txtOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_txtOutput.Location = new System.Drawing.Point(3, 16);
            this.m_txtOutput.Multiline = true;
            this.m_txtOutput.Name = "m_txtOutput";
            this.m_txtOutput.ReadOnly = true;
            this.m_txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.m_txtOutput.Size = new System.Drawing.Size(778, 87);
            this.m_txtOutput.TabIndex = 1;
            // 
            // m_mnsMain
            // 
            this.m_mnsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mniFile,
            this.m_mniEdit,
            this.m_mniBuild,
            this.m_mniDebug,
            this.m_mniHelp});
            this.m_mnsMain.Location = new System.Drawing.Point(0, 0);
            this.m_mnsMain.Name = "m_mnsMain";
            this.m_mnsMain.Size = new System.Drawing.Size(784, 24);
            this.m_mnsMain.TabIndex = 2;
            this.m_mnsMain.Text = "menuStrip1";
            // 
            // m_mniFile
            // 
            this.m_mniFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mniFileNew,
            this.m_mniFileOpen,
            this.m_mniFileClose,
            this.toolStripSeparator1,
            this.m_mniFileSave,
            this.m_mniFileSaveAs,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.m_mniFile.Name = "m_mniFile";
            this.m_mniFile.Size = new System.Drawing.Size(37, 20);
            this.m_mniFile.Text = "&File";
            // 
            // m_mniFileNew
            // 
            this.m_mniFileNew.Image = global::Conscript.IDE.Properties.Resources.FileNew;
            this.m_mniFileNew.Name = "m_mniFileNew";
            this.m_mniFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.m_mniFileNew.Size = new System.Drawing.Size(155, 22);
            this.m_mniFileNew.Text = "&New";
            this.m_mniFileNew.Click += new System.EventHandler(this.m_mniFileNew_Click);
            // 
            // m_mniFileOpen
            // 
            this.m_mniFileOpen.Image = global::Conscript.IDE.Properties.Resources.FileOpen;
            this.m_mniFileOpen.Name = "m_mniFileOpen";
            this.m_mniFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.m_mniFileOpen.Size = new System.Drawing.Size(155, 22);
            this.m_mniFileOpen.Text = "&Open...";
            this.m_mniFileOpen.Click += new System.EventHandler(this.m_mniFileOpen_Click);
            // 
            // m_mniFileClose
            // 
            this.m_mniFileClose.Name = "m_mniFileClose";
            this.m_mniFileClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.m_mniFileClose.Size = new System.Drawing.Size(155, 22);
            this.m_mniFileClose.Text = "&Close";
            this.m_mniFileClose.Click += new System.EventHandler(this.m_mniFileClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // m_mniFileSave
            // 
            this.m_mniFileSave.Image = global::Conscript.IDE.Properties.Resources.FileSave;
            this.m_mniFileSave.Name = "m_mniFileSave";
            this.m_mniFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.m_mniFileSave.Size = new System.Drawing.Size(155, 22);
            this.m_mniFileSave.Text = "&Save";
            this.m_mniFileSave.Click += new System.EventHandler(this.m_mniFileSave_Click);
            // 
            // m_mniFileSaveAs
            // 
            this.m_mniFileSaveAs.Name = "m_mniFileSaveAs";
            this.m_mniFileSaveAs.Size = new System.Drawing.Size(155, 22);
            this.m_mniFileSaveAs.Text = "Save &As...";
            this.m_mniFileSaveAs.Click += new System.EventHandler(this.m_mniFileSaveAs_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(152, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // m_mniEdit
            // 
            this.m_mniEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mniEditUndo,
            this.m_mniEditRedo,
            this.toolStripSeparator3,
            this.m_mniEditCut,
            this.m_mniEditCopy,
            this.m_mniEditPaste,
            this.m_mniEditDelete,
            this.toolStripSeparator6,
            this.m_mniEditSelectAll});
            this.m_mniEdit.Name = "m_mniEdit";
            this.m_mniEdit.Size = new System.Drawing.Size(39, 20);
            this.m_mniEdit.Text = "&Edit";
            // 
            // m_mniEditUndo
            // 
            this.m_mniEditUndo.Image = global::Conscript.IDE.Properties.Resources.EditUndo;
            this.m_mniEditUndo.Name = "m_mniEditUndo";
            this.m_mniEditUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.m_mniEditUndo.Size = new System.Drawing.Size(164, 22);
            this.m_mniEditUndo.Text = "Undo";
            this.m_mniEditUndo.Click += new System.EventHandler(this.m_mniEditUndo_Click);
            // 
            // m_mniEditRedo
            // 
            this.m_mniEditRedo.Image = global::Conscript.IDE.Properties.Resources.EditRedo;
            this.m_mniEditRedo.Name = "m_mniEditRedo";
            this.m_mniEditRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.m_mniEditRedo.Size = new System.Drawing.Size(164, 22);
            this.m_mniEditRedo.Text = "Redo";
            this.m_mniEditRedo.Click += new System.EventHandler(this.m_mniEditRedo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(161, 6);
            // 
            // m_mniEditCut
            // 
            this.m_mniEditCut.Image = global::Conscript.IDE.Properties.Resources.EditCut;
            this.m_mniEditCut.Name = "m_mniEditCut";
            this.m_mniEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.m_mniEditCut.Size = new System.Drawing.Size(164, 22);
            this.m_mniEditCut.Text = "Cut";
            this.m_mniEditCut.Click += new System.EventHandler(this.m_mniEditCut_Click);
            // 
            // m_mniEditCopy
            // 
            this.m_mniEditCopy.Image = global::Conscript.IDE.Properties.Resources.EditCopy;
            this.m_mniEditCopy.Name = "m_mniEditCopy";
            this.m_mniEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.m_mniEditCopy.Size = new System.Drawing.Size(164, 22);
            this.m_mniEditCopy.Text = "Copy";
            this.m_mniEditCopy.Click += new System.EventHandler(this.m_mniEditCopy_Click);
            // 
            // m_mniEditPaste
            // 
            this.m_mniEditPaste.Image = global::Conscript.IDE.Properties.Resources.EditPaste;
            this.m_mniEditPaste.Name = "m_mniEditPaste";
            this.m_mniEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.m_mniEditPaste.Size = new System.Drawing.Size(164, 22);
            this.m_mniEditPaste.Text = "Paste";
            this.m_mniEditPaste.Click += new System.EventHandler(this.m_mniEditPaste_Click);
            // 
            // m_mniEditDelete
            // 
            this.m_mniEditDelete.Image = global::Conscript.IDE.Properties.Resources.EditDelete;
            this.m_mniEditDelete.Name = "m_mniEditDelete";
            this.m_mniEditDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.m_mniEditDelete.Size = new System.Drawing.Size(164, 22);
            this.m_mniEditDelete.Text = "Delete";
            this.m_mniEditDelete.Click += new System.EventHandler(this.m_mniEditDelete_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(161, 6);
            // 
            // m_mniEditSelectAll
            // 
            this.m_mniEditSelectAll.Name = "m_mniEditSelectAll";
            this.m_mniEditSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.m_mniEditSelectAll.Size = new System.Drawing.Size(164, 22);
            this.m_mniEditSelectAll.Text = "Select All";
            this.m_mniEditSelectAll.Click += new System.EventHandler(this.m_mniEditSelectAll_Click);
            // 
            // m_mniBuild
            // 
            this.m_mniBuild.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mniBuildScript,
            this.m_mniBuildRebuild,
            this.m_mniBuildSettings,
            this.m_mniBuildHostEnvironment});
            this.m_mniBuild.Name = "m_mniBuild";
            this.m_mniBuild.Size = new System.Drawing.Size(46, 20);
            this.m_mniBuild.Text = "&Build";
            // 
            // m_mniBuildScript
            // 
            this.m_mniBuildScript.Image = global::Conscript.IDE.Properties.Resources.BuildScript;
            this.m_mniBuildScript.Name = "m_mniBuildScript";
            this.m_mniBuildScript.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.m_mniBuildScript.Size = new System.Drawing.Size(179, 22);
            this.m_mniBuildScript.Text = "Build Script";
            this.m_mniBuildScript.Click += new System.EventHandler(this.m_mniBuildScript_Click);
            // 
            // m_mniBuildRebuild
            // 
            this.m_mniBuildRebuild.Name = "m_mniBuildRebuild";
            this.m_mniBuildRebuild.Size = new System.Drawing.Size(179, 22);
            this.m_mniBuildRebuild.Text = "&Rebuild Script";
            this.m_mniBuildRebuild.Click += new System.EventHandler(this.m_mniBuildRebuild_Click);
            // 
            // m_mniBuildSettings
            // 
            this.m_mniBuildSettings.Image = global::Conscript.IDE.Properties.Resources.BuildSettings;
            this.m_mniBuildSettings.Name = "m_mniBuildSettings";
            this.m_mniBuildSettings.Size = new System.Drawing.Size(179, 22);
            this.m_mniBuildSettings.Text = "Build &Settings...";
            this.m_mniBuildSettings.Click += new System.EventHandler(this.m_mniBuildSettings_Click);
            // 
            // m_mniBuildHostEnvironment
            // 
            this.m_mniBuildHostEnvironment.Image = global::Conscript.IDE.Properties.Resources.BuildHostEnvironment;
            this.m_mniBuildHostEnvironment.Name = "m_mniBuildHostEnvironment";
            this.m_mniBuildHostEnvironment.Size = new System.Drawing.Size(179, 22);
            this.m_mniBuildHostEnvironment.Text = "&Host Environment...";
            this.m_mniBuildHostEnvironment.Click += new System.EventHandler(this.m_mniBuildHostEnvironment_Click);
            // 
            // m_mniDebug
            // 
            this.m_mniDebug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mniDebugStart,
            this.m_mniDebugRun,
            this.m_mniDebugBreak,
            this.m_mniDebugStop,
            this.toolStripSeparator4,
            this.m_mniDebugStepInto,
            this.m_mniDebugStepOver,
            this.m_mniDebugStepOut,
            this.toolStripSeparator5,
            this.m_mniDebugToggleBreakpoint,
            this.m_mniDebugDeleteAllBreakpoints});
            this.m_mniDebug.Name = "m_mniDebug";
            this.m_mniDebug.Size = new System.Drawing.Size(54, 20);
            this.m_mniDebug.Text = "&Debug";
            // 
            // m_mniDebugStart
            // 
            this.m_mniDebugStart.Image = global::Conscript.IDE.Properties.Resources.DebugStart;
            this.m_mniDebugStart.Name = "m_mniDebugStart";
            this.m_mniDebugStart.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.m_mniDebugStart.Size = new System.Drawing.Size(267, 22);
            this.m_mniDebugStart.Text = "Start Debugging";
            this.m_mniDebugStart.Click += new System.EventHandler(this.m_mniDebugStart_Click);
            // 
            // m_mniDebugRun
            // 
            this.m_mniDebugRun.Image = global::Conscript.IDE.Properties.Resources.DebugRun;
            this.m_mniDebugRun.Name = "m_mniDebugRun";
            this.m_mniDebugRun.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.m_mniDebugRun.Size = new System.Drawing.Size(267, 22);
            this.m_mniDebugRun.Text = "Start Without Debugging";
            this.m_mniDebugRun.Click += new System.EventHandler(this.m_mniDebugRun_Click);
            // 
            // m_mniDebugBreak
            // 
            this.m_mniDebugBreak.Image = global::Conscript.IDE.Properties.Resources.DebugBreak;
            this.m_mniDebugBreak.Name = "m_mniDebugBreak";
            this.m_mniDebugBreak.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F12)));
            this.m_mniDebugBreak.Size = new System.Drawing.Size(267, 22);
            this.m_mniDebugBreak.Text = "Break";
            this.m_mniDebugBreak.Click += new System.EventHandler(this.m_mniDebugBreak_Click);
            // 
            // m_mniDebugStop
            // 
            this.m_mniDebugStop.Image = global::Conscript.IDE.Properties.Resources.DebugStop;
            this.m_mniDebugStop.Name = "m_mniDebugStop";
            this.m_mniDebugStop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.End)));
            this.m_mniDebugStop.Size = new System.Drawing.Size(267, 22);
            this.m_mniDebugStop.Text = "Stop";
            this.m_mniDebugStop.Click += new System.EventHandler(this.m_mniDebugStop_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(264, 6);
            // 
            // m_mniDebugStepInto
            // 
            this.m_mniDebugStepInto.Image = global::Conscript.IDE.Properties.Resources.DebugStepInto;
            this.m_mniDebugStepInto.Name = "m_mniDebugStepInto";
            this.m_mniDebugStepInto.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.m_mniDebugStepInto.Size = new System.Drawing.Size(267, 22);
            this.m_mniDebugStepInto.Text = "Step Into";
            this.m_mniDebugStepInto.Click += new System.EventHandler(this.m_mniDebugStepInto_Click);
            // 
            // m_mniDebugStepOver
            // 
            this.m_mniDebugStepOver.Image = global::Conscript.IDE.Properties.Resources.DebugStepOver;
            this.m_mniDebugStepOver.Name = "m_mniDebugStepOver";
            this.m_mniDebugStepOver.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.m_mniDebugStepOver.Size = new System.Drawing.Size(267, 22);
            this.m_mniDebugStepOver.Text = "Step Over";
            this.m_mniDebugStepOver.Click += new System.EventHandler(this.m_mniDebugStepOver_Click);
            // 
            // m_mniDebugStepOut
            // 
            this.m_mniDebugStepOut.Image = global::Conscript.IDE.Properties.Resources.DebugStepOut;
            this.m_mniDebugStepOut.Name = "m_mniDebugStepOut";
            this.m_mniDebugStepOut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F11)));
            this.m_mniDebugStepOut.Size = new System.Drawing.Size(267, 22);
            this.m_mniDebugStepOut.Text = "Step Out";
            this.m_mniDebugStepOut.Click += new System.EventHandler(this.m_mniDebugStepOut_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(264, 6);
            // 
            // m_mniDebugToggleBreakpoint
            // 
            this.m_mniDebugToggleBreakpoint.Image = global::Conscript.IDE.Properties.Resources.DebugToggleBreakpoint;
            this.m_mniDebugToggleBreakpoint.Name = "m_mniDebugToggleBreakpoint";
            this.m_mniDebugToggleBreakpoint.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.m_mniDebugToggleBreakpoint.Size = new System.Drawing.Size(267, 22);
            this.m_mniDebugToggleBreakpoint.Text = "Toggle Breakpoint";
            this.m_mniDebugToggleBreakpoint.Click += new System.EventHandler(this.m_mniDebugToggleBreakpoint_Click);
            // 
            // m_mniDebugDeleteAllBreakpoints
            // 
            this.m_mniDebugDeleteAllBreakpoints.Image = global::Conscript.IDE.Properties.Resources.DebugClearBreakpoints;
            this.m_mniDebugDeleteAllBreakpoints.Name = "m_mniDebugDeleteAllBreakpoints";
            this.m_mniDebugDeleteAllBreakpoints.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.F9)));
            this.m_mniDebugDeleteAllBreakpoints.Size = new System.Drawing.Size(267, 22);
            this.m_mniDebugDeleteAllBreakpoints.Text = "Delete All Breakpoints";
            this.m_mniDebugDeleteAllBreakpoints.Click += new System.EventHandler(this.m_mniDebugDeleteAllBreakpoints_Click);
            // 
            // m_mniHelp
            // 
            this.m_mniHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_mniHelpAbout});
            this.m_mniHelp.Name = "m_mniHelp";
            this.m_mniHelp.Size = new System.Drawing.Size(44, 20);
            this.m_mniHelp.Text = "&Help";
            // 
            // m_mniHelpAbout
            // 
            this.m_mniHelpAbout.Image = global::Conscript.IDE.Properties.Resources.HelpAbout;
            this.m_mniHelpAbout.Name = "m_mniHelpAbout";
            this.m_mniHelpAbout.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.m_mniHelpAbout.Size = new System.Drawing.Size(200, 22);
            this.m_mniHelpAbout.Text = "&About Conscript IDE";
            this.m_mniHelpAbout.Click += new System.EventHandler(this.m_mniHelpAbout_Click);
            // 
            // m_tmrDebugger
            // 
            this.m_tmrDebugger.Interval = 1;
            this.m_tmrDebugger.Tick += new System.EventHandler(this.m_tmrDebugger_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 564);
            this.Controls.Add(this.m_spcHorizontal);
            this.Controls.Add(this.m_mnsMain);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.m_mnsMain;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Conscript IDE";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.m_spcHorizontal.Panel1.ResumeLayout(false);
            this.m_spcHorizontal.Panel2.ResumeLayout(false);
            this.m_spcHorizontal.ResumeLayout(false);
            this.m_spcVertical.Panel1.ResumeLayout(false);
            this.m_spcVertical.Panel2.ResumeLayout(false);
            this.m_spcVertical.ResumeLayout(false);
            this.m_grpScripts.ResumeLayout(false);
            this.m_cmsScript.ResumeLayout(false);
            this.m_grpVirtualMachine.ResumeLayout(false);
            this.m_tbcVirtualMachine.ResumeLayout(false);
            this.m_tbpByteCode.ResumeLayout(false);
            this.m_tbpScopeGlobal.ResumeLayout(false);
            this.m_tbpScopeScript.ResumeLayout(false);
            this.m_tbpScopeLocal.ResumeLayout(false);
            this.m_tbpCallStack.ResumeLayout(false);
            this.m_tbpParameterStack.ResumeLayout(false);
            this.m_tbpLocks.ResumeLayout(false);
            this.m_grpOutput.ResumeLayout(false);
            this.m_grpOutput.PerformLayout();
            this.m_stsStatus.ResumeLayout(false);
            this.m_stsStatus.PerformLayout();
            this.m_mnsMain.ResumeLayout(false);
            this.m_mnsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer m_spcHorizontal;
        private System.Windows.Forms.MenuStrip m_mnsMain;
        private System.Windows.Forms.ToolStripMenuItem m_mniFile;
        private System.Windows.Forms.TextBox m_txtOutput;
        private System.Windows.Forms.ToolStripMenuItem m_mniFileNew;
        private System.Windows.Forms.ToolStripMenuItem m_mniFileOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem m_mniFileSave;
        private System.Windows.Forms.ToolStripMenuItem m_mniFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabControl m_tbcScripts;
        private System.Windows.Forms.ToolStripMenuItem m_mniEdit;
        private System.Windows.Forms.ToolStripMenuItem m_mniEditCut;
        private System.Windows.Forms.ToolStripMenuItem m_mniEditCopy;
        private System.Windows.Forms.ToolStripMenuItem m_mniEditPaste;
        private System.Windows.Forms.ToolStripMenuItem m_mniBuild;
        private System.Windows.Forms.ToolStripMenuItem m_mniBuildScript;
        private System.Windows.Forms.ToolStripMenuItem m_mniBuildSettings;
        private System.Windows.Forms.ToolStripMenuItem m_mniBuildHostEnvironment;
        private System.Windows.Forms.GroupBox m_grpOutput;
        private System.Windows.Forms.GroupBox m_grpScripts;
        private System.Windows.Forms.SplitContainer m_spcVertical;
        private System.Windows.Forms.GroupBox m_grpVirtualMachine;
        private System.Windows.Forms.TabControl m_tbcVirtualMachine;
        private System.Windows.Forms.TabPage m_tbpScopeGlobal;
        private System.Windows.Forms.TabPage m_tbpScopeScript;
        private System.Windows.Forms.TabPage m_tbpScopeLocal;
        private System.Windows.Forms.TabPage m_tbpByteCode;
        private System.Windows.Forms.TabPage m_tbpLocks;
        private System.Windows.Forms.ToolStripMenuItem m_mniFileClose;
        private System.Windows.Forms.TabPage m_tbpCallStack;
        private System.Windows.Forms.TabPage m_tbpParameterStack;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem m_mniEditSelectAll;
        private System.Windows.Forms.ToolStripMenuItem m_mniDebug;
        private System.Windows.Forms.ToolStripMenuItem m_mniDebugStart;
        private System.Windows.Forms.ToolStripMenuItem m_mniDebugRun;
        private System.Windows.Forms.ToolStripMenuItem m_mniDebugStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem m_mniDebugStepInto;
        private System.Windows.Forms.ToolStripMenuItem m_mniDebugStepOver;
        private System.Windows.Forms.ToolStripMenuItem m_mniDebugStepOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem m_mniDebugToggleBreakpoint;
        private System.Windows.Forms.ToolStripMenuItem m_mniDebugDeleteAllBreakpoints;
        private System.Windows.Forms.Timer m_tmrDebugger;
        private System.Windows.Forms.ToolStripMenuItem m_mniDebugBreak;
        private ByteCodeListBox m_lsbByteCode;
        private System.Windows.Forms.ListBox m_lsbParameterStack;
        private VariableDictionaryTreeView m_vdtLocal;
        private VariableDictionaryTreeView m_vdtScript;
        private VariableDictionaryTreeView m_vdtGlobal;
        private System.Windows.Forms.ListBox m_lsbCallStack;
        private System.Windows.Forms.ToolStripMenuItem m_mniBuildRebuild;
        private System.Windows.Forms.ToolStripMenuItem m_mniHelp;
        private System.Windows.Forms.ToolStripMenuItem m_mniHelpAbout;
        private System.Windows.Forms.ListBox m_lsbLocks;
        private System.Windows.Forms.StatusStrip m_stsStatus;
        private System.Windows.Forms.ToolStripStatusLabel m_tslMessage;
        private System.Windows.Forms.ToolStripStatusLabel m_tslLineNumber;
        private System.Windows.Forms.ToolStripStatusLabel m_tslCharNumber;
        private System.Windows.Forms.ToolStripMenuItem m_mniEditDelete;
        private System.Windows.Forms.ToolStripMenuItem m_mniEditUndo;
        private System.Windows.Forms.ToolStripMenuItem m_mniEditRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ContextMenuStrip m_cmsScript;
        private System.Windows.Forms.ToolStripMenuItem m_tmiScriptSave;
        private System.Windows.Forms.ToolStripMenuItem m_tmiScriptClose;
    }
}

