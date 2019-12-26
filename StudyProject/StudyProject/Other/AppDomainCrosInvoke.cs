using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Runtime.Remoting;

namespace StudyProject.Other
{
    public class AppDomainCrosInvoke
    {
        private static void Marshalling()
        {   //获取当前线程的AppDomain.
            AppDomain adCallingThreadDomain = Thread.GetDomain();

            //每个AppDomain都分配了友好字符串名称（以便调试）
            //获取这个AppDomin的友好字符串名称并显示它
            String callingDomainName = adCallingThreadDomain.FriendlyName;
            Console.WriteLine("默认AppDomain的友好名称={0}", callingDomainName);

            //获取并显示我们的AppDomain中包含了“Main”方法的程序集
            String exeAssembly = Assembly.GetEntryAssembly().FullName;
            Console.WriteLine("Main Assembly={0}", exeAssembly);

            //定义局部变量来引用一个AppDomain;
            AppDomain ad2 = null;

            //Demo 1: 使用Marshal-by-Referrence 进行跨AppDomain通信
            Console.WriteLine("{0}Demo #1", Environment.NewLine);

            //新建一个AppDomain(从当前AppDomain继承安全性和配置)
            ad2 = AppDomain.CreateDomain("AD #2");
            MarshalByRefType mbrt = null;

            //将我们的程序集加载到新AppDomain中，构造一个对象,把它封送回我们的AppDomain(实际得到一个代理的引用)
            mbrt = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(exeAssembly, "MarshalByRefType");
            Console.WriteLine("Type={0}", mbrt.GetType());//CLR在类型上撒谎了

            //证明得到的是一个对代理对象的引用
           // Console.WriteLine("Is proxy={0}", System.Runtime.Remoting.ObjectHandle.RemotingServices.IsTransparentPrixy(mbrt))
        }
    }

    public sealed class MarshalByRefType : MarshalByRefObject
    {
        public MarshalByRefType()
        {
            Console.WriteLine("{0} ctor running in {1}", this.GetType().ToString(), Thread.GetDomain().FriendlyName);
        }

        public void SomeMethod()
        {
            Console.WriteLine("Executing in " + Thread.GetDomain().FriendlyName);
        }

        public MarshalByValType MethodWithReturn()
        {
            Console.WriteLine("Executing in" + Thread.GetDomain().FriendlyName);
            MarshalByValType t = new MarshalByValType();
            return t;
        }

        public NonMarshalableType MethodArgAndReturn(String callingDomainName)
        {
            Console.WriteLine("Calling from '{0}' to '{1}'.", callingDomainName, Thread.GetDomain().FriendlyName);
            NonMarshalableType t = new NonMarshalableType();
            return t;
        }
    }

    [Serializable]
    public sealed class MarshalByValType : Object
    {
        private DateTime m_creationTime = DateTime.Now;//DateTime可序列化

        public MarshalByValType()
        {
            Console.WriteLine("{0} ctor running in {1}，Created on {2:D}", this.GetType().ToString(), Thread.GetDomain().FriendlyName, m_creationTime);
        }
        public override string ToString()
        {
            return m_creationTime.ToLongDateString();
        }
    }
    public sealed class NonMarshalableType : Object
    {
        public NonMarshalableType()
        {
            Console.WriteLine("Executing in" + Thread.GetDomain().FriendlyName);
        }
    }
}
