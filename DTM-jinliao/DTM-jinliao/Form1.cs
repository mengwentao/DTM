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
using MySql.Data.MySqlClient;
using System.Net.Http;
using System.IO;
using MySqlX.Serialization;
using Newtonsoft.Json;
//引入命名空间：
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace DTM_jinliao
{
    public partial class Form1 : Form
    {
        string basePath = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            basePath = getBasePath();
        }
        public string getBasePath()
        {
            Directory.SetCurrentDirectory(Directory.GetParent(Directory.GetCurrentDirectory()).FullName);
            Directory.SetCurrentDirectory(Directory.GetParent(Directory.GetCurrentDirectory()).FullName);
            return Directory.GetCurrentDirectory();
        }
        public void show_message(DataItem data)
        {
            label6.Text = data.RCNo;//RC编号
            label8.Text = data.PrdName;//产品型号
            label25.Text = data.MaterialCode;//供应商代码
            label26.Text = data.IDODMachineNo;//IDOD机台号
            label27.Text = data.IDODDTime;//IDOD过站时间
            label28.Text = data.SXMachineNo;//酸洗机台号
            label29.Text = data.SXDTime;//酸洗过站时间
            label30.Text = data.RGMachineNo;//粗磨机台号
            label31.Text = data.RGDTime;//粗磨过站时间
            label32.Text = data.CXMachineNo;//粗细机台号
            label33.Text = data.CXDTime;//粗细过站时间
            label34.Text = data.THMachineNo;//退火机台号
            label35.Text = data.THDTime;//退火时间
            label36.Text = data.THEquipment1;//退火工装1
            label37.Text = data.THEquipment2;//退火工装2
            label38.Text = data.FGMachineNo;//精磨机台号
            label39.Text = data.FGDTime;//精磨过站时间
        }
        /// <summary>
        /// //为了通过证书验证，总是返回true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cert"></param>
        /// <param name="chain"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }
      
        private void label1_Click(object sender, EventArgs e)
        {
            string RCname = this.label1.Text;
            this.label1.ForeColor = Color.Blue;
            string testUrl = basePath + "\\testJsonTxt\\" + RCname + ".txt";//拼接路径
            //string res = File.ReadAllText(testUrl);//通过读取txt,模拟获取到json数据

            //以下内容为通过webapi获取数据            
            string url = "https://www.easy-mock.com/mock/5ddfd4937605f1121decf358/test1/test#!method=get"; 
           
             GetData getJson = new GetData();
             ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
            try
            {
                string res = getJson.HttpCodeCreate(url);//通过webappi获取到json数据
                 //以下内容为序列化，解析json数据
                JSONObject jp = (JSONObject)JsonConvert.DeserializeObject<JSONObject>(res);//序列化
                if (jp.Success == "true")
                {
                    this.label1.ForeColor = Color.Blue;
                    List<DataItem> list = jp.Data;
                    list = jp.Data;
                    DataItem data = list[0];
                    // MessageBox.Show(data.RCNo);
                    show_message(data);
                    Console.WriteLine(res);
                }
                else
                {
                    MessageBox.Show("错误编号:" + jp.Error);
                }
            }
            catch
            {
                MessageBox.Show("查询异常，请重试");
            }
            

        }
        private void label2_Click(object sender, EventArgs e)
        {
            string RCname = this.label2.Text;          
            string testUrl = basePath + "\\testJsonTxt\\" + RCname + ".txt";//拼接路径
            string res = File.ReadAllText(testUrl);//通过读取txt,模拟获取到json数据

            //以下内容为通过webapi获取数据
            // string url = "https://www.easy-mock.com/mock/5ddfd4937605f1121decf358/test1/test#!method=get";
            /* string url = "http://106.12.3.103:8080/MyProject/user/MyTest.do";
             GetData getJson = new GetData();
             ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
             string res = getJson.HttpCodeCreate(url);//获取到json数据*/

            //以下内容为序列化，解析json数据
            JSONObject jp = (JSONObject)JsonConvert.DeserializeObject<JSONObject>(res);//序列化
            if (jp.Success == "true")
            {
                this.label2.ForeColor = Color.Blue;
                List<DataItem> list = jp.Data;
                list = jp.Data;
                DataItem data = list[0];
                // MessageBox.Show(data.RCNo);
                show_message(data);
                Console.WriteLine(res);
            }
            else
            {
                MessageBox.Show("错误编号:" + jp.Error);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            string RCname = this.label3.Text;          
            string testUrl = basePath + "\\testJsonTxt\\" + RCname + ".txt";//拼接路径
            string res = File.ReadAllText(testUrl);//通过读取txt,模拟获取到json数据

            //以下内容为通过webapi获取数据
            // string url = "https://www.easy-mock.com/mock/5ddfd4937605f1121decf358/test1/test#!method=get";
            /* string url = "http://106.12.3.103:8080/MyProject/user/MyTest.do";
             GetData getJson = new GetData();
             ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
             string res = getJson.HttpCodeCreate(url);//获取到json数据*/

            //以下内容为序列化，解析json数据
            JSONObject jp = (JSONObject)JsonConvert.DeserializeObject<JSONObject>(res);//序列化
            if (jp.Success == "true")
            {
                this.label3.ForeColor = Color.Blue;
                List<DataItem> list = jp.Data;
                list = jp.Data;
                DataItem data = list[0];
                // MessageBox.Show(data.RCNo);
                show_message(data);
                Console.WriteLine(res);
            }
            else
            {
                MessageBox.Show("错误编号:" + jp.Error);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            string RCname = this.label4.Text;        
            string testUrl = basePath + "\\testJsonTxt\\" + RCname + ".txt";//拼接路径
            string res = File.ReadAllText(testUrl);//通过读取txt,模拟获取到json数据

            //以下内容为通过webapi获取数据
            // string url = "https://www.easy-mock.com/mock/5ddfd4937605f1121decf358/test1/test#!method=get";
            /* string url = "http://106.12.3.103:8080/MyProject/user/MyTest.do";
             GetData getJson = new GetData();
             ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
             string res = getJson.HttpCodeCreate(url);//获取到json数据*/

            //以下内容为序列化，解析json数据
            JSONObject jp = (JSONObject)JsonConvert.DeserializeObject<JSONObject>(res);//序列化
            if (jp.Success == "true")
            {
                this.label4.ForeColor = Color.Blue;
                List<DataItem> list = jp.Data;
                list = jp.Data;
                DataItem data = list[0];
                // MessageBox.Show(data.RCNo);
                show_message(data);
                Console.WriteLine(res);
            }
            else
            {
                MessageBox.Show("错误编号:" + jp.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            get_messageBywebapi("RC02848");
        }
        public void get_messageBywebapi(string RCname)
        {
           
            string testUrl = basePath + "\\testJsonTxt\\" + RCname + ".txt";//拼接路径
            string res = File.ReadAllText(testUrl);//通过读取txt,模拟获取到json数据

            //以下内容为通过webapi获取数据
            // string url = "https://www.easy-mock.com/mock/5ddfd4937605f1121decf358/test1/test#!method=get";
            /* string url = "http://106.12.3.103:8080/MyProject/user/MyTest.do";
             GetData getJson = new GetData();
             ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
             string res = getJson.HttpCodeCreate(url);//获取到json数据*/

            //以下内容为序列化，解析json数据
            JSONObject jp = (JSONObject)JsonConvert.DeserializeObject<JSONObject>(res);//序列化
            if (jp.Success == "true")
            {
                this.label4.ForeColor = Color.Blue;
                List<DataItem> list = jp.Data;
                list = jp.Data;
                DataItem data = list[0];
                // MessageBox.Show(data.RCNo);
                show_message(data);
                Console.WriteLine(res);
            }
            else
            {
                MessageBox.Show("错误编号:" + jp.Error);
            }
        }
        
    }
}
