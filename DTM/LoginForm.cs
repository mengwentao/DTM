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
using MySql.Data.MySqlClient;
using System.Configuration;


namespace DTM
{
    public partial class LoginForm : Skin_Color
    {
        private MainForm mf;
        public LoginForm()
        {
            InitializeComponent();
        }
        //定义一个全局变量 Uid;
        //用于获取登录成功后的用户名
        public static string uid;
        string connStr = ConfigurationManager.ConnectionStrings["str"].ConnectionString;
        //用于获取用户权限
        public static string UserType;


        private void Login_Btn_BtnClick(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                string sql = "select userpwd,usertype from useraccount where UserName='" + txtName.Text + "'";
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    //打开数据库
                    con.Open();
                    //使用 SqlDataReader 来 读取数据库
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        //SqlDataReader 在数据库中为 从第1条数据开始 一条一条往下读
                        if (sdr.Read()) //如果读取账户成功(文本框中的用户名在数据库中存在)
                        {
                            //则将第1条 密码 赋给 字符串pwd  ,并且依次往后读取 所有的密码
                            //Trim()方法为移除字符串前后的空白
                            string pwd = sdr.GetString(0).Trim();
                            //读取器sdr获取了2列数据 第1列为密码 第2列 即索引为1的是用户类型
                            string uType = sdr.GetString(1).Trim();
                            //如果 文本框中输入的密码 ==数据库中的密码
                            if (pwd == txtPwd.Text)
                            {
                                //说明在该账户下 密码正确, 系统登录成功
                                MessageBox.Show("登录成功，正在进入主界面......");
                                uid = txtName.Text;
                                //用于获取当前登录 用户的类型
                                UserType = uType;
                                mf = new MainForm();
                                mf.Show();
                                //TestForm1 tf = new TestForm1();
                                //tf.Show();
                                this.Hide();
                            }
                            else
                            {
                                //密码错误
                                MessageBox.Show("密码错误，请重新输入");
                                txtName.Text = "";
                            }
                        }
                        else
                        {
                            //用户名错误
                            MessageBox.Show("用户名错误，请重新输入!");
                            txtName.Text = "";
                        }
                    }
                }
            }
        }

        private void User_tb_TextChanged(object sender, EventArgs e)
        {

        }

        private void Password_tb_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
