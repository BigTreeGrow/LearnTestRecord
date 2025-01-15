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
    public enum MachineState
    {
        Idle=1,
        Start,
        Producing,
        Paused,

        Stopped,
        End
    }
    public partial class FormAutoProgram : Form
    {
        public FormAutoProgram()
        {
            InitializeComponent();
            this.Load += FormAutoProgram_Load;
            //初始化定时器
            this.updateTimer.Interval = 200;
            this.updateTimer.Tick += UpdateTimer_Tick;
            this.updateTimer.Enabled = true;
            stopwatch = new Stopwatch();
            this.FormClosing += FormAutoProgram_FormClosing;
        }

        private void FormAutoProgram_FormClosing(object sender, FormClosingEventArgs e)
        {
            cts?.Cancel();
        }

        private void FormAutoProgram_Load(object sender, EventArgs e)
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
            for (int i = 0; i < keyPostions.Length; i++)
            {
                keyPostions[i] = new KeyPostion(); // 实例化每个 KeyPostion 对象  
            }

            //创建自动流程线程
            cts = new CancellationTokenSource();

            Task MainTask = new Task(MainProcess, cts.Token);
            //轴误差带创建
            GTN.mc.GTN_SetAxisBand(CORE, 1, (int)5, (int)10);
            GTN.mc.GTN_SetAxisBand(CORE, 2, (int)5, (int)10);
            GTN.mc.GTN_SetAxisBand(CORE, 3, (int)5, (int)10);
            GTN.mc.GTN_ClrSts(CORE, 1, 1);
            GTN.mc.GTN_ClrSts(CORE, 2, 1);
            GTN.mc.GTN_ClrSts(CORE, 3, 1);
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
        public Stopwatch stopwatch;
        public static double timecal;
        public static MachineState LastMachineState;
        public static MachineState CurrrentMachineState;
        public static MachineState NextMachineState;

        public KeyPostion[] keyPostions = new KeyPostion[4];
        //创建暂停上升沿的缓存值
        private bool PauseRiseCache = false;

        //创建暂停下降沿的缓存值
        private bool PauseFallCache = true;
        public static bool PVTEnable;
        public static bool singleflowfinish;
        public static int  CurrentCount;
        public static int TotalCount;


        List<Label> ListLab = null;
        private void label3_Click(object sender, EventArgs e)
        {

        }

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
            finishcount.Text = ((int)CurrentCount).ToString();
            TotalCount = (int)Totalcount.Value;
            short AxisId = MAxis;
            ChangeColor(ListLab, "label20", AxisControl.mc.AxisSts(AxisId, 1));//报警
            ChangeColor(ListLab, "label22", AxisControl.mc.AxisSts(AxisId, 5));//正限位
            ChangeColor(ListLab, "label30", AxisControl.mc.AxisSts(AxisId, 6));//负限位
            ChangeColor(ListLab, "label24", AxisControl.mc.AxisSts(AxisId, 7));//平滑停止
            ChangeColor(ListLab, "label32", AxisControl.mc.AxisSts(AxisId, 8));//急停
            ChangeColor(ListLab, "label21", AxisControl.mc.AxisSts(AxisId, 9));//使能
            ChangeColor(ListLab, "label25", AxisControl.mc.AxisSts(AxisId, 10));//规划运动
            ChangeColor(ListLab, "label23", AxisControl.mc.AxisSts(AxisId, 11));//电机到位

            //位置和速度更新
            keyPostions[0].Xaxis_pos = (double)numericUpXpos.Value;
            keyPostions[0].Yaxis_pos = (double)numericUpYpos.Value;
            keyPostions[0].Zaxis_pos = (double)numericUpZpos.Value;

            keyPostions[1].Xaxis_pos = (double)numericUpXpos1.Value;
            keyPostions[1].Yaxis_pos = (double)numericUpYpos1.Value;
            keyPostions[1].Zaxis_pos = (double)numericUpZpos1.Value;

            keyPostions[2].Xaxis_pos = (double)numericUpXpos2.Value;
            keyPostions[2].Yaxis_pos= (double)numericUpYpos2.Value;
            keyPostions[2].Zaxis_pos= (double)numericUpZpos2.Value;

            keyPostions[3].Xaxis_pos = (double)numericUpXpos3.Value;
            keyPostions[3].Yaxis_pos = (double)numericUpYpos3.Value;
            keyPostions[3].Zaxis_pos = (double)numericUpZpos3.Value;
  
                keyPostions[0].Xaxis_Vel = (double)numVel1.Value;
                keyPostions[0].Xaxis_Acc = (double)numAcc1.Value;

                keyPostions[0].Yaxis_Vel = (double)numVel2.Value;
                keyPostions[0].Yaxis_Acc = (double)numAcc2.Value;
                keyPostions[0].Zaxis_Vel = (double)numVel3.Value;
                keyPostions[0].Zaxis_Acc = (double)numAcc3.Value;

        

            if (PVTEnable)
            {
                keyPostions[0].Xaxis_Vel = (double)numericUpDown3.Value;
                keyPostions[0].Yaxis_Vel = (double)numericUpDown3.Value;
                keyPostions[0].Zaxis_Vel = (double)numericUpDown3.Value;

                keyPostions[0].Xaxis_Acc = (double)numericUpDown2.Value;
                keyPostions[0].Yaxis_Acc = (double)numericUpDown2.Value;
                keyPostions[0].Zaxis_Acc = (double)numericUpDown2.Value;

                keyPostions[0].Xaxis_J = (double)numJ.Value;
                keyPostions[0].Yaxis_J = (double)numJ.Value;
                keyPostions[0].Zaxis_J = (double)numJ.Value;

            }
            keyPostions[1].Xaxis_Vel = keyPostions[0].Xaxis_Vel;
            keyPostions[1].Xaxis_Acc = keyPostions[0].Xaxis_Acc;
            keyPostions[1].Xaxis_J = keyPostions[0].Xaxis_J;

            keyPostions[2].Xaxis_Vel = keyPostions[0].Xaxis_Vel;
            keyPostions[2].Xaxis_Acc = keyPostions[0].Xaxis_Acc;
            keyPostions[2].Xaxis_J = keyPostions[0].Xaxis_J;

            keyPostions[3].Xaxis_Vel = keyPostions[0].Xaxis_Vel;
            keyPostions[3].Xaxis_Acc = keyPostions[0].Xaxis_Acc;
            keyPostions[3].Xaxis_J = keyPostions[0].Xaxis_J;

            keyPostions[1].Yaxis_Vel = keyPostions[0].Yaxis_Vel;
            keyPostions[1].Yaxis_Acc = keyPostions[0].Yaxis_Acc;
            keyPostions[1].Yaxis_J = keyPostions[0].Yaxis_J;

            keyPostions[2].Yaxis_Vel = keyPostions[0].Yaxis_Vel;
            keyPostions[2].Yaxis_Acc = keyPostions[0].Yaxis_Acc;
            keyPostions[2].Yaxis_J = keyPostions[0].Yaxis_J;

            keyPostions[3].Yaxis_Vel = keyPostions[0].Yaxis_Vel;
            keyPostions[3].Yaxis_Acc = keyPostions[0].Yaxis_Acc;
            keyPostions[3].Yaxis_J = keyPostions[0].Yaxis_J;

            keyPostions[1].Zaxis_Vel = keyPostions[0].Zaxis_Vel;
            keyPostions[1].Zaxis_Acc = keyPostions[0].Zaxis_Acc;
            keyPostions[1].Zaxis_J = keyPostions[0].Zaxis_J;

            keyPostions[2].Zaxis_Vel = keyPostions[0].Zaxis_Vel;
            keyPostions[2].Zaxis_Acc = keyPostions[0].Zaxis_Acc;
            keyPostions[2].Zaxis_J = keyPostions[0].Zaxis_J;

            keyPostions[3].Zaxis_Vel = keyPostions[0].Zaxis_Vel;
            keyPostions[3].Zaxis_Acc = keyPostions[0].Zaxis_Acc;
            keyPostions[3].Zaxis_J = keyPostions[0].Zaxis_J;


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
                        int i ;
                        for (i = 0; i < TotalCount; i++)
                        {
                            SingleFlow();
                            CurrentCount += 1;

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

        //各轴执行定位 普通模式
        bool Basic_MultisAxisMove(KeyPostion keyPostion)
        {
            bool posdone1 = false;
            bool posdone2 = false;
            bool posdone3 = false;
            bool Waitdone = true;
         //   motionEx.motion.EcatMotionBoard.MoveAbsoluteSync((EnumStageAxis)1, keyPostion.Xaxis_pos, keyPostion.Xaxis_Vel, keyPostion.Xaxis_Acc, out short err);
            motionEx.motion.EcatMotionBoard.MoveAbsoluteSync((EnumStageAxis)2, keyPostion.Yaxis_pos, keyPostion.Yaxis_Vel, keyPostion.Yaxis_Acc, out short err1);
            motionEx.motion.EcatMotionBoard.MoveAbsoluteSync((EnumStageAxis)3, keyPostion.Zaxis_pos, keyPostion.Zaxis_Vel, keyPostion.Zaxis_Acc, out short err2);

            do
            {
                posdone1 = AxisControl.mc.AxisSts(1, 11);
                posdone1 = true;
                posdone2 = AxisControl.mc.AxisSts(2, 11);
                posdone3 = AxisControl.mc.AxisSts(3, 11);

                if (posdone1 && posdone2 && posdone3)
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
                        singleflowfinish = false;
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
                            flow = 4;
                        }
                        else
                        {

                            flow = flow;
                        }
                        break;
                    case 4:
                        keyposdone = Basic_MultisAxisMove(keyPostions[2]);
                        flow = 5;
                        break;
                    case 5:
                        if (keyposdone)
                        {
                            keyposdone = false;
                            flow = 6;
                        }
                        else
                        {

                            flow = flow;
                        }
                        break;
                    case 6:
                        keyposdone = Basic_MultisAxisMove(keyPostions[3]);
                        flow = 7;
                        break;
                    case 7:
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
                        singleflowfinish = true;
                        break;

                }
            
            }
          
        }

        /// <summary>
        /// 检测沿信号
        /// </summary>
        /// <param name="current">当前值</param>
        /// <param name="cache">缓存值</param>
        /// <param name="isRiseOrFall">上升沿或下降沿，True为上升沿，False为下降沿</param>
        /// <returns>是否检测到沿信号</returns>
        private bool CheckEdgeSingal(bool current, ref bool cache, bool isRiseOrFall = true)
        {
            if (isRiseOrFall)
            {
                if (current && !cache)
                {
                    cache = current;
                    return true;
                }
                else
                {
                    cache = current;
                    return false;
                }
            }
            else
            {
                if (!current && cache)
                {
                    cache = current;
                    return true;
                }
                else
                {
                    cache = current;
                    return false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CurrrentMachineState = MachineState.Start;



        }
    }
    public class KeyPostion
    {
        public double Xaxis_pos{ get; set; }
        public double Yaxis_pos { get; set; }
        public double Zaxis_pos { get; set; }

        public double Xaxis_Vel { get; set; }
        public double Yaxis_Vel { get; set; }
        public double Zaxis_Vel { get; set; }

        public double Xaxis_Acc { get; set; }
        public double Yaxis_Acc { get; set; }
        public double Zaxis_Acc { get; set; }

        public double Xaxis_J { get; set; }
        public double Yaxis_J{ get; set; }
        public double Zaxis_J { get; set; }



    }


}
