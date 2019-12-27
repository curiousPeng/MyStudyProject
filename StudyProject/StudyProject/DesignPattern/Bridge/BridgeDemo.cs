using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.DesignPattern.Bridge
{
    /// <summary>
    /// 桥接模式
    /// </summary>
    class BridgeDemo
    {
    }
    public class WindowsImplementor : ImageImplementor
    {
        public void DoPaint(Matrix m)
        {
            // 调用Windows的绘制函数绘制像素矩阵
            Console.WriteLine("在Windows系统中显示图像");
        }
    }

    public class LinuxImplementor : ImageImplementor
    {
        public void DoPaint(Matrix m)
        {
            // 调用Linux的绘制函数绘制像素矩阵
            Console.WriteLine("在Linux系统中显示图像");
        }
    }

    public class UnixImplementor : ImageImplementor
    {
        public void DoPaint(Matrix m)
        {
            // 调用Unix的绘制函数绘制像素矩阵
            Console.WriteLine("在Unix系统中显示图像");
        }
    }
    public class Matrix
    {
        // 此处代码省略
    }
    /// <summary>
    /// 抽象图像类：抽象类
    /// </summary>
    public abstract class Image
    {
        protected ImageImplementor imageImpl;

        public void SetImageImplementor(ImageImplementor imageImpl)
        {
            this.imageImpl = imageImpl;
        }

        public abstract void ParstFile(string fileName);
    }
    public class JPGImage : Image
    {
        public override void ParstFile(string fileName)
        {
            // 模拟解析JPG文件并获得一个像素矩阵对象m
            Matrix m = new Matrix();
            imageImpl.DoPaint(m);
            Console.WriteLine("{0} : 格式为JPG", fileName);
        }
    }

    public class BMPImage : Image
    {
        public override void ParstFile(string fileName)
        {
            // 模拟解析BMP文件并获得一个像素矩阵对象m
            Matrix m = new Matrix();
            imageImpl.DoPaint(m);
            Console.WriteLine("{0} : 格式为BMP", fileName);
        }
    }

    public class GIFImage : Image
    {
        public override void ParstFile(string fileName)
        {
            // 模拟解析GIF文件并获得一个像素矩阵对象m
            Matrix m = new Matrix();
            imageImpl.DoPaint(m);
            Console.WriteLine("{0} : 格式为GIF", fileName);
        }
    }
    /// <summary>
    /// 抽象操作系统实现类：实现类接口
    /// </summary>
    public interface ImageImplementor
    {
        // 显示像素矩阵
        void DoPaint(Matrix m);
    }
}
