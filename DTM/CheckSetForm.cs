using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DTM
{
    public partial class CheckSetForm : Form
    {
        XmlDocument xmldoc = null;
        string xmlPath = "";
        public CheckSetForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveCheckSetXml();
        }
        public void CheckSetInit()
        {
            xmlPath = Directory.GetCurrentDirectory() + "\\AppSet\\SETXMLFile.xml";
            xmldoc = new XmlDocument();
            xmldoc.Load(xmlPath);
            XmlElement xmlRoot = xmldoc.DocumentElement;
            //4、获取根结点下的子节点

            foreach (XmlNode node in xmlRoot.ChildNodes)//<setItem>
            {
                if (node.Name == "checkSet")
                {
                    textBox1.Text = node["timeInterval"].InnerText;

                }
            }
        }
        public void saveCheckSetXml()
        {
            xmlPath = Directory.GetCurrentDirectory() + "\\AppSet\\SETXMLFile.xml";
            xmldoc = new XmlDocument();
            xmldoc.Load(xmlPath);
            XmlElement xmlRoot = xmldoc.DocumentElement;
            //4、获取根结点下的子节点

            foreach (XmlNode node in xmlRoot.ChildNodes)//<setItem>
            {
                if (node.Name == "checkSet")
                {
                    node["timeInterval"].InnerText = textBox1.Text;//抽检间隔设置
                }
            }
            xmldoc.Save(xmlPath);
        }

        private void CheckSetForm_Load(object sender, EventArgs e)
        {
            CheckSetInit();
        }
    }
}
