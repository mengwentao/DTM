using DTM.UC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DTM
{
    public partial class DebugForm : Form
    {
        public DebugControl DC;
        public Socket newclient1;
        public Socket newclient2;
        public bool Connected;
        public Thread myThread;
        public Thread myThread1;
        public Thread myThread2;
        public Thread myThread3;
        public Thread myThread4;
        public Thread myThread5;

        public Thread myThread_1;
        public Thread myThread_2;
        public Thread myThread_3;
        public Thread myThread_4;
        public Thread myThread_5;
        public Thread myThread_6;
        public delegate void MyInvoke(string str);
        //进料站状态定义
        List<PictureBox> p1_1 = new List<PictureBox>();
        List<PictureBox> p1_2 = new List<PictureBox>();
        List<PictureBox> p1_3 = new List<PictureBox>();
        List<PictureBox> p1_4 = new List<PictureBox>();
        List<PictureBox> p1_5 = new List<PictureBox>();
        List<PictureBox> p1_6 = new List<PictureBox>();
        List<PictureBox> p1_7 = new List<PictureBox>();
        List<PictureBox> p1_8 = new List<PictureBox>();

        //测量前缓冲站
        List<PictureBox> p1_9 = new List<PictureBox>();
        List<PictureBox> p1_10 = new List<PictureBox>();
        List<PictureBox> p1_11 = new List<PictureBox>();
       
        //测量至成品
        List<PictureBox> p1_12 = new List<PictureBox>();
        List<PictureBox> p1_13 = new List<PictureBox>();
        List<PictureBox> p1_14 = new List<PictureBox>();
        List<PictureBox> p1_15 = new List<PictureBox>();
        List<PictureBox> p1_16 = new List<PictureBox>();
        List<PictureBox> p1_17 = new List<PictureBox>();
        List<PictureBox> p1_18 = new List<PictureBox>();
        List<PictureBox> p1_19 = new List<PictureBox>();
        List<PictureBox> p1_20 = new List<PictureBox>();

        List<PictureBox> p2_1 = new List<PictureBox>();
        List<PictureBox> p2_2 = new List<PictureBox>();
        List<PictureBox> p2_3 = new List<PictureBox>();
        List<PictureBox> p2_4 = new List<PictureBox>();
        List<PictureBox> p2_5 = new List<PictureBox>();
        List<PictureBox> p2_6 = new List<PictureBox>();
        List<PictureBox> p2_7 = new List<PictureBox>();
        List<PictureBox> p2_8 = new List<PictureBox>();
        List<PictureBox> p2_9 = new List<PictureBox>();
        List<PictureBox> p2_10 = new List<PictureBox>();
        List<PictureBox> p2_11 = new List<PictureBox>();
        List<PictureBox> p2_12 = new List<PictureBox>();
        List<PictureBox> p2_13 = new List<PictureBox>();
        List<PictureBox> p2_14 = new List<PictureBox>();
        List<PictureBox> p2_15 = new List<PictureBox>();
        List<PictureBox> p2_16 = new List<PictureBox>();
        List<PictureBox> p2_17 = new List<PictureBox>();
        List<PictureBox> p2_18= new List<PictureBox>();
        //电机故障
        
        List<PictureBox> p2_19 = new List<PictureBox>();
        List<PictureBox> p2_20 = new List<PictureBox>();
        
       
        //电机运行状态和气缸35和33
        
       
      
        //总伺服on和off
        List<PictureBox> p28 = new List<PictureBox>();
        public DebugForm()
        {
            InitializeComponent();
        }

        private void DebugForm_Load(object sender, EventArgs e)
        {
            //p1 =  PB_Init(panel1);
            // p2 = PB_Init(panel2);
            //  p3 = PB_Init(panel3);
           // tabControl1.SelectedIndex = 1;
            pb_init();
            button1.Enabled = false;          
            closebtn.Enabled = false;
            tabPage1.Enabled = false;
            tabPage2.Enabled = false;
            tabPage3.Enabled = false;
            tabPage4.Enabled = false;
            tabPage5.Enabled = false;
            tabPage6.Enabled = false;
            tabPage7.Enabled = false;
           
        }
        public void pb_init()
        {
            
            p1_1.Add(GD1); p1_1.Add(GD2); p1_1.Add(GD3); p1_1.Add(GD4);
            p1_1.Add(GD5); p1_1.Add(GD6); p1_1.Add(GD7); p1_1.Add(GD8);

            p1_2.Add(GD12); p1_2.Add(GD9); p1_2.Add(GD10); p1_2.Add(GD13);
            p1_2.Add(GD11); p1_2.Add(GD14); p1_2.Add(GD15); p1_2.Add(GD16);

            p1_3.Add(pictureBox5); p1_3.Add(pictureBox18); p1_3.Add(pictureBox19); p1_3.Add(pictureBox24);
            p1_3.Add(pictureBox25); p1_3.Add(pictureBox32); p1_3.Add(pictureBox33); p1_3.Add(pictureBox28);

            p1_4.Add(pictureBox29); p1_4.Add(pictureBox48); p1_4.Add(pictureBox49); p1_4.Add(pictureBox44);
            p1_4.Add(pictureBox45); p1_4.Add(pictureBox40); p1_4.Add(pictureBox41); p1_4.Add(pictureBox36);

            p1_5.Add(pictureBox37); p1_5.Add(pictureBox20); p1_5.Add(pictureBox21); p1_5.Add(pictureBox22);
            p1_5.Add(pictureBox23); p1_5.Add(pictureBox30); p1_5.Add(pictureBox31); p1_5.Add(pictureBox26);//cx4-1

            p1_6.Add(pictureBox27); p1_6.Add(pictureBox46); p1_6.Add(pictureBox47); p1_6.Add(pictureBox42);
            p1_6.Add(pictureBox43); p1_6.Add(pictureBox38); p1_6.Add(pictureBox39); p1_6.Add(pictureBox34);

            p1_7.Add(pictureBox35); p1_7.Add(pictureBox50); p1_7.Add(pictureBox1); p1_7.Add(pictureBox8);
            p1_7.Add(pictureBox7); p1_7.Add(pictureBox181); p1_7.Add(pictureBox180); p1_7.Add(pictureBox251);

            //测量前缓冲站

            p1_8.Add(pictureBox250); p1_8.Add(pictureBox70); p1_8.Add(pictureBox221); p1_8.Add(pictureBox222);
            p1_8.Add(pictureBox223); p1_8.Add(pictureBox13); p1_8.Add(pictureBox14); p1_8.Add(pictureBox65);

            p1_9.Add(pictureBox64); p1_9.Add(pictureBox57); p1_9.Add(pictureBox56); p1_9.Add(pictureBox53);
            p1_9.Add(pictureBox52); p1_9.Add(pictureBox60); p1_9.Add(pictureBox61); p1_9.Add(pictureBox15);

            p1_10.Add(pictureBox16);p1_10.Add(pictureBox62); p1_10.Add(pictureBox63); p1_10.Add(pictureBox54);
            p1_10.Add(pictureBox55);p1_10.Add(pictureBox17); p1_10.Add(pictureBox51); p1_10.Add(pictureBox58);

            p1_11.Add(pictureBox59);p1_11.Add(pictureBox267); p1_11.Add(pictureBox266); p1_11.Add(pictureBox261);
            p1_11.Add(pictureBox260); p1_11.Add(pictureBox224); p1_11.Add(pictureBox225); p1_11.Add(pictureBox67);

            p1_12.Add(pictureBox68); p1_12.Add(pictureBox91); p1_12.Add(pictureBox90); p1_12.Add(pictureBox83);
            p1_12.Add(pictureBox82); p1_12.Add(pictureBox79); p1_12.Add(pictureBox78); p1_12.Add(pictureBox74);

            p1_13.Add(pictureBox75); p1_13.Add(pictureBox88); p1_13.Add(pictureBox89); p1_13.Add(pictureBox80);
            p1_13.Add(pictureBox81);p1_13.Add(pictureBox76); p1_13.Add(pictureBox77); p1_13.Add(pictureBox271);

            p1_14.Add(pictureBox270); p1_14.Add(pictureBox253); p1_14.Add(pictureBox252);p1_14.Add(pictureBox106);
            p1_14.Add(pictureBox6);   p1_14.Add(pictureBox179); p1_14.Add(pictureBox249); p1_14.Add(pictureBox265);

            p1_15.Add(pictureBox259); p1_15.Add(pictureBox269); p1_15.Add(pictureBox195); p1_15.Add(pictureBox184);
            p1_15.Add(pictureBox3); p1_15.Add(pictureBox109); p1_15.Add(pictureBox201); p1_15.Add(pictureBox263);

            p1_16.Add(pictureBox257); p1_16.Add(pictureBox255); p1_16.Add(pictureBox193); p1_16.Add(pictureBox189);
            p1_16.Add(pictureBox2);  p1_16.Add(pictureBox108); p1_16.Add(pictureBox196); p1_16.Add(pictureBox262);

            p1_17.Add(pictureBox256); p1_17.Add(pictureBox254); p1_17.Add(pictureBox192);

            //存在和故障
            p1_18.Add(pictureBox285); p1_18.Add(pictureBox286);p1_18.Add(pictureBox287); p1_18.Add(pictureBox288);
            p1_18.Add(pictureBox291); p1_18.Add(pictureBox292);p1_18.Add(pictureBox293); p1_18.Add(pictureBox294);

            p1_19.Add(pictureBox107); p1_19.Add(pictureBox4); p1_19.Add(pictureBox178); p1_19.Add(pictureBox248);
            p1_19.Add(pictureBox264); p1_19.Add(pictureBox258); p1_19.Add(pictureBox268); p1_19.Add(pictureBox194);

            //第二plc信息

            p2_1.Add(pictureBox182); p2_1.Add(pictureBox209); p2_1.Add(pictureBox122); p2_1.Add(pictureBox242);
            p2_1.Add(pictureBox202); p2_1.Add(pictureBox86); p2_1.Add(pictureBox226); p2_1.Add(pictureBox227);

            p2_2.Add(pictureBox94); p2_2.Add(pictureBox95); p2_2.Add(pictureBox113); p2_2.Add(pictureBox112);
            p2_2.Add(pictureBox105); p2_2.Add(pictureBox104); p2_2.Add(pictureBox101); p2_2.Add(pictureBox100);//dc19-2

            p2_3.Add(pictureBox96); p2_3.Add(pictureBox97); p2_3.Add(pictureBox110); p2_3.Add(pictureBox111);
            p2_3.Add(pictureBox102); p2_3.Add(pictureBox103); p2_3.Add(pictureBox98); p2_3.Add(pictureBox99);//cx19-2

            p2_4.Add(pictureBox279); p2_4.Add(pictureBox278); p2_4.Add(pictureBox228); p2_4.Add(pictureBox229);
            p2_4.Add(pictureBox231); p2_4.Add(pictureBox230); p2_4.Add(pictureBox124); p2_4.Add(pictureBox125);//dc20-2

            p2_5.Add(pictureBox143); p2_5.Add(pictureBox142); p2_5.Add(pictureBox135); p2_5.Add(pictureBox134);
            p2_5.Add(pictureBox131); p2_5.Add(pictureBox130); p2_5.Add(pictureBox138); p2_5.Add(pictureBox139);//dc34-2

            p2_6.Add(pictureBox126); p2_6.Add(pictureBox127); p2_6.Add(pictureBox140); p2_6.Add(pictureBox141);
            p2_6.Add(pictureBox132); p2_6.Add(pictureBox133); p2_6.Add(pictureBox128); p2_6.Add(pictureBox129);//cx23-2

            p2_7.Add(pictureBox136); p2_7.Add(pictureBox137); p2_7.Add(pictureBox71); p2_7.Add(pictureBox199);
            p2_7.Add(pictureBox188); p2_7.Add(pictureBox187); p2_7.Add(pictureBox232); p2_7.Add(pictureBox233);

            p2_8.Add(pictureBox146); p2_8.Add(pictureBox147);p2_8.Add(pictureBox177); p2_8.Add(pictureBox176);
            p2_8.Add(pictureBox165); p2_8.Add(pictureBox164); p2_8.Add(pictureBox157); p2_8.Add(pictureBox156);

            p2_9.Add(pictureBox172); p2_9.Add(pictureBox173); p2_9.Add(pictureBox168); p2_9.Add(pictureBox169);
            p2_9.Add(pictureBox160); p2_9.Add(pictureBox161); p2_9.Add(pictureBox148); p2_9.Add(pictureBox149);//cx24-1-2

            p2_10.Add(pictureBox174); p2_10.Add(pictureBox175); p2_10.Add(pictureBox162); p2_10.Add(pictureBox163);
            p2_10.Add(pictureBox154); p2_10.Add(pictureBox155); p2_10.Add(pictureBox170); p2_10.Add(pictureBox171);//cx26-1-2

            p2_11.Add(pictureBox166); p2_11.Add(pictureBox167); p2_11.Add(pictureBox158); p2_11.Add(pictureBox159);
            p2_11.Add(pictureBox281); p2_11.Add(pictureBox280); p2_11.Add(pictureBox153); p2_11.Add(pictureBox152);//dj13反转

            p2_12.Add(pictureBox236); p2_12.Add(pictureBox237); p2_12.Add(pictureBox238); p2_12.Add(pictureBox239);
            p2_12.Add(pictureBox240); p2_12.Add(pictureBox241); p2_12.Add(pictureBox204); p2_12.Add(pictureBox205);//dc28-2

            p2_13.Add(pictureBox219); p2_13.Add(pictureBox218); p2_13.Add(pictureBox215); p2_13.Add(pictureBox214);
            p2_13.Add(pictureBox206); p2_13.Add(pictureBox207); p2_13.Add(pictureBox216); p2_13.Add(pictureBox217);//cx29-2

            p2_14.Add(pictureBox208); p2_14.Add(pictureBox213); p2_14.Add(pictureBox247); p2_14.Add(pictureBox246);
            p2_14.Add(pictureBox235); p2_14.Add(pictureBox234); p2_14.Add(pictureBox121); p2_14.Add(pictureBox120);//dj16反转
            //气缸35和33
            p2_15.Add(pictureBox283); p2_15.Add(pictureBox282); p2_15.Add(pictureBox200); p2_15.Add(pictureBox220);
            p2_15.Add(pictureBox84); p2_15.Add(pictureBox85); p2_15.Add(pictureBox72); p2_15.Add(pictureBox73);
            
            //电机伺服on
            
            p2_16.Add(pictureBox277); p2_16.Add(pictureBox198); p2_16.Add(pictureBox186); p2_16.Add(pictureBox273);
            p2_16.Add(pictureBox151); p2_16.Add(pictureBox245); p2_16.Add(pictureBox212); p2_16.Add(pictureBox117);
            //电机伺服off
            
            p2_17.Add(pictureBox275); p2_17.Add(pictureBox191); p2_17.Add(pictureBox183); p2_17.Add(pictureBox210);
            p2_17.Add(pictureBox123); p2_17.Add(pictureBox243); p2_17.Add(pictureBox203); p2_17.Add(pictureBox87);
            //电机运行状态 

            p2_18.Add(pictureBox274); p2_18.Add(pictureBox190); //dj10运行状态

            //电机故障
            p2_19.Add(pictureBox276); p2_19.Add(pictureBox197); p2_19.Add(pictureBox185); p2_19.Add(pictureBox272);
            p2_19.Add(pictureBox150); p2_19.Add(pictureBox244); p2_19.Add(pictureBox211); p2_19.Add(pictureBox116);
            //上电
            p2_20.Add(pictureBox295); p2_20.Add(pictureBox296); p2_20.Add(pictureBox297); p2_20.Add(pictureBox298);
            p2_20.Add(pictureBox299); p2_20.Add(pictureBox300); p2_20.Add(pictureBox301); p2_20.Add(pictureBox302);
           
            //总伺服on和off //todu
            p28.Add(pictureBox289); p28.Add(pictureBox290);


        }
        /// <summary>
        /// 将picturebox加入list中
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<PictureBox> PB_Init(Panel p)
        {
            List<PictureBox> pb = new List<PictureBox>();
            foreach (Control c in p.Controls)
            {
                if ((c as PictureBox) != null)
                {   //如果是Label控件，加入list
                     pb.Insert(0, (PictureBox)c);
                }
            }            
            return pb;
        }

        private void link_btn_Click(object sender, EventArgs e)
        {
            Connect();
        }
        private void button157_Click(object sender, EventArgs e)
        {
            Connect_plc2();
        }

        /// <summary>
        /// 判断是否modbus链接存活
        /// </summary>
        public void keep_alive()
        {
            if (newclient1.Connected)
            {
                pictureBox284.BackColor = Color.Lime;
            }
            else
            {
                pictureBox284.BackColor = Color.Gray;
            }
        }
        /// <summary>
        /// 创建连接
        /// </summary>
        public void Connect()
        {
            byte[] data = new byte[1024];

            string ip1 = serverIP.Text.Trim();//将服务器IP地址存放在字符串 ipadd中
            string ip2 = textBox2.Text.Trim();//将服务器IP地址存放在字符串 ipadd中
            int port = Convert.ToInt32(serverPort.Text.Trim());//将端口号强制为32位整型，存放在port中
            //创建一个套接字 
            IPEndPoint ie1 = new IPEndPoint(IPAddress.Parse(ip1), port);
            newclient1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ie2 = new IPEndPoint(IPAddress.Parse(ip2), port);
            newclient2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //将套接字与远程服务器地址相连
            try
            {
                newclient1.Connect(ie1);
                Thread.Sleep(100);
                //newclient2.Connect(ie2);
                link_btn.Enabled = false;//使连接按钮变成虚的，无法点击
                button1.Enabled = true;//急停按钮可以点击
                closebtn.Enabled = true;//断开的按钮，可以点击
                Connected = true;
            }
            catch (SocketException e)
            {
                MessageBox.Show("连接服务器失败  " + e.Message);
                return;
            }

            ThreadStart myThreaddelegate = new ThreadStart(ReceiveMsg);
            ThreadStart myThreaddelegate1 = new ThreadStart(ReceiveMsg);
            ThreadStart myThreaddelegate2 = new ThreadStart(ReceiveMsg);
            ThreadStart myThreaddelegate3 = new ThreadStart(ReceiveMsg);
            ThreadStart myThreaddelegate4 = new ThreadStart(ReceiveMsg);
            ThreadStart myThreaddelegate5 = new ThreadStart(ReceiveMsg);
            myThread = new Thread(myThreaddelegate);
            myThread.Start();
            myThread1 = new Thread(myThreaddelegate1);
            myThread1.Start();
            myThread2 = new Thread(myThreaddelegate3);
            myThread2.Start();
            myThread3 = new Thread(myThreaddelegate4);
            myThread3.Start();
            myThread4 = new Thread(myThreaddelegate5);
            myThread4.Start();
            myThread5 = new Thread(myThreaddelegate2);
            myThread5.Start();

            //定时器开
           /*jinliao_ceqian_timer1.Enabled = true;         
            celliang_chengpin_timer1.Enabled = true;
            dianji_guzhang_timer1.Enabled = true;
            sifu_on_off_timer1.Enabled = true;
            temp_add_timer1.Enabled = true;
            judge_moubus_alive_timer1.Enabled = true;*/
            plc1_zhuangtai_timer1.Enabled = true;
            plc_cunzaihezguzhang_timer1.Enabled = true;
            //按键使能控制
            closebtn.Enabled = true;
            tabPage1.Enabled = true;
            tabPage2.Enabled = true;
            tabPage3.Enabled = true;
            tabPage4.Enabled = true;
            tabPage5.Enabled = true;
            tabPage6.Enabled = true;
            tabPage7.Enabled = true;

        }
        public void Connect_plc2()
        {
            byte[] data = new byte[1024];

           
            string ip2 = textBox2.Text.Trim();//将服务器IP地址存放在字符串 ipadd中
            int port = Convert.ToInt32(serverPort.Text.Trim());//将端口号强制为32位整型，存放在port中
            //创建一个套接字 
           
          

            IPEndPoint ie2 = new IPEndPoint(IPAddress.Parse(ip2), port);
            newclient2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //将套接字与远程服务器地址相连
            try
            {
                newclient2.Connect(ie2);
                
                //newclient2.Connect(ie2);
                link_btn.Enabled = false;//使连接按钮变成虚的，无法点击
                button1.Enabled = true;//急停按钮可以点击
                closebtn.Enabled = true;//断开的按钮，可以点击
                Connected = true;
            }
            catch (SocketException e)
            {
                MessageBox.Show("连接服务器失败  " + e.Message);
                return;
            }

            ThreadStart myThreaddelegate_1 = new ThreadStart(ReceiveMsg_plc2);
            ThreadStart myThreaddelegate_2 = new ThreadStart(ReceiveMsg_plc2);
            ThreadStart myThreaddelegate_3 = new ThreadStart(ReceiveMsg_plc2);
            ThreadStart myThreaddelegate_4 = new ThreadStart(ReceiveMsg_plc2);
            ThreadStart myThreaddelegate_5 = new ThreadStart(ReceiveMsg_plc2);
            ThreadStart myThreaddelegate_6 = new ThreadStart(ReceiveMsg_plc2);
            myThread_1 = new Thread(myThreaddelegate_1);
            myThread_1.Start();
            myThread_2 = new Thread(myThreaddelegate_2);
            myThread_2.Start();
            myThread_3 = new Thread(myThreaddelegate_3);
            myThread_3.Start();
            myThread_4 = new Thread(myThreaddelegate_4);
            myThread_4.Start();
            myThread_5 = new Thread(myThreaddelegate_5);
            myThread_5.Start();
            myThread_6 = new Thread(myThreaddelegate_6);
            myThread_6.Start();
            plc2_zhuangtai_timer1.Enabled = true;
            plc2_cunzaiheguzhangtimer1.Enabled = true;
            //定时器开
            /*jinliao_ceqian_timer1.Enabled = true;         
             celliang_chengpin_timer1.Enabled = true;
             dianji_guzhang_timer1.Enabled = true;
             sifu_on_off_timer1.Enabled = true;
             temp_add_timer1.Enabled = true;
             judge_moubus_alive_timer1.Enabled = true;*/

            //按键使能控制
            closebtn.Enabled = true;
            tabPage1.Enabled = true;
            tabPage2.Enabled = true;
            tabPage3.Enabled = true;
            tabPage4.Enabled = true;
            tabPage5.Enabled = true;
            tabPage6.Enabled = true;
            tabPage7.Enabled = true;

        }
        /// <summary>
        /// 接收数据，并处理接收数据的内容
        /// </summary>
        public void ReceiveMsg()
        {
            while (true)
            {
                byte[] data = new byte[1024];
                newclient1.Receive(data);
                int length = data[5];
                Byte[] datashow = new byte[length + 6];
                for (int i = 0; i <= length + 5; i++)
                    datashow[i] = data[i];
                string stringdata = BitConverter.ToString(datashow);               
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p1_1, "d1");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p1_2, "d2");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p1_3, "d3");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p1_4, "d4");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p1_5, "d5");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p1_6, "d6");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p1_7, "d7");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p1_8, "d8");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p1_9, "d9");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p1_10, "d10");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p1_11, "d11");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p1_12, "d12");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p1_13, "d13");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p1_14, "d14");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p1_15, "d15");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p1_16, "d16");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p1_17, "d17");

                showState(data[0], 0x02, data[7], 0x01, data[6], 0x01, data, p1_18, "d1");
                showState(data[0], 0x02, data[7], 0x01, data[6], 0x01, data, p1_19, "d2");


               
                showMsg(stringdata + "\r\n");
            }
        }
        public void ReceiveMsg_plc2()
        {
            while (true)
            {
                byte[] data = new byte[1024];
                newclient2.Receive(data);
                int length = data[5];
                Byte[] datashow = new byte[length + 6];
                for (int i = 0; i <= length + 5; i++)
                    datashow[i] = data[i];
                string stringdata = BitConverter.ToString(datashow);

                showState(data[0], 0x01, data[7], 0x01, data[6], 0x02, data, p2_1, "d1");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x02, data, p2_2, "d2");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x02, data, p2_3, "d3");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x02, data, p2_4, "d4");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x02, data, p2_5, "d5");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x02, data, p2_6, "d6");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x02, data, p2_7, "d7");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x02, data, p2_8, "d8");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x02, data, p2_9, "d9");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x02, data, p2_10, "d10");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x02, data, p2_11, "d11");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x02, data, p2_12, "d12");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x02, data, p2_13, "d13");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x02, data, p2_14, "d14");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x02, data, p2_15, "d15");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x02, data, p2_16, "d16");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x02, data, p2_17, "d17");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x02, data, p2_18, "d18");

                 showState(data[0], 0x02, data[7], 0x01, data[6], 0x02, data, p2_19, "d1");
                 showState(data[0], 0x02, data[7], 0x01, data[6], 0x02, data, p2_20, "d2");
                showMsg(stringdata + "\r\n");
            }
        }
        /// <summary>
        /// 根据接收到的数据进行
        /// </summary>
        /// <param name="m">消息号data[0]</param>
        /// <param name="m_data">信息号</param>
        /// <param name="f">功能码data[7]</param>
        /// <param name="f_data">功能吗</param>
        /// <param name="s">站号data[6]</param>
        /// <param name="s_data"></param>
        /// <param name="d1">接收到的数据</param>
        /// <param name="d2">接收到的数据</param>
        /// <param name="d3">接收到的数据</param>
        /// <param name="d4">接收到的数据</param>
        ///  <param name="pb_list">存放pictureBox的列表</param>

        public void showState(int m, int m_data, int f, int f_data, int s, int s_data,byte[]d,List<PictureBox> pb_list, string str)
        {
            if (m == m_data && f == f_data && s == s_data)//信息号为01，功能码读取01，站号00
            {
                char[] str1 = Convert.ToString(d[9], 2).Reverse().ToArray();
                char[] str2 = Convert.ToString(d[10], 2).Reverse().ToArray();
                char[] str3 = Convert.ToString(d[11], 2).Reverse().ToArray();
                char[] str4 = Convert.ToString(d[12], 2).Reverse().ToArray();
                char[] str5 = Convert.ToString(d[13], 2).Reverse().ToArray();
                char[] str6 = Convert.ToString(d[14], 2).Reverse().ToArray();
                char[] str7 = Convert.ToString(d[15], 2).Reverse().ToArray();
                char[] str8 = Convert.ToString(d[16], 2).Reverse().ToArray();
                char[] str9 = Convert.ToString(d[17], 2).Reverse().ToArray();
                char[] str10 = Convert.ToString(d[18], 2).Reverse().ToArray();
                char[] str11 = Convert.ToString(d[19], 2).Reverse().ToArray();
                char[] str12 = Convert.ToString(d[20], 2).Reverse().ToArray();
                char[] str13 = Convert.ToString(d[21], 2).Reverse().ToArray();
                char[] str14 = Convert.ToString(d[22], 2).Reverse().ToArray();
                char[] str15 = Convert.ToString(d[23], 2).Reverse().ToArray();
                char[] str16 = Convert.ToString(d[24], 2).Reverse().ToArray();
                char[] str17 = Convert.ToString(d[25], 2).Reverse().ToArray();
                char[] str18 = Convert.ToString(d[26], 2).Reverse().ToArray();
                char[] str19 = Convert.ToString(d[27], 2).Reverse().ToArray();
                char[] str20 = Convert.ToString(d[28], 2).Reverse().ToArray();
                char[] temp = new char[8];
                switch (str)
                {
                    case "d1":
                        for (int i = 0; i < str1.Length; i++) temp[i] = str1[i];
                        break;
                    case "d2":
                        for (int i = 0; i < str2.Length; i++) temp[i] = str2[i];
                        break;
                    case "d3":
                        for (int i = 0; i < str3.Length; i++) temp[i] = str3[i];
                        break;
                    case "d4":
                        for (int i = 0; i < str4.Length; i++) temp[i] = str4[i];
                        break;
                    case "d5":
                        for (int i = 0; i < str5.Length; i++) temp[i] = str5[i];
                        break;
                    case "d6":
                        for (int i = 0; i < str6.Length; i++) temp[i] = str6[i];
                        break;
                    case "d7":
                        for (int i = 0; i < str7.Length; i++) temp[i] = str7[i];
                        break;
                    case "d8":
                        for (int i = 0; i < str8.Length; i++) temp[i] = str8[i];
                        break;
                    case "d9":
                        for (int i = 0; i < str9.Length; i++) temp[i] = str9[i];
                        break;
                    case "d10":
                        for (int i = 0; i < str10.Length; i++) temp[i] = str10[i];
                        break;
                    case "d11":
                        for (int i = 0; i < str11.Length; i++) temp[i] = str11[i];
                        break;
                    case "d12":
                        for (int i = 0; i < str12.Length; i++) temp[i] = str12[i];
                        break;
                    case "d13":
                        for (int i = 0; i < str13.Length; i++) temp[i] = str13[i];
                        break;
                    case "d14":
                        for (int i = 0; i < str14.Length; i++) temp[i] = str14[i];
                        break;
                    case "d15":
                        for (int i = 0; i < str15.Length; i++) temp[i] = str15[i];
                        break;
                    case "d16":
                        for (int i = 0; i < str16.Length; i++) temp[i] = str16[i];
                        break;
                    case "d17":
                        for (int i = 0; i < str17.Length; i++) temp[i] = str17[i];
                        break;
                    case "d18":
                        for (int i = 0; i < str18.Length; i++) temp[i] = str18[i];
                        break;
                    case "d19":
                        for (int i = 0; i < str19.Length; i++) temp[i] = str19[i];
                        break;
                    case "d20":
                        for (int i = 0; i < str20.Length; i++) temp[i] = str20[i];
                        break;
                }
                string temp_str = "";
                for (int i = 0; i < pb_list.Count; i++)
                {
                    if (m_data == 0x05)
                    {
                       
                        if (str == "d1")
                        {
                            if (temp[i] == '1')
                            {
                                temp_str = temp_str + (1 + i).ToString();
                                pb_list[i].BackColor = Color.Red;
                            }
                            else
                            {
                                pb_list[i].BackColor = Color.Gray;
                            }
                            
                        }
                        else if(str == "d2")
                        {
                            if (temp[i] == '1')
                            {
                                temp_str = temp_str + (9 + i).ToString();
                                pb_list[i].BackColor = Color.Red;
                            }
                            else
                            {
                                pb_list[i].BackColor = Color.Gray;
                            }
                        }
                    }
                    else
                    {
                        if (temp[i] == '1')
                        {
                            pb_list[i].BackColor = Color.Lime;
                        }
                        else
                        {
                            pb_list[i].BackColor = Color.Gray;
                        }
                    }
                    
                }
                if (temp_str != "")
                {
                    label8.Text = temp_str;
                    label8.BackColor = Color.Red;
                  
                }
               /* else if(temp_str == "")
                {
                    label8.Text = "正常";
                    label8.BackColor = Color.Lime;
                }*/
            }
        }
     
        public void showMsg(string msg)
        {
            msg = DateTime.Now.ToLongTimeString().ToString()+ "*   " + msg;
            //在线程里以安全方式调用控件
            if (receiveBox.InvokeRequired)
            {
                MyInvoke _myinvoke = new MyInvoke(showMsg);
                receiveBox.Invoke(_myinvoke, new object[] { msg });
            }
            else
            {
                receiveBox.AppendText(msg);
            }

        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("断开链接", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                jinliao_ceqian_timer1.Enabled = false;
                celliang_chengpin_timer1.Enabled = false;
                dianji_guzhang_timer1.Enabled = false;
                sifu_on_off_timer1.Enabled = false;
                temp_add_timer1.Enabled = false;
                judge_moubus_alive_timer1.Enabled = false;

                plc1_zhuangtai_timer1.Enabled = false;
                plc_cunzaihezguzhang_timer1.Enabled = false;
                plc2_zhuangtai_timer1.Enabled = false;
                plc2_cunzaiheguzhangtimer1.Enabled = false;
                if(newclient1.Connected == true)
                {
                    myThread.Abort();//接受消息任务关闭   
                    myThread1.Abort();//接受消息任务关闭   
                    myThread2.Abort();//接受消息任务关闭   
                    myThread3.Abort();//接受消息任务关闭   
                    myThread4.Abort();//接受消息任务关闭   
                    myThread5.Abort();//接受消息任务关闭           
                    newclient1.Close();//连接关闭
                }
                if (newclient2.Connected == true)
                {
                    myThread_1.Abort();
                    myThread_2.Abort();
                    myThread_3.Abort();
                    myThread_4.Abort();
                    myThread_5.Abort();
                    myThread_6.Abort();
                    newclient2.Close();

                }
                pictureBox284.BackColor = Color.Gray;
                link_btn.Enabled = true;
                closebtn.Enabled = false;
                tabPage1.Enabled = false;
                tabPage2.Enabled = false;
                tabPage3.Enabled = false;
                tabPage4.Enabled = false;
                tabPage5.Enabled = false;
                tabPage6.Enabled = false;
                tabPage7.Enabled = false;
            }
        }


        private void jinliao_ceqian_timer1_Tick(object sender, EventArgs e)
        {
            //byte[] data = new byte[] { 0x04, 0x00, 0x00, 0x00, 0x00, 0x06, 0x03, 0x01, 0x00, 0x56, 0x00, 0x80 };
            int isecond = 10;//以毫秒为单位
            jinliao_ceqian_timer1.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x01, 0x08, 0x01, 0x00, 0x55 };
            newclient1.Send(data);
        }

        private void celliang_chengpin_timer1_Tick(object sender, EventArgs e)
        {
            int isecond = 10;//以毫秒为单位
            celliang_chengpin_timer1.Interval = isecond;//50ms触发一次

            byte[] data = new byte[] { 0x04, 0x00, 0x00, 0x00, 0x00, 0x06, 0x03, 0x01, 0x08, 0x56, 0x00, 0x80 };
            newclient1.Send(data);
        }
        private void sifu_on_off_timer1_Tick(object sender, EventArgs e)
        {
            int isecond = 10;//以毫秒为单位
            sifu_on_off_timer1.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x00, 0x06, 0x02, 0x01, 0x08, 0xd6, 0x00, 0x20 };
            newclient1.Send(data);

        }
        private void dianji_guzhang_timer1_Tick(object sender, EventArgs e)
        {
            int isecond = 10;//以毫秒为单位
            dianji_guzhang_timer1.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x05, 0x00, 0x00, 0x00, 0x00, 0x06, 0x04, 0x01, 0x08, 0xf6, 0x00, 0x10 };
            newclient1.Send(data);
        }
        private void temp_add_timer1_Tick(object sender, EventArgs e)
        {
            int isecond = 10;//以毫秒为单位
            temp_add_timer1.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x03, 0x00, 0x00, 0x00, 0x00, 0x06, 0x05, 0x01, 0x09, 0x06, 0x00, 0x92 };
            newclient1.Send(data);
        }
        private void judge_moubus_alive_timer1_Tick(object sender, EventArgs e)
        {
            int isecond = 10;//以毫秒为单位
            judge_moubus_alive_timer1.Interval = isecond;//50ms触发一次
            keep_alive();

        }

        //以下为按键事件
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸1", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK") {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x12, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x13, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x15, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);

                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x14, 0xff, 0x00 };
                newclient1.Send(data);
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x14, 0x00, 0x00 };
                newclient1.Send(data);

                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x15, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void DC1_1_btn_Click(object sender, EventArgs e)
        {
           DialogResult res =  MessageBox.Show("顶起气缸1","提示信息",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
           
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x13, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);

                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x12, 0xff, 0x00 };
                newclient1.Send(data);
            }
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸3", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x17, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x16, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸3", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x16, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x17, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸4", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x19, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x18, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸4", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x18, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x19, 0xff, 0x00 };
                newclient1.Send(data);
            }

        }

        private void button17_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸5", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1b, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1a, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸5", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1a, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1b, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸6", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1d, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1c, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸6", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1c, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1d, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸7", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1f, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1e, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸7", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1e, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1f, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸8", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x21, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x20, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸8", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x20, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x21, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸9", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x3f, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x3e, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸9", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x3e, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x3f, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸10", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x41, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x40, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸10", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x40, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x41, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸11", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x43, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x42, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸11", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x42, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x43, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸12", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x45, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);

                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x44, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸12", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x44, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x45, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸16", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x47, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x46, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸16", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x46, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x47, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸13-1和13-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x59, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5b, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //顶起
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x58, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5a, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button40_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸13-1", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x58, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5a, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x59, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5b, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button55_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸13-2和13-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {

                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x59, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5b, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //顶起
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x58, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5a, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button54_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸13-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x58, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5a, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x59, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5b, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button45_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸14", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5d, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5c, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button44_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸14", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5c, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5d, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸15", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5f, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5e, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button42_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸15", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5e, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5f, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }


        private void button61_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸18", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x6f, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x6e, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button60_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸18", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x6e, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x6f, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button69_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸17-1和17-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x73, 0x00, 0x00 };
                newclient2.Send(data);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x71, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x70, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x72, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button68_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸17-1和17-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x70, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x72, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x73, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x71, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button65_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸17-2和17-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x73, 0x00, 0x00 };
                newclient2.Send(data);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x71, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x70, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x72, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button64_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸17-1和17-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x70, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x72, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x73, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x71, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button63_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸19", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x75, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x74, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button62_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸19", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x74, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x75, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button80_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸20", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x85, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x84, 0xff, 0x00 };
                newclient2.Send(data);
            }

        }

        private void button79_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸20", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x84, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x85, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button88_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸21", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x87, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x86, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button87_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸21", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x86, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x87, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button84_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸22", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x89, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x88, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button83_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸22", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x88, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x89, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button82_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸23", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x8b, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x8a, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button81_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸23", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x8a, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x8b, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button86_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸34", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x8d, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x8c, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button85_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸34", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x8c, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x8d, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button90_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸24-1和24-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x9f, 0x00, 0x00 };
                newclient2.Send(data);

                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa1, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa0, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x9e, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button89_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸24-1和24-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa0, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x9e, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x9f, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa1, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button104_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸和24-1和24-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x9f, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa1, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa0, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x9e, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button103_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸24-1和24-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa0, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x9e, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x9f, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa1, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button98_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸25", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa3, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa2, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button97_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸25", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa2, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa3, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button94_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸27", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa5, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa4, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button93_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸27", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa4, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa5, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button102_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸26-1和26-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa7, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa9, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa8, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa6, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button101_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸26-1和26-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa8, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa6, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa7, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa9, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button100_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸26-1和26-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa7, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa9, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa8, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa6, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button99_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸26-1和26-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa8, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa6, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa7, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa9, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button96_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸32", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xab, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xaa, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button95_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸32", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xaa, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xab, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button134_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸28", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc5, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc4, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button133_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸28", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc4, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc5, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button138_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸29", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc7, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc6, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button137_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸29", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc6, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc7, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button136_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸30", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc9, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc8, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button135_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸30", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc8, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc9, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸35", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x17, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x16, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸35", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x16, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x17, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸33", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x1b, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x1a, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸33", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x1a, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x1b, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button46_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xab, 0x00, 0x00 };
            newclient1.Send(data);
            Thread.Sleep(5);
            //伺服on
            data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xaa, 0xff, 0x00 };
            newclient1.Send(data);

        }

        private void button47_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xaa, 0x00, 0x00 };
            newclient1.Send(data);
            Thread.Sleep(5);
            //伺服off
            data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xab, 0xff, 0x00 };
            newclient1.Send(data);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("紧急停止", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0xa0, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }
        //正
        /// <summary>
        /// 电机1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button19_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机1正转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbc, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbc, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //反
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xad, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);

                //正
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xac, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }
        //反转
        private void button18_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机1反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //暂停
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbc, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(100);

                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbc, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xac, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                
              
                //反转        
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xad, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }
        //暂停
        private void button26_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机1暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xac, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xad, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbc, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(1000);
            }
        }

        
        
        /// <summary>
        /// DJ2正转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button22_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机2正转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbd, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbd, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xaf, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xae, 0xff, 0x00 };
                newclient1.Send(data);
            }

        }

       

        /// <summary>
        /// DJ2暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button20_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机2暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xaf, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xae, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbd, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        /// <summary>
        /// DJ2反转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button21_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机2反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbd, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbd, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xae, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xaf, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }
        
        /// <summary>
        /// 电机3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button25_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机3正转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbe, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbe, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb1, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb0, 0xff, 0x00 };
                newclient1.Send(data);
            }

        }

        private void button23_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机3暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb1, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb0, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbe, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机3反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbe, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbe, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb0, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb1, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        /// <summary>
        /// 电机4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button29_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机4正转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbf, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbf, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb3, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb2, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机4暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb3, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb2, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbf, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机4反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbf, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbf, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb2, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb3, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        /// <summary>
        /// 电机5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button121_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机5正转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc0, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc0, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb5, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb4, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button119_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机5暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb5, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb4, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc0, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button120_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机5反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc0, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc0, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb4, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb5, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        /// <summary>
        /// 电机6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button115_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机6正转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc1, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc1, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb7, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb6, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button110_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机6暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb7, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb6, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc1, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button114_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机6反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc1, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc1, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb6, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb7, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        /// <summary>
        /// 电机7
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button109_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机7正转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc2, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc2, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb9, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb8, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button92_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机7暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb9, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb8, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc2, 0xff, 0x00 };
                newclient1.Send(data);
            }

        }

        private void button105_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机7反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc2, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc2, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb8, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xb9, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        /// <summary>
        /// 电机8
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button91_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机8正转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc3, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc3, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbb, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xba, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button66_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机8暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbb, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xba, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc3, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        private void button67_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机8反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc3, 0xff, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc3, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xba, 0x00, 0x00 };
                newclient1.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xbb, 0xff, 0x00 };
                newclient1.Send(data);
            }
        }

        /// <summary>
        /// 电机9
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button124_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机9正转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x2f, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x2f, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x20, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x1f, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button122_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机9暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x20, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x1f, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x2f, 0xff, 0x00 };
                newclient2.Send(data);
            }

        }

        private void button123_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机9反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x2f, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x2f, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x1f, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x20, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        /// <summary>
        /// 电机10
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button59_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机10正转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x30, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x30, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x22, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x21, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button57_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机10暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x22, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x21, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x30, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button58_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机10反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x30, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x30, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x21, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x22, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        /// <summary>
        /// 电机11
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button56_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机11正转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x31, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x31, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x24, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x23, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button52_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机11暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x24, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x23, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x31, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button53_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机11反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x31, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x31, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x23, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x24, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        /// <summary>
        /// 电机12
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button75_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机12正转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x32, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x32, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x26, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x25, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button73_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机12暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x26, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x25, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x32, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button74_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机12反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x32, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x32, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x25, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x26, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        /// <summary>
        /// 电机13
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button72_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机13正转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x33, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x33, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x28, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x27, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button70_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机13暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x28, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x27, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x33, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button71_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机13反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x33, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x33, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x27, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x28, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }
        /// <summary>
        /// 电机14
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button108_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机14正转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x34, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x34, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x2a, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x29, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button106_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机14暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x2a, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x29, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x34, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button107_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机14反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x34, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x34, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x29, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x2a, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        /// <summary>
        /// 电机15
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button78_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机15正转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x35, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x35, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x2c, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x2b, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button76_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机15暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x2c, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x2b, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x35, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button77_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机15反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x35, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x35, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x2b, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x2c, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        /// <summary>
        /// 电机16
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button51_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机16正转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x36, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x36, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x2e, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x2d, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button49_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机16暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x2e, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x2d, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x36, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void button50_Click(object sender, EventArgs e)
        { DialogResult res = MessageBox.Show("电机16反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x36, 0xff, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x36, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x2d, 0x00, 0x00 };
                newclient2.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x2e, 0xff, 0x00 };
                newclient2.Send(data);
            }
        }

        private void plc1_zhuangtai_timer1_Tick(object sender, EventArgs e)
        {
            int isecond = 10;//以毫秒为单位
            plc1_zhuangtai_timer1.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x01, 0x08, 0x01, 0x00, 0x83 };
            newclient1.Send(data);
        }

        private void plc_cunzaihezguzhang_timer1_Tick(object sender, EventArgs e)
        {
            int isecond = 10;//以毫秒为单位
            plc_cunzaihezguzhang_timer1.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x01, 0x08, 0x94, 0x00, 0x10 };
            newclient1.Send(data);
        }

        private void plc2_zhuangtai_timer1_Tick(object sender, EventArgs e)
        {
            int isecond = 10;//以毫秒为单位
            plc2_zhuangtai_timer1.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x06, 0x02, 0x01, 0x08, 0x66, 0x00, 0x9a };
            newclient2.Send(data);

        }

        private void plc2_cunzaiheguzhangtimer1_Tick(object sender, EventArgs e)
        {
            int isecond = 10;//以毫秒为单位
            plc2_cunzaiheguzhangtimer1.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x00, 0x06, 0x02, 0x01, 0x08, 0xfe, 0x00, 0x10 };
            newclient2.Send(data);
        }

        private void button159_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x1e, 0x00, 0x00 };
            newclient2.Send(data);
            Thread.Sleep(5);
            //伺服on
            data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x1d, 0xff, 0x00 };
            newclient2.Send(data);
        }

        private void button158_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x1d, 0x00, 0x00 };
            newclient2.Send(data);
            Thread.Sleep(5);
            //伺服off
            data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x1e, 0xff, 0x00 };
            newclient2.Send(data);
        }

        private void GD1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox245_Click(object sender, EventArgs e)
        {

        }
    }
}
