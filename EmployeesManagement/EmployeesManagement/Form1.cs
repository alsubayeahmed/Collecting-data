using Employeesinquiry;
using EmployeesManagement.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Xml.Linq;
using System.Xml;


namespace EmployeesManagement
    {
        public partial class LoginForm : Form
        {
            public LoginForm()
            {
                InitializeComponent();
                this.KeyPreview = true; // التأكد من أن النموذج يمكنه استقبال الأحداث الخاصة بلوحة المفاتيح
                this.KeyDown += new KeyEventHandler(LoginForm_KeyDown);
            }

        public static string servername = ".";
        public static string database_name = "";
        public static string user_id = "";
        public static string passwoed = "";
        public static string Trusted_Connection = ".";


        private void LoginForm_Load(object sender, EventArgs e)
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





        public static string IdUser;

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e); // استدعاء دالة الضغط على زر تسجيل الدخول
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           // string connectionString = "Data Source=DESKTOP-KKP05IN\\SQLEXPRESS01;Initial Catalog=Administratordb;Integrated Security=True;";

            string username = textUserName.Text.Trim();
            string password = textPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("الرجاء إدخال اسم المستخدم وكلمة المرور.", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = "SELECT Role,Id FROM [Log in Table]  WHERE UserName = @Username AND Password = @Password";
                DataTable dt = new DataTable();

                // استخدام SqlDataAdapter بدلاً من ExecuteScalar
                using (SqlDataAdapter da = new SqlDataAdapter(query, $"Server={servername};Database={database_name};User id={user_id};Password={passwoed};Trusted_Connection= {Trusted_Connection};"))
                {
                    da.SelectCommand.Parameters.AddWithValue("@Username", username);
                    da.SelectCommand.Parameters.AddWithValue("@Password", password);
                    da.Fill(dt); // تحميل البيانات إلى DataTable
                }

                if (dt.Rows.Count > 0) // التحقق مما إذا كان هناك بيانات
                {
                    string Role = dt.Rows[0]["Role"].ToString();
                    IdUser = dt.Rows[0]["Id"].ToString();
                    // التوجيه بناءً على الدور
                    Form targetForm = null;
                    switch (Role)
                    {
                        case "Owner":
                        case "Admin":
                            targetForm = new Input { LoggedInUserName = username };
                            break;

                        case "User":
                            targetForm = new inquiry { LoggedInUserName = username };
                            break;

                        case "Inquirer":
                            targetForm = new Search { LoggedInUserName = username };
                            break;

                        default:
                            MessageBox.Show("لم يتم التعرف على دورك!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }

                    // عرض النموذج المخُصص بناءً على الدور
                    this.Hide();
                    targetForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("اسم المستخدم أو كلمة المرور غير صالحة.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"حدث خطأ أثناء تسجيل الدخول: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            textPassword.UseSystemPasswordChar = !checkBox1.Checked;


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل أنت متأكد؟", "Exit message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //MessageBox.Show($"Server={servername};Database={database_name};User id={user_id};Password={passwoed};Trusted_Connection= {Trusted_Connection};");
            
            //SqlConnection con= new SqlConnection($"Server={servername};Database={database_name};User id={user_id};Password={passwoed};Trusted_Connection= {Trusted_Connection};");
            //con.Open();
            //MessageBox.Show(con.State.ToString());
            //con.Close();
            //return;

            if (MessageBox.Show("هل تريد حقًا مسح الحقول؟", "Confirmation message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                textUserName.Clear();
                textPassword.Clear();
                checkBox1.Checked = false; // إعادة CheckBox إلى حالته الافتراضية
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

     

        private void LoginLable_Click(object sender, EventArgs e)
        {

        }

        private void textUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Log_in_to_SQLConfigrtion log_In_To_SQLConfigrtion=new Log_in_to_SQLConfigrtion();
            log_In_To_SQLConfigrtion.ShowDialog();
        }
    }
}
