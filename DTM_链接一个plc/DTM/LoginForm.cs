using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;

namespace DTM
{
    public partial class LoginForm : Skin_Color
    {
        private MainForm mf;
        public LoginForm()
        {
            InitializeComponent();
        }

      

        private void Login_Btn_BtnClick(object sender, EventArgs e)
        {
            if (User_tb.Text == "test" && Password_tb.Text == "test")
            {
                mf = new MainForm();
                this.Hide();     //隐藏当前窗体  
                mf.ShowDialog();
                Application.ExitThread();
            }
        }
    }
}
