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
using System.Diagnostics;


namespace MotionTestSystem
{
    public partial class FormPVTmotion : Form
    {
        public FormPVTmotion()
        {
 
            InitializeComponent();
           /* ListLab = new List<Label>();
            ListLab.Clear();
            foreach (var cs in groupBox2.Controls)
            {
                if (cs.GetType() == typeof(Label))
                {
                    ListLab.Add((Label)cs);
                }
            }*/
            this.Load += FormSupermotion_Load;
            //初始化定时器
            this.updateTimer.Interval = 200;
            this.updateTimer.Tick += UpdateTimer_Tick;
            this.updateTimer.Enabled = true;
            stopwatch = new Stopwatch();
        }

        private void FormSupermotion_Load(object sender, EventArgs e)
        {
            Form_MotionFlag_Load(sender ,e);
        }

        //创建一个更新定时器
        private Timer updateTimer = new Timer();
        //创建单例模式对象
        private GtsMotionEx motionEx = GtsMotionEx.GetInstance();
        const short CORE = 1;
        short rtn;
        public static short MAxis;
        public Stopwatch stopwatch;
        public  static double timecal;



        List<Label> ListLab = null;

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            MAxis = (short)numMAxis.Value;
            double PrfPos, EncPos, PrfVel, EncVel;
            ushort mode;
            int Sts;
            uint clk;
            EncPos = motionEx.GetCmdPos(MAxis);
            txtMEncPos.Text = ((double)EncPos).ToString();
            GTN.mc.GTN_GetSts(CORE, MAxis, out Sts, 1, out clk);
            GTN.mc.GTN_GetPrfVel(CORE, MAxis, out PrfVel, 1, out clk);
            GTN.mc.GTN_GetEncVel(CORE, MAxis, out EncVel, 1, out clk);
            GTN.mc.GTN_GetPrfPos(CORE, MAxis, out PrfPos, 1, out clk);
            txtMPrfPos.Text = ((double)PrfPos).ToString();
            txtMPrfVel.Text = PrfVel.ToString("0");
            txtMEncPos.Text = ((double)EncPos).ToString();
            txtMEncVel.Text = EncVel.ToString("0");
            txtMSts.Text = Convert.ToString(Sts, 2);
            textTime.Text = timecal.ToString();
            short AxisId = MAxis;
            ChangeColor(ListLab, "label20", AxisControl.mc.AxisSts(AxisId, 1));//报警
            ChangeColor(ListLab, "label22", AxisControl.mc.AxisSts(AxisId, 5));//正限位
            ChangeColor(ListLab, "label30", AxisControl.mc.AxisSts(AxisId, 6));//负限位
            ChangeColor(ListLab, "label24", AxisControl.mc.AxisSts(AxisId, 7));//平滑停止
            ChangeColor(ListLab, "label32", AxisControl.mc.AxisSts(AxisId, 8));//急停
            ChangeColor(ListLab, "label21", AxisControl.mc.AxisSts(AxisId, 9));//使能
            ChangeColor(ListLab, "label25", AxisControl.mc.AxisSts(AxisId, 10));//规划运动
            ChangeColor(ListLab, "label23", AxisControl.mc.AxisSts(AxisId, 11));//电机到位



        }

        private void button2_Click(object sender, EventArgs e)
        {
            rtn = GTN.mc.GTN_Stop(CORE, 1 << MAxis - 1, 1 << MAxis - 1);
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (motionEx.motion.EcatMotionBoard.Get_AxisSts_Enable((EnumStageAxis)MAxis))
            {
                rtn = GTN.mc.GTN_AxisOff(CORE, MAxis);
                
            }
            else
            {
                rtn = GTN.mc.GTN_AxisOn(CORE, MAxis);
                
            }
        }

        private void btnStartHome_Click(object sender, EventArgs e)
        {

            motionEx.motion.EcatMotionBoard.Home((EnumStageAxis)MAxis, (short)HomeMode.Value);

        }

        private void btnStopHome_Click(object sender, EventArgs e)
        {
            rtn = GTN.mc.GTN_StopEcatHoming(CORE, MAxis);
            rtn = GTN.mc.GTN_Stop(CORE, 1 << MAxis - 1, 1 << MAxis - 1);
            rtn = GTN.mc.GTN_SetHomingMode(CORE, MAxis, 8);

        }

        private void ChangeColor(List<Label> list, string name, bool bo)
        {
            foreach (var cs in list)
            {
                if (cs.Name == name)
                {
                    if (bo)
                    {
                        cs.BackColor = Color.Red;
                    }
                    else
                    {
                        cs.BackColor = Color.Lime;
                    }
                }
            }
        }
        private void Form_MotionFlag_Load(object sender, EventArgs e)
        {
            ListLab = new List<Label>();
            ListLab.Clear();
            foreach (var cs in groupBox2.Controls)
            {
                if (cs.GetType() == typeof(Label))
                {
                    ListLab.Add((Label)cs);
                }
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            rtn = GTN.mc.GTN_SetAxisBand(CORE, MAxis, (int)numericUpDown2.Value, (int)numericUpDown3.Value);
            if (rtn == 0)
            {
                MessageBox.Show("误差带设置", "成功", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GTN.mc.GTN_ClrSts(CORE, Convert.ToInt16(MAxis), 1);
                ChangeColor(ListLab, "labelErrbind", false);
            }
            else
            {
                MessageBox.Show("误差带设置", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GTN.mc.GTN_ClrSts(CORE, Convert.ToInt16(MAxis), 1);
                ChangeColor(ListLab, "labelErrbind", true);

            }


        }

        private void btnStartMove_Click(object sender, EventArgs e)
        {

           short  AxisId = MAxis;
           
            
                motionEx.motion.EcatMotionBoard.S_Movetion((EnumStageAxis)AxisId, (double)numPos.Value, (double)numVel.Value, (double)numAcc.Value, (double)numJ.Value);
                Task.Run( ()=>
                {

                    if (AxisControl.mc.AxisSts(AxisId, 11) == false)
                    {
                        stopwatch.Start();
                    }
                    while (!AxisControl.mc.AxisSts(AxisId, 11)) ;

                    if (AxisControl.mc.AxisSts(AxisId, 11) == true)
                    {
                        stopwatch.Stop();
                        double elapsedMSeconds = stopwatch.Elapsed.TotalMilliseconds; // 获取经过的时间（毫秒）  
                        
                        timecal = elapsedMSeconds / 1000;
                        stopwatch.Reset(); // 重置计时器以便下次使用  

                    }


                   });
          

        }

        private void button5_Click(object sender, EventArgs e)
        {
           
            rtn = GTN.mc.GTN_ClrSts(CORE, MAxis, 1);
        }

        private void btnJogPos_MouseDown(object sender, MouseEventArgs e)
        {
            motionEx.motion.EcatMotionBoard.JogMove((EnumStageAxis)MAxis, (double)numVel.Value, (double)numAcc.Value, out short err);
        }

        private void btnJogPos_MouseUp(object sender, MouseEventArgs e)
        {
            motionEx.motion.EcatMotionBoard.StopMotion((EnumStageAxis)MAxis);

        }

        private void btnJogNei_MouseDown(object sender, MouseEventArgs e)
        {
            motionEx.motion.EcatMotionBoard.JogMove((EnumStageAxis)MAxis, -(double)numVel.Value, (double)numAcc.Value, out short err);
            ;
        }

        private void btnJogNei_MouseUp(object sender, MouseEventArgs e)
        {
            motionEx.motion.EcatMotionBoard.StopMotion((EnumStageAxis)MAxis);

        }

        private void numSm_ValueChanged(object sender, EventArgs e)
        {

        }

        public double TimeCal(bool btn)
        {
            if (!btn) // btn 为假时开始计时  
            {
                stopwatch.Start();
                return 0; // 返回0表示计时尚未结束  
            }
            else // btn 为真时结束计时  
            {
                stopwatch.Stop();
                double elapsedMSeconds = stopwatch.Elapsed.Milliseconds; // 获取经过的时间（毫秒）  
                stopwatch.Reset(); // 重置计时器以便下次使用  
                return elapsedMSeconds/1000; // 返回计时结果  
            }
        }



    }
}
