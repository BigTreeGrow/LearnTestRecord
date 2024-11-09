
namespace MotionTestSystem
{
    partial class FormLogin
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmb_LoginName = new System.Windows.Forms.Button();
            this.buttonOutlogin = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comUserName = new System.Windows.Forms.ComboBox();
            this.txt_LoginPwd = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("华文隶书", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "运动控制测试系统V1.0";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-1, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(801, 78);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // cmb_LoginName
            // 
            this.cmb_LoginName.Location = new System.Drawing.Point(37, 296);
            this.cmb_LoginName.Name = "cmb_LoginName";
            this.cmb_LoginName.Size = new System.Drawing.Size(75, 23);
            this.cmb_LoginName.TabIndex = 2;
            this.cmb_LoginName.Text = "登录";
            this.cmb_LoginName.UseVisualStyleBackColor = true;
            this.cmb_LoginName.Click += new System.EventHandler(this.cmb_LoginName_Click);
            // 
            // buttonOutlogin
            // 
            this.buttonOutlogin.Location = new System.Drawing.Point(149, 296);
            this.buttonOutlogin.Name = "buttonOutlogin";
            this.buttonOutlogin.Size = new System.Drawing.Size(75, 23);
            this.buttonOutlogin.TabIndex = 3;
            this.buttonOutlogin.Text = "取消";
            this.buttonOutlogin.UseVisualStyleBackColor = true;
            this.buttonOutlogin.Click += new System.EventHandler(this.buttonOutlogin_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 196);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "用户";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 247);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "密码";
            // 
            // comUserName
            // 
            this.comUserName.FormattingEnabled = true;
            this.comUserName.Location = new System.Drawing.Point(77, 193);
            this.comUserName.Name = "comUserName";
            this.comUserName.Size = new System.Drawing.Size(121, 23);
            this.comUserName.TabIndex = 6;
            // 
            // txt_LoginPwd
            // 
            this.txt_LoginPwd.Location = new System.Drawing.Point(77, 237);
            this.txt_LoginPwd.Name = "txt_LoginPwd";
            this.txt_LoginPwd.Size = new System.Drawing.Size(121, 25);
            this.txt_LoginPwd.TabIndex = 7;
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MotionTestSystem.Properties.Resources.三轴;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(751, 411);
            this.Controls.Add(this.txt_LoginPwd);
            this.Controls.Add(this.comUserName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonOutlogin);
            this.Controls.Add(this.cmb_LoginName);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormLogin";
            this.Text = "Form1";
            this.DoubleClick += new System.EventHandler(this.FormLogin_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainPanel_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainPanel_MouseMove);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmb_LoginName;
        private System.Windows.Forms.Button buttonOutlogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comUserName;
        private System.Windows.Forms.TextBox txt_LoginPwd;
    }
}

