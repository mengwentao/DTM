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
   
    public partial class MeasureSetForm : Form
    {
        XmlDocument xmldoc = null;
        string xmlPath = "";
        public MeasureSetForm()
        {
            InitializeComponent();
        }
        public void measureSetInit()
        {
            xmlPath = Directory.GetCurrentDirectory() + "\\AppSet\\SETXMLFile.xml";
            xmldoc = new XmlDocument();
            xmldoc.Load(xmlPath);
            XmlElement xmlRoot = xmldoc.DocumentElement;
            //4、获取根结点下的子节点

            foreach (XmlNode node in xmlRoot.ChildNodes)//<setItem>
            {
                if (node.Name == "measureSet")
                {
                    textBox1.Text = node["referenceValue"].InnerText;
                    textBox2.Text = node["deviationValue"].InnerText;

                }
            }
        }
        public void measureSetSave()
        {
            xmlPath = Directory.GetCurrentDirectory() + "\\AppSet\\SETXMLFile.xml";
            xmldoc = new XmlDocument();
            xmldoc.Load(xmlPath);
            XmlElement xmlRoot = xmldoc.DocumentElement;
            //4、获取根结点下的子节点

            foreach (XmlNode node in xmlRoot.ChildNodes)//<setItem>
            {
                if (node.Name == "measureSet")
                {
                     node["referenceValue"].InnerText = textBox1.Text;
                     node["deviationValue"].InnerText = textBox2.Text ;
                }
            }
            xmldoc.Save(xmlPath);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            measureSetSave();
        }

        private void MeasureSetForm_Load(object sender, EventArgs e)
        {
            measureSetInit();
        }
    }
}
