using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinger.AutomaticStoreMotionModels
{
   public class BasicParam
    {
        //XYZ轴坐标值设置  （单位：mm）

        //X轴称重坐标值
        public double Weight_X { get; set; } = 0.0;

        //Y轴称重坐标值
        public double Weight_Y { get; set; } = 0.0;

        //Z轴称重坐标值
        public double Weight_Z { get; set; } = 0.0;


        //X轴回收坐标值
        public double Recycle_X { get; set; } = 0.0;

        //Y轴回收坐标值
        public double Recycle_Y { get; set; } = 0.0;

        //Z轴回收坐标值
        public double Recycle_Z { get; set; } = 0.0;


        //X轴待机坐标值
        public double Standby_X { get; set; } = 0.0;

        //Y轴待机坐标值
        public double Standby_Y { get; set; } = 0.0;

        //Z轴待机坐标值
        public double Standby_Z { get; set; } = 0.0;



        //X轴初始坐标值
        public double Initial_X { get; set; } = 0.0;

        //Y轴初始坐标值
        public double Initial_Y { get; set; } = 0.0;

        //Z轴库位坐标值
        public double Store_Z { get; set; } = 0.0;



        //X轴偏移坐标值
        public double Offset_X { get; set; } = 0.0;

        //Y轴偏移坐标值
        public double Offset_Y { get; set; } = 0.0;

        //Z轴安全坐标值
        public double Safty_Z { get; set; } = 0.0;


        //XYZ轴速度设置  （单位：pulse/ms）

        //X轴自动运动速度
        public double SpeedAuto_X { get; set; } = 15;

        //Y轴自动运动速度
        public double SpeedAuto_Y { get; set; } = 2;

        //Z轴自动运动速度
        public double SpeedAuto_Z { get; set; } = 1;


        //X轴手动运动速度
        public double SpeedHand_X { get; set; } = 15;

        //Y轴手动运动速度
        public double SpeedHand_Y { get; set; } = 2;

        //Z轴手动运动速度
        public double SpeedHand_Z { get; set; } = 1;


        //系统参数设置

        //秤重端口
        public string WeightPort { get; set; } = "COM1";

        //扫码枪端口
        public string ScannerPort { get; set; } = "COM2";

        //秤重波特率
        public int WeightBaud { get; set; } = 9600;

        //扫码枪波特率
        public int ScannerBaud { get; set; } = 9600;

        //秤重参数
        public string WeightParam { get; set; } = "N81";

        //扫码枪参数
        public string ScannerParam { get; set; } = "N81";

        //空瓶重量
        public double EmptyWeight { get; set; } = 12.0f;

        //冗余重量
        public double RedundancyWeight { get; set; } = 2.0f;

        //允许入库数量
        public int AllowInCount { get; set; } = 100;

        //允许出库数量
        public int AllowOutCount { get; set; } = 3;

        //自动锁定间隔 单位秒
        public int AutoLock { get; set; } = 100;


    }
}
