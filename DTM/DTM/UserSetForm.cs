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
    public partial class UserSetForm : Form
    {
        public UserSetForm()
        {
            InitializeComponent();
        }
        static string connStr = ConfigurationManager.ConnectionStrings["str"].ConnectionString;
        private void button3_Click(object sender, EventArgs e)
        {
            if (LoginForm.UserType != "Administrator")
            {
                MessageBox.Show("非管理员不能操作！");
                return;
            }
            //使用sql删除语句,where 1=1 就是没有条件，等于全部数据删除
            string sql = "delete from useraccount where 1=1";
            //如果选中某行则执行
            if (dgvManager.CurrentRow.Selected)
            {
                sql = sql + " and UserId=" + Convert.ToInt32(dgvManager.CurrentRow.Cells[0].Value.ToString());
            }
            else
            {
                MessageBox.Show("没有选中行！");
                return;
            }
            int n = 0;
            //创建连接数据库对象
            MySqlConnection conn = new MySqlConnection(connStr);
            //创建操作数据库对象
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            //打开数据库
            conn.Open();
            //取得ExecuteNonQuery返回的受影响行数，无影响则为0
            n = cmd.ExecuteNonQuery();
            if (n == 0)
            {
                MessageBox.Show("删除操作失败!不存在的ID");
                conn.Close();
                return;
            }
            else if (n > 0)
            {
                MessageBox.Show("删除操作成功!");
            }
            //关闭数据库连接
            conn.Close();
            //刷新数据界面
            Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (LoginForm.UserType != "Administrator")
            {
                MessageBox.Show("非管理员不能操作！");
                return;
            }
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "" )
            {
                MessageBox.Show("所提供的数据不完整，请填写完整数据");
                return;
            }
            int n = 0;
            //更新SQL语句
            string sqlupdate = "update useraccount set UserName='" + textBox1.Text + "',UserPwd='" + textBox2.Text + "',UserType='" + comboBox1.Text + "' where UserId='" + dgvManager.CurrentRow.Cells[0].Value.ToString() + "'";
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand(sqlupdate, conn);
            conn.Open();
            n = cmd.ExecuteNonQuery();
            if (n == 0)
            {
                MessageBox.Show("修改操作失败！");
                conn.Close();
                return;
            }
            else if (n > 0)
            {
                MessageBox.Show("修改操作成功!");
            }
            conn.Close();
            Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Refresh();
        }
            public void Refresh()
        {
            if (LoginForm.UserType!= "Administrator")
            {
                MessageBox.Show("非管理员不能查看！");
                return;
            }
            //查询数据库字符串
            string sql = String.Format("select UserId '{0}',UserName '{1}',UserPwd '{2}',UserType '{3}' from useraccount", "编号", "名字", "密码", "权限");
            //连接数据库对象
            MySqlConnection conn = new MySqlConnection(connStr);
            //操作数据库对象
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            //创建表对象
            System.Data.DataTable dt = new System.Data.DataTable();
            //创建数据库填充操作对象(语句)
            MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
            //把数据填充进dt表中
            sda.Fill(dt);
            //指定dgvManager控件的数据源:dt
            dgvManager.DataSource = dt;

            //if (isAdded)
            //{
            //    if (dt.Rows.Count > 0)
            //        dgvManager.Rows[0].Selected = false;
            //    dgvManager.Rows[dt.Rows.Count - 1].Selected = true;
            //}
        }
    

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UserSetForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (LoginForm.UserType != "Administrator")
            {
                MessageBox.Show("非管理员不能注册！");
                return;
            }
            UserRegister userRegister = new UserRegister();
            userRegister.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
