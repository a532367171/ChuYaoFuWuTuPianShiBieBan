using System.Collections;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace 出窑服务图片识别版.common
{
    public class IniFileReference
    {
        //INI文件名  
        private string path;

        //声明读写INI文件的API函数  
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key,
                                                             string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def,
                                                          StringBuilder retVal, int size, string filePath);

        //类的构造函数，传递INI文件名  
        public IniFileReference(string INIPath)
        {
            path = INIPath;
        }

        //写INI文件  
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        //读取INI文件指定  
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
            return temp.ToString();
        }

        /// <summary>
        /// 验证文件是否存在
        /// </summary>
        /// <returns>布尔值</returns>
        public bool ExistIniFile()
        {
            return File.Exists(path);
        }
    }
}
