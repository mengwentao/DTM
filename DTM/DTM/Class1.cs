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


namespace DTM
{
    class BoxState
    {
            private ModbusFactory modbusFactory;
            private IModbusMaster master;
            //写线圈或写寄存器数组
            bool[] coilsBuffer;
            ushort[] registerBuffer;
            //功能码
            string functionCode;
            //参数(分别为站号,起始地址,长度)
            byte slaveAddress;
            ushort startAddress;
            ushort numberOfPoints;
            public static List<BoxState> list = new List<BoxState>();
            public static List<int> InList = new List<int>();
            public int positionState = 0;//盒子位置状态
            public bool measureState = false;//盒子测量通过状态
            public bool pastmeasureState = false;//盒子是否换料过
            public static int boxCount=0;//盒子抽检数目
            public bool chooseFlag = false;//是否被抽检到
            public int boxId;//盒子id
            public int barCode;//条形码
            public int standard_pan_thickness;//盘片厚度（标准）
            public int [] measure_pan_thickness = new int[25];//盘片厚度（测量）
            public bool[] measure_pan_thickness_flag = new bool[25];//盘片厚度（测量）是否需要换片
        public BoxState(int standard_pan_thickness, int barCode, int boxId)
            {
                this.barCode = barCode;
                this.boxId = boxId;
                this.standard_pan_thickness = standard_pan_thickness;
            }
        public void Run()
        {
            list.Add(this);
            InList.Add(this.boxId);
            //初始化modbusmaster
            modbusFactory = new ModbusFactory();
            //在本地测试 所以使用回环地址,modbus协议规定端口号 502
            master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            //设置读取超时时间
            master.Transport.ReadTimeout = 2000;
            master.Transport.Retries = 2000;           
            while (positionState != 7|| positionState != 8)
            {
                sign(positionState, measureState);
            }
            list.Remove(this);
            //todo 发送给数据库          
        }
        private void sign(int positionState,bool measureState)
        {            
            if (positionState == 0) { waitSign0(); return; }
            if (positionState == 1) { waitSign1(); return; }
            if (positionState == 2) { waitSign2(); return; }
            if (positionState == 3&&measureState==false) { waitSign3_1(); return; }
            if (positionState == 3&&measureState==true) { waitSign3_2(); return; }//要退料或者达到抽检数量
            if (positionState == 4) { waitSign4(); return; }
            if (positionState == 5) { waitSign5(); return; }
            if (positionState == 6) { waitSign6(); return; }
            if (positionState == 9) { waitSign9(); return; }       
            if (positionState == 10) { waitSign10(); return; }

        }
        //向位置0的光电开关查询信号，光电触发结束函数，以此类推
        private void waitSign0()
        {
            while (true)
            {
                if (InList[0]== this.boxId)
                {
                    //todo 查询光电开关是否触发 （以下为本机测试）
                    master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
                    slaveAddress = 0; startAddress = 0; numberOfPoints = 1;
                    coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                    if (!coilsBuffer[0])
                    {
                     InList.RemoveAt(0);
                     break;
                    }        
                }
            }
            positionState = 1;
            return;
        }
        private void waitSign1()
        {
            while (true)
            {
                master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
                slaveAddress = 1; startAddress = 0; numberOfPoints = 1;
                coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                if (!coilsBuffer[0])
                {

                    break;
                }
            }
            positionState = 2;
            return;
        }
        private void waitSign2()
        {
            boxCount++;
            thickSide();//测厚          
            while (true)
            {
                master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
                slaveAddress = 2; startAddress = 0; numberOfPoints = 1;
                coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                if (!coilsBuffer[0])
                {
                    break;
                }
            }
            positionState = 3;
            return;
        }
        private void thickSide()
        {
            slaveAddress = 20;
            for (int i = 0; i < measure_pan_thickness.Length; i++)
            {
                master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
                startAddress = (ushort)i;
                master.WriteSingleCoil(slaveAddress, startAddress, true);
                Thread.Sleep(100);
                numberOfPoints = 10;
                registerBuffer = master.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
                int temp = 0;
                temp = evaluation(registerBuffer);
                measure_pan_thickness[i] = temp;
                if (temp < standard_pan_thickness * 0.9 || temp > standard_pan_thickness * 1.1)//+-10%误差
                {
                    measure_pan_thickness_flag[i] = true;
                    measureState = true;
                    pastmeasureState = true;
                }
            }
            if (boxCount == 40)
            {
                measureState = true;
                chooseFlag = true;
                boxCount = 0;
            }
            slaveAddress = 30;            
            master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            master.WriteSingleCoil(slaveAddress, startAddress, measureState);
            measureState = false;
        }
        private int evaluation(ushort[] values)//求得数组舍弃一些差异较大的数据，并且求剩余数据平均值
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
        }
        private void waitSign3_1()
        {
            while (true)
            {
                master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
                slaveAddress = 3; startAddress = 0; numberOfPoints = 1;
                coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                if (!coilsBuffer[0])
                {
                    break;
                }
            }
            positionState = 4;
            return;
        }
        private void waitSign3_2()
        {   master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            while (true)
            {
                if (chooseFlag)
                {                
                  slaveAddress = 31; startAddress = 0; numberOfPoints = 1;
                  coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                  if (!coilsBuffer[0])
                  {   positionState = 8;
                    break;
                  }
                }
                else
                {
                  slaveAddress = 32; startAddress = 0; numberOfPoints = 1;
                  coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                  if (!coilsBuffer[0])
                  {
                    positionState = 9;
                    break;
                   }
                }                     
            } 
            return;
        }
        
        private void waitSign4()
        {
            while (true)
            {
                master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
                slaveAddress = 4; startAddress = 0; numberOfPoints = 1;
                coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                if (!coilsBuffer[0])
                {

                    break;
                }
            }
            positionState = 5;
            return;
        }
        private void waitSign5()
        {
            while (true)
            {
                master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
                slaveAddress = 5; startAddress = 0; numberOfPoints = 1;
                coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                if (!coilsBuffer[0])
                {

                    break;
                }
            }
            positionState =6;
            return;
        }
        private void waitSign6()
        {
            while (true)
            {
                master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
                slaveAddress = 6; startAddress = 0; numberOfPoints = 1;
                coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                if (!coilsBuffer[0])
                {

                    break;
                }
            }
            positionState = 7;
            return;
        }
        private void waitSign9()
        {  
            while (true)
            {
                master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
                slaveAddress = 9; startAddress = 0; numberOfPoints = 1;
                coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                if (!coilsBuffer[0])
                {
                    break;
                }
            }
            positionState = 10;
            return;
        }
        private void waitSign10()
        {
            while (true)
            {
                master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
                slaveAddress = 10; startAddress = 0; numberOfPoints = 1;
                coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                if (!coilsBuffer[0])
                {

                    break;
                }
            }
            positionState = 2;
            return;
        }

    }
   
}
