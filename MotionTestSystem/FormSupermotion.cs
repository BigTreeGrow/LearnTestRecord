using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using thinger.AutomaticStoreMotionDAL;
using Timer = System.Windows.Forms.Timer;

namespace MotionTestSystem
{
    public partial class FormSupermotion : Form
    {
        public FormSupermotion()
        {
 
            InitializeComponent();
           
            this.Load += FormSupermotion_Load;
            //初始化定时器
            this.updateTimer.Interval = 200;
            this.updateTimer.Tick += UpdateTimer_Tick;
            this.updateTimer.Enabled = true;
            stopwatch = new Stopwatch();
            this.FormClosing += FormSupermotion_FormClosing;
        }

        private void FormSupermotion_FormClosing(object sender, FormClosingEventArgs e)
        {
            cts?.Cancel();
        }

        private void FormSupermotion_Load(object sender, EventArgs e)
        {
          
            Form_MotionFlag_Load(sender ,e);
            for (int i = 0; i < keyPostions.Length; i++)
            {
                keyPostions[i] = new KeyPostion(); // 实例化每个 KeyPostion 对象  
            }
            GTN.mc.GTN_SetAxisBand(CORE, 1, (int)50, (int)100);
            GTN.mc.GTN_SetAxisBand(CORE, 2, (int)5, (int)10);
            GTN.mc.GTN_SetAxisBand(CORE, 3, (int)5, (int)10);

            //创建自动流程线程
            cts = new CancellationTokenSource();

            Task MainTask = new Task(MainProcess, cts.Token);
       
            MainTask.Start();
        }

        //创建一个更新定时器
        private Timer updateTimer = new Timer();
        //创建单例模式对象
        private GtsMotionEx motionEx = GtsMotionEx.GetInstance();
        //创建取消线程源对象
        private CancellationTokenSource cts;
        const short CORE = 1;
        short rtn;
        public static short MAxis;
        List<Label> ListLab = null;
        public Stopwatch stopwatch;
        public static double timecal;
        public KeyPostion[] keyPostions = new KeyPostion[2];
        public static MachineState CurrrentMachineState;
        public static int CurrentCount;
        public static int TotalCount;
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            MAxis = (short)numMAxis.Value;
            double PrfPos, EncPos, PrfVel, EncVel;
            ushort mode;
            int Sts;
            uint clk;
            short current,torque;
            short rtn;
            short AxisId = MAxis;
            EncPos = motionEx.GetCmdPos(MAxis);
            txtMEncPos.Text = ((double)EncPos).ToString();
            GTN.mc.GTN_GetSts(CORE, MAxis, out Sts, 1, out clk);
            GTN.mc.GTN_GetPrfVel(CORE, MAxis, out PrfVel, 1, out clk);
            GTN.mc.GTN_GetEncVel(CORE, MAxis, out EncVel, 1, out clk);
            GTN.mc.GTN_GetPrfPos(CORE, MAxis, out PrfPos, 1, out clk);
            GTN.mc.GTN_GetEcatAxisAtlTorque(CORE, MAxis, out torque);
            GTN.mc.GTN_GetEcatAxisAtlCurrent(CORE, MAxis, out current);

            txtMPrfPos.Text = ((double)PrfPos).ToString();
            txtMPrfVel.Text = PrfVel.ToString("0");
            txtMEncPos.Text = ((double)EncPos).ToString();
            txtMEncVel.Text = EncVel.ToString("0");
            txtMSts.Text = Convert.ToString(Sts, 2);
            textBox1.Text = timecal.ToString();
            textTore.Text = torque.ToString();
            textCurrent.Text = current.ToString();

            ChangeColor(ListLab, "label20", AxisControl.mc.AxisSts(AxisId, 1));//报警
            ChangeColor(ListLab, "label22", AxisControl.mc.AxisSts(AxisId, 5));//正限位
            ChangeColor(ListLab, "label30", AxisControl.mc.AxisSts(AxisId, 6));//负限位
            ChangeColor(ListLab, "label24", AxisControl.mc.AxisSts(AxisId, 7));//平滑停止
            ChangeColor(ListLab, "label32", AxisControl.mc.AxisSts(AxisId, 8));//急停
            ChangeColor(ListLab, "label21", AxisControl.mc.AxisSts(AxisId, 9));//使能
            ChangeColor(ListLab, "label25", AxisControl.mc.AxisSts(AxisId, 10));//规划运动
            ChangeColor(ListLab, "label23", AxisControl.mc.AxisSts(AxisId, 11));//电机到位
            GTN.mc.GTN_GetEcatAxisMode(CORE, MAxis, out mode);
            if (checkBox.Checked)
            {
              
                if (mode != 10)
                {
                    rtn = GTN.mc.GTN_SetEcatAxisMode(CORE, MAxis, 10);
                }
               


            }
            else
            {

                if ((mode != 8) && (mode != 6))
                {
                    rtn = GTN.mc.GTN_SetEcatAxisMode(CORE, MAxis, 8);
                }
            }
            keyPostions[0].Xaxis_pos = (double)numericStrartPos.Value;
            keyPostions[0].Xaxis_Vel = (double)numericVel.Value;
            keyPostions[0].Xaxis_Acc = (double)numericAcc.Value;

            keyPostions[1].Xaxis_pos = (double)numericEndpos.Value;
            keyPostions[1].Xaxis_Vel = (double)numericVel.Value;
            keyPostions[1].Xaxis_Acc = (double)numericAcc.Value;
            TotalCount = (int)WhileCount.Value;
            finishcount.Text = ((int)CurrentCount).ToString();

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


            Task.Run(() =>
            {


                motionEx.motion.EcatMotionBoard.Home((EnumStageAxis)MAxis, (short)HomeMode.Value);


            });

          

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
           
            if (checkBox1.Checked)
            {
                motionEx.motion.EcatMotionBoard.MoveRelativeSync((EnumStageAxis)AxisId, (double)numPos.Value, (double)numVel.Value, (double)numAcc.Value, out short err);
            }
            else
            {
                motionEx.motion.EcatMotionBoard.MoveAbsoluteSync((EnumStageAxis)AxisId, (double)numPos.Value, (double)numVel.Value, (double)numAcc.Value, out short err);
            }
           Task.Run(() =>
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

                    timecal = elapsedMSeconds/1000;
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

        private void buttonTore_Click(object sender, EventArgs e)
        {
            short T = (short)numTore.Value;
            short rtn;

            rtn= GTN.mc.GTN_SetEcatAxisPT(CORE, MAxis, T);
            //Task.Delay(10);
           // GTN.mc.GTN_SetEcatAxisMode(CORE, MAxis, 8);


        }

        private void MainProcess()
        {
            while (!cts.IsCancellationRequested)
            {
                switch (CurrrentMachineState)
                {
                    case MachineState.Start:

                        CurrrentMachineState = MachineState.Start;
                        CurrentCount = 0;
                        CurrrentMachineState = MachineState.Producing;
                        break;
                    case MachineState.Producing:
                        int i;
                        for (i = 0; i < TotalCount; i++)
                        {
                            SingleFlow();
                            CurrentCount += 1;
                            if (CurrrentMachineState != MachineState.Producing)
                            {
                                break;
                            
                            }

                        }
                        if (CurrentCount >= TotalCount)
                        {


                            CurrrentMachineState = MachineState.End;
                        }

                        break;
                    case MachineState.Idle:

                        break;
                    case MachineState.Paused:

                        break;
                    case MachineState.Stopped:

                        break;
                    case MachineState.End:
                        Task.Delay(100);
                        break;
                    default:

                        break;
                }

            }


        }


        void SingleFlow()
        {

            bool keyposdone = false;
            bool flowstart = true;
            int flow = 0;
            while (flowstart)
            {
                switch (flow)
                {
                    case 0:
                        keyposdone = Basic_MultisAxisMove(keyPostions[0]);
                        flow = 1;
                       
                        break;
                    case 1:
                        if (keyposdone)
                        {
                            keyposdone = false;
                            flow = 2;
                        }
                        else
                        {
                            flow = flow;
                        }
                        break;
                    case 2:
                        keyposdone = Basic_MultisAxisMove(keyPostions[1]);
                        flow = 3;
                        break;
                    case 3:
                        if (keyposdone)
                        {
                            keyposdone = false;
                            flow = 8;
                        }
                        else
                        {

                            flow = flow;
                        }
                        break;
                   
                    case 8:
                        flow = 0;
                        flowstart = false;
                      
                        break;

                }

            }

        }

        bool Basic_MultisAxisMove(KeyPostion keyPostion)
        {
            bool posdone1 = false;
         
            bool Waitdone = true;
            
            motionEx.motion.EcatMotionBoard.MoveAbsoluteSync((EnumStageAxis)MAxis, keyPostion.Xaxis_pos, keyPostion.Xaxis_Vel, keyPostion.Xaxis_Acc, out short err1);
            do
            {
                posdone1 = AxisControl.mc.AxisSts(MAxis, 11);
               

                if (posdone1 )
                {
                    Waitdone = false;
                }
                else
                {
                    Waitdone = true;

                }

                Task.Delay(10);
            } while (Waitdone);


            return true;
        }

        private void StartWhile_Click(object sender, EventArgs e)
        {
            CurrrentMachineState = MachineState.Start;
        }

        private void EndWhile_Click(object sender, EventArgs e)
        {
            CurrrentMachineState = MachineState.End;

        }
    }
}
