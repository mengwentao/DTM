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
using System.Threading;

namespace DTM
{
    public partial class BarCode : Form
    {
        public BarCode()
        {
            InitializeComponent();
        }
        string connStr = ConfigurationManager.ConnectionStrings["str"].ConnectionString;
        public bool flag = false;
        private void BarCode_Load(object sender, EventArgs e)
        {
            new Thread(Run).Start();
        }
        private void Run()
        {
            while (true)
            {
                Thread.Sleep(10);
                if (!flag) continue;
                string barcode= "RccTTT"  +new Random().Next(10000, 20000).ToString();
                string Number0 = "Rcclll" +new Random().Next(20000, 30000).ToString();
                string Number1 = "Rcclll" +new Random().Next(30000, 40000).ToString();
                string Number2 = "Rcclll" +new Random().Next(40000, 50000).ToString();
                string Number3 = "Rcclll" +new Random().Next(50000, 60000).ToString();
                label6.Text =barcode;
                label7.Text =Number0;
                label8.Text =Number1;
                label9.Text =Number2;
                label10.Text=Number3;
                using (MySqlConnection con = new MySqlConnection(connStr))
                {
                    con.Open();
                    MySqlTransaction transaction = con.BeginTransaction();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = con;
                    cmd.Transaction = transaction;
                    try
                    {
                    string sql = "delete from codenumber";
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    sql = string.Format("insert into codenumber(barCode,numberId0,numberId1,numberId2,numberId3)values('{0}','{1}','{2}','{3}','{4}')", barcode, Number0, Number1, Number2, Number3);
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    } catch(Exception ex)
                    {
                        transaction.Rollback();//事务ExecuteNonQuery()执行失败报错
                        con.Close();
                    }
                    finally
                    {
                        if (con.State != ConnectionState.Closed)
                        {
                            transaction.Commit();//事务要么回滚要么提交，即Rollback()与Commit()只能执行一个
                            con.Close();
                        }    
                    }

                }
                flag = false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            flag = true;
        }
    }
}
