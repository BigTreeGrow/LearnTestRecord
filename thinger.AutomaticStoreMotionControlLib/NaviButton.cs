using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace thinger.AutomaticStoreMotionControlLib
{
    [DefaultEvent("ClientEvent")]
    public partial class NaviButton : UserControl
    {
      
        public NaviButton()
        {
            InitializeComponent();
            SaveBackColor = this.BackColor;
        }
        private Color SaveBackColor;
        private Image naviImage = Properties.Resources.RealData;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("导航按钮图片设置")]
        public Image NaviImage
        {
            get { return naviImage; }
            set { naviImage = value;
             
                this.pic_Mian.Image = naviImage;
            }
        }


        private string naviName = "实时监控";
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("导航按钮名称设置")]
        public string NaviName
        {
            get { return naviName; }
            set
            {
                naviName = value;
                this.lbl_Navi.Text = naviName;


            }
        }

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("单机事件")]
        public event EventHandler ClientEvent;

        private void lbl_Navi_Click(object sender, EventArgs e)
        {
            if (ClientEvent != null)
            {
                ClientEvent.Invoke(this, e);
            }
        }

        private void pic_Mian_Click(object sender, EventArgs e)
        {
            if (ClientEvent != null)
            {
                ClientEvent.Invoke(this, e);
            }
        }
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("悬浮渐变系数")]
        public float ColorDepth { get; set; } = -0.2f;


        private bool isactive = false;
        [Browsable(true)]
        [Category("自定义属性")]
        [Description("激活")]
        public bool Isactive
        {
            get { return isactive; }
            set { isactive = value; 
                //通知更新重绘
                this.Invalidate();  }
            
        }

        private int activegap = 0;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("激活方框边距")]
       
        public int  Activegap
        {
            get { return activegap; }
            set
            {
                activegap = value;
                //通知更新重绘
                this.Invalidate();
            }

        }

        private int activeheight = 4;

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("激活方框高度")]
       
        public int Activeheight
        {
            get { return activeheight; }
            set
            {
                activeheight = value;
                //通知更新重绘
                this.Invalidate();
            }

        }

        private Color  activcolor = Color.FromArgb(236,240,243);

        [Browsable(true)]
        [Category("自定义属性")]
        [Description("激活方框颜色")]

        public Color Activcolor
        {
            get { return activcolor; }
            set
            {
                activcolor = value;
                //通知更新重绘
                this.Invalidate();
            }

        }
        //Invalidate 会调用 OnPaint方法
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graphics = e.Graphics;
            Rectangle rectangle = new Rectangle(activegap, this.Height - activeheight,
                                                     this.Width - 2 * activegap, activeheight);
            if (isactive)
            {
                graphics.FillRectangle(new SolidBrush(this.activcolor), rectangle);
            }
            else
            {
                graphics.FillRectangle(new SolidBrush(this.BackColor), rectangle);
            }
        }



        [Browsable(true)]
        [Category("自定义属性")]
        [Description("权限登记")]
        public int Role { get; set; } = 0;

        private void lbl_Navi_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = ChangeColor(this.BackColor, ColorDepth);

        }

        private void lbl_Navi_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = SaveBackColor;
        }
        private void pic_Mian_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = ChangeColor(this.BackColor, ColorDepth);
        }

        private void pic_Mian_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = SaveBackColor;
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
