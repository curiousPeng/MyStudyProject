using SocketTestApp.Protocol.Protocol808.MessageBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.Process
{
    /**
  * 终端信息
  * @author 仁德
  */
    public class T808Terminal
    {

        /** 终端设备编号  全系统唯一*/
        private string terminalNum;

        private string simNum;

        /** 终端是否注册 */
        private bool isRegistered = false;

        /** 是否开启基站定位 */
        private bool isJzdw = false;

        /** 企业号 */
        private long companyInfoId = 0L;

        /** 企业是否开启任务单模式  */
        private bool isOpenTaskMode = false;

        /** 是否手动开锁 */
        private bool isManualUnlock = false;

        /** 是否手动解除报警 */
        private bool isManualRemoveWarn = false;

        /** 移动设备国家地区代码MCC */
        private int mcc = 460;

        /** 移动设备网络代码MNC */
        private int mnc = 0;
        
        private string uuid;

        private T808_MessageHeader header;

        private T808_MessageBody messagebody;

        private int messageId = 0;

        private string warning = "";
     
        public string getTerminalNum()
        {
            return terminalNum;
        }

        public void setTerminalNum(string terminalNum)
        {
            this.terminalNum = terminalNum;
        }

        public string getSimNum()
        {
            return simNum;
        }

        public void setSimNum(string simNum)
        {
            this.simNum = simNum;
        }

        public bool isRegister()
        {
            return isRegistered;
        }

        public void setRegistered(bool isRegistered)
        {
            this.isRegistered = isRegistered;
        }

        public bool isDw()
        {
            return isJzdw;
        }

        public void setJzdw(bool isJzdw)
        {
            this.isJzdw = isJzdw;
        }

        public long getCompanyInfoId()
        {
            return companyInfoId;
        }

        public void setCompanyInfoId(long companyInfoId)
        {
            this.companyInfoId = companyInfoId;
        }

        public bool isOpenTM()
        {
            return isOpenTaskMode;
        }

        public void setOpenTaskMode(bool isOpenTaskMode)
        {
            this.isOpenTaskMode = isOpenTaskMode;
        }

        public bool isSDUnlock()
        {
            return isManualUnlock;
        }

        public void setManualUnlock(bool isManualUnlock)
        {
            this.isManualUnlock = isManualUnlock;
        }

        public bool isSDRemoveWarn()
        {
            return isManualRemoveWarn;
        }

        public void setManualRemoveWarn(bool isManualRemoveWarn)
        {
            this.isManualRemoveWarn = isManualRemoveWarn;
        }

        public int getMcc()
        {
            return mcc;
        }

        public void setMcc(int mcc)
        {
            this.mcc = mcc;
        }

        public int getMnc()
        {
            return mnc;
        }

        public void setMnc(int mnc)
        {
            this.mnc = mnc;
        }
        
        public string getUuid()
        {
            return uuid;
        }

        public void setUuid(string uuid)
        {
            this.uuid = uuid;
        }

        public T808_MessageHeader getHeader()
        {
            return header;
        }

        public void setHeader(T808_MessageHeader header)
        {
            this.header = header;
        }

        public T808_MessageBody getMessagebody()
        {
            return messagebody;
        }

        public void setMessagebody(T808_MessageBody messagebody)
        {
            this.messagebody = messagebody;
        }

        public int getMessageId()
        {
            return messageId;
        }

        public void setMessageId(int messageId)
        {
            this.messageId = messageId;
        }

        public string getWarning()
        {
            return warning;
        }

        public void setWarning(string warning)
        {
            this.warning = warning;
        }

    }

}
