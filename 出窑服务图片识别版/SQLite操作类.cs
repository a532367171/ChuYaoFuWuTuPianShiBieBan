using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using 出窑工位采集服务.common;

namespace 出窑工位采集服务
{
    public class SQLite操作类
    {
        public void SQL操作事件执行函数(string sql)
        {
            try
            {
                SQLiteHelper.ExecuteNonQuery(sql, null);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
