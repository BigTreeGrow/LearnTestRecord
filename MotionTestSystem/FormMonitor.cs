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

        /// <summary>
        /// 添加日志信息
        /// </summary>
        /// <param name="index">日志等级</param>
        /// <param name="log">日志信息</param>
        public void AddLog(int index, string log)
        {
            if (!this.lst_Info.InvokeRequired)
            {
                ListViewItem lst = new ListViewItem("   " + CurrentTime, index);

                lst.SubItems.Add(log);

                this.lst_Info.Items.Insert(0, lst);

            }
            else
            {
                this.lst_Info.Invoke(new Action(() =>
                {
                    ListViewItem lst = new ListViewItem("   " + CurrentTime, index);

                    lst.SubItems.Add(log);

                    this.lst_Info.Items.Insert(0, lst);
                }));

            }


        }

        /// <summary>
        /// 添加报警
        /// </summary>
        /// <param name="info"></param>
        /// <param name="isAck"></param>
        public void AddAlarm(string info, bool isAck)
        {
            if (isAck)
            {
                if (!AlarmInfoList.Contains(info))
                {
                    AlarmInfoList.Add(info);
                }
            }
            else
            {
                if (AlarmInfoList.Contains(info))
                {
                    AlarmInfoList.Remove(info);
                }
            }

            //更新界面
            RefreshAlarm();

        }

        /// <summary>
        /// 更新报警界面
        /// </summary>
        private void RefreshAlarm()
        {
            this.Invoke(new Action(() =>
            {
                if (AlarmInfoList.Count == 0)
                {
                    this.led_state.Value = true;
                    this.lbl_Info.Visible = true;
                    this.lbl_ScrollInfo.Visible = false;
                    this.lbl_Info.Text = "系统运行正常";

                }

                else if (AlarmInfoList.Count == 1)
                {
                    this.led_state.Value = false;
                    this.lbl_Info.Visible = true;
                    this.lbl_ScrollInfo.Visible = false;
                    this.lbl_Info.Text = AlarmInfoList[0];

                }
                else
                {
                    this.led_state.Value = false;
                    this.lbl_Info.Visible = false;
                    this.lbl_ScrollInfo.Visible = true;
                    this.lbl_ScrollInfo.Text = string.Join("  ", AlarmInfoList).Trim();
                }
            }));


        }

        /// <summary>
        /// 报警信息列表
        /// </summary>
        private List<string> AlarmInfoList = new List<string>();



        /// <summary>
        /// 当前时间
        /// </summary>
        private string CurrentTime
        {
            get { return DateTime.Now.ToString("HH:mm:ss"); }
        }

    }
}
