namespace SocketTestApp
{
    partial class ELockTerminal
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Iplabel = new System.Windows.Forms.Label();
            this.IpBox = new System.Windows.Forms.TextBox();
            this.PortBox = new System.Windows.Forms.TextBox();
            this.Connbutton = new System.Windows.Forms.Button();
            this.MsgBox = new System.Windows.Forms.TextBox();
            this.TimeOutlabel = new System.Windows.Forms.Label();
            this.TimeOutBox = new System.Windows.Forms.TextBox();
            this.KeyText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SimNumtext = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.VoltageText = new System.Windows.Forms.TextBox();
            this.VersionText = new System.Windows.Forms.TextBox();
            this.StatusText = new System.Windows.Forms.TextBox();
            this.JZDWText = new System.Windows.Forms.TextBox();
            this.AltitudeText = new System.Windows.Forms.TextBox();
            this.SpeedText = new System.Windows.Forms.TextBox();
            this.DirectionTezt = new System.Windows.Forms.TextBox();
            this.SendLocaltionBt = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.SendLocalNum = new System.Windows.Forms.TextBox();
            this.SendIntervals = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.PingNumText = new System.Windows.Forms.TextBox();
            this.PwdText = new System.Windows.Forms.TextBox();
            this.KeyLoginBtn = new System.Windows.Forms.Button();
            this.LogReportBtn = new System.Windows.Forms.Button();
            this.LockNumText = new System.Windows.Forms.TextBox();
            this.LockLable = new System.Windows.Forms.Label();
            this.ClearBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Iplabel
            // 
            this.Iplabel.AutoSize = true;
            this.Iplabel.Location = new System.Drawing.Point(-1, 33);
            this.Iplabel.Name = "Iplabel";
            this.Iplabel.Size = new System.Drawing.Size(31, 15);
            this.Iplabel.TabIndex = 0;
            this.Iplabel.Text = "IP:";
            // 
            // IpBox
            // 
            this.IpBox.Location = new System.Drawing.Point(36, 30);
            this.IpBox.Name = "IpBox";
            this.IpBox.Size = new System.Drawing.Size(169, 25);
            this.IpBox.TabIndex = 1;
            this.IpBox.Text = "172.16.10.147";
            // 
            // PortBox
            // 
            this.PortBox.Location = new System.Drawing.Point(211, 30);
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(71, 25);
            this.PortBox.TabIndex = 2;
            this.PortBox.Text = "8603";
            // 
            // Connbutton
            // 
            this.Connbutton.Location = new System.Drawing.Point(408, 115);
            this.Connbutton.Name = "Connbutton";
            this.Connbutton.Size = new System.Drawing.Size(123, 30);
            this.Connbutton.TabIndex = 3;
            this.Connbutton.Text = "连接(0x0011)";
            this.Connbutton.UseVisualStyleBackColor = true;
            this.Connbutton.Click += new System.EventHandler(this.Connbutton_Click);
            // 
            // MsgBox
            // 
            this.MsgBox.Location = new System.Drawing.Point(763, 12);
            this.MsgBox.Multiline = true;
            this.MsgBox.Name = "MsgBox";
            this.MsgBox.ReadOnly = true;
            this.MsgBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.MsgBox.Size = new System.Drawing.Size(339, 426);
            this.MsgBox.TabIndex = 4;
            // 
            // TimeOutlabel
            // 
            this.TimeOutlabel.AutoSize = true;
            this.TimeOutlabel.Location = new System.Drawing.Point(288, 33);
            this.TimeOutlabel.Name = "TimeOutlabel";
            this.TimeOutlabel.Size = new System.Drawing.Size(137, 15);
            this.TimeOutlabel.TabIndex = 8;
            this.TimeOutlabel.Text = "连接超时时间(ms):";
            // 
            // TimeOutBox
            // 
            this.TimeOutBox.Location = new System.Drawing.Point(431, 30);
            this.TimeOutBox.Name = "TimeOutBox";
            this.TimeOutBox.Size = new System.Drawing.Size(75, 25);
            this.TimeOutBox.TabIndex = 9;
            this.TimeOutBox.Text = "5000";
            // 
            // KeyText
            // 
            this.KeyText.Location = new System.Drawing.Point(36, 78);
            this.KeyText.Name = "KeyText";
            this.KeyText.Size = new System.Drawing.Size(100, 25);
            this.KeyText.TabIndex = 10;
            this.KeyText.Text = "KY100206";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "Key:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(150, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "SimNum:";
            // 
            // SimNumtext
            // 
            this.SimNumtext.Location = new System.Drawing.Point(211, 81);
            this.SimNumtext.Name = "SimNumtext";
            this.SimNumtext.Size = new System.Drawing.Size(142, 25);
            this.SimNumtext.TabIndex = 13;
            this.SimNumtext.Text = "440192670554";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-1, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "电压：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(-1, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "状态：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(230, 207);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 16;
            this.label5.Text = "基站定位：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(230, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 15);
            this.label6.TabIndex = 17;
            this.label6.Text = "版本：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(-1, 251);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 15);
            this.label7.TabIndex = 18;
            this.label7.Text = "高度（米）：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(230, 251);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 15);
            this.label8.TabIndex = 19;
            this.label8.Text = "速度（km/h）：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(-1, 300);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 15);
            this.label9.TabIndex = 20;
            this.label9.Text = "方向：";
            // 
            // VoltageText
            // 
            this.VoltageText.ForeColor = System.Drawing.SystemColors.WindowText;
            this.VoltageText.Location = new System.Drawing.Point(73, 158);
            this.VoltageText.Name = "VoltageText";
            this.VoltageText.Size = new System.Drawing.Size(100, 25);
            this.VoltageText.TabIndex = 21;
            this.VoltageText.Text = "42";
            // 
            // VersionText
            // 
            this.VersionText.Location = new System.Drawing.Point(315, 158);
            this.VersionText.Name = "VersionText";
            this.VersionText.Size = new System.Drawing.Size(100, 25);
            this.VersionText.TabIndex = 22;
            this.VersionText.Text = "040300";
            // 
            // StatusText
            // 
            this.StatusText.Location = new System.Drawing.Point(73, 207);
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(100, 25);
            this.StatusText.TabIndex = 23;
            this.StatusText.Text = "34000000";
            // 
            // JZDWText
            // 
            this.JZDWText.Location = new System.Drawing.Point(315, 204);
            this.JZDWText.Name = "JZDWText";
            this.JZDWText.Size = new System.Drawing.Size(100, 25);
            this.JZDWText.TabIndex = 24;
            this.JZDWText.Text = "810900B9";
            // 
            // AltitudeText
            // 
            this.AltitudeText.Location = new System.Drawing.Point(102, 248);
            this.AltitudeText.Name = "AltitudeText";
            this.AltitudeText.Size = new System.Drawing.Size(100, 25);
            this.AltitudeText.TabIndex = 25;
            this.AltitudeText.Text = "856";
            // 
            // SpeedText
            // 
            this.SpeedText.Location = new System.Drawing.Point(341, 248);
            this.SpeedText.Name = "SpeedText";
            this.SpeedText.Size = new System.Drawing.Size(100, 25);
            this.SpeedText.TabIndex = 26;
            this.SpeedText.Text = "12";
            // 
            // DirectionTezt
            // 
            this.DirectionTezt.Location = new System.Drawing.Point(73, 297);
            this.DirectionTezt.Name = "DirectionTezt";
            this.DirectionTezt.Size = new System.Drawing.Size(100, 25);
            this.DirectionTezt.TabIndex = 27;
            this.DirectionTezt.Text = "0";
            // 
            // SendLocaltionBt
            // 
            this.SendLocaltionBt.Enabled = false;
            this.SendLocaltionBt.Location = new System.Drawing.Point(365, 425);
            this.SendLocaltionBt.Name = "SendLocaltionBt";
            this.SendLocaltionBt.Size = new System.Drawing.Size(100, 52);
            this.SendLocaltionBt.TabIndex = 28;
            this.SendLocaltionBt.Text = "发送定位(0x0200)";
            this.SendLocaltionBt.UseVisualStyleBackColor = true;
            this.SendLocaltionBt.Click += new System.EventHandler(this.SendLocaltionBt_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(2, 338);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(227, 156);
            this.textBox1.TabIndex = 29;
            this.textBox1.Text = "版本号：040201，代表4.2.1；状态：需填写32位转换后的16进制数,高度速度填int值就行 ,方向0-359，正北为0,顺时针算，共360°；界面支持的是" +
    "主动请求的协议，还有其他被动发送的，都有，只需要服务端发送对应的协议，这边都会自动应答。";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(277, 341);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 15);
            this.label10.TabIndex = 30;
            this.label10.Text = "发送次数：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(245, 386);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(114, 15);
            this.label11.TabIndex = 31;
            this.label11.Text = "每次间隔(ms)：";
            // 
            // SendLocalNum
            // 
            this.SendLocalNum.Location = new System.Drawing.Point(365, 341);
            this.SendLocalNum.Name = "SendLocalNum";
            this.SendLocalNum.Size = new System.Drawing.Size(100, 25);
            this.SendLocalNum.TabIndex = 32;
            this.SendLocalNum.Text = "1";
            // 
            // SendIntervals
            // 
            this.SendIntervals.Location = new System.Drawing.Point(365, 383);
            this.SendIntervals.Name = "SendIntervals";
            this.SendIntervals.Size = new System.Drawing.Size(100, 25);
            this.SendIntervals.TabIndex = 33;
            this.SendIntervals.Text = "5000";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(462, 164);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 15);
            this.label12.TabIndex = 34;
            this.label12.Text = "Ping码：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(479, 210);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 15);
            this.label13.TabIndex = 35;
            this.label13.Text = "密码：";
            // 
            // PingNumText
            // 
            this.PingNumText.Location = new System.Drawing.Point(537, 161);
            this.PingNumText.Name = "PingNumText";
            this.PingNumText.Size = new System.Drawing.Size(162, 25);
            this.PingNumText.TabIndex = 36;
            // 
            // PwdText
            // 
            this.PwdText.Location = new System.Drawing.Point(537, 210);
            this.PwdText.Name = "PwdText";
            this.PwdText.Size = new System.Drawing.Size(162, 25);
            this.PwdText.TabIndex = 38;
            // 
            // KeyLoginBtn
            // 
            this.KeyLoginBtn.Enabled = false;
            this.KeyLoginBtn.Location = new System.Drawing.Point(603, 261);
            this.KeyLoginBtn.Name = "KeyLoginBtn";
            this.KeyLoginBtn.Size = new System.Drawing.Size(96, 54);
            this.KeyLoginBtn.TabIndex = 39;
            this.KeyLoginBtn.Text = "钥匙登陆(0x0302)";
            this.KeyLoginBtn.UseVisualStyleBackColor = true;
            this.KeyLoginBtn.Click += new System.EventHandler(this.KeyLoginBtn_Click);
            // 
            // LogReportBtn
            // 
            this.LogReportBtn.Enabled = false;
            this.LogReportBtn.Location = new System.Drawing.Point(559, 425);
            this.LogReportBtn.Name = "LogReportBtn";
            this.LogReportBtn.Size = new System.Drawing.Size(154, 52);
            this.LogReportBtn.TabIndex = 40;
            this.LogReportBtn.Text = "钥匙操作锁日志上报(0x701)";
            this.LogReportBtn.UseVisualStyleBackColor = true;
            this.LogReportBtn.Click += new System.EventHandler(this.LogReportBtn_Click);
            // 
            // LockNumText
            // 
            this.LockNumText.Location = new System.Drawing.Point(613, 376);
            this.LockNumText.Name = "LockNumText";
            this.LockNumText.Size = new System.Drawing.Size(100, 25);
            this.LockNumText.TabIndex = 41;
            // 
            // LockLable
            // 
            this.LockLable.AutoSize = true;
            this.LockLable.Location = new System.Drawing.Point(502, 383);
            this.LockLable.Name = "LockLable";
            this.LockLable.Size = new System.Drawing.Size(105, 15);
            this.LockLable.TabIndex = 42;
            this.LockLable.Text = "锁号/封铅号：";
            // 
            // ClearBtn
            // 
            this.ClearBtn.Location = new System.Drawing.Point(763, 444);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(102, 50);
            this.ClearBtn.TabIndex = 43;
            this.ClearBtn.Text = "清理消息";
            this.ClearBtn.UseVisualStyleBackColor = true;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // ELockTerminal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 500);
            this.Controls.Add(this.ClearBtn);
            this.Controls.Add(this.LockLable);
            this.Controls.Add(this.LockNumText);
            this.Controls.Add(this.LogReportBtn);
            this.Controls.Add(this.KeyLoginBtn);
            this.Controls.Add(this.PwdText);
            this.Controls.Add(this.PingNumText);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.SendIntervals);
            this.Controls.Add(this.SendLocalNum);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.SendLocaltionBt);
            this.Controls.Add(this.DirectionTezt);
            this.Controls.Add(this.SpeedText);
            this.Controls.Add(this.AltitudeText);
            this.Controls.Add(this.JZDWText);
            this.Controls.Add(this.StatusText);
            this.Controls.Add(this.VersionText);
            this.Controls.Add(this.VoltageText);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SimNumtext);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KeyText);
            this.Controls.Add(this.TimeOutBox);
            this.Controls.Add(this.TimeOutlabel);
            this.Controls.Add(this.MsgBox);
            this.Controls.Add(this.Connbutton);
            this.Controls.Add(this.PortBox);
            this.Controls.Add(this.IpBox);
            this.Controls.Add(this.Iplabel);
            this.Name = "ELockTerminal";
            this.Text = "ELockTerminal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Iplabel;
        private System.Windows.Forms.TextBox IpBox;
        private System.Windows.Forms.TextBox PortBox;
        private System.Windows.Forms.Button Connbutton;
        private System.Windows.Forms.TextBox MsgBox;
        private System.Windows.Forms.Label TimeOutlabel;
        private System.Windows.Forms.TextBox TimeOutBox;
        private System.Windows.Forms.TextBox KeyText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SimNumtext;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox VoltageText;
        private System.Windows.Forms.TextBox VersionText;
        private System.Windows.Forms.TextBox StatusText;
        private System.Windows.Forms.TextBox JZDWText;
        private System.Windows.Forms.TextBox AltitudeText;
        private System.Windows.Forms.TextBox SpeedText;
        private System.Windows.Forms.TextBox DirectionTezt;
        private System.Windows.Forms.Button SendLocaltionBt;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox SendLocalNum;
        private System.Windows.Forms.TextBox SendIntervals;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox PingNumText;
        private System.Windows.Forms.TextBox PwdText;
        private System.Windows.Forms.Button KeyLoginBtn;
        private System.Windows.Forms.Button LogReportBtn;
        private System.Windows.Forms.TextBox LockNumText;
        private System.Windows.Forms.Label LockLable;
        private System.Windows.Forms.Button ClearBtn;
    }
}

