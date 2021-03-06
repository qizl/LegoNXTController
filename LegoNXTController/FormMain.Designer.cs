﻿namespace LegoNXTController
{
    partial class FormMain
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
            this.gbxOutput = new System.Windows.Forms.GroupBox();
            this.lbxOutput = new System.Windows.Forms.ListBox();
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.cbxPorts = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbpPorts = new System.Windows.Forms.TabPage();
            this.btnClosePort = new System.Windows.Forms.Button();
            this.tbpBaseDatas = new System.Windows.Forms.TabPage();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxProgramName = new System.Windows.Forms.ComboBox();
            this.cbxSoundFileName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnReadVersion = new System.Windows.Forms.Button();
            this.btnReadBattery = new System.Windows.Forms.Button();
            this.tbpControl = new System.Windows.Forms.TabPage();
            this.txtSKFocus = new System.Windows.Forms.TextBox();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnForwards = new System.Windows.Forms.Button();
            this.btnRotateA = new System.Windows.Forms.Button();
            this.gbxOutput.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbpPorts.SuspendLayout();
            this.tbpBaseDatas.SuspendLayout();
            this.tbpControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxOutput
            // 
            this.gbxOutput.Controls.Add(this.lbxOutput);
            this.gbxOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbxOutput.Location = new System.Drawing.Point(0, 253);
            this.gbxOutput.Name = "gbxOutput";
            this.gbxOutput.Size = new System.Drawing.Size(830, 211);
            this.gbxOutput.TabIndex = 0;
            this.gbxOutput.TabStop = false;
            this.gbxOutput.Text = "Output";
            // 
            // lbxOutput
            // 
            this.lbxOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxOutput.FormattingEnabled = true;
            this.lbxOutput.ItemHeight = 12;
            this.lbxOutput.Location = new System.Drawing.Point(3, 17);
            this.lbxOutput.Name = "lbxOutput";
            this.lbxOutput.Size = new System.Drawing.Size(824, 191);
            this.lbxOutput.TabIndex = 100;
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.Location = new System.Drawing.Point(127, 18);
            this.btnOpenPort.Name = "btnOpenPort";
            this.btnOpenPort.Size = new System.Drawing.Size(75, 23);
            this.btnOpenPort.TabIndex = 1;
            this.btnOpenPort.Text = "Open";
            this.btnOpenPort.UseVisualStyleBackColor = true;
            this.btnOpenPort.Click += new System.EventHandler(this.btnOpenPort_Click);
            // 
            // cbxPorts
            // 
            this.cbxPorts.FormattingEnabled = true;
            this.cbxPorts.Location = new System.Drawing.Point(28, 19);
            this.cbxPorts.Name = "cbxPorts";
            this.cbxPorts.Size = new System.Drawing.Size(93, 20);
            this.cbxPorts.TabIndex = 0;
            this.cbxPorts.Text = "COM3";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbpPorts);
            this.tabControl1.Controls.Add(this.tbpBaseDatas);
            this.tabControl1.Controls.Add(this.tbpControl);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(830, 253);
            this.tabControl1.TabIndex = 2;
            // 
            // tbpPorts
            // 
            this.tbpPorts.Controls.Add(this.btnClosePort);
            this.tbpPorts.Controls.Add(this.btnOpenPort);
            this.tbpPorts.Controls.Add(this.cbxPorts);
            this.tbpPorts.Location = new System.Drawing.Point(4, 22);
            this.tbpPorts.Name = "tbpPorts";
            this.tbpPorts.Padding = new System.Windows.Forms.Padding(3);
            this.tbpPorts.Size = new System.Drawing.Size(365, 159);
            this.tbpPorts.TabIndex = 0;
            this.tbpPorts.Text = "Ports";
            this.tbpPorts.UseVisualStyleBackColor = true;
            // 
            // btnClosePort
            // 
            this.btnClosePort.Location = new System.Drawing.Point(208, 18);
            this.btnClosePort.Name = "btnClosePort";
            this.btnClosePort.Size = new System.Drawing.Size(75, 23);
            this.btnClosePort.TabIndex = 2;
            this.btnClosePort.Text = "Close";
            this.btnClosePort.UseVisualStyleBackColor = true;
            this.btnClosePort.Click += new System.EventHandler(this.btnClosePort_Click);
            // 
            // tbpBaseDatas
            // 
            this.tbpBaseDatas.Controls.Add(this.btnRotateA);
            this.tbpBaseDatas.Controls.Add(this.btnStop);
            this.tbpBaseDatas.Controls.Add(this.btnRun);
            this.tbpBaseDatas.Controls.Add(this.label2);
            this.tbpBaseDatas.Controls.Add(this.cbxProgramName);
            this.tbpBaseDatas.Controls.Add(this.cbxSoundFileName);
            this.tbpBaseDatas.Controls.Add(this.label1);
            this.tbpBaseDatas.Controls.Add(this.btnPlay);
            this.tbpBaseDatas.Controls.Add(this.btnReadVersion);
            this.tbpBaseDatas.Controls.Add(this.btnReadBattery);
            this.tbpBaseDatas.Location = new System.Drawing.Point(4, 22);
            this.tbpBaseDatas.Name = "tbpBaseDatas";
            this.tbpBaseDatas.Padding = new System.Windows.Forms.Padding(3);
            this.tbpBaseDatas.Size = new System.Drawing.Size(822, 227);
            this.tbpBaseDatas.TabIndex = 1;
            this.tbpBaseDatas.Text = "Base Commands";
            this.tbpBaseDatas.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(249, 108);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(168, 107);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 5;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = ".rxe";
            // 
            // cbxProgramName
            // 
            this.cbxProgramName.FormattingEnabled = true;
            this.cbxProgramName.Items.AddRange(new object[] {
            "autorunning"});
            this.cbxProgramName.Location = new System.Drawing.Point(28, 108);
            this.cbxProgramName.Name = "cbxProgramName";
            this.cbxProgramName.Size = new System.Drawing.Size(111, 20);
            this.cbxProgramName.TabIndex = 4;
            this.cbxProgramName.Text = "autorunning";
            // 
            // cbxSoundFileName
            // 
            this.cbxSoundFileName.FormattingEnabled = true;
            this.cbxSoundFileName.Items.AddRange(new object[] {
            "good",
            "good job",
            "night",
            "have a nice day",
            "you\'re good"});
            this.cbxSoundFileName.Location = new System.Drawing.Point(28, 69);
            this.cbxSoundFileName.Name = "cbxSoundFileName";
            this.cbxSoundFileName.Size = new System.Drawing.Size(111, 20);
            this.cbxSoundFileName.TabIndex = 2;
            this.cbxSoundFileName.Text = "good job";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = ".rso";
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(168, 68);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 3;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnReadVersion
            // 
            this.btnReadVersion.Location = new System.Drawing.Point(121, 19);
            this.btnReadVersion.Name = "btnReadVersion";
            this.btnReadVersion.Size = new System.Drawing.Size(75, 23);
            this.btnReadVersion.TabIndex = 1;
            this.btnReadVersion.Text = "Version";
            this.btnReadVersion.UseVisualStyleBackColor = true;
            this.btnReadVersion.Click += new System.EventHandler(this.btnReadVersion_Click);
            // 
            // btnReadBattery
            // 
            this.btnReadBattery.Location = new System.Drawing.Point(28, 19);
            this.btnReadBattery.Name = "btnReadBattery";
            this.btnReadBattery.Size = new System.Drawing.Size(75, 23);
            this.btnReadBattery.TabIndex = 0;
            this.btnReadBattery.Text = "Battery";
            this.btnReadBattery.UseVisualStyleBackColor = true;
            this.btnReadBattery.Click += new System.EventHandler(this.btnReadBattery_Click);
            // 
            // tbpControl
            // 
            this.tbpControl.Controls.Add(this.txtSKFocus);
            this.tbpControl.Controls.Add(this.btnRight);
            this.tbpControl.Controls.Add(this.btnLeft);
            this.tbpControl.Controls.Add(this.btnBack);
            this.tbpControl.Controls.Add(this.btnForwards);
            this.tbpControl.Location = new System.Drawing.Point(4, 22);
            this.tbpControl.Name = "tbpControl";
            this.tbpControl.Size = new System.Drawing.Size(365, 159);
            this.tbpControl.TabIndex = 2;
            this.tbpControl.Text = "Directions";
            this.tbpControl.UseVisualStyleBackColor = true;
            // 
            // txtSKFocus
            // 
            this.txtSKFocus.Location = new System.Drawing.Point(28, 21);
            this.txtSKFocus.Name = "txtSKFocus";
            this.txtSKFocus.ReadOnly = true;
            this.txtSKFocus.Size = new System.Drawing.Size(58, 21);
            this.txtSKFocus.TabIndex = 0;
            this.txtSKFocus.Text = "↑↓←→";
            this.txtSKFocus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSKFocus_KeyDown);
            // 
            // btnRight
            // 
            this.btnRight.Location = new System.Drawing.Point(262, 61);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(75, 23);
            this.btnRight.TabIndex = 2;
            this.btnRight.Text = "Right";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(28, 61);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(75, 23);
            this.btnLeft.TabIndex = 1;
            this.btnLeft.Text = "Left";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(145, 112);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnForwards
            // 
            this.btnForwards.Location = new System.Drawing.Point(145, 19);
            this.btnForwards.Name = "btnForwards";
            this.btnForwards.Size = new System.Drawing.Size(75, 23);
            this.btnForwards.TabIndex = 0;
            this.btnForwards.Text = "Forwards";
            this.btnForwards.UseVisualStyleBackColor = true;
            this.btnForwards.Click += new System.EventHandler(this.btnForwards_Click);
            // 
            // btnRotateA
            // 
            this.btnRotateA.Location = new System.Drawing.Point(340, 19);
            this.btnRotateA.Name = "btnRotateA";
            this.btnRotateA.Size = new System.Drawing.Size(75, 23);
            this.btnRotateA.TabIndex = 7;
            this.btnRotateA.Text = "Rotate A";
            this.btnRotateA.UseVisualStyleBackColor = true;
            this.btnRotateA.Click += new System.EventHandler(this.btnRotateA_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 464);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.gbxOutput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LEGO NXT Controller";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.gbxOutput.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tbpPorts.ResumeLayout(false);
            this.tbpBaseDatas.ResumeLayout(false);
            this.tbpBaseDatas.PerformLayout();
            this.tbpControl.ResumeLayout(false);
            this.tbpControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxOutput;
        private System.Windows.Forms.Button btnOpenPort;
        private System.Windows.Forms.ComboBox cbxPorts;
        private System.Windows.Forms.ListBox lbxOutput;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbpPorts;
        private System.Windows.Forms.TabPage tbpBaseDatas;
        private System.Windows.Forms.Button btnClosePort;
        private System.Windows.Forms.Button btnReadVersion;
        private System.Windows.Forms.Button btnReadBattery;
        private System.Windows.Forms.TabPage tbpControl;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnForwards;
        private System.Windows.Forms.TextBox txtSKFocus;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxSoundFileName;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxProgramName;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRotateA;
    }
}

