namespace DTM
{
    partial class LoginForm
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
            this.Login_Btn = new HZH_Controls.Controls.UCBtnExt();
            this.cur_measure_lable = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Login_Btn
            // 
            this.Login_Btn.BackColor = System.Drawing.Color.Transparent;
            this.Login_Btn.BtnBackColor = System.Drawing.Color.Transparent;
            this.Login_Btn.BtnFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Login_Btn.BtnForeColor = System.Drawing.Color.White;
            this.Login_Btn.BtnText = "登陆";
            this.Login_Btn.ConerRadius = 5;
            this.Login_Btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Login_Btn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.Login_Btn.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Login_Btn.ForeColor = System.Drawing.Color.White;
            this.Login_Btn.IsRadius = true;
            this.Login_Btn.IsShowRect = false;
            this.Login_Btn.IsShowTips = false;
            this.Login_Btn.Location = new System.Drawing.Point(247, 272);
            this.Login_Btn.Margin = new System.Windows.Forms.Padding(0);
            this.Login_Btn.Name = "Login_Btn";
            this.Login_Btn.RectColor = System.Drawing.Color.Gainsboro;
            this.Login_Btn.RectWidth = 1;
            this.Login_Btn.Size = new System.Drawing.Size(124, 41);
            this.Login_Btn.TabIndex = 19;
            this.Login_Btn.TabStop = false;
            this.Login_Btn.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.Login_Btn.TipsText = "";
            this.Login_Btn.BtnClick += new System.EventHandler(this.Login_Btn_BtnClick);
            // 
            // cur_measure_lable
            // 
            this.cur_measure_lable.AutoSize = true;
            this.cur_measure_lable.BackColor = System.Drawing.Color.Transparent;
            this.cur_measure_lable.Font = new System.Drawing.Font("宋体", 18F);
            this.cur_measure_lable.Location = new System.Drawing.Point(136, 119);
            this.cur_measure_lable.Name = "cur_measure_lable";
            this.cur_measure_lable.Size = new System.Drawing.Size(73, 30);
            this.cur_measure_lable.TabIndex = 20;
            this.cur_measure_lable.Text = "工号";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Location = new System.Drawing.Point(219, 108);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtName.Multiline = true;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(209, 42);
            this.txtName.TabIndex = 21;
            this.txtName.TextChanged += new System.EventHandler(this.User_tb_TextChanged);
            // 
            // txtPwd
            // 
            this.txtPwd.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPwd.Location = new System.Drawing.Point(219, 171);
            this.txtPwd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPwd.Multiline = true;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(209, 42);
            this.txtPwd.TabIndex = 22;
            this.txtPwd.TextChanged += new System.EventHandler(this.Password_tb_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 18F);
            this.label1.Location = new System.Drawing.Point(136, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 30);
            this.label1.TabIndex = 23;
            this.label1.Text = "密码";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderPalace = global::DTM.Properties.Resources._19588ac08489c03c9cc6aefde7693819;
            this.ClientSize = new System.Drawing.Size(639, 404);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.cur_measure_lable);
            this.Controls.Add(this.Login_Btn);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MdiStretchImage = true;
            this.Name = "LoginForm";
            this.ShowDrawIcon = false;
            this.Text = "                登录";
            this.TitleCenter = true;
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HZH_Controls.Controls.UCBtnExt Login_Btn;
        private System.Windows.Forms.Label cur_measure_lable;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label label1;
    }
}