using System;
using System.Collections.Generic;
using System.Text;
using NModbus;
using System.Net.Sockets;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Linq;
using HslCommunication.ModBus;
using HslCommunication;
namespace DTM
{
    class BoxState
    {
        //private ModbusFactory modbusFactory;
        //private IModbusMaster master;
        //写线圈或写寄存器数组
        //bool[] coilsBuffer;
        //ushort[] registerBuffer;            
        //参数(分别为站号,起始地址,长度)
        //byte slaveAddress;
        //ushort startAddress;
        //ushort numberOfPoints;
        private ModbusTcpNet busTcpClient1 = MainForm.busTcpClient1;
        private ModbusTcpNet busTcpClient2 = MainForm.busTcpClient2;
        public string[] numberId = new string[2];//扫码枪扫出来的盒子id
        public List<BoxState> list;
        public static long warnTime = 7000;
        public static bool warning_loss_box = false;//非法拿走盒子
        //public static List<string> InList = new List<string>();//进队排队队列
        public static List<string> OutList = new List<string>();//出队排队队列
        public bool measureFlag = false;//测厚仪器上传送带上有两个盒子
        public bool changeFlag = true;//换盒传送带上是一组的第一个盒子
        public bool changeBoxFirstFlag = true;//第一个盒子在换料站
        public static bool chooseComplete = false;//抽检完成 todo
        public bool exflag = false;//是否从换料轨上来的
        public int positionState = 0;//盒子位置状态
                                     //public bool measureState = false;//盒子测量通过状态
        public bool pastmeasureState = false;//盒子是否换料过
        public bool reach = false;//盒子到达末尾位置
                                  //public static int boxCount=0;//盒子抽检数目
        public bool chooseFlag = false;//是否被抽检到
                                       //public int boxId;//盒子id
        public string barCode;//条形码
        public int standard_pan_thickness;//盘片厚度（标准）
                                          //public int [] measure_pan_thickness = new int[50];//盘片厚度（测量）
                                          //public bool[] measure_pan_thickness_flag = new bool[50];//需要换的盘片
        public BoxState(List<BoxState> list, int standard_pan_thickness, string barCode, string[] numberid)
        {
            this.list = list;
            this.barCode = barCode;
            //this.boxId = boxId;
            this.standard_pan_thickness = standard_pan_thickness;
            this.numberId[0] = numberid[0];
            this.numberId[1] = numberid[1];
        }
        public void Run()
        {
            lock (list)
            {
                list.Add(this);
            }
            /*if (MainForm.reflag)
            {
                lock (InList)
                {
                    InList.Add(this.numberId[0]);
                }
            }
            string temp = "";
            lock (InList)
            {
                temp = InList[0];
            }*/
            /*while (true)
            {                    
                if (MainForm.reflag) break;
                Thread.Sleep(10);
            }*/
            /*if (positionState == 0 && (this.numberId[0] == temp) && (!MainForm.reflag))
            {
                while (true)
                {
                    if (busTcpClient1.ReadCoil("2096").Content)
                    {
                        break;
                    }
                    if (busTcpClient1.ReadCoil("2097").Content)
                    {
                        while (true)
                        {
                            Thread.Sleep(10);
                            //coilsBuffer = master.ReadCoils(0,2107,1);//参数为光电开光19
                            if (busTcpClient1.ReadCoil("2107").Content)
                            {
                                while (true)
                                {
                                    Thread.Sleep(10);
                                    //coilsBuffer = master.ReadCoils(0,2106,1);//参数为光电开光18
                                    if (busTcpClient1.ReadCoil("2106").Content)
                                    {
                                        lock (InList)
                                        {
                                            InList.RemoveAt(0);
                                        }
                                        positionState = 1;
                                        break;
                                    }
                                }
                                break;
                            }

                        }
                    }
                }
            }*/
            if (positionState == 5 && reach && (!MainForm.reflag))
            {
                while (true)
                {
                    Thread.Sleep(10);
                    //coilsBuffer = master.ReadCoils(0, 2241, 1);//参数为光电开光35
                    if (!busTcpClient2.ReadCoil("2241").Content)
                    {
                        while (true)
                        {
                            Thread.Sleep(10);
                            //coilsBuffer = master.ReadCoils(0, 2242, 1);//参数为光电开光36
                            if (!busTcpClient2.ReadCoil("2242").Content)
                            {
                                positionState = 6;
                                lock (OutList)
                                {
                                    OutList.Remove(this.numberId[0]);
                                }
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            //初始化modbusmaster
            //modbusFactory = new ModbusFactory();
            //在本地测试 所以使用回环地址,modbus协议规定端口号 502
            /*try
            {
                //master = modbusFactory.CreateMaster(new TcpClient("192.168.1.5", 502));
                busTcpClient1.ConnectServer();
            }
            catch(Exception ex)
            {                
                return;
            }*/
            //设置读取超时时间
            // master.Transport.ReadTimeout = 2000;
            //master.Transport.Retries = 2000;
            //故障之后重新定位位置
            /*if (positionState == 2 && measureFlag == false)
            {
                positionState = 1;
            }
            if (positionState == 4 && changeFlag == true)
            {
                positionState = 3;
            }*/
            while (true)
            {
                try
                {
                    if (positionState == 6) break;
                    sign(positionState, measureFlag, exflag, changeBoxFirstFlag, changeFlag);
                }
                catch (Exception ex)
                {
                    MainForm.runSuccess = false;
                    return;
                }
            }
        }
        private void sign(int positionState, bool measureFlag, bool exflag, bool changeBoxFirstFlag, bool changeFlag)
        {
            if (positionState == 0) { waitSign0(); return; }
            if (positionState == 20) { waitSign20(); return; }
            if (positionState == 21) { waitSign21(); return; }
            if (positionState == 22) { waitSign22(); return; }
            if (positionState == 23) { waitSign23(); return; }
            if (positionState == 24) { waitSign24(); return; }
            if (positionState == 25) { waitSign25(); return; }
            if (positionState == 26) { waitSign26(); return; }
            if (positionState == 1) { waitSign1(); return; }
            if (positionState == 2 && (!measureFlag) && (!exflag)) { waitSign2_1(); return; }
            if (positionState == 2 && measureFlag && exflag) { waitSign2(); return; }
            if (positionState == 2 && (!measureFlag) && exflag) { waitSign2_2(); return; }
            if (positionState == 3) { waitSign3(); return; }
            if (positionState == 4 && changeFlag) { waitSign4_1(); return; }
            if (positionState == 4 && (!changeFlag)) { waitSign4(); return; }
            if (positionState == 5) { waitSign5(); return; }
            if (positionState == 7) { waitSign7(); return; }
            if (positionState == 8 && changeBoxFirstFlag) { waitSign8_1(); return; }
            if (positionState == 8 && (!changeBoxFirstFlag)) { waitSign8(); return; }
            if (positionState == 9) { waitSign9(); return; }
            if (positionState == 10) { waitSign10(); return; }
            if (positionState == 11) { waitSign11(); return; }

        }
        private long nowTime()
        {
            long currentTicks = DateTime.Now.Ticks;
            DateTime dtFrom = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long currentMillis = (currentTicks - dtFrom.Ticks) / 10000;
            return currentMillis;
        }

        private void waitSign0()
        {
            while (true)
            {
                Thread.Sleep(10);
                if (busTcpClient1.ReadCoil("2083").Content)//气缸1落下
                {
                    long past = nowTime();
                    while (true)
                    {
                        Thread.Sleep(10);
                        long now1 = nowTime();
                        if (now1 - past >= warnTime)
                        {
                            warning_loss_box = true;
                        }
                        if (busTcpClient1.ReadCoil("2053").Content)//参数为光电开光5
                        {
                            while (true)
                            {
                                Thread.Sleep(10);
                                long now2 = nowTime();
                                if (now2 - past >= warnTime)
                                {
                                    warning_loss_box = true;
                                }
                                if (busTcpClient1.ReadCoil("2052").Content)//参数为光电开光4
                                {
                                    positionState = 21;
                                    warning_loss_box = false;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
            }
            return;
        }
        private void waitSign21()
        {
            if (busTcpClient1.ReadCoil("2084").Content)//气缸2顶起
            {
                while (true)
                {
                    Thread.Sleep(10);
                    if (busTcpClient1.ReadCoil("2085").Content)
                    {
                        warning_loss_box = false;
                        break;//气缸2落下
                    }
                    if (!busTcpClient1.ReadCoil("2052").Content || !busTcpClient1.ReadCoil("2053").Content)//光电开关5和光电开关4
                    {
                        warning_loss_box = true;
                    }
                    else
                    {
                        warning_loss_box = false;
                    } 
                }
            }          
                long past = nowTime();
                while (true)
                {
                    Thread.Sleep(10);
                    long now1 = nowTime();
                    if (now1 - past >= warnTime)
                    {
                        warning_loss_box = true;
                    }
                    if (busTcpClient1.ReadCoil("2055").Content)//参数为光电开光7
                    {
                        while (true)
                        {
                            Thread.Sleep(10);
                            long now2 = nowTime();
                            if (now2 - past >= warnTime)
                            {
                                warning_loss_box = true;
                            }
                            if (busTcpClient1.ReadCoil("2054").Content)//参数为光电开光6
                            {
                                positionState = 22;
                                warning_loss_box = false;
                                break;
                            }
                        }
                        break;
                    }
                }

            }
        private void waitSign22()
        {
            if (busTcpClient1.ReadCoil("2086").Content)//气缸3顶起
            {
                while (true)
                {
                    Thread.Sleep(10);
                    if (busTcpClient1.ReadCoil("2087").Content) {
                        warning_loss_box = false;
                        break;//气缸3落下
                    }
                    if (!busTcpClient1.ReadCoil("2055").Content || !busTcpClient1.ReadCoil("2054").Content)//光电开关7和光电开关6
                    {
                        warning_loss_box = true;
                    }
                    else
                    {
                        warning_loss_box = false;
                    }
                    
                }
            }
            long past = nowTime();
            while (true)
            {
                Thread.Sleep(10);
                long now1 = nowTime();
                if (now1 - past >= warnTime)
                {
                    warning_loss_box = true;
                }
                if (busTcpClient1.ReadCoil("2057").Content)//参数为光电开光9
                {
                    while (true)
                    {
                        Thread.Sleep(10);
                        long now2 = nowTime();
                        if (now2 - past >= warnTime)
                        {
                            warning_loss_box = true;
                        }
                        if (busTcpClient1.ReadCoil("2056").Content)//参数为光电开光8
                        {
                            positionState = 23;
                            warning_loss_box = false;
                            break;
                        }
                    }
                    break;
                }
            }

        }
        private void waitSign23()
        {
            if (busTcpClient1.ReadCoil("2088").Content)//气缸4顶起
            {
                while (true)
                {
                    Thread.Sleep(10);
                    if (busTcpClient1.ReadCoil("2089").Content) {
                        warning_loss_box = false;
                        break;//气缸4落下
                    }
                    if (!busTcpClient1.ReadCoil("2056").Content || !busTcpClient1.ReadCoil("2057").Content)//光电开关8和光电开关9
                    {
                        warning_loss_box = true;
                    }
                    else
                    {
                        warning_loss_box = false;
                    }                    
                }
            }
            long past = nowTime();
            while (true)
            {
                Thread.Sleep(10);
                long now1 = nowTime();
                if (now1 - past >= warnTime)
                {
                    warning_loss_box = true;
                }
                if (busTcpClient1.ReadCoil("2059").Content)//参数为光电开光11
                {
                    while (true)
                    {
                        Thread.Sleep(10);
                        long now2 = nowTime();
                        if (now2 - past >= warnTime)
                        {
                            warning_loss_box = true;
                        }
                        if (busTcpClient1.ReadCoil("2058").Content)//参数为光电开光10
                        {
                            positionState = 24;
                            warning_loss_box = false;
                            break;
                        }
                    }
                    break;
                }
            }

        }
        private void waitSign24()
        {
            if (busTcpClient1.ReadCoil("2090").Content)//气缸5顶起
            {
                while (true)
                {
                    Thread.Sleep(10);
                    if (busTcpClient1.ReadCoil("2091").Content)
                    {
                        warning_loss_box = false;
                        break;//气缸5落下
                    }
                    if (!busTcpClient1.ReadCoil("2059").Content || !busTcpClient1.ReadCoil("2058").Content)//光电开关10和光电开关11
                    {
                        warning_loss_box = true;
                    }
                    else
                    {
                        warning_loss_box = false;
                    }
                    
                }
            }
            long past = nowTime();
            while (true)
            {
                Thread.Sleep(10);
                long now1 = nowTime();
                if (now1 - past >= warnTime)
                {
                    warning_loss_box = true;
                }
                if (busTcpClient1.ReadCoil("2061").Content)//参数为光电开光13
                {
                    while (true)
                    {
                        Thread.Sleep(10);
                        long now2 = nowTime();
                        if (now2 - past >= warnTime)
                        {
                            warning_loss_box = true;
                        }
                        if (busTcpClient1.ReadCoil("2060").Content)//参数为光电开光12
                        {
                            positionState = 25;
                            warning_loss_box = false;
                            break;
                        }
                    }
                    break;
                }
            }

        }
        private void waitSign25()
        {
            if (busTcpClient1.ReadCoil("2092").Content)//气缸6顶起
            {
                while (true)
                {
                    Thread.Sleep(10);
                    if (busTcpClient1.ReadCoil("2093").Content)
                    {
                        warning_loss_box = false;
                        break;//气缸6落下
                    }
                    if (!busTcpClient1.ReadCoil("2061").Content || !busTcpClient1.ReadCoil("2060").Content)//光电开关13和光电开关12
                    {
                        warning_loss_box = true;
                    }
                    else
                    {
                        warning_loss_box = false;
                    }
                    
                }
            }
            long past = nowTime();
            while (true)
            {
                Thread.Sleep(10);
                long now1 = nowTime();
                if (now1 - past >= warnTime)
                {
                    warning_loss_box = true;
                }
                if (busTcpClient1.ReadCoil("2063").Content)//参数为光电开光15
                {
                    while (true)
                    {
                        Thread.Sleep(10);
                        long now2 = nowTime();
                        if (now2 - past >= warnTime)
                        {
                            warning_loss_box = true;
                        }
                        if (busTcpClient1.ReadCoil("2062").Content)//参数为光电开光14
                        {
                            positionState = 26;
                            warning_loss_box = false;
                            break;
                        }
                    }
                    break;
                }
            }

        }
        private void waitSign26()
        {
            if (busTcpClient1.ReadCoil("2094").Content)//气缸7顶起
            {
                while (true)
                {
                    Thread.Sleep(10);
                    if (busTcpClient1.ReadCoil("2095").Content)
                    {
                        warning_loss_box = false;
                        break;//气缸7落下
                    }
                    if (!busTcpClient1.ReadCoil("2063").Content || !busTcpClient1.ReadCoil("2062").Content)//光电开关15和光电开关14
                    {
                        warning_loss_box = true;
                    }
                    else
                    {
                        warning_loss_box = false;
                    }
                    
                }
            }
            long past = nowTime();
            while (true)
            {
                Thread.Sleep(10);
                long now1 = nowTime();
                if (now1 - past >= warnTime)
                {
                    warning_loss_box = true;
                }
                if (busTcpClient1.ReadCoil("2065").Content)//参数为光电开光17
                {
                    while (true)
                    {
                        Thread.Sleep(10);
                        long now2 = nowTime();
                        if (now2 - past >= warnTime)
                        {
                            warning_loss_box = true;
                        }
                        if (busTcpClient1.ReadCoil("2064").Content)//参数为光电开光16
                        {
                            positionState = 20;
                            warning_loss_box = false;
                            break;
                        }
                    }
                    break;
                }
            }

        }

        private void waitSign20()
        {   //master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            if (busTcpClient1.ReadCoil("2096").Content)//气缸8顶起
            {
                while (true)
                {
                    Thread.Sleep(10);
                    if (busTcpClient1.ReadCoil("2097").Content)
                    {
                        warning_loss_box = false;
                        break;//气缸8落下
                    }
                    if (!busTcpClient1.ReadCoil("2065").Content || !busTcpClient1.ReadCoil("2064").Content)//光电开关17和光电开关16
                    {
                        warning_loss_box = true;
                    }
                    else
                    {
                        warning_loss_box = false;
                    }
                    
                }
            }
            long past = nowTime();
            while (true)
            {
                Thread.Sleep(10);
                long now1 = nowTime();
                if (now1 - past >= warnTime)
                {
                    warning_loss_box = true;
                }
                if (busTcpClient1.ReadCoil("2107").Content)//参数为光电开光19
                {
                    while (true)
                    {
                        Thread.Sleep(10);
                        long now2 = nowTime();
                        if (now2 - past >= warnTime)
                        {
                            warning_loss_box = true;
                        }
                        if (busTcpClient1.ReadCoil("2106").Content)//参数为光电开光18
                        {
                            positionState = 1;
                            warning_loss_box = false;
                            break;
                        }
                    }
                    break;
                }
            }
            return;
        }
        private void waitSign1()
        {   //master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            if (busTcpClient1.ReadCoil("2122").Content)//气缸10顶起
            {
                while (true)
                {
                    Thread.Sleep(10);
                    if (busTcpClient1.ReadCoil("2123").Content)
                    {
                        warning_loss_box = false;
                        break;//气缸10落下
                    }
                    if (!busTcpClient1.ReadCoil("2106").Content || !busTcpClient1.ReadCoil("2107").Content)//光电开关18和光电开关19
                    {
                        warning_loss_box = true;
                    }
                    else
                    {
                        warning_loss_box = false;
                    }                   
                }
            }
            //new Thread(onetotwo).Start();
            long past = nowTime();
            while (true)
            {
                Thread.Sleep(10);                
                //coilsBuffer = master.ReadCoils(0,2107,1);//参数为光电开光19
                if (!busTcpClient1.ReadCoil("2107").Content)
                {
                    while (true)
                    {
                        Thread.Sleep(10);
                        long now1 = nowTime();
                        if (now1 - past >= warnTime)
                        {
                            warning_loss_box = true;
                        }
                        //coilsBuffer = master.ReadCoils(0, 2134,1);//参数为光电开光22
                        if (busTcpClient1.ReadCoil("2134").Content)
                        {
                            positionState = 2;
                            warning_loss_box = false;
                            measureFlag = false;
                            break;
                        }
                    }
                    break;
                }                       
            }
             //master.Dispose();
             return;
        }
        private void onrtotwo()
        {
            if (!busTcpClient1.ReadCoil("2106").Content)//光电开关18
            {

            }
        }
        private void waitSign2_1()
        {
            while (true)
            {
                Thread.Sleep(10);
                //coilsBuffer = master.ReadCoils(0,2135,1);//参数为光电开光23
                if (busTcpClient1.ReadCoil("2135").Content)
                {
                    while (true)
                    {
                        Thread.Sleep(10);
                        //coilsBuffer = master.ReadCoils(0,2134,1);//参数为光电开光22
                        if (busTcpClient1.ReadCoil("2134").Content)
                        {
                            measureFlag = true;
                            break;
                        }
                    }
                    break;
                }
            }
        }
        private void waitSign2()
        {
            Thread.Sleep(10);
            //thickSide();//测厚          
            //master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            //if (!measureFlag) return;           
            while (true)
            {
                Thread.Sleep(10);
                bool flag1 = busTcpClient1.ReadCoil("2554").Content;
                bool flag2 = busTcpClient1.ReadCoil("2555").Content;
                chooseFlag = busTcpClient1.ReadCoil("2557").Content;
                //coilsBuffer = master.ReadCoils(0,2134,1);//参数为光电开光22
                if (!busTcpClient1.ReadCoil("2134").Content)
                {
                    if ((!flag1)&&(!flag2)&&(!chooseFlag))
                    {
                     while (true)
                      {
                        Thread.Sleep(10);
                        //coilsBuffer = master.ReadCoils(0, 2177, 1);//参数为光电开光26
                        if (busTcpClient2.ReadCoil("2177").Content)
                        {
                            while (true)
                            {
                                Thread.Sleep(10);
                                //coilsBuffer = master.ReadCoils(0, 2177, 1);//参数为光电开光27
                                if (busTcpClient2.ReadCoil("2176").Content)
                                {
                                  positionState = 3;
                                  break;
                                }
                            }
                           break;
                        }
                    }
                break;
                    }
                    else
                    {
                        while (true)
                        {
                            Thread.Sleep(10);
                            //coilsBuffer = master.ReadCoils(0, 2179, 1);//参数为光电开光29
                            if (busTcpClient2.ReadCoil("2179").Content)
                            {
                                while (true)
                                {
                                    Thread.Sleep(10);
                                    //coilsBuffer = master.ReadCoils(0, 2178, 1);//参数为光电开光28
                                    if (busTcpClient2.ReadCoil("2178").Content)
                                    {
                                        positionState = 7;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        break;
                    }
                }
            }                             
            return;
        }
        /*private void thickSide()
        {
            int baseNumber = 0;
            if (measureFlag == true)
            {
                baseNumber = 25;
            }
            //master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            for (int i = 0; i < measure_pan_thickness.Length; i++)
            {                       
                slaveAddress = 0;startAddress = 3;
                master.WriteSingleCoil(slaveAddress, startAddress, true);//向plc告诉准备好开始读数据,每调用一次让plc换下一个盘片
                startAddress += 1; numberOfPoints = 1;       
                while (true)
                {   Thread.Sleep(100);
                    coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);//向plc询问是否可以接收数据了
                    if (!coilsBuffer[0]) break;
                }
                numberOfPoints = 10;
                registerBuffer = master.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
                int temp = 0;
                temp = evaluation(registerBuffer);
                measure_pan_thickness[baseNumber+i] = temp;
                if (temp < standard_pan_thickness * 0.9 || temp > standard_pan_thickness * 1.1)//+-10%误差
                {
                    measure_pan_thickness_flag[baseNumber+i] = true;
                    measureState = true;
                    pastmeasureState = true;
                }
            }
            if (measureState||boxCount == 40)
            {
                chooseFlag = true;
                if(boxCount==40)boxCount = 0;
            }
            slaveAddress = 30;                       
            if(measureFlag)master.WriteSingleCoil(slaveAddress, startAddress, measureState||chooseFlag);//告诉plc应该是用缓存轨还是换料轨
            if(measureFlag)measureState = false;
        }
        private int evaluation(ushort[] values) //求得数组舍弃一些差异较大的数据，并且求剩余数据平均值
        {
            int sum = 0;
            for (int i = 0; i < values.Length; i++)
            {
                sum += values[i];
            }
            float agv = (sum+0f) / values.Length;          
            List<float> templist = new List<float>();
            for (int i = 0; i < values.Length; i++)
            {
                templist.Add(values[i]-agv);
            }
            templist.Sort();
            float numberAdd = 0f;
            for (int i = 0; i < 6; i++)
            {
                numberAdd += (templist[i] + agv);
            }
            int number = (int)numberAdd / 6;
            return number;
        }*/
        private void waitSign3()
        {   //master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            while (true)
            {
                Thread.Sleep(10);
                //coilsBuffer = master.ReadCoils(0, 2176, 1);//参数为光电开光26
                if (!busTcpClient2.ReadCoil("2177").Content)
                {
                    while (true)
                    {
                        Thread.Sleep(10);
                        //coilsBuffer = master.ReadCoils(0, 2204, 1);//参数为光电开光30
                        if (busTcpClient2.ReadCoil("2204").Content)
                        {
                            positionState = 4;
                            changeFlag = true;
                            break;
                        }

                    }
                    break;
                }
            }
            //changeFlag = true;
            return;
        }
        private void waitSign7()
        {
            //master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            /*if (chooseFlag)
            {
              MessageBox.Show("请拿走抽检盒子！");
            }*/
            while (true)
            {
                Thread.Sleep(10);
                
                //coilsBuffer = master.ReadCoils(0, 2179, 1);//参数为光电开光29
                if (!busTcpClient2.ReadCoil("2179").Content)
                {
                    /*if (chooseFlag)
                    {
                        while (true)
                        {
                            Thread.Sleep(10);
                            //coilsBuffer = master.ReadCoils(0, 2178, 1);//参数为光电开光28
                            if (!busTcpClient2.ReadCoil("2178").Content)
                            {
                                positionState = 10;
                                break;
                            }
                        }
                        break;
                    }*/
                    //else
                    //{
                        while (true)
                        {
                            Thread.Sleep(10);
                            //coilsBuffer = master.ReadCoils(0, 2156, 1);//参数为光电开光24
                            if (busTcpClient2.ReadCoil("2156").Content)
                            {
                                while (true)
                                {
                                    Thread.Sleep(10);
                                    //coilsBuffer = master.ReadCoils(0, 2157, 1);//参数为光电开光25
                                    if (busTcpClient2.ReadCoil("2157").Content)
                                    {
                                        positionState = 8;
                                        changeBoxFirstFlag = true;                                        
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        break;
                   // }
                }
            }
            return;
        }
        private void waitSign4_1()
        {
            while (true)
            {
                Thread.Sleep(10);
                //coilsBuffer = master.ReadCoils(0, 2176, 1);//参数为光电开光27
                if (!busTcpClient2.ReadCoil("2176").Content)
                {     
                    while (true)
                    {
                        Thread.Sleep(10);
                        //coilsBuffer = master.ReadCoils(0, 2204, 1);//参数为光电开光30
                        if (busTcpClient2.ReadCoil("2204").Content)
                        {
                            changeFlag = false;
                            break;
                        }
                    }
                    break;
                }
            }
            return;
        }
        
        private void waitSign4()
        {   //master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            while (true)
            {
                Thread.Sleep(10);
                //coilsBuffer = master.ReadCoils(0, 2240, 1);//参数为光电开光34
                if (busTcpClient2.ReadCoil("2240").Content)
                {
                    while (true)
                    {
                        Thread.Sleep(10);
                        //coilsBuffer = master.ReadCoils(0, 2239, 1);//参数为光电开光33
                        if (busTcpClient2.ReadCoil("2239").Content)
                        {
                            if (MainForm.reflag)
                            {
                                lock (OutList)
                                {
                                    OutList.Add(this.numberId[0]);
                                }        
                            }
                            positionState = 5;
                            break;
                        }
                    }
                    break;
                }
            }            
            return;
        }
        private void waitSign5()
        {
            //master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            while (true)
            {
                
                Thread.Sleep(10);
                string temp = "";
                lock (OutList)
                {
                    temp = OutList[0];
                }
                if (temp == this.numberId[0])
                {
                  //coilsBuffer = master.ReadCoils(0, 2242, 1);//参数为光电开光36
                    if (busTcpClient2.ReadCoil("2242").Content)
                   {
                    while (true)
                     {
                        Thread.Sleep(10);
                        //coilsBuffer = master.ReadCoils(0, 2241, 1);//参数为光电开光35
                            if (busTcpClient2.ReadCoil("2241").Content)
                          {
                                reach = true;
                                while (true)
                                {
                                    Thread.Sleep(10);
                                    //coilsBuffer = master.ReadCoils(0, 2241, 1);//参数为光电开光35
                                    if (!busTcpClient2.ReadCoil("2241").Content)
                                    {
                                        while (true)
                                        {
                                            Thread.Sleep(10);
                                            //coilsBuffer = master.ReadCoils(0, 2242, 1);//参数为光电开光36
                                            if (!busTcpClient2.ReadCoil("2242").Content)
                                            {
                                                positionState = 6;
                                                lock (OutList)
                                                {
                                                    OutList.Remove(this.numberId[0]);
                                                }                                             
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                                break;                                
                           }
                     }
                        break;                   
                  }
               }
            }            
            return;
        }    
        private void waitSign8_1()
        {
            if (chooseFlag)
            {
                changeBoxFirstFlag = false;
                return;
            }
            while (true)
            {
                Thread.Sleep(10);
                changeBoxFirstFlag = true;
                //coilsBuffer = master.ReadCoils(0, 2157, 1);//参数为光电开光25
                if (!busTcpClient2.ReadCoil("2157").Content)
                {
                    changeBoxFirstFlag = false;                   
                    break;
                }
            }
        }
          
        private void waitSign8()
        {
            if (chooseFlag)
            {
                MessageBox.Show("请拿走抽检盒子！");
                while (true)
                {
                    Thread.Sleep(10);
                    if (!busTcpClient2.ReadCoil("2156").Content)//参数为光电开光24
                    {
                        while (true)
                        {
                            if (!busTcpClient2.ReadCoil("2157").Content) //参数为光电开光25
                            { 
                                positionState = 10;
                                break;
                            }                                
                        }
                        break;
                    }
                }
                return;
            }
            //master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            while (true)
            {
                Thread.Sleep(10);
                //coilsBuffer = master.ReadCoils(0, 2108, 1);//参数为光电开光20
                if (busTcpClient1.ReadCoil("2108").Content)
                {
                    while (true)
                    {
                        Thread.Sleep(10);
                        //coilsBuffer = master.ReadCoils(0, 2109, 1);//参数为光电开光21
                        if (busTcpClient1.ReadCoil("2109").Content)
                        {
                            positionState = 9;
                            exflag = true;
                            break;
                        }
                    }
                    break;
                }
            }
            return;
        }
        private void waitSign10()
        {   
            while (true)
            {
                Thread.Sleep(10);
                if (busTcpClient2.ReadCoil("2156").Content)//参数为光电开光24
                {
                    while (true)
                    {
                        if (busTcpClient2.ReadCoil("2157").Content) //参数为光电开光25
                        {
                            while (true) {
                            Thread.Sleep(30);
                            if (chooseComplete)
                            {
                            positionState = 11;
                            chooseComplete = false;
                            break;
                            }
                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }
        private void waitSign11()
        {
            while (true)
            {
                Thread.Sleep(10);
                if (!busTcpClient2.ReadCoil("2157").Content) //参数为光电开光25
                {
                    while (true)
                    {
                        Thread.Sleep(10);
                        if (busTcpClient1.ReadCoil("2108").Content)//参数为光电开光20
                        {
                            while (true)
                            {
                                Thread.Sleep(10);
                                if (busTcpClient1.ReadCoil("2109").Content)//参数为光电开光21
                                {
                                    positionState = 9;
                                    break;
                                }

                            }
                            break;
                        }
                            
                    }
                    break;
                }
            }
            return;
        }
        private void waitSign9()
        {
            while (true)
            {
                Thread.Sleep(10);
                //coilsBuffer = master.ReadCoils(0, 2109, 1);//参数为光电开光21
                if (!busTcpClient1.ReadCoil("2109").Content)
                {
                    while (true)
                    {
                        Thread.Sleep(10);
                        //coilsBuffer = master.ReadCoils(0, 2134, 1);//参数为光电开光22
                        if (busTcpClient1.ReadCoil("2134").Content)
                        {
                            positionState = 2;
                            measureFlag = false;
                            exflag = true;
                            break;
                        }
                    }
                    break;
                }
            }
            return;
        }

        private void waitSign2_2()
        {
            //master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            while (true)
            {
                Thread.Sleep(10);
                //coilsBuffer = master.ReadCoils(0, 2108, 1);//参数为光电开光20
                if (!busTcpClient1.ReadCoil("2108").Content)
                {
                    while (true)
                    {
                        Thread.Sleep(10);
                        //coilsBuffer = master.ReadCoils(0, 2135, 1);//参数为光电开光23
                        if (busTcpClient1.ReadCoil("2135").Content)
                        {                          
                            while (true)
                            {
                                Thread.Sleep(10);
                                //coilsBuffer = master.ReadCoils(0, 2134, 1);//参数为光电开光22
                                if (busTcpClient1.ReadCoil("2134").Content)
                                {
                                    measureFlag = true;
                                    exflag = false;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
            }
            //master.Dispose();
            return;

        }

    }
   
}
