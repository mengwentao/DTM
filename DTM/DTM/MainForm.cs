﻿using System;
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
using System.IO;
using HZH_Controls.Controls;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DTM
{
    public partial class MainForm : Skin_Color
    {
        string parentPath = "";
        DataTable dt = new DataTable();
        int q = 0;//定义合格产品数量
        int uq = 0;//定义不合格产品数数量
        public static int boxId = 0;//盒子id
        public int barCode = 0;//条形码
        public int standard_pan_thickness = 0;//盘片厚度（标准） 
        string connStr = ConfigurationManager.ConnectionStrings["str"].ConnectionString;
        public static bool flag = false;
        List<BoxState> list = new List<BoxState>();

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

            CurveChart_Init();//表格初始化
            pieChar_Init();
            barChar_Init();
            label5.Text = "欢迎：" + LoginForm.uid;           
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
            if (btn_flag)
            {
                button1.BackColor = Color.Lime;
                btn_flag = false;
            }
            else
            {
                button1.BackColor = Color.LightGray;
                btn_flag = true;
            }

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }
        private void Runsystem()
        {
            //故障后重新运行            
            using (MySqlConnection con = new MySqlConnection(connStr))
            {
                int number = 0;
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
                            number = sdr.GetInt16(0);
                        }
                    }
                }
                if (number != 0)
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
                                BoxState boxState = new BoxState(list,sdr.GetInt16("positionState"), sdr.GetInt16("barCode"), sdr.GetInt16("boxId"));
                                int testFlag = sdr.GetInt16("measureState");
                                boxState.measureState = testFlag == 0 ? false : true;
                                BoxState.boxCount = sdr.GetInt16("boxCount");
                                testFlag = sdr.GetInt16("chooseFlag");
                                boxState.chooseFlag = testFlag == 0 ? false : true;
                                boxState.standard_pan_thickness = sdr.GetInt16("standardpanthickness");
                                String teststring;
                                if (!sdr.IsDBNull(8))
                                {
                                    teststring = sdr.GetString("InList");
                                    for (int i = 0; i < teststring.Length; i++)
                                    {
                                        BoxState.InList.Add(teststring.Substring(i, 1) == "0" ? 0 : 1);
                                    }
                                }
                                teststring = sdr.GetString("measurepanthicknessflag");
                                for (int i = 0; i < teststring.Length; i++)
                                {
                                    boxState.measure_pan_thickness_flag[i] = (teststring.Substring(i, 1) == "0" ? false : true);
                                }
                                teststring = sdr.GetString("measurepanthickness");
                                string[] teststrings = teststring.Split(',');
                                for (int i = 0; i < teststring.Length; i++)
                                {
                                    boxState.measure_pan_thickness[i] = Convert.ToInt16(teststrings[i]);
                                }
                                new Thread(boxState.Run).Start();
                                Thread.Sleep(100);
                            }
                        }
                    }
                }
            }
            Thread.Sleep(100);
            new Thread(Run).Start();
            new Thread(Run1).Start();
            while (true)
            {
                if (!flag)
                {
                    Thread.Sleep(100);             
                    continue;
                }
                flag = false;
                //扫码枪扫码完成(获得条形码和标准盘片数据)
                if (BoxState.InList.Count == 0)
                {
                    boxId = 0;
                }
                else
                {
                    boxId = BoxState.InList.Last() + 1;
                }
                boxId = boxId > 50 ? 0 : boxId;//50保证了运行在系统上的盒子ID是不重复的
                new Thread(new BoxState(list,this.standard_pan_thickness, this.barCode, boxId).Run).Start(); 
            }
        }       
        private void ucBtnExt17_BtnClick(object sender, EventArgs e)
        {
            new Thread(Runsystem).Start();               
        }        
        private void Run()
        {  //向数据库保存当前所有盒子的信息
            while (true)
            {
                Thread.Sleep(100);
                for(int i = 0; i < 3; i++)
                {
                 list.Remove(list.Where(p => (p.positionState == 6 || p.positionState == 10)).FirstOrDefault());
                }              
                label6.Text = "编号：" + list.Count;                 
                //List<BoxState> list = list;
                int count = list.Count();
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
                           
                            foreach (BoxState box_state in list)
                            {
                                if (box_state.positionState == 6 || box_state.positionState == 10) continue;
                                String value = "";
                                String value1 = "";
                                String value2 = "";
                                for (int i = 0; i < box_state.measure_pan_thickness.Length - 1; i++)
                                {
                                    value += "" + box_state.measure_pan_thickness[i] + ",";
                                }
                                value += "" + box_state.measure_pan_thickness[24];
                                for (int i = 0; i < box_state.measure_pan_thickness_flag.Length; i++)
                                {
                                    value1 += "" + (box_state.measure_pan_thickness_flag[i] == false ? 0 : 1);
                                }
                                for (int i = 0; i < BoxState.InList.Count; i++)
                                {
                                    value2 += "" + BoxState.InList[i];
                                }
                                sql = string.Format("insert into preventdisaster(positionState, measureState, boxCount,chooseFlag,boxId,barCode,measurepanthickness,measurepanthicknessflag,InList,standardpanthickness)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", box_state.positionState, box_state.measureState == false ? 0 : 1, BoxState.boxCount, box_state.chooseFlag == false ? 0 : 1, box_state.boxId, box_state.barCode, value, value1, value2, box_state.standard_pan_thickness);
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
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
                Thread.Sleep(2000);
                //List<BoxState> list = BoxState.list;
                foreach (BoxState box_state in list)
                {
                    if (box_state.positionState == 1)
                    {
                        // todo得到数据填充到前端界面
                    }
                    if (box_state.positionState == 2)
                    {
                        // todo得到数据填充到前端界面
                    }
                    if (box_state.positionState == 3)
                    {
                        // todo得到数据填充到前端界面
                    }
                }              
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button27_Click(object sender, EventArgs e)
        {    //扫码枪触发事件
            flag = true;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
