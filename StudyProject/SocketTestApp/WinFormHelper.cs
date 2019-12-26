using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketTestApp
{
   public static class WinFormHelper
    {

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="txtInfo"></param>
        /// <param name="Info"></param>
        public static void ShowInfo(System.Windows.Forms.TextBox txtInfo, string Info)
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string info = string.Empty;
            info = time + " :" + Info;

            txtInfo.BeginInvoke((MethodInvoker)delegate () {
                txtInfo.AppendText(info);
                txtInfo.AppendText(Environment.NewLine);
                txtInfo.ScrollToCaret();
            });
        }
    }
}
