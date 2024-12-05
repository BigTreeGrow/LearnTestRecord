using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
namespace thinger.AutomaticStoreMotionDAL
{
   public class iniConfigHelper
    {
        #region API函数声明
        /// <summary>
        /// 用于更新或创建 INI 文件中的字符串。
        /// INI 文件是一种简单的文本文件格式，通常用于存储配置信息。
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key,
    string val, string filePath);

        /// <summary>
        /// 从ini中读取文件内容
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <param name="retVal"></param>
        /// <param name="size"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        //需要调用GetPrivateProfileString的重载
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern long GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileString")]
        private static extern uint GetPrivateProfileStringA(string section, string key,
            string def, byte[] retVal, int size, string filePath);
        #endregion

        public static string filePath = string.Empty;


        #region 读Ini文件

        public static string ReadIniData(string Section, string Key, string NoText)
        {
            return ReadIniData(Section, Key, NoText, filePath);
        }
        public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section, Key, NoText, temp, 1024, iniFilePath);
                return temp.ToString();
            }
            else return String.Empty;
        }

        #endregion

        #region 写Ini文件

        public static bool WriteIniData(string Section, string Key, string Value)
        {
            return WriteIniData(Section, Key, Value, filePath);
        }

        public static bool WriteIniData(string Section, string Key, string Value, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                long OpStation = WritePrivateProfileString(Section, Key, Value, iniFilePath);
                if (OpStation == 0)
                    return false;
                else return true;
            }
            else return false;
        }
        #endregion

        #region 获取所有的Sections
        /// <summary>
        /// 获取所有的Sections
        /// </summary>
        /// <param name="iniFilename">文件路径</param>
        /// <returns>Sections集合</returns>
        public static List<string> ReadSections(string iniFilename)
        {
            List<string> result = new List<string>();
            Byte[] buf = new Byte[65536];
            uint len = GetPrivateProfileStringA(null, null, null, buf, buf.Length, iniFilename);
            int j = 0;
            for (int i = 0; i < len; i++)
                if (buf[i] == 0)
                {
                    result.Add(Encoding.Default.GetString(buf, j, i - j));
                    j = i + 1;
                }
            return result;
        }

        #endregion

        #region 获取指定Section下的所有Keys
        /// <summary>
        /// 获取指定Section下的所有Keys
        /// </summary>
        /// <param name="SectionName">SectionName</param>
        /// <param name="iniFilename">文件路径</param>
        /// <returns>Keys集合</returns>
        public static List<string> ReadKeys(string SectionName, string iniFilename)
        {
            List<string> result = new List<string>();
            Byte[] buf = new Byte[65536];
            uint len = GetPrivateProfileStringA(SectionName, null, null, buf, buf.Length, iniFilename);
            int j = 0;
            for (int i = 0; i < len; i++)
                if (buf[i] == 0)
                {
                    result.Add(Encoding.Default.GetString(buf, j, i - j));
                    j = i + 1;
                }
            return result;
        }

        #endregion

    }
}

