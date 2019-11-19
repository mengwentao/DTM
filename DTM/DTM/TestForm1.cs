using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HslCommunication.ModBus;
using HslCommunication;

namespace DTM
{
    public partial class TestForm1 : Form
    {
        string parentPath = "";
        private ModbusTcpNet busTcpClient1 = new ModbusTcpNet("10.175.29.84");
        private ModbusTcpNet busTcpClient2 = new ModbusTcpNet("10.175.29.84");
        DataTable dt = new DataTable();
        public TestForm1()
        {
            InitializeComponent();
        }

        private void TestForm1_Load(object sender, EventArgs e)
        {
            Directory.SetCurrentDirectory(Directory.GetParent(Directory.GetCurrentDirectory()).FullName);
            Directory.SetCurrentDirectory(Directory.GetParent(Directory.GetCurrentDirectory()).FullName);
            parentPath = Directory.GetCurrentDirectory() + "\\Data\\curData.csv";
            Console.WriteLine(parentPath);
            OpenCSV(parentPath);
           // dataGridView1.DataSource = dt;
        }
        public DataTable OpenCSV(string filePath)
        {
            // Encoding encoding = Common.GetType(filePath); //Encoding.ASCII;//

            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            //StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            //string fileContent = sr.ReadToEnd();
            //encoding = sr.CurrentEncoding;
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine = null;
            string[] tableHead = null;
            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;
            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                //strLine = Common.ConvertStringUTF8(strLine, encoding);
                //strLine = Common.ConvertStringUTF8(strLine);

                if (IsFirst == true)
                {
                    tableHead = strLine.Split(',');
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    //创建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(tableHead[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    aryLine = strLine.Split(',');
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (aryLine != null && aryLine.Length > 0)
            {
                dt.DefaultView.Sort = tableHead[0] + " " + "asc";
            }

            sr.Close();
            fs.Close();
            return dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                busTcpClient1.ConnectServer();
                busTcpClient2.ConnectServer();
            }
            catch
            {
                MessageBox.Show("plc连接异常");
            }
           

            /*
              if (busTcpClient.ReadCoil("100").Content)
              {
                  MessageBox.Show("True");
              }
              else
              {
                  MessageBox.Show("false");
              }
              */
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            busTcpClient1.ConnectClose();
            busTcpClient2.ConnectClose();
            //busTcpClient = null;
            /*bool[] values = new bool[] { true, true, true, false, true, true, false, true, false, false };
            busTcpClient.Write("100", values);*/
            // 写入线圈100为通
        }

        private void button3_Click(object sender, EventArgs e)
        {
            short []a = busTcpClient1.ReadInt16("0",250).Content;
            for (int i = 0; i < a.Length;i++)
            {
                if (a[i] != 0)
                {
                    Console.WriteLine(a[i] + " " + i);
                }
               
            }
        }
    }
    
}
