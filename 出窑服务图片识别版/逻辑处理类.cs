using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 出窑服务图片识别版;
using 出窑服务图片识别版.common;

namespace 出窑服务图片识别版
{
    class 逻辑处理类
    {
        int int32_这次标识 = 100;
        int int32_上一次标识 = 100;
        int int32_次数标识 = 0;
        int int32_上一次有用的标识 = 100;

        保存旧值 _保存旧值 ;

        private string Manufacture = ConfigurationManager.AppSettings["生产企业"];
        private string 重复次数 = ConfigurationManager.AppSettings["重复次数"];


        #region 事件

        #region 空车
        public delegate void 空车委托(string str);

        public event 空车委托 空车事件;
        protected virtual void On空车事件触发(string str)
        {
            if (空车事件 != null)
            {
                空车事件(str); /* 事件被触发 */
            }
            else
            {
                Console.WriteLine("event not fire");
                Console.ReadKey(); /* 回车继续 */
            }
        }


        #endregion

        #region 满车
        public delegate void 满车委托(string str);

        public event 满车委托 满车事件;
        protected virtual void On满车事件触发(string str)
        {
            if (满车事件 != null)
            {
                满车事件(str); /* 事件被触发 */
            }
            else
            {
                Console.WriteLine("event not fire");
                Console.ReadKey(); /* 回车继续 */
            }
        }


        #endregion

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
                工位判断错误日记事件(str, ex); /* 事件被触发 */
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

        #endregion

        public 逻辑处理类()
        {

             _保存旧值 = new 保存旧值();

            var sql_ini_出窑 = "select cCAR_nub from Car_state where cManufacture='福建省榕圣市政工程股份有限公司连江建材分公司' AND cCAR_NO='出窑图片版' order by [id] desc  limit 0,2";

            DataTable isqlite1_ini_出窑 = SQLiteHelper.ExecuteDataTable(sql_ini_出窑, null);


            String[] str_old_value_出窑 = StringHelper.dtToArr1(isqlite1_ini_出窑);

            if (str_old_value_出窑[0]==null)
            {
                int32_次数标识 = 100;

            }
            else
            {
                int32_上一次有用的标识 = int.Parse(str_old_value_出窑[0]);

            }
            //if (str_old_value_出窑[1]==null)
            //{
            //     int32_上一次有用的标识 = 100;
            //}
            //else
            //{
            //    int32_上一次有用的标识 = int.Parse(str_old_value_出窑[1]);

            //}

        }



        public void 逻辑判断方法(Int32 bestIdx)
        {

            if (bestIdx == int32_这次标识)
            {
                int32_次数标识++;
            }
            else
            {
                int32_次数标识 = 0;
                int32_上一次标识 = int32_这次标识;
                int32_这次标识 = bestIdx;
            }

            if (int32_次数标识 > int.Parse(重复次数) && int32_这次标识 != int32_上一次有用的标识)
            {
                int32_上一次有用的标识 = int32_这次标识;

                var isqlite_结果 = _保存旧值.Save_the_车辆状态_value("出窑图片版", int32_这次标识.ToString(), Manufacture);

                switch (int32_这次标识)
                {
                    case 0:
                        On空车事件触发(Manufacture);
                        break;
                    case 1:
                        On出一号窑事件触发(Manufacture);
                        break;
                    case 2:
                        On出二号窑事件触发(Manufacture);
                        break;
                    case 3:
                        On出三号窑事件触发(Manufacture);
                        break;
                    case 4:
                        On出四号窑事件触发(Manufacture);
                        break;
                    case 5:
                        On一号生产线上车事件触发(Manufacture);
                        break;
                    case 6:
                        On二号生产线下车事件触发(Manufacture);
                        break;
                    case 7:
                        On满车事件触发(Manufacture);
                        break;
                }
                int32_次数标识 = 0;

            }


        }
    }
}
