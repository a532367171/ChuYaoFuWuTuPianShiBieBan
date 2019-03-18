using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 出窑工位采集服务
{
   public class 重启类
    {
        string path;
        public 重启类(string str)
        {
            path = str;
        }

        public void 重启(string str)
        {
            System.Diagnostics.Process.Start(path, "5");
        }


    }
}
