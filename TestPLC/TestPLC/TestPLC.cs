using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net.Sockets;
using System.Threading;
using System.Net;
using HZH_Controls;

//modbus格式说明
//https://blog.csdn.net/weixin_33788244/article/details/86003757
namespace TestPLC
{
    
    public partial class TestPLC : Form

    {
        public Socket newclient1;
        public Socket newclient2;
        public bool Connected;
        public Thread myThread;
        public Thread myThread2;
        public delegate void MyInvoke(string str);
        List<Label> La = new List<Label>();
        List<Label> La1 = new List<Label>();
        List<Label> La2 = new List<Label>();
        List<Label> La3 = new List<Label>();
        List<Label> dt_data = new List<Label>();
        static int[] DT_data = new int[256];
        public TestPLC()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connect();
        }
        public void DT_lable()
        {
            dt_data.Add(label73); dt_data.Add(label72); dt_data.Add(label71); dt_data.Add(label70); dt_data.Add(label69);
            dt_data.Add(label74); dt_data.Add(label75); dt_data.Add(label76); dt_data.Add(label77); dt_data.Add(label78);
        }
        public void show_dt_data(List<Label> lable_list, int[] data,int len)
        {
            for (int i = 0;i < len; i++)
            {
                lable_list[i].Text = data[i].ToString();
            }
        }
        public void Connect()
        {
            byte[] data = new byte[1024];

            string ipadd1 = serverIP.Text.Trim();//将服务器IP地址存放在字符串 ipadd中
            string ipadd2 = textBox13.Text.Trim();//将服务器IP地址存放在字符串 ipadd中
            int port = Convert.ToInt32(serverPort.Text.Trim());//将端口号强制为32位整型，存放在port中

            //创建一个套接字 

            IPEndPoint ie1 = new IPEndPoint(IPAddress.Parse(ipadd1), port);
            newclient1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ie2 = new IPEndPoint(IPAddress.Parse(ipadd2), port);
            newclient2 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


            //将套接字与远程服务器地址相连
            try
            {
                newclient1.Connect(ie1);
               // MessageBox.Show("链接第一个成功");
                //newclient2.Connect(ie2);
                MessageBox.Show("链接第二个成功");
                link_btn.Enabled = false;//使连接按钮变成虚的，无法点击
                closebtn.Enabled = true;//断开的按钮，可以点击
                testButton.Enabled = true;
                dt_shuju.Enabled = true;
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

            /*ThreadStart myThreaddelegate2 = new ThreadStart(ReceiveMsg2);
            myThread2 = new Thread(myThreaddelegate2);
            myThread2.Start();*/
            //   timersend.Enabled = true;//定时任务开

        }
        //消息显示
        int j = 0;
        int m = 0;
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
                //把数组转换成16进制字符串
                /*if (data[7] == 0x01) { showMsg01(stringdata + "\r\n"); };
                if (data[7] == 0x02) { showMsg02(stringdata + "\r\n"); };
                if (data[7] == 0x03) { showMsg03(stringdata + "\r\n"); };
                if (data[7] == 0x05) { showMsg05(stringdata + "\r\n"); };
                if (data[7] == 0x06) { showMsg06(stringdata + "\r\n"); };
                if (data[7] == 0x0F) { showMsg0F(stringdata + "\r\n"); };
                if (data[7] == 0x10) { showMsg10(stringdata + "\r\n"); };*/

                if (data[7] == 0x01)//如果功能码是读线圈
                {

                    char[] str = Convert.ToString(data[9], 2).Reverse().ToArray();
                    for (int i = 0; i < str.Length; i++)
                    {
                        Console.WriteLine(str[i]);
                    }
                    Console.WriteLine("================================");
                    char[] str1 = Convert.ToString(data[10], 2).Reverse().ToArray();
                    char[] str2 = Convert.ToString(data[11], 2).Reverse().ToArray();
                    char[] str3 = Convert.ToString(data[12], 2).Reverse().ToArray();

                    showGuangdian(str, La);
                    showGuangdian(str1, La1);
                    showGuangdian(str2, La2);
                    showGuangdian(str3, La3);

                }
                /*DT_data[0] = convert(data[9], data[10]);
                DT_data[1] = convert(data[11], data[12]);
                DT_data[2] = convert(data[13], data[14]);
                DT_data[3] = convert(data[15], data[16]);
                DT_data[4] = convert(data[17], data[18]);
                DT_data[5] = convert(data[19], data[20]);
                DT_data[6] = convert(data[21], data[22]);
                DT_data[7] = convert(data[23], data[24]);
                DT_data[8] = convert(data[25], data[26]);
                DT_data[9] = convert(data[27], data[28]);
                DT_data[10] = convert(data[29], data[30]);
                DT_data[11] = convert(data[31], data[32]);*/
                for (int j = 0;j < 10;j++)
                {
                    DT_data[j] = convert(data[9 + m], data[10 + m]);
                    m += 2;
                }
                m = 0;
                show_dt_data(dt_data,DT_data,10);
                showMsg(stringdata + "==" + DT_data[0] + "////"+DT_data[1] + "\r\n");
            }
        }
        public void conver_arr()
        {

        }
        public void ReceiveMsg2()
        {
            while (true)
            {
                byte[] data2 = new byte[1024];
                newclient2.Receive(data2);
                int length = data2[5];
                Byte[] datashow = new byte[length + 6];
                for (int i = 0; i <= length + 5; i++)
                    datashow[i] = data2[i];
                string stringdata = BitConverter.ToString(datashow);
                //把数组转换成16进制字符串
                /*if (data[7] == 0x01) { showMsg01(stringdata + "\r\n"); };
                if (data[7] == 0x02) { showMsg02(stringdata + "\r\n"); };
                if (data[7] == 0x03) { showMsg03(stringdata + "\r\n"); };
                if (data[7] == 0x05) { showMsg05(stringdata + "\r\n"); };
                if (data[7] == 0x06) { showMsg06(stringdata + "\r\n"); };
                if (data[7] == 0x0F) { showMsg0F(stringdata + "\r\n"); };
                if (data[7] == 0x10) { showMsg10(stringdata + "\r\n"); };*/

                 string str = convert(data2[9], data2[10]).ToString();
                showMsg(stringdata + "=="+ str + "\r\n");
                //测试
            }
        }
       
        /// <summary>
        /// 显示光电开关的状态
        /// </summary>
        /// <param name="str">modbus收到的字节</param>
        /// <param name="la">用于存储lable的list</param>
        /// 

        public void showGuangdian(char []str, List<Label> la)
        {
            char[] temp = new char[8];


            //if (str.Length < 8)
            //{
            for (int i = 0; i < str.Length; i++)
                {
                    temp[i] = str[i];
                }
           // }
            Console.WriteLine("----------------------------");
            for (int i = 0; i < temp.Length; i++)
            {
                Console.WriteLine(temp[i]);
                if (temp[i] == '1')
                {
                    la[i].Text = "●";
                    la[i].ForeColor = Color.Red;
                }
                else
                {
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
        private void lable_init(List<Label> la,Panel panel1)
        {
            foreach (Control c in panel1.Controls)
            {
                if ((c as Label) != null)
                {   //如果是Label控件，加入链表
                    
                    la.Insert(0, (Label)c);
                }
            }

            for (int j = 0; j < La.Count; j++)
            {
                Console.WriteLine(La[j].Text);
                la[j].Text = "●";
                la[j].ForeColor = Color.Blue;
            }
        }

        private void TestPLC_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            lable_init(La,panel1);//光电开关状态的lable
            lable_init(La1,panel2);
            lable_init(La2, panel6);
            lable_init(La3, panel7);
            dianjiLableInit();
            closebtn.Enabled = false;//使断开的按钮，无法点击
            testButton.Enabled = false;
            DT_lable();
        }

       

        private void timersend_Tick(object sender, EventArgs e)
        {
            int isecond = 50;//以毫秒为单位
            timersend.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x01, 0x00, 0x00, 0x00, 0x1c };
            newclient1.Send(data);
        }

      

        private void closebtn_Click_1(object sender, EventArgs e)
        {
            timersend.Enabled = false;//定时查询任务关闭
            myThread.Abort();//接受消息任务关闭           
            newclient1.Close();//连接关闭
            newclient2.Close();//连接关闭
            link_btn.Enabled = true;
            testButton.Enabled = false;
            dt_shuju.Enabled = false;
        }

        private void receiveBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[12];

            data[0] = Convert.ToByte(textBox1.Text, 16);
            data[1] = Convert.ToByte(textBox2.Text, 16);
            data[2] = Convert.ToByte(textBox3.Text, 16);
            data[3] = Convert.ToByte(textBox4.Text, 16);
            data[4] = Convert.ToByte(textBox5.Text, 16);
            data[5] = Convert.ToByte(textBox6.Text, 16);
            data[6] = Convert.ToByte(textBox7.Text, 16);
            data[7] = Convert.ToByte(textBox8.Text, 16);
            data[8] = Convert.ToByte(textBox9.Text, 16);
            data[9] = Convert.ToByte(textBox10.Text, 16);
            data[10] = Convert.ToByte(textBox11.Text, 16);
            data[11] = Convert.ToByte(textBox12.Text, 16);
            for (int i = 0; i < 12; i++)
            {
                Console.WriteLine(data[i]);
            }
            newclient1.Send(data);
        }
        Boolean timeflag = false;
        private void button1_Click_1(object sender, EventArgs e)
        {
            timersend.Enabled = timeflag;//定时任务开
            timeflag = !timeflag;
            if (timeflag)
            {
                timestatebtn.Text = "定时器开";
            }
            else
            {
                timestatebtn.Text = "定时器关";
            }
           


        }
        Boolean dianji0State = false;
        private void button1_Click_2(object sender, EventArgs e)
        {
            //byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, 0x00, 0x00, 0xff, 0x00 };
            //newclient.Send(data);
            if (dianji0State)
            {
                button1.Text = "电机0启动";
                dianjiState(0x00, 0x00);
                dianji0State = !dianji0State;


            }
            else
            {
                button1.Text = "电机0停止";

                
                dianjiState(0x00, 0xff);
                dianji0State = !dianji0State;
            }
            

        }
        public void dianjiState(byte add,byte state)
        {
            byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x00, add, state, 0x00 };
            //byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x05, 0x01, 0x00, 0x00, 0x00, 0x00 };
            newclient1.Send(data);
        }
        public void dianjiLableInit()
        {
            label67.Text = "●";
            label67.ForeColor = Color.Blue;
        }

        private void timerdianji_Tick(object sender, EventArgs e)
        {
            int isecond = 50;//以毫秒为单位
            dt_shuju.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0xff, 0x03, 0x00, 0x00, 0x00, 0x10 };
            newclient1.Send(data);
        }
         static int i = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            //i++;
            i =Convert.ToInt16(ucTrackBar.Value);
            
            byte low = Convert.ToByte(i & 0xff);  // 低8位
            byte high = Convert.ToByte((i >> 8) & 0xff); // 高8位
            //byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x00, 0x05, 0x00, add, state, 0x00 };
            byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x05, 0x06, 0x00, 0x00, high, low };
            newclient1.Send(data);
          
        }
        public int convert(byte h, byte l)
        {
            int res = 0;
            res = (h << 8) | (l);
            return res;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[12];

            data[0] = Convert.ToByte(textBox25.Text, 16);
            data[1] = Convert.ToByte(textBox24.Text, 16);
            data[2] = Convert.ToByte(textBox23.Text, 16);
            data[3] = Convert.ToByte(textBox22.Text, 16);
            data[4] = Convert.ToByte(textBox21.Text, 16);
            data[5] = Convert.ToByte(textBox20.Text, 16);
            data[6] = Convert.ToByte(textBox18.Text, 16);
            data[7] = Convert.ToByte(textBox17.Text, 16);
            data[8] = Convert.ToByte(textBox19.Text, 16);
            data[9] = Convert.ToByte(textBox16.Text, 16);
            data[10] = Convert.ToByte(textBox15.Text, 16);
            data[11] = Convert.ToByte(textBox14.Text, 16);
            for (int i = 0; i < 12; i++)
            {
                Console.WriteLine(data[i]);
            }
            newclient2.Send(data);
        }
    }
}
    
