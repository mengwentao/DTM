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
            this.User_tb = new System.Windows.Forms.TextBox();
            this.Password_tb = new System.Windows.Forms.TextBox();
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
            this.Login_Btn.Location = new System.Drawing.Point(274, 364);
            this.Login_Btn.Margin = new System.Windows.Forms.Padding(0);
            this.Login_Btn.Name = "Login_Btn";
            this.Login_Btn.RectColor = System.Drawing.Color.Gainsboro;
            this.Login_Btn.RectWidth = 1;
            this.Login_Btn.Size = new System.Drawing.Size(139, 50);
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
            this.cur_measure_lable.Location = new System.Drawing.Point(153, 142);
            this.cur_measure_lable.Name = "cur_measure_lable";
            this.cur_measure_lable.Size = new System.Drawing.Size(87, 36);
            this.cur_measure_lable.TabIndex = 20;
            this.cur_measure_lable.Text = "工号";
            // 
            // User_tb
            // 
            this.User_tb.Location = new System.Drawing.Point(246, 129);
            this.User_tb.Multiline = true;
            this.User_tb.Name = "User_tb";
            this.User_tb.Size = new System.Drawing.Size(235, 49);
            this.User_tb.TabIndex = 21;
            // 
            // Password_tb
            // 
            this.Password_tb.Location = new System.Drawing.Point(246, 205);
            this.Password_tb.Multiline = true;
            this.Password_tb.Name = "Password_tb";
            this.Password_tb.Size = new System.Drawing.Size(235, 49);
            this.Password_tb.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 18F);
            this.label1.Location = new System.Drawing.Point(153, 205);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 36);
            this.label1.TabIndex = 23;
            this.label1.Text = "密码";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderPalace = global::DTM.Properties.Resources._19588ac08489c03c9cc6aefde7693819;
            this.ClientSize = new System.Drawing.Size(737, 490);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Password_tb);
            this.Controls.Add(this.User_tb);
            this.Controls.Add(this.cur_measure_lable);
            this.Controls.Add(this.Login_Btn);
            this.MaximizeBox = false;
            this.MdiStretchImage = true;
            this.Name = "LoginForm";
            this.ShowDrawIcon = false;
            this.Text = "登录";
            this.TitleCenter = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HZH_Controls.Controls.UCBtnExt Login_Btn;
        private System.Windows.Forms.Label cur_measure_lable;
        private System.Windows.Forms.TextBox User_tb;
        private System.Windows.Forms.TextBox Password_tb;
        private System.Windows.Forms.Label label1;
    }
}