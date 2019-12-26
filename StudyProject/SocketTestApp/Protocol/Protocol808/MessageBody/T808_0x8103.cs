using System.Collections.Generic;

namespace SocketTestApp.Protocol.Protocol808.MessageBody
{
    public class T808_0x8103 : T808_MessageBody
    {
    public const int CMD_0x0017 = 23;
    private Dictionary<int, string> ALLATORIxDEMO = new Dictionary<int, string>();
    public const int CMD_0x0018 = 24;
    public const int CMD_0x0004 = 4;
    public const int CMD_0x0019 = 25;
    public const int CMD_0x0021 = 33;
    public const int CMD_0x0001 = 1;
    public const int CMD_0x0002 = 2;
    public const int CMD_0x0003 = 3;
    public const int CMD_0x0005 = 5;

    //public string getDesc(int a, string a)
    //{
    //        StringBuilder var3 = new StringBuilder();
    //        StringBuilder var10000;
    //        if (a == 1)
    //        {
    //            var3.append(NoTerminalException.ALLATORIxDEMO("a纼竕德跉厥逻門隮N"));
    //            var10000 = var3;
    //            var3.append(a).append(ProtocolException.ALLATORIxDEMO("秚\u00103"));
    //        }
    //        else if (a == 2)
    //        {
    //            var3.append(NoTerminalException.ALLATORIxDEMO("/n7j淼恕廠筮跱旌斂闎N"));
    //            var10000 = var3;
    //            var3.append(a).append(ProtocolException.ALLATORIxDEMO("秚\u00103"));
    //        }
    //        else if (a == 3)
    //        {
    //            var3.append(NoTerminalException.ALLATORIxDEMO("/n7j淼恕醹会歕敊N"));
    //            var10000 = var3;
    //            var3.append(a).append(ProtocolException.ALLATORIxDEMO("\u00103"));
    //        }
    //        else if (a == 4)
    //        {
    //            var3.append(NoTerminalException.ALLATORIxDEMO("/o0j淼恕廠筮跱旌斂闎N"));
    //            var10000 = var3;
    //            var3.append(a).append(ProtocolException.ALLATORIxDEMO("秚\u00103"));
    //        }
    //        else if (a == 5)
    //        {
    //            var3.append(NoTerminalException.ALLATORIxDEMO("/o0j淼恕醹会歕敊N"));
    //            var10000 = var3;
    //            var3.append(a).append(ProtocolException.ALLATORIxDEMO("\u00103"));
    //        }
    //        else if (a == 23)
    //        {
    //            var3.append(NoTerminalException.ALLATORIxDEMO("a杹力嘜s$戬垫吷N"));
    //            var10000 = var3;
    //            var3.append(a).append(ProtocolException.ALLATORIxDEMO("\u00103"));
    //        }
    //        else if (a == 24)
    //        {
    //            var3.append(NoTerminalException.ALLATORIxDEMO("a杹力嘜n7j窛叙N"));
    //            var10000 = var3;
    //            var3.append(a).append(ProtocolException.ALLATORIxDEMO("\u00103"));
    //        }
    //        else if (a == 25)
    //        {
    //            var3.append(NoTerminalException.ALLATORIxDEMO("a杹力嘜o0j窛叙N"));
    //            var10000 = var3;
    //            var3.append(a).append(ProtocolException.ALLATORIxDEMO("\u00103"));
    //        }
    //        else if (a == 33)
    //        {
    //            var3.append(NoTerminalException.ALLATORIxDEMO("/讄缚}$i寮佷杽敲斂镅N"));
    //            var10000 = var3;
    //            var3.append(a).append(ProtocolException.ALLATORIxDEMO("秚\u00103"));
    //        }
    //        else
    //        {
    //            var10000 = var3;
    //            var3.append(NoTerminalException.ALLATORIxDEMO("/朐瞑液怛gO"));
    //        }

    //        return var10000.tostring();
    //    }

    public T808_0x8103()
    {
        this.messageID = 33027;
    }

    //public string formatstring(string a)
    //{
    //    StringBuilder var2 = new StringBuilder();
    //    string[] var3;
    //    if ((var3 = this.split(ProtocolException.ALLATORIxDEMO("\u0011&"))).length != 4)
    //    {
    //            throw new Exception("未知错误");
    //    }
    //    else
    //    {
    //        string[] var5 = var3;
    //        int var4 = var3.Length;

    //        int var6;
    //        for (int var10000 = var6 = 0; var10000 < var4; var10000 = var6)
    //        {
    //            a = var5[var6];
    //            string var10001 = ProtocolException.ALLATORIxDEMO("h8~l");
    //            Object[] var10002 = new Object[1];
    //            bool var10004 = true;
    //            var10002[0] = Integer.parseInt(a);
    //            StringBuilder var7 = var2.append(string.format(var10001, var10002));
    //            ++var6;
    //            var7.append(NoTerminalException.ALLATORIxDEMO("Z"));
    //        }

    //        var2.delete(var2.length() - 1, var2.length());
    //        return var2.tostring();
    //    }
    //}

    //public void addCmd(int a, string a)
    //{
    //    if (a != null && this.trim().length() != 0)
    //    {
    //        if (a == 23)
    //        {
    //            a = this.formatstring(a);
    //        }

    //        this.ALLATORIxDEMO.put(a, a);
    //    }
    //    else
    //    {
    //        throw new ProtocolPackException(ProtocolException.ALLATORIxDEMO("\u001bi!}((${mm x9ql(&m42") + a);
    //    }
    //}

    public Dictionary<int, string> getParamList()
    {
        return this.ALLATORIxDEMO;
    }
}
}
