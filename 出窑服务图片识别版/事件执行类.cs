using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using 出窑服务图片识别版.common;

namespace 出窑服务图片识别版
{
   public class 事件执行类
    {
       public void WriteLog1(string old_value, string new_value, string middle_value)
       {
           int i = 0;
           string strD = "";

           try
           {
               #region 保旧值
               IniFileReference _iniFile = new IniFileReference(AppDomain.CurrentDomain.BaseDirectory + "Geometry.ini");

               _iniFile.IniWriteValue("SYSDNSection", "local_old_value", Convert.ToString(new_value));

               _iniFile.IniWriteValue("SYSDNSection", "local_middle_value", Convert.ToString(middle_value));

               _iniFile = null;
               #endregion

               #region 工位判断

               string Manufacture = ConfigurationManager.AppSettings["生产企业"];

               if (old_value == "62" && new_value == "30")
               {
                   string str = "INSERT INTO CmdGongWei (cGWid,cGWlineCode,cManufacture) VALUES ('08','01','" + Manufacture + "')";
                   strD=str;
               }
               //if (old_value == "29" && new_value == "31")
               if (old_value == "61" && new_value == "29")
               {
                   string str = "INSERT INTO CmdGongWei (cGWid,cGWlineCode,cManufacture) VALUES ('08','02','" + Manufacture + "')";
                   strD=str;
               }
               if (old_value == "27" && new_value == "59")
               {
                   string str = "INSERT INTO CmdGongWei (cGWid,cGWlineCode,cManufacture) VALUES ('09','01','" + Manufacture + "')";
                   strD=str;
               }
               if (old_value == "23" && new_value == "55")
               {
                   string str = "INSERT INTO CmdGongWei (cGWid,cGWlineCode,cManufacture) VALUES ('09','02','" + Manufacture + "')";
                   strD=str;
               }
               if (old_value == "15" && new_value == "47")
               {
                   string str = "INSERT INTO CmdGongWei (cGWid,cGWlineCode,cManufacture) VALUES ('09','03','" + Manufacture + "')";
                   strD=str;
               }
               try
               {
                   if (strD != "")
                   {
                       i = DbUtils.ExecuteNonQuerySp(strD);

                   }
               }
               catch (Exception ex)
               {
                   #region 错误日记
                   AppLog.WriteErr(ex.Message);
                   #endregion

               }
               finally
               {
                   #region 变化日记
                   AppLog.Write(System.Convert.ToString(Convert.ToInt32(old_value), 2).PadLeft(7, '0') + "--" + System.Convert.ToString(Convert.ToInt32(new_value), 2).PadLeft(7, '0') + "--插入标识：" + Convert.ToString(i) + "--" + strD);
                   #endregion
               }

               #endregion
           }
           catch (Exception ex)
           {

               #region 错误日记
               AppLog.WriteErr(ex.Message);
               #endregion
           }

       

       }

    }
}
