using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using TensorFlow;
using 出窑服务图片识别版;
using 出窑服务图片识别版.common;

namespace 出窑服务图片识别版
{
    public partial class Service1 : ServiceBase
    {
        private string Manufacture = ConfigurationManager.AppSettings["生产企业"];

        运行情况 _运行情况;
        实时取流类 _实时取流类;
        public Service1()
        {
            InitializeComponent();
            InitService();
            _运行情况 = new 运行情况();

        }
        /// <summary>
        /// 初始化服务参数
        /// </summary>
        private void InitService()
        {
            base.AutoLog = false;
            base.CanShutdown = true;
            base.CanStop = true;
            base.CanPauseAndContinue = true;
            base.ServiceName = "出窑服务图片识别版";  //这个名字很重要，设置不一致会产生 1083 错误哦(在文章最后会说到这个问题)！

        }

        //#region 空车
        //public delegate void 空车委托(string str);

        //public event 空车委托 空车事件;
        //protected virtual void On空车事件触发(string str)
        //{
        //    if (空车事件 != null)
        //    {
        //        空车事件(str); /* 事件被触发 */
        //    }
        //    else
        //    {
        //        Console.WriteLine("event not fire");
        //        Console.ReadKey(); /* 回车继续 */
        //    }
        //}


        //#endregion

        //#region 满车
        //public delegate void 满车委托(string str);

        //public event 满车委托 满车事件;
        //protected virtual void On满车事件触发(string str)
        //{
        //    if (满车事件 != null)
        //    {
        //        满车事件(str); /* 事件被触发 */
        //    }
        //    else
        //    {
        //        Console.WriteLine("event not fire");
        //        Console.ReadKey(); /* 回车继续 */
        //    }
        //}


        //#endregion

        //#region 一号生产线下车
        //public delegate void 一号生产线下车委托(string str);

        //public event 一号生产线下车委托 一号生产线下车事件;
        //protected virtual void On一号生产线上车事件触发(string str)
        //{
        //    if (一号生产线下车事件 != null)
        //    {
        //        一号生产线下车事件(str); /* 事件被触发 */
        //    }
        //    else
        //    {
        //        Console.WriteLine("event not fire");
        //        Console.ReadKey(); /* 回车继续 */
        //    }
        //}


        //#endregion

        //#region 二号生产线下车
        //public delegate void 二号生产线下车委托(string str);

        //public event 二号生产线下车委托 二号生产线下车事件;
        //protected virtual void On二号生产线下车事件触发(string str)
        //{
        //    if (二号生产线下车事件 != null)
        //    {
        //        二号生产线下车事件(str); /* 事件被触发 */
        //    }
        //    else
        //    {
        //        Console.WriteLine("event not fire");
        //        Console.ReadKey(); /* 回车继续 */
        //    }
        //}


        //#endregion

        //#region 出一号窑
        //public delegate void 出一号窑委托(string str);

        //public event 出一号窑委托 出一号窑事件;
        //protected virtual void On出一号窑事件触发(string str)
        //{
        //    if (出一号窑事件 != null)
        //    {
        //        出一号窑事件(str); /* 事件被触发 */
        //    }
        //    else
        //    {
        //        Console.WriteLine("event not fire");
        //        Console.ReadKey(); /* 回车继续 */
        //    }
        //}


        //#endregion

        //#region 出二号窑
        //public delegate void 出二号窑委托(string str);

        //public event 出二号窑委托 出二号窑事件;
        //protected virtual void On出二号窑事件触发(string str)
        //{
        //    if (出二号窑事件 != null)
        //    {
        //        出二号窑事件(str); /* 事件被触发 */
        //    }
        //    else
        //    {
        //        Console.WriteLine("event not fire");
        //        Console.ReadKey(); /* 回车继续 */
        //    }
        //}


        //#endregion

        //#region 出三号窑
        //public delegate void 出三号窑委托(string str);

        //public event 出三号窑委托 出三号窑事件;
        //protected virtual void On出三号窑事件触发(string str)
        //{
        //    if (出三号窑事件 != null)
        //    {
        //        出三号窑事件(str); /* 事件被触发 */
        //    }
        //    else
        //    {
        //        Console.WriteLine("event not fire");
        //        Console.ReadKey(); /* 回车继续 */
        //    }
        //}


        //#endregion

        //#region 出四号窑
        //public delegate void 出四号窑委托(string str);

        //public event 出四号窑委托 出四号窑事件;
        //protected virtual void On出四号窑事件触发(string str)
        //{
        //    if (出四号窑事件 != null)
        //    {
        //        出四号窑事件(str); /* 事件被触发 */
        //    }
        //    else
        //    {
        //        Console.WriteLine("event not fire");
        //        Console.ReadKey(); /* 回车继续 */
        //    }
        //}


        //#endregion

        //#region 错误日记
        //public delegate void 工位判断错误日记委托(string str, Exception ex);

        //public event 工位判断错误日记委托 工位判断错误日记事件;
        //protected virtual void On工位判断错误日记事件触发(string str, Exception ex)
        //{
        //    if (工位判断错误日记事件 != null)
        //    {
        //        工位判断错误日记事件(str, ex); /* 事件被触发 */
        //    }
        //    else
        //    {
        //        Console.WriteLine("event not fire");
        //        Console.ReadKey(); /* 回车继续 */
        //    }
        //}
        //#endregion

        //#region 保存变化值
        //public delegate void 保存变化值委托(string 出_出窑, string cCAR_nub, string c_Manufacture);

        //public event 保存变化值委托 保存变化值事件;
        //protected virtual void On保存变化值事件触发(string 出_出窑, string cCAR_nub, string c_Manufacture)
        //{
        //    if (保存变化值事件 != null)
        //    {
        //        保存变化值事件(出_出窑, cCAR_nub, c_Manufacture); /* 事件被触发 */
        //    }
        //    else
        //    {
        //        Console.WriteLine("event not fire");
        //        Console.ReadKey(); /* 回车继续 */
        //    }
        //}
        //#endregion

        //bool b1;
        //private string Manufacture = ConfigurationManager.AppSettings["生产企业"];
        //private string 取流路径 = ConfigurationManager.AppSettings["取流路径"];
        //private string 数据库路径 = ConfigurationManager.AppSettings["数据库路径"];
        //private string 重启路径 = ConfigurationManager.AppSettings["重启路径"];
        protected override void OnStart(string[] args)
        {
            //保存旧值 _保存旧值 = new 保存旧值();
            //重启类 _重启类 = new 重启类(重启路径);
            //b1 = true;
            //var capture = new VideoCapture(取流路径);
            ////var modelFile = "logs_2\\pb\\frozen_model.pb";
            //var graph = new TFGraph();
            //var model = File.ReadAllBytes(数据库路径);
            //graph.Import(model, "");
            //using (var frame = new Mat())
            //using (var image缩小 = new Mat())
            //using (var session = new TFSession(graph))
            //{
            //    string file = "1.jpg";
            //    int int32_这次标识 = 100;
            //    int int32_上一次标识 = 100;
            //    int int32_次数标识 = 0;
            //    int int32_上一次有用的标识 = 100;

            //    var sql_ini_出窑 = "select cCAR_nub from Car_state where cManufacture='福建省榕圣市政工程股份有限公司连江建材分公司' AND cCAR_NO='出窑图片版' order by [id] desc  limit 0,2";

            //    DataTable isqlite1_ini_出窑 = SQLiteHelper.ExecuteDataTable(sql_ini_出窑, null);

            //    String[] str_old_value_出窑 = StringHelper.dtToArr1(isqlite1_ini_出窑);

            //    int32_次数标识 = int.Parse(str_old_value_出窑[0]);

            //    int32_上一次有用的标识 = int.Parse(str_old_value_出窑[1]);

            //    while (b1)
            //    {
            //        capture.Read(frame);

            //        if (frame.Empty())
            //            break;

            //        Cv2.Resize(frame, image缩小, new Size(280, 280), 0, 0, InterpolationFlags.Linear);//缩小28*28

            //        Cv2.ImWrite(file, image缩小);

            //        var tensor = CreateTensorFromImageFile(file);

            //        var runner = session.GetRunner();

            //        runner.AddInput(graph["x_input"][0], tensor).Fetch(graph["softmax_linear/softmax_linear"][0]);

            //        var output = runner.Run();

            //        var result = output[0];

            //        var rshape = result.Shape;

            //        if (result.NumDims != 2 || rshape[0] != 1)
            //        {
            //            var shape = "";
            //            foreach (var d in rshape)
            //            {
            //                shape += $"{d} ";
            //            }
            //            shape = shape.Trim();
            //            Console.WriteLine($"Error: expected to produce a [1 N] shaped tensor where N is the number of labels, instead it produced one with shape [{shape}]");
            //            Environment.Exit(1);
            //        }

            //        bool jagged = true;

            //        var bestIdx = 0;

            //        float p = 0, best = 0;
            //        if (jagged)
            //        {
            //            var probabilities = ((float[][])result.GetValue(jagged: true))[0];
            //            double[] d = floatTodouble(probabilities);
            //            double[] retResult = Softmax(d);

            //            for (int i = 0; i < retResult.Length; i++)
            //            {
            //                if (probabilities[i] > best)
            //                {
            //                    bestIdx = i;
            //                    best = probabilities[i];
            //                }
            //            }

            //        }
            //        else
            //        {
            //            var val = (float[,])result.GetValue(jagged: false);
            //            for (int i = 0; i < val.GetLength(1); i++)
            //            {
            //                if (val[0, i] > best)
            //                {
            //                    bestIdx = i;
            //                    best = val[0, i];
            //                }
            //            }
            //        }

            //        if (bestIdx == int32_这次标识)
            //        {
            //            int32_次数标识++;
            //        }
            //        else
            //        {
            //            int32_次数标识 = 0;
            //            int32_上一次标识 = int32_这次标识;
            //            int32_这次标识 = bestIdx;
            //        }

            //        if (int32_次数标识 > 20 && int32_这次标识 != int32_上一次有用的标识)
            //        {
            //            int32_上一次有用的标识 = int32_这次标识;
            //            var isqlite_结果 = _保存旧值.Save_the_车辆状态_value("出窑图片版", int32_这次标识.ToString(), Manufacture);

            //            switch (int32_这次标识)
            //            {
            //                case 0:
            //                    On空车事件触发(Manufacture);
            //                    break;
            //                case 1:
            //                    On出一号窑事件触发(Manufacture);
            //                    break;
            //                case 2:
            //                    On出二号窑事件触发(Manufacture);
            //                    break;
            //                case 3:
            //                    On出三号窑事件触发(Manufacture);
            //                    break;
            //                case 4:
            //                    On出四号窑事件触发(Manufacture);
            //                    break;
            //                case 5:
            //                    On一号生产线上车事件触发(Manufacture);
            //                    break;
            //                case 6:
            //                    On二号生产线下车事件触发(Manufacture);
            //                    break;
            //                case 7:
            //                    On满车事件触发(Manufacture);
            //                    break;
            //            }

            //        }
            //        else
            //        {
            //            int32_次数标识 = 0;
            //        }
            //        Cv2.WaitKey(100);
            //    }
            //}

            _运行情况.insert_into_软件运行情况("开始", "出窑服务启动", Manufacture);
            _实时取流类 = new 实时取流类();
            _实时取流类.开始标识1 = true;
            _实时取流类.开始实时取流();



        }

        protected override void OnStop()
        {
            //b1 = false;



            _运行情况.insert_into_软件运行情况("停止", "出窑服务停止", Manufacture);
            _实时取流类.开始标识1 = false;


        }








        private static double[] floatTodouble(float[] probabilities)
        {
            double[] DOU = new double[8];
            for (int i = 0; i < probabilities.Length; i++)
            {
                DOU[i] = (double)probabilities[i];
            }
            return DOU;
        }

        private static double[] Softmax(double[] probabilities)
        {
            double max = 0;
            double sum = 0;
            for (int i = 0; i < 8; i++)
                if (max < probabilities[i])
                    max = probabilities[i];
            //#pragma omp parallel for  
            for (int i = 0; i < 8; i++)
            {
                probabilities[i] = Math.Exp(probabilities[i] - max);//防止数据溢出
                sum += probabilities[i];
            }
            //#pragma omp parallel for  
            for (int i = 0; i < 8; i++)
                probabilities[i] /= sum;

            return probabilities;
        }



        //private static TFTensor CreateTensorFromImageMat(Mat mat)
        //{

        //    TFGraph graph;
        //    TFOutput input, output;





        //}

        static TFTensor CreateTensorFromImageFile(string file)
        {
            var contents = File.ReadAllBytes(file);

            // DecodeJpeg uses a scalar String-valued tensor as input.
            var tensor = TFTensor.CreateString(contents);

            TFGraph graph;
            TFOutput input, output;

            // Construct a graph to normalize the image 归一化
            ConstructGraphToNormalizeImage(out graph, out input, out output);

            // Execute that graph to normalize this one image 执行图规范化这个形象
            using (var session = new TFSession(graph))
            {
                var normalized = session.Run(
                         inputs: new[] { input },
                         inputValues: new[] { tensor },
                         outputs: new[] { output });

                return normalized[0];
            }
        }


        static void ConstructGraphToNormalizeImage(out TFGraph graph, out TFOutput input, out TFOutput output)
        {
            // Some constants specific to the pre-trained model at:
            // https://storage.googleapis.com/download.tensorflow.org/models/inception5h.zip
            //
            // - The model was trained after with images scaled to 224x224 pixels.
            // - The colors, represented as R, G, B in 1-byte each were converted to
            //   float using (value - Mean)/Scale.

            const int W = 208;
            const int H = 208;
            const float Mean = 0;
            const float Scale = 1;

            graph = new TFGraph();
            input = graph.Placeholder(TFDataType.String);

            output = graph.Div(
                x: graph.Sub(
                    x: graph.ResizeBilinear(
                        images: graph.ExpandDims(
                            input: graph.Cast(
                                graph.DecodeJpeg(contents: input, channels: 3), DstT: TFDataType.Float),
                            dim: graph.Const(0, "make_batch")),
                        size: graph.Const(new int[] { W, H }, "size")),
                    y: graph.Const(Mean, "mean")),
                y: graph.Const(Scale, "scale"));
        }





    }
}
