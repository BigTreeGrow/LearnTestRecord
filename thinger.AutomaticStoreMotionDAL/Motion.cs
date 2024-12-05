using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using thinger.AutomaticStoreMotionModels;
namespace thinger.AutomaticStoreMotionDAL
{
   public  class Motion
    {
        public BasicParam basicParam = new BasicParam();

        public AdvanceParam advanceParam = new AdvanceParam();

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public string configFile = Application.StartupPath + "\\Config\\Config.ini";

        /// <summary>
        /// 保存基础参数
        /// </summary>
        /// <returns></returns>
        public OperationResult SaveBasicParam()
        {
            ///内容转化为json
            string json = JSONHelper.EntityToJSON(basicParam);

            //ini write
            if (iniConfigHelper.WriteIniData("参数", "基础参数", json, configFile))
            {
                return OperationResult.CreateSuccessResult();
            }
            else
            {
                return OperationResult.CreateFailResul();
            }
        
        }
        /// <summary>
        /// 保存高级参数
        /// </summary>
        /// <returns></returns>
        public OperationResult SaveAdvaceParam()
        {
            ///内容转化为json
            string json = JSONHelper.EntityToJSON(advanceParam);

            //ini write
            if (iniConfigHelper.WriteIniData("参数", "高级参数", json, configFile))
            {
                return OperationResult.CreateSuccessResult();
            }
            else
            {
                return OperationResult.CreateFailResul();
            }

        }

        /// <summary>
        /// 参数加载
        /// </summary>
        /// <returns></returns>
        public OperationResult LoadParam()
        {
            try
            {
                //读取json
                string jsonbasic = iniConfigHelper.ReadIniData("参数", "基础参数", JSONHelper.EntityToJSON(basicParam), configFile);
                //json转换成对象
                basicParam = JSONHelper.JSONToEntity<BasicParam>(jsonbasic);

                //读取json
                string jsonadvanced = iniConfigHelper.ReadIniData("参数", "高级参数", JSONHelper.EntityToJSON(advanceParam), configFile);

                //json转换成对象
                advanceParam = JSONHelper.JSONToEntity<AdvanceParam>(jsonadvanced);



            }
            catch (Exception ex)
            {
                return new OperationResult()
                {
                    IsSuccess = false,
                    ErrorMsg = ex.Message
                };

            }

            return OperationResult.CreateSuccessResult();

        }



    }
}
