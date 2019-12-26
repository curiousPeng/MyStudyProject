using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.DesignPattern.Factory
{
    /// <summary>
    /// 工厂模式
    /// </summary>
    public class FactoryADemo : IDemo
    {
        public int A()
        {
            throw new NotImplementedException();
        }
    }
    public class FactoryBDemo : IDemo
    {
        public int A()
        {
            throw new NotImplementedException();
        }
    }
    public class FactoryCDemo
    {
        public IDemo Demo()
        {
            return new FactoryADemo();
        }
    }
}
