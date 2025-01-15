using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using thinger.AutomaticStoreMotionControlLib;
using thinger.AutomaticStoreMotionDAL;
using thinger.AutomaticStoreMotionModels;

namespace MotionTestSystem
{
    public partial class FormHand : Form
    {
        public FormHand()
        {
            InitializeComponent();

            ///事件及参数初始化
            ///
            InitEventAndParam(this.headPanel1, motionEx.advanceParam);
            InitEventAndParam(this.headPanel2, motionEx.advanceParam);
            InitEventAndParam(this.headPanel3, motionEx.basicParam);
            InitEventAndParam(this.headPanel4, motionEx.basicParam);

            //初始化定时器
            this.updateTimer.Interval = 200;
            this.updateTimer.Tick += UpdateTimer_Tick;
            this.updateTimer.Enabled = true;

            this.FormClosing += FrmHand_FormClosing;
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateState(this.headPanel1);
            UpdateState(this.headPanel2);
            UpdateState(this.headPanel3);
            UpdateState(this.headPanel4);
            UpdateState(this.headPanel5);
        }
        private void FrmHand_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.updateTimer.Enabled = false;
        }
        //创建一个更新定时器
        private Timer updateTimer = new Timer();
        //创建单例模式对象
        private GtsMotionEx motionEx = GtsMotionEx.GetInstance();
        /// <summary>
        /// 输出字典集合
        /// </summary>
        private Dictionary<string, short> CurrentOutDic = new Dictionary<string, short>();

       /// <summary>
       /// 运动控制字典集合
       /// </summary>
        private Dictionary<string, double > CurrentMoveDic = new Dictionary<string, double>();
        private Form F1 = null;
        private Form F2 = null;
        private Form F3 = null;
        private Form F4 = null;

        private void InitEventAndParam<T>(HeadPanel headPanel,T obj)  //处理对象泛型处理
        {
            foreach (Control item in headPanel.Controls)  //找到所有control控件
            {
                if (item is Button btn)//属性为按钮
                {
                    if (btn.Tag != null && btn.Tag.ToString().Length > 0) //过滤标签来获得目标
                    {
                        string tag = btn.Tag.ToString();

                        if (tag.Contains(';'))
                        {
                            string[] tags = tag.Split(';');
                            if (tags.Length == 2)
                            {
                                btn.Click += btn_OutCtrl_Click;
                                string propertyName = tags[0];
                                string value = GetObjectPropertyValue(obj, propertyName);
                                if (value != null && value.ToString().Length > 0)
                                {
                                    if (short.TryParse(value, out short res))
                                    {
                                        if (CurrentOutDic.ContainsKey(propertyName))
                                        {
                                            CurrentOutDic[propertyName] = res;
                                        }
                                        else
                                        {
                                            CurrentOutDic.Add(propertyName, res);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            btn.Click += btn_AxisCtrl_Click;
                            string propertyName = tag;

                            string value = GetObjectPropertyValue(obj, propertyName);

                            if (value != null && value.ToString().Length > 0)
                            {
                                if (double.TryParse(value, out double res))
                                {
                                    if (CurrentMoveDic.ContainsKey(propertyName))
                                    {
                                        CurrentMoveDic[propertyName] = res;
                                    }
                                    else
                                    {
                                        CurrentMoveDic.Add(propertyName, res);
                                    }
                                }
                            }
                        }
                    }
                }    
            }
        }




        private void btn_OutCtrl_Click(object sender, EventArgs e)
        {

        }

 

        #region 轴绝对定位
        /// <summary>
        /// 轴绝对定位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AxisCtrl_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.Tag != null && btn.Tag.ToString().Length > 0)
                {
                    if (btn.Name != "btn_AutoMode" && btn.Name != "btn_HandMode")
                    {
                        if (motionEx.GetModeState())
                        {
                            HandModeVerify();
                            return;
                        }
                    }

                    string propertyName = btn.Tag.ToString();

                    if (CurrentMoveDic.ContainsKey(propertyName))
                    {
                        double pos = CurrentMoveDic[propertyName];

                        Task.Run(new Action(() =>
                        {
                            OperationResult result = new OperationResult();

                            if (propertyName.EndsWith("X"))
                            {
                                result = motionEx.MoveXAbs(pos);
                            }
                            else if (propertyName.EndsWith("Y"))
                            {
                                result = motionEx.MoveYAbs(pos);
                            }
                            else if (propertyName.EndsWith("Z"))
                            {
                                result = motionEx.MoveZAbs(pos);
                            }

                            
                        }));


                    }

                }
            }
        }
        #endregion

        #region 点动
        private void btn_JOGPos_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.Name != "btn_AutoMode" && btn.Name != "btn_HandMode")
                {
                    if (motionEx.ModeState == true)
                    {
                        HandModeVerify();
                        return;
                    }
                }
                var result = motionEx.JogMoveAxis(btn.Text);
                if (result.IsSuccess)
                {
                    ;
                }
                else
                {
                    new FrmConfirmSingle("点动操作", btn.Text + "操作失败") { TopMost = true }.Show();
                }

            }
        }

        private void btn_JOGPos_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender is Button btn)
            {
                motionEx.StopAxis(btn.Text);
            }
        }

        #endregion

        /// <summary>
        /// 手动模式通用的验证方法
        /// </summary>
        private void HandModeVerify()
        {
            DialogResult dialogResult = new FormConfirm("模式切换", "是否切换至手动模式？", "确认切换", "取消切换") { TopMost = true }.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                motionEx.ModeState = false;

                if (motionEx.ModeState)
                {
                    new FrmConfirmSingle("切换失败", "手动模式切换失败") { TopMost = true }.Show();
                }
            }
        }
        /// <summary>
        /// 设置按钮样式
        /// </summary>
        /// <param name="button">按钮对象</param>
        /// <param name="isOpen">是否为真</param>
        private void SetButtonStyle(Button button, bool isOpen)
        {
            button.BackColor = isOpen ? Color.LimeGreen : SystemColors.Control;
            button.ForeColor = isOpen ? Color.White : SystemColors.ControlText;
            button.FlatAppearance.MouseOverBackColor = isOpen ? Color.FromArgb(0, 195, 0) : Color.Gainsboro;
        }

        /// <summary>
        /// 判断是否到位
        /// </summary>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <param name="diffset"></param>
        /// <returns></returns>
        private bool Equals(double val1, double val2, double diffset = 0.1)
        {
            double diff = val1 - val2;

            return Math.Abs(diff) <= diffset;
        }


        /// <summary>
        /// 通过属性名称获取值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>返回值</returns>
        private string GetObjectPropertyValue<T>(T obj, string propertyName)
        {
            try
            {
                Type type = typeof(T);

                PropertyInfo property = type.GetProperty(propertyName);

                if (property == null) return string.Empty;

                object val = property.GetValue(obj, null);

                return val?.ToString();
            }
            catch (Exception ex)
            {

                LogHelper.Info("通过属性名称获取值", ex);
                return null;
            }
        }


        #region 实时更新

        /// <summary>
        /// 实时更新
        /// </summary>
        /// <param name="headPanel">控件</param>
        private void UpdateState(HeadPanel headPanel)
        {
            //获取输出结果
            short status = motionEx.GetOutput();

            //获取坐标
            double xPos = motionEx.GetCmdPosX();
            double yPos = motionEx.GetCmdPosY();
            double zPos = motionEx.GetCmdPosZ();


            foreach (Control item in headPanel.Controls)
            {
                if (item is Button btn)
                {
                    if (btn.Tag != null && btn.Tag.ToString().Length > 0)
                    {
                        string tag = btn.Tag.ToString();

                        if (tag.Contains(';'))
                        {
                            string[] tags = tag.Split(';');

                            if (tags.Length == 2)
                            {

                                string propertyName = tags[0];

                                if (CurrentOutDic.ContainsKey(propertyName))
                                {
                                    short index = CurrentOutDic[propertyName];

                                    bool val = (status & Convert.ToInt32(Math.Pow(2, index - 1))) != 0;

                                    SetButtonStyle(btn, tags[1] == "1" ? val : !val);

                                }
                            }
                        }

                        else
                        {
                            string propertyName = tag;

                            if (CurrentMoveDic.ContainsKey(propertyName))
                            {
                                double value = CurrentMoveDic[propertyName];

                                if (propertyName.EndsWith("X"))
                                {
                                    SetButtonStyle(btn, Equals(value, xPos));
                                }
                                else if (propertyName.EndsWith("Y"))
                                {
                                    SetButtonStyle(btn, Equals(value, yPos));
                                }
                                else if (propertyName.EndsWith("Z"))
                                {
                                    SetButtonStyle(btn, Equals(value, zPos));
                                }
                            }

                        }
                    }
                }
            }
        }
        #endregion

        private void btn_SingleAxis_Click(object sender, EventArgs e)
        {
            

            if (F1 == null || F1.IsDisposed)
            {
                F1 = new FormSupermotion();
                F1.Show();//未打开，直接打开。
            }
            else
            {
                F1.Activate();//已打开，获得焦点，置顶。
            }


           // Form frm = new FormSupermotion() { TopMost = true };
           // frm.Show();
        }

        private void btn_SinglePVT_Click(object sender, EventArgs e)
        {
           

            if (F2 == null || F2.IsDisposed)
            {
                F2 = new FormPVTmotion();
                F2.Show();//未打开，直接打开。
            }
            else
            {
                F2.Activate();//已打开，获得焦点，置顶。
            }



        }

        private void btn_LightOn_Click(object sender, EventArgs e)
        {
            

            if (F3 == null || F3.IsDisposed)
            {
                F3 = new ForminterCrd();
                F3.Show();//未打开，直接打开。
            }
            else
            {
                F3.Activate();//已打开，获得焦点，置顶。
            }


        }

        private void btn_LightOff_Click(object sender, EventArgs e)
        {
            if (F4 == null || F4.IsDisposed)
            {
                F4 = new FormAutoProgram();
                F4.Show();//未打开，直接打开。
            }
            else
            {
                F4.Activate();//已打开，获得焦点，置顶。
            }




        }
    }
}
