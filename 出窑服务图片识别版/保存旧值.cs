using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using 出窑服务图片识别版.common;

namespace 出窑服务图片识别版
{
    public class 保存旧值
    {
        string sql_进出窑_变化日记 = "insert into Car_state (Date,cCAR_NO,cCAR_nub,cCAR_binary,cremark,cManufacture) values('{0}','{1}','{2}','{3}','{4}','{5}')";
        string sql_工位及RFID = "insert into The_historical_status (Date,cGWid,cGWlineCode,cGW_text,cManufacture,statu) values('{0}','{1}','{2}','{3}','{4}','{5}')";

        StringBuilder sb_sql_车辆状态 = new StringBuilder();
        StringBuilder sb_sql_工位及RFID = new StringBuilder();

        int i;
        int j;

        public int Save_the_车辆状态_value(string 进_出窑, string cCAR_nub, string Manufacture)
        {
            i = 0;

            sb_sql_车辆状态.AppendFormat(sql_进出窑_变化日记, DateTime.Now.ToString(), 进_出窑, cCAR_nub, System.Convert.ToString(Convert.ToInt32(cCAR_nub), 2).PadLeft(7, '0'), Position_state(cCAR_nub), Manufacture);

            try
            {
                i = SQLiteHelper.ExecuteNonQuery(sb_sql_车辆状态.ToString(), null);
            }
            catch (Exception ex)
            {
               #region 错误日记
                AppLog.WriteErr(ex.Message + "出窑_Save_the_车辆状态_value_异常");
                #endregion
            }
            finally
            {

                sb_sql_车辆状态.Remove(0, sb_sql_车辆状态.Length);
            }
            return i;
        }

        private string Position_state(string cCAR_nub)
        {
            return "还没写";
        }

        public int Save_the_工位及RFID_value(string c_工位, string c_线号, string c_RFID数据, string c_Manufacture, string c_state)
        {
            j = 0;

            sb_sql_工位及RFID.AppendFormat(sql_工位及RFID, DateTime.Now.ToString(), c_工位, c_线号, c_RFID数据, c_Manufacture, c_state);


            try
            {
                j = SQLiteHelper.ExecuteNonQuery(sb_sql_工位及RFID.ToString(), null);
            }
            catch (Exception ex)
            {
                #region 错误日记
                AppLog.WriteErr(ex.Message + "出窑_Save_the_工位及RFID_value");
                #endregion
            }
            finally
            {

                sb_sql_工位及RFID.Remove(0, sb_sql_工位及RFID.Length);
            }
            return j;
        }

    }
}
