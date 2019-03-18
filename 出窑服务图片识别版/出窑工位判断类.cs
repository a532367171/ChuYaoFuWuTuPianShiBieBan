using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using 出窑工位采集服务.common;

namespace 出窑工位采集服务
{
    public class 出窑工位判断类
    {
        string read_old_value_出窑;

        string read_middle_value_出窑;

        string read_new_value_出窑;

        string Manufacture;

        //string Manufacture = ConfigurationManager.AppSettings["生产企业"];

        保存旧值 _保存旧值 = new 保存旧值();

        String[] str_old_value_出窑 = new String[2];

        public 出窑工位判断类()
        {
        }

        public 出窑工位判断类(string str)
        {
            Manufacture = str;
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            var sql_ini_出窑 = "select cCAR_nub from Car_state where cManufacture='"+ Manufacture+"' AND cCAR_NO='出窑' order by [id] desc  limit 0,2";

            DataTable isqlite1_ini_出窑 = SQLiteHelper.ExecuteDataTable(sql_ini_出窑, null);

            str_old_value_出窑 = StringHelper.dtToArr1(isqlite1_ini_出窑);

            read_old_value_出窑 = str_old_value_出窑[0];

            read_middle_value_出窑 = str_old_value_出窑[1];
        }

        #region 一号生产线下车
        public delegate void 一号生产线下车委托(string str);

        public event 一号生产线下车委托 一号生产线下车事件;
        protected virtual void On一号生产线上车事件触发(string str)
        {
            if (一号生产线下车事件 != null)
            {
                一号生产线下车事件(str); /* 事件被触发 */
            }
            else
            {
                Console.WriteLine("event not fire");
                Console.ReadKey(); /* 回车继续 */
            }
        }


        #endregion

        #region 二号生产线下车
        public delegate void 二号生产线下车委托(string str);

        public event 二号生产线下车委托 二号生产线下车事件;
        protected virtual void On二号生产线下车事件触发(string str)
        {
            if (二号生产线下车事件 != null)
            {
                二号生产线下车事件(str); /* 事件被触发 */
            }
            else
            {
                Console.WriteLine("event not fire");
                Console.ReadKey(); /* 回车继续 */
            }
        }


        #endregion

        #region 出一号窑
        public delegate void 出一号窑委托(string str);

        public event 出一号窑委托 出一号窑事件;
        protected virtual void On出一号窑事件触发(string str)
        {
            if (出一号窑事件 != null)
            {
                出一号窑事件(str); /* 事件被触发 */
            }
            else
            {
                Console.WriteLine("event not fire");
                Console.ReadKey(); /* 回车继续 */
            }
        }


        #endregion

        #region 出二号窑
        public delegate void 出二号窑委托(string str);

        public event 出二号窑委托 出二号窑事件;
        protected virtual void On出二号窑事件触发(string str)
        {
            if (出二号窑事件 != null)
            {
                出二号窑事件(str); /* 事件被触发 */
            }
            else
            {
                Console.WriteLine("event not fire");
                Console.ReadKey(); /* 回车继续 */
            }
        }


        #endregion

        #region 出三号窑
        public delegate void 出三号窑委托(string str);

        public event 出三号窑委托 出三号窑事件;
        protected virtual void On出三号窑事件触发(string str)
        {
            if (出三号窑事件 != null)
            {
                出三号窑事件(str); /* 事件被触发 */
            }
            else
            {
                Console.WriteLine("event not fire");
                Console.ReadKey(); /* 回车继续 */
            }
        }


        #endregion

        #region 出四号窑
        public delegate void 出四号窑委托(string str);

        public event 出四号窑委托 出四号窑事件;
        protected virtual void On出四号窑事件触发(string str)
        {
            if (出四号窑事件 != null)
            {
                出四号窑事件(str); /* 事件被触发 */
            }
            else
            {
                Console.WriteLine("event not fire");
                Console.ReadKey(); /* 回车继续 */
            }
        }


        #endregion

        #region 错误日记
        public delegate void 工位判断错误日记委托(string str, Exception ex);

        public event 工位判断错误日记委托 工位判断错误日记事件;
        protected virtual void On工位判断错误日记事件触发(string str, Exception ex)
        {
            if (工位判断错误日记事件 != null)
            {
                工位判断错误日记事件(str,ex); /* 事件被触发 */
            }
            else
            {
                Console.WriteLine("event not fire");
                Console.ReadKey(); /* 回车继续 */
            }
        }
        #endregion

        #region 保存变化值
        public delegate void 保存变化值委托(string 出_出窑, string cCAR_nub, string c_Manufacture);

        public event 保存变化值委托 保存变化值事件;
        protected virtual void On保存变化值事件触发(string 出_出窑, string cCAR_nub, string c_Manufacture)
        {
            if (保存变化值事件 != null)
            {
                保存变化值事件(出_出窑, cCAR_nub, c_Manufacture); /* 事件被触发 */
            }
            else
            {
                Console.WriteLine("event not fire");
                Console.ReadKey(); /* 回车继续 */
            }
        }
        #endregion

        public void 工位判断执行类(string read_new_value_出窑)
        {
            if (read_new_value_出窑 == read_old_value_出窑 || read_new_value_出窑 == read_middle_value_出窑)
            {
            }
            else
            {
                try
                {
                    if (read_old_value_出窑 == "65" && read_new_value_出窑 == "1")
                    {
                        On二号生产线下车事件触发(Manufacture);
                    }
                    if (read_old_value_出窑 == "66" && read_new_value_出窑 == "2")
                    {
                        On一号生产线上车事件触发(Manufacture);
                    }
                    if (read_old_value_出窑 == "68" && read_new_value_出窑 == "4")
                    {
                        On出一号窑事件触发(Manufacture);
                    }
                    if (read_old_value_出窑 == "72" && read_new_value_出窑 == "8")
                    {
                        On出二号窑事件触发(Manufacture);
                    }
                    if (read_old_value_出窑 == "80" && read_new_value_出窑 == "16")
                    {
                        On出三号窑事件触发(Manufacture);
                    }
                    if (read_old_value_出窑 == "96" && read_new_value_出窑 == "32")
                    {
                        On出四号窑事件触发(Manufacture);
                    }
                    read_middle_value_出窑 = read_old_value_出窑;
                    read_old_value_出窑 = read_new_value_出窑;
                }
                catch (Exception ex)
                {
                    On工位判断错误日记事件触发("工位判断错误", ex);
                }
                finally
                {
                    On保存变化值事件触发("出窑",read_new_value_出窑, Manufacture);
                }
            }
        }
    }
}
