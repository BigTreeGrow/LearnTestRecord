using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotionTestSystem
{
    public partial class FormMonitor : Form
    {
        public FormMonitor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add(DateTime.Now.ToString());
        }
    }
}
