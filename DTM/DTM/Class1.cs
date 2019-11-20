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

namespace DTM
{
    class BoxState
    {
            private ModbusFactory modbusFactory;
            private IModbusMaster master;
            //写线圈或写寄存器数组
            bool[] coilsBuffer;
            ushort[] registerBuffer;            
            //参数(分别为站号,起始地址,长度)
            byte slaveAddress;
            ushort startAddress;
            ushort numberOfPoints;
            public List<BoxState> list;
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
        public BoxState(List<BoxState>list,int standard_pan_thickness, int barCode, int boxId)
            {
                this.list = list;
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
            while (true)
            {
                if (positionState == 6 || positionState == 10) break;
                sign(positionState, measureState);
            }
            //todo 发送给数据库          
        }
        private void sign(int positionState,bool measureState)
        {            
            if (positionState == 0) { waitSign0(); return; }
            if (positionState == 1) { waitSign1(); return; }
            if (positionState == 2) { waitSign2(); return; }
            if (positionState == 3) { waitSign3(); return; }
            if (positionState == 4) { waitSign4(); return; }//要退料或者达到抽检数量
            if (positionState == 5) { waitSign5(); return; }
            if (positionState == 7) { waitSign7(); return; }           
            if (positionState == 8) { waitSign8(); return; }       
            if (positionState == 9) { waitSign9(); return; }

        }
        //向位置0的光电开关查询信号，光电触发结束函数，以此类推
        private void waitSign0()
        {   master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            while (true)
            {
                Thread.Sleep(100);
                if (InList[0]== this.boxId)
                {
                    //todo 查询光电开关是否触发 （以下为本机测试）
                    slaveAddress = 0; startAddress = 0; numberOfPoints = 1;
                    coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                    if (!coilsBuffer[0])
                    {
                     master.WriteSingleCoil(slaveAddress, startAddress, true);
                     InList.RemoveAt(0);
                     break;
                    }        
                }
            }
            master.Dispose();
            positionState = 1;
            return;
        }
        private void waitSign1()
        {   master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            while (true)
            {
                Thread.Sleep(100);
                slaveAddress = 0; startAddress = 1; numberOfPoints = 1;
                coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                if (!coilsBuffer[0])
                {
                    master.WriteSingleCoil(slaveAddress, startAddress, true);
                    break;
                }
            }
            master.Dispose();
            positionState = 6;
            return;
        }
        private void waitSign2()
        {
            Thread.Sleep(100);
            boxCount++;
            thickSide();//测厚          
            master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            while (true)
            {
                slaveAddress = 0; startAddress = 2; numberOfPoints = 1;
                coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                if (!coilsBuffer[0])
                {
                    master.WriteSingleCoil(slaveAddress, startAddress, true);
                    break;
                }
            }
            if (measureState)
            {
                positionState = 7;
            }
            else
            {
                positionState = 3;
            }
            return;
        }
        private void thickSide()
        {
           
            master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
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
            master.WriteSingleCoil(slaveAddress, startAddress, measureState);//告诉plc应该是用缓存轨还是换料轨
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
        private void waitSign3()
        {   master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            while (true)
            {
                Thread.Sleep(100);
                slaveAddress = 0; startAddress = 0; numberOfPoints = 1;
                coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                if (!coilsBuffer[0])
                {
                    master.WriteSingleCoil(slaveAddress, startAddress, true);
                    break;
                }
            }
            positionState = 4;
            return;
        }
        private void waitSign7()
        {
            master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            while (true)
            {
                Thread.Sleep(100);
                if (chooseFlag)//被选中抽检
                {                
                  slaveAddress = 0; startAddress = 0; numberOfPoints = 1;
                  coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                  if (!coilsBuffer[0])
                  {
                    positionState = 10;
                    master.WriteSingleCoil(slaveAddress, startAddress, true);
                    break;
                  }
                }
                else
                {
                  slaveAddress = 32; startAddress = 0; numberOfPoints = 1;
                  coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                  if (!coilsBuffer[0])
                  {
                    positionState = 8;
                    master.WriteSingleCoil(slaveAddress, startAddress, true);
                    break;
                   }
                }                     
            } 
            return;
        }
        
        private void waitSign4()
        {   master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            while (true)
            {
                Thread.Sleep(100);
                slaveAddress = 4; startAddress = 0; numberOfPoints = 1;
                coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                if (!coilsBuffer[0])
                {
                    master.WriteSingleCoil(slaveAddress, startAddress, true);
                    break;
                }
            }
            positionState = 5;
            return;
        }
        private void waitSign5()
        {
            master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            while (true)
            {
                Thread.Sleep(100);
                slaveAddress = 5; startAddress = 0; numberOfPoints = 1;
                coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                if (!coilsBuffer[0])
                {
                    master.WriteSingleCoil(slaveAddress, startAddress, true);
                    break;
                }
            }
            positionState =6;
            return;
        }        
        private void waitSign8()
        {
            master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            while (true)
            {
                Thread.Sleep(100);
                slaveAddress = 9; startAddress = 0; numberOfPoints = 1;
                coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                if (!coilsBuffer[0])
                {
                    master.WriteSingleCoil(slaveAddress, startAddress, true);
                    break;
                }
            }
            positionState = 9;
            return;
        }
        private void waitSign9()
        {
            master = modbusFactory.CreateMaster(new TcpClient("127.0.0.1", 502));
            while (true)
            {
                Thread.Sleep(100);
                slaveAddress = 10; startAddress = 0; numberOfPoints = 1;
                coilsBuffer = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                if (!coilsBuffer[0])
                {
                    master.WriteSingleCoil(slaveAddress, startAddress, true);
                    break;
                }
            }
            positionState = 2;
            return;
        }

    }
   
}
