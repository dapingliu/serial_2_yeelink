namespace Location_Tracking
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.cboxSerialPort = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtStatusLog = new System.Windows.Forms.TextBox();
            this.btnClearInfo = new System.Windows.Forms.Button();
            this.cboxAutoConnect = new System.Windows.Forms.CheckBox();
            this.txtServerURL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.cboxBdRate = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 369);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(457, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.IsLink = true;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(260, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "感谢Nicholas Skinner的源码";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(180, 16);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "串口号:";
            // 
            // cboxSerialPort
            // 
            this.cboxSerialPort.FormattingEnabled = true;
            this.cboxSerialPort.Location = new System.Drawing.Point(74, 6);
            this.cboxSerialPort.Name = "cboxSerialPort";
            this.cboxSerialPort.Size = new System.Drawing.Size(96, 20);
            this.cboxSerialPort.TabIndex = 0;
            this.cboxSerialPort.TextChanged += new System.EventHandler(this.cboxSerialPort_TextChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(15, 84);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(430, 30);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "连接服务器";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.TextChanged += new System.EventHandler(this.btnConnect_TextChanged);
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtStatusLog
            // 
            this.txtStatusLog.BackColor = System.Drawing.SystemColors.Window;
            this.txtStatusLog.Location = new System.Drawing.Point(14, 120);
            this.txtStatusLog.Multiline = true;
            this.txtStatusLog.Name = "txtStatusLog";
            this.txtStatusLog.ReadOnly = true;
            this.txtStatusLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatusLog.Size = new System.Drawing.Size(431, 215);
            this.txtStatusLog.TabIndex = 4;
            // 
            // btnClearInfo
            // 
            this.btnClearInfo.Location = new System.Drawing.Point(11, 339);
            this.btnClearInfo.Name = "btnClearInfo";
            this.btnClearInfo.Size = new System.Drawing.Size(43, 21);
            this.btnClearInfo.TabIndex = 5;
            this.btnClearInfo.Text = "清除";
            this.btnClearInfo.UseVisualStyleBackColor = true;
            this.btnClearInfo.Click += new System.EventHandler(this.btnClearInfo_Click);
            // 
            // cboxAutoConnect
            // 
            this.cboxAutoConnect.AutoSize = true;
            this.cboxAutoConnect.Location = new System.Drawing.Point(373, 9);
            this.cboxAutoConnect.Name = "cboxAutoConnect";
            this.cboxAutoConnect.Size = new System.Drawing.Size(72, 16);
            this.cboxAutoConnect.TabIndex = 1;
            this.cboxAutoConnect.Text = "自动连接";
            this.cboxAutoConnect.UseVisualStyleBackColor = true;
            this.cboxAutoConnect.CheckedChanged += new System.EventHandler(this.cboxAutoConnect_CheckedChanged);
            // 
            // txtServerURL
            // 
            this.txtServerURL.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtServerURL.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.txtServerURL.Location = new System.Drawing.Point(74, 30);
            this.txtServerURL.Name = "txtServerURL";
            this.txtServerURL.Size = new System.Drawing.Size(371, 21);
            this.txtServerURL.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "上传地址:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "API-KEY:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtApiKey
            // 
            this.txtApiKey.Location = new System.Drawing.Point(74, 57);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Size = new System.Drawing.Size(371, 21);
            this.txtApiKey.TabIndex = 10;
            // 
            // cboxBdRate
            // 
            this.cboxBdRate.FormattingEnabled = true;
            this.cboxBdRate.Items.AddRange(new object[] {
            "110",
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "56000",
            "57600",
            "115200"});
            this.cboxBdRate.Location = new System.Drawing.Point(229, 7);
            this.cboxBdRate.Name = "cboxBdRate";
            this.cboxBdRate.Size = new System.Drawing.Size(123, 20);
            this.cboxBdRate.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(176, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "波特率:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(350, 348);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(95, 12);
            this.linkLabel1.TabIndex = 13;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "www.yeelink.net";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 391);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboxBdRate);
            this.Controls.Add(this.txtApiKey);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtServerURL);
            this.Controls.Add(this.cboxAutoConnect);
            this.Controls.Add(this.btnClearInfo);
            this.Controls.Add(this.txtStatusLog);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.cboxSerialPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Yeelink串口工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboxSerialPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtStatusLog;
        private System.Windows.Forms.Button btnClearInfo;
        private System.Windows.Forms.CheckBox cboxAutoConnect;
        private System.Windows.Forms.TextBox txtServerURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtApiKey;
        private System.Windows.Forms.ComboBox cboxBdRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

