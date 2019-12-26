using SocketTestApp.Protocol.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.MessageBody
{
    public class T808_MessageHeader : CommonMessageHeader
    {
        private int packageNum;
        private int encrypt;
        private int bodyLength;
        private int packageCounts;
        private bool subpackage;
        private int runningNum = 1;
        private string simNum;

        public T808_MessageHeader()
        {
        }

        public int getPackageNum()
        {
            return this.packageNum;
        }

        public void setSubpackage(bool a)
        {
            this.subpackage = a;
        }

        public void setRunningNum(int a)
        {
            this.runningNum = a;
        }

        public void setPackageCounts(int a)
        {
            this.packageCounts = a;
        }

        public bool getSubpackage()
        {
            return this.subpackage;
        }

        public int getPackageCounts()
        {
            return this.packageCounts;
        }

        public string getSimNum()
        {
            return this.simNum;
        }

        public void setBodyLength(int a)
        {
            this.bodyLength = a;
        }

        public int getRunningNum()
        {
            return this.runningNum;
        }

        public int getBodyLength()
        {
            return this.bodyLength;
        }

        public void setSimNum(String a)
        {
            this.simNum = a;
        }

        public void setEncrypt(int a)
        {
            this.encrypt = a;
        }

        public int getEncrypt()
        {
            return this.encrypt;
        }

        public void setPackageNum(int a)
        {
            this.packageNum = a;
        }
    }

}
