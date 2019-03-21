using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TensorFlow;

namespace 出窑服务图片识别版
{
    class 图片识别类
    {
        private string 数据库路径 = ConfigurationManager.AppSettings["数据库路径"];

        TFGraph graph;

        private string 临时图片路径 = ConfigurationManager.AppSettings["临时图片路径"];

        public 图片识别类()
        {
            graph = new TFGraph();
            var model = File.ReadAllBytes(数据库路径);
            graph.Import(model, "");
        }

        public Int32 识别方法(out double OU)
        {
            using (var session = new TFSession(graph))
            {
                var tensor = CreateTensorFromImageFile(临时图片路径);

                var runner = session.GetRunner();

                runner.AddInput(graph["x_input"][0], tensor).Fetch(graph["softmax_linear/softmax_linear"][0]);

                var output = runner.Run();

                var result = output[0];

                var rshape = result.Shape;

                if (result.NumDims != 2 || rshape[0] != 1)
                {
                    var shape = "";
                    foreach (var d in rshape)
                    {
                        shape += $"{d} ";
                    }
                    shape = shape.Trim();
                    Console.WriteLine($"Error: expected to produce a [1 N] shaped tensor where N is the number of labels, instead it produced one with shape [{shape}]");
                    Environment.Exit(1);
                }

                bool jagged = true;

                var bestIdx = 0;

                float p = 0;
                double best = 0;
                if (jagged)
                {
                    var probabilities = ((float[][])result.GetValue(jagged: true))[0];
                    double[] d = floatTodouble(probabilities);
                    double[] retResult = Softmax(d);

                    for (int i = 0; i < retResult.Length; i++)
                    {
                        if (retResult[i] > best)
                        {
                            bestIdx = i;
                            //best = probabilities[i];
                            best = retResult[i];

                        }
                    }

                }
                else
                {
                    var val = (float[,])result.GetValue(jagged: false);
                    for (int i = 0; i < val.GetLength(1); i++)
                    {
                        if (val[0, i] > best)
                        {
                            bestIdx = i;
                            best = val[0, i];
                        }
                    }
                }
                OU = best;
                return bestIdx;
            }


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
