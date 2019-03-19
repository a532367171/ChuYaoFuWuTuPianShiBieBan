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

        VideoCapture capture;
        图片识别类 _图片识别类;
        逻辑处理类 _逻辑处理类;
        数据处理类 _数据处理类;
        错误日记类 _错误日记类;

        bool 开始标识;
        public 实时取流类()
        {
          capture = new VideoCapture(取流路径);
           _图片识别类 = new 图片识别类();
            _逻辑处理类 = new 逻辑处理类();
            _数据处理类 = new 数据处理类();
            _错误日记类 = new 错误日记类();
            InitEvent();
        }

        private void InitEvent()
        {
            //逻辑处理类.重启事件 += _重启类.重启;
            //逻辑处理类.错误日记事件 += _错误日记类.错误日记执行函数;
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

            //_数据处理类.插入SQL事件 += _SQL操作类.SQL操作事件执行函数;
            //_数据处理类.插入SQLite事件 += _SQLite操作类.SQL操作事件执行函数;
            //_数据处理类.插入SQLite错误日记事件 += _错误日记类.错误日记执行函数;
            //_数据处理类.插入SQL错误事件 += _错误日记类.错误日记执行函数;
        }


        public bool 开始标识1 { get => 开始标识; set => 开始标识 = value; }

        public void  开始实时取流()
        {
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

                        Cv2.WaitKey(100);
                    }

                }
            }

        }

    }
}
