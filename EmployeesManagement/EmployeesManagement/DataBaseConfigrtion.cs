using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace EmployeesManagement
{
    public partial class DataBaseConfigrtion : Form
    {
        public DataBaseConfigrtion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("ConData/ConData.xml");

            // العثور على العنصر 'data'
            XmlNode dataNode = xmlDoc.SelectSingleNode("/root/server_name");
            XmlNode database = xmlDoc.SelectSingleNode("/root/database_name");
            XmlNode userid = xmlDoc.SelectSingleNode("/root/user_id");
            XmlNode pas = xmlDoc.SelectSingleNode("/root/password");
            XmlNode Trusted = xmlDoc.SelectSingleNode("/root/Trusted_Connection");
            if (dataNode != null)
            {
                // تحديث القيمة
                dataNode.InnerText = textBox1.Text;

                database.InnerText= DB.Text;
                userid.InnerText= textBox3.Text;
                pas.InnerText=textBox4.Text;
                if (checkBox1.Checked)
                {

                    Trusted.InnerText="true";
                }
                else
                {
                    Trusted.InnerText="false";
                }


                xmlDoc.Save("ConData/ConData.xml");

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("ConData/ColConData.xml");

            // العثور على العنصر 'data'
            XmlNode dataNode = xmlDoc.SelectSingleNode("/root/server_name");
            XmlNode database = xmlDoc.SelectSingleNode("/root/database_name");
            XmlNode userid = xmlDoc.SelectSingleNode("/root/user_id");
            XmlNode pas = xmlDoc.SelectSingleNode("/root/password");
            XmlNode Trusted = xmlDoc.SelectSingleNode("/root/Trusted_Connection");
            if (dataNode != null)
            {
                // تحديث القيمة
                dataNode.InnerText = ColServerName.Text;

                database.InnerText= ColDB.Text;
                userid.InnerText= ColUser.Text;
                pas.InnerText=ColPas.Text;
                if (ColTC.Checked)
                {

                    Trusted.InnerText="true";
                }
                else
                {
                    Trusted.InnerText="false";
                }


                xmlDoc.Save("ConData/ColConData.xml");

            }
        }

        private void DataBaseConfigrtion_Load(object sender, EventArgs e)
        {
            fillToLoginInput();
            fillToColInput();
            fillToMInput();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("ConData/MainPawerCon.xml");

            // العثور على العنصر 'data'
            XmlNode dataNode = xmlDoc.SelectSingleNode("/root/server_name");
            XmlNode database = xmlDoc.SelectSingleNode("/root/database_name");
            XmlNode userid = xmlDoc.SelectSingleNode("/root/user_id");
            XmlNode pas = xmlDoc.SelectSingleNode("/root/password");
            XmlNode Trusted = xmlDoc.SelectSingleNode("/root/Trusted_Connection");
            if (dataNode != null)
            {
                // تحديث القيمة
                dataNode.InnerText = MServerName.Text;

                database.InnerText= MDB.Text;
                userid.InnerText= MUser.Text;
                pas.InnerText=MPas.Text;
                if (MTC.Checked)
                {

                    Trusted.InnerText="true";
                }
                else
                {
                    Trusted.InnerText="false";
                }


                xmlDoc.Save("ConData/MainPawerCon.xml");
                try
                {
                    SqlConnection con = new SqlConnection($"Server={MServerName.Text};Database={MDB.Text};User id={MUser.Text};Password={MPas.Text};Trusted_Connection= {Trusted.InnerText}");
                    con.Open();
                    MessageBox.Show(con.State.ToString());
                    con.Close();
                }
                catch (Exception ex) { 
                
                MessageBox.Show(ex.ToString());
                }
                
                }
        }
        private void fillToMInput()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("ConData/MainPawerCon.xml");

            // العثور على العنصر 'data'
            XmlNode dataNode = xmlDoc.SelectSingleNode("/root/server_name");
            XmlNode database = xmlDoc.SelectSingleNode("/root/database_name");
            XmlNode userid = xmlDoc.SelectSingleNode("/root/user_id");
            XmlNode pas = xmlDoc.SelectSingleNode("/root/password");
            XmlNode Trusted = xmlDoc.SelectSingleNode("/root/Trusted_Connection");
            if (dataNode != null)
            {
                // تحديث القيمة
                MServerName.Text= dataNode.InnerText;

                MDB.Text= database.InnerText;
                MUser.Text = userid.InnerText;
                MPas.Text = pas.InnerText;
                if (Trusted.InnerText=="true" )
                {
                    MTC.Checked=true;
                    
                }
                else
                {
                    MTC.Checked=false;
                }


                xmlDoc.Save("ConData/MainPawerCon.xml");

            }
        }
       
        private void fillToColInput()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("ConData/ColConData.xml");

            // العثور على العنصر 'data'
            XmlNode dataNode = xmlDoc.SelectSingleNode("/root/server_name");
            XmlNode database = xmlDoc.SelectSingleNode("/root/database_name");
            XmlNode userid = xmlDoc.SelectSingleNode("/root/user_id");
            XmlNode pas = xmlDoc.SelectSingleNode("/root/password");
            XmlNode Trusted = xmlDoc.SelectSingleNode("/root/Trusted_Connection");
            if (dataNode != null)
            {
                // تحديث القيمة
                ColServerName.Text=dataNode.InnerText;

                ColDB.Text= database.InnerText;
                ColUser.Text= userid.InnerText;
                ColPas.Text= pas.InnerText;
                if (Trusted.InnerText=="true")
                {

                    ColTC.Checked=true;
                }
                else
                {
                    ColTC.Checked=false;
                }

            }

        }


        private void fillToLoginInput()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("ConData/ConData.xml");

            // العثور على العنصر 'data'
            XmlNode dataNode = xmlDoc.SelectSingleNode("/root/server_name");
            XmlNode database = xmlDoc.SelectSingleNode("/root/database_name");
            XmlNode userid = xmlDoc.SelectSingleNode("/root/user_id");
            XmlNode pas = xmlDoc.SelectSingleNode("/root/password");
            XmlNode Trusted = xmlDoc.SelectSingleNode("/root/Trusted_Connection");
            if (dataNode != null)
            {
                // تحديث القيمة
                textBox1.Text=dataNode.InnerText;

                DB.Text= database.InnerText;
                 textBox3.Text=userid.InnerText;
               textBox4.Text= pas.InnerText;
                checkBox1.Checked = Trusted.InnerText=="true";

            }
        }
        }
    }
