using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotionTestSystem
{
    public partial class FrmConfirmSingle : Form
    {
        public FrmConfirmSingle()
        {
            InitializeComponent();
            SetToolTip();
        }


        /// <summary>
        /// 构造方法修改文本内容
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="message">消息内容</param>
        /// <param name="okMsg">OK按钮</param>
        /// <param name="cancelMsg">取消按钮</param>
        public FrmConfirmSingle(string title, string message, string okMsg = "好的！,我已知晓！") : this()
        {
            this.lbl_Title.Text = title;
            this.lbl_Message.Text = message;
            this.btn_OK.Text = okMsg;
        }





        #region 实现窗体拖动

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        #endregion

        #region 绘制一条边线
        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            Rectangle rectangle = new Rectangle(0, 0, this.MainPanel.Width - 1, this.MainPanel.Height - 1);

            graphics.DrawRectangle(new Pen(this.TopPanel.BackColor), rectangle);
        }
        #endregion

        #region SetToolTip

        private void SetToolTip()
        {
            //定义ToolTip对象
            ToolTip toolTip = new ToolTip();
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 100;
            toolTip.ReshowDelay = 500;
            toolTip.ShowAlways = true;
            toolTip.SetToolTip(this.pic_min, "最小化");
            toolTip.SetToolTip(this.pic_Exit, "关闭");
        }

        #endregion

        #region 关闭及最小化

        private void pic_Exit_MouseEnter(object sender, EventArgs e)
        {
            this.pic_Exit.Image = Properties.Resources.closehover;
        }

        private void pic_Exit_MouseLeave(object sender, EventArgs e)
        {
            this.pic_Exit.Image = Properties.Resources.close;
        }

        private void pic_min_MouseEnter(object sender, EventArgs e)
        {
            this.pic_min.Image = Properties.Resources.minhover;
        }

        private void pic_min_MouseLeave(object sender, EventArgs e)
        {
            this.pic_min.Image = Properties.Resources.min;
        }


        #endregion

        #region 按钮操作
        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void pic_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pic_min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        #endregion
    }
}
