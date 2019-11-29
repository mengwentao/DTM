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
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DTM
{
    public partial class TestForm1 : Form
    {
        string parentPath = "";
        float[] res_25 = new float[25];//储存25片的测量数据
        List<Label> label_list = new List<Label>();
        Thread th = null;
        OperateResult op_res = null;
        public static bool odd_even_flag = true;//测量盒子的奇数偶数的判断的标志位，用于区分显示坐标
       // private ModbusTcpNet busTcpClient2 = new ModbusTcpNet("10.177.144.231");
        private ModbusTcpNet busTcpClient1 = new ModbusTcpNet("192.168.1.5");
        static string connetStr = "server=106.12.3.103;port=3501;user=root;password=MysqlPassword; database=DTM";
        MySqlConnection conn = null;
        string cur_date_table = "";
        DataTable dt = new DataTable();
        List<Button> list_button1 = new List<Button>();
        List<Button> list_button2 = new List<Button>();
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
            Init_Button();            
            cur_date_table = "measure_" + DateTime.Now.ToString("yyyy_MM_dd"); ;//获取当天的表格名称
            try{
                Mysql_init();//连接数据库
                count_num = get_count(cur_date_table);//获取已测量的数据
            }catch
            {
                MessageBox.Show("数据库异常");
            }
            label43.Text = count_num.ToString();//显示测量完成的盒数

          

            // dataGridView1.DataSource = dt;

        }
        public void Init_Button()
        {
            list_button1.Add(button7); list_button1.Add(button8); list_button1.Add(button9); list_button1.Add(button18); list_button1.Add(button17);
            list_button1.Add(button16); list_button1.Add(button23); list_button1.Add(button22); list_button1.Add(button21); list_button1.Add(button20);
            list_button1.Add(button19); list_button1.Add(button14); list_button1.Add(button15); list_button1.Add(button13); list_button1.Add(button24);
            list_button1.Add(button26); list_button1.Add(button25); list_button1.Add(button29); list_button1.Add(button28); list_button1.Add(button27);
            list_button1.Add(button32); list_button1.Add(button31); list_button1.Add(button30); list_button1.Add(button34); list_button1.Add(button33);

            list_button2.Add(button62); list_button2.Add(button61); list_button2.Add(button60); list_button2.Add(button54); list_button2.Add(button53);
            list_button2.Add(button52); list_button2.Add(button51); list_button2.Add(button50); list_button2.Add(button49); list_button2.Add(button48);
            list_button2.Add(button47); list_button2.Add(button57); list_button2.Add(button55); list_button2.Add(button56); list_button2.Add(button46);
            list_button2.Add(button45); list_button2.Add(button44); list_button2.Add(button43); list_button2.Add(button42); list_button2.Add(button41);
            list_button2.Add(button40); list_button2.Add(button39); list_button2.Add(button38); list_button2.Add(button37); list_button2.Add(button36);

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
               // busTcpClient2.ConnectServer();
                Mysql_init();//连接数据库
                //Thread.Sleep(300);
                count_num = get_count(cur_date_table);//获取已测量的数据
                label43.Text = count_num.ToString();//显示测量完成的盒数
                if (op_res.IsSuccess)
                {
                    MessageBox.Show("连接成功");
                     pictureBox1.BackColor = Color.Lime;
                   // timer1.Enabled = true;
                }                
                 th1 = new Thread(watch_measure_flag);//启动监听测量标志位
                th1.IsBackground = true;
                th1.Start();

            }
            catch
            {
                MessageBox.Show("连接异常");
            }
        }
        /// <summary>
        /// mysql连接初始化
        /// </summary>
        public void Mysql_init()
        {
            try
            {
                if (conn == null)
                {
                    conn = new MySqlConnection(connetStr);
                    conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句                  
                }
                //查询是否存在每日测量数据表格，如果不存在，以日期的名字进行命名，新建table
                string sql = String.Format("CREATE TABLE IF NOT EXISTS {0}(RCno VARCHAR(100) NOT NULL,Avg VARCHAR(100) NOT NULL,Checked INT,Other VARCHAR(100),PRIMARY KEY (RCno)); ", cur_date_table);
                executeMysql(sql);
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                conn.Close();
            }
            finally
            {
                //
            }
        }
        /// <summary>
        /// 执行mysql语句
        /// </summary>
        /// <param name="sql"></param>
        public int executeMysql(string sql)
        {
            int result = 0;
            if (conn == null)
            {
                conn = new MySqlConnection(connetStr);
            }
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                MySqlCommand sqlInsert = new MySqlCommand(sql, conn);
                result = sqlInsert.ExecuteNonQuery();//3.执行插入、删除、更改语句。执行成功返回受影响的数据的行数，返回1可做true判断。执行失败不返回任何数据，报错，
                if (result == 1)
                {
                    // richTextBox1.AppendText("增加成功" + "\r\n");
                }
            }
            catch (MySqlException ex)
            {
                //richTextBox1.AppendText(ex.Message + "\r\n");
            }
            return result;
        }
        /// <summary>
        /// 查询测量个数
        /// </summary>
        /// <returns>返回已经测量的值</returns>
        public int get_count(string para)
        {
            int res = 0;
            if (conn == null)
            {
                conn = new MySqlConnection(connetStr);
            }
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }           
            string sql = String.Format("select count(*) as count from {0}", para);
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                MySqlDataReader reader = cmd.ExecuteReader();//执行ExecuteReader()返回一个MySqlDataReader对象

                if (reader.HasRows)
                {
                    reader.Read();
                    res = int.Parse(reader.GetString("count"));
                }               
            }
            if (conn != null || conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            return res;
        }
        bool thread_flag = true;
        static short flag = 0;//PLC寄存器中记录的测量位置
        Thread th2 = null;
        bool temp_flag = true;
        bool temp_flag1 = true;
        bool temp_flag2 = true;
        static int count_num = 0;
        static int RCno = 1000;
        public void watch_measure_flag()
        {
            while (true)
            {
                flag = busTcpClient1.ReadInt16("900").Content;
                if (flag!=0)
                {
                    //获取盘片信息并显示
                    if (temp_flag1)//保证执行一次
                    {
                        count_num = get_count(cur_date_table);//获取已测量的数据
                        label43.Text = count_num.ToString();//显示测量完成的盒数
                        //todo,查询webapi，获取当前盒的信息
                       
                        //Thread.Sleep(1000);
                        temp_flag1 = false;
                        // count_num++;//数据是通过数据库获的
                       
                    }
                    if (flag==25&&temp_flag)//保证25片执行一次，防止char反复刷新
                    {
                        measure_show(flag);
                        temp_flag = false;
                        busTcpClient1.Write("900",0);//清0
                    }
                    else if (flag != 25)
                    {
                        measure_show(flag);
                    }                    

                        label27.Text = res_25[flag - 1].ToString();//显示当前测量数据
                    temp_flag2 = true;
                }
               
                else if(flag ==0 )//测量完成
                {
                    if (busTcpClient1.ReadCoil("2551").Content)//TODO,读取测量完成线圈每测完一盒进行入库，
                    {
                        try
                        {   //插入之前先判断是否存在，如果存在，则需要进行修改,如果不存在，则直接插入
                            string sql = String.Format("INSERT INTO {0}(RCno, Avg, Checked, Other) VALUE('{1}', '{2}', {3}, '{4}') ON DUPLICATE KEY UPDATE RCno = '{5}', Avg = '{6}';", cur_date_table, RCno.ToString(), temp[3].ToString(), 1, "NULL", RCno.ToString(), temp[3].ToString());
                            int res = executeMysql(sql);
                            // MessageBox.Show(res.ToString());
                            if (res != 0)//如果写入成功
                            {
                                RCno++;                                
                                busTcpClient1.Write("2551", false);//表示读取成功，标志清除
                                count_num = get_count(cur_date_table);//更新测量数
                                label43.Text = count_num.ToString();//显示测量完成的盒数
                            }
                        }
                        catch
                        {
                            MessageBox.Show("数据库写入异常");
                        }                                 
                                                                     
                    }
                   // Thread.Sleep(50);

                    label27.Text = "-1";//显示当前测量数据
                    temp_flag = true;
                    temp_flag1 = true;                                
                    if (busTcpClient1.ReadCoil("2556").Content && temp_flag2)//读取两盒测量完成的线圈，如果完成//note:要在900寄存器清零前，plc给出这个信号
                    {
                        //判断是否补料或者抽检 
                        if (huanliao_id_odd.Count > 0 )
                        {
                            MessageBox.Show("第一盒请补料");                          
                            busTcpClient1.Write("2554",true);//给线圈信号，标记换料的盒子
                        }
                        if(huanliao_id_even.Count > 0)
                        {
                            MessageBox.Show("第二盒请补料");
                            busTcpClient1.Write("2555", true);////给线圈信号，标记换料的盒子
                        }
                        if (count_num % 6 ==0)//如果到达了抽检的设定值
                        {
                            busTcpClient1.Write("2557", true);//给线圈信号，进入换料站
                            MessageBox.Show("请抽检" + count_num);
                        }
                        if(huanliao_id_odd.Count==0&& huanliao_id_even.Count ==0)
                        {
                           // busTcpClient1.Write("4", true);//给线圈信号，进入倒盒站
                            //数据入库
                            MessageBox.Show("入库");
                        }
                        if (huanliao_id_odd.Count > 0 || huanliao_id_even.Count > 0)//进入换料站,需要换料或者补料
                        {
                            //busTcpClient1.Write("4", true);//给线圈信号，进入换料站
                            //
                            //  buliaozhan(huanliao_id_odd, huanliao_id_even);//todo,何时加入使能？
                            Thread thread = new Thread(new ThreadStart(jianting_huanliao));//创建线程
                            thread.Start();                 //启动线程

                        }
                        busTcpClient1.Write("2556",false);//两盒测量完成标志
                        busTcpClient1.Write("2558", true);//两盒数据读取完成
                        temp_flag2 = false;
                        
                    }                  
                }
                label37.Text = flag.ToString();//显示测量的进度
                Thread.Sleep(20);
               
            }
        }
        public void buliaozhan(List<int> list1,List<int>list2)
        {
            string str1 = " ";
            string str2 = " ";

            if (huanliao_id_odd.Count > 0)
            {
                for (int i = 0; i < huanliao_id_odd.Count; i++)
                {
                    str1 = str1 + huanliao_id_odd[i] + ",";
                }

                label39.Text = str1.Substring(0, str1.Length - 1);

            }
            if (huanliao_id_even.Count > 0)
            {
                for (int i = 0; i < huanliao_id_even.Count; i++)
                {
                    str2 = str2 + huanliao_id_even[i] + ",";
                }
                label40.Text = str2.Substring(0, str2.Length - 1);
            }

            for (int i = 0;i < list_button1.Count;i++)
            {
                if (list1.Contains(i + 1))
                {
                    list_button1[i].BackColor = Color.Red;
                }
                if (list2.Contains(i + 1))
                {
                    list_button2[i].BackColor = Color.Red;
                }

            }          
        }
        public void jianting_huanliao()
        {
            bool temp_flag = true;
            while (temp_flag)
            {
                if (busTcpClient1.ReadCoil("2600").Content)//进入换料站
                {
                    buliaozhan(huanliao_id_odd, huanliao_id_even);//显示换料的样式                    
                    //TODO通过webapi获取盒子的信息
                    temp_flag = false;//退出线程
                    MessageBox.Show("进入换料站啦，哈哈哈哈");
                }
                Thread.Sleep(200);
            }
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
            float max = -20;
            float min = 65535;
            int num = 0;//有效数据的个数
            float sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                
                if (arr[i] > 0)//todo判断有效数据,有效条件的数据需要进行修改
                {
                    max = Math.Max(max, arr[i]);
                    min = Math.Min(min, arr[i]);
                    sum += arr[i];
                    num++;
                   
                }             

            }
            if (max == -20) max = 0;//如果全是无效的数据，则max = 0；
            if (min == 65535) min = 0;//如果全是无效的数据，则min = 0；          
            res[0] = max;
            res[1] = min;
            res[2] = max - min;
            if (num - 2 > 0)
            {
                res[3] = (sum - max - min) / (num - 2);
            }else if(num > 0 && num <= 2)
            {
                res[3] = (sum) / num;
            }
            else
            {
                res[3] = sum;
            }
            
            for (int i = 0;i < res.Length;i++)
            {
                res[i] = (float)Math.Round((double)res[i], 6);//四舍五入，小数点后6位
            }
            label45.Text = num.ToString();
            label46.Text = sum.ToString();
            return res;            
        }
        float[] temp = new float[4];
        List<int> huanliao_id_odd = new List<int>();
        List<int> huanliao_id_even = new List<int>();
        List<float> dp_list = new List<float>();
        int[] dt_arr = new int[250];
        
        /// <summary>
        /// 数据展示
        /// </summary>
        ///
        public void measure_show(short flag)
        {

            // int[] dt_arr = busTcpClient1.ReadInt32("1", 250).Content;
            ushort[] temp_dt = busTcpClient1.ReadUInt16("1001",500).Content;
            for (int j = 0;j < dt_arr.Length;j++)//进制转换
            {
                dt_arr[j] = temp_dt[j * 2] + temp_dt[j * 2 +1] * 65536;
            }
                for (int i = 0;i < dt_arr.Length;i++)//打印测试的数据
                {
                    Console.Write(dt_arr[i] + " ");
                    if (i!=0 && i%10 == 0)
                    {
                        Console.WriteLine();
                    }
                }
                res_25 = avg_dt_arr(dt_arr);//计算出25片的测量数据           
                res_25[flag-1] = (float)Math.Round((double)res_25[flag -1], 6);//四舍五入，小数点后六位
                Console.WriteLine("第" + flag +"数据"+res_25[flag-1]);
                label_list[flag-1].Text = res_25[flag-1].ToString();//将量结果进行显示
                if (Math.Abs(res_25[flag-1] - float.Parse(textBox1.Text)) > float.Parse(textBox2.Text))//如果不符合标准
                {
                     label_list[flag -1].ForeColor = Color.Red;
                    if (odd_even_flag&& !huanliao_id_odd.Contains(flag))//分别以奇偶盒记录信息
                    {                    
                        huanliao_id_odd.Add(flag);//记录换料的编号
                     }
                    if(!odd_even_flag&&!huanliao_id_even.Contains(flag ))//偶数盒
                    {
                        huanliao_id_even.Add(flag);//记录换料的编号
                    }
                

            }
                else
                {
                     label_list[flag -1].ForeColor = Color.Black;
                }
                  th = new Thread(curveShow);//表格显示数据线程
                  th.IsBackground = true;
                  th.Start();            

             
                Console.WriteLine();
                //显示当前测量的统计结果
                temp = tongji(res_25);
                label29.Text = temp[0].ToString();
                label30.Text = temp[1].ToString();
                label33.Text = temp[2].ToString();
                label35.Text = temp[3].ToString();//平均值
              
        }
        public float [] avg_dt_arr(int []arr)
        {
            float[] res = new float[25];
            for (int i = 0;i < 25;i++)
            {
                res[i] = cal_arr_avg(arr,i)/8/100000;
            }
            return res;
        }
        public float cal_arr_avg(int[] arr, int start)
        {
            long res = 0;
            long max_pre = 0;
            long min_pre = 65536 * 8;
            for (int i = start * 10; i < start * 10 + 10; i++)
            {
                res += arr[i];
                max_pre = Math.Max(max_pre, arr[i]);
                min_pre = Math.Min(min_pre, arr[i]);
            }
            //去掉最大值和最小值
            return (res- max_pre - min_pre); //取平均
        }
        
        public void curveShow()
        {

            //ucCurve1.ValueMinLeft = (float)Math.Round((double)temp[1], 3);
            //ucCurve1.ValueMaxLeft = (float)Math.Round((double)temp[0], 3);
            //ucCurve1.ValueSegment = 10;
            if (odd_even_flag)
            {
                ucCurve1.SetLeftCurve("测量值", res_25);//用于显示奇数盒
                ucCurve1.AddMarkText("测量值", flag, "");
                
            }
            else
            {
                ucCurve2.SetLeftCurve("测量值", res_25); //用于显示偶数盒
                ucCurve2.AddMarkText("测量值", flag, "");
                
            }


           
            // for (int i = 0; i <= flag; i++)
            //  {           

            //ucCurve1.AddMarkText("测量值", count, res_25[count >24 ? 0 : count].ToString());
            // ucCurve1.AddMarkText("测量值", flag,"");

            //   }

            if (flag == 25&&temp_flag)
            {
                //   ucCurve1.AddLeftAuxiliary(temp[3], Color.Blue);
                odd_even_flag = !odd_even_flag;     //显示完成后，标志位取反
               
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
            bool[] temp = new bool[] { true,true};
            busTcpClient1.Write("198",1);//发送移动盘片位置           
            busTcpClient1.Write("2360", temp);//计算
            if (this.button7.BackColor != Color.Lime)
            {
                this.button7.BackColor = Color.Lime;
            }
            else
            {
                this.button7.BackColor = Color.Gray;
            }
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            string str1 = "";
            busTcpClient1.Write("198", 2);//发送移动盘片位置
            busTcpClient1.Write("2360", temp);//计算
            if (huanliao_id_odd.Contains(2))//如果包含
            {
                huanliao_id_odd.Remove(2);//移除
            }
            for (int i = 0; i < huanliao_id_odd.Count; i++)
            {
                str1 = str1 + huanliao_id_odd[i] + ",";
            }

            label39.Text = str1.Substring(0, str1.Length - 1);//显示

            if (this.button8.BackColor != Color.Lime)
            {
                this.button8.BackColor = Color.Lime;
            }
            else
            {
                this.button8.BackColor = Color.Gray;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient1.Write("198", 3);//发送移动盘片位置

            busTcpClient1.Write("2360", temp);//计算
        }

        private void button18_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient1.Write("198", 4);//发送移动盘片位置

            busTcpClient1.Write("2360", temp);//计算
        }

        private void button33_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient1.Write("198", 25);//发送移动盘片位置

            busTcpClient1.Write("2360", temp);//计算
        }

        private void button29_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient1.Write("198", 18);//发送移动盘片位置

            busTcpClient1.Write("2360", temp);//计算
        }

        private void button17_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient1.Write("198", 5);//发送移动盘片位置

            busTcpClient1.Write("2360", temp);//计算
        }

        private void button16_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient1.Write("198", 6);//发送移动盘片位置

            busTcpClient1.Write("2360", temp);//计算
        }

        private void button23_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient1.Write("198", 7);//发送移动盘片位置

            busTcpClient1.Write("2360", temp);//计算
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //huanliao_id_odd.Clear();
            //判断是否还有未移除的盘片，如果有，进行提示
            if (huanliao_id_odd.Count > 0)
            {
                MessageBox.Show("还有盘片");
            }
            busTcpClient1.Write("3", false);//完成补料信息
        }

        private void button35_Click(object sender, EventArgs e)
        {
            huanliao_id_even.Clear();
            busTcpClient1.Write("3", false);

        }

        private void button63_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] {true,false };
            busTcpClient1.Write("2218", temp);
        }

        private void button64_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { false, true };
            busTcpClient1.Write("2218", temp);
        }

        private void button66_Click(object sender, EventArgs e)
        {
            busTcpClient1.Write("2833", true);
        }

        private void button65_Click(object sender, EventArgs e)
        {
            busTcpClient1.Write("2832", true);
        }
    }
    
}
