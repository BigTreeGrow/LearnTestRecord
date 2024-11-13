
namespace thinger.AutomaticStoreMotionControlLib
{
    partial class NaviButton
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pic_Mian = new System.Windows.Forms.PictureBox();
            this.lbl_Navi = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Mian)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_Mian
            // 
            this.pic_Mian.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pic_Mian.Image = global::thinger.AutomaticStoreMotionControlLib.Properties.Resources.RealData;
            this.pic_Mian.Location = new System.Drawing.Point(23, 3);
            this.pic_Mian.Name = "pic_Mian";
            this.pic_Mian.Size = new System.Drawing.Size(51, 46);
            this.pic_Mian.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Mian.TabIndex = 0;
            this.pic_Mian.TabStop = false;
            this.pic_Mian.Click += new System.EventHandler(this.pic_Mian_Click);
            this.pic_Mian.MouseEnter += new System.EventHandler(this.pic_Mian_MouseEnter);
            this.pic_Mian.MouseLeave += new System.EventHandler(this.pic_Mian_MouseLeave);
            // 
            // lbl_Navi
            // 
            this.lbl_Navi.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_Navi.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Navi.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_Navi.Location = new System.Drawing.Point(0, 0);
            this.lbl_Navi.Name = "lbl_Navi";
            this.lbl_Navi.Size = new System.Drawing.Size(95, 65);
            this.lbl_Navi.TabIndex = 1;
            this.lbl_Navi.Text = "实时监控";
            this.lbl_Navi.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lbl_Navi.Click += new System.EventHandler(this.lbl_Navi_Click);
            this.lbl_Navi.MouseEnter += new System.EventHandler(this.lbl_Navi_MouseEnter);
            this.lbl_Navi.MouseLeave += new System.EventHandler(this.lbl_Navi_MouseLeave);
            // 
            // NaviButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(69)))), ((int)(((byte)(163)))));
            this.Controls.Add(this.pic_Mian);
            this.Controls.Add(this.lbl_Navi);
            this.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "NaviButton";
            this.Size = new System.Drawing.Size(95, 72);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Mian)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_Mian;
        private System.Windows.Forms.Label lbl_Navi;
    }
}
