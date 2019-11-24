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
    public partial class ConnectSetForm : Form
    {
        XmlDocument xmldoc = null;
        string xmlPath = "";
        public ConnectSetForm()
        {
            InitializeComponent();
        }

        private void ConnectSetForm_Load(object sender, EventArgs e)
        {
            connectInit();
        }
        public void connectInit()
        {
            xmlPath = Directory.GetCurrentDirectory() + "\\AppSet\\SETXMLFile.xml";
            xmldoc = new XmlDocument();
            xmldoc.Load(xmlPath);
            XmlElement xmlRoot = xmldoc.DocumentElement;
            //4、获取根结点下的子节点

            foreach (XmlNode node in xmlRoot.ChildNodes)
            {
                if (node.Name == "connectSet")
                {
                    foreach (XmlNode keys in node.ChildNodes)
                    {
                        if (keys.Name == "PLC_1")
                        {
                            textBox1.Text = keys["ipaddress"].InnerText;
                            textBox2.Text = keys["port"].InnerText;
                        }
                        else if (keys.Name == "PLC_2")
                        {
                            textBox4.Text = keys["ipaddress"].InnerText;
                            textBox3.Text = keys["port"].InnerText;
                        }
                    }
                }
            }
        }
        public void saveConnectXml()
        {
            xmlPath = Directory.GetCurrentDirectory() + "\\AppSet\\SETXMLFile.xml";
            //xmldoc = new XmlDocument();
            xmldoc.Load(xmlPath);
            XmlElement xmlRoot = xmldoc.DocumentElement;
            //4、获取根结点下的子节点

            foreach (XmlNode node in xmlRoot.ChildNodes)
            {
                if (node.Name == "connectSet")
                {
                    foreach (XmlNode keys in node.ChildNodes)
                    {
                        if (keys.Name == "PLC_1")
                        {
                            keys["ipaddress"].InnerText = textBox1.Text;
                            keys["port"].InnerText = textBox2.Text;
                        }
                        else if (keys.Name == "PLC_2")
                        {
                            keys["ipaddress"].InnerText = textBox4.Text;
                            keys["port"].InnerText = textBox3.Text;
                        }
                    }
                }
            }
            xmldoc.Save(xmlPath);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            saveConnectXml();
        }
    }
}
