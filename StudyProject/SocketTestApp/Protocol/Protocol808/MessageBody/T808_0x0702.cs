using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.MessageBody
{
    public class T808_0x0702 : T808_MessageBody
    {
        private int s;
        private int o;
        private float d;
        private int h;
        private string i;
        private int I;
        private string c;
        private int e;
        private int L;
        private string M;
        private int l;
        private double j;
        private int g;
        private int D;
        private int A;
        private int f;
        private string K;
        private int G;
        private int E;
        private DateTime J;
        private string a;
        private int k;
        private double H;
        private int B;
        private double C;
        private int F;
        private double b;
        private int m;
        private double ALLATORIxDEMO;

        public void setSign4(int a)
        {
            this.A = a;
        }

        public void setSign1(int a)
        {
            this.f = a;
        }

        public int getSign12()
        {
            return this.L;
        }

        public int getSign11()
        {
            return this.I;
        }

        public string getJzdw()
        {
            return this.M;
        }

        public int getSign13()
        {
            return this.g;
        }

        public double getAltitude()
        {
            return this.b;
        }

        public void setSign15(int a)
        {
            this.F = a;
        }

        public void setSign3(int a)
        {
            this.o = a;
        }

        public void setSpeed(float a)
        {
            this.d = a;
        }

        public T808_0x0702()
        {
            this.messageID = 1794;
        }

        public string getVersion()
        {
            return this.K;
        }

        public int getSign10()
        {
            return this.G;
        }

        public int getSign1()
        {
            return this.f;
        }

        public void setPin(string a)
        {
            this.c = a;
        }

        public int getSign4()
        {
            return this.A;
        }

        public double getLatitude()
        {
            return this.H;
        }

        public void setSign2(int a)
        {
            this.h = a;
        }

        public DateTime getTime()
        {
            return this.J;
        }

        public void setSign8(int a)
        {
            this.e = a;
        }

        public int getSign15()
        {
            return this.F;
        }

        public string getLockNo()
        {
            return this.i;
        }

        public void setSign0(int a)
        {
            this.s = a;
        }

        public void setLongitude_o(double a)
        {
            this.C = a;
        }

        public void setSign13(int a)
        {
            this.g = a;
        }

        public string getPin()
        {
            return this.c;
        }

        public void setPwd(string a)
        {
            this.a = a;
        }

        public void setSign5(int a)
        {
            this.E = a;
        }

        public void setDirection(int a)
        {
            this.D = a;
        }

        public void setTime(DateTime a)
        {
            this.J = a;
        }

        public int getSign0()
        {
            return this.s;
        }

        public int getSign2()
        {
            return this.h;
        }

        public int getSign7()
        {
            return this.l;
        }

        public int getDirection()
        {
            return this.D;
        }

        public void setVersion(string a)
        {
            this.K = a;
        }

        public void setLatitude_o(double a)
        {
            this.j = a;
        }

        public void setJzdw(string a)
        {
            this.M = a;
        }

        public void setSign11(int a)
        {
            this.I = a;
        }

        public void setSign14(int a)
        {
            this.k = a;
        }

        public void setLongitude(double a)
        {
            this.ALLATORIxDEMO = a;
        }

        public string getPwd()
        {
            return this.a;
        }

        public void setSign10(int a)
        {
            this.G = a;
        }

        public void setAltitude(double a)
        {
            this.b = a;
        }

        public double getLongitude()
        {
            return this.ALLATORIxDEMO;
        }

        public float getSpeed()
        {
            return this.d;
        }

        public void setLockNo(string a)
        {
            this.i = a;
        }

        public int getSign5()
        {
            return this.E;
        }

        public int getSign6()
        {
            return this.B;
        }

        public double getLongitude_o()
        {
            return this.C;
        }

        public void setSign9(int a)
        {
            this.m = a;
        }

        public int getSign14()
        {
            return this.k;
        }

        public int getSign3()
        {
            return this.o;
        }

        public int getSign8()
        {
            return this.e;
        }

        public void setLatitude(double a)
        {
            this.H = a;
        }

        public double getLatitude_o()
        {
            return this.j;
        }

        public void setSign7(int a)
        {
            this.l = a;
        }

        public int getSign9()
        {
            return this.m;
        }

        public void setSign12(int a)
        {
            this.L = a;
        }

        public void setSign6(int a)
        {
            this.B = a;
        }
    }

}
