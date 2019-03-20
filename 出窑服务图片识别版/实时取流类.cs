using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 出窑服务图片识别版
{
    class 实时取流类
    {
        private string 取流路径 = ConfigurationManager.AppSettings["取流路径"];
        private string 临时图片路径 = ConfigurationManager.AppSettings["临时图片路径"];
        private string 显示小窗标识 = ConfigurationManager.AppSettings["显示小窗标识"];
        private string 等待时间 = ConfigurationManager.AppSettings["等待时间"];
        private string 重启路径 = ConfigurationManager.AppSettings["重启路径"];

        public delegate void 错误日记委托(string str, Exception ex);

        public event 错误日记委托 错误日记事件;
        protected virtual void On错误日记事件触发(string str, Exception ex)
        {
            if (错误日记事件 != null)
            {
                错误日记事件(str, ex); /* 事件被触发 */
            }
            else
            {
                Console.WriteLine("event not fire");
                Console.ReadKey(); /* 回车继续 */
            }
        }


        VideoCapture capture;
        图片识别类 _图片识别类;
        逻辑处理类 _逻辑处理类;
        数据处理类 _数据处理类;
        错误日记类 _错误日记类;
        SQL操作类 _SQL操作类;
        SQLite操作类 _SQLite操作类;
        重启类 _重启类;

        bool 开始标识;
        public 实时取流类()
        {
            _重启类 = new 重启类(重启路径);
            _逻辑处理类 = new 逻辑处理类();
            _数据处理类 = new 数据处理类();
            _错误日记类 = new 错误日记类();
            _SQL操作类 = new SQL操作类();
            _SQLite操作类 = new SQLite操作类();

            InitEvent();

            try
            {
                capture = new VideoCapture(取流路径);
                //capture = new VideoCapture("");


            }
            catch (Exception ex)
            {
                On错误日记事件触发("取流错误",ex);
                _重启类.重启(重启路径);
            }

            try
            {
                _图片识别类 = new 图片识别类();
            }
            catch (Exception ex)
            {
                On错误日记事件触发("图片识别类", ex);
                _重启类.重启(重启路径);
            }
        }

        private void InitEvent()
        {
            //逻辑处理类.重启事件 += _重启类.重启;

            错误日记事件 += _错误日记类.错误日记执行函数;

            //逻辑处理类.采集事件 += _出窑工位判断类.工位判断执行类;

            _逻辑处理类.一号生产线下车事件 += _数据处理类.一号生产线下车事件执行函数;
            _逻辑处理类.二号生产线下车事件 += _数据处理类.二号生产线下车事件执行函数;
            _逻辑处理类.出一号窑事件 += _数据处理类.出一号窑事件执行函数;
            _逻辑处理类.出二号窑事件 += _数据处理类.出二号窑事件执行函数;
            _逻辑处理类.出三号窑事件 += _数据处理类.出三号窑事件执行函数;
            _逻辑处理类.出四号窑事件 += _数据处理类.出四号窑事件执行函数;

            _逻辑处理类.满车事件 += _数据处理类.满车事件执行函数;
            _逻辑处理类.空车事件 += _数据处理类.空车事件执行函数;

            _逻辑处理类.保存变化值事件 += _数据处理类.进出窑_变化日记;
            _逻辑处理类.工位判断错误日记事件 += _错误日记类.错误日记执行函数;

            _数据处理类.插入SQL事件 += _SQL操作类.SQL操作事件执行函数;
            _数据处理类.插入SQLite事件 += _SQLite操作类.SQL操作事件执行函数;
            _数据处理类.插入SQLite错误日记事件 += _错误日记类.错误日记执行函数;
            _数据处理类.插入SQL错误事件 += _错误日记类.错误日记执行函数;
        }


        public bool 开始标识1 { get => 开始标识; set => 开始标识 = value; }

        public void  开始实时取流()
        {
            using (var windowSrc = new Window("src"))
            using (var frame = new Mat())
            using (var image缩小 = new Mat())
            {
                while (开始标识1)
                {
                    while (true)
                    {
                        capture.Read(frame);

                        if (frame.Empty())
                            break;

                        Cv2.Resize(frame, image缩小, new Size(280, 280), 0, 0, InterpolationFlags.Linear);//缩小28*28

                        Cv2.ImWrite(临时图片路径, image缩小);

                       Int32 I= _图片识别类.识别方法();

                        _逻辑处理类.逻辑判断方法(I);

                        if (显示小窗标识=="1")
                        {
                            windowSrc.ShowImage(image缩小);

                        }


                        Cv2.WaitKey(int.Parse(等待时间));
                    }

                }
            }

        }

    }
}
