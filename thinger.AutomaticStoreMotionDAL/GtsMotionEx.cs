using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thinger.AutomaticStoreMotionModels;

namespace thinger.AutomaticStoreMotionDAL
{
   public class GtsMotionEx
    {

        #region//单例模式
        private static GtsMotionEx instance;

        //定义一个锁
        private static readonly object objlock = new object();

        //创建私有构造方法
        private GtsMotionEx()
        { 
        
        }

        public static GtsMotionEx GetInstance()
        {
            if (instance == null)
            {
                lock (objlock)
                {
                    if (instance == null)
                    {
                        instance = new GtsMotionEx();
                    }
                
                }
            
            
            }
            return instance;


        }
        #endregion

        #region  字段属性

        private Motion motion = new Motion();

        public BasicParam basicParam
        {
            get { return motion.basicParam; }
        }

        public AdvanceParam advanceParam
        {
            get { return motion.advanceParam; }
        }

        //初始化状态
        public bool initialOK
        {
            get { return motion.initedOK; }
        }
        private bool isPause;
        /// <summary>
        /// 系统暂停暂停
        /// </summary>
        public bool IsPause
        {
            get { return isPause; }
            set
            {
                isPause = value;
                motion.isPause = isPause;
            }
        }
        #endregion

        #region 板卡控制
        /// <summary>
        /// 初始化卡
        /// </summary>
        /// <returns></returns>
        public OperationResult InitCard()
        {
            return motion.InitCard();
        }

        /// <summary>
        /// 关闭卡
        /// </summary>
        /// <returns></returns>
        public OperationResult CloseCard()
        {
            return motion.CloseCard();
        }

        #endregion

        #region   点动控制
        /// <summary>
        /// X轴点动
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public OperationResult JogMoveAxisX(bool dir)
        {
            return motion.JogMove(advanceParam.Axis_X, dir ? basicParam.SpeedHand_X : basicParam.SpeedHand_X * (-1.0), advanceParam.Acc_X);


        }

        /// <summary>
        /// Y轴点动
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public OperationResult JogMoveAxisY(bool dir)
        {
            return motion.JogMove(advanceParam.Axis_Y, dir ? basicParam.SpeedHand_Y : basicParam.SpeedHand_Y * (-1.0), advanceParam.Acc_Y);


        }

        /// <summary>
        /// Z轴点动
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public OperationResult JogMoveAxisZ(bool dir)
        {
            return motion.JogMove(advanceParam.Axis_Z, dir ? basicParam.SpeedHand_Z : basicParam.SpeedHand_Z * (-1.0), advanceParam.Acc_Z);


        }
        /// <summary>
        /// XYZ轴点动
        /// </summary>
        /// <param name="name">按钮名称，开始字符表示轴，结束字符表示方向</param>
        /// <returns>操作结果</returns>
        public OperationResult JogMoveAxis(string name)
        {
            name = name.ToUpper();
            if (name.StartsWith("X"))
            {
                if (name.EndsWith("+"))
                {
                    return JogMoveAxisX(true);
                }
                else
                {
                    return JogMoveAxisX(false);
                }
            }

            else if (name.StartsWith("Y"))
            {
                if (name.EndsWith("+"))
                {
                    return JogMoveAxisY(true);
                }
                else
                {
                    return JogMoveAxisY(false);
                }
            }
            else if (name.StartsWith("Z"))
            {
                if (name.EndsWith("+"))
                {
                    return JogMoveAxisZ(true);
                }
                else
                {
                    return JogMoveAxisZ(false);
                }
            }

            return new OperationResult()
            {
                IsSuccess = false,
                ErrorMsg = "参数名称不正确，没有以XYZ开头"
            };
        }

        /// <summary>
        /// 停止轴
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public OperationResult StopAxis(string name)
        {
            name = name.ToUpper();
            if (name.StartsWith("X"))
            {
                return motion.StopAxis(advanceParam.Axis_X);
            }
            else if (name.StartsWith("Y"))
            {
                return motion.StopAxis(advanceParam.Axis_Y);
            }
            if (name.StartsWith("Z"))
            {
                return motion.StopAxis(advanceParam.Axis_Z);
            }
            return new OperationResult()
            {
                IsSuccess = false,
                ErrorMsg = "参数名称不正确，没有以XYZ开头"
            };
        }




        #endregion
        #region XYZ轴绝对定位

        public OperationResult MoveXAbs(double pos)
        {
            return motion.MoveAbs(advanceParam.Axis_X, pos, basicParam.SpeedHand_X, advanceParam.Acc_X);
        }

        public OperationResult MoveYAbs(double pos)
        {
            return motion.MoveAbs(advanceParam.Axis_Y, pos, basicParam.SpeedHand_Y, advanceParam.Acc_Y);
        }

        public OperationResult MoveZAbs(double pos)
        {
            return motion.MoveAbs(advanceParam.Axis_Z, pos, basicParam.SpeedHand_Z, advanceParam.Acc_Z);
        }


        #endregion


        #region 获取实时位置

        public double GetCmdPosX()
        {
            return motion.GetCmdPos(advanceParam.Axis_X);
        }

        public double GetCmdPosY()
        {
            return motion.GetCmdPos(advanceParam.Axis_Y);
        }

        public double GetCmdPosZ()
        {
            return motion.GetCmdPos(advanceParam.Axis_Z);
        }

        public string GetCmdPos()
        {
            return GetCmdPosX().ToString("f1") + ";" + GetCmdPosY().ToString("f1") + ";" + GetCmdPosZ().ToString("f1");
        }

        #endregion

        #region 配置方法


        /// <summary>
        /// 保存基础参数
        /// </summary>
        /// <returns>操作结果</returns>
        public OperationResult SaveBasicParam()
        {
            return motion.SaveBasicParam();

        }

        /// <summary>
        /// 保存高级参数
        /// </summary>
        /// <returns>操作结果</returns>
        public OperationResult SaveAdvancedParam()
        {
            return motion.SaveAdvaceParam();
        }

        /// <summary>
        /// 参数加载
        /// </summary>
        /// <returns>操作结果</returns>
        public OperationResult LoadParam()
        {
            return motion.LoadParam();
        }

        #endregion

    }
}
