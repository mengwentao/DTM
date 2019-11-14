namespace DTM
{
    partial class MainForm
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
            HZH_Controls.Controls.BarChartItem barChartItem1 = new HZH_Controls.Controls.BarChartItem();
            HZH_Controls.Controls.PieItem pieItem1 = new HZH_Controls.Controls.PieItem();
            HZH_Controls.Controls.PieItem pieItem2 = new HZH_Controls.Controls.PieItem();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ucledNums2 = new HZH_Controls.Controls.UCLEDNums();
            this.cur_measure_lable = new System.Windows.Forms.Label();
            this.ucBtnExt17 = new HZH_Controls.Controls.UCBtnExt();
            this.ucCurve1 = new HZH_Controls.Controls.UCCurve();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ucBarChart3 = new HZH_Controls.Controls.UCBarChart();
            this.ucPieChart1 = new HZH_Controls.Controls.UCPieChart();
            this.ucBtnExt1 = new HZH_Controls.Controls.UCBtnExt();
            this.ucBtnExt2 = new HZH_Controls.Controls.UCBtnExt();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(662, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(433, 64);
            this.label1.TabIndex = 0;
            this.label1.Text = "DTM激光测厚系统";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ucledNums2);
            this.groupBox1.Controls.Add(this.cur_measure_lable);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(11, 278);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(413, 381);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Chartreuse;
            this.pictureBox1.Location = new System.Drawing.Point(226, 199);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(46, 46);
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 18F);
            this.label3.Location = new System.Drawing.Point(124, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 36);
            this.label3.TabIndex = 48;
            this.label3.Text = "状态：";
            // 
            // ucledNums2
            // 
            this.ucledNums2.Font = new System.Drawing.Font("楷体", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucledNums2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.ucledNums2.LineWidth = 5;
            this.ucledNums2.Location = new System.Drawing.Point(226, 89);
            this.ucledNums2.Margin = new System.Windows.Forms.Padding(4);
            this.ucledNums2.Name = "ucledNums2";
            this.ucledNums2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ucledNums2.Size = new System.Drawing.Size(186, 58);
            this.ucledNums2.TabIndex = 47;
            this.ucledNums2.Value = "1.722";
            // 
            // cur_measure_lable
            // 
            this.cur_measure_lable.AutoSize = true;
            this.cur_measure_lable.Font = new System.Drawing.Font("宋体", 18F);
            this.cur_measure_lable.Location = new System.Drawing.Point(28, 101);
            this.cur_measure_lable.Name = "cur_measure_lable";
            this.cur_measure_lable.Size = new System.Drawing.Size(231, 36);
            this.cur_measure_lable.TabIndex = 0;
            this.cur_measure_lable.Text = "当前测量值：";
            // 
            // ucBtnExt17
            // 
            this.ucBtnExt17.BackColor = System.Drawing.Color.Transparent;
            this.ucBtnExt17.BtnBackColor = System.Drawing.Color.Transparent;
            this.ucBtnExt17.BtnFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucBtnExt17.BtnForeColor = System.Drawing.Color.White;
            this.ucBtnExt17.BtnText = "开始";
            this.ucBtnExt17.ConerRadius = 5;
            this.ucBtnExt17.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExt17.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.ucBtnExt17.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnExt17.ForeColor = System.Drawing.Color.White;
            this.ucBtnExt17.IsRadius = true;
            this.ucBtnExt17.IsShowRect = false;
            this.ucBtnExt17.IsShowTips = false;
            this.ucBtnExt17.Location = new System.Drawing.Point(19, 121);
            this.ucBtnExt17.Margin = new System.Windows.Forms.Padding(0);
            this.ucBtnExt17.Name = "ucBtnExt17";
            this.ucBtnExt17.RectColor = System.Drawing.Color.Gainsboro;
            this.ucBtnExt17.RectWidth = 1;
            this.ucBtnExt17.Size = new System.Drawing.Size(159, 65);
            this.ucBtnExt17.TabIndex = 18;
            this.ucBtnExt17.TabStop = false;
            this.ucBtnExt17.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.ucBtnExt17.TipsText = "";
            // 
            // ucCurve1
            // 
            this.ucCurve1.ColorDashLines = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.ucCurve1.IntervalAbscissaText = -1;
            this.ucCurve1.IsAbscissaStrech = true;
            this.ucCurve1.Location = new System.Drawing.Point(438, 219);
            this.ucCurve1.Name = "ucCurve1";
            this.ucCurve1.Size = new System.Drawing.Size(1630, 579);
            this.ucCurve1.StrechDataCountMax = 25;
            this.ucCurve1.TabIndex = 19;
            this.ucCurve1.Title = "当前盒数据";
            this.ucCurve1.ValueMaxLeft = 300F;
            this.ucCurve1.ValueMinLeft = 30F;
            this.ucCurve1.ValueSegment = 20;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.ucBarChart3);
            this.groupBox2.Controls.Add(this.ucPieChart1);
            this.groupBox2.Location = new System.Drawing.Point(19, 874);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(2049, 494);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 18F);
            this.label2.Location = new System.Drawing.Point(571, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(231, 36);
            this.label2.TabIndex = 4;
            this.label2.Text = "今日测量情况";
            // 
            // ucBarChart3
            // 
            barChartItem1.BarBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            barChartItem1.BarPercentWidth = 0.8F;
            barChartItem1.ItemName = null;
            this.ucBarChart3.BarChartItems = new HZH_Controls.Controls.BarChartItem[] {
        barChartItem1};
            this.ucBarChart3.ColorDashLines = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.ucBarChart3.ColorLinesAndText = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.ucBarChart3.ConerRadius = 10;
            this.ucBarChart3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(220)))), ((int)(((byte)(219)))));
            this.ucBarChart3.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBarChart3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.ucBarChart3.IsRadius = false;
            this.ucBarChart3.IsShowRect = false;
            this.ucBarChart3.Location = new System.Drawing.Point(471, 89);
            this.ucBarChart3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucBarChart3.Name = "ucBarChart3";
            this.ucBarChart3.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.ucBarChart3.RectWidth = 1;
            this.ucBarChart3.ShowBarValueFormat = "{0}";
            this.ucBarChart3.ShowChartItemName = false;
            this.ucBarChart3.Size = new System.Drawing.Size(1003, 367);
            this.ucBarChart3.TabIndex = 3;
            this.ucBarChart3.Text = "ucBarChart1";
            this.ucBarChart3.Title = "测量数据分布";
            // 
            // ucPieChart1
            // 
            this.ucPieChart1.BackColor = System.Drawing.Color.Transparent;
            this.ucPieChart1.CenterOfCircleColor = System.Drawing.Color.White;
            this.ucPieChart1.CenterOfCircleWidth = 0;
            pieItem1.Name = "合格数";
            pieItem1.PieColor = System.Drawing.Color.Magenta;
            pieItem1.Value = 90;
            pieItem2.Name = "不合格数";
            pieItem2.PieColor = System.Drawing.SystemColors.WindowText;
            pieItem2.Value = 40;
            this.ucPieChart1.DataSource = new HZH_Controls.Controls.PieItem[] {
        pieItem1,
        pieItem2};
            this.ucPieChart1.IsRenderPercent = true;
            this.ucPieChart1.Location = new System.Drawing.Point(45, 119);
            this.ucPieChart1.Name = "ucPieChart1";
            this.ucPieChart1.Size = new System.Drawing.Size(342, 298);
            this.ucPieChart1.TabIndex = 2;
            this.ucPieChart1.TiTle = null;
            this.ucPieChart1.TitleFont = new System.Drawing.Font("微软雅黑", 12F);
            this.ucPieChart1.TitleFroeColor = System.Drawing.Color.Black;
            // 
            // ucBtnExt1
            // 
            this.ucBtnExt1.BackColor = System.Drawing.Color.Transparent;
            this.ucBtnExt1.BtnBackColor = System.Drawing.Color.Transparent;
            this.ucBtnExt1.BtnFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucBtnExt1.BtnForeColor = System.Drawing.Color.White;
            this.ucBtnExt1.BtnText = "暂停";
            this.ucBtnExt1.ConerRadius = 5;
            this.ucBtnExt1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExt1.FillColor = System.Drawing.Color.Red;
            this.ucBtnExt1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnExt1.ForeColor = System.Drawing.Color.White;
            this.ucBtnExt1.IsRadius = true;
            this.ucBtnExt1.IsShowRect = false;
            this.ucBtnExt1.IsShowTips = false;
            this.ucBtnExt1.Location = new System.Drawing.Point(218, 121);
            this.ucBtnExt1.Margin = new System.Windows.Forms.Padding(0);
            this.ucBtnExt1.Name = "ucBtnExt1";
            this.ucBtnExt1.RectColor = System.Drawing.Color.Gainsboro;
            this.ucBtnExt1.RectWidth = 1;
            this.ucBtnExt1.Size = new System.Drawing.Size(159, 65);
            this.ucBtnExt1.TabIndex = 19;
            this.ucBtnExt1.TabStop = false;
            this.ucBtnExt1.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.ucBtnExt1.TipsText = "";
            // 
            // ucBtnExt2
            // 
            this.ucBtnExt2.BackColor = System.Drawing.Color.Transparent;
            this.ucBtnExt2.BtnBackColor = System.Drawing.Color.Transparent;
            this.ucBtnExt2.BtnFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucBtnExt2.BtnForeColor = System.Drawing.Color.White;
            this.ucBtnExt2.BtnText = "抽检";
            this.ucBtnExt2.ConerRadius = 5;
            this.ucBtnExt2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExt2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.ucBtnExt2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ucBtnExt2.ForeColor = System.Drawing.Color.White;
            this.ucBtnExt2.IsRadius = true;
            this.ucBtnExt2.IsShowRect = false;
            this.ucBtnExt2.IsShowTips = false;
            this.ucBtnExt2.Location = new System.Drawing.Point(415, 121);
            this.ucBtnExt2.Margin = new System.Windows.Forms.Padding(0);
            this.ucBtnExt2.Name = "ucBtnExt2";
            this.ucBtnExt2.RectColor = System.Drawing.Color.Gainsboro;
            this.ucBtnExt2.RectWidth = 1;
            this.ucBtnExt2.Size = new System.Drawing.Size(159, 65);
            this.ucBtnExt2.TabIndex = 20;
            this.ucBtnExt2.TabStop = false;
            this.ucBtnExt2.TipsColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(30)))), ((int)(((byte)(99)))));
            this.ucBtnExt2.TipsText = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackPalace = global::DTM.Properties.Resources._19588ac08489c03c9cc6aefde7693819;
            this.BackShade = false;
            this.CaptionBackColorBottom = System.Drawing.Color.White;
            this.CaptionBackColorTop = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(2079, 1379);
            this.Controls.Add(this.ucBtnExt2);
            this.Controls.Add(this.ucBtnExt1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ucCurve1);
            this.Controls.Add(this.ucBtnExt17);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.EffectWidth = 3;
            this.Name = "MainForm";
            this.RoundStyle = CCWin.SkinClass.RoundStyle.All;
            this.ShowDrawIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Special = false;
            this.Text = "";
            this.TitleCenter = true;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private HZH_Controls.Controls.UCBtnExt ucBtnExt17;
        private HZH_Controls.Controls.UCLEDNums ucledNums2;
        private System.Windows.Forms.Label cur_measure_lable;
        private HZH_Controls.Controls.UCCurve ucCurve1;
        private System.Windows.Forms.GroupBox groupBox2;
        private HZH_Controls.Controls.UCBarChart ucBarChart3;
        private HZH_Controls.Controls.UCPieChart ucPieChart1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private HZH_Controls.Controls.UCBtnExt ucBtnExt1;
        private HZH_Controls.Controls.UCBtnExt ucBtnExt2;
    }
}

