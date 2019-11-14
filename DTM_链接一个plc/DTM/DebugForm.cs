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
        public Socket newclient;
        public bool Connected;
        public Thread myThread;
        public Thread myThread1;
        public Thread myThread2;
        public Thread myThread3;
        public Thread myThread4;
        public Thread myThread5;
        public delegate void MyInvoke(string str);
        //进料站状态定义
        List<PictureBox> p01 = new List<PictureBox>();
        List<PictureBox> p02 = new List<PictureBox>();
        List<PictureBox> p03 = new List<PictureBox>();
        List<PictureBox> p04 = new List<PictureBox>();
        List<PictureBox> p05 = new List<PictureBox>();
        List<PictureBox> p06 = new List<PictureBox>();
        List<PictureBox> p07 = new List<PictureBox>();
        List<PictureBox> p08 = new List<PictureBox>();

        //测量前缓冲站
        List<PictureBox> p09 = new List<PictureBox>();
        List<PictureBox> p010 = new List<PictureBox>();
        List<PictureBox> p011 = new List<PictureBox>();
        List<PictureBox> p012 = new List<PictureBox>();
        List<PictureBox> p013 = new List<PictureBox>();
        //测量至成品
        List<PictureBox> p1 = new List<PictureBox>();
        List<PictureBox> p2 = new List<PictureBox>();
        List<PictureBox> p3 = new List<PictureBox>();
        List<PictureBox> p4 = new List<PictureBox>();
        List<PictureBox> p5 = new List<PictureBox>();
        List<PictureBox> p6 = new List<PictureBox>();
        List<PictureBox> p7 = new List<PictureBox>();
        List<PictureBox> p8 = new List<PictureBox>();
        List<PictureBox> p9 = new List<PictureBox>();
        List<PictureBox> p10 = new List<PictureBox>();
        List<PictureBox> p11 = new List<PictureBox>();
        List<PictureBox> p12 = new List<PictureBox>();
        List<PictureBox> p13 = new List<PictureBox>();
        List<PictureBox> p14 = new List<PictureBox>();
        List<PictureBox> p15 = new List<PictureBox>();
        List<PictureBox> p16 = new List<PictureBox>();
        //电机故障
        List<PictureBox> p17 = new List<PictureBox>();
        List<PictureBox> p18 = new List<PictureBox>();
        //电机伺服on和off
        List<PictureBox> p19 = new List<PictureBox>();
        List<PictureBox> p20 = new List<PictureBox>();
        List<PictureBox> p21 = new List<PictureBox>();
        List<PictureBox> p22 = new List<PictureBox>();
        //电机运行状态和气缸35和33
        List<PictureBox> p23 = new List<PictureBox>();
        List<PictureBox> p24 = new List<PictureBox>();
        List<PictureBox> p25 = new List<PictureBox>();
        //上电
        List<PictureBox> p26 = new List<PictureBox>();
        List<PictureBox> p27 = new List<PictureBox>();
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
            
            p01.Add(GD1); p01.Add(GD2); p01.Add(GD3); p01.Add(GD4);
            p01.Add(GD5); p01.Add(GD6); p01.Add(GD7); p01.Add(GD8);
            p02.Add(GD12); p02.Add(GD9); p02.Add(GD10); p02.Add(GD13);
            p02.Add(GD11); p02.Add(GD14); p02.Add(GD15); p02.Add(GD16);

            p03.Add(pictureBox5); p03.Add(pictureBox18); p03.Add(pictureBox19); p03.Add(pictureBox24);
            p03.Add(pictureBox25); p03.Add(pictureBox32); p03.Add(pictureBox33); p03.Add(pictureBox28);

            p04.Add(pictureBox29); p04.Add(pictureBox48); p04.Add(pictureBox49); p04.Add(pictureBox44);
            p04.Add(pictureBox45); p04.Add(pictureBox40); p04.Add(pictureBox41); p04.Add(pictureBox36);
            p05.Add(pictureBox37); p05.Add(pictureBox20); p05.Add(pictureBox21); p05.Add(pictureBox22);
            p05.Add(pictureBox23); p05.Add(pictureBox30); p05.Add(pictureBox31); p05.Add(pictureBox26);
            p06.Add(pictureBox27); p06.Add(pictureBox46); p06.Add(pictureBox47); p06.Add(pictureBox42);
            p06.Add(pictureBox43); p06.Add(pictureBox38); p06.Add(pictureBox39); p06.Add(pictureBox34);
            p07.Add(pictureBox35); p07.Add(pictureBox50); p07.Add(pictureBox1); p07.Add(pictureBox8);
            p07.Add(pictureBox7); p07.Add(pictureBox181); p07.Add(pictureBox180); p07.Add(pictureBox251);
            p08.Add(pictureBox250);
            //测量前缓冲站

            p08.Add(pictureBox70); p08.Add(pictureBox221); p08.Add(pictureBox222); p08.Add(pictureBox223);
            p08.Add(pictureBox13); p08.Add(pictureBox14); p08.Add(pictureBox65);
            p09.Add(pictureBox64); p09.Add(pictureBox57); p09.Add(pictureBox56); p09.Add(pictureBox53);
            p09.Add(pictureBox52); p09.Add(pictureBox60); p09.Add(pictureBox61); p09.Add(pictureBox15);

            p010.Add(pictureBox16);p010.Add(pictureBox62); p010.Add(pictureBox63); p010.Add(pictureBox54);
            p010.Add(pictureBox55);p010.Add(pictureBox17); p010.Add(pictureBox51); p010.Add(pictureBox58);

            p011.Add(pictureBox59);p011.Add(pictureBox267); p011.Add(pictureBox266); p011.Add(pictureBox261);
            p011.Add(pictureBox260);

            //测量至成品
            p1.Add(pictureBox224); p1.Add(pictureBox225); p1.Add(pictureBox67); p1.Add(pictureBox68);
            p1.Add(pictureBox91); p1.Add(pictureBox90); p1.Add(pictureBox83); p1.Add(pictureBox82);
           
            p2.Add(pictureBox79); p2.Add(pictureBox78); p2.Add(pictureBox74); p2.Add(pictureBox75);
            p2.Add(pictureBox88); p2.Add(pictureBox89); p2.Add(pictureBox80); p2.Add(pictureBox81);

            p3.Add(pictureBox76); p3.Add(pictureBox77); p3.Add(pictureBox271); p3.Add(pictureBox270);
            p3.Add(pictureBox253); p3.Add(pictureBox252); p3.Add(pictureBox226); p3.Add(pictureBox227);

            p4.Add(pictureBox94); p4.Add(pictureBox95); p4.Add(pictureBox113); p4.Add(pictureBox112);
            p4.Add(pictureBox105); p4.Add(pictureBox104); p4.Add(pictureBox101); p4.Add(pictureBox100);

            p5.Add(pictureBox96); p5.Add(pictureBox97); p5.Add(pictureBox110); p5.Add(pictureBox111);
            p5.Add(pictureBox102); p5.Add(pictureBox103); p5.Add(pictureBox98); p5.Add(pictureBox99);

            p6.Add(pictureBox279); p6.Add(pictureBox278); p6.Add(pictureBox228); p6.Add(pictureBox229);
            p6.Add(pictureBox231); p6.Add(pictureBox230); p6.Add(pictureBox124); p6.Add(pictureBox125);

            p7.Add(pictureBox143); p7.Add(pictureBox142); p7.Add(pictureBox135); p7.Add(pictureBox134);
            p7.Add(pictureBox131); p7.Add(pictureBox130); p7.Add(pictureBox138); p7.Add(pictureBox139);

            p8.Add(pictureBox126); p8.Add(pictureBox127); p8.Add(pictureBox140); p8.Add(pictureBox141);
            p8.Add(pictureBox132); p8.Add(pictureBox133); p8.Add(pictureBox128); p8.Add(pictureBox129);

            p9.Add(pictureBox136); p9.Add(pictureBox137); p9.Add(pictureBox71); p9.Add(pictureBox199);
            p9.Add(pictureBox188); p9.Add(pictureBox187); p9.Add(pictureBox232); p9.Add(pictureBox233);

            p10.Add(pictureBox146); p10.Add(pictureBox147);p10.Add(pictureBox177); p10.Add(pictureBox176);
            p10.Add(pictureBox165); p10.Add(pictureBox164); p10.Add(pictureBox157); p10.Add(pictureBox156);

            p11.Add(pictureBox172); p11.Add(pictureBox173); p11.Add(pictureBox168); p11.Add(pictureBox169);
            p11.Add(pictureBox160); p11.Add(pictureBox161); p11.Add(pictureBox148); p11.Add(pictureBox149);//cx24-1-2

            p12.Add(pictureBox174); p12.Add(pictureBox175); p12.Add(pictureBox162); p12.Add(pictureBox163);
            p12.Add(pictureBox154); p12.Add(pictureBox155); p12.Add(pictureBox170); p12.Add(pictureBox171);

            p13.Add(pictureBox166); p13.Add(pictureBox167); p13.Add(pictureBox158); p13.Add(pictureBox159);
            p13.Add(pictureBox281); p13.Add(pictureBox280); p13.Add(pictureBox153); p13.Add(pictureBox152);

            p14.Add(pictureBox236); p14.Add(pictureBox237); p14.Add(pictureBox238); p14.Add(pictureBox239);
            p14.Add(pictureBox240); p14.Add(pictureBox241); p14.Add(pictureBox204); p14.Add(pictureBox205);

            p15.Add(pictureBox219); p15.Add(pictureBox218); p15.Add(pictureBox215); p15.Add(pictureBox214);
            p15.Add(pictureBox206); p15.Add(pictureBox207); p15.Add(pictureBox216); p15.Add(pictureBox217);

            p16.Add(pictureBox208); p16.Add(pictureBox213); p16.Add(pictureBox247); p16.Add(pictureBox246);
            p16.Add(pictureBox235); p16.Add(pictureBox234); p16.Add(pictureBox121); p16.Add(pictureBox120);
            //电机故障
            p17.Add(pictureBox107); p17.Add(pictureBox4); p17.Add(pictureBox178); p17.Add(pictureBox248);
            p17.Add(pictureBox264); p17.Add(pictureBox258);p17.Add(pictureBox268); p17.Add(pictureBox194);

            p18.Add(pictureBox276); p18.Add(pictureBox197); p18.Add(pictureBox185); p18.Add(pictureBox272);
            p18.Add(pictureBox150); p18.Add(pictureBox244); p18.Add(pictureBox211); p18.Add(pictureBox116);
            //电机伺服on
            p19.Add(pictureBox106); p19.Add(pictureBox6); p19.Add(pictureBox179); p19.Add(pictureBox249);
            p19.Add(pictureBox265); p19.Add(pictureBox259); p19.Add(pictureBox269); p19.Add(pictureBox195);
            p20.Add(pictureBox277); p20.Add(pictureBox198); p20.Add(pictureBox186); p20.Add(pictureBox273);
            p20.Add(pictureBox151); p20.Add(pictureBox245); p20.Add(pictureBox212); p20.Add(pictureBox117);
            //电机伺服off
            p21.Add(pictureBox184); p21.Add(pictureBox3); p21.Add(pictureBox109); p21.Add(pictureBox201);
            p21.Add(pictureBox263); p21.Add(pictureBox257); p21.Add(pictureBox255); p21.Add(pictureBox193);
            p22.Add(pictureBox275); p22.Add(pictureBox191); p22.Add(pictureBox183); p22.Add(pictureBox210);
            p22.Add(pictureBox123); p22.Add(pictureBox243); p22.Add(pictureBox203); p22.Add(pictureBox87);
            //电机运行状态 //todo电机运行状态读取线圈？
            p23.Add(pictureBox189); p23.Add(pictureBox2); p23.Add(pictureBox108); p23.Add(pictureBox196);
            p23.Add(pictureBox262); p23.Add(pictureBox256); p23.Add(pictureBox254); p23.Add(pictureBox192);
            p24.Add(pictureBox274); p24.Add(pictureBox190); p24.Add(pictureBox182); p24.Add(pictureBox209);
            p24.Add(pictureBox122); p24.Add(pictureBox242); p24.Add(pictureBox202); p24.Add(pictureBox86);
            //气缸35和33
            p25.Add(pictureBox283); p25.Add(pictureBox282); p25.Add(pictureBox200); p25.Add(pictureBox220);
            p25.Add(pictureBox84); p25.Add(pictureBox85); p25.Add(pictureBox72); p25.Add(pictureBox73);
            //上电
            p26.Add(pictureBox285); p26.Add(pictureBox286); p26.Add(pictureBox287); p26.Add(pictureBox288);
            p26.Add(pictureBox291); p26.Add(pictureBox292); p26.Add(pictureBox293); p26.Add(pictureBox294);

            p27.Add(pictureBox295); p27.Add(pictureBox296); p27.Add(pictureBox297); p27.Add(pictureBox298);
            p27.Add(pictureBox299); p27.Add(pictureBox300); p27.Add(pictureBox301); p27.Add(pictureBox302);
            //总伺服on和off
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

        /// <summary>
        /// 判断是否modbus链接存活
        /// </summary>
        public void keep_alive()
        {
            if (newclient.Connected)
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

            string ipadd = serverIP.Text.Trim();//将服务器IP地址存放在字符串 ipadd中
            int port = Convert.ToInt32(serverPort.Text.Trim());//将端口号强制为32位整型，存放在port中
            //创建一个套接字 
            IPEndPoint ie = new IPEndPoint(IPAddress.Parse(ipadd), port);
            newclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //将套接字与远程服务器地址相连
            try
            {
                newclient.Connect(ie);
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
            myThread2 = new Thread(myThreaddelegate1);
            myThread2.Start();
            myThread3 = new Thread(myThreaddelegate1);
            myThread3.Start();
            myThread4 = new Thread(myThreaddelegate1);
            myThread4.Start();
            myThread5 = new Thread(myThreaddelegate1);
            myThread5.Start();

            //定时器开
            jinliao_ceqian_timer1.Enabled = true;         
            celliang_chengpin_timer1.Enabled = true;
            dianji_guzhang_timer1.Enabled = true;
            sifu_on_off_timer1.Enabled = true;
            temp_add_timer1.Enabled = true;
            judge_moubus_alive_timer1.Enabled = true;
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
                newclient.Receive(data);
                int length = data[5];
                Byte[] datashow = new byte[length + 6];
                for (int i = 0; i <= length + 5; i++)
                    datashow[i] = data[i];
                string stringdata = BitConverter.ToString(datashow);               
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p01, "d1");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p02, "d2");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p03, "d3");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p04, "d4");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p05, "d5");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p06, "d6");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p07, "d7");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p08, "d8");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p09, "d9");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p010, "d10");
                showState(data[0], 0x01, data[7], 0x01, data[6], 0x01, data, p011, "d11");
                showState(data[0], 0x04, data[7], 0x01, data[6], 0x03, data, p1, "d1");
                showState(data[0], 0x04, data[7], 0x01, data[6], 0x03, data, p2, "d2");
                showState(data[0], 0x04, data[7], 0x01, data[6], 0x03, data, p3, "d3");
                showState(data[0], 0x04, data[7], 0x01, data[6], 0x03, data, p4, "d4");
                showState(data[0], 0x04, data[7], 0x01, data[6], 0x03, data, p4, "d4");
                showState(data[0], 0x04, data[7], 0x01, data[6], 0x03, data, p5, "d5");
                showState(data[0], 0x04, data[7], 0x01, data[6], 0x03, data, p6, "d6");
                showState(data[0], 0x04, data[7], 0x01, data[6], 0x03, data, p7, "d7");
                showState(data[0], 0x04, data[7], 0x01, data[6], 0x03, data, p8, "d8");
                showState(data[0], 0x04, data[7], 0x01, data[6], 0x03, data, p9, "d9");
                showState(data[0], 0x04, data[7], 0x01, data[6], 0x03, data, p10, "d10");
                showState(data[0], 0x04, data[7], 0x01, data[6], 0x03, data, p11, "d11");
                showState(data[0], 0x04, data[7], 0x01, data[6], 0x03, data, p12, "d12");
                showState(data[0], 0x04, data[7], 0x01, data[6], 0x03, data, p13, "d13");
                showState(data[0], 0x04, data[7], 0x01, data[6], 0x03, data, p14, "d14");
                showState(data[0], 0x04, data[7], 0x01, data[6], 0x03, data, p15, "d15");
                showState(data[0], 0x04, data[7], 0x01, data[6], 0x03, data, p16, "d16");
                //电机故障
                showState(data[0], 0x05, data[7], 0x01, data[6], 0x04, data, p17, "d1");
                showState(data[0], 0x05, data[7], 0x01, data[6], 0x04, data, p18, "d2");
                
                //电机伺服on和off
                showState(data[0], 0x02, data[7], 0x01, data[6], 0x02, data, p19, "d1");
                showState(data[0], 0x02, data[7], 0x01, data[6], 0x02, data, p20, "d2");
                showState(data[0], 0x02, data[7], 0x01, data[6], 0x02, data, p21, "d3");
                showState(data[0], 0x02, data[7], 0x01, data[6], 0x02, data, p22, "d4");
                //气缸35和气缸33状态读取（临时增加部分）
                showState(data[0], 0x03, data[7], 0x01, data[6], 0x05, data, p23, "d1");
                showState(data[0], 0x03, data[7], 0x01, data[6], 0x05, data, p24, "d2");
                showState(data[0], 0x03, data[7], 0x01, data[6], 0x05, data, p25, "d3");
                showState(data[0], 0x03, data[7], 0x01, data[6], 0x05, data, p26, "d4");
                showState(data[0], 0x03, data[7], 0x01, data[6], 0x05, data, p27, "d5");
                showState(data[0], 0x03, data[7], 0x01, data[6], 0x05, data, p28, "d14");
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
                char[] str17 = Convert.ToString(d[23], 2).Reverse().ToArray();
                char[] str18 = Convert.ToString(d[24], 2).Reverse().ToArray();
                char[] str19 = Convert.ToString(d[23], 2).Reverse().ToArray();
                char[] str20 = Convert.ToString(d[24], 2).Reverse().ToArray();
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

                myThread.Abort();//接受消息任务关闭   
                myThread1.Abort();//接受消息任务关闭   
                myThread2.Abort();//接受消息任务关闭   
                myThread3.Abort();//接受消息任务关闭   
                myThread4.Abort();//接受消息任务关闭   
                myThread5.Abort();//接受消息任务关闭           
                newclient.Close();//连接关闭
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
            newclient.Send(data);
        }

        private void celliang_chengpin_timer1_Tick(object sender, EventArgs e)
        {
            int isecond = 10;//以毫秒为单位
            celliang_chengpin_timer1.Interval = isecond;//50ms触发一次

            byte[] data = new byte[] { 0x04, 0x00, 0x00, 0x00, 0x00, 0x06, 0x03, 0x01, 0x08, 0x56, 0x00, 0x80 };
            newclient.Send(data);
        }
        private void sifu_on_off_timer1_Tick(object sender, EventArgs e)
        {
            int isecond = 10;//以毫秒为单位
            sifu_on_off_timer1.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x00, 0x06, 0x02, 0x01, 0x08, 0xd6, 0x00, 0x20 };
            newclient.Send(data);

        }
        private void dianji_guzhang_timer1_Tick(object sender, EventArgs e)
        {
            int isecond = 10;//以毫秒为单位
            dianji_guzhang_timer1.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x05, 0x00, 0x00, 0x00, 0x00, 0x06, 0x04, 0x01, 0x08, 0xf6, 0x00, 0x10 };
            newclient.Send(data);
        }
        private void temp_add_timer1_Tick(object sender, EventArgs e)
        {
            int isecond = 10;//以毫秒为单位
            temp_add_timer1.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x03, 0x00, 0x00, 0x00, 0x00, 0x06, 0x05, 0x01, 0x09, 0x06, 0x00, 0x92 };
            newclient.Send(data);
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
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x13, 0xff, 0x00 };
                newclient.Send(data);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x15, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);

                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x14, 0xff, 0x00 };
                newclient.Send(data);
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x14, 0x00, 0x00 };
                newclient.Send(data);

                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x15, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void DC1_1_btn_Click(object sender, EventArgs e)
        {
           DialogResult res =  MessageBox.Show("顶起气缸1","提示信息",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
           
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x13, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);

                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x12, 0xff, 0x00 };
                newclient.Send(data);
            }
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸3", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x17, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x16, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸3", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x16, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x17, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸4", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x19, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x18, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸4", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x18, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x19, 0xff, 0x00 };
                newclient.Send(data);
            }

        }

        private void button17_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸5", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1b, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1a, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸5", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1a, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1b, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸6", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1d, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1c, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸6", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1c, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1d, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸7", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1f, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1e, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸7", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1e, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x1f, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸8", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x21, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x20, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸8", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x20, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x21, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸9", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x3f, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x3e, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸9", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x3e, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x3f, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸10", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x41, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x40, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸10", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x40, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x41, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸11", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x43, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x42, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸11", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x42, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x43, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸12", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x45, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);

                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x44, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸12", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x44, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x45, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸16", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x47, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x46, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸16", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x46, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x47, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸13-1和13-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x59, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5b, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //顶起
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x58, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5a, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button40_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸13-1", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x58, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5a, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x59, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5b, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button55_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸13-2和13-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (res.ToString() == "OK")
            {

                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x59, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5b, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //顶起
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x58, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5a, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button54_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸13-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x58, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5a, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x59, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5b, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button45_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸14", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5d, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5c, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button44_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸14", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5c, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5d, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸15", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5f, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5e, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button42_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸15", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5e, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x5f, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button61_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸18", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x6f, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x6e, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button60_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸18", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x6e, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x6f, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button69_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸17-1和17-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x73, 0x00, 0x00 };
                newclient.Send(data);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x71, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x70, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x72, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button68_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸17-1和17-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x70, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x72, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x73, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x71, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button65_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸17-2和17-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x73, 0x00, 0x00 };
                newclient.Send(data);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x71, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x70, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x72, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button64_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸17-1和17-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x70, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x72, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x73, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x71, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button63_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸19", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x75, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x74, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button62_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸19", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x74, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x75, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button80_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸20", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x85, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x84, 0xff, 0x00 };
                newclient.Send(data);
            }

        }

        private void button79_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸20", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x84, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x85, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button88_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸21", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x87, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x86, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button87_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸21", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x86, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x87, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button84_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸22", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x89, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x88, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button83_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸22", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x88, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x89, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button82_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸23", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x8b, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x8a, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button81_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸23", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x8a, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x8b, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button86_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸34", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x8d, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x8c, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button85_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸34", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x8c, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x8d, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button90_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸24-1和24-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x9f, 0x00, 0x00 };
                newclient.Send(data);
                
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa1, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa0, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x9e, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button89_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸24-1和24-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa0, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x9e, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x9f, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa1, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button104_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸和24-1和24-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x9f, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa1, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa0, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x9e, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button103_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸24-1和24-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa0, 0x00, 0x00 };
                newclient.Send(data);                
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x9e, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0x9f, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa1, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button98_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸25", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa3, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa2, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button97_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸25", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa2, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa3, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button94_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸27", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa5, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa4, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button93_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸27", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa4, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa5, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button102_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸26-1和26-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa7, 0x00, 0x00 };
                newclient.Send(data);               
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa9, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa8, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa6, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button101_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸26-1和26-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa8, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa6, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa7, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa9, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button100_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸26-1和26-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa7, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa9, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa8, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa6, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button99_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸26-1和26-2", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa8, 0x00, 0x00 };
                newclient.Send(data);               
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa6, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa7, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xa9, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button96_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸32", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xab, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xaa, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button95_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸32", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xaa, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xab, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button134_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸28", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc5, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc4, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button133_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸28", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc4, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc5, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button138_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸29", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc7, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc6, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button137_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸29", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc6, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc7, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button136_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸30", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc9, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc8, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button135_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸30", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc8, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x08, 0xc9, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸35", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x17, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x16, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸35", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x16, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x17, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("顶起气缸33", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x1b, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x1a, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("收回气缸33", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x1a, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x1b, 0xff, 0x00 };
                newclient.Send(data);
            }
        }
        
        private void button46_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x6f, 0x00, 0x00 };
            newclient.Send(data);
            Thread.Sleep(5);
            //伺服on
            data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x6e, 0xff, 0x00 };
            newclient.Send(data);
           
        }

        private void button47_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x6e, 0x00, 0x00 };
            newclient.Send(data);
            Thread.Sleep(5);
            //伺服off
            data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x6f, 0xff, 0x00 };
            newclient.Send(data);
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("紧急停止", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0xa0, 0xff, 0x00 };
                newclient.Send(data);
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
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x90, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x90, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x71, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);

                //正
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x70, 0xff, 0x00 };
                newclient.Send(data);
            }
        }
        //反转
        private void button18_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机1反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //暂停
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x90, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x70, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x90, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转        
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x71, 0xff, 0x00 };
                newclient.Send(data);
            }
        }
        //暂停
        private void button26_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机1暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x70, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x71, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x90, 0xff, 0x00 };
                newclient.Send(data);
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
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x91, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x91, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x73, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x72, 0xff, 0x00 };
                newclient.Send(data);
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
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x73, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x72, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x91, 0xff, 0x00 };
                newclient.Send(data);
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
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x91, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x91, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x72, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x73, 0xff, 0x00 };
                newclient.Send(data);
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
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x92, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x92, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x75, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x74, 0xff, 0x00 };
                newclient.Send(data);
            }

        }

        private void button23_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机3暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x75, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x74, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x92, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机3反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x92, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x92, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x74, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x75, 0xff, 0x00 };
                newclient.Send(data);
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
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x93, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x93, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x77, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x76, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机4暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x77, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x76, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x93, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机4反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x93, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x93, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x76, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x77, 0xff, 0x00 };
                newclient.Send(data);
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
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x94, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x94, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x79, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x78, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button119_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机5暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x79, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x78, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x94, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button120_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机5反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x94, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x94, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x78, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x79, 0xff, 0x00 };
                newclient.Send(data);
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
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x95, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x95, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x7b, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x7a, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button110_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机6暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x7b, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x7a, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x95, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button114_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机6反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x95, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x95, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x7a, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x7b, 0xff, 0x00 };
                newclient.Send(data);
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
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x96, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x96, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x7d, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x7c, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button92_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机7暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x7d, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x7c, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x96, 0xff, 0x00 };
                newclient.Send(data);
            }

        }

        private void button105_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机7反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x96, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x96, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x7c, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x7d, 0xff, 0x00 };
                newclient.Send(data);
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
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x97, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x97, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x7f, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x7e, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button66_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机8暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x7f, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x7e, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x97, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button67_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机8反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x97, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x97, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x7e, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x7f, 0xff, 0x00 };
                newclient.Send(data);
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
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x98, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x98, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x81, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x80, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button122_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机9暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x81, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x80, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x98, 0xff, 0x00 };
                newclient.Send(data);
            }

        }

        private void button123_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机9反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x98, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x98, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x80, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x81, 0xff, 0x00 };
                newclient.Send(data);
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
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x99, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x99, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x83, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x82, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button57_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机10暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x83, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x82, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x99, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button58_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机10反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x99, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x99, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x82, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x83, 0xff, 0x00 };
                newclient.Send(data);
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
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9a, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9a, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x85, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x84, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button52_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机11暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x85, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x84, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9a, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button53_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机11反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9a, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9a, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x84, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x85, 0xff, 0x00 };
                newclient.Send(data);
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
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9b, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9b, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x87, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x86, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button73_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机12暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x87, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x86, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9b, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button74_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机12反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9b, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9b, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x86, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x87, 0xff, 0x00 };
                newclient.Send(data);
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
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9c, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9c, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x89, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x88, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button70_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机13暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x89, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x88, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9c, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button71_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机13反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9c, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9c, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x88, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x89, 0xff, 0x00 };
                newclient.Send(data);
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
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9d, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9d, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x8b, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x8a, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button106_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机14暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x8b, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x8a, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9d, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button107_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机14反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9d, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9d, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x8a, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x8b, 0xff, 0x00 };
                newclient.Send(data);
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
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9e, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9e, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x8d, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x8c, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button76_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机15暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x8d, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x8c, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9e, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button77_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机15反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9e, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9e, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x8c, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x8d, 0xff, 0x00 };
                newclient.Send(data);
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
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9f, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9f, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x8f, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x8e, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button49_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("电机16暂停", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                //反转
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x8f, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x8e, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9f, 0xff, 0x00 };
                newclient.Send(data);
            }
        }

        private void button50_Click(object sender, EventArgs e)
        { DialogResult res = MessageBox.Show("电机16反转", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res.ToString() == "OK")
            {
                byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9f, 0xff, 0x00 };
                newclient.Send(data);
                Thread.Sleep(100);
                //暂停
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x9f, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //正转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x8e, 0x00, 0x00 };
                newclient.Send(data);
                Thread.Sleep(5);
                //反转
                data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x09, 0x8f, 0xff, 0x00 };
                newclient.Send(data);
            }
        }
    }
}
