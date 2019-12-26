using SocketTestApp.Common;
using SocketTestApp.Protocol.Common;
using SocketTestApp.Protocol.Protocol808.MessageBody;
using SocketTestApp.Protocol.Protocol808.Process;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketTestApp.Case
{
    public class ProtocolFunction
    {
        private static Socket clientSocket;
        private byte[] receive = new byte[1024];
        private bool isConnection = false;
        private static System.Windows.Forms.TextBox txtInfo;
        private static string key;
        private static string simNum;
        public ProtocolFunction(string ipstr, int port, int timeOutSec, string _key, string _simnum, System.Windows.Forms.TextBox msgBox)
        {
            IPAddress ip = IPAddress.Parse(ipstr);
            txtInfo = msgBox;
            key = _key;
            simNum = _simnum;
            WinFormHelper.ShowInfo(txtInfo, $"开始连接Connection");
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // clientSocket.Connect(new IPEndPoint(ip, port)); //配置服务器IP与端口
            clientSocket.BeginConnect(new IPEndPoint(ip, port), CallBackMethod, clientSocket);
            WinFormHelper.ShowInfo(txtInfo, $"Connection 正在连接......");
            //阻塞当前线程           
            if (TimeoutObject.WaitOne(timeOutSec, false))
            {
                if (!clientSocket.Connected)
                {
                    WinFormHelper.ShowInfo(txtInfo, "连接服务器失败");
                    throw new Exception("socket连接失败");
                }
                WinFormHelper.ShowInfo(txtInfo, $"连接服务器成功，占用端口：{clientSocket.LocalEndPoint.ToString()}");
                isConnection = true;

            }
            else
            {
                throw new Exception("连接服务器超时了");
            }
        }

        private ManualResetEvent TimeoutObject = new ManualResetEvent(false);

        private void CallBackMethod(IAsyncResult asyncresult)
        {
            TimeoutObject.Set();
        }
        public static void EndConn()
        {
            if (clientSocket.Connected)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
        }

        public void Start()
        {
            if (!isConnection)
            {
                throw new Exception("socket未连接");
            }
            T808_0x0011_Process();
            Task.Run(() =>
            {
                while (true)
                {
                    if (clientSocket.Connected)
                    {
                        Receive();
                    }
                    else
                    {
                        WinFormHelper.ShowInfo(txtInfo, "服务器连接已断开");
                        break;
                    }

                }
            });
        }

        /// <summary>
        /// 发送登陆请求
        /// </summary>
        private static void T808_0x0011_Process()
        {
            //s SleepSendData(0);
            T808_0x0011 body = new T808_0x0011();
            body.setTerminalID(simNum);
            T808_MessageHeader mh = new T808_MessageHeader();
            // 获取终端对应的sim卡号
            mh.setSimNum(simNum);
            var cm = new CommonMessage<T808_0x0011>(body.getMessageID(), mh, body);

            //通过 clientSocket 发送数据
            try
            {
                var porto = new T808_0x0011_Process<T808_0x0011>();
                var sendByte = porto.PackData(cm, key);
                clientSocket.Send(sendByte);

                WinFormHelper.ShowInfo(txtInfo, $"向服务器发送登陆消息：{ToolHelper.ByteConvertToHex(sendByte)}");
            }
            catch (Exception ex)
            {
                WinFormHelper.ShowInfo(txtInfo, $"发送登陆消息过程出现异常：{ex.Message},关闭连接！");
                EndConn();
            }
            WinFormHelper.ShowInfo(txtInfo, $"连接登陆消息发送完毕，保持连接！");
        }

        /// <summary>
        /// 平台设置终端参数，终端应答
        /// </summary>
        public static void T808_0x8103AndT808_0x0103_Process()
        {
            T808_0x0103 body = new T808_0x0103();
            body.setTerminalID(simNum);
            T808_MessageHeader mh = new T808_MessageHeader();
            // 获取终端对应的sim卡号
            mh.setSimNum(simNum);
            var cm = new CommonMessage<T808_0x0103>(body.getMessageID(), mh, body);
            try
            {
                var porto = new T808_0x0103_Process<T808_0x0103>();
                var sendByte = porto.PackData(cm, key);
                clientSocket.Send(sendByte);
                WinFormHelper.ShowInfo(txtInfo, $"向服务器发送设置终端参数回复消息：{ToolHelper.ByteConvertToHex(sendByte)}");
            }
            catch (Exception ex)
            {
                WinFormHelper.ShowInfo(txtInfo, $"设置终端参数回复消息发送过程出现异常：{ex.Message},关闭连接！");
                EndConn();
            }
        }

        /// <summary>
        /// 终端位置信息汇报，心跳
        /// </summary>
        /// <param name="voltage">电压</param>
        /// <param name="status">状态32位</param>
        /// <param name="version">版本</param>
        /// <param name="jzdw">基站定位</param>
        /// <param name="altitude">高度</param>
        /// <param name="speed">速度</param>
        /// <param name="direction">方向</param>
        public static void T808_0x0200_Process(string voltage, string status, string version, string jzdw, int altitude, int speed, int direction)
        {
            T808_0x0200 body = new T808_0x0200();
            body.setTerminalID(simNum);
            T808_MessageHeader mh = new T808_MessageHeader();
            // 获取终端对应的sim卡号
            mh.setSimNum(simNum);
            var cm = new CommonMessage<T808_0x0200>(body.getMessageID(), mh, body);
            try
            {
                var porto = new T808_0x0200_Process<T808_0x0200>();
                var sendByte = porto.PackData(cm, key, voltage, status, version, jzdw, altitude, speed, direction);
                clientSocket.Send(sendByte);
                WinFormHelper.ShowInfo(txtInfo, $"向服务器发送位置消息：{ToolHelper.ByteConvertToHex(sendByte)}");
            }
            catch (Exception ex)
            {
                WinFormHelper.ShowInfo(txtInfo, $"发送位置消息过程出现异常：{ex.Message},关闭连接！");
                EndConn();
            }
        }

        /// <summary>
        /// 电子围栏平台设置命令，终端回应
        /// </summary>
        private static void T808_0x8300AndT808_0x0300_Process()
        {

            T808_0x0300 body = new T808_0x0300();
            body.setTerminalID(simNum);
            T808_MessageHeader mh = new T808_MessageHeader();
            // 获取终端对应的sim卡号
            mh.setSimNum(simNum);
            var cm = new CommonMessage<T808_0x0300>(body.getMessageID(), mh, body);
            try
            {
                var porto = new T808_0x0300_Process<T808_0x0300>();
                var sendByte = porto.PackData(cm, key);
                clientSocket.Send(sendByte);
                WinFormHelper.ShowInfo(txtInfo, $"向服务器发送电子围栏设置应答消息：{ToolHelper.ByteConvertToHex(sendByte)}");
            }
            catch (Exception ex)
            {
                WinFormHelper.ShowInfo(txtInfo, $"电子围栏设置应答消息发送过程出现异常：{ex.Message},关闭连接！");
                EndConn();
            }
        }

        /// <summary>
        /// 智能钥匙登陆请求
        /// </summary>
        /// <param name="pinNum">操作员ping码</param>
        /// <param name="password">操作员密码</param>
        /// <param name="jzdw">基站定位</param>
        /// <param name="altitude">高度</param>
        /// <param name="speed">速度</param>
        /// <param name="direction">方向</param>
        public static void T808_0x0302_Process(string ping, string password, string jzdw, int altitude, int speed, int direction)
        {

            T808_0x0302 body = new T808_0x0302();
            body.setTerminalID(simNum);
            T808_MessageHeader mh = new T808_MessageHeader();
            // 获取终端对应的sim卡号
            mh.setSimNum(simNum);
            var cm = new CommonMessage<T808_0x0302>(body.getMessageID(), mh, body);
            try
            {
                var porto = new T808_0x0302_Process<T808_0x0302>();
                var sendByte = porto.PackData(cm, key, ping, password, jzdw, altitude, speed, direction);
                clientSocket.Send(sendByte);
                WinFormHelper.ShowInfo(txtInfo, $"向服务器发送智能钥匙登陆消息：{ToolHelper.ByteConvertToHex(sendByte)}");
            }
            catch (Exception ex)
            {
                WinFormHelper.ShowInfo(txtInfo, $"智能钥匙登陆消息发送过程出现异常：{ex.Message},关闭连接！");
                EndConn();
            }
        }

        /// <summary>
        /// 平台 设备控制命令，终端应答
        /// </summary>
        private static void T808_0x8600AndT808_0x0600_Process()
        {

            T808_0x0600 body = new T808_0x0600();
            body.setTerminalID(simNum);
            T808_MessageHeader mh = new T808_MessageHeader();
            // 获取终端对应的sim卡号
            mh.setSimNum(simNum);
            var cm = new CommonMessage<T808_0x0600>(body.getMessageID(), mh, body);
            try
            {
                var porto = new T808_0x0600_Process<T808_0x0600>();
                var sendByte = porto.PackData(cm, key);
                WinFormHelper.ShowInfo(txtInfo, $"向服务器发送电子围栏设置应答消息：{ToolHelper.ByteConvertToHex(sendByte)}");
            }
            catch (Exception ex)
            {
                WinFormHelper.ShowInfo(txtInfo, $"电子围栏设置应答消息发送过程出现异常：{ex.Message},关闭连接！");
                EndConn();
            }
        }

        /// <summary>
        /// 平台直接控制电子锁命令，终端应答
        /// </summary>
        private static void T808_0x8700AndT808_0x0700_Process()
        {
            T808_0x0700 body = new T808_0x0700();
            body.setTerminalID(simNum);
            T808_MessageHeader mh = new T808_MessageHeader();
            // 获取终端对应的sim卡号
            mh.setSimNum(simNum);
            var cm = new CommonMessage<T808_0x0700>(body.getMessageID(), mh, body);
            try
            {
                var porto = new T808_0x0700_Process<T808_0x0700>();
                var sendByte = porto.PackData(cm, key);
                clientSocket.Send(sendByte);
                WinFormHelper.ShowInfo(txtInfo, $"向服务器回复控制命令消息：{ToolHelper.ByteConvertToHex(sendByte)}");
            }
            catch (Exception ex)
            {
                WinFormHelper.ShowInfo(txtInfo, $"回复控制命令消息发送过程出现异常：{ex.Message},关闭连接！");
                EndConn();
            }
        }

        /// <summary>
        /// 钥匙接触电子锁自动上报，0x0702上报日志，和0x0701参数相同，发送的内容也相同
        /// </summary>
        /// <param name="pinNum">操作员ping码</param>
        /// <param name="password">操作员密码</param>
        /// <param name="lockNum">锁号/封铅号</param>
        /// <param name="jzdw">基站定位</param>
        /// <param name="altitude">高度</param>
        /// <param name="speed">速度</param>
        /// <param name="direction">方向</param>
        public static void T808_0x0701_Process(string pinNum, string password, string lockNum, string jzdw, int altitude, int speed, int direction)
        {
            T808_0x0701 body = new T808_0x0701();
            body.setTerminalID(simNum);
            T808_MessageHeader mh = new T808_MessageHeader();
            // 获取终端对应的sim卡号
            mh.setSimNum(simNum);
            var cm = new CommonMessage<T808_0x0701>(body.getMessageID(), mh, body);
            try
            {
                var porto = new T808_0x0701_Process<T808_0x0701>();
                var sendByte = porto.PackData(cm, key, pinNum, password, lockNum, jzdw, altitude, speed, direction);
                clientSocket.Send(sendByte);
                WinFormHelper.ShowInfo(txtInfo, $"钥匙接触电子锁上传消息：{ToolHelper.ByteConvertToHex(sendByte)}");
            }
            catch (Exception ex)
            {
                WinFormHelper.ShowInfo(txtInfo, $"钥匙接触电子锁消息发送过程出现异常：{ex.Message},关闭连接！");
                EndConn();
            }
        }

        /// <summary>
        /// TTS语音播放命令
        /// </summary>
        private static void T808_0x8800AndT808_0x0800_Process()
        {
            T808_0x0800 body = new T808_0x0800();
            body.setTerminalID(simNum);
            T808_MessageHeader mh = new T808_MessageHeader();
            // 获取终端对应的sim卡号
            mh.setSimNum(simNum);
            var cm = new CommonMessage<T808_0x0800>(body.getMessageID(), mh, body);
            try
            {
                var porto = new T808_0x0800_Process<T808_0x0800>();
                var sendByte = porto.PackData(cm, key);
                clientSocket.Send(sendByte);
                WinFormHelper.ShowInfo(txtInfo, $"TTS语音播放应答消息：{ToolHelper.ByteConvertToHex(sendByte)}");
            }
            catch (Exception ex)
            {
                WinFormHelper.ShowInfo(txtInfo, $"TTS语音播放应答消息发送过程出现异常：{ex.Message},关闭连接！");
                EndConn();
            }
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        private static void Receive()
        {
            List<byte> data = new List<byte>();
            byte[] buffer = new byte[1024];
            int length = 0;
            try
            {
                while ((length = clientSocket.Receive(buffer)) > 0)
                {
                    for (int j = 0; j < length; j++)
                    {
                        data.Add(buffer[j]);
                    }
                    if (length < buffer.Length)
                    {
                        break;
                    }
                }
                if (data.Count > 0)
                {
                    var getresult = data.ToArray();
                    var net = new NetStream(getresult);
                    getresult = ProtocolHelper.reverseEscapeData(net.GetBuffer());
                    var header = ProtocolHelper.getHeader(getresult);
                    var body = getresult.Skip(13).Take(header.getBodyLength()).ToArray();
                    switch (header.getMessageID())
                    {
                        case 0x8011:
                            //var date = body.Skip(8).Take(6).ToArray();
                            WinFormHelper.ShowInfo(txtInfo, $"收到服务器8011消息：{ToolHelper.ByteConvertToHex(getresult)},无须回复");
                            // T808_0x8103AndT808_0x0103_Process();
                            break;
                        case 0x8103:
                            WinFormHelper.ShowInfo(txtInfo, $"收到服务器8103消息：{ToolHelper.ByteConvertToHex(getresult)}，回复0103消息");
                            T808_0x8103AndT808_0x0103_Process();
                            break;
                        case 0x8302:
                            WinFormHelper.ShowInfo(txtInfo, $"收到服务器8302消息：{ToolHelper.ByteConvertToHex(getresult)}，无需回复");
                            break;
                        case 0x8300:
                            WinFormHelper.ShowInfo(txtInfo, $"收到服务器8300消息：{ToolHelper.ByteConvertToHex(getresult)}，回复0300消息");
                            T808_0x8300AndT808_0x0300_Process();
                            break;
                        case 0x8600:
                            T808_0x8600AndT808_0x0600_Process();
                            WinFormHelper.ShowInfo(txtInfo, $"收到服务器8600消息：{ToolHelper.ByteConvertToHex(getresult)}，回复0600消息");
                            break;
                        case 0x8700:
                            T808_0x8700AndT808_0x0700_Process();
                            WinFormHelper.ShowInfo(txtInfo, $"收到服务器8700消息：{ToolHelper.ByteConvertToHex(getresult)}，回复0700消息");
                            break;
                        case 0x8702:
                        case 0x8701:
                            WinFormHelper.ShowInfo(txtInfo, $"收到服务器日志上报回复消息：{ToolHelper.ByteConvertToHex(getresult)}，无需回复");
                            break;
                        case 0x8800:
                            T808_0x8800AndT808_0x0800_Process();
                            WinFormHelper.ShowInfo(txtInfo, $"收到服务器8800消息：{ToolHelper.ByteConvertToHex(getresult)}，回复0800消息");
                            break;
                        default:
                            //解析成功，没有处理器处理
                            WinFormHelper.ShowInfo(txtInfo, $"没有找到对应的协议处理器,收到消息：{ToolHelper.ByteConvertToHex(getresult)}");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                WinFormHelper.ShowInfo(txtInfo, $"接收消息处理出现异常：{ex.Message},请查看处理");
            }

            return;
        }

        //private static void SleepSendData(int sec)
        //{
        //    var sleeptime = sec;
        //    var conntime = DateTime.Now;
        //    bool sleep = true;
        //    while (sleep)
        //    {
        //        var nowtime = DateTime.Now - conntime;
        //        if (nowtime.TotalSeconds >= sleeptime)
        //        {
        //            sleep = false;
        //            break;
        //        }
        //        WinFormHelper.ShowInfo(txtInfo, $"连接正在等待{sec}S发送消息，还剩{sleeptime - nowtime.TotalSeconds}s");
        //        Thread.Sleep(5000);
        //    }
        //}
    }

}
