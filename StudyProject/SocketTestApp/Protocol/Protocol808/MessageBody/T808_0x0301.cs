using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.MessageBody
{
    public class T808_0x0301 : T808_MessageBody
    {
        private double b;
        private double m;
        private int ALLATORIxDEMO;

        public void setRadius(int a)
        {
            this.ALLATORIxDEMO = a;
        }

        public void setLatitude(double a)
        {
            this.m = a;
        }

        public double getLongitude()
        {
            return this.b;
        }

        public T808_0x0301()
        {
            this.messageID = 769;
        }

        public int getRadius()
        {
            return this.ALLATORIxDEMO;
        }

        public void setLongitude(double a)
        {
            this.b = a;
        }

        public double getLatitude()
        {
            return this.m;
        }
    }
}
