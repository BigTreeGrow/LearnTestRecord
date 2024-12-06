using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace thinger.AutomaticStoreMotionControlLib
{
    public partial class HeadPanel : Panel
    {
        public HeadPanel()
        {
            InitializeComponent();

            //初始化
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            this.sf = new StringFormat();
            this.sf.Alignment = StringAlignment.Center;
            this.sf.LineAlignment = StringAlignment.Center;

        }


        private StringFormat sf;

        private string titleText = "系统控制";

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取标题文本")]
        public string TitleText
        {
            get { return titleText; }
            set
            {
                titleText = value;
                this.Invalidate();
            }
        }

        private Color themeColor = Color.FromArgb(2, 69, 163);

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取标题文本背景色")]
        public Color ThemeColor
        {
            get { return themeColor; }
            set
            {
                themeColor = value;
                this.Invalidate();
            }
        }

        private Color themeForeColor = Color.White;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取标题文本前景色")]
        public Color ThemeForeColor
        {
            get { return themeForeColor; }
            set
            {
                themeForeColor = value;
                this.Invalidate();
            }
        }


        private int headHeight = 40;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取标题栏高度")]
        public int HeadHeight
        {
            get { return headHeight; }
            set
            {
                headHeight = value;
                this.Invalidate();
            }
        }


        private Color borderColor = Color.Gray;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取边框颜色")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }



        private float linearGradientRate = 0.4F;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取背景色渐变系数")]
        public float LinearGradientRate
        {
            get { return linearGradientRate; }
            set
            {
                linearGradientRate = value;
                this.Invalidate();
            }
        }


        private ContentAlignment textAlign = ContentAlignment.MiddleCenter;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("设置或获取标题文本位置")]
        public ContentAlignment TextAlign
        {
            get { return textAlign; }
            set
            {
                textAlign = value;

                switch (textAlign)
                {
                    case ContentAlignment.TopLeft:
                        this.sf.Alignment = StringAlignment.Near;
                        this.sf.LineAlignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.TopCenter:
                        this.sf.Alignment = StringAlignment.Center;
                        this.sf.LineAlignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.TopRight:
                        this.sf.Alignment = StringAlignment.Far;
                        this.sf.LineAlignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.MiddleLeft:
                        this.sf.Alignment = StringAlignment.Near;
                        this.sf.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.MiddleCenter:
                        this.sf.Alignment = StringAlignment.Center;
                        this.sf.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.MiddleRight:
                        this.sf.Alignment = StringAlignment.Far;
                        this.sf.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.BottomLeft:
                        this.sf.Alignment = StringAlignment.Near;
                        this.sf.LineAlignment = StringAlignment.Far;
                        break;
                    case ContentAlignment.BottomCenter:
                        this.sf.Alignment = StringAlignment.Center;
                        this.sf.LineAlignment = StringAlignment.Far;
                        break;
                    case ContentAlignment.BottomRight:
                        this.sf.Alignment = StringAlignment.Far;
                        this.sf.LineAlignment = StringAlignment.Far;
                        break;
                    default:
                        break;
                }


                this.Invalidate();
            }
        }




        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            //创建矩形
            Rectangle rectangle = new Rectangle(0, 0, this.Width, this.headHeight);
            //创建画刷
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, ChangeColor(this.themeColor, linearGradientRate), this.themeColor, LinearGradientMode.Horizontal);
            //绘制
            graphics.FillRectangle(linearGradientBrush, rectangle);


            //写文字
            SolidBrush solidBrush = new SolidBrush(this.themeForeColor);

            graphics.DrawString(this.titleText, this.Font, solidBrush, rectangle, this.sf);


            //绘制边框

            graphics.DrawRectangle(new Pen(this.borderColor), new Rectangle(0, 0, this.Width - 1, this.Height - 1));

            graphics.DrawLine(new Pen(this.borderColor), 0, this.headHeight, this.Width, this.headHeight);
        }



        private Color ChangeColor(Color color, float correctionFactor)
        {
            if (correctionFactor > 1.0f) correctionFactor = 1.0f;
            if (correctionFactor < -1.0f) correctionFactor = -1.0f;

            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            if (red < 0) red = 0;

            if (red > 255) red = 255;

            if (green < 0) green = 0;

            if (green > 255) green = 255;

            if (blue < 0) blue = 0;

            if (blue > 255) blue = 255;

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

    }
}
