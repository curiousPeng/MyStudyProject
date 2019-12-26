using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.MessageBody
{
    public class T808_0x0302 : T808_MessageBody
    {
        private double G;
        private double E;
        private double J;
        private String a;
        private float k;
        private double H;
        private double B;
        private String C;
        private DateTime F;
        private String b;
        private String m;
        private int ALLATORIxDEMO;

        public void setLatitude_o(double a)
        {
            this.H = a;
        }

        public double getAltitude()
        {
            return this.B;
        }

        public String getJzdw()
        {
            return this.b;
        }

        public void setLatitude(double a)
        {
            this.E = a;
        }

        public void setTime(DateTime a)
        {
            this.F = a;
        }

        public String getVersion()
        {
            return this.C;
        }

        public void setPwd(String a)
        {
            this.a = a;
        }

        public float getSpeed()
        {
            return this.k;
        }

        public double getLongitude()
        {
            return this.J;
        }

        public void setJzdw(String a)
        {
            this.b = a;
        }

        public void setSpeed(float a)
        {
            this.k = a;
        }

        public double getLongitude_o()
        {
            return this.G;
        }

        public String getPin()
        {
            return this.m;
        }

        public double getLatitude()
        {
            return this.E;
        }

        public T808_0x0302()
        {
            this.messageID = 770;
        }

        public DateTime getTime()
        {
            return this.F;
        }

        public void setAltitude(double a)
        {
            this.B = a;
        }

        public void setLongitude_o(double a)
        {
            this.G = a;
        }

        public int getDirection()
        {
            return this.ALLATORIxDEMO;
        }

        public void setPin(String a)
        {
            this.m = a;
        }

        public void setDirection(int a)
        {
            this.ALLATORIxDEMO = a;
        }

        public void setLongitude(double a)
        {
            this.J = a;
        }

        public String getPwd()
        {
            return this.a;
        }

        public double getLatitude_o()
        {
            return this.H;
        }

        public void setVersion(String a)
        {
            this.C = a;
        }
    }
}
