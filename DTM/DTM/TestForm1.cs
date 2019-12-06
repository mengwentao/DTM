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
        public static int count_flag = 0;
        public static bool odd_even_flag = true;//测量盒子的奇数偶数的判断的标志位，用于区分显示坐标
        private ModbusTcpNet busTcpClient1 = new ModbusTcpNet("192.168.1.5");
        private ModbusTcpNet busTcpClient2 = new ModbusTcpNet("192.168.1.6");
        static string connetStr = "server=106.12.3.103;port=3501;user=root;password=MysqlPassword; database=DTM;charset=utf8";
        MySqlConnection conn = null;
        string cur_date_table = "";
        string Operator = "张三";
       

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
           // OpenCSV(parentPath);
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
                Thread.Sleep(200);
                OperateResult o = busTcpClient2.ConnectServer();
                Mysql_init();//连接数据库
                Thread.Sleep(300);
                count_num = get_count(cur_date_table);//获取已测量的数据
                label43.Text = count_num.ToString();//显示测量完成的盒数
                if (op_res.IsSuccess)
                {
                    MessageBox.Show("1连接成功");
                     pictureBox1.BackColor = Color.Lime;
                   // timer1.Enabled = true;
                }
                if (o.IsSuccess)
                {
                    MessageBox.Show("2连接成功");
                }           
                 th1 = new Thread(watch_measure_flag);//启动监听测量标志位
                th1.IsBackground = true;
                th1.Start();
                Thread thread = new Thread(new ThreadStart(jianting_huanliao));//启动换料监听进程
                thread.Start();                 //启动线程

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
  
                string sql = String.Format("CREATE TABLE IF NOT EXISTS {0}(`RCno` varchar(100) NOT NULL,`Average` varchar(100) NOT NULL,`Checked` int(11) NOT NULL,`Replenished` int(11) NOT NULL,`MeasureTime` datetime NOT NULL, `Operator` varchar(100) DEFAULT NULL,`ReplenishID` varchar(100) NOT NULL, PRIMARY KEY(`RCno`)); ", cur_date_table);
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
        static int RCno = 1001;
        int choujian_set_num = 100;
        
        public void watch_measure_flag()
        {
            while (true)
            {
                flag = busTcpClient1.ReadInt16("900").Content;//监听测量进度标志位
                if (flag!=0)
                {
                    //获取盘片信息并显示
                    if (temp_flag1)//保证执行一次
                    {
                        count_num = get_count(cur_date_table);//获取已测量的数据
                        label43.Text = count_num.ToString();//显示测量完成的盒数
                         //todo,查询webapi，获取当前盒的信息
                        label48.Text = RCno.ToString();

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
                    label48.Text = "0";
                    int Checked = 0, Replenished = 0;
                    if (busTcpClient1.ReadCoil("2551").Content)//TODO,读取测量完成线圈每测完一盒进行入库，
                    {
                        count_flag++;
                       
                        try
                        {
                            string ReplenishID = "";//记录换料的编号
                           //需要抽检
                           //todo,当数据已经存在时，也会默认数量增加，例如，当抽检条件设置为每四盒，已经存入6盒，当存入第七盒时，因为
                           //存入之前还未判断是否已经保存过，所以就认为是需要抽检的。
                            if ((count_num + 2) % choujian_set_num == 0 || (count_num + 1) % choujian_set_num == 0)//如果到达了抽检的设定值
                            {
                                //busTcpClient1.Write("2557", true);//给线圈信号，进入换料站
                                MessageBox.Show("此盒请抽检");
                                Checked = 1;//抽检
                            }
                            if (huanliao_id_odd.Count > 0)//第一盒需要换料
                            {

                                for (int i = 0; i < huanliao_id_odd.Count; i++)
                                {
                                    ReplenishID = ReplenishID + huanliao_id_odd[i] + ",";                                    
                                }
                                Replenished = 1;//换料
                               
                           //     busTcpClient1.Write("2554", true);//给线圈信号，标记换料的盒子
                                
                            }
                            else if (huanliao_id_even.Count > 0)//第二盒需要补料
                            {
                                for (int i = 0; i < huanliao_id_even.Count; i++)
                                {
                                    ReplenishID = ReplenishID + huanliao_id_even[i] + ",";
                                }
                                Replenished = 1;//换料
                                
                                busTcpClient1.Write("2555", true);////给线圈信号，标记换料的盒子

                                
                            }
                            
                            //插入之前先判断是否存在，如果存在，则需要进行修改,如果不存在，则直接插入
                            string date = DateTime.Now.ToLocalTime().ToString();
                            string sql = String.Format("INSERT INTO {0}(RCno, Average, Checked, Replenished,MeasureTime,Operator,ReplenishID) VALUE('{1}', '{2}', {3}, {4},'{5}','{6}','{7}') ON DUPLICATE KEY UPDATE RCno = '{1}', Average = '{2}',Checked = {3}, Replenished = {4};", cur_date_table, RCno.ToString(), temp[3].ToString(), Checked, Replenished, date, Operator, ReplenishID,RCno.ToString(), temp[3].ToString(),Checked,Replenished);
                            int res = executeMysql(sql);
                            // MessageBox.Show(res.ToString());
                            if (res != 0)//如果写入成功
                            {
                                ReplenishID = "";
                                RCno++;                                
                                busTcpClient1.Write("2551", false);//表示读取成功，标志清除
                                count_num = get_count(cur_date_table);//更新测量数
                                label43.Text = count_num.ToString();//显示测量完成的盒数     
                                Checked = 0;
                                Replenished = 0;
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

                        if (count_num % choujian_set_num == 0)//先判断是否要进行抽检
                        {
                            busTcpClient1.Write("2557", true);//给线圈信号，到进料站进行抽检R31D
                            MessageBox.Show("要去抽检啦");
                        }else
                        {
                            if (huanliao_id_odd.Count == 0 && huanliao_id_even.Count == 0)
                            {
                                //busTcpClient1.Write("6", true);//给线圈信号，进倒盒站的信号

                                MessageBox.Show("要去倒盒站");
                            }
                             if (huanliao_id_odd.Count > 0 )//进入换料站,需要换料或者补料
                            {
                                huanliao_id_odd.Clear();
                                
                             //   busTcpClient1.Write("2554", true);//设置线圈信号，表示第一盒有问题，状态清楚需要PLC进行操作
                               // MessageBox.Show("要去换料啦");                                
                            } if (huanliao_id_even.Count > 0)
                            {
                                
                                huanliao_id_even.Clear();
                                busTcpClient1.Write("2555", true);
                            }
                        }                       
                        
                        busTcpClient1.Write("2556",false);//两盒测量完成标志
                        busTcpClient1.Write("2558", true);//两盒数据读取完成，也同时告知PLC可以进行下一步操作
                        temp_flag2 = false;
                        
                    }                  
                }
                label37.Text = flag.ToString();//显示测量的进度
                Thread.Sleep(20);
               
            }
        }
       
        public void buliao_ing(string cur_rcno,short jiou_flag)
        {           
            string str = "";
            string str2 = "";            
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            label50.Text = cur_rcno;
            //todo.利用cur_rcno通过webapi获取盒子显示数据

            if (conn == null)
            {
                conn = new MySqlConnection(connetStr);
            }
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            List<int> panpian_id = new List<int>();
            //string sql = String.Format("select * from test_table where id='{0}' and name='{1}'",id,name);
            string sql = string.Format("select ReplenishID from {0} where RCno= {1};", cur_date_table, cur_rcno);//在sql语句中定义parameter，然后再给parameter赋值
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {

                MySqlDataReader reader = cmd.ExecuteReader();//执行ExecuteReader()返回一个MySqlDataReader对象
                while (reader.Read())//初始索引是-1，执行读取下一行数据，返回值是bool
                {
                    str = reader.GetString("ReplenishID");
                    string[] tem = str.Substring(0, str.Length - 1).Split(',');
                    //string[] temp = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,".Split(',');
                    Console.WriteLine(str);
                    for (int i = 0; i < tem.Length; i++)
                    {
                        panpian_id.Add(int.Parse(tem[i]));
                    }
                }


            }
            if (jiou_flag == 2 || jiou_flag == 3)//如果是第一盒
            {
                label39.Text = str.Substring(0, str.Length - 1);//显示补料的提示信息
                //label39.Text = str.Length.ToString();
                groupBox1.Enabled = true;
                for (int i = 0; i < panpian_id.Count; i++)
                {
                    list_button1[panpian_id[i] - 1].BackColor = Color.Red;//需要补料的盘片标记红色
                }

            }
            else if (jiou_flag == 1 || jiou_flag == 4)//如果是第二盒
            {
                for (int i = 0; i < panpian_id.Count; i++)
                {
                    str2 = str2 + (panpian_id[i] + 25).ToString() + ",";
                }
                //
              //  label40.Text = str2.Length.ToString();
                label40.Text = str2.Substring(0, str2.Length - 1);
                groupBox2.Enabled = true;
                for (int i = 0; i < panpian_id.Count; i++)
                {
                    list_button2[panpian_id[i] - 1].BackColor = Color.Red;
                }
            }
            if (conn != null || conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        private void button67_Click(object sender, EventArgs e)
        {
            
        }
        //仿真测试流程
        //1.textbox输入PCno，保证PCno在当日的测试表中能够查询到
        //2.线圈2置true
        //3.寄存器0写入需要补料的盒子的次序，即第一盒、第二盒或者是第一二盒，
        //3.线圈1置true,模拟正在补料
        public void jianting_huanliao()
        {
            while (true)
            {
                while (busTcpClient2.ReadCoil("2610").Content)//读取是否在补料的状态r352
                {
                    //读取第几盒补料
                    short buliaohezi_id = busTcpClient2.ReadInt16("499").Content;//需要换料的盒子编号
                    if (busTcpClient2.ReadCoil("2609").Content)//用于流程控制，和是否补料的状态一起变true
                    {
                        //进入补料流程
                        string cur_rcno = textBox3.Text;
                        buliao_ing(cur_rcno,buliaohezi_id);
                        busTcpClient2.Write("2609",false);
                    }
                    Thread.Sleep(50);
                }
                Thread.Sleep(50);
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
                    if (count_flag %2 ==0&& !huanliao_id_odd.Contains(flag))//分别以奇偶盒记录信息
                    {                    
                        huanliao_id_odd.Add(flag);//记录换料的编号   
                  //  busTcpClient1.Write("2554", true);//设置线圈信号，表示第一盒有问题，状态清楚需要PLC进行操作                
                }
                    if(count_flag %2 !=0&& !huanliao_id_even.Contains(flag ))//偶数盒
                    {
                        huanliao_id_even.Add(flag);//记录换料的编号
                    busTcpClient1.Write("2555", true);//设置线圈信号，表示第一盒有问题，状态清楚需要PLC进行操作
                    label52.Text = huanliao_id_even.Count.ToString();
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
                ucCurve1.AddMarkText("测量值", flag -1, res_25[flag - 1].ToString());
                
            }
            else
            {
                ucCurve2.SetLeftCurve("测量值", res_25); //用于显示偶数盒
                ucCurve2.AddMarkText("测量值", flag -1 , res_25[flag - 1].ToString());
                
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
            busTcpClient2.Write("560", 1);//发送移动盘片位置 
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true,true};
            busTcpClient2.Write("559", 1);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
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

            busTcpClient2.Write("559", 2);//发送移动盘片位置
            busTcpClient2.Write("2779", temp);//计算            

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
            busTcpClient2.Write("559", 3);//发送移动盘片位置

            busTcpClient2.Write("2779", temp);//计算           

            if (this.button9.BackColor != Color.Lime)
            {
                this.button9.BackColor = Color.Lime;
            }
            else
            {
                this.button9.BackColor = Color.Gray;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 4);//发送移动盘片位置

            busTcpClient2.Write("2779", temp);//计算
           

            if (this.button18.BackColor != Color.Lime)
            {
                this.button18.BackColor = Color.Lime;
            }
            else
            {
                this.button18.BackColor = Color.Gray;
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 25);//发送移动盘片位置

            busTcpClient2.Write("2779", temp);//计算
           

            if (this.button33.BackColor != Color.Lime)
            {
                this.button33.BackColor = Color.Lime;
            }
            else
            {
                this.button33.BackColor = Color.Gray;
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 18);//发送移动盘片位置

            busTcpClient2.Write("2779", temp);//计算
           

            if (this.button29.BackColor != Color.Lime)
            {
                this.button29.BackColor = Color.Lime;
            }
            else
            {
                this.button29.BackColor = Color.Gray;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 5);//发送移动盘片位置
            busTcpClient2.Write("2779", temp);//计算          

            if (this.button17.BackColor != Color.Lime)
            {
                this.button17.BackColor = Color.Lime;
            }
            else
            {
                this.button17.BackColor = Color.Gray;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 6);//发送移动盘片位置

            busTcpClient2.Write("2779", temp);//计算          
                      

            if (this.button16.BackColor != Color.Lime)
            {
                this.button16.BackColor = Color.Lime;
            }
            else
            {
                this.button16.BackColor = Color.Gray;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 7);//发送移动盘片位置

            busTcpClient2.Write("2779", temp);//计算            

            if (this.button23.BackColor != Color.Lime)
            {
                this.button23.BackColor = Color.Lime;
            }
            else
            {
                this.button23.BackColor = Color.Gray;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //huanliao_id_odd.Clear();
            //判断是否还有未移除的盘片，如果有，进行提示
            
            busTcpClient2.Write("2611", true);//完成补料信息

        }

        private void button35_Click(object sender, EventArgs e)
        {
            huanliao_id_even.Clear();
            busTcpClient2.Write("2611", true);

        }

        private void button63_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] {true,false };
            busTcpClient1.Write("2218", temp);//伺服开
        }

        private void button64_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { false, true };
            busTcpClient1.Write("2218", temp);//伺服关
        }

        private void button66_Click(object sender, EventArgs e)
        {
            busTcpClient1.Write("2833", true);//清警告
        }

        private void button65_Click(object sender, EventArgs e)
        {
            busTcpClient1.Write("2832", true);//清错误
        }

        private void button62_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 1);//发送移动盘片位置

            busTcpClient2.Write("2779", temp);//计算            

            if (this.button62.BackColor != Color.Lime)
            {
                this.button62.BackColor = Color.Lime;
            }
            else
            {
                this.button62.BackColor = Color.Gray;
            }
        }

        private void button61_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 2);//发送移动盘片位置

            busTcpClient2.Write("2779", temp);//计算            

            if (this.button61.BackColor != Color.Lime)
            {
                this.button61.BackColor = Color.Lime;
            }
            else
            {
                this.button61.BackColor = Color.Gray;
            }
        }

        private void button60_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 3);//发送移动盘片位置

            busTcpClient2.Write("2779", temp);//计算            

            if (this.button60.BackColor != Color.Lime)
            {
                this.button60.BackColor = Color.Lime;
            }
            else
            {
                this.button60.BackColor = Color.Gray;
            }
        }

        private void button54_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 4);//发送移动盘片位置

            busTcpClient2.Write("2779", temp);//计算            

            if (this.button54.BackColor != Color.Lime)
            {
                this.button54.BackColor = Color.Lime;
            }
            else
            {
                this.button54.BackColor = Color.Gray;
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 25);//发送移动盘片位置

            busTcpClient2.Write("2779", temp);//计算            

            if (this.button36.BackColor != Color.Lime)
            {
                this.button36.BackColor = Color.Lime;
            }
            else
            {
                this.button36.BackColor = Color.Gray;
            }
        }

        private void button58_Click(object sender, EventArgs e)
        {
            busTcpClient2.Write("2262",true);
            busTcpClient2.Write("2263", false);
        }

        private void button59_Click(object sender, EventArgs e)
        {
            busTcpClient2.Write("2262", false);
            busTcpClient2.Write("2263", true);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            busTcpClient1.Write("2290", true);
        }
    }
    
}
