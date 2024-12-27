
namespace MotionTestSystem
{
    partial class FrmConfirmSingle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfirmSingle));
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lbl_Message = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.pic_Exit = new System.Windows.Forms.PictureBox();
            this.pic_min = new System.Windows.Forms.PictureBox();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.btn_OK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Exit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_min)).BeginInit();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(27, 92);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(40, 37);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // lbl_Message
            // 
            this.lbl_Message.AutoSize = true;
            this.lbl_Message.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lbl_Message.ForeColor = System.Drawing.Color.Black;
            this.lbl_Message.Location = new System.Drawing.Point(88, 104);
            this.lbl_Message.Name = "lbl_Message";
            this.lbl_Message.Size = new System.Drawing.Size(297, 25);
            this.lbl_Message.TabIndex = 0;
            this.lbl_Message.Text = "当前用户权限不足，请切换用户！";
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lbl_Title.ForeColor = System.Drawing.Color.White;
            this.lbl_Title.Location = new System.Drawing.Point(12, 7);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(88, 25);
            this.lbl_Title.TabIndex = 1;
            this.lbl_Title.Text = "权限不足";
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(69)))), ((int)(((byte)(163)))));
            this.TopPanel.Controls.Add(this.pic_Exit);
            this.TopPanel.Controls.Add(this.pic_min);
            this.TopPanel.Controls.Add(this.lbl_Title);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(629, 35);
            this.TopPanel.TabIndex = 0;
            this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
            // 
            // pic_Exit
            // 
            this.pic_Exit.BackColor = System.Drawing.Color.Transparent;
            this.pic_Exit.Image = global::MotionTestSystem.Properties.Resources.cancel;
            this.pic_Exit.Location = new System.Drawing.Point(558, 7);
            this.pic_Exit.Name = "pic_Exit";
            this.pic_Exit.Size = new System.Drawing.Size(18, 18);
            this.pic_Exit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Exit.TabIndex = 6;
            this.pic_Exit.TabStop = false;
            this.pic_Exit.Click += new System.EventHandler(this.pic_Exit_Click);
            this.pic_Exit.MouseEnter += new System.EventHandler(this.pic_Exit_MouseEnter);
            this.pic_Exit.MouseLeave += new System.EventHandler(this.pic_Exit_MouseLeave);
            // 
            // pic_min
            // 
            this.pic_min.BackColor = System.Drawing.Color.Transparent;
            this.pic_min.Image = global::MotionTestSystem.Properties.Resources.min;
            this.pic_min.Location = new System.Drawing.Point(470, 7);
            this.pic_min.Name = "pic_min";
            this.pic_min.Size = new System.Drawing.Size(18, 18);
            this.pic_min.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_min.TabIndex = 5;
            this.pic_min.TabStop = false;
            this.pic_min.Click += new System.EventHandler(this.pic_min_Click);
            this.pic_min.MouseEnter += new System.EventHandler(this.pic_min_MouseEnter);
            this.pic_min.MouseLeave += new System.EventHandler(this.pic_min_MouseLeave);
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(243)))));
            this.MainPanel.Controls.Add(this.btn_OK);
            this.MainPanel.Controls.Add(this.pictureBox3);
            this.MainPanel.Controls.Add(this.lbl_Message);
            this.MainPanel.Controls.Add(this.TopPanel);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(629, 352);
            this.MainPanel.TabIndex = 2;
            // 
            // btn_OK
            // 
            this.btn_OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(69)))), ((int)(((byte)(163)))));
            this.btn_OK.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OK.ForeColor = System.Drawing.Color.White;
            this.btn_OK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_OK.Location = new System.Drawing.Point(218, 216);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(105, 38);
            this.btn_OK.TabIndex = 4;
            this.btn_OK.Text = "好的";
            this.btn_OK.UseVisualStyleBackColor = false;
            // 
            // FrmConfirmSingle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 352);
            this.Controls.Add(this.MainPanel);
            this.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmConfirmSingle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormConfirm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Exit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_min)).EndInit();
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lbl_Message;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.PictureBox pic_Exit;
        private System.Windows.Forms.PictureBox pic_min;
        private System.Windows.Forms.Button btn_OK;
    }
}