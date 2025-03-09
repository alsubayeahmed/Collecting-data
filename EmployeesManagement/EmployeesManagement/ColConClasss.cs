using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EmployeesManagement
{
    internal class ColConClasss
    {
        public ColConClasss() {
            fillToColInput();
            fillToMInput();
        }

        public static string ServerName104 = "";
        public static string DataBase104 = "";
        public static string User104 = "";
        public static string Password104 = "";
        public static string Trusted_Connection104 = "";

        public static string ServerNameCol = "";
        public static string DataBaseCol = "";
        public static string UserCol = "";
        public static string PasswordCol = "";
        public static string Trusted_ConnectionCol = "";

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
                ServerNameCol= dataNode.InnerText;

                DataBaseCol= database.InnerText;
                UserCol = userid.InnerText;
                PasswordCol = pas.InnerText;
                Trusted_ConnectionCol= Trusted.InnerText;
                

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
                ServerName104=dataNode.InnerText;

                DataBase104= database.InnerText;
                User104= userid.InnerText;
                Password104= pas.InnerText;
                Trusted_Connection104 = Trusted.InnerText;

            }

        }


    }
}
