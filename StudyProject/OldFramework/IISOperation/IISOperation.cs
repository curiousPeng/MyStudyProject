using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace StudyProject.IISOperation
{
    public class IISOperation
    {
        private static Dictionary<string, string> site_list;

        static IISOperation()
        {
            string config = ConfigurationManager.AppSettings["sites"];
            if (string.IsNullOrWhiteSpace(config))
            {
                throw new Exception("未在AppConfig中发现任何站点配置信息");
            }
            Console.WriteLine("本次更新将会更新如下站点：");
            site_list = config.Split(',').ToDictionary(delegate (string p)
            {
                string text = p.Split('@')[0].ToLower();
                Console.WriteLine(text);
                return text;
            }, (string p) => p.Split('@')[1]);
            Console.WriteLine();
        }

        public static void Publish()
        {
            Console.WriteLine("停止IIS服务器");
            StopIIS();
            Console.WriteLine("按任意键开始后续更新动作");
            Console.Read();
            ServerManager iisManager = new ServerManager();
            SiteCollection sites = iisManager.Sites;
            foreach (Site site2 in sites)
            {
                string key2 = site2.Name.ToLower();
                if (site_list.ContainsKey(key2))
                {
                    try
                    {
                        CopyFiles(site_list[key2], site2.Applications["/"].VirtualDirectories["/"].PhysicalPath);
                        Console.WriteLine("站点[" + site2.Name + "]拷贝文件成功");
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine("站点[" + site2.Name + "]更新文件失败，异常信息：\n" + ex2.Message);
                    }
                }
            }
            StartIIS();
            Thread.Sleep(10000);
            foreach (Site site in sites)
            {
                string key = site.Name.ToLower();
                if (site_list.ContainsKey(key))
                {
                    try
                    {
                        site.Start();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("站点[" + site.Name + "]启动失败，异常信息：\n" + ex.Message);
                    }
                    Console.WriteLine("站点[" + site.Name + "]更新完毕");
                }
            }
            Console.WriteLine("恢复IIS服务器");
            Console.Read();
        }

        private static void StopIIS()
        {
            Process iisReset = new Process();
            iisReset.StartInfo.FileName = "cmd.exe";
            iisReset.StartInfo.UseShellExecute = true;
            iisReset.StartInfo.Arguments = "/C C:\\Windows\\System32\\iisreset.exe /stop";
            iisReset.Start();
            iisReset.WaitForExit();
        }

        private static void StartIIS()
        {
            Process iisReset = new Process();
            iisReset.StartInfo.FileName = "cmd.exe";
            iisReset.StartInfo.UseShellExecute = true;
            iisReset.StartInfo.Arguments = "/C C:\\Windows\\System32\\iisreset.exe /start";
            iisReset.Start();
            iisReset.WaitForExit();
        }

        private static void CopyFiles(string from_path, string to_path)
        {
            if (!Directory.Exists(to_path))
            {
                throw new Exception("路径" + to_path + "不存在");
            }
            string fileName = string.Empty;
            string destPath = string.Empty;
            string[] files = Directory.GetFiles(from_path, "*.*", SearchOption.AllDirectories);
            string[] array = files;
            foreach (string file in array)
            {
                fileName = Path.GetFileName(file);
                string temp = file.Substring(from_path.Length + 1);
                temp = temp.Substring(0, temp.Length - fileName.Length);
                destPath = Path.Combine(to_path, temp);
                if (!Directory.Exists(destPath))
                {
                    Directory.CreateDirectory(destPath);
                }
                File.Copy(file, Path.Combine(destPath, fileName), overwrite: true);
            }
        }
    }
}

