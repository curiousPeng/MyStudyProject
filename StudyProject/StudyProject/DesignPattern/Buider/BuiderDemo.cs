using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.DesignPattern.Buider
{
    /// <summary>
    /// 建造者模式
    /// </summary>
    public class BuiderDemo
    {
        private Builder builder;

        public BuiderDemo(Builder builder)
        {
            this.builder = builder;
        }

        public void SetBuilder(Builder builder)
        {
            this.builder = builder;
        }

        // 产品构建与组装方法
        public Product Construct()
        {
            builder.BuildPartA();
            builder.BuildPartB();
            builder.BuildPartC();

            return builder.GetResult();
        }
    }
    public class Product
    {
        public string PartA { get; set; }
        public string PartB { get; set; }
        public string PartC { get; set; }
    }
    public abstract class Builder
    {
        // 创建产品对象
        protected Product product = new Product();

        public abstract void BuildPartA();
        public abstract void BuildPartB();
        public abstract void BuildPartC();

        // 返回产品对象
        public Product GetResult()
        {
            return product;
        }
    }
}
