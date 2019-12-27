using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.DesignPattern.Composite
{
    /// <summary>
    /// 组合模式
    /// </summary>
    class CompositeDemo : AbstractFile
    {
        private IList<AbstractFile> fileList = new List<AbstractFile>();
        private string name;
        public CompositeDemo(string name)
        {
            this.name = name;
        }
        public override void Add(AbstractFile file)
        {
            fileList.Add(file);
        }
        public override void Remove(AbstractFile file)
        {
            fileList.Remove(file);
        }
        public override AbstractFile GetChild(int index)
        {
            return fileList[index];
        }
        public override void KillVirus()
        {            // 此处模拟杀毒操作
            Console.WriteLine("---- 对文件夹‘{0}’进行杀毒", name); foreach (var item in fileList)
            {
                item.KillVirus();
            }
        }
    }
    /// <summary>
    ///  抽象文件类：抽象构件    
    /// </summary>
    public abstract class AbstractFile
    {
        public abstract void Add(AbstractFile file);
        public abstract void Remove(AbstractFile file);
        public abstract AbstractFile GetChild(int index);
        public abstract void KillVirus();
    }
    /// <summary>
    /// 叶子构件：图像文件、文本文件 和 视频文件    /// </summary>
    public class ImageFile : AbstractFile
    {
        private string name; public ImageFile(string name)
        {
            this.name = name;
        }
        public override void Add(AbstractFile file)
        {
            Console.WriteLine("对不起，系统不支持该方法！");
        }
        public override void Remove(AbstractFile file)
        {
            Console.WriteLine("对不起，系统不支持该方法！");
        }
        public override AbstractFile GetChild(int index)
        {
            Console.WriteLine("对不起，系统不支持该方法！"); return null;
        }
        public override void KillVirus()
        {            // 此处模拟杀毒操作
            Console.WriteLine("**** 对图像文件‘{0}’进行杀毒", name);
        }
    }
    public class TextFile : AbstractFile
    {
        private string name; public TextFile(string name)
        {
            this.name = name;
        }
        public override void Add(AbstractFile file)
        {
            Console.WriteLine("对不起，系统不支持该方法！");
        }
        public override void Remove(AbstractFile file)
        {
            Console.WriteLine("对不起，系统不支持该方法！");
        }
        public override AbstractFile GetChild(int index)
        {
            Console.WriteLine("对不起，系统不支持该方法！"); return null;
        }
        public override void KillVirus()
        {            // 此处模拟杀毒操作
            Console.WriteLine("**** 对文本文件‘{0}’进行杀毒", name);
        }
    }
    public class VideoFile : AbstractFile
    {
        private string name; public VideoFile(string name)
        {
            this.name = name;
        }
        public override void Add(AbstractFile file)
        {
            Console.WriteLine("对不起，系统不支持该方法！");
        }
        public override void Remove(AbstractFile file)
        {
            Console.WriteLine("对不起，系统不支持该方法！");
        }
        public override AbstractFile GetChild(int index)
        {
            Console.WriteLine("对不起，系统不支持该方法！"); return null;
        }
        public override void KillVirus()
        {            // 此处模拟杀毒操作
            Console.WriteLine("**** 对视频文件‘{0}’进行杀毒", name);
        }
    }
}
