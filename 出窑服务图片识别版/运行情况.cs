using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using 出窑服务图片识别版.common;

namespace 出窑服务图片识别版
{
    public class 运行情况
    {
        string sql_软件运行情况 = "insert into service_Start_or_Stop (Date,Class_name,Message,cManufacture) values('{0}','{1}','{2}','{3}')";

        StringBuilder sb_sql_软件运行情况 = new StringBuilder();

        int i;

        public int insert_into_软件运行情况(string c_开始或停止, string c_消息, string Manufacture)
        {
            i = 0;

            sb_sql_软件运行情况.AppendFormat(sql_软件运行情况, DateTime.Now.ToString(), c_开始或停止, c_消息, Manufacture);

            try
            {
                i = SQLiteHelper.ExecuteNonQuery(sb_sql_软件运行情况.ToString(), null);
            }
            catch (Exception ex)
            {
                #region 错误日记
                LogManager.LogInfo("插入SQLite数据库异常  " + ex.StackTrace.Substring(ex.StackTrace.IndexOf("行号"), ex.StackTrace.Length - ex.StackTrace.IndexOf("行号")), ex.Message);
                #endregion
            }

            sb_sql_软件运行情况.Remove(0, sb_sql_软件运行情况.Length);

            return i;
        }
    }
}
