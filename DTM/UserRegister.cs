using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace DTM
{
    public partial class UserRegister : Form
    {
        public UserRegister()
        {
            InitializeComponent();
        }

        private void UserRegister_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblName.Text = "";
            lblPwd.Text = "";
            lblPwdConfirm.Text = "";
            string connstr = ConfigurationManager.ConnectionStrings["str"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connstr);
            string sql = string.Format("select username from userAccount where UserName='{0}' ", txtName.Text);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            MySqlDataReader sda = cmd.ExecuteReader();
            if (txtName.Text.Trim() == "")
            {
                lblName.Text = "用户名不能为空";
                return;
            }
            else if (txtPwd.Text.Trim() == "" || txtPwdConfirm.Text.Trim() == "")
            {
                lblPwd.Text = "密码不能为空";
                return;
            }
            else if (txtPwdConfirm.Text.Trim() != txtPwd.Text.Trim())
            {
                lblPwdConfirm.Text = "两次密码输入不同，请确认后再输";
                return;
            }
            else if (sda.Read())
            {
                lblName.Text = "用户名已存在，请重新输入";
                return;
            }
            else
            {
                conn.Close();
                MySqlConnection conninsert = new MySqlConnection(connstr);
                string insertsql = string.Format("insert into userAccount(UserName,UserPwd) values('{0}','{1}')", txtName.Text, txtPwd.Text);
                MySqlCommand cmdinsert = new MySqlCommand(insertsql, conninsert);
                conninsert.Open();
                int n = cmdinsert.ExecuteNonQuery();
                if (n == 0)
                {
                    MessageBox.Show("注册失败，请重新输入");
                }
                else
                {
                    MessageBox.Show("注册成功");
                }
                conninsert.Close();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserSetForm usersetForm = new UserSetForm();
            usersetForm.Show();
            this.Hide();
        }
    }
}
