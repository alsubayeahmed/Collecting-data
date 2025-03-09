using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace EmployeesManagement
{
    internal class Class1
    {
        public static string servername = ".";
        public static string database_name = "";
        public static string user_id = "";
        public static string passwoed = "";
        public static string Trusted_Connection = ".";
        public Class1()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("ConData/ConData.xml");
            XmlNode dataNode = xmlDoc.SelectSingleNode("/root/server_name");
            XmlNode database = xmlDoc.SelectSingleNode("/root/database_name");
            XmlNode userid = xmlDoc.SelectSingleNode("/root/user_id");
            XmlNode pas = xmlDoc.SelectSingleNode("/root/password");
            XmlNode Trusted = xmlDoc.SelectSingleNode("/root/Trusted_Connection");
            if (dataNode != null)
            {
                // استخراج القيمة ووضعها في متغير نصي
                servername= dataNode.InnerText;
                database_name = database.InnerText;
                user_id = userid.InnerText;
                passwoed = pas.InnerText;
                Trusted_Connection = Trusted.InnerText;
                // عرض القيمة
                //   MessageBox.Show("T" + servername);
            }
            else
            {
                MessageBox.Show("اعد تشغيل البرنامج");
            }

        }

    }
}
