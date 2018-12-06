namespace DupeFinder
{
    partial class Main
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
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fdbMain = new System.Windows.Forms.FolderBrowserDialog();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.btnCompareFiles = new System.Windows.Forms.Button();
            this.lblCurrentFile = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tempTreeView = new System.Windows.Forms.TreeView();
            this.radByHash = new System.Windows.Forms.RadioButton();
            this.radByteByByte = new System.Windows.Forms.RadioButton();
            this.grpCompareMethod = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkRecursive = new System.Windows.Forms.CheckBox();
            this.mnuMain.SuspendLayout();
            this.grpCompareMethod.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.mnuMain.Size = new System.Drawing.Size(584, 24);
            this.mnuMain.TabIndex = 0;
            this.mnuMain.Text = "mainMenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 22);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comingToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.H)));
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // comingToolStripMenuItem
            // 
            this.comingToolStripMenuItem.Name = "comingToolStripMenuItem";
            this.comingToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.comingToolStripMenuItem.Text = "Coming...";
            // 
            // fdbMain
            // 
            this.fdbMain.ShowNewFolderButton = false;
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(15, 55);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.ReadOnly = true;
            this.txtFolderPath.Size = new System.Drawing.Size(410, 20);
            this.txtFolderPath.TabIndex = 1;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(466, 55);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(90, 23);
            this.btnSelectFolder.TabIndex = 2;
            this.btnSelectFolder.Text = "Select Folder";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // btnCompareFiles
            // 
            this.btnCompareFiles.Location = new System.Drawing.Point(466, 165);
            this.btnCompareFiles.Name = "btnCompareFiles";
            this.btnCompareFiles.Size = new System.Drawing.Size(90, 23);
            this.btnCompareFiles.TabIndex = 3;
            this.btnCompareFiles.Text = "Compare Files";
            this.btnCompareFiles.UseVisualStyleBackColor = true;
            this.btnCompareFiles.Click += new System.EventHandler(this.btnCompareFiles_Click);
            // 
            // lblCurrentFile
            // 
            this.lblCurrentFile.Location = new System.Drawing.Point(12, 211);
            this.lblCurrentFile.Name = "lblCurrentFile";
            this.lblCurrentFile.Size = new System.Drawing.Size(410, 23);
            this.lblCurrentFile.TabIndex = 4;
            this.lblCurrentFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 165);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(410, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(466, 205);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoEllipsis = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 192);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(333, 23);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tempTreeView
            // 
            this.tempTreeView.Location = new System.Drawing.Point(574, 230);
            this.tempTreeView.Name = "tempTreeView";
            this.tempTreeView.Size = new System.Drawing.Size(10, 10);
            this.tempTreeView.TabIndex = 9;
            this.tempTreeView.TabStop = false;
            this.tempTreeView.Visible = false;
            // 
            // radByHash
            // 
            this.radByHash.AutoSize = true;
            this.radByHash.Location = new System.Drawing.Point(10, 41);
            this.radByHash.Name = "radByHash";
            this.radByHash.Size = new System.Drawing.Size(50, 17);
            this.radByHash.TabIndex = 10;
            this.radByHash.Text = "Hash";
            this.radByHash.UseVisualStyleBackColor = true;
            // 
            // radByteByByte
            // 
            this.radByteByByte.AutoSize = true;
            this.radByteByByte.Checked = true;
            this.radByteByByte.Location = new System.Drawing.Point(10, 20);
            this.radByteByByte.Name = "radByteByByte";
            this.radByteByByte.Size = new System.Drawing.Size(84, 17);
            this.radByteByByte.TabIndex = 11;
            this.radByteByByte.TabStop = true;
            this.radByteByByte.Text = "Byte-by-Byte";
            this.radByteByByte.UseVisualStyleBackColor = true;
            // 
            // grpCompareMethod
            // 
            this.grpCompareMethod.Controls.Add(this.radByHash);
            this.grpCompareMethod.Controls.Add(this.radByteByByte);
            this.grpCompareMethod.Location = new System.Drawing.Point(15, 88);
            this.grpCompareMethod.Name = "grpCompareMethod";
            this.grpCompareMethod.Size = new System.Drawing.Size(126, 68);
            this.grpCompareMethod.TabIndex = 12;
            this.grpCompareMethod.TabStop = false;
            this.grpCompareMethod.Text = "Comparison Method";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkRecursive);
            this.groupBox1.Location = new System.Drawing.Point(164, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(110, 49);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Include Subfolders";
            // 
            // chkRecursive
            // 
            this.chkRecursive.AutoSize = true;
            this.chkRecursive.Location = new System.Drawing.Point(11, 20);
            this.chkRecursive.Name = "chkRecursive";
            this.chkRecursive.Size = new System.Drawing.Size(44, 17);
            this.chkRecursive.TabIndex = 0;
            this.chkRecursive.Text = "Yes";
            this.chkRecursive.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 251);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpCompareMethod);
            this.Controls.Add(this.tempTreeView);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblCurrentFile);
            this.Controls.Add(this.btnCompareFiles);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.txtFolderPath);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = this.mnuMain;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Main";
            this.Text = "DupeFinder";
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.grpCompareMethod.ResumeLayout(false);
            this.grpCompareMethod.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comingToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog fdbMain;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Button btnCompareFiles;
        private System.Windows.Forms.Label lblCurrentFile;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TreeView tempTreeView;
        private System.Windows.Forms.RadioButton radByHash;
        private System.Windows.Forms.RadioButton radByteByByte;
        private System.Windows.Forms.GroupBox grpCompareMethod;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkRecursive;
    }
}

