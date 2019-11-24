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
using System.Threading;
using HZH_Controls;

namespace DTM
{
    public partial class TestForm1 : Form
    {
        string parentPath = "";
        float[] res_25 = new float[25];//储存25片的测量数据
        List<Label> label_list = new List<Label>();
        Thread th = null;
        OperateResult op_res = null;
        private ModbusTcpNet busTcpClient1 = new ModbusTcpNet("192.168.1.5");
        //private ModbusTcpNet busTcpClient2 = new ModbusTcpNet("192.168.43.198");
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
            label_init();
            Control.CheckForIllegalCrossThreadCalls = false;
           


            // dataGridView1.DataSource = dt;
        }
        public void label_init()
        {
            label_list.Add(label1); label_list.Add(label2); label_list.Add(label3); label_list.Add(label4); label_list.Add(label5);
            label_list.Add(label6); label_list.Add(label7); label_list.Add(label8); label_list.Add(label9); label_list.Add(label10);
            label_list.Add(label11); label_list.Add(label12); label_list.Add(label13); label_list.Add(label14); label_list.Add(label15);
            label_list.Add(label16); label_list.Add(label17); label_list.Add(label18); label_list.Add(label19); label_list.Add(label20);
            label_list.Add(label21); label_list.Add(label22); label_list.Add(label23); label_list.Add(label24); label_list.Add(label25);
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
        Thread th1 = null;
        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                busTcpClient1.ConnectTimeOut = 1000;
                op_res = busTcpClient1.ConnectServer();
                if (op_res.IsSuccess)
                {
                    MessageBox.Show("连接成功");
                     pictureBox1.BackColor = Color.Lime;
                   // timer1.Enabled = true;
                }
                 // busTcpClient2.ConnectServer();
                 th1 = new Thread(watch_measure_flag);//启动监听测量标志位
                th1.IsBackground = true;
                th1.Start();

            }
            catch
            {
                MessageBox.Show("plc连接异常");
            }
        }
        bool thread_flag = true;
        static short flag = 0;//PLC寄存器中记录的测量位置
        Thread th2 = null;
        public void watch_measure_flag()
        {
            while (true)
            {
                flag = busTcpClient1.ReadInt16("900").Content;
                if (flag!=0)
                {
                    measure_show(flag);
                    label27.Text = res_25[flag - 1].ToString();//显示当前测量数据
                }
               
                else
                {
                    label27.Text = "-1";//显示当前测量数据
                }
                label37.Text = flag.ToString();//显示测量的进度
                Thread.Sleep(20);
            }
            
            /* while (true) {
                 short temp = busTcpClient1.ReadInt16("900").Content;//新的标志位
                // Console.WriteLine(temp);
                // flag = busTcpClient1.ReadInt16("900").Content;

                 if (temp != 0 && flag != temp )//如果标志位发生变化
                 {

                     measure_show();//读取数据                

                 }
                 else if (flag == 0 || flag == temp && !thread_flag)
                     {
                         //th.Abort();

                         thread_flag = true;
                    // Thread.Sleep(50);
                 }
                 flag = busTcpClient1.ReadInt16("900").Content;

                // Console.WriteLine(flag);
                 if (flag != 0)
                 {
                     label27.Text = res_25[flag - 1].ToString();//显示当前测量数据

                 }
                 else
                 {
                     label27.Text = "0";//显示当前测量数据
                 }
                 //label27.Text = res_25[flag - 1].ToString();//显示当前测量数据 
                 label37.Text = flag.ToString();//显示测量的进度

                 //label_list[flag> 24? 24:flag].Text = res_25[flag > 24 ? 24 : flag].ToString();
                 Thread.Sleep(50);*/

        }
        private void button2_Click(object sender, EventArgs e)
        {
           // th.Abort();
            th1.Abort();
           // th2.Abort();
           OperateResult o = busTcpClient1.ConnectClose();
            if (o.IsSuccess)
            {
                pictureBox1.BackColor = Color.Gray;
            }
            thread_flag = true;
            //busTcpClient2.ConnectClose();            
        }
        
      /// <summary>
      /// 统计偏差，最大值，最小值，平均値等
      /// </summary>
      /// <param name="arr"></param>
      /// <returns></returns>
      public float[] tongji(float []arr)
        {
            float[] res = new float[4];
            float max = 0;
            float min = 65535;

            float sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                max = Math.Max(max, arr[i]);
                min = Math.Min(min, arr[i]);
                sum += arr[i];

            }            
            res[0] = max;
            res[1] = min;
            res[2] = max - min;
            if (arr.Length - 2 > 0)
            {
                res[3] = (sum - max - min) / (arr.Length - 2);
            }else
            {
                res[3] = (sum) / arr.Length;
            }
            
            for (int i = 0;i < res.Length;i++)
            {
                res[i] = (float)Math.Round((double)res[i], 4);//四舍五入，小数点后四位
            }
            return res;            
        }
        float[] temp = new float[4];
        List<int> huanliao_id = new List<int>();
        List<float> dp_list = new List<float>();
        /// <summary>
        /// 数据展示
        /// </summary>
        public void measure_show(short flag)
        {          
            
                int[] dt_arr = busTcpClient1.ReadInt32("1", 250).Content;
              /*  for (int i = 0;i < dt_arr.Length;i++)
                {
                    Console.Write(dt_arr[i] + " ");
                    if (i!=0 && i%10 == 0)
                    {
                        Console.WriteLine();
                    }

                }*/
                res_25 = avg_dt_arr(dt_arr);//计算出25片的测量数据
               
                //float[] res_temp = new List<float>(res_25).GetRange(0, flag + 1).ToArray();//todo数组切割
                //float[] res_temp = dp_list.ToArray();//todo数组切割
               // MessageBox.Show(res_temp.Length.ToString());
                 //onsole.WriteLine();
            //  for (int i = flag; i <= flag; i++)
            //  {
           // for (int i = 0;i < 25;i++)
            {
             //   res_25[i] = (float)Math.Round((double)res_25[i], 4);//四舍五入，小数点后四位
             //   label_list[i].Text = res_25[i].ToString();//将测试结果进行显示
            }
                    res_25[flag-1] = (float)Math.Round((double)res_25[flag -1], 6);//四舍五入，小数点后六位

                    Console.WriteLine("第" + flag +"数据"+res_25[flag-1]);
           // MessageBox.Show(flag.ToString());
                    label_list[flag-1].Text = res_25[flag-1].ToString();//将量结果进行显示
                    if (Math.Abs(res_25[flag-1] - float.Parse(textBox1.Text)) > float.Parse(textBox2.Text))//如果不符合标准
                    {
                        label_list[flag -1].ForeColor = Color.Red;
                        if(!huanliao_id.Contains(flag ))
                            huanliao_id.Add(flag );//记录换料的编号
                    }
                    else
                    {
                        label_list[flag -1].ForeColor = Color.Black;
                    }
                    th = new Thread(curveShow);//表格显示数据线程
                    th.IsBackground = true;
                    th.Start();
                //    curveShow();                   
            //    }

                string str = "";
                
                if (huanliao_id.Count > 0)
                {
                    for (int i = 0; i < huanliao_id.Count; i++)
                    {
                        str = str + huanliao_id[i] + ",";
                    }
                    label39.Text = str.Substring(0, str.Length - 1);
                }
                Console.WriteLine();
                //显示当前测量的统计结果
                temp = tongji(res_25);
                label29.Text = temp[0].ToString();
                label30.Text = temp[1].ToString();
                label33.Text = temp[2].ToString();
                label35.Text = temp[3].ToString();
               
                
             //   Thread.Sleep(10);
           
        }
        public float [] avg_dt_arr(int []arr)
        {
            float[] res = new float[25];
            for (int i = 0;i < 25;i++)
            {
                res[i] = cal_arr_avg(arr,i)/8/10000;
            }
            return res;
        }
        public float cal_arr_avg(int[] arr, int start)
        {
            float res = 0;
            float max_pre = 0;
            int min_pre = 65536;
            for (int i = start * 10; i < start * 10 + 10; i++)
            {
                res += arr[i];
                max_pre = Math.Max(max_pre, arr[i]);
                min_pre = Math.Min(min_pre, arr[i]);
            }
            //去掉最大值和最小值
            return (res- max_pre - min_pre); //取平均值//todu
        }

        public void curveShow()
        {
           
                //ucCurve1.ValueMinLeft = (float)Math.Round((double)temp[1], 3);
                //ucCurve1.ValueMaxLeft = (float)Math.Round((double)temp[0], 3);
                //ucCurve1.ValueSegment = 10;
              
                ucCurve1.SetLeftCurve("测量值", res_25);

               // for (int i = 0; i <= flag; i++)
              //  {           

                //ucCurve1.AddMarkText("测量值", count, res_25[count >24 ? 0 : count].ToString());
               // ucCurve1.AddMarkText("测量值", flag,"");

         //   }
            
            if (flag == 25)
            {
             //   ucCurve1.AddLeftAuxiliary(temp[3], Color.Blue);
            }
               
           
        }
        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            count = 0;
            //ucCurve1.
          //  curveShow();
        }
        static short count = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 800;
            count++;
            if (count > 25)
            {
                count = 0;
                timer1.Enabled = false;
            }
            //  curveShow();
            //Console.WriteLine(op_res.Message);
            OperateResult o =  busTcpClient1.Write("900", count);
           // Console.WriteLine(o.IsSuccess);
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            busTcpClient1.Write("900",Convert.ToInt16(comboBox1.Text));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            int[] dt_arr = busTcpClient1.ReadInt32("1", 250).Content;
            res_25 = avg_dt_arr(dt_arr);
            for (int i = 0;i < res_25.Length;i++)
            {
                Console.Write(res_25[i]+ "  " );
            }
            Console.WriteLine();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            busTcpClient1.Write("198",1);
           // busTcpClient1.Write("3089",true);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            busTcpClient1.Write("198", 2);
          //  busTcpClient1.Write("3090", true);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            busTcpClient1.Write("198", 3);
          //  busTcpClient1.Write("3091", true);
        }
    }
    
}
