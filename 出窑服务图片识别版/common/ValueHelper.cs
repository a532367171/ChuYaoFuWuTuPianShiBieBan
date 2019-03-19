using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 出窑服务图片识别版.common
{
    public class ValueHelper
    {
        #region Factory
        public static ValueHelper _Instance = null;
        internal static ValueHelper Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ValueHelper();
                }
                return _Instance;
            }
        }
        #endregion
     

        public virtual byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        public virtual string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();

        }
    }
}
