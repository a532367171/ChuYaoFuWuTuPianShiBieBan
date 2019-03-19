using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 出窑服务图片识别版
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            实时取流类 _实时取流类 = new 实时取流类();
            _实时取流类.开始标识1 = true;
            _实时取流类.开始实时取流();
        }
    }
}
