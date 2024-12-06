using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using thinger.AutomaticStoreMotionDAL;
using thinger.AutomaticStoreMotionModels;

namespace MotionTestSystem
{
    /// <summary>
    /// 在类的外面构建串口参数结构体
    /// </summary>
    public enum SerialParam
    {
        N81,
        N71,
        E81,
        E71,
        O81,
        O71
    }
    public partial class FormParamSet : Form
    {
        public FormParamSet()
        {
            InitializeComponent();
            //端口号
            this.WeightPort.DataSource = SerialPort.GetPortNames();
            this.ScannerPort.DataSource = SerialPort.GetPortNames();

            //波特率
            this.WeightBaud.DataSource = new string[] { "4800", "9600", "19200", "38400" };
            this.ScannerBaud.DataSource = new string[] { "4800", "9600", "19200", "38400" };

            //参数
            this.WeightParam.DataSource = Enum.GetNames(typeof(SerialParam));
            this.ScannerParam.DataSource = Enum.GetNames(typeof(SerialParam));

            //方向
            this.HomeDir_X.DataSource = new string[] { "正向", "反向" };
            this.HomeDir_Y.DataSource = new string[] { "正向", "反向" };
            this.HomeDir_Z.DataSource = new string[] { "正向", "反向" };

            //配置
            DirectoryInfo directoryInfo = new DirectoryInfo(Application.StartupPath + "\\Config");

            foreach (FileInfo file in directoryInfo.GetFiles("*.cfg"))
            {
                this.Cfg.Items.Add(file.Name);
            }

            //读取配置文件
            LoadParam(this.tabPage1, Program.motion.basicParam);
            LoadParam(this.tabPage2, Program.motion.advanceParam);
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tabPage"></param>
        /// <param name="obj"></param>
        private void LoadParam<T>(TabPage tabPage, T obj)
        {

            foreach (var item in tabPage.Controls)
            {
                if (item is NumericUpDown numericUpDown)
                {
                    //拿到属性名称
                    string propertyName = numericUpDown.Name;
                    //通过属性拿到值
                    string value = GetObjectPropertyValue(obj, propertyName);
                    if (value != null && value.Length > 0)
                    {
                        if (decimal.TryParse(value, out decimal val))
                        {
                            numericUpDown.Value = val;
                        }
                    }


                }
                else if (item is ComboBox comboBox)
                {

                    //拿到属性名称
                    string propertyName = comboBox.Name;

                    comboBox.Text = GetObjectPropertyValue(obj, propertyName);

                }
            
            
            
            }
        
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

        /// <summary>
        /// 修改参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="paramType"></param>
        /// <param name="obj"></param>
        private void ModifyParam<T>(string paramType, T obj)
        {

            StringBuilder sb = new StringBuilder();

            foreach (var item in this.MainTab.SelectedTab.Controls)
            {
                if (item is NumericUpDown numeric)
                {
                    //获取属性名称
                    string propertyName = numeric.Name;

                    //获取对象里的值
                    string value = GetObjectPropertyValue(obj, propertyName);

                    if (numeric.Value.ToString() != value)
                    {
                        //添加修改记录
                        sb.Append(numeric.Tag.ToString() + "修改为：" + numeric.Value.ToString() + " ");

                        SetObjectPropertyValue(obj, propertyName, numeric.Value.ToString());
                    }
                }
                else if (item is ComboBox cmb)
                {
                    //获取属性名称
                    string propertyName = cmb.Name;

                    //获取对象里的值
                    string value = GetObjectPropertyValue(obj, propertyName);

                    if (cmb.Text != value)
                    {
                        //添加修改记录
                        sb.Append(cmb.Tag.ToString() + "修改为：" + cmb.Text.ToString() + " ");

                        SetObjectPropertyValue(obj, propertyName, cmb.Text);
                    }
                }
            }

            if (sb.ToString().Length > 0)
            {
                OperationResult result = paramType == "基础参数" ? Program.motion.SaveBasicParam() : Program.motion.SaveAdvaceParam();

                if (result.IsSuccess)
                {
                    MessageBox.Show(paramType + "修改成功", "保存修改");
                }
                else
                {
                    MessageBox.Show("参数修改", paramType + "修改失败") ;
                }
            }
            else
            {
                MessageBox.Show("参数修改", "参数未做任何修改");
            }

        }
        /// <summary>
        /// 通过属性名称设置值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="value">值</param>
        /// <returns>是否成功</returns>
        private bool SetObjectPropertyValue<T>(T obj, string propertyName, string value)
        {
            try
            {
                Type type = typeof(T);

                object t = Convert.ChangeType(value, type.GetProperty(propertyName).PropertyType);

                type.GetProperty(propertyName).SetValue(obj, t, null);

                return true;

            }
            catch (Exception ex)
            {
             
                return false;
            }
        }

        private void btn_ModifyAdvanced_Click(object sender, EventArgs e)
        {
            ModifyParam("高级参数", Program.motion.advanceParam);
        }

        private void btn_CancelAdvanced_Click(object sender, EventArgs e)
        {
      
            LoadParam(this.tabPage2, Program.motion.advanceParam);
        }

        private void btn_ModifyBasic_Click(object sender, EventArgs e)
        {
            ModifyParam("高级参数", Program.motion.basicParam);
        }

        private void btn_CancelBasic_Click(object sender, EventArgs e)
        {
            LoadParam(this.tabPage1, Program.motion.basicParam);
        }
    }
}
