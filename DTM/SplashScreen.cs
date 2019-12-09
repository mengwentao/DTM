﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DTM
{
    public partial class SplashScreen : Form
    {
        /// <summary>  
        /// 启动画面本身  
        /// </summary>  
        static SplashScreen instance;
        static string filename = "C:\\Users\\15009\\Desktop\\test.jpg";

        /// <summary>  
        /// 显示的图片  
        /// </summary>  
        Bitmap bitmap;
        public static SplashScreen Instance
        {
            get
            {
                return instance;
            }
            set
            {
                instance = value;
            }
        }
        public SplashScreen()
        {
            InitializeComponent();
            const string showInfo = "启动画面：我们正在努力的加载程序，请稍后...";
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;
            bitmap = new Bitmap(filename);
            ClientSize = bitmap.Size;

            using (Font font = new Font("Consoles", 10))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.DrawString(showInfo, font, Brushes.White, 130, 100);
                }
            }

            BackgroundImage = bitmap;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                if (bitmap != null)
                {
                    bitmap.Dispose();
                    bitmap = null;
                }
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        public static void ShowSplashScreen()
        {
            instance = new SplashScreen();
            instance.Show();
        }
        private void Welcome_Load(object sender, EventArgs e)
        {

        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
