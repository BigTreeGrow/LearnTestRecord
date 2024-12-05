using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinger.AutomaticStoreMotionModels
{
   public class OperationResult
    {
        /// <summary>
        ///success flag
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string  ErrorMsg { get; set; }


        public static OperationResult CreateSuccessResult()
        {
            return new OperationResult()
            {
                IsSuccess = true,

                ErrorMsg = "OK"
            };

        }

        public static OperationResult CreateFailResul()
        {
            return new OperationResult()
            {
                IsSuccess = false,

                ErrorMsg = "NG"
            };

        }

    }
}
