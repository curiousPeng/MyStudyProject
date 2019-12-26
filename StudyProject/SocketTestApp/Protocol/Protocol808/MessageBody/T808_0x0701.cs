using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTestApp.Protocol.Protocol808.MessageBody
{
    public class T808_0x0701 : T808_MessageBody
    {
        private string s;
        private int o;
        private int d;
        private double h;
        private int i;
        private int I;
        private double c;
        private string e;
        private float L;
        private int M;
        private int l;
        private DateTime j;
        private double g;
        private int D;
        private int A;
        private string f;
        private string K;
        private double G;
        private int E;
        private int J;
        private int a;
        private int k;
        private double H;
        private int B;
        private string C;
        private int F;
        private int b;
        private int m;
        private int ALLATORIxDEMO;

        public int getDirection()
        {
            return this.d;
        }

        public double getLongitude_o()
        {
            return this.h;
        }

        public void setSign4(int a)
        {
            this.ALLATORIxDEMO = a;
        }

        public int getSign6()
        {
            return this.i;
        }

        public void setLongitude(double a)
        {
            this.G = a;
        }

        public void setSign3(int a)
        {
            this.a = a;
        }

        public void setLongitude_o(double a)
        {
            this.h = a;
        }

        public void setTime(DateTime a)
        {
            this.j = a;
        }

        public int getSign5()
        {
            return this.k;
        }

        public void setLatitude_o(double a)
        {
            this.g = a;
        }

        public float getSpeed()
        {
            return this.L;
        }

        public void setLockNo(string a)
        {
            this.e = a;
        }

        public void setSign9(int a)
        {
            this.l = a;
        }

        public string getPwd()
        {
            return this.C;
        }

        public int getSign14()
        {
            return this.F;
        }

        public void setJzdw(string a)
        {
            this.K = a;
        }

        public int getSign13()
        {
            return this.J;
        }

        public string getVersion()
        {
            return this.s;
        }

        public void setSign2(int a)
        {
            this.o = a;
        }

        public void setSign1(int a)
        {
            this.I = a;
        }

        public void setPwd(string a)
        {
            this.C = a;
        }

        public void setSign13(int a)
        {
            this.J = a;
        }

        public int getSign4()
        {
            return this.ALLATORIxDEMO;
        }

        public double getAltitude()
        {
            return this.c;
        }

        public int getSign7()
        {
            return this.D;
        }

        public int getSign10()
        {
            return this.E;
        }

        public int getSign3()
        {
            return this.a;
        }

        public void setSign10(int a)
        {
            this.E = a;
        }

        public int getSign2()
        {
            return this.o;
        }

        public void setSign8(int a)
        {
            this.B = a;
        }

        public void setSign12(int a)
        {
            this.A = a;
        }

        public int getSign12()
        {
            return this.A;
        }

        public void setSign0(int a)
        {
            this.M = a;
        }

        public double getLongitude()
        {
            return this.G;
        }

        public T808_0x0701()
        {
            this.messageID = 1793;
        }

        public DateTime getTime()
        {
            return this.j;
        }

        public void setVersion(string a)
        {
            this.s = a;
        }

        public string getLockNo()
        {
            return this.e;
        }

        public void setLatitude(double a)
        {
            this.H = a;
        }

        public int getSign0()
        {
            return this.M;
        }

        public int getSign9()
        {
            return this.l;
        }

        public void setSign11(int a)
        {
            this.m = a;
        }

        public void setSign15(int a)
        {
            this.b = a;
        }

        public double getLatitude_o()
        {
            return this.g;
        }

        public double getLatitude()
        {
            return this.H;
        }

        public void setSign6(int a)
        {
            this.i = a;
        }

        public int getSign1()
        {
            return this.I;
        }

        public string getJzdw()
        {
            return this.K;
        }

        public void setSign14(int a)
        {
            this.F = a;
        }

        public int getSign11()
        {
            return this.m;
        }

        public int getSign15()
        {
            return this.b;
        }

        public string getPin()
        {
            return this.f;
        }

        public void setSpeed(float a)
        {
            this.L = a;
        }

        public int getSign8()
        {
            return this.B;
        }

        public void setSign7(int a)
        {
            this.D = a;
        }

        public void setAltitude(double a)
        {
            this.c = a;
        }

        public void setPin(string a)
        {
            this.f = a;
        }

        public void setSign5(int a)
        {
            this.k = a;
        }

        public void setDirection(int a)
        {
            this.d = a;
        }
    }
}
