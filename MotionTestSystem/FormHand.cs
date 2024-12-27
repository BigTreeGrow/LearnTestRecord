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
        }
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
                            btn.Click += btn_AxisCtrol_Click;
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

        private void btn_AxisCtrol_Click(object sender, EventArgs e)
        {

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
    }
}
