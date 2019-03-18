//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.IO;
//using System.Linq;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
//using 出窑工位采集服务.common;
//using 出窑工位采集服务.ModBus;

//namespace 出窑工位采集服务
//{
//    public class 出窑工位周期执行类
//    {

//        #region 字段声明

//        string read_old_value_出窑;

//        string read_middle_value_出窑;

//        string read_new_value_出窑;

//        //private static readonly Object locker = new Object();

//        string Manufacture = ConfigurationManager.AppSettings["生产企业"];

//        保存旧值 _保存旧值 = new 保存旧值();

//        String[] str_old_value_出窑 = new String[2];

//        string s_cGWid = "";
//        string s_cGWlineCode = "";
//        string s_cManufacture = "";

//        private  string IP_出窑 = ConfigurationManager.AppSettings["出窑工控机IP"];


//        #endregion

//        #region 初始化
//        public 出窑工位周期执行类()
//        {
//            //IniFileReference _iniFile = new IniFileReference(AppDomain.CurrentDomain.BaseDirectory + "Geometry.ini");

//            //string local_old_value = _iniFile.IniReadValue("SYSDNSection", "local_old_value");

//            //read_new_value = _iniFile.IniReadValue("SYSDNSection", "local_middle_value");

//            //read_old_value = local_old_value;

//            //_iniFile = null;


//            //string strLogFilePath1 = AppDomain.CurrentDomain.BaseDirectory;

//            //string strLogFilePath错误日记位置 = ConfigHelper.GetValue("错误日记位置").ToLower();
//            //string strLogFilePath日记位置 = ConfigHelper.GetValue("日记位置").ToLower();

//            //strLogFilePath错误日记位置 = strLogFilePath1 + strLogFilePath错误日记位置;
//            //strLogFilePath日记位置 = strLogFilePath1 + strLogFilePath日记位置;

//            var sql_ini_出窑 = "select cCAR_nub from Car_state where cManufacture='福建省榕圣市政工程股份有限公司连江建材分公司' AND cCAR_NO='出窑' order by [id] desc  limit 0,2";

//            DataTable isqlite1_ini_出窑 = SQLiteHelper.ExecuteDataTable(sql_ini_出窑, null);

//            str_old_value_出窑 = StringHelper.dtToArr1(isqlite1_ini_出窑);

//            read_old_value_出窑 = str_old_value_出窑[0];

//            read_middle_value_出窑 = str_old_value_出窑[1];





//            //if (Directory.Exists(strLogFilePath错误日记位置) == false)//如果不存在就创建file文件夹
//            //{
//            //    Directory.CreateDirectory(strLogFilePath错误日记位置);
//            //}
//            //else
//            //{
//            //    FileInfo fi = new FileInfo(strLogFilePath错误日记位置);

//            //    long i = fi.Length;

//            //    if (i > 102400)
//            //    {
//            //        string contents = "文件太长覆盖";

//            //        File.WriteAllText(strLogFilePath错误日记位置, contents);
//            //    }
//            //}




//            //if (Directory.Exists(strLogFilePath日记位置) == false)//如果不存在就创建file文件夹
//            //{
//            //    Directory.CreateDirectory(strLogFilePath日记位置);
//            //}
//            //else
//            //{
//            //    FileInfo fi = new FileInfo(strLogFilePath日记位置);

//            //    long i = fi.Length;

//            //    if (i > 102400)
//            //    {
//            //        string contents = "文件太长覆盖";

//            //        File.WriteAllText(strLogFilePath日记位置, contents);
//            //    }
//            //}

//        }
//        #endregion

//        //#region 事件的申明

//        //public delegate void NumManipulationHandler1(string x, string y, string z);

//        //public event NumManipulationHandler1 ChangeNum1;

//        //protected virtual void OnNumChanged(object obj)
//        //{
//        //    新旧值结构 struct参数 = (新旧值结构)obj;

//        //    if (ChangeNum1 != null)
//        //    {
//        //        ChangeNum1(struct参数.old_value, struct参数.new_value, struct参数.middle_value); /* 事件被触发 */
//        //    }
//        //    else
//        //    {
//        //        Console.WriteLine("event not fire");
//        //        Console.ReadKey(); /* 回车继续 */
//        //    }
//        //}


//        //#endregion

//        #region 周期执行（定时器到期执行）
//        //public void theout(object source, System.Timers.ElapsedEventArgs e)
//        public void theout()

//        {
//            //System.Timers.Timer t1 = (System.Timers.Timer)source;
//            //t1.Enabled = false;
//            //t1.Stop();

//            try
//            {
//                #region 读取工控机

//                ModBus采集类1 read_出窑 = ModBus采集类1.GetInstance(IP_出窑,"");

//                try
//                {
//                    //read_new_value_出窑 = ModBusTCPIPWrapper.Instance.Read();
//                    //read_new_value_出窑 = read_出窑.Read();
//                    read_new_value_出窑 ="";


//                    if (read_new_value_出窑 == "0")
//                    {
//                        #region 错误日记

//                        LogManager.LogInfo("出窑读取到0值 准备重启  ", "读的太快？");

//                        #endregion

//                        Thread.Sleep(500);
//                        System.Diagnostics.Process.Start("D:\\管片工位采集服务(连江)-分支\\出窑工位采集服务\\出窑工位采集服务\\bin\\Debug\\出窑工位采集服务.exe", "5");

//                    }
//                }
//                catch (Exception ex)
//                {

//                    #region 错误日记

//                    LogManager.LogInfo("出窑读取工控机异常 准备重启  " + ex.StackTrace.Substring(ex.StackTrace.IndexOf("行号"), ex.StackTrace.Length - ex.StackTrace.IndexOf("行号")), ex.Message);

//                    #endregion

//                    Thread.Sleep(500);
//                    System.Diagnostics.Process.Start("D:\\管片工位采集服务(连江)-分支\\出窑工位采集服务\\出窑工位采集服务\\bin\\Debug\\出窑工位采集服务.exe", "5");

//                }

//                #endregion
//                if (read_new_value_出窑 == read_old_value_出窑 || read_new_value_出窑 == read_middle_value_出窑)
//                {
//                }
//                else
//                {
                   
//                    try
//                    {
//                        #region 保旧值
//                        //IniFileReference _iniFile = new IniFileReference(AppDomain.CurrentDomain.BaseDirectory + "Geometry.ini");

//                        //_iniFile.IniWriteValue("SYSDNSection", "local_old_value", Convert.ToString(read_new_value_出窑));

//                        //_iniFile.IniWriteValue("SYSDNSection", "local_middle_value", Convert.ToString(read_old_value_出窑));

//                        //_iniFile = null;
//                        #endregion

//                        #region 变化日记


//                        var isqlite_结果 = _保存旧值.Save_the_车辆状态_value("出窑", read_new_value_出窑, Manufacture);

//                        #region 旧
//                        ////AppLog.Write(System.Convert.ToString(Convert.ToInt32(read_old_value_出窑), 2).PadLeft(7, '0') + "--" + System.Convert.ToString(Convert.ToInt32(read_new_value_出窑), 2).PadLeft(7, '0') + "--插入标识：" + Convert.ToString(i) + "--" + strD);

//                        ////var sql_出窑_变化日记 =
//                        ////    "insert into Car_state (Date,cCAR_NO,cCAR_nub,cCAR_binary,cremark,cManufacture) values('{0}','{1}','{2}','{3}','{4}','{5}')";

//                        //sb_出窑_变化日记.AppendFormat(sql_变化日记, timenow, "出窑", read_new_value_出窑, System.Convert.ToString(Convert.ToInt32(read_new_value_出窑), 2).PadLeft(7, '0'), "目前还没写自己判断吧", Manufacture);

//                        //if (sb_出窑_变化日记.Length > 0)
//                        //{
//                        //    var isqlite_结果 = SQLiteHelper.ExecuteNonQuery(sb_出窑_变化日记.ToString(), null);
//                        //}

//                        //sb_出窑_变化日记.Remove(0, sb_出窑_变化日记.Length); 
//                        #endregion

//                        #endregion

//                        #region 工位判断
            
//                        int i = 0;

//                        string strD = "";

//                        if (read_old_value_出窑 == "62" && read_new_value_出窑 == "30")
//                        {
//                            string str = "INSERT INTO CmdGongWei (cGWid,cGWlineCode,cManufacture) VALUES ('08','02','" + Manufacture + "')";
//                            strD = str;

//                            s_cGWid = "08";
//                            s_cGWlineCode = "02";
//                            s_cManufacture = Manufacture;
//                        }
//                        //if (read_old_value == "29" && read_new_value == "31")
//                        if (read_old_value_出窑 == "61" && read_new_value_出窑 == "29")
//                        {
//                            string str = "INSERT INTO CmdGongWei (cGWid,cGWlineCode,cManufacture) VALUES ('08','01','" + Manufacture + "')";
//                            strD = str;

//                            s_cGWid = "08";
//                            s_cGWlineCode = "01";
//                            s_cManufacture = Manufacture;
//                        }
//                        if (read_old_value_出窑 == "27" && read_new_value_出窑 == "59")
//                        {
//                            string str = "INSERT INTO CmdGongWei (cGWid,cGWlineCode,cManufacture) VALUES ('09','01','" + Manufacture + "')";
//                            strD = str;

//                            s_cGWid = "09";
//                            s_cGWlineCode = "01";
//                            s_cManufacture = Manufacture;
//                        }
//                        if (read_old_value_出窑 == "23" && read_new_value_出窑 == "55")
//                        {
//                            string str = "INSERT INTO CmdGongWei (cGWid,cGWlineCode,cManufacture) VALUES ('09','02','" + Manufacture + "')";
//                            strD = str;

//                            s_cGWid = "09";
//                            s_cGWlineCode = "02";
//                            s_cManufacture = Manufacture;
//                        }
//                        if (read_old_value_出窑 == "15" && read_new_value_出窑 == "47")
//                        {
//                            string str = "INSERT INTO CmdGongWei (cGWid,cGWlineCode,cManufacture) VALUES ('09','03','" + Manufacture + "')";
//                            strD = str;

//                            s_cGWid = "09";
//                            s_cGWlineCode = "03";
//                            s_cManufacture = Manufacture;
//                        }
//                        try
//                        {
//                            if (strD != "")
//                            {
//                                //i = DbUtils.ExecuteNonQuerySp(strD);
//                                int isqlite = 0;

//                                isqlite = _保存旧值.Save_the_工位及RFID_value(s_cGWid, s_cGWlineCode, "", s_cManufacture, "Y");


//                                i = 10000;
//                                i = DbUtils.ExecuteNonQuerySp("Pro_INSERT_INTO_CmdGongWei", s_cGWid, s_cGWlineCode, s_cManufacture);

//                            }
//                        }
//                        catch (Exception ex)
//                        {
//                            #region 错误日记

//                            LogManager.LogInfo("出窑 插入外网数据库异常  " + ex.StackTrace.Substring(ex.StackTrace.IndexOf("行号"), ex.StackTrace.Length - ex.StackTrace.IndexOf("行号")), ex.Message);

//                            #endregion

//                        }
//                        finally
//                        {
//                            //#region 变化日记
//                            //AppLog.Write(System.Convert.ToString(Convert.ToInt32(read_old_value_出窑), 2).PadLeft(7, '0') + "--" + System.Convert.ToString(Convert.ToInt32(read_new_value_出窑), 2).PadLeft(7, '0') + "--插入标识：" + Convert.ToString(i) + "--" + strD);
//                            //#endregion

//                            if (i == 10000)
//                            {
//                                var sqlTemp1 = "UPDATE The_historical_status SET statu ='N' WHERE Id =(select MAX(Id) from The_historical_status where cGWid='" + s_cGWid + "' AND cGWlineCode='" + s_cGWlineCode + "'AND cManufacture='" + s_cManufacture + "')";
//                                SQLiteHelper.ExecuteNonQuery(sqlTemp1, null);
//                            }

//                            read_middle_value_出窑 = read_old_value_出窑;
//                            read_old_value_出窑 = read_new_value_出窑;
//                        }

//                        #endregion
//                    }
//                    catch (Exception ex)
//                    {

//                        #region 错误日记

//                        LogManager.LogInfo("工位判断出错了  ",ex.Message);

//                        #endregion
//                    }


//                }
//            }
//            catch (Exception ex)
//            {
//                #region 错误日记

//                LogManager.LogInfo("大循环出错了  " + ex.StackTrace.Substring(ex.StackTrace.IndexOf("行号"), ex.StackTrace.Length - ex.StackTrace.IndexOf("行号")), ex.Message);

//                #endregion
//                System.Diagnostics.Process.Start("D:\\管片工位采集服务(连江)-分支\\出窑工位采集服务\\出窑工位采集服务\\bin\\Debug\\出窑工位采集服务.exe", "5");
//            }
//            finally
//            {
//                //t1.Enabled = true;
//                //t1.Start();
//            }
//        }

//        #endregion

//    }
//}
