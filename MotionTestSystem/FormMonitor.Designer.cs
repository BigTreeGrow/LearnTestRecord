
namespace MotionTestSystem
{
    partial class FormMonitor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMonitor));
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Info = new System.Windows.Forms.Label();
            this.headPanel1 = new thinger.AutomaticStoreMotionControlLib.HeadPanel();
            this.lst_Info = new System.Windows.Forms.ListView();
            this.infoTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.info = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lbl_ScrollInfo = new SeeSharpTools.JY.GUI.ScrollingText();
            this.led_state = new SeeSharpTools.JY.GUI.LED();
            this.headPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(645, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "系统状态";
            // 
            // lbl_Info
            // 
            this.lbl_Info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Info.Location = new System.Drawing.Point(775, 33);
            this.lbl_Info.Name = "lbl_Info";
            this.lbl_Info.Size = new System.Drawing.Size(250, 30);
            this.lbl_Info.TabIndex = 1;
            this.lbl_Info.Text = "系统正常";
            this.lbl_Info.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // headPanel1
            // 
            this.headPanel1.BorderColor = System.Drawing.Color.Gray;
            this.headPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.headPanel1.Controls.Add(this.lst_Info);
            this.headPanel1.HeadHeight = 40;
            this.headPanel1.LinearGradientRate = 0.4F;
            this.headPanel1.Location = new System.Drawing.Point(662, 285);
            this.headPanel1.Name = "headPanel1";
            this.headPanel1.Size = new System.Drawing.Size(496, 289);
            this.headPanel1.TabIndex = 2;
            this.headPanel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.headPanel1.ThemeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(69)))), ((int)(((byte)(163)))));
            this.headPanel1.ThemeForeColor = System.Drawing.Color.White;
            this.headPanel1.TitleText = "系统日志";
            // 
            // lst_Info
            // 
            this.lst_Info.BackColor = System.Drawing.SystemColors.Control;
            this.lst_Info.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.infoTime,
            this.info});
            this.lst_Info.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lst_Info.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lst_Info.HideSelection = false;
            this.lst_Info.Location = new System.Drawing.Point(0, 36);
            this.lst_Info.Name = "lst_Info";
            this.lst_Info.ShowItemToolTips = true;
            this.lst_Info.Size = new System.Drawing.Size(492, 249);
            this.lst_Info.SmallImageList = this.imageList1;
            this.lst_Info.TabIndex = 0;
            this.lst_Info.UseCompatibleStateImageBehavior = false;
            this.lst_Info.View = System.Windows.Forms.View.Details;
            // 
            // infoTime
            // 
            this.infoTime.Text = "infoTime";
            this.infoTime.Width = 90;
            // 
            // info
            // 
            this.info.Text = "info";
            this.info.Width = 150;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "info.ico");
            this.imageList1.Images.SetKeyName(1, "warning.ico");
            this.imageList1.Images.SetKeyName(2, "error.ico");
            // 
            // lbl_ScrollInfo
            // 
            this.lbl_ScrollInfo.BorderColor = System.Drawing.Color.Black;
            this.lbl_ScrollInfo.BorderVisible = true;
            this.lbl_ScrollInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_ScrollInfo.Location = new System.Drawing.Point(775, 33);
            this.lbl_ScrollInfo.Name = "lbl_ScrollInfo";
            this.lbl_ScrollInfo.ScrollDirection = SeeSharpTools.JY.GUI.ScrollingText.TextDirection.RightToLeft;
            this.lbl_ScrollInfo.ScrollSpeed = 25;
            this.lbl_ScrollInfo.Size = new System.Drawing.Size(250, 30);
            this.lbl_ScrollInfo.TabIndex = 3;
            this.lbl_ScrollInfo.Text = "scrollingText1";
            this.lbl_ScrollInfo.VerticleAligment = SeeSharpTools.JY.GUI.ScrollingText.TextVerticalAlignment.Center;
            // 
            // led_state
            // 
            this.led_state.BlinkColor = System.Drawing.Color.Lime;
            this.led_state.BlinkInterval = 1000;
            this.led_state.BlinkOn = false;
            this.led_state.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.led_state.Interacton = SeeSharpTools.JY.GUI.LED.InteractionStyle.Indicator;
            this.led_state.Location = new System.Drawing.Point(1059, 27);
            this.led_state.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.led_state.Name = "led_state";
            this.led_state.OffColor = System.Drawing.Color.Red;
            this.led_state.OnColor = System.Drawing.Color.Lime;
            this.led_state.Size = new System.Drawing.Size(36, 35);
            this.led_state.Style = SeeSharpTools.JY.GUI.LED.LedStyle.Circular;
            this.led_state.TabIndex = 4;
            this.led_state.Value = true;
            // 
            // FormMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 603);
            this.Controls.Add(this.led_state);
            this.Controls.Add(this.headPanel1);
            this.Controls.Add(this.lbl_Info);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_ScrollInfo);
            this.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormMonitor";
            this.Text = "实时监控";
            this.headPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Info;
        private thinger.AutomaticStoreMotionControlLib.HeadPanel headPanel1;
        private System.Windows.Forms.ListView lst_Info;
        private System.Windows.Forms.ColumnHeader infoTime;
        private System.Windows.Forms.ColumnHeader info;
        private System.Windows.Forms.ImageList imageList1;
        private SeeSharpTools.JY.GUI.ScrollingText lbl_ScrollInfo;
        private SeeSharpTools.JY.GUI.LED led_state;
    }
}