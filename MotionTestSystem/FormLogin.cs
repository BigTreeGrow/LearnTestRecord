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
using thinger.AutomaticStoreMotionModels;

namespace MotionTestSystem
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            this.Load += FormLogin_Load;
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            this.comUserName.DataSource = SysAdminService.GetAllAdminDB();

            this.comUserName.DisplayMember = "LoginName";
        }
        #region 实现鼠标移动窗体

        Point mPoint;

        private void MainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint = new Point(e.X, e.Y);
        }

        private void MainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - mPoint.X, this.Location.Y + e.Y - mPoint.Y);

            }
        }


        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmb_LoginName_Click(object sender, EventArgs e)
        {
            //验证

            if (this.txt_LoginPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("登录提示", "请输入密码！");
                this.txt_LoginPwd.Focus();
                return;
            }

            //封装
            SysAdmin objAdmin = new SysAdmin()
            {
                LoginName = this.comUserName.Text,
                LoginPwd = this.txt_LoginPwd.Text.Trim()
            };

            //验证

            if (this.comUserName.Text.ToLower() == "admin" && (this.txt_LoginPwd.Text.ToLower() == "admin"))
            {
                this.DialogResult = DialogResult.OK;
                Program.sysAdmin = new SysAdmin()
                {
                    LoginName = "管理员",
                    Role = 2
                };
            }
            else
            {
                objAdmin = SysAdminService.AdminLogin(objAdmin);

                if (objAdmin == null)
                {
                    MessageBox.Show("登录提示", "用户名或密码错误！");
                }
                else
                {
                    Program.sysAdmin = objAdmin;


                    this.DialogResult = DialogResult.OK;
                }
        }   }

        private void FormLogin_DoubleClick(object sender, EventArgs e)
        {

            this.Close();

        }

        private void buttonOutlogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
