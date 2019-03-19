using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace 出窑服务图片识别版.common
{
    public class FileHelper
    {
        public static long GetDirectoryLength(string dirPath)
        {
            //判断给定的路径是否存在,如果不存在则退出
            if (!Directory.Exists(dirPath))
                return 0;
            long len = 0;
            //定义一个DirectoryInfo对象
            DirectoryInfo di = new DirectoryInfo(dirPath);
            //通过GetFiles方法,获取di目录中的所有文件的大小
            foreach (FileInfo fi in di.GetFiles())
            {
                len += fi.Length;
            }
            //获取di中所有的文件夹,并存到一个新的对象数组中,以进行递归
            DirectoryInfo[] dis = di.GetDirectories();
            if (dis.Length > 0)
            {
                for (int i = 0; i < dis.Length; i++)
                {
                    len += GetDirectoryLength(dis[i].FullName);
                }
            }
            return len;
        }

        public static bool isExists(string dirPath) 
        {
            if (!Directory.Exists(dirPath))
            {
                return false;
            }
            else
            {
                return true;
            }
               
        }

    }
}
