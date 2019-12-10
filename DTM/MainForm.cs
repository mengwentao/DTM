using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using CCWin;
using NModbus;
using System.IO;
using HZH_Controls.Controls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Net.Sockets;
using HslCommunication.ModBus;
using HslCommunication;
using System.Xml;

namespace DTM
{
    public partial class MainForm : Skin_Color
    {
        string parentPath = "";
        float[] res_25 = new float[25];//储存25片的测量数据
        DataTable dt = new DataTable();
        int q = 0;//定义合格产品数量
        int uq = 0;//定义不合格产品数数量
        //public static int boxId = 0;//盒子id
        public static string barCode ="";//条形码
        public int standard_pan_thickness = 0;//盘片厚度（标准） 
        string connStr = ConfigurationManager.ConnectionStrings["str"].ConnectionString;      
        public static bool reflag = false;//恢复启动是否完成
        List<BoxState> list = new List<BoxState>();
        public static Thread th = null;//系统线程 
        public static bool runSuccess=true;
        public static bool warning = false;
        public int number = 0;//判断是否进入盒子数量
        //private ModbusFactory modbusFactory;
        //private IModbusMaster master;
        //写线圈或写寄存器数组
        //bool[] coilsBuffer;
        int count_flag = 0;
        bool thread_flag = true;
        static short flag = 0;//PLC寄存器中记录的测量位置
        Thread th2 = null;
        bool temp_flag = true;
        bool temp_flag1 = true;
        bool temp_flag2 = true;
        static int count_num = 0;
        static string RCno = "";
        static string RCno_bulaio = "";
        int choujian_set_num = 100;
        float[] temp = new float[4];
        List<int> huanliao_id_odd = new List<int>();
        List<int> huanliao_id_even = new List<int>();
        List<float> dp_list = new List<float>();
        int[] dt_arr = new int[250];
        public static string[] numberId = new string[4];
        public static ModbusTcpNet busTcpClient1 = new ModbusTcpNet("192.168.1.5");
        public static ModbusTcpNet busTcpClient2 = new ModbusTcpNet("192.168.1.6");
        MySqlConnection conn = null;
        string cur_date_table = "";
        string Operator = "张三";
        public static bool odd_even_flag = true;//测量盒子的奇数偶数的判断的标志位，用于区分显示坐标
        static string connetStr = "server=127.0.0.1;port=3306;user=root;password=root; database=dtm;charset=utf8";
        //static string connetStr = "server=106.12.3.103;port=3501;user=root;password=MysqlPassword; database=DTM;charset=utf8";
        List<Button> list_button1 = new List<Button>();
        List<Button> list_button2 = new List<Button>();
        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        private Random random = new Random();
        private void MainForm_Load(object sender, EventArgs e)
        {
             
            //获取csv文件路径
            Directory.SetCurrentDirectory(Directory.GetParent(Directory.GetCurrentDirectory()).FullName);
            Directory.SetCurrentDirectory(Directory.GetParent(Directory.GetCurrentDirectory()).FullName);
            parentPath = Directory.GetCurrentDirectory() + "\\Data\\curData.csv";
            Console.WriteLine(parentPath);
            string [] item_set = getSetItem();

            Init_Button();

            cur_date_table = "measure_" + DateTime.Now.ToString("yyyy_MM_dd"); ;//获取当天的表格名称
            try
            {
                Mysql_init();//连接数据库
                count_num = get_count(cur_date_table);//获取已测量的数据
            }
            catch
            {
                MessageBox.Show("数据库异常");
            }
            label18.Text = count_num.ToString();//显示测量完成的盒数

            // CurveChart_Init();//表格初始化
            pieChar_Init();
            barChar_Init();
            label5.Text = "欢迎：" + LoginForm.uid;            
        }
        public void Init_Button()
        {
            list_button1.Add(button1); list_button1.Add(button2); list_button1.Add(button4); list_button1.Add(button3); list_button1.Add(button8);
            list_button1.Add(button7); list_button1.Add(button6); list_button1.Add(button5); list_button1.Add(button11); list_button1.Add(button10);
            list_button1.Add(button9); list_button1.Add(button12); list_button1.Add(button13); list_button1.Add(button15); list_button1.Add(button14);
            list_button1.Add(button17); list_button1.Add(button16); list_button1.Add(button21); list_button1.Add(button20); list_button1.Add(button19);
            list_button1.Add(button18); list_button1.Add(button25); list_button1.Add(button24); list_button1.Add(button23); list_button1.Add(button22);

            list_button2.Add(button31); list_button2.Add(button33); list_button2.Add(button37); list_button2.Add(button39); list_button2.Add(button43);
            list_button2.Add(button47); list_button2.Add(button51); list_button2.Add(button48); list_button2.Add(button49); list_button2.Add(button52);
            list_button2.Add(button50); list_button2.Add(button45); list_button2.Add(button41); list_button2.Add(button35); list_button2.Add(button29);
            list_button2.Add(button46); list_button2.Add(button44); list_button2.Add(button42); list_button2.Add(button40); list_button2.Add(button38);
            list_button2.Add(button36); list_button2.Add(button34); list_button2.Add(button32); list_button2.Add(button30); list_button2.Add(button28);

        }
        public string [] getSetItem()
        {
            string[] set_res = new string[6];
            string xmlPath = Directory.GetCurrentDirectory() + "\\AppSet\\SETXMLFile.xml";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(xmlPath);
            XmlElement xmlRoot = xmldoc.DocumentElement;
            //4、获取根结点下的子节点

            foreach (XmlNode node in xmlRoot.ChildNodes)
            {
                if (node.Name == "connectSet")
                {
                    foreach (XmlNode keys in node.ChildNodes)
                    {
                        if (keys.Name == "PLC_1")
                        {
                            set_res[0] = keys["ipaddress"].InnerText;
                            set_res[1] = keys["port"].InnerText;
                        }
                        else if (keys.Name == "PLC_2")
                        {
                            set_res[2] = keys["ipaddress"].InnerText;
                            set_res[3] = keys["port"].InnerText;
                        }
                    }
                }
                if (node.Name == "checkSet")
                {
                    set_res[4] = node["timeInterval"].InnerText;
                }
                if (node.Name == "measureSet")
                {
                    set_res[4] = node["referenceValue"].InnerText;
                    set_res[5] = node["deviationValue"].InnerText;
                }
            }
            return set_res;
        }
        public void barChar_Init()
        {
            this.ucBarChart3.SetDataSource(new double[] { 3, 12, 18,17, 8 }, new string[] { "1.715-1.716", "1.717-1.718", "1.719-7.720", "1.721-1.722", "1.723-1.724" });
        }
        public void pieChar_Init()
        {
            var pieItem = new PieItem[2];
            pieItem[0] = new PieItem
            {
                Name = "合格数:" + q,
                PieColor = Color.DarkRed,
                Value = q
            };
            pieItem[1] = new PieItem
            {
                Name = "不合格数:" + uq,
                PieColor = Color.Blue,
                Value = uq
            };

            this.ucPieChart1.SetDataSource(pieItem);
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
        public void CurveChart_Init()
        {
            OpenCSV(parentPath);
            /* string[] str = new string[25];
             for (int i = 0;i < str.Length;i++)
             {
                 str[i] = (i + 1).ToString();
             }*/

            ucCurve1.StrechDataCountMax = Convert.ToInt32(dt.Rows.Count);

            string[] str = new string[ucCurve1.StrechDataCountMax];
            for (int i = 0; i < str.Length; i++)
            {
                str[i] = dt.Rows[i]["编号"].ToString();
            }
            
            this.ucCurve1.SetCurveText(str);//x坐标表示

           // int maxSV = dt.AsEnumerable().Select(t => t.Field<int>("测量值")).Max();

            // int maxUP = dt.AsEnumerable().Select(t => t.Field<int>("UnitPrice")).Max();

           // MessageBox.Show(maxSV.ToString());
            float[] data1 = new float[ucCurve1.StrechDataCountMax];
           //获取最大值和最小值
            float min = 2;
            float max = 0;
            for (int i = 0; i < data1.Length; i++)
            {
                data1[i] = float.Parse(dt.Rows[i]["测量值"].ToString());
                min = Math.Min(data1[i],min);
                max = Math.Max(data1[i], max);
                if (data1[i] > 1.718 && data1[i] < 1.725)//获取合格数和不合格数
                {
                    q++;
                }
                else
                {
                    uq++;
                }
            }
            ucCurve1.ValueMinLeft = min;
            ucCurve1.ValueMaxLeft = max;

            ucCurve1.ValueSegment = 10;

            ucCurve1.SetLeftCurve("测量值", data1);
            for(int i = 0; i < data1.Length; i++)
            {
                ucCurve1.AddMarkText("测量值", i, data1[i].ToString());
               
            }
            ucCurve1.AddAuxiliaryLabel(new HZH_Controls.Controls.AuxiliaryLable()
            {
                LocationX = 0.8f,
                Text = "不合格数：5",
                TextBack = Brushes.Red,
                TextBrush = Brushes.White
            });

            ucCurve1.AddLeftAuxiliary(20, Color.Red);

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 电缸ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 抽检设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckSetForm checkfrm = new CheckSetForm();           
            checkfrm.ShowDialog();
        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserSetForm userfrm = new UserSetForm();
            userfrm.ShowDialog();
        }

        private void 通讯设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectSetForm confrm = new ConnectSetForm();
            confrm.ShowDialog();
        }

        private void 测量设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MeasureSetForm mesfrm = new MeasureSetForm();
            mesfrm.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DebugForm debugfrm = new DebugForm();
            debugfrm.ShowDialog();
        }
        static bool btn_flag = true;
        private void button1_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 1);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button1.BackColor != Color.Lime)
            {
                this.button1.BackColor = Color.Lime;
            }
            else
            {
                this.button1.BackColor = Color.Gray;
            }

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }
        private void Runsystem()
        {
            /*try
            {
                busTcpClient1.ConnectServer();
                busTcpClient2.ConnectServer();
            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常退出！");
                return;
            }*/
            Thread th1;
            //故障后重新运行            
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                try {
                    int number1 = 0;
                    string sql = "SELECT Count(*) As MyCount FROM preventdisaster";
                    using (MySqlCommand cmd = new MySqlCommand(sql, con))
                    {
                        //打开数据库

                        con.Open();
                        //使用 SqlDataReader 来 读取数据库
                        using (MySqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if (sdr.Read()) //如果读取账户成功(文本框中的用户名在数据库中存在)
                            {
                                //SqlDataReader 在数据库中为 从第1条数据开始 一条一条往下读                       
                                number1 = sdr.GetInt16(0);
                            }
                        }
                    }
                    if (number1 != 0)
                    {
                        sql = "select * from preventdisaster";
                        using (MySqlCommand cmd = new MySqlCommand(sql, con))
                        {
                            //打开数据库
                            //con.Open();
                            //使用 SqlDataReader 来 读取数据库
                            using (MySqlDataReader sdr = cmd.ExecuteReader())
                            {
                                while (sdr.Read()) //如果读取账户成功(文本框中的用户名在数据库中存在)
                                {
                                    string[] numberid = { sdr.GetString("NumberID1"), sdr.GetString("NumberID2") };
                                    BoxState boxState = new BoxState(list, sdr.GetInt16("positionState"), sdr.GetString("barCode"), numberid);                                    
                                    //int testFlag = sdr.GetInt16("measureState");
                                    //boxState.measureState = testFlag == 0 ? false : true;
                                    //BoxState.boxCount = sdr.GetInt16("boxCount");
                                    number = sdr.GetInt16("Number");
                                    int testFlag = sdr.GetInt16("exflag");
                                    boxState.exflag = testFlag == 0 ? false : true;
                                    testFlag = sdr.GetInt16("reach");
                                    boxState.reach = testFlag == 0 ? false : true;
                                    testFlag = sdr.GetInt16("chooseFlag");
                                    boxState.chooseFlag = testFlag == 0 ? false : true;
                                    testFlag = sdr.GetInt16("measureFlag");
                                    boxState.measureFlag = testFlag == 0 ? false : true;
                                    testFlag = sdr.GetInt16("changeFlag");
                                    boxState.changeFlag = testFlag == 0 ? false : true;
                                    testFlag = sdr.GetInt16("changeboxfirstflag");
                                    boxState.changeBoxFirstFlag = testFlag == 0 ? false : true;
                                    boxState.standard_pan_thickness = sdr.GetInt16("standardpanthickness");
                                    String teststring;
                                    /*if (!sdr.IsDBNull(8))
                                    {
                                        teststring = sdr.GetString("InList");
                                        if (teststring != "")
                                        {
                                            string[] teststr = teststring.Split(',');
                                            BoxState.InList.Clear();
                                            for (int i = 0; i < teststr.Length; i++)
                                            {
                                                BoxState.InList.Add(teststr[i]);
                                            }
                                        }
                                    }*/
                                    /*if (!sdr.IsDBNull(10))
                                    {
                                        teststring = sdr.GetString("OutList");
                                        if (teststring != "")
                                        {
                                            string[] teststr = teststring.Split(',');
                                            BoxState.OutList.Clear();
                                            for (int i = 0; i < teststr.Length; i++)
                                            {
                                                BoxState.OutList.Add(teststr[i]);
                                            }
                                        }
                                    }*/
                                    /*teststring = sdr.GetString("measurepanthicknessflag");
                                    for (int i = 0; i < teststring.Length; i++)
                                    {
                                        boxState.measure_pan_thickness_flag[i] = (teststring.Substring(i, 1) == "0" ? false : true);
                                    }*/

                                    /*teststring = sdr.GetString("measurepanthickness");
                                    string[] teststrings = teststring.Split(',');
                                    for (int i = 0; i < teststrings.Length; i++)
                                    {
                                        boxState.measure_pan_thickness[i] = Convert.ToInt16(teststrings[i]);
                                    }*/
                                    th1 = new Thread(boxState.Run);
                                    th1.IsBackground = true;
                                    th1.Start();
                                    Thread.Sleep(5);
                                }
                            }
                        }
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show("数据库连接异常！");
                    con.Close();
                }
                finally
                {
                    con.Close();
                }
             }
            Thread.Sleep(50);
            number=number == 4 ? 0 :number ;
            reflag = true;
            Thread.Sleep(100);
            th1=new Thread(Run);
            th1.IsBackground = true;
            th1.Start();
            th1=new Thread(Run1);
            th1.IsBackground = true;
            th1.Start();
            //初始化modbusmaster
            //modbusFactory = new ModbusFactory();
            //在本地测试 所以使用回环地址,modbus协议规定端口号 502
            /* try
             {
                 master = modbusFactory.CreateMaster(new TcpClient("192.168.1.5", 502));
             }
             catch (Exception ex)
             {
                 MessageBox.Show("系统异常退出！");
                 return;
             }*/
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                try
                {
                    string sql = "select * from codenumber";
                    using (MySqlCommand cmd = new MySqlCommand(sql, con))
                    {
                        con.Open();
                        using (MySqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())//如果读取账户成功
                            {
                                barCode = sdr.GetString("barCode");
                                numberId[0] = sdr.GetString("numberId0");
                                numberId[1] = sdr.GetString("numberId1");
                                numberId[2] = sdr.GetString("numberId2");
                                numberId[3] = sdr.GetString("numberId3");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    runSuccess = false;
                    MessageBox.Show("数据库连接异常！");
                    con.Close();
                }
                finally
                {
                    con.Close();
                }
            }
            try {
                while (true)
             {                       
                Thread.Sleep(10);
                    //coilsBuffer = master.ReadCoils(0,2082,1);//参数为气缸1顶起
                    if (busTcpClient1.ReadCoil("2082").Content)
                    {
                        while (true)
                        {
                            Thread.Sleep(10);
                            //coilsBuffer = master.ReadCoils(0,2083,1);//参数为气缸1落下
                            if (busTcpClient1.ReadCoil("2083").Content)
                            {                              
                                number++;
                                break;
                            }
                        }
                    }
                    else continue;               
                    /*if (BoxState.InList.Count == 0)
                    {
                        boxId = 0;
                    }
                    else
                    {
                        boxId = BoxState.InList.Last() + 1;
                    }
                    */
                    string[] numberid=new string[2];                   
                            using (MySqlConnection con = new MySqlConnection(connStr))
                            {
                                try {
                                    string sql = "select * from codenumber";
                                    using (MySqlCommand cmd = new MySqlCommand(sql, con))
                                    {
                                        con.Open();
                                        using (MySqlDataReader sdr = cmd.ExecuteReader())
                                        {
                                            while (sdr.Read())//如果读取账户成功
                                            {
                                                barCode = sdr.GetString("barCode");
                                                numberId[0] = sdr.GetString("numberId0");
                                                numberId[1] = sdr.GetString("numberId1");
                                                numberId[2] = sdr.GetString("numberId2");
                                                numberId[3] = sdr.GetString("numberId3");
                                            }
                                        }
                                    }
                                }catch(Exception ex)
                                {
                                    runSuccess = false;
                                    MessageBox.Show("数据库连接异常！");
                                    con.Close();
                                }
                                finally
                                {
                                    con.Close();
                                }
                                }                                                          
                    numberid[0] = numberId[number-1];
                    numberid[1] = numberId[number-1];                                      
                    th1 = new Thread(new BoxState(list, this.standard_pan_thickness, barCode,numberid).Run);
                    th1.IsBackground = true;
                    th1.Start();                    
                    if(number==4)number = 0;                                                                     
            }
            } catch(Exception ex)
            {
                runSuccess = false;
                MessageBox.Show("系统异常退出！");
                return;
            }      
        }       
        private void ucBtnExt17_BtnClick(object sender, EventArgs e)
        {          
            try
            {
               
                busTcpClient1.ConnectTimeOut = 1000;
                OperateResult op_res = busTcpClient1.ConnectServer();
                Thread.Sleep(200);
                OperateResult o = busTcpClient2.ConnectServer();
                Mysql_init();//连接数据库
                Thread.Sleep(300);
                count_num = get_count(cur_date_table);//获取已测量的数据
                label18.Text = count_num.ToString();//显示测量完成的盒数
                if (op_res.IsSuccess)
                {
                    MessageBox.Show("1连接成功");                    
                    //timer1.Enabled = true;
                }
                if (o.IsSuccess)
                {
                    MessageBox.Show("2连接成功");

                }
                if (op_res.IsSuccess&& o.IsSuccess){
                    pictureBox181.BackColor = Color.Lime;
                }
               
                Thread thread = new Thread(new ThreadStart(watch_measure_flag));//启动监听测量标志位
                thread.Start(); //启动线程
                th = new Thread(Runsystem);
                th.Start();
                Thread threadback = new Thread(killMainThread);
                threadback.IsBackground = true;
                threadback.Start();
                Thread thread1 = new Thread(new ThreadStart(jianting_huanliao));//启动换料监听进程
                thread1.Start();                 //启动线程
                Thread thread2 = new Thread(new ThreadStart(jianting_chengpinzhan));//启动成品站监听进程
                thread2.Start();                 //启动线程
            }
            catch (ThreadAbortException ex)
            {
                runSuccess = false;
                return;
            }
           
                      
        }
        public void jianting_chengpinzhan()
        {
            while (true)
            {
                bool[] chengpin_state = busTcpClient2.ReadCoil("2477", 3).Content;
                if (chengpin_state[0])
                {
                    MessageBox.Show("请及时取出空盒");
                }
                if (chengpin_state[1])
                {
                    MessageBox.Show("请及时取出成品盒");
                }
                if (chengpin_state[2])
                {
                    MessageBox.Show("请及时放入空成品盒");
                }
                Thread.Sleep(50);
            }
        }
        //仿真测试流程
        //1.textbox输入PCno，保证PCno在当日的测试表中能够查询到
        //2.线圈2609置true
        //3.寄存器499写入需要补料的盒子的次序，即第一盒、第二盒或者是第一二盒，
        //3.线圈2610置true,模拟正在补料
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
                        //string cur_rcno = RCno_bulaio;//todo,测试改动
                        Thread.Sleep(500);
                        string cur_rcno = RCno_bulaio;
                        buliao_ing(cur_rcno, buliaohezi_id);
                        busTcpClient2.Write("2609", false);
                    }
                    Thread.Sleep(50);
                }
                Thread.Sleep(50);
            }
        }

        public void buliao_ing(string cur_rcno, short jiou_flag)
        {
            string str = "";
            string str2 = "";
            string avg = "";
            // groupBox3.Enabled = false;
            // groupBox8.Enabled = false;
            label54.Text = cur_rcno;
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
            string sql = string.Format("select * from {0} where RCno= '{1}';", cur_date_table, cur_rcno);//在sql语句中定义parameter，然后再给parameter赋值
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {

                MySqlDataReader reader = cmd.ExecuteReader();//执行ExecuteReader()返回一个MySqlDataReader对象
                while (reader.Read())//初始索引是-1，执行读取下一行数据，返回值是bool
                {
                    str = reader.GetString("ReplenishID");
                    avg = reader.GetString("Average");
                    string[] tem = str.Substring(0, str.Length - 1).Split(',');
                    //string[] temp = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,".Split(',');
                    Console.WriteLine(str);
                    for (int i = 0; i < tem.Length; i++)
                    {
                        panpian_id.Add(int.Parse(tem[i]));
                    }
                }


            }
            label55.Text = avg;//显示补料的厚度
            if (jiou_flag == 2 || jiou_flag == 3)//如果是第一盒
            {
                str=str == "" ? "1,2,3,4," : str; 
                label4.Text = str.Substring(0, str.Length - 1);//显示补料的提示信息
                //label39.Text = str.Length.ToString();
                groupBox3.Enabled = true;
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
                str2 = str2 == "" ? "31,32,33,34," : str2; 
                label53.Text = str2.Substring(0, str2.Length - 1);
                groupBox8.Enabled = true;
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
        /// <summary>
        /// 测量监听
        /// </summary>
        public void watch_measure_flag()
        {
            while (true)
            {
                flag = busTcpClient1.ReadInt16("900").Content;//监听测量进度标志位
                if (flag != 0)
                {
                    //获取盘片信息并显示
                    if (temp_flag1)//保证执行一次
                    {
                        count_num = get_count(cur_date_table);//获取已测量的数据
                        label18.Text = count_num.ToString();//显示测量完成的盒数
                                                            //todo,查询webapi，获取当前盒的信息
                        label6.Text = RCno.ToString();

                        //Thread.Sleep(1000);
                        temp_flag1 = false;
                        // count_num++;//数据是通过数据库获的

                    }
                    if (flag == 25 && temp_flag)//保证25片执行一次，防止char反复刷新
                    {
                        measure_show(flag);
                        temp_flag = false;
                        busTcpClient1.Write("900", 0);//清0
                    }
                    else if (flag != 25)
                    {
                        measure_show(flag);
                    }

                    label1.Text = res_25[flag - 1].ToString();//显示当前测量数据
                    temp_flag2 = true;
                }

                else if (flag == 0)//测量完成
                {

                    label6.Text = "0";
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

                                busTcpClient1.Write("2554", true);//给线圈信号，标记换料的盒子

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
                            string sql = String.Format("INSERT INTO {0}(RCno, Average, Checked, Replenished,MeasureTime,Operator,ReplenishID) VALUE('{1}', '{2}', {3}, {4},'{5}','{6}','{7}') ON DUPLICATE KEY UPDATE RCno = '{1}', Average = '{2}',Checked = {3}, Replenished = {4},ReplenishID = '{7}';", cur_date_table, RCno.ToString(), temp[3].ToString(), Checked, Replenished, date, Operator, ReplenishID, RCno.ToString(), temp[3].ToString(), Checked, Replenished, ReplenishID);
                            int res = executeMysql(sql);
                            // MessageBox.Show(res.ToString());
                            if (res != 0)//如果写入成功
                            {
                                ReplenishID = "";
                                //RCno = new Random().Next(1, 20000).ToString();//todo,后续屏蔽
                                busTcpClient1.Write("2551", false);//表示读取成功，标志清除
                                count_num = get_count(cur_date_table);//更新测量数
                                label18.Text = count_num.ToString();//显示测量完成的盒数                               
                            }
                        }
                        catch
                        {
                            MessageBox.Show("数据库写入异常");
                        }

                    }
                    // Thread.Sleep(50);

                    label1.Text = "-1";//显示当前测量数据
                    temp_flag = true;
                    temp_flag1 = true;
                    if (busTcpClient1.ReadCoil("2556").Content && temp_flag2)//读取两盒测量完成的线圈，如果完成//note:要在900寄存器清零前，plc给出这个信号
                    {

                        if (count_num % choujian_set_num == 0)//先判断是否要进行抽检
                        {
                            busTcpClient1.Write("2557", true);//给线圈信号，到进料站进行抽检R31D
                            MessageBox.Show("要去抽检啦");
                        }
                        else
                        {
                            if (huanliao_id_odd.Count == 0 && huanliao_id_even.Count == 0)
                            {
                                //busTcpClient1.Write("6", true);//给线圈信号，进倒盒站的信号

                             //   MessageBox.Show("要去倒盒站");
                            }
                            else if (huanliao_id_odd.Count > 0)//进入换料站,需要换料或者补料
                            {
                                huanliao_id_odd.Clear();

                                busTcpClient1.Write("2554", true);//设置线圈信号，表示第一盒有问题，状态清楚需要PLC进行操作
                               // MessageBox.Show("要去换料啦");
                            }
                            else if (huanliao_id_even.Count > 0)
                            {

                                huanliao_id_even.Clear();
                                busTcpClient1.Write("2555", true);
                            }
                        }

                        busTcpClient1.Write("2556", false);//两盒测量完成标志
                        busTcpClient1.Write("2558", true);//两盒数据读取完成，也同时告知PLC可以进行下一步操作
                        temp_flag2 = false;

                    }
                }
                label21.Text = flag.ToString();//显示测量的进度
                Thread.Sleep(20);

            }
        }
        Thread th3 = null;
        /// <summary>
        /// 数据展示
        /// </summary>
        ///
        public void measure_show(short flag)
        {

            // int[] dt_arr = busTcpClient1.ReadInt32("1", 250).Content;
            ushort[] temp_dt = busTcpClient1.ReadUInt16("1001", 500).Content;
            for (int j = 0; j < dt_arr.Length; j++)//进制转换
            {
                dt_arr[j] = temp_dt[j * 2] + temp_dt[j * 2 + 1] * 65536;
            }
            for (int i = 0; i < dt_arr.Length; i++)//打印测试的数据
            {
                Console.Write(dt_arr[i] + " ");
                if (i != 0 && i % 10 == 0)
                {
                    Console.WriteLine();
                }
            }
            res_25 = avg_dt_arr(dt_arr);//计算出25片的测量数据           
            res_25[flag - 1] = (float)Math.Round((double)res_25[flag - 1], 6);//四舍五入，小数点后六位
            Console.WriteLine("第" + flag + "数据" + res_25[flag - 1]);
            //label_list[flag - 1].Text = res_25[flag - 1].ToString();//将量结果进行显示
            if (Math.Abs(res_25[flag - 1] - float.Parse(textBox1.Text)) > float.Parse(textBox2.Text))//如果不符合标准
            {
                //label_list[flag - 1].ForeColor = Color.Red;
                if (count_flag % 2==0 && !huanliao_id_odd.Contains(flag))//分别以奇偶盒记录信息
                {
                    huanliao_id_odd.Add(flag);//记录换料的编号          
                    busTcpClient1.Write("2554", true);
                }
                if (count_flag % 2 !=0 && !huanliao_id_even.Contains(flag))//偶数盒
                {
                    huanliao_id_even.Add(flag);//记录换料的编号
                    busTcpClient1.Write("2555", true);
                }


            }
            else
            {
                //label_list[flag - 1].ForeColor = Color.Black;
            }
            th3 = new Thread(curveShow);//表格显示数据线程
            th3.IsBackground = true;
            th3.Start();


            Console.WriteLine();
            //显示当前测量的统计结果
            temp = tongji(res_25);
            label50.Text = temp[0].ToString();
            label48.Text = temp[1].ToString();
            label37.Text = temp[2].ToString();
            label35.Text = temp[3].ToString();//平均值

        }
        /// <summary>
        /// 统计偏差，最大值，最小值，平均値等
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public float[] tongji(float[] arr)
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
            }
            else if (num > 0 && num <= 2)
            {
                res[3] = (sum) / num;
            }
            else
            {
                res[3] = sum;
            }

            for (int i = 0; i < res.Length; i++)
            {
                res[i] = (float)Math.Round((double)res[i], 6);//四舍五入，小数点后6位
            }           
            return res;
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
        public float[] avg_dt_arr(int[] arr)
        {
            float[] res = new float[25];
            for (int i = 0; i < 25; i++)
            {
                res[i] = cal_arr_avg(arr, i) / 8 / 100000;
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
            return (res - max_pre - min_pre); //取平均
        }

        public void curveShow()
        {

            //ucCurve1.ValueMinLeft = (float)Math.Round((double)temp[1], 3);
            //ucCurve1.ValueMaxLeft = (float)Math.Round((double)temp[0], 3);
            //ucCurve1.ValueSegment = 10;
           
            if (count_flag % 2 == 0)
            {
                ucCurve1.SetLeftCurve("测量值", res_25);//用于显示奇数盒
                ucCurve1.AddMarkText("测量值", flag - 1, res_25[flag - 1].ToString());

            }
            else
            {
                ucCurve2.SetLeftCurve("测量值", res_25); //用于显示偶数盒
                ucCurve2.AddMarkText("测量值", flag - 1, res_25[flag - 1].ToString());

            }



            // for (int i = 0; i <= flag; i++)
            //  {           

            //ucCurve1.AddMarkText("测量值", count, res_25[count >24 ? 0 : count].ToString());
            // ucCurve1.AddMarkText("测量值", flag,"");

            //   }

            if (flag == 25 && temp_flag)
            {
                //   ucCurve1.AddLeftAuxiliary(temp[3], Color.Blue);
                odd_even_flag = !odd_even_flag;     //显示完成后，标志位取反
                
            }


        }
        private void killMainThread()
        {

            while (true)
            {
            Thread.Sleep(1000);
            if (!runSuccess)
            {
             th.Abort();
             MessageBox.Show("系统异常退出！"); 
             return;
              } 
            }  
        }
        private void Run()
        {  //向数据库保存当前所有盒子的信息
            while (true)
            {   
                Thread.Sleep(300);
                int count = 0;              
                lock (list)
                {
                    for (int i = 0; i < 3; i++)
                    {
                    list.Remove(list.Where(p => (p.positionState == 6)).FirstOrDefault());
                    }         
                label32.Text = list.Count.ToString();               
                //List<BoxState> list = list;
                count= list.Count();
                }
                if (count == 0)
                {
                    using (MySqlConnection con = new MySqlConnection(connStr))
                    {
                        try
                        {
                            string sql = "delete from preventdisaster";
                            MySqlCommand cmd = new MySqlCommand(sql, con);
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (MySqlException ex)
                        {
                            con.Close();
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
                if (count != 0)
                {
                    using (MySqlConnection con = new MySqlConnection(connStr))
                    {
                        con.Open();
                        MySqlTransaction transaction = con.BeginTransaction();
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = con;
                        cmd.Transaction = transaction;
                        try
                        {
                            string sql = "delete from preventdisaster";
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            lock (list)
                            {
                             foreach (BoxState box_state in list)
                            {
                                if (box_state.positionState == 6) continue;
                                //String value = "";
                                //String value1 = "";
                                //String value2 = "";
                                //String value3 = "";
                                /*for (int i = 0; i < box_state.measure_pan_thickness.Length - 1; i++)
                                {
                                    value += "" + box_state.measure_pan_thickness[i] + ",";
                                }
                                value += "" + box_state.measure_pan_thickness[49];
                                for (int i = 0; i < box_state.measure_pan_thickness_flag.Length; i++)
                                {
                                    value1 += "" + (box_state.measure_pan_thickness_flag[i] == false ? 0 : 1);
                                }*/
                                /*for (int i = 0; i < BoxState.InList.Count; i++)
                                {
                                        if (BoxState.InList.Count == 1)
                                        {
                                            value2 += "" + BoxState.InList[i];
                                            break;
                                        }
                                         if(i!= BoxState.InList.Count - 1)
                                        {
                                         value2 += "" + BoxState.InList[i] + ",";
                                        }
                                        else
                                        {
                                          value2 += "" + BoxState.InList[i];
                                        }
                                        
                                    }*/
                                /*for (int i = 0; i < BoxState.OutList.Count; i++)
                                {
                                        if (BoxState.OutList.Count == 1)
                                        {
                                            value3 += "" + BoxState.OutList[i];
                                            break;
                                        }
                                        if (i != BoxState.OutList.Count - 1)
                                        {
                                            value3 += "" + BoxState.OutList[i] + ",";
                                        }else
                                        {
                                           value3 += "" + BoxState.OutList[i];
                                        }                                        
                                    }*/
                                sql = string.Format("insert into preventdisaster(positionState,chooseFlag,barCode,standardpanthickness,measureFlag,changeFlag,Number,NumberID1,NumberID2,exflag,reach,changeboxfirstflag)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", box_state.positionState, box_state.chooseFlag == false ? 0 : 1,box_state.barCode, box_state.standard_pan_thickness,box_state.measureFlag == false ? 0 : 1,box_state.changeFlag == false ? 0 : 1,number,box_state.numberId[0],box_state.numberId[1],box_state.exflag==false?0:1,box_state.reach==false?0:1,box_state.changeBoxFirstFlag==false?0:1);
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                            }
                            
                        }
                        catch (MySqlException ex)
                        {
                            transaction.Rollback();//事务ExecuteNonQuery()执行失败报错，username被设置unique
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
                }
            }
        }              
       private void Run1(){
             while (true)
                {
                Thread.Sleep(50);
                //List<BoxState> list = BoxState.list;
                lock (list)
                {
                    string value0 = "";
                    string value1 = "";
                    string value2 = "";
                    string value3 = "";
                    string value4 = "";
                    string value5 = "";
                    string value7 = "";
                    string value8 = "";
                    string value9 = "";
                    bool testflag = false;
                    bool changestateflag = false;
                    int i = 0;
                    foreach (BoxState box_state in list)
                    {
                        if (box_state.warning_loss_box != 0)
                        {
                            i++;
                        }
                        if (box_state.positionState == 0|| box_state.positionState >= 20)
                    {
                            value0 += box_state.numberId[0] + "," + box_state.numberId[1]+"   ";
                            continue;                                               
                    }
                        if (box_state.positionState == 1)
                    {
                            value1 = box_state.numberId[0] + "," + box_state.numberId[1];
                            continue;
                    }
                        if (box_state.positionState == 2)
                    {
                            testflag = true;
                            
                            if (box_state.measureFlag)
                            {
                                value2 = box_state.numberId[0] + "," + box_state.numberId[1];
                            }
                            else
                            {
                                if (box_state.exflag)
                                {
                                    value2 = box_state.numberId[0];
                                    value9 = box_state.numberId[1];
                                }
                                else
                                {
                                value2 = box_state.numberId[0];
                                value1 = box_state.numberId[1];
                                }                               
                            }
                            if (box_state.measureFlag)
                            {
                               // label6.Text = "编号：" + box_state.numberId[1];//todo，测量站的RCno
                                RCno = box_state.numberId[1];
                            }
                            else
                            {
                                //label6.Text = "编号：" + box_state.numberId[0];//todo，测量站的RCno
                                 RCno = box_state.numberId[0];
                            }                            
                            continue;
                    }
                        if (box_state.positionState == 3)
                        {
                            value3 = box_state.numberId[0] + "," + box_state.numberId[1];
                            continue;
                        }
                        if (box_state.positionState == 4)
                        {
                            if (box_state.changeFlag)
                            {
                                value4 = box_state.numberId[0];//todo,倒盒站RCno
                                value3 = box_state.numberId[1];
                            }
                            else if(!box_state.changeFlag)
                            {
                                value5 += box_state.numberId[0];
                                value4 = box_state.numberId[1];//todo,倒盒站RCno
                            }                            
                            continue;
                        }
                        if (box_state.positionState == 5)
                        {
                            value5 += box_state.numberId[0] + "," + box_state.numberId[1];
                            continue;
                        }
                        if (box_state.positionState == 7)
                        {
                            value7 = box_state.numberId[0] + "," + box_state.numberId[1];
                            continue;
                        }
                        if (box_state.positionState == 8)
                        {
                            changestateflag = true;
                            if (box_state.changeBoxFirstFlag)
                            {
                                value8 = box_state.numberId[0] + "," + box_state.numberId[1];
                                label33.Text= "编号：" + box_state.numberId[1];//todo,换料rcno
                                RCno_bulaio = box_state.numberId[1];//换料rcno
                            }
                            else
                            {
                                value8 = box_state.numberId[0];
                                value9 = box_state.numberId[1];
                                label33.Text = "编号：" + box_state.numberId[0];//todo,换料rcno
                                RCno_bulaio = box_state.numberId[0];//换料rcno
                            }
                            continue;
                        }
                        if (box_state.positionState == 9)
                        {
                            value9 += box_state.numberId[0] + "," + box_state.numberId[1]+"   ";
                            continue;
                        }
                    }
                    if (i != 0)
                    {
                        warning = true;
                    }
                    else
                    {
                        warning=false;
                    }
                    label56.Text = warning.ToString();
                    if (!testflag)
                    {
                        //label6.Text = "编号：";
                     }
                    if (!changestateflag)
                    {
                        label33.Text = "编号：";
                    }
                    label47.Text = value0;
                    label39.Text = value1;
                    label40.Text = value2;
                    label41.Text = value3;
                    label44.Text = value4;
                    label42.Text = value5;
                    label45.Text = value7;
                    label43.Text = value8;
                    label46.Text = value9;
                }
                              
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button27_Click(object sender, EventArgs e)
        {    //扫码枪触发事件
            BarCode bar = new BarCode();
            bar.Show();
        
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void ucBtnExt2_BtnClick(object sender, EventArgs e)
        {
          
            th.Abort();            
            busTcpClient1.ConnectClose();
            busTcpClient2.ConnectClose();

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void label47_Click(object sender, EventArgs e)
        {

        }
        static short count = 0;
        private void button53_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            count = 0;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

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
            OperateResult o = busTcpClient1.Write("900", count);
             Console.WriteLine(o.IsSuccess);
        }

        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button54_Click(object sender, EventArgs e)
        {
            bool temp_button = true;
            for (int i = 0;i < list_button1.Count;i++)
            {
                if (list_button1[i].BackColor == Color.Red)
                {
                    temp_button = false;
                    break;
                }
            }
            if (temp_button)
            {
                busTcpClient2.Write("2611", true);//完成补料信息
            }else
            {
                DialogResult res = MessageBox.Show("存在未完成换料的盘片", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res.ToString() == "OK")
                {
                    busTcpClient2.Write("2611", true);//完成补料信息
                }              
            } 
        }

        private void button55_Click(object sender, EventArgs e)
        {
            huanliao_id_even.Clear();
            bool temp_button = true;
            for (int i = 0; i < list_button1.Count; i++)
            {
                if (list_button1[i].BackColor == Color.Red)
                {
                    temp_button = false;
                    break;
                }
            }
            if (temp_button)
            {
                busTcpClient2.Write("2611", true);//完成补料信息
            }
            else
            {
                DialogResult res = MessageBox.Show("存在未完成换料的盘片", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res.ToString() == "OK")
                {
                    busTcpClient2.Write("2611", true);//完成补料信息
                }
            }
            
        }

        private void button56_Click(object sender, EventArgs e)
        {
            busTcpClient2.Write("2262", true);
            busTcpClient2.Write("2263", false);
        }

        private void button57_Click(object sender, EventArgs e)
        {
            busTcpClient2.Write("2262", false);
            busTcpClient2.Write("2263", true);
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 2);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button2.BackColor != Color.Lime)
            {
                this.button2.BackColor = Color.Lime;
            }
            else
            {
                this.button2.BackColor = Color.Gray;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 3);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button4.BackColor != Color.Lime)
            {
                this.button4.BackColor = Color.Lime;
            }
            else
            {
                this.button4.BackColor = Color.Gray;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 4);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button3.BackColor != Color.Lime)
            {
                this.button3.BackColor = Color.Lime;
            }
            else
            {
                this.button3.BackColor = Color.Gray;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 5);//发送移动盘片位置           
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

        private void button7_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 6);//发送移动盘片位置           
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

        private void button6_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 7);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button6.BackColor != Color.Lime)
            {
                this.button6.BackColor = Color.Lime;
            }
            else
            {
                this.button6.BackColor = Color.Gray;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 8);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button5.BackColor != Color.Lime)
            {
                this.button5.BackColor = Color.Lime;
            }
            else
            {
                this.button5.BackColor = Color.Gray;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 9);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button11.BackColor != Color.Lime)
            {
                this.button11.BackColor = Color.Lime;
            }
            else
            {
                this.button11.BackColor = Color.Gray;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 10);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button10.BackColor != Color.Lime)
            {
                this.button10.BackColor = Color.Lime;
            }
            else
            {
                this.button10.BackColor = Color.Gray;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 11);//发送移动盘片位置           
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

        private void button12_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 12);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button12.BackColor != Color.Lime)
            {
                this.button12.BackColor = Color.Lime;
            }
            else
            {
                this.button12.BackColor = Color.Gray;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 13);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button13.BackColor != Color.Lime)
            {
                this.button13.BackColor = Color.Lime;
            }
            else
            {
                this.button13.BackColor = Color.Gray;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 14);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button15.BackColor != Color.Lime)
            {
                this.button15.BackColor = Color.Lime;
            }
            else
            {
                this.button15.BackColor = Color.Gray;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 15);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button14.BackColor != Color.Lime)
            {
                this.button14.BackColor = Color.Lime;
            }
            else
            {
                this.button14.BackColor = Color.Gray;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 16);//发送移动盘片位置           
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
            busTcpClient2.Write("559", 17);//发送移动盘片位置           
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

        private void button21_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 18);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button21.BackColor != Color.Lime)
            {
                this.button21.BackColor = Color.Lime;
            }
            else
            {
                this.button21.BackColor = Color.Gray;
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 19);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button20.BackColor != Color.Lime)
            {
                this.button20.BackColor = Color.Lime;
            }
            else
            {
                this.button20.BackColor = Color.Gray;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 20);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button19.BackColor != Color.Lime)
            {
                this.button19.BackColor = Color.Lime;
            }
            else
            {
                this.button19.BackColor = Color.Gray;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 21);//发送移动盘片位置           
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

        private void button25_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 22);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button25.BackColor != Color.Lime)
            {
                this.button25.BackColor = Color.Lime;
            }
            else
            {
                this.button25.BackColor = Color.Gray;
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 23);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button24.BackColor != Color.Lime)
            {
                this.button24.BackColor = Color.Lime;
            }
            else
            {
                this.button24.BackColor = Color.Gray;
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 24);//发送移动盘片位置           
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

        private void button22_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 25);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button22.BackColor != Color.Lime)
            {
                this.button22.BackColor = Color.Lime;
            }
            else
            {
                this.button22.BackColor = Color.Gray;
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 1);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button31.BackColor != Color.Lime)
            {
                this.button31.BackColor = Color.Lime;
            }
            else
            {
                this.button31.BackColor = Color.Gray;
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 2);//发送移动盘片位置           
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

        private void button37_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 3);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button37.BackColor != Color.Lime)
            {
                this.button37.BackColor = Color.Lime;
            }
            else
            {
                this.button37.BackColor = Color.Gray;
            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 4);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button39.BackColor != Color.Lime)
            {
                this.button39.BackColor = Color.Lime;
            }
            else
            {
                this.button39.BackColor = Color.Gray;
            }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 5);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button43.BackColor != Color.Lime)
            {
                this.button43.BackColor = Color.Lime;
            }
            else
            {
                this.button43.BackColor = Color.Gray;
            }
        }

        private void button47_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 6);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button47.BackColor != Color.Lime)
            {
                this.button47.BackColor = Color.Lime;
            }
            else
            {
                this.button47.BackColor = Color.Gray;
            }
        }

        private void button51_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 7);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button51.BackColor != Color.Lime)
            {
                this.button51.BackColor = Color.Lime;
            }
            else
            {
                this.button51.BackColor = Color.Gray;
            }
        }

        private void button48_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 8);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button48.BackColor != Color.Lime)
            {
                this.button48.BackColor = Color.Lime;
            }
            else
            {
                this.button48.BackColor = Color.Gray;
            }
        }

        private void button49_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 9);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button49.BackColor != Color.Lime)
            {
                this.button49.BackColor = Color.Lime;
            }
            else
            {
                this.button49.BackColor = Color.Gray;
            }
        }

        private void button52_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 10);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button52.BackColor != Color.Lime)
            {
                this.button52.BackColor = Color.Lime;
            }
            else
            {
                this.button52.BackColor = Color.Gray;
            }
        }

        private void button50_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 11);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button50.BackColor != Color.Lime)
            {
                this.button50.BackColor = Color.Lime;
            }
            else
            {
                this.button50.BackColor = Color.Gray;
            }
        }

        private void button45_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 12);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button45.BackColor != Color.Lime)
            {
                this.button45.BackColor = Color.Lime;
            }
            else
            {
                this.button45.BackColor = Color.Gray;
            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 13);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button41.BackColor != Color.Lime)
            {
                this.button41.BackColor = Color.Lime;
            }
            else
            {
                this.button41.BackColor = Color.Gray;
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 14);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button35.BackColor != Color.Lime)
            {
                this.button35.BackColor = Color.Lime;
            }
            else
            {
                this.button35.BackColor = Color.Gray;
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 15);//发送移动盘片位置           
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

        private void button46_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 16);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button46.BackColor != Color.Lime)
            {
                this.button46.BackColor = Color.Lime;
            }
            else
            {
                this.button46.BackColor = Color.Gray;
            }
        }

        private void button44_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 17);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button44.BackColor != Color.Lime)
            {
                this.button44.BackColor = Color.Lime;
            }
            else
            {
                this.button44.BackColor = Color.Gray;
            }
        }

        private void button42_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 18);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button42.BackColor != Color.Lime)
            {
                this.button42.BackColor = Color.Lime;
            }
            else
            {
                this.button42.BackColor = Color.Gray;
            }
        }

        private void button40_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 19);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button40.BackColor != Color.Lime)
            {
                this.button40.BackColor = Color.Lime;
            }
            else
            {
                this.button40.BackColor = Color.Gray;
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 20);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button38.BackColor != Color.Lime)
            {
                this.button38.BackColor = Color.Lime;
            }
            else
            {
                this.button38.BackColor = Color.Gray;
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 21);//发送移动盘片位置           
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

        private void button34_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 22);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button34.BackColor != Color.Lime)
            {
                this.button34.BackColor = Color.Lime;
            }
            else
            {
                this.button34.BackColor = Color.Gray;
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 23);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button32.BackColor != Color.Lime)
            {
                this.button32.BackColor = Color.Lime;
            }
            else
            {
                this.button32.BackColor = Color.Gray;
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 24);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button30.BackColor != Color.Lime)
            {
                this.button30.BackColor = Color.Lime;
            }
            else
            {
                this.button30.BackColor = Color.Gray;
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            bool[] temp = new bool[] { true, true };
            busTcpClient2.Write("559", 25);//发送移动盘片位置           
            busTcpClient2.Write("2779", temp);//计算          
            if (this.button28.BackColor != Color.Lime)
            {
                this.button28.BackColor = Color.Lime;
            }
            else
            {
                this.button28.BackColor = Color.Gray;
            }
        }

        /// <summary>
        /// 清除电机测量故障
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button58_Click(object sender, EventArgs e)
        {
            
            DialogResult res = MessageBox.Show("清除凸轮故障", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                busTcpClient1.Write("2290", true);
            }
               
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            

        }

        private void ucBtnExt1_BtnClick(object sender, EventArgs e)
        {

        }

        private void pictureBox181_Click(object sender, EventArgs e)
        {

        }

        private void ucPieChart1_Load(object sender, EventArgs e)
        {

        }

        private void label57_Click(object sender, EventArgs e)
        {

        }
    }
}
