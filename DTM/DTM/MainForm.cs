using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
using System.IO;
using HZH_Controls.Controls;

namespace DTM
{
    public partial class MainForm : Skin_Color
    {
        string parentPath = "";
        DataTable dt = new DataTable();
        int q = 0;//定义合格产品数量
        int uq = 0;//定义不合格产品数数量
        

        public MainForm()
        {
            InitializeComponent();
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
    }
}
