using SocketTestApp.Case;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketTestApp
{
    public partial class ELockTerminal : Form
    {
        public ELockTerminal()
        {
            InitializeComponent();
        }

        private static ProtocolFunction Connection;
        private void Connbutton_Click(object sender, EventArgs e)
        {
            var ipstr = this.IpBox.Text;
            var port = Convert.ToInt32(this.PortBox.Text);
            var TimeOutMse = Convert.ToInt32(this.TimeOutBox.Text);
            var key = this.KeyText.Text;
            var simNum = this.SimNumtext.Text;
            if (port == 0 || port > 65535)
            {
                WinFormHelper.ShowInfo(MsgBox, "端口号应在0-65535之间");
                return;
            }
            if (string.IsNullOrEmpty(key))
            {
                WinFormHelper.ShowInfo(MsgBox, "key不能为空");
                return;
            }
            if (string.IsNullOrEmpty(simNum))
            {
                WinFormHelper.ShowInfo(MsgBox, "SimNum不能为空");
                return;
            }
            //if (startNum > 5000)
            //{
            //    WinFormHelper.ShowInfo(MsgBox, "开的线程过多请小于5000个！");
            //    return;
            //}
            if (TimeOutMse < 1000 || TimeOutMse > 20000)
            {
                WinFormHelper.ShowInfo(MsgBox, "超时时间应在1000-20000之间！");
                return;
            }
            //string ipPattern = "/^(?:(?:2[0-4][0-9]\\.)|(?:25[0-5]\\.)|(?:1[0-9][0-9]\\.)|(?:[1-9][0-9]\\.)|(?:[0-9]\\.)){3}(?:(?:2[0-5][0-5])|(?:25[0-5])|(?:1[0-9][0-9])|(?:[1-9][0-9])|(?:[0-9]))$/";
            //Regex regex = new Regex(ipPattern);
            //if (!regex.IsMatch(ipstr))
            //{
            //    WinFormHelper.ShowInfo(MsgBox, "请输入正确的IPv4号");
            //    return;
            //}
            this.Connbutton.Text = "正在处理...";
            this.Connbutton.Size = new System.Drawing.Size(105, 30);
            this.Connbutton.Enabled = false;
            ///开始连接
            try
            {
                Connection = new ProtocolFunction(ipstr, port, 5000, key, simNum, MsgBox);

                Connection.Start();
                this.Connbutton.Text = "连接完毕";
            }
            catch (Exception ex)
            {
                WinFormHelper.ShowInfo(MsgBox, ex.Message);
                FreeConnBtn();
                return;
            }
            this.Connbutton.Text = "关闭连接";
            this.Connbutton.Size = new System.Drawing.Size(105, 30);
            this.Connbutton.Enabled = true;
            EnableOtherBtn();
            this.Connbutton.Click -= Connbutton_Click;
            this.Connbutton.Click += Connbutton_Click_Close;
        }
        private void Connbutton_Click_Close(object sender, EventArgs e)
        {
            this.Connbutton.Text = "正在关闭连接";
            this.Connbutton.Size = new System.Drawing.Size(120, 30);
            this.Connbutton.Enabled = false;
            WinFormHelper.ShowInfo(MsgBox, "正在关闭连接......");
            ProtocolFunction.EndConn();
            WinFormHelper.ShowInfo(MsgBox, "连接已关闭......");
            FreeConnBtn();
            this.Connbutton.Click += Connbutton_Click;
            this.Connbutton.Click -= Connbutton_Click_Close;
        }

        private void SendLocaltionBt_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    if (c.Name == "PingNumText" || c.Name == "PwdText" || c.Name == "LockNumText")
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty((c as TextBox).Text))
                    {
                        MessageBox.Show("电压，版本，基站定位，状态，高度，速度，方向的text都不可为空！");
                        return;
                    }
                }
            }
            try
            {
                var vol = this.VoltageText.Text;
                var status = this.StatusText.Text;
                var version = "56" + this.VersionText.Text;
                var jzdw = this.JZDWText.Text;
                var altitude = Convert.ToInt32(this.AltitudeText.Text);
                var speed = Convert.ToInt32(this.SpeedText.Text);
                var direction = Convert.ToInt32(this.DirectionTezt.Text);
                var intervals = Convert.ToInt32(this.SendIntervals.Text);
                var sendNum = Convert.ToInt32(this.SendLocalNum.Text);
                for (var i = 0; i < sendNum; i++)
                {
                    ProtocolFunction.T808_0x0200_Process(vol, status, version, jzdw, altitude, speed, direction);
                    if (i < sendNum - 1)
                        Thread.Sleep(intervals);
                }
                //Connection.T808_0x0103_Process();
            }
            catch (Exception ex)
            {
                WinFormHelper.ShowInfo(MsgBox, ex.Message);
                FreeConnBtn();
            }

            // Connection.SendLocation();
        }

        public void FreeConnBtn()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Button)
                {
                    if (c.Name == "Connbutton")
                    {
                        c.Text = "连接(0x0011)";
                        c.Size = new System.Drawing.Size(123, 30);
                        c.Enabled = true;
                        continue;
                    }
                    c.Enabled = false;
                }
            }
        }
        private void EnableOtherBtn()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Button)
                {
                    if (c.Name == "Connbutton")
                    {
                        continue;
                    }
                    c.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 钥匙登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyLoginBtn_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    if (c.Name == "VoltageText" || c.Name == "StatusText" || c.Name == "VersionText" || c.Name == "LockNumText")
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty((c as TextBox).Text))
                    {
                        MessageBox.Show("ping码，密码，基站定位，状态，高度，速度，方向的text都不可为空！");
                        return;
                    }
                }
            }

            try
            {
                var pingnum = this.PingNumText.Text;
                var pwd = this.PwdText.Text;
                var jzdw = this.JZDWText.Text;
                var altitude = Convert.ToInt32(this.AltitudeText.Text);
                var speed = Convert.ToInt32(this.SpeedText.Text);
                var direction = Convert.ToInt32(this.DirectionTezt.Text);
                var intervals = Convert.ToInt32(this.SendIntervals.Text);
                var sendNum = Convert.ToInt32(this.SendLocalNum.Text);
                ProtocolFunction.T808_0x0302_Process(pingnum, pwd, jzdw, altitude, speed, direction);
            }
            catch (Exception ex)
            {
                WinFormHelper.ShowInfo(MsgBox, ex.Message);
                FreeConnBtn();
            }
        }

        private void LogReportBtn_Click(object sender, EventArgs e)
        {

            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    if (c.Name == "VoltageText" || c.Name == "StatusText" || c.Name == "VersionText")
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty((c as TextBox).Text))
                    {
                        MessageBox.Show("ping码，密码，锁号/铅封号，基站定位，状态，高度，速度，方向的text都不可为空！");
                        return;
                    }
                }
            }

            try
            {
                var pingnum = this.PingNumText.Text;
                var pwd = this.PwdText.Text;
                var jzdw = this.JZDWText.Text;
                var locknum = this.LockNumText.Text;
                var altitude = Convert.ToInt32(this.AltitudeText.Text);
                var speed = Convert.ToInt32(this.SpeedText.Text);
                var direction = Convert.ToInt32(this.DirectionTezt.Text);
                var intervals = Convert.ToInt32(this.SendIntervals.Text);
                var sendNum = Convert.ToInt32(this.SendLocalNum.Text);
                ProtocolFunction.T808_0x0701_Process(pingnum, pwd, locknum, jzdw, altitude, speed, direction);
            }
            catch (Exception ex)
            {
                WinFormHelper.ShowInfo(MsgBox, ex.Message);
                FreeConnBtn();
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            this.MsgBox.Clear();
        }
    }
}
