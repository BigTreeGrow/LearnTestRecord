using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinger.AutomaticStoreMotionModels
{
   public  class AdvanceParam
    {
        //运动控制轴

        //卡号
        public short CardNo { get; set; } = 1;

        //轴数量
        public short AxisCount { get; set; } = 4;

        //X轴
        public short Axis_X { get; set; } = 1;

        //Y轴
        public short Axis_Y { get; set; } = 2;

        //Z轴
        public short Axis_Z { get; set; } = 3;

        //U轴
        public short Axis_U { get; set; } = 4;


        //输入输出口

        //存料按钮
        public short StoreButton { get; set; } = 1;

        //取料按钮
        public short ReclaimButton { get; set; } = 2;

        //急停按钮
        public short EmergencyButton { get; set; } = 3;

        //夹子打开到位
        public short ClipOpen { get; set; } = 4;

        //夹子关闭到位
        public short ClipClose { get; set; } = 5;

        //存料指示
        public short StoreState { get; set; } = 1;

        //取料指示
        public short ReclaimState { get; set; } = 2;

        //模式指示
        public short ModeState { get; set; } = 3;

        //夹子控制
        public short ClipCtrl { get; set; } = 4;

        //上左门控制
        public short TopLeftDoor { get; set; } = 5;

        //上后门控制
        public short TopBehindDoor { get; set; } = 6;

        //上右门控制
        public short TopRightDoor { get; set; } = 7;

        //下前门控制
        public short BottomFrontDoor { get; set; } = 8;

        //照明控制
        public short LightCtrl { get; set; } = 9;


        //总行数
        public short RowCount { get; set; } = 10;

        //总列数
        public short ColumnCount { get; set; } = 9;


        //运动控制卡配置文件
        public string Cfg { get; set; } = "thinger";


        //脉冲当量

        //X轴缩放系数 
        public double Scale_X { get; set; } = 4000;

        //Y轴缩放系数 
        public double Scale_Y { get; set; } = 4000;

        //Z轴缩放系数 
        public double Scale_Z { get; set; } = 4000;

        //加速度

        //X轴运动加速度
        public double Acc_X { get; set; } = 0.01;

        //Y轴运动加速度
        public double Acc_Y { get; set; } = 0.01;

        //Z轴运动加速度
        public double Acc_Z { get; set; } = 0.01;


        //复位方向

        //X复位方向
        public string HomeDir_X { get; set; } = "正向";
        //Y复位方向
        public string HomeDir_Y { get; set; } = "正向";
        //Z复位方向
        public string HomeDir_Z { get; set; } = "正向";


        //回原点距离 单位mm

        //X轴正向寻原点距离
        public short HomePos_X { get; set; } = 10;

        //X轴反向寻原点距离
        public short HomeNeg_X { get; set; } = 100;

        //Y轴正向寻原点距离
        public short HomePos_Y { get; set; } = 10;

        //Y轴反向寻原点距离
        public short HomeNeg_Y { get; set; } = 100;

        //Z轴正向寻原点距离
        public short HomePos_Z { get; set; } = 10;

        //Z轴反向寻原点距离
        public short HomeNeg_Z { get; set; } = 100;

        //寻原点偏移  Pulse

        //X轴寻原点偏移
        public short HomeOffset_X { get; set; } = 8000;

        //Y轴寻原点偏移
        public short HomeOffset_Y { get; set; } = 8000;

        //Z轴寻原点偏移
        public short HomeOffset_Z { get; set; } = 8000;

    }
}
