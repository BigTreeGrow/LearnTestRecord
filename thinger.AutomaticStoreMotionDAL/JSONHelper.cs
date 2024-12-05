using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace thinger.AutomaticStoreMotionDAL
{
  public  class JSONHelper
    {

        /// <summary>
        /// 实体对象转换成JSON字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string EntityToJSON<T>(T x)
        {
            string result = string.Empty;

            try
            {
                result = JsonConvert.SerializeObject(x);
            }
            catch (Exception)
            {
                result = string.Empty;
            }
            return result;

        }

        /// <summary>
        /// JSON字符串转换成实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JSONToEntity<T>(string json)
        {
            T t = default(T);
            try
            {
                t = (T)JsonConvert.DeserializeObject(json, typeof(T));
            }
            catch (Exception)
            {
                t = default(T);
            }

            return t;
        }
    }
}
