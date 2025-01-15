using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using thinger.AutomaticStoreMotionDAL;

namespace MotionTestSystem
{
    public partial class ForminterCrd : Form
    {
        public ForminterCrd()
        {
            InitializeComponent();
            this.updateTimer.Interval = 200;
            this.updateTimer.Tick += UpdateTimer_Tick;
            this.updateTimer.Enabled = true;
        }
        short rtn;
        short CORE = 1;
        short crd = 1;
        short fifo = 0;
        //创建一个更新定时器
        private Timer updateTimer = new Timer();
        //创建单例模式对象
        private GtsMotionEx motionEx = GtsMotionEx.GetInstance();

        public static short MAxis1;
        public static short MAxis2;

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            short run;
            int Space, pSeg;
            double[] Crdpos = new double[4];
            double crdvel;
            MAxis1 = (short)numMAxis.Value;
            MAxis2 = (short)numMAxis2.Value;
            GTN.mc.GTN_CrdSpace(CORE, crd, out Space, fifo);
            GTN.mc.GTN_CrdStatus(CORE, crd, out run, out pSeg, fifo);
            //GTN.mc.GTN_GetCrdPos(CORE,crd, out Crdpos[0]);
            GTN.mc.GTN_GetCrdVel(CORE, crd, out crdvel);

            label1.Text = string.Format("插补状态：{0}", run);
            label2.Text = string.Format("剩余FIFO：{0}", Space);
            label3.Text = string.Format("完成数：{0}", pSeg);
            label4.Text = string.Format("插补速度：{0}", Math.Round(crdvel, 2));
            label5.Text = string.Format("A:{0}", Math.Round(Crdpos[3]));
            label6.Text = string.Format("Y:{0}", Math.Round(Crdpos[1]));
            label7.Text = string.Format("Z:{0}", Math.Round(Crdpos[2]));
            label8.Text = string.Format("X:{0}", Math.Round(Crdpos[0]));
        }

        private void btnCreatCrd_Click(object sender, EventArgs e)
        {
            bool rtn;
            rtn = motionEx.motion.EcatMotionBoard.Set2DCoordinate(500, 5, MAxis1, MAxis2, 0, 0);
            if (rtn)
            {
                MessageBox.Show("坐标系建立", "成功", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("坐标系建立", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnStartcrd_Click(object sender, EventArgs e)
        {
            Int32[,] positions = new Int32[4, 2];

            // 填充数据  
            positions[0, 0] = 100000; positions[0, 1] = 0; // 组 1  
            positions[1, 0] = 100000; positions[1, 1] = 100000; // 组 2  
            positions[2, 0] = 0; positions[2, 1] = 100000; // 组 3  
            positions[3, 0] = 0; positions[3, 1] = 0; // 组 4  


            motionEx.motion.EcatMotionBoard.ClearCrdFifo(CORE,crd,0);

            // 使用循环将数据传递给 lnxy 函数  
            for (int i = 0; i < positions.GetLength(0); i++) // 遍历每一组  
            {
                Int32  x = positions[i, 0]; // 获取 X 值  
                Int32 y = positions[i, 1]; // 获取 Y 值  
                motionEx.motion.EcatMotionBoard.LnXYDataWrite(crd, (Int32)x, (Int32)y, 100, 0.1);

            } 


        

            motionEx.motion.EcatMotionBoard.GetCrdSpace(crd, out Int32 pSpace);

            motionEx.motion.EcatMotionBoard.StartCrdMove();






        }
    }
}
