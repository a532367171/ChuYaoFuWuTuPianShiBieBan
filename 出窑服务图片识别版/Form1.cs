using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 出窑服务图片识别版
{
    public partial class Form1 : Form
    {
        Thread vThread;
        实时取流类 _实时取流类;
        public Form1()
        {
            InitializeComponent();

             _实时取流类 = new 实时取流类();
             vThread = new Thread(new ThreadStart(_实时取流类.开始实时取流));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _实时取流类.开始标识1 = true;

            vThread.Start();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _实时取流类.开始标识1 = false;
            vThread.Join();

        }
    }
}
