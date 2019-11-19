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
            timer1.Enabled = false;
            button3.Text = "开启定时";
            busTcpClient1.ConnectClose();
            busTcpClient2.ConnectClose();
            //busTcpClient = null;
            /*bool[] values = new bool[] { true, true, true, false, true, true, false, true, false, false };
            busTcpClient.Write("100", values);*/
            // 写入线圈100为通
        }

        private void button4_Click(object sender, EventArgs e)
        {
           

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2049").Content)
            {
                busTcpClient1.Write("2049", false);
                this.button5.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2049", true);
                this.button5.BackColor = Color.Lime;                
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2050").Content)
            {
                busTcpClient1.Write("2050", false);
                this.button6.BackColor = Color.Gray;
            }
            else
            {
                this.button6.BackColor = Color.Lime;
                busTcpClient1.Write("2050", true);
            }
           

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2051").Content)
            {
                busTcpClient1.Write("2051", false);
                this.button8.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2051", true);
                this.button8.BackColor = Color.Lime;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2052").Content)
            {
                busTcpClient1.Write("2052", false);
                this.button7.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2052", true);
                this.button7.BackColor = Color.Lime;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2053").Content)
            {
                busTcpClient1.Write("2053", false);
                this.button10.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2053", true);
                this.button10.BackColor = Color.Lime;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2054").Content)
            {
                busTcpClient1.Write("2054", false);
                this.button9.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2054", true);
                this.button9.BackColor = Color.Lime;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2055").Content)
            {
                busTcpClient1.Write("2055", false);
                this.button12.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2055", true);
                this.button12.BackColor = Color.Lime;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2056").Content)
            {
                busTcpClient1.Write("2056", false);
                this.button11.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2056", true);
                this.button11.BackColor = Color.Lime;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2057").Content)
            {
                busTcpClient1.Write("2057", false);
                this.button14.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2057", true);
                this.button14.BackColor = Color.Lime;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2058").Content)
            {
                busTcpClient1.Write("2058", false);
                this.button13.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2058", true);
                this.button13.BackColor = Color.Lime;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2059").Content)
            {
                busTcpClient1.Write("2059", false);
                this.button16.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2059", true);
                this.button16.BackColor = Color.Lime;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2060").Content)
            {
                busTcpClient1.Write("2060", false);
                this.button15.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2060", true);
                this.button15.BackColor = Color.Lime;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2061").Content)
            {
                busTcpClient1.Write("2061", false);
                this.button18.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2061", true);
                this.button18.BackColor = Color.Lime;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2062").Content)
            {
                busTcpClient1.Write("2062", false);
                this.button17.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2062", true);
                this.button17.BackColor = Color.Lime;
            }

        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2063").Content)
            {
                busTcpClient1.Write("2063", false);
                this.button20.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2063", true);
                this.button20.BackColor = Color.Lime;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2064").Content)
            {
                busTcpClient1.Write("2064", false);
                this.button19.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2064", true);
                this.button19.BackColor = Color.Lime;
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2065").Content)
            {
                busTcpClient1.Write("2065", false);
                this.button21.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2065", true);
                this.button21.BackColor = Color.Lime;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2106").Content)
            {
                busTcpClient1.Write("2106", false);
                this.button23.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2106", true);
                this.button23.BackColor = Color.Lime;
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2107").Content)
            {
                busTcpClient1.Write("2107", false);
                this.button22.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2107", true);
                this.button22.BackColor = Color.Lime;
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2134").Content)
            {
                busTcpClient1.Write("2134", false);
                this.button25.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2134", true);
                this.button25.BackColor = Color.Lime;
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2135").Content)
            {
                busTcpClient1.Write("2135", false);
                this.button24.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2135", true);
                this.button24.BackColor = Color.Lime;
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (busTcpClient2.ReadCoil("2177").Content)
            {
                busTcpClient2.Write("2177", false);
                this.button27.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient2.Write("2177", true);
                this.button27.BackColor = Color.Lime;
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (busTcpClient2.ReadCoil("2176").Content)
            {
                busTcpClient2.Write("2176", false);
                this.button26.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient2.Write("2176", true);
                this.button26.BackColor = Color.Lime;
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (busTcpClient2.ReadCoil("2204").Content)
            {
                busTcpClient2.Write("2204", false);
                this.button31.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient2.Write("2204", true);
                this.button31.BackColor = Color.Lime;
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (busTcpClient2.ReadCoil("2205").Content)
            {
                busTcpClient2.Write("2205", false);
                this.button30.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient2.Write("2205", true);
                this.button30.BackColor = Color.Lime;
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (busTcpClient2.ReadCoil("2238").Content)
            {
                busTcpClient2.Write("2238", false);
                this.button33.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient2.Write("2238", true);
                this.button33.BackColor = Color.Lime;
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (busTcpClient2.ReadCoil("2239").Content)
            {
                busTcpClient2.Write("2239", false);
                this.button32.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient2.Write("2239", true);
                this.button32.BackColor = Color.Lime;
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (busTcpClient2.ReadCoil("2240").Content)
            {
                busTcpClient2.Write("2240", false);
                this.button35.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient2.Write("2240", true);
                this.button35.BackColor = Color.Lime;
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (busTcpClient2.ReadCoil("2241").Content)
            {
                busTcpClient2.Write("2241", false);
                this.button34.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient2.Write("2241", true);
                this.button34.BackColor = Color.Lime;
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            if (busTcpClient2.ReadCoil("2242").Content)
            {
                busTcpClient2.Write("2242", false);
                this.button36.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient2.Write("2242", true);
                this.button36.BackColor = Color.Lime;
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            if (busTcpClient2.ReadCoil("2243").Content)
            {
                busTcpClient2.Write("2243", false);
                this.button37.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient2.Write("2243", true);
                this.button37.BackColor = Color.Lime;
            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2108").Content)
            {
                busTcpClient1.Write("2108", false);
                this.button39.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2108", true);
                this.button39.BackColor = Color.Lime;
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            if (busTcpClient1.ReadCoil("2109").Content)
            {
                busTcpClient1.Write("2109", false);
                this.button38.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient1.Write("2109", true);
                this.button38.BackColor = Color.Lime;
            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            if (busTcpClient2.ReadCoil("2156").Content)
            {
                busTcpClient2.Write("2156", false);
                this.button41.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient2.Write("2156", true);
                this.button41.BackColor = Color.Lime;
            }

        }

        private void button40_Click(object sender, EventArgs e)
        {
            if (busTcpClient2.ReadCoil("2157").Content)
            {
                busTcpClient2.Write("2157", false);
                this.button40.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient2.Write("2157", true);
                this.button40.BackColor = Color.Lime;
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (busTcpClient2.ReadCoil("2178").Content)
            {
                busTcpClient2.Write("2178", false);
                this.button29.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient2.Write("2178", true);
                this.button29.BackColor = Color.Lime;
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (busTcpClient2.ReadCoil("2179").Content)
            {
                busTcpClient2.Write("2179", false);
                this.button28.BackColor = Color.Gray;
            }
            else
            {
                busTcpClient2.Write("2179", true);
                this.button28.BackColor = Color.Lime;
            }
        }
        bool[] p0 = new bool[17];
        bool[] p1 = new bool[2];
        bool[] p2 = new bool[2];
        bool[] p3 = new bool[2];
        bool[] p33 = new bool[2];
        bool[] p4_5 = new bool[2];       
        bool[] p6_7 = new bool[6];        
        bool[] p9 = new bool[2];
        bool[] p10 = new bool[2];
        bool[] p = new bool[9];
        private void timer1_Tick(object sender, EventArgs e)
        {
            int isecond = 100;//以毫秒为单位
            timer1.Interval = isecond;//50ms触发一次

            p0 = busTcpClient1.ReadCoil("2049",17).Content;//gd1-17
            p1 = busTcpClient1.ReadCoil("2106", 2).Content;//gd18-19
            p2 = busTcpClient1.ReadCoil("2134", 2).Content;//gd22-23
            p3 = busTcpClient2.ReadCoil("2176", 2).Content;//gd26-27
            p33 = busTcpClient2.ReadCoil("2278", 2).Content;//gd28-29
            p4_5 = busTcpClient2.ReadCoil("2204", 2).Content;//gd30-31
            p6_7 = busTcpClient2.ReadCoil("2238", 6).Content;//gd32-37
            p10 = busTcpClient1.ReadCoil("2108", 2).Content;//gd20-21
            p9 = busTcpClient2.ReadCoil("2157", 2).Content;//24-25
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "开启定时器")
            {
                timer1.Enabled = true;
                button3.Text = "关闭定时器";
            }
            else
            {
                timer1.Enabled = false;
                button3.Text = "开启定时器";

            }
           
        }
    }
    
}
