using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using thinger.AutomaticStoreMotionModels;
using thinger.AutomaticStoreMotionDAL;

namespace MotionTestSystem
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //链接数据库
            SqlSugarService.SetConnectionString("Data Source=" + Application.StartupPath + "\\DataBase\\AutomaticStoreMotion;Pooling=true;FailIfMissing=false");
            //打开登录界面并保持最前
            FormLogin objFrm = new FormLogin() { TopMost = true };



            DialogResult dr = objFrm.ShowDialog();

            if (dr == DialogResult.OK)
            {
                Application.Run(new FrmMain());
            }
            else
            {
                Application.Exit();
            }
        }
        //创建全局用户对象
        public static SysAdmin sysAdmin;
    }
}
