using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 出窑工位采集服务
{
    public class 数据处理类
    {
        string sqlite_进出窑_变化日记 = "insert into Car_state (Date,cCAR_NO,cCAR_nub,cCAR_binary,cremark,cManufacture) values('{0}','{1}','{2}','{3}','{4}','{5}')";
        string sqlite_工位及RFID = "insert into The_historical_status (Date,cGWid,cGWlineCode,cGW_text,cManufacture,statu) values('{0}','{1}','{2}','{3}','{4}','{5}')";
        string sql_工位 = "INSERT INTO CmdGongWei1 (cGWid,cGWlineCode,cManufacture) VALUES ('{0}','{1}','{2}')";
        string sqlite_错误日记 = "insert into The_historical_status (Date,cGWid,cGWlineCode,cGW_text,cManufacture,statu) values('{0}','{1}','{2}','{3}','{4}','{5}')";

        StringBuilder sb_sql_车辆状态 = new StringBuilder();

        StringBuilder sb_sqlite_工位及RFID = new StringBuilder();

        StringBuilder sb_sql_工位及RFID = new StringBuilder();

        StringBuilder sb_sqlite_错误日记 = new StringBuilder();


        public 数据处理类()
        {
        }

        #region 插入SQLite
        public delegate void 插入SQLite委托(string str);

        public event 插入SQLite委托 插入SQLite事件;
        protected virtual void On插入SQLite事件触发(string str)
        {
            if (插入SQLite事件 != null)
            {
                插入SQLite事件(str); /* 事件被触发 */
            }
            else
            {
                Console.WriteLine("event not fire");
                Console.ReadKey(); /* 回车继续 */
            }
        }


        #endregion

        #region  插入SQL
        public delegate void 插入SQL委托(string str);

        public event 插入SQL委托 插入SQL事件;
        protected virtual void On插入SQL事件触发(string str)
        {
            if (插入SQL事件 != null)
            {
                插入SQL事件(str); /* 事件被触发 */
            }
            else
            {
                Console.WriteLine("event not fire");
                Console.ReadKey(); /* 回车继续 */
            }
        }


        #endregion

        #region 插入SQL错误
        public delegate void 插入SQL错误委托(string str, Exception ex);

        public event 插入SQL错误委托 插入SQL错误事件;
        protected virtual void On插入SQL错误事件触发(string str, Exception ex)
        {
            if (插入SQL错误事件 != null)
            {
                插入SQL错误事件(str,ex); /* 事件被触发 */
            }
            else
            {
                Console.WriteLine("event not fire");
                Console.ReadKey(); /* 回车继续 */
            }
        }
        #endregion

        #region 插入SQLite错误
        public delegate void 插入SQLite错误日记委托(string str, Exception ex);

        public event 插入SQLite错误日记委托 插入SQLite错误日记事件;
        protected virtual void On插入SQLite错误日记事件触发(string str, Exception ex)
        {
            if (插入SQLite错误日记事件 != null)
            {
                插入SQLite错误日记事件(str,ex); /* 事件被触发 */
            }
            else
            {
                Console.WriteLine("event not fire");
                Console.ReadKey(); /* 回车继续 */
            }
        }
        #endregion


        public void 空车事件执行函数(string c_Manufacture)
        {
            sb_sql_工位及RFID.AppendFormat(sql_工位, "空车", "空车", c_Manufacture);
            bool t2 = true;
            try
            {
                SQL操作函数(sb_sql_工位及RFID.ToString());
            }
            catch (Exception ex)
            {
                t2 = false;
                On插入SQL错误事件触发("空车事件执行函数", ex);
            }
            finally
            {
                try
                {
                    if (t2)
                    {
                        sb_sqlite_工位及RFID.AppendFormat(sqlite_工位及RFID, DateTime.Now.ToString(), "空车", "空车", "", c_Manufacture, "Y");
                    }
                    else
                    {
                        sb_sqlite_工位及RFID.AppendFormat(sqlite_工位及RFID, DateTime.Now.ToString(), "空车", "空车", "", c_Manufacture, "N");
                    }
                    SQLite操作函数(sb_sqlite_工位及RFID.ToString());

                }
                catch (Exception ex)
                {
                    On插入SQLite错误日记事件触发("", ex);
                    //throw;
                }
                finally
                {
                    sb_sql_工位及RFID.Remove(0, sb_sql_工位及RFID.Length);
                    sb_sqlite_工位及RFID.Remove(0, sb_sqlite_工位及RFID.Length);
                }
            }
        }

        public void 满车事件执行函数(string c_Manufacture)
        {
            sb_sql_工位及RFID.AppendFormat(sql_工位, "满车", "满车", c_Manufacture);
            bool t1 = true;
            try
            {
                SQL操作函数(sb_sql_工位及RFID.ToString());
            }
            catch (Exception ex)
            {
                t1 = false;
                On插入SQL错误事件触发("满车事件执行函数", ex);
            }
            finally
            {
                try
                {
                    if (t1)
                    {
                        sb_sqlite_工位及RFID.AppendFormat(sqlite_工位及RFID, DateTime.Now.ToString(), "满车", "满车", "", c_Manufacture, "Y");
                    }
                    else
                    {
                        sb_sqlite_工位及RFID.AppendFormat(sqlite_工位及RFID, DateTime.Now.ToString(), "满车", "满车", "", c_Manufacture, "N");
                    }
                    SQLite操作函数(sb_sqlite_工位及RFID.ToString());

                }
                catch (Exception ex)
                {
                    On插入SQLite错误日记事件触发("满车事件执行函数", ex);
                    //throw;
                }
                finally
                {
                    sb_sql_工位及RFID.Remove(0, sb_sql_工位及RFID.Length);
                    sb_sqlite_工位及RFID.Remove(0, sb_sqlite_工位及RFID.Length);
                }
            }

        }


        public void 二号生产线下车事件执行函数(string c_Manufacture)
        {
            sb_sql_工位及RFID.AppendFormat(sql_工位, "01", "02", c_Manufacture);
            bool t2 = true;
            try
            {
                SQL操作函数(sb_sql_工位及RFID.ToString());
            }
            catch (Exception ex)
            {
                t2 = false;
                On插入SQL错误事件触发("二号生产线下车事件执行函数", ex);
            }
            finally
            {
                try
                {
                    if (t2)
                    {
                        sb_sqlite_工位及RFID.AppendFormat(sqlite_工位及RFID, DateTime.Now.ToString(), "01", "02", "", c_Manufacture, "Y");
                    }
                    else
                    {
                        sb_sqlite_工位及RFID.AppendFormat(sqlite_工位及RFID, DateTime.Now.ToString(), "01", "02", "", c_Manufacture, "N");
                    }
                    SQLite操作函数(sb_sqlite_工位及RFID.ToString());

                }
                catch (Exception ex)
                {
                    On插入SQLite错误日记事件触发("", ex);
                    //throw;
                }
                finally
                {
                    sb_sql_工位及RFID.Remove(0, sb_sql_工位及RFID.Length);
                    sb_sqlite_工位及RFID.Remove(0, sb_sqlite_工位及RFID.Length);
                }
            }
        }

        public void 一号生产线下车事件执行函数(string c_Manufacture)
        {
            sb_sql_工位及RFID.AppendFormat(sql_工位, "01", "01", c_Manufacture);
            bool t1 = true;
            try
            {
                SQL操作函数(sb_sql_工位及RFID.ToString());
            }
            catch (Exception ex)
            {
                t1 = false;
                On插入SQL错误事件触发("一号生产线下车事件执行函数", ex);
            }
            finally
            {
                try
                {
                    if (t1)
                    {
                        sb_sqlite_工位及RFID.AppendFormat(sqlite_工位及RFID, DateTime.Now.ToString(), "01", "01", "", c_Manufacture, "Y");
                    }
                    else
                    {
                        sb_sqlite_工位及RFID.AppendFormat(sqlite_工位及RFID, DateTime.Now.ToString(), "01", "01", "", c_Manufacture, "N");
                    }
                    SQLite操作函数(sb_sqlite_工位及RFID.ToString());

                }
                catch (Exception ex)
                {
                    On插入SQLite错误日记事件触发("一号生产线下车事件执行函数", ex);
                    //throw;
                }
                finally
                {
                    sb_sql_工位及RFID.Remove(0, sb_sql_工位及RFID.Length);
                    sb_sqlite_工位及RFID.Remove(0, sb_sqlite_工位及RFID.Length);
                }
            }

        }

        public void 出一号窑事件执行函数(string c_Manufacture)
        {
            sb_sql_工位及RFID.AppendFormat(sql_工位, "13", "01", c_Manufacture);
            bool t3 = true;
            try
            {
                SQL操作函数(sb_sql_工位及RFID.ToString());
            }
            catch (Exception ex)
            {
                t3 = false;
                On插入SQL错误事件触发("出一号窑事件执行函数", ex);
            }
            finally
            {
                try
                {
                    if (t3)
                    {
                        sb_sqlite_工位及RFID.AppendFormat(sqlite_工位及RFID, DateTime.Now.ToString(), "09", "01", "", c_Manufacture, "Y");
                    }
                    else
                    {
                        sb_sqlite_工位及RFID.AppendFormat(sqlite_工位及RFID, DateTime.Now.ToString(), "09", "01", "", c_Manufacture, "N");
                    }
                    SQLite操作函数(sb_sqlite_工位及RFID.ToString());
                }
                catch (Exception ex)
                {
                    On插入SQLite错误日记事件触发("出一号窑事件执行函数", ex);
                    //throw;
                }
                finally
                {
                    sb_sql_工位及RFID.Remove(0, sb_sql_工位及RFID.Length);
                    sb_sqlite_工位及RFID.Remove(0, sb_sqlite_工位及RFID.Length);
                }
            }

        }

        public void 出二号窑事件执行函数(string c_Manufacture)
        {
            sb_sql_工位及RFID.AppendFormat(sql_工位, "13", "02", c_Manufacture);
            bool t4 = true;
            try
            {
                SQL操作函数(sb_sql_工位及RFID.ToString());
            }
            catch (Exception ex)
            {
                t4 = false;
                On插入SQL错误事件触发("出二号窑事件执行函数", ex);
            }
            finally
            {
                try
                {
                    if (t4)
                    {
                        sb_sqlite_工位及RFID.AppendFormat(sqlite_工位及RFID, DateTime.Now.ToString(), "09", "02", "", c_Manufacture, "Y");
                    }
                    else
                    {
                        sb_sqlite_工位及RFID.AppendFormat(sqlite_工位及RFID, DateTime.Now.ToString(), "09", "02", "", c_Manufacture, "N");
                    }
                    SQLite操作函数(sb_sqlite_工位及RFID.ToString());

                }
                catch (Exception ex)
                {
                    On插入SQLite错误日记事件触发("出二号窑事件执行函数", ex);
                    //throw;
                }
                finally
                {
                    sb_sql_工位及RFID.Remove(0, sb_sql_工位及RFID.Length);
                    sb_sqlite_工位及RFID.Remove(0, sb_sqlite_工位及RFID.Length);
                }
            }
        }

        public void 出三号窑事件执行函数(string c_Manufacture)
        {
            sb_sql_工位及RFID.AppendFormat(sql_工位, "13", "03", c_Manufacture);
            bool t5 = true;
            try
            {
                SQL操作函数(sb_sql_工位及RFID.ToString());
            }
            catch (Exception ex)
            {
                t5 = false;
                On插入SQL错误事件触发("出三号窑事件执行函数", ex);
            }
            finally
            {
                try
                {
                    if (t5)
                    {
                        sb_sqlite_工位及RFID.AppendFormat(sqlite_工位及RFID, DateTime.Now.ToString(), "09", "03", "", c_Manufacture, "Y");
                    }
                    else
                    {
                        sb_sqlite_工位及RFID.AppendFormat(sqlite_工位及RFID, DateTime.Now.ToString(), "09", "03", "", c_Manufacture, "N");
                    }
                    SQLite操作函数(sb_sqlite_工位及RFID.ToString());

                }
                catch (Exception ex)
                {
                    On插入SQLite错误日记事件触发("出三号窑事件执行函数", ex);
                    //throw;
                }
                finally
                {
                    sb_sql_工位及RFID.Remove(0, sb_sql_工位及RFID.Length);
                    sb_sqlite_工位及RFID.Remove(0, sb_sqlite_工位及RFID.Length);
                }
            }
        }

        public void 出四号窑事件执行函数(string c_Manufacture)
        {
            sb_sql_工位及RFID.AppendFormat(sql_工位, "13", "04", c_Manufacture);
            bool t5 = true;
            try
            {
                SQL操作函数(sb_sql_工位及RFID.ToString());
            }
            catch (Exception ex)
            {
                t5 = false;
                On插入SQL错误事件触发("出四号窑事件执行函数", ex);
            }
            finally
            {
                try
                {
                    if (t5)
                    {
                        sb_sqlite_工位及RFID.AppendFormat(sqlite_工位及RFID, DateTime.Now.ToString(), "09", "04", "", c_Manufacture, "Y");
                    }
                    else
                    {
                        sb_sqlite_工位及RFID.AppendFormat(sqlite_工位及RFID, DateTime.Now.ToString(), "09", "04", "", c_Manufacture, "N");
                    }
                    SQLite操作函数(sb_sqlite_工位及RFID.ToString());

                }
                catch (Exception ex)
                {
                    On插入SQLite错误日记事件触发("出四号窑事件执行函数", ex);
                    //throw;
                }
                finally
                {
                    sb_sql_工位及RFID.Remove(0, sb_sql_工位及RFID.Length);
                    sb_sqlite_工位及RFID.Remove(0, sb_sqlite_工位及RFID.Length);
                }
            }
        }


        public void 进出窑_变化日记(string 进_出窑, string cCAR_nub, string c_Manufacture)
        {
            //sb_sql_车辆状态.AppendFormat(sqlite_进出窑_变化日记, DateTime.Now.ToString(), 进_出窑, cCAR_nub, System.Convert.ToString(Convert.ToInt32(cCAR_nub), 2).PadLeft(7, '0'), Position_state(cCAR_nub), c_Manufacture);
            try
            {
                sb_sql_车辆状态.AppendFormat(sqlite_进出窑_变化日记, DateTime.Now.ToString(), 进_出窑, cCAR_nub, System.Convert.ToString(Convert.ToInt32(cCAR_nub), 2).PadLeft(7, '0'), "状态说明 还没写", c_Manufacture);
                SQLite操作函数(sb_sql_车辆状态.ToString());
            }
            catch (Exception ex)
            {

                On插入SQLite错误日记事件触发("进出窑_变化日记", ex);
            }
            finally
            {
                sb_sql_车辆状态.Remove(0, sb_sql_车辆状态.Length);
            }

        }

        private string Position_state(string cCAR_nub)
        {
            return "状态说明 还没写";
        }

        private void SQLite操作函数(string sql)
        {
            try
            {
                On插入SQLite事件触发(sql);
            }
            catch (Exception ex)
            {
                On插入SQLite错误日记事件触发("SQLite操作函数",ex);
                throw;
            }
        }

        private void SQL操作函数(string sql)
        {
            try
            {
                On插入SQL事件触发(sql);

            }
            catch (Exception ex)
            {
                On插入SQL错误事件触发("SQL操作函数",ex);
                throw;
            }
        }
    }
}
