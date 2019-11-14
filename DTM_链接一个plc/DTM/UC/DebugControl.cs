using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace DTM.UC
{
    public partial class DebugControl : UserControl
    {
        public Socket newclient;
        public bool Connected;
        public Thread myThread;
        public delegate void MyInvoke(string str);
        //进料lable定义
        List<Label> jinliao_gd1 = new List<Label>();
        List<Label> jinliao_gd2 = new List<Label>();
        List<Label> jinliao_dc1 = new List<Label>();
        List<Label> jinliao_dc2 = new List<Label>();
        List<Label> jinliao_dianji1 = new List<Label>();

        List<Label> gd3 = new List<Label>();
        List<Label> gd4 = new List<Label>();

        
        List<Label> qg2 = new List<Label>();
        List<Label> qg3 = new List<Label>();
        List<Label> qg4 = new List<Label>();
        public DebugControl()
        {
            InitializeComponent();
        }
       
        private void link_btn_Click(object sender, EventArgs e)
        {
            Connect();
        }
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
                closebtn.Enabled = true;//断开的按钮，可以点击
                

                Connected = true;

            }
            catch (SocketException e)
            {
                MessageBox.Show("连接服务器失败  " + e.Message);
                return;
            }

            ThreadStart myThreaddelegate = new ThreadStart(ReceiveMsg);
            myThread = new Thread(myThreaddelegate);
            myThread.Start();
            timersend_guangdian.Enabled = true;//光电开关定时查询开启
            Thread.Sleep(10);
            timersend_qigang.Enabled = true;//气缸定时查询开启

        }
        //消息显示
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
                //把数组转换成16进制字符串
                /*if (data[7] == 0x01) { showMsg01(stringdata + "\r\n"); };
                if (data[7] == 0x02) { showMsg02(stringdata + "\r\n"); };
                if (data[7] == 0x03) { showMsg03(stringdata + "\r\n"); };
                if (data[7] == 0x05) { showMsg05(stringdata + "\r\n"); };
                if (data[7] == 0x06) { showMsg06(stringdata + "\r\n"); };
                if (data[7] == 0x0F) { showMsg0F(stringdata + "\r\n"); };
                if (data[7] == 0x10) { showMsg10(stringdata + "\r\n"); };*/

                if (data[0] == 0x01 && data[7] == 0x01 && data[6] == 0x00)//信息号为01，功能码读取01，站号00
                {

                    char[] str = Convert.ToString(data[9], 2).Reverse().ToArray();

                    char[] str1 = Convert.ToString(data[10], 2).Reverse().ToArray();
                    char[] str2 = Convert.ToString(data[11], 2).Reverse().ToArray();
                    char[] str3 = Convert.ToString(data[12], 2).Reverse().ToArray();

                    showState(str, jinliao_gd1);
                    showState(str1, jinliao_gd2);
                    showState(str2, gd3);
                    showState(str3, gd4);

                }
                else if (data[0] == 0x02 && data[7] == 0x01 && data[6] == 0x00)
                {
                    char[] str = Convert.ToString(data[9], 2).Reverse().ToArray();

                    char[] str1 = Convert.ToString(data[10], 2).Reverse().ToArray();
                    char[] str2 = Convert.ToString(data[11], 2).Reverse().ToArray();
                    char[] str3 = Convert.ToString(data[12], 2).Reverse().ToArray();

                    showState(str, jinliao_dc1);
                    showState(str1, qg2);
                    showState(str2, qg3);
                    showState(str3, qg4);

                }
                showMsg(stringdata + "\r\n");
            }
        }

        /// <summary>
        /// 显示光电开关的状态
        /// </summary>
        /// <param name="str">modbus收到的字节</param>
        /// <param name="la">用于存储lable的list</param>
        public void showState(char[] str, List<Label> la)
        {
            char[] temp = new char[8];
            for (int i = 0; i < str.Length; i++)
            {
                temp[i] = str[i];
            }
            for (int i = 0; i < la.Count; i++)
            {
                
                    if (temp[i] == '1')
                    {
                        la[i].Text = "●";
                        la[i].ForeColor = Color.Red;
                    }
                    else
                    {
                        la[i].Text = "●";
                        la[i].ForeColor = Color.Blue;
                    }

                
            }
        }
        public void showMsg(string msg)
        {

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


        /// <summary>
        /// 初始化lable的样式，并将panel中的lable加入到list中
        /// </summary>
        /// <param name="la">用于储存对应panel的lable</param>
        /// <param name="panel1">从目标panel获取</param>
        private void lable_init(List<Label> la, Panel panel)
        {
            foreach (Control c in panel.Controls)
            {
                if ((c as Label) != null)
                {   //如果是Label控件，加入list

                    la.Insert(0, (Label)c);
                }
            }

            for (int j = 0; j < la.Count; j++)
            {
               // Console.WriteLine(la[j].Text);
                la[j].Text = "●";
                la[j].ForeColor = Color.Blue;

            }
        }
        
        private void DebugControl_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;

            jinliao_Init();//进料站初始化显示

            lable_init(gd3, panel6);
            lable_init(gd4, panel7);

           
            lable_init(qg2, panel16);
            lable_init(qg3, panel12);
            lable_init(qg4, panel9);

           
            closebtn.Enabled = false;//使断开的按钮，无法点击
           
        }
        public void jinliao_Init()
        {
            lable_init(jinliao_gd1, panel1);//光电开关状态的lable
            lable_init(jinliao_gd2, panel2);
            lable_init(jinliao_dc1, panel15);
            lable_init(jinliao_dc2, panel31);
            lable_init(jinliao_dianji1, panel18);
            label133.Text = "●";
            label133.ForeColor = Color.Blue;

        }
       

        private void timerdianji_Tick(object sender, EventArgs e)
        {

        }

        private void timersend_Tick(object sender, EventArgs e)
        {
            int isecond = 50;//以毫秒为单位
            timersend_guangdian.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x01, 0x01, 0xa0, 0x00, 0x1c };
            newclient.Send(data);
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            timersend_guangdian.Enabled = false;//定时查询任务关闭
            timersend_qigang.Enabled = false;//定时任务关闭

            myThread.Abort();//接受消息任务关闭           
            newclient.Close();//连接关闭
            link_btn.Enabled = true;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void timersend_qigang_Tick(object sender, EventArgs e)
        {
            int isecond = 50;//以毫秒为单位
            timersend_guangdian.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x01, 0x00, 0x00, 0x00, 0x1c };
            newclient.Send(data);
        }

      

        private void ucCheckBox1_CheckedChangeEvent(object sender, EventArgs e)
        {
            if(ucCheckBox1.Checked)
            {
                byte[] data = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x00, 0x00, 0xff, 0x00 };
                newclient.Send(data);
            }
            else
            {
                byte[] data = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x00, 0x00, 0x00, 0x00 };
                newclient.Send(data);
            }
            
        }

        private void ucCheckBox2_CheckedChangeEvent(object sender, EventArgs e)
        {
            if (ucCheckBox2.Checked)
            {
                byte[] data = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x00, 0x01, 0xff, 0x00 };
                newclient.Send(data);
            }
            else
            {
                byte[] data = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x00, 0x01, 0x00, 0x00 };
                newclient.Send(data);
            }
        }
    }
}
