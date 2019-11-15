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
        public Thread myThread3;
        public Thread myThread4;
        public Thread myThread5;
        public Thread myThread6;
        static int per_len = 4;//整盒测量时，每个盘片获取数据量，范围是1-25
        int total_len = 5 * per_len;
        int test_per_len = 10;//测试单片时，盘片获取的数据量，范围是1-20；
        public delegate void MyInvoke(string str);
        List<Label> La = new List<Label>();
        List<Label> La1 = new List<Label>();
        List<Label> La2 = new List<Label>();
        List<Label> La3 = new List<Label>();

        List<Label> dt_data_list_1 = new List<Label>();
        List<Label> dt_data_list_2 = new List<Label>();
        List<Label> dt_data_list_3 = new List<Label>();
        List<Label> dt_data_list_4 = new List<Label>();
        List<Label> dt_data_list_5 = new List<Label>();
        List<Label> test_per_list = new List<Label>();
        static int[] DT_data1 = new int[125];
        static int[] DT_data2 = new int[125];
        static int[] DT_data3 = new int[125];
        static int[] DT_data4 = new int[125];
        static int[] DT_data5 = new int[125];
        static int[] one_arr = new int[20];//测试一个盘片的厚度
        static int[] cal_data = new int[25];//储存25片最终的数据数据
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
            dt_data_list_1.Add(label73); dt_data_list_1.Add(label72); dt_data_list_1.Add(label71); dt_data_list_1.Add(label70); dt_data_list_1.Add(label69);
            dt_data_list_2.Add(label74); dt_data_list_2.Add(label75); dt_data_list_2.Add(label76); dt_data_list_2.Add(label77); dt_data_list_2.Add(label78);
            dt_data_list_3.Add(label80); dt_data_list_3.Add(label81); dt_data_list_3.Add(label82); dt_data_list_3.Add(label90); dt_data_list_3.Add(label91);
            dt_data_list_4.Add(label92); dt_data_list_4.Add(label93); dt_data_list_4.Add(label94); dt_data_list_4.Add(label85); dt_data_list_4.Add(label86);
            dt_data_list_5.Add(label87); dt_data_list_5.Add(label88); dt_data_list_5.Add(label89); dt_data_list_5.Add(label83); dt_data_list_5.Add(label84);

            test_per_list.Add(label95); test_per_list.Add(label96); test_per_list.Add(label97); test_per_list.Add(label98); test_per_list.Add(label99);
            test_per_list.Add(label100); test_per_list.Add(label101); test_per_list.Add(label102); test_per_list.Add(label103); test_per_list.Add(label104);
            test_per_list.Add(label105); test_per_list.Add(label106); test_per_list.Add(label107); test_per_list.Add(label108); test_per_list.Add(label109);
            test_per_list.Add(label110); test_per_list.Add(label111); test_per_list.Add(label112); test_per_list.Add(label113); test_per_list.Add(label114);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lable_list">lable 列表显示</param>
        /// <param name="data">与列表相对应的数组</param>
        /// <param name="len">lable的数量</param>
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
               // MessageBox.Show("链接第二个成功");
                link_btn.Enabled = false;//使连接按钮变成虚的，无法点击
                closebtn.Enabled = true;//断开的按钮，可以点击
                testButton.Enabled = true;
                

                timer1.Enabled = true;
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

            ThreadStart myThreaddelegate2 = new ThreadStart(ReceiveMsg);
            myThread2 = new Thread(myThreaddelegate2);
            myThread2.Start();

            ThreadStart myThreaddelegate3 = new ThreadStart(ReceiveMsg);
            myThread3 = new Thread(myThreaddelegate3);
            myThread3.Start();

            ThreadStart myThreaddelegate4 = new ThreadStart(ReceiveMsg);
            myThread4 = new Thread(myThreaddelegate4);
            myThread4.Start();

            ThreadStart myThreaddelegate5 = new ThreadStart(ReceiveMsg);
            myThread5 = new Thread(myThreaddelegate5);
            myThread5.Start();
            //   timersend.Enabled = true;//定时任务开

        }
        //消息显示
        int j = 0;
        int m = 0;
        int flag = 0;
        int one_sum_res = 0;
        int one_arr_res = 0;
        int test_min = 65535;
        int test_max = 0;
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

                /* if (data[7] == 0x01)//如果功能码是读线圈
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

                 }*/
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
                /* if (data[0] == 0x01)
                 {
                     for (int j = 0; j < total_len; j++)
                     {
                         DT_data1[j] = convert(data[9 + m], data[10 + m]);
                         m += 2;
                     }
                     m = 0;

                     for (int j = 0; j < total_len; j++)
                     {
                         Console.Write(DT_data1[j] + " ");
                     }
                     Console.WriteLine();
                     int[] arr_avg_res = avg_res(DT_data1);

                    // show_dt_data(dt_data_list_1, arr_avg_res, 5);
                     showMsg(stringdata + "\r\n");

                 }

                 if (data[0] == 0x02)
                 {
                     for (int j = 0; j < total_len; j++)
                     {
                         DT_data2[j] = convert(data[9 + m], data[10 + m]);
                         m += 2;
                     }
                     m = 0;

                     for (int j = 0; j < total_len; j++)
                     {
                         Console.Write(DT_data2[j] + " ");
                     }
                     Console.WriteLine();
                     int[] arr_avg_res = avg_res(DT_data2);

                    // show_dt_data(dt_data_list_2, arr_avg_res, 5);
                     showMsg(stringdata + "\r\n");

                 }*/
               
              
                if (data[0] == 0x00)
                {
                    flag = convert(data[9], data[10]);
                    

                }

                if (flag <= 25 && flag >=21)
                  {
                      showNum(data, dt_data_list_5, DT_data5, 0x05, 5);
                  }
                  if (flag <= 20 && flag >= 16)
                  {
                     
                      showNum(data, dt_data_list_4, DT_data4, 0x04, 4);

                  }
                if (flag <= 15 && flag >= 11)
                  {
                      
                      showNum(data, dt_data_list_3, DT_data3, 0x03, 3);
                  } 
                if (flag <= 10 && flag >= 6)
                {
                    
                    showNum(data, dt_data_list_2, DT_data2, 0x02, 2);

                }
               
                if (flag <= 5 && flag >= 1)
                {
                  
                    showNum(data, dt_data_list_1, DT_data1, 0x01, 1);
                }
               
               
                for (int i =0; i < 25;i++)
                {
                    Console.Write(cal_data[i] + "  ");
                }
               // showNum(data, dt_data_list_1, DT_data1, 0x01, 1);
                Console.WriteLine();
                int[] temp = cal_25_item(cal_data); //求解一盒盘片的最大值、最小值、平均值以及正负误差
                label120.Text = temp[0].ToString();//显示最大值
                label122.Text = temp[1].ToString();//显示最小值
                label126.Text = temp[2].ToString();//显示偏差
                label124.Text = temp[3].ToString();//显示平均値
               
                //以下内容是测量一片的调试内容
                //todu 每一片的最大值和最小值
               
                if (data[0] == 0x06)//读取测试一片数据的返回结果
                {
                    int n = 0;
                    for (int j = 0; j < test_per_len; j++)
                    {
                        //
                        one_arr[j] = convert(data[9 + n], data[10 + n]);
                        test_max = Math.Max(test_max, one_arr[j]);
                        test_min = Math.Min(test_min, one_arr[j]);
                        n += 2;
                    }
                    n = 0;
                }
                label127.Text = test_max.ToString();//显示最大值
                label129.Text = test_min.ToString();//显示最小值
                show_dt_data(test_per_list, one_arr, test_per_len);
                one_sum_res = 0;
                for (int i = 0; i < test_per_len; i++)
                {
                   // Console.Write(one_arr[i] + "  ");
                    one_sum_res += one_arr[i];
                }
                one_arr_res = (one_sum_res - test_min - test_max) / (test_per_len - 2);
                label116.Text = one_arr_res .ToString();//平均値
                Console.Write(one_arr_res);
                label117.Text = test_per_len.ToString();

            }
        }

        public void showNum(byte []data,List<Label> lable_list,int [] arr,byte mes,int start)
        {
            
            if (data[0] == mes)
            {
                int m = 0;
                for (int j = 0; j < total_len; j++)
                {
                    arr[j] = convert(data[9 + m], data[10 + m]);//获取寄存器中的数据
                    m += 2;
                }
                m = 0;

                for (int j = 0; j < total_len; j++)
                {
                  //  Console.Write(arr[j] + " ");
                }
                Console.WriteLine();
                int[] arr_avg_res = avg_res(arr);//计算出这个五片的数据
                for (int i = 0; i < 5; i++)
                {
                    cal_data[start * 5 + i - 5] = arr_avg_res[i];
                }
                show_dt_data(lable_list, arr_avg_res, 5);
               
               



            }
        }
        public int [] cal_25_item(int []arr)
        {
            int max = 0;
            int min = 65535;
            int[] res = new int[4];
            int sum = 0;
            for (int i = 0;i < arr.Length;i++)
            {
                max = Math.Max(max,arr[i]);
                min = Math.Min(min,arr[i]);
                sum += arr[i];

            }
            Console.WriteLine(sum);
            res[0] = max;
            res[1] = min;
            res[2] = max - min;
            res[3] =( sum  - res[2])/ 23;
            return res;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr">寄存器的全部数值的长度</param>       
        /// <returns>返回5片的计算结果计算</returns>
        public int[] avg_res(int []arr)
        {
            int []res = new int[5];//返回平均値计算结果
            for (int i = 0;i < 5; i++)
            {
                res[i] = cal_arr_avg(arr,i * per_len);
            }
            return res;

        }
        
        /// <summary>
        /// 统计寄存器中每一片的平均値。去除一个最大值和一个最小值
        /// </summary>
        /// <param name="arr">寄存器的全部数值的长度</param>
        /// <param name="start">每次计算的起始位置</param>
        /// <returns>返回平均値计算结果</returns>
        
        public int cal_arr_avg(int [] arr,int start)
        {
            int res = 0;
            int max = 0;
            int min = 65536;
            for (int i = start; i < start + per_len; i++)
            {
                res += arr[i];
                max = Math.Max(max, arr[i]);
                min = Math.Min(min,arr[i]);
            }
            res = res - max - min;//去掉最大值和最小值
            return (res/(per_len-2));//取平均值
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
                //Console.WriteLine(La[j].Text);
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
            dt1.Enabled = false;
            dt2.Enabled = false;
            dt3.Enabled = false;
            dt4.Enabled = false;
            dt5.Enabled = false;
            timer1.Enabled = false;
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
        //plc读取寄存器
        /// <summary>
        /// 读取1-5，寄存器地址1-125
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerdianji_Tick(object sender, EventArgs e)
        {
            byte low = Convert.ToByte(total_len & 0xff);  // 低8位
            byte high = Convert.ToByte((total_len >> 8) & 0xff); // 高8位

            int isecond = 500;//以毫秒为单位
            dt1.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x06, 0xff, 0x03, 0x00, 0x01, high, low };
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

        private void dt2_Tick(object sender, EventArgs e)
        {
            byte low = Convert.ToByte(total_len & 0xff);  // 低8位
            byte high = Convert.ToByte((total_len >> 8) & 0xff); // 高8位

            int isecond = 100;//以毫秒为单位
            dt2.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x02, 0x00, 0x00, 0x00, 0x00, 0x06, 0xff, 0x03, 0x00, 0x7e, high, low };
            newclient1.Send(data);
        }

        private void dt3_Tick(object sender, EventArgs e)
        {
            byte low = Convert.ToByte(total_len & 0xff);  // 低8位
            byte high = Convert.ToByte((total_len >> 8) & 0xff); // 高8位

            int isecond = 100;//以毫秒为单位
            dt3.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x03, 0x00, 0x00, 0x00, 0x00, 0x06, 0xff, 0x03, 0x00, 0xfb, high, low };
            newclient1.Send(data);
        }

        private void dt4_Tick(object sender, EventArgs e)
        {
            byte low = Convert.ToByte(total_len & 0xff);  // 低8位
            byte high = Convert.ToByte((total_len >> 8) & 0xff); // 高8位

            int isecond = 100;//以毫秒为单位
            dt4.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x04, 0x00, 0x00, 0x00, 0x00, 0x06, 0xff, 0x03, 0x01, 0x78, high, low };
            newclient1.Send(data);

        }

        private void dt5_Tick(object sender, EventArgs e)
        {
            byte low = Convert.ToByte(total_len & 0xff);  // 低8位
            byte high = Convert.ToByte((total_len >> 8) & 0xff); // 高8位

            int isecond = 100;//以毫秒为单位
            dt5.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x05, 0x00, 0x00, 0x00, 0x00, 0x06, 0xff, 0x03, 0x01, 0xf5, high, low };
            newclient1.Send(data);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            byte low = Convert.ToByte(total_len & 0xff);  // 低8位
            byte high = Convert.ToByte((total_len >> 8) & 0xff); // 高8位

            int isecond = 100;//以毫秒为单位
            timer1.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0xff, 0x03, 0x00, 0x00, 0x00, 0x01 };
            newclient1.Send(data);
        }

        private void celiang_yihe_timer2_Tick(object sender, EventArgs e)
        {
            byte low = Convert.ToByte(test_per_len & 0xff);  // 低8位
            byte high = Convert.ToByte((test_per_len >> 8) & 0xff); // 高8位

            int isecond = 100;//以毫秒为单位
            celiang_yihe_timer2.Interval = isecond;//50ms触发一次
            byte[] data = new byte[] { 0x06, 0x00, 0x00, 0x00, 0x00, 0x06, 0xff, 0x03, 0x03, 0xe8, high, low };
            newclient1.Send(data);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            celiang_yihe_timer2.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            celiang_yihe_timer2.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dt1.Enabled = true;
            dt2.Enabled = true;
            dt3.Enabled = true;
            dt4.Enabled = true;
            dt5.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dt1.Enabled = false;
            dt2.Enabled = false;
            dt3.Enabled = false;
            dt4.Enabled = false;
            dt5.Enabled = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
           test_per_len = Convert.ToInt16( ucTrackBar1.Value);
        }
    }
}
    
