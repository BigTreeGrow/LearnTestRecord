using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using thinger.AutomaticStoreMotionControlLib;

namespace MotionTestSystem
{
   ///所有窗体的枚举
   ///<remarks ></remarks>
   ///预留⑩个窗体为固定窗体


    public enum FromName
    {
        实时监控,
        手动操作 = 10,
        报警记录,
        历史记录,
        用户管理,
            设置,
            退出系统
    }




    public partial class FrmMain : Form
    {


        public FrmMain()
        {
            InitializeComponent();
            NaviButtonBind();
            NaviButtonInit();
            Program.motion.LoadParam();

        }
        /// <summary>
        /// 导航按钮事件绑定
        /// </summary>
        private void NaviButtonBind()
        {
            foreach (var item in this.panelTop.Controls)
            {
                if (item is NaviButton navi)
                {

                    navi.ClientEvent += CommonNaviButton_ClickEvent;
                }
            
            
            }
        
        }
        /// <summary>
        /// 导航按钮初始化
        /// </summary>
        private void NaviButtonInit()
        {
            foreach (var item in this.panelTop.Controls)
            {
                if (item is NaviButton navi1)
                {
                    navi1.Isactive = false;
                }

            }

        }

        private void CommonNaviButton_ClickEvent(object sender, EventArgs e)
        {
            if (sender is NaviButton navi )
            {
                FromName fromName =new FromName();
                if (Enum.TryParse(navi.NaviName, out fromName))
                {
                    OpenWindow(fromName);

                    foreach (var item in this.panelTop.Controls)
                    {
                        if (item is NaviButton navi1)
                        {
                            navi1.Isactive = false;
                        }
                    
                    }
                    navi.Isactive = true;


                }



            }
        
       
        }
        private void OpenWindow(FromName fromName)
        {
            bool isfind = false;
            int total = this.Mainpanel.Controls.Count;
            int CloseCount = 0;




            for (int i= 0;i< total; i++)
            {
                Control ct = this.Mainpanel.Controls[i - CloseCount];
                if (ct is Form form)
                {    ///此窗体为需要窗体
                    if (form.Text == fromName.ToString())
                    {
                        form.BringToFront();
                        isfind = true;

                    }
                    else if (isFixedForm(form.Text))
                    {

                        form.SendToBack();
                    }
                    else
                    {
                        form.Close();
                        CloseCount = CloseCount + 1;
                    }
                
                }
            
            
            }

            if (isfind == false)
            {
                Form frm = null;
                switch (fromName)
                {
                    case FromName.实时监控:
                        frm = new FormMonitor();
                        break;
                    case FromName.历史记录 :
                        frm = new FormHistory ();
                        break;
                    case FromName.手动操作 :
                        frm = new FormHand();
                        break;
                    case FromName.报警记录 :
                        frm = new FormSysLog ();
                        break;
                    case FromName.用户管理 :
                        frm = new FormUserManger();
                        break;
                    case FromName.设置 :
                        frm = new FormParamSet();
                        break;
                    case FromName.退出系统 :
                     
                        break;

                }
                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Dock = DockStyle.Fill;
                frm.Parent = this.Mainpanel;
                frm.BringToFront();
                frm.Show();

            }
        
        }

        /// <summary>
        /// 是否为固定窗体的判断
        /// </summary>
        /// <param name="fromName"></param>
        /// <returns></returns>
        private bool isFixedForm(string fromName)
        {
            var list = Enum.GetNames(typeof(FromName)).Where(c => (int)Enum.Parse(typeof(FromName), c, true) < 10).ToList();
            return list.Contains(fromName.ToString());
        }

        #region 实现鼠标移动窗体

        Point mPoint;

        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint = new Point(e.X, e.Y);
        }

        private void FrmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);

            }
        }


        #endregion

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
