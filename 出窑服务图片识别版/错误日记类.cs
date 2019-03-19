using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using 出窑服务图片识别版.common;

namespace 出窑服务图片识别版
{
    public class 错误日记类
    {
        public void 错误日记执行函数(string str, Exception ex)
        {
            try
            {
                LogManager.LogInfo(str, ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
