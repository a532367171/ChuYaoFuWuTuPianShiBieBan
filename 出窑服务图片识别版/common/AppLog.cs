using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace 出窑服务图片识别版.common
{
    public class AppLog
    {
        public static void Write(string content)
        {

            string text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
            bool flag = !Directory.Exists(text);
            if (flag)
            {
                Directory.CreateDirectory(text);
            }
            StreamWriter streamWriter = new StreamWriter(Path.Combine(text, DateTime.Now.ToString("yyyyMMdd") + ".log"), true, Encoding.UTF8);
            try
            {
                streamWriter.WriteLine(DateTime.Now.ToString() + "\t" + content);
                streamWriter.WriteLine("------------------------------------------------------------------------------");
            }
            catch (Exception ex)
            {
                #region 保旧值
                IniFileReference _iniFile = new IniFileReference(AppDomain.CurrentDomain.BaseDirectory + "写入错误.ini");
                _iniFile.IniWriteValue("SYSDNSection", "错误代码", Convert.ToString(content + ex.Message));
                _iniFile = null;
                #endregion
            }
            finally
            {
                streamWriter.Close();
            }
        }
        public static void WriteErr(string content)
        {

            string text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
            bool flag = !Directory.Exists(text);
            if (flag)
            {
                Directory.CreateDirectory(text);
            }
            StreamWriter streamWriter = new StreamWriter(Path.Combine(text, DateTime.Now.ToString("yyyyMMdd") + "Err.log"), true, Encoding.UTF8);
            try
            {
                streamWriter.WriteLine(DateTime.Now.ToString() + "\t" + content);
                streamWriter.WriteLine("------------------------------------------------------------------------------");
            }
            catch (Exception ex)
            {
                #region 保旧值
                IniFileReference _iniFile = new IniFileReference(AppDomain.CurrentDomain.BaseDirectory + "写入错误.ini");
                _iniFile.IniWriteValue("SYSDNSection", "错误代码", Convert.ToString(content + ex.Message));
                _iniFile = null;
                #endregion
            }
            finally
            {
                streamWriter.Close();
            }
        }

    }
}
