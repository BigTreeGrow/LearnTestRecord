using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using thinger.AutomaticStoreMotionModels;
namespace thinger.AutomaticStoreMotionDAL
{
   public  class Motion
    {


        /// <summary>
        /// 板卡初始成功标志
        /// </summary>
        public bool initedOK = false;

        /// <summary>
        /// 坐标系建立成功
        /// </summary>
        public bool buildOK = false;

        /// <summary>
        /// 是否暂停
        /// </summary>
        public bool isPause = false;
        public BasicParam basicParam = new BasicParam();

        public AdvanceParam advanceParam = new AdvanceParam();
      public  GoogolBoardCardController EcatMotionBoard = new GoogolBoardCardController();//实例化板卡
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
                MotorPara.AxisMotionPara[advanceParam.Axis_X].EactID = advanceParam.Axis_X;
                MotorPara.AxisMotionPara[advanceParam.Axis_X].AXISModule.lead = advanceParam.Lead_X;
                MotorPara.AxisMotionPara[advanceParam.Axis_X].DynamicsParaIn.circlePulse = (int)advanceParam.Scale_X;

                MotorPara.AxisMotionPara[advanceParam.Axis_Y].EactID = advanceParam.Axis_Y;
                MotorPara.AxisMotionPara[advanceParam.Axis_Y].AXISModule.lead = advanceParam.Lead_Y;
                MotorPara.AxisMotionPara[advanceParam.Axis_Y].DynamicsParaIn.circlePulse = (int)advanceParam.Scale_Y;

                MotorPara.AxisMotionPara[advanceParam.Axis_Z].EactID = advanceParam.Axis_Z;
                MotorPara.AxisMotionPara[advanceParam.Axis_Z].AXISModule.lead = advanceParam.Lead_Z;
                MotorPara.AxisMotionPara[advanceParam.Axis_Z].DynamicsParaIn.circlePulse = (int)advanceParam.Scale_Z;








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
        #region 通用初始化验证

        /// <summary>
        /// 通用初始化验证
        /// </summary>
        /// <returns>操作结果</returns>
        private OperationResult CommonInitedValidate()
        {
            OperationResult result = new OperationResult();

            if (initedOK == false)
            {
                result.IsSuccess = false;
                result.ErrorMsg = "未初始化";
                return result;
            }
            return OperationResult.CreateSuccessResult();
        }

        #endregion

        #region 通用运动初始化验证

        /// <summary>
        /// 通用运动初始化验证
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        private OperationResult CommonMotionValidate(short axis)
        {
            OperationResult result = CommonInitedValidate();

            if (!result.IsSuccess) return result;

            if (IsMoving(axis))
            {
                result.IsSuccess = false;
                result.ErrorMsg = "正在运动";
                return result;
            };

            return OperationResult.CreateSuccessResult();
        }


        #endregion


        #region 判断某个轴是否运行

        public bool IsMoving(short axis)
        {
            try
            {
                short error = 0;
                bool result;
                result= EcatMotionBoard.Get_AxisSts_Busy((EnumStageAxis)axis, out error);
                ErrorHandler("GT_GetSts", error);

                return result;
            }
            catch (Exception)
            {
                return true;
            }
        }


        #endregion



        #region 错误处理

        /// <summary>
        /// 错误处理
        /// </summary>
        /// <param name="command">指令</param>
        /// <param name="error">错误码</param>
        /// <remarks>
        /// 运动控制器指令返回值定义
        /// 0 指令执行成功
        /// 1 指令执行错误
        /// 2 license 不支持
        /// 7 指令参数错误
        /// -1 主机和运动控制器通讯失败
        /// -6 打开控制器失败
        /// -7 运动控制器没有响应  
        /// -10
        /// -13
        /// -14
        /// 
        /// 
        /// </remarks>
        private void ErrorHandler(string command, short error)
        {
            string result = string.Empty;

            switch (error)
            {
                case 0: break;
                case 1: result = $"{command}指令执行错误"; break;
                case 2: result = $"{command}license 不支持"; break;
                case 7: result = $"{command}指令参数错误"; break;
                case -1: result = $"{command}主机和运动控制器通讯失败"; break;
                case -6: result = $"{command}打开控制器失败"; break;
                case -7: result = $"{command}运动控制器没有响应"; break;
                default: result = $"{command}未知错误"; break;
            }

            if (result.Length > 0)
            {
                throw new Exception(result);
            }
        }

        #endregion


        #region 板卡初始化

        /// <summary>
        /// 板卡初始化
        /// </summary>
        /// <returns>操作结果</returns>
        public OperationResult InitCard()
        {
            //读取配置
            OperationResult result = LoadParam();

            if (!result.IsSuccess) return result;

            //定义运动控制指令的返回值
            short error;
            

            try
            {

                EcatMotionBoard.Connect();//板卡建立连接
                                         
                //清除轴标志
             
                EcatMotionBoard.ALLAxisClrAlarm(advanceParam.AxisCount,out error);
                // error = GTN.mc.GTN_ClrSts(1, 1, advanceParam.AxisCount);

                ErrorHandler("GT_ClrSts", error);

                //使能驱动器

                for (short i = 1; i <= advanceParam.AxisCount; i++)
                {
                    EcatMotionBoard.Enable((EnumStageAxis)i);
                }
                //获取所有驱动器使能状态
                if (EcatMotionBoard.Get_ALLAxisSts_Enable(advanceParam.AxisCount) == true)
                {
                    initedOK = true;
                }
                

               
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }

            return OperationResult.CreateSuccessResult();
        }
        #endregion

        #region 板卡关闭

        public OperationResult CloseCard()
        {
            //判断是否初始化成功
            OperationResult result = CommonInitedValidate();

            if (!result.IsSuccess) return result;

            //创建指令返回值
            short error;

            //关闭轴
            for (short i = 1; i < advanceParam.AxisCount; i++)
            {
                result = CloseAxis(i);

                if (!result.IsSuccess) return result;
            }

            //关闭卡
            try
            {
                EcatMotionBoard.BoardCardClose(out error);

                ErrorHandler("GT_Close", error);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }

            return OperationResult.CreateSuccessResult();
        }

        #endregion

        #region 轴关闭

        /// <summary>
        /// 关闭某个轴
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <returns>操作结果</returns>
        public OperationResult CloseAxis(short axis)
        {
            //判断是否初始化成功
            OperationResult result = CommonInitedValidate();

            if (!result.IsSuccess) return result;

            //创建指令返回值
            short error;
            try
            {
                EcatMotionBoard.Disable((EnumStageAxis)axis, out error);

                ErrorHandler("GT_AxisOff", error);

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }
            return OperationResult.CreateSuccessResult();

        }

        #endregion

        #region 点动

        /// <summary>
        /// 点动JOG
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="vel">速度</param>
        /// <param name="acc">加减速度</param>
        /// <returns>操作结果</returns>
        public OperationResult JogMove(short axis, double vel = 10, double acc = 10)
        {

            //通用运行初始化验证

            OperationResult result = CommonMotionValidate(axis);

            if (!result.IsSuccess) return result;

            //定义一个命令返回值
            short error = 0;

            try
            {
                EcatMotionBoard.JogMove((EnumStageAxis)axis, vel,acc,out error);
                ErrorHandler("JogMove", error);

            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }

            return OperationResult.CreateSuccessResult();
        }

        #endregion

        #region 单轴绝对定位
        /// <summary>
        /// 单轴绝对定位
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="pos">位置</param>
        /// <param name="vel">速度</param>
        /// <param name="acc">加减速度</param>
        /// <returns>操作结果</returns>
        public OperationResult MoveAbs(short axis, double pos, double vel = 10, double acc = 0.0125)
        {

            //通用运行初始化验证

            OperationResult result = CommonMotionValidate(axis);

            if (!result.IsSuccess) return result;

            //定义一个命令返回值
            short error = 0;

            try
            {
                EcatMotionBoard.MoveAbsoluteSync((EnumStageAxis)axis, pos, vel, acc,out  error);


                ErrorHandler("MoveAbsoluteSync", error);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }
            return OperationResult.CreateSuccessResult();
        }


        public OperationResult MoveAbs(Axis axis)
        {
            return MoveAbs(axis.AxisNo, axis.AxisDestPos, axis.AxisDestVel, axis.AxisDestAcc);
        }


        #endregion

        #region 单轴相对定位
        /// <summary>
        /// 单轴相对定位
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="pos">位置</param>
        /// <param name="vel">速度</param>
        /// <param name="acc">加减速度</param>
        /// <returns>操作结果</returns>
        public OperationResult MoveRelative(short axis, double pos, double vel = 10, double acc = 0.0125)
        {

            //通用运行初始化验证

            OperationResult result = CommonMotionValidate(axis);

            if (!result.IsSuccess) return result;

            //定义一个命令返回值
            short error = 0;

            try
            {
                EcatMotionBoard.MoveRelativeSync((EnumStageAxis)axis, pos, vel, acc, out error);
                ErrorHandler("MoveRelativeSync", error);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }
            return OperationResult.CreateSuccessResult();
        }


        #endregion
        #region 获取实时位置

        /// <summary>
        /// 获取实时位置
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <returns>实时位置</returns>
        public double GetCmdPos(short axis)
        {
            //判断是否初始化成功

            OperationResult result = CommonInitedValidate();

            if (!result.IsSuccess) return -999999.0;

            //定义指令返回值
            short error = 0;
            double pValue = 0;
            try
            {
                pValue = EcatMotionBoard.GetCurrentPosition((EnumStageAxis)axis);
                return pValue;
            }
            catch (Exception)
            {
                return -999999.0;
            }
        }

        #endregion
        #region 根据轴号返回轴的相关信息

        /// <summary>
        /// 根据轴号返回轴的相关信息
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <returns>轴相关信息</returns>
        private Axis GetAxisInfo(short axis)
        {
            if (axis == advanceParam.Axis_X)
            {
                return new Axis()
                {
                    AxisNo = advanceParam.Axis_X,
                    AxisHomeDir = advanceParam.HomeDir_X == "正向" ? 1 : -1,
                    AxisHomePos = advanceParam.HomePos_X,
                    AxisHomeNeg = advanceParam.HomeNeg_X,
                    AxisMoveVel = basicParam.SpeedAuto_X,
                    AxisHomeVel = basicParam.SpeedAuto_X * 0.25,
                    AxisHomeAcc = advanceParam.Acc_X,
                    AxisHomeOffset = advanceParam.HomeOffset_X,
                };
            }

            else if (axis == advanceParam.Axis_Y)
            {
                return new Axis()
                {
                    AxisNo = advanceParam.Axis_Y,
                    AxisHomeDir = advanceParam.HomeDir_Y == "正向" ? 1 : -1,
                    AxisHomePos = advanceParam.HomePos_Y,
                    AxisHomeNeg = advanceParam.HomeNeg_Y,
                    AxisMoveVel = basicParam.SpeedAuto_Y,
                    AxisHomeVel = basicParam.SpeedAuto_Y * 0.25,
                    AxisHomeAcc = advanceParam.Acc_Y,
                    AxisHomeOffset = advanceParam.HomeOffset_Y,
                };
            }
            else if (axis == advanceParam.Axis_Z)
            {
                return new Axis()
                {
                    AxisNo = advanceParam.Axis_Z,
                    AxisHomeDir = advanceParam.HomeDir_Z == "正向" ? 1 : -1,
                    AxisHomePos = advanceParam.HomePos_Z,
                    AxisHomeNeg = advanceParam.HomeNeg_Z,
                    AxisMoveVel = basicParam.SpeedAuto_Z,
                    AxisHomeVel = basicParam.SpeedAuto_Z * 0.25,
                    AxisHomeAcc = advanceParam.Acc_Z,
                    AxisHomeOffset = advanceParam.HomeOffset_Z,
                };
            }
            return null;

        }
        #endregion
        #region 等待正常停止

        /// <summary>
        /// 等待轴正常停止
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <returns>操作结果</returns>
        public OperationResult WaitStop(Axis axis)
        {

            OperationResult result = CommonInitedValidate();

            if (!result.IsSuccess) return result;

            //定义指令返回值
            short error = 0;
            //轴状态
            int sts;

            do
            {
                //如果为True，说明暂停被按下
                if (isPause)
                {
                    EcatMotionBoard.StopMotion((EnumStageAxis)axis.AxisNo);
                  

                    while (true)
                    {
                        if (isPause)
                        {
                            Thread.Sleep(10);
                        }
                        else
                        {
                            //恢复指令
                            MoveAbs(axis);

                            //跳出
                            break;
                        }
                    }

                }


                try
                {
                    sts = EcatMotionBoard.GetAxisState((EnumStageAxis)axis.AxisNo);
                   
                   
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.ErrorMsg = ex.Message;
                    return result;
                }
            } while ((sts & 0x400) != 0);
            return OperationResult.CreateSuccessResult();
        }



        /// <summary>
        /// 等待轴正常停止
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <returns>操作结果</returns>
        public OperationResult WaitStop(List<Axis> axis)
        {
            if (axis.Count != 2)
            {
                return new OperationResult()
                {
                    IsSuccess = false,
                    ErrorMsg = "传递对象集合数量不为2"
                };
            }

            OperationResult result = CommonInitedValidate();

            if (!result.IsSuccess) return result;

            //定义指令返回值
            short error = 0;
            //轴状态
            int sts0 = 0;
            int sts1 = 0;
            //定义控制器时钟
            uint pClock;
            do
            {
                try
                {
                    //如果为True，说明暂停被按下
                    if (isPause)
                    {
                        EcatMotionBoard.StopMotion((EnumStageAxis)axis[0].AxisNo);
                        EcatMotionBoard.StopMotion((EnumStageAxis)axis[1].AxisNo);

                        while (true)
                        {
                            if (isPause)
                            {
                                Thread.Sleep(10);
                            }
                            else
                            {
                                //恢复指令
                                MoveAbs(axis[0]);
                                MoveAbs(axis[1]);
                                //跳出
                                break;
                            }
                        }

                    }


                    sts0 = EcatMotionBoard.GetAxisState((EnumStageAxis)axis[0].AxisNo);
                    sts1 = EcatMotionBoard.GetAxisState((EnumStageAxis)axis[1].AxisNo);
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.ErrorMsg = ex.Message;
                    return result;
                }
            } while ((sts0 & 0x400) != 0 || (sts1 & 0x400) != 0);
            return OperationResult.CreateSuccessResult();
        }



        #endregion
        #region 单轴回原点
        /// <summary>
        /// 单轴回原点
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <returns>操作结果</returns>
        public OperationResult HomeAxis(short axis)
        {
            //判断是否运动
            OperationResult result = CommonMotionValidate(axis);

            if (!result.IsSuccess) return result;

            //获取轴的相关信息

            Axis axisInfo = GetAxisInfo(axis);

            if (axisInfo == null)
            {
                result.IsSuccess = false;
                result.ErrorMsg = "轴信息获取失败";
                return result;
            }

            

            //寻原点
            result = ExcuteHomeCommand(axisInfo);
            if (!result.IsSuccess) return result;


            return OperationResult.CreateSuccessResult();

        }

        /// <summary>
        /// 执行回原点指令
        /// </summary>
        /// <param name="axis">轴信息</param>
        /// <param name="isFirst">是否是第一次回原点</param>
        /// <returns>操作结果</returns>
        private OperationResult ExcuteHomeCommand(Axis axis, bool isFirst = true)
        {
            bool error ;

            OperationResult result = new OperationResult();

            //寻原点
            try
            {
                error= EcatMotionBoard.Home((EnumStageAxis)axis.AxisNo);
                if (false == error)
                {

                    throw new Exception();
                
                }
            }

            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }

            return OperationResult.CreateSuccessResult();
        }


        #endregion
        #region 2轴绝对定位

        /// <summary>
        /// 2轴绝对定位
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="pos">位置</param>
        /// <param name="vel">速度</param>
        /// <param name="acc">加速度</param>
        /// <returns>操作结果</returns>
        public OperationResult Move2DAbs(short[] axis, double[] pos, double[] vel, double[] acc)
        {
            if (axis.Length == 2 && pos.Length == 2 && vel.Length == 2 && acc.Length == 2)
            {
                //判断是否满足运动要求
                OperationResult result = CommonMotionValidate(axis[0]);
                if (!result.IsSuccess) return result;

                result = CommonMotionValidate(axis[1]);
                if (!result.IsSuccess) return result;


                Axis axisInfoX = new Axis();
                axisInfoX.AxisNo = axis[0];
                axisInfoX.AxisDestPos = pos[0];
                axisInfoX.AxisDestVel = vel[0];
                axisInfoX.AxisDestAcc = acc[0];


                Axis axisInfoY = new Axis();
                axisInfoY.AxisNo = axis[1];
                axisInfoY.AxisDestPos = pos[1];
                axisInfoY.AxisDestVel = vel[1];
                axisInfoY.AxisDestAcc = acc[1];

                //绝对定位
                result = MoveAbs(axisInfoX);
                if (!result.IsSuccess) return result;

                result = MoveAbs(axisInfoY);
                if (!result.IsSuccess) return result;

                //等待停止
                result = WaitStop(new List<Axis>() { axisInfoX, axisInfoY });
                if (!result.IsSuccess) return result;

                return OperationResult.CreateSuccessResult();
            }
            return new OperationResult()
            {
                IsSuccess = false,
                ErrorMsg = "传递参数长度不正确"
            };
        }



        #endregion
        #region 3轴绝对定位

        /// <summary>
        /// 3轴绝对定位，前面两个索引为XY轴，最后一个索引为Z轴
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <param name="pos">位置</param>
        /// <param name="vel">速度</param>
        /// <param name="acc">加速度</param>
        /// <returns>操作结果</returns>
        public OperationResult Move3DAbs(short[] axis, double[] pos, double[] vel, double[] acc)
        {
            if (axis.Length == 3 && pos.Length == 3 && vel.Length == 3 && acc.Length == 3)
            {
                //判断是否满足运动要求
                OperationResult result = CommonMotionValidate(axis[0]);
                if (!result.IsSuccess) return result;

                result = CommonMotionValidate(axis[1]);
                if (!result.IsSuccess) return result;

                result = CommonMotionValidate(axis[2]);
                if (!result.IsSuccess) return result;


                Axis axisInfoZ = new Axis();
                axisInfoZ.AxisNo = axis[2];
                axisInfoZ.AxisDestPos = basicParam.Safty_Z;
                axisInfoZ.AxisDestVel = vel[2];
                axisInfoZ.AxisDestAcc = acc[2];


                //Z轴运行到安全位置
                result = MoveAbs(axisInfoZ);
                if (!result.IsSuccess) return result;


                result = WaitStop(axisInfoZ);
                if (!result.IsSuccess) return result;

                //二轴定位
                result = Move2DAbs(new short[] { axis[0], axis[1] }, new double[] { pos[0], pos[1] }, new double[] { vel[0], vel[1] }, new double[] { acc[0], acc[1] });
                if (!result.IsSuccess) return result;

                //Z轴再次定位

                axisInfoZ = new Axis();
                axisInfoZ.AxisNo = axis[2];
                axisInfoZ.AxisDestPos = pos[2];
                axisInfoZ.AxisDestVel = vel[2];
                axisInfoZ.AxisDestAcc = acc[2];

                result = MoveAbs(axisInfoZ);
                if (!result.IsSuccess) return result;

                result = WaitStop(axisInfoZ);
                if (!result.IsSuccess) return result;

                return OperationResult.CreateSuccessResult();

            }
            return new OperationResult()
            {
                IsSuccess = false,
                ErrorMsg = "传递参数长度不正确"
            };
        }


        #endregion
        #region 停止单轴

        /// <summary>
        /// 停止单轴
        /// </summary>
        /// <param name="axis">轴号</param>
        /// <returns>操作结果</returns>
        public OperationResult StopAxis(short axis)
        {
            OperationResult result = CommonInitedValidate();

            if (!result.IsSuccess) return result;

            //定义返回值
            short error = 0;

            try
            {

                EcatMotionBoard.StopMotion((EnumStageAxis)axis,out error);
                ErrorHandler("GT_Stop", error);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }
            return OperationResult.CreateSuccessResult();

        }

        #endregion

        #region 根据索引获输入取位
        /// <summary>
        /// 根据索引获取位
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>位结果</returns>
        public bool GetInput(short index)

        {
            int vaule=0;
            EcatMotionBoard.GlinkIO_ReadInput(index,out vaule);
            if (vaule == 0)
            {
                return false;
            }
            else
            {
                return true ;
            }
          

        }
        #endregion      
        #region 获取输出
        /// <summary>
        /// 获取输出
        /// </summary>
        /// <returns>输出结果</returns>
        public short GetOutput()
        {
            //是否初始化
            OperationResult result = CommonInitedValidate();

            if (!result.IsSuccess) return 0;

            short error = 0;

            int res;
            try
            {
                EcatMotionBoard.IO_ReadOutput_2(0,out res);
                ErrorHandler("GT_GetDo", error);
                return (short)~res;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// 根据索引获取位
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>位结果</returns>
        public bool GetOutput(short index)
        {
            short status = GetOutput();

            //32

            if ((status & Convert.ToInt32(Math.Pow(2, index - 1))) != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region 根据索引操作输出位

        /// <summary>
        /// 根据索引操作位
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="open">操作位</param>
        /// <returns>操作结果</returns>
        public OperationResult SetOutput(short index, bool open)
        {
            //是否初始化
            OperationResult result = CommonInitedValidate();

            if (!result.IsSuccess) return result;

            short error = 0;
            short res = 0;
            try
            {
                if (open)
                {
                    res = 1;
                }
                else
                {
                    res = 0;
                }
                EcatMotionBoard.IO_WriteOutPut_2(0, index, res);

                ErrorHandler("GT_SetDoBit", error);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMsg = ex.Message;
                return result;
            }
            return OperationResult.CreateSuccessResult();
        }

        #endregion



    }
    #region 轴相关信息
    /// <summary>
    /// 轴相关信息
    /// </summary>
    public class Axis
    {
        /// <summary>
        /// 轴号
        /// </summary>
        public short AxisNo { get; set; }

        /// <summary>
        /// 轴寻原点方向，1为正向，-1为方向
        /// </summary>
        public int AxisHomeDir { get; set; }

        /// <summary>
        /// 轴寻原点正向距离
        /// </summary>
        public short AxisHomePos { get; set; }

        /// <summary>
        /// 轴寻原点反向距离
        /// </summary>
        public short AxisHomeNeg { get; set; }

        /// <summary>
        /// 轴寻原点正常速度
        /// </summary>
        public double AxisMoveVel { get; set; }

        /// <summary>
        /// 轴寻原点速度
        /// </summary>
        public double AxisHomeVel { get; set; }

        /// <summary>
        /// 轴寻原点加速度
        /// </summary>
        public double AxisHomeAcc { get; set; }

        /// <summary>
        /// 轴寻原点偏移
        /// </summary>
        public short AxisHomeOffset { get; set; }

        /// <summary>
        /// 轴目标位置
        /// </summary>
        public double AxisDestPos { get; set; }

        /// <summary>
        /// 轴目标速度
        /// </summary>
        public double AxisDestVel { get; set; }

        /// <summary>
        /// 轴目标加速度
        /// </summary>
        public double AxisDestAcc { get; set; }



    }

    #endregion
}
