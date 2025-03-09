using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EmployeesManagement
{
    public partial class Search : Form
    {
        private string username;
        public Search(string loggedInUserName)
        {
            InitializeComponent();
            username = loggedInUserName;

        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GBO380T;Initial Catalog=DIC2025;Integrated Security=True;");
        SqlConnection coon = new SqlConnection(@"Data Source=DESKTOP-GBO380T;Initial Catalog=DWConfiguration;Integrated Security=True;");
        public string LoggedInUserName { get; set; }

        public Search()
        {
            InitializeComponent();
            ResetFields();
            ResetFields2();
           // BindData();
           // BindData2();
        }
       void BindData()
        {
            SqlCommand command1 = new SqlCommand("SELECT  nId, cus_name, mo_name, ministry,  old_work,  work_sub, region, hala, n_hala,  makteb, OffNum,  def, Dofa,Deficiencies  FROM iiinsertion", con);


            SqlDataAdapter sa = new SqlDataAdapter(command1);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void BindData2()
        {
            SqlCommand command2 = new SqlCommand("SELECT  text_id, empname, mo_name, ministry, work_dir, work_sub, Region, edited, ekres, makteb,dofa    FROM [373]", coon);


            SqlDataAdapter sa = new SqlDataAdapter(command2);
            DataTable dt = new DataTable();
            sa.Fill(dt);
            dataGridView2.DataSource = dt;
        }
        private void ResetFields()
        {

            textEid.Clear();

        }

        private void ResetFields2()
        {
            textBox1.Clear();
            textBox2.Clear();

        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*   DataTable emptyTable = new DataTable();
               dataGridView2.DataSource = emptyTable;*/
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد حقًا مسح الحقول؟?", "رسالة التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                textEid.Clear();

            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من أن الحقل ليس فارغًا
                if (string.IsNullOrWhiteSpace(textEid.Text))
                {
                    MessageBox.Show("يرجى إدخال رقم الهوية للبحث.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // إيقاف التنفيذ إذا كان الحقل فارغًا
                }

                // استعلام البحث بناءً على إدخال المستخدم
                string query = "SELECT  nId, cus_name, mo_name, ministry, old_work, work_sub, region, hala, n_hala, makteb, OffNum, def, Dofa, Deficiencies " +
                               "FROM iiinsertion WHERE nId = @nId";

                using (SqlDataAdapter da = new SqlDataAdapter(query, "Data Source=DESKTOP-GBO380T;Initial Catalog=DIC2025;Integrated Security=True;"))
                {
                    da.SelectCommand.Parameters.AddWithValue("@nId", textEid.Text); // استخدام الرقم الوطني المدخل

                    DataTable dataTable = new DataTable();
                    da.Fill(dataTable);

                    // تحديث البيانات المعروضة في DataGridView
                    if (dataTable.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("لا توجد بيانات مطابقة للبحث", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.DataSource = null; // إذا كانت لا توجد بيانات، تأكد من مسح أي بيانات سابقة في الـ DataGridView
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء البحث: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            /*  try
              {
                  con.Open();

                  // استعلام البحث الأساسي
                  string query = "SELECT CID, nId, cus_name, mo_name, ministry,  old_work,  work_sub, region, hala, n_hala,  makteb, OffNum,  def, Dofa,Deficiencies FROM iiinsertion WHERE 1=1"; // استخدم 1=1 لتمكين إضافة شروط البحث الديناميكية

                  // إضافة الشروط بناءً على الحقول المعبأة (باستثناء textdate)
                  if (!string.IsNullOrEmpty(textname.Text)) query += " AND cus_name LIKE @cus_name";
                  if (!string.IsNullOrEmpty(textEid.Text)) query += " AND nId LIKE @nId";

                  // إعداد الكود والقيام بإضافة المعلمات
                  SqlCommand cmd = new SqlCommand(query, con);
                  if (!string.IsNullOrEmpty(textname.Text)) cmd.Parameters.AddWithValue("@cus_name", $"%{textname.Text}%");
                  if (!string.IsNullOrEmpty(textEid.Text)) cmd.Parameters.AddWithValue("@nId", $"%{textEid.Text}%");

                  // تنفيذ الاستعلام وجلب البيانات
                  SqlDataAdapter da = new SqlDataAdapter(cmd);
                  DataTable dt = new DataTable();
                  da.Fill(dt);
                 // dataGridView2.DataSource = dt;
                  // عرض البيانات في DataGridView
                  if (dt.Rows.Count > 0)
                  {
                      dataGridView1.DataSource = dt;
                  }
                  else
                  {
                      MessageBox.Show("لا توجد بيانات مطابقة للبحث", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  }
              }
              catch (Exception ex)
              {
                  MessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
              }*/
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد حقًا مسح الحقول؟?", "رسالة التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                textBox1.Clear();
                textBox2.Clear();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من أن أحد الحقول قد تمت تعبئته
                if (string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("يرجى تعبئة حقل واحد على الأقل للبحث.", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // إيقاف عملية البحث إذا كانت الحقول فارغة
                }

                // استعلام البحث بناءً على إدخال المستخدم
                string query = "SELECT text_id, empname, mo_name, ministry, work_dir, work_sub, Region, edited, ekres, makteb, dofa FROM [373] WHERE 1=1";

                // إضافة الشروط بناءً على الإدخالات
                if (!string.IsNullOrEmpty(textBox1.Text)) query += " AND text_id LIKE @text_id";
                if (!string.IsNullOrEmpty(textBox2.Text)) query += " AND dofa LIKE @dofa";

                using (SqlDataAdapter da = new SqlDataAdapter(query, "Data Source=DESKTOP-GBO380T;Initial Catalog=DWConfiguration;Integrated Security=True;"))
                {
                    // إضافة المعلمات
                    da.SelectCommand.Parameters.AddWithValue("@text_id", $"%{textBox1.Text}%");
                    da.SelectCommand.Parameters.AddWithValue("@dofa", $"%{textBox2.Text}%");

                    DataTable dataTable = new DataTable();
                    da.Fill(dataTable);

                    // تحديث البيانات في DataGridView
                    if (dataTable.Rows.Count > 0)
                    {
                        dataGridView2.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("لا توجد بيانات مطابقة للبحث.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء البحث: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            /* try
             {
                 coon.Open();

                 // استعلام البحث الأساسي
                 string query = "SELECT text_id, empname, mo_name, ministry, work_dir, work_sub, Region, edited, ekres, makteb,dofa FROM [373] WHERE 1=1"; // استخدم 1=1 لتمكين إضافة شروط البحث الديناميكية

                 // إضافة الشروط بناءً على الحقول المعبأة
                 if (!string.IsNullOrEmpty(textBox1.Text)) query += " AND text_id LIKE @text_id";
                 if (!string.IsNullOrEmpty(textBox2.Text)) query += " AND dofa LIKE @dofa";

                 // إعداد الكود والقيام بإضافة المعلمات
                 SqlCommand cmd = new SqlCommand(query, coon);
                 if (!string.IsNullOrEmpty(textBox1.Text)) cmd.Parameters.AddWithValue("@text_id", $"%{textBox1.Text}%");
                 if (!string.IsNullOrEmpty(textBox2.Text)) cmd.Parameters.AddWithValue("@dofa", $"%{textBox2.Text}%");

                 // تنفيذ الاستعلام وجلب البيانات
                 SqlDataAdapter da = new SqlDataAdapter(cmd);
                 DataTable dt = new DataTable();
                 da.Fill(dt);

                 // عرض البيانات في DataGridView
                 if (dt.Rows.Count > 0)
                 {
                     dataGridView2.DataSource = dt;
                 }
                 else
                 {
                     MessageBox.Show("لا توجد بيانات مطابقة للبحث", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
             finally
             {
                 coon.Close(); // إغلاق الاتصال بقاعدة البيانات
             }*/

        }


        private DataTable dataTable;
        private DataTable dataTable2;
        private void Search_Load(object sender, EventArgs e)
        {
            lblUserName.Text = LoggedInUserName;
            lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            try
            {
                // الاتصال الأول لجلب البيانات
                SqlDataAdapter da1 = new SqlDataAdapter("SELECT top 0  nId, cus_name, mo_name, ministry, old_work, work_sub, region, hala, n_hala, makteb, OffNum, def, Dofa, Deficiencies FROM iiinsertion",
                                                        "Data Source=DESKTOP-GBO380T;Initial Catalog=DIC2025;Integrated Security=True;");
                DataTable dataTable1 = new DataTable();
                da1.Fill(dataTable1);

                // ربط DataTable بـ DataGridView
                dataGridView1.DataSource = dataTable1;
                // منع إضافة صفوف جديدة يدويًا
                dataGridView1.AllowUserToAddRows = false;

                // الاتصال الثاني لجلب البيانات
                SqlDataAdapter da2 = new SqlDataAdapter("SELECT top 0  text_id, empname, mo_name, ministry, work_dir, work_sub, Region, edited, ekres, makteb, dofa FROM [373]",
                                                        "Data Source=DESKTOP-GBO380T;Initial Catalog=DWConfiguration;Integrated Security=True;");
                DataTable dataTable2 = new DataTable();
                da2.Fill(dataTable2);

                // تعيين أسماء الأعمدة فقط إلى DataGridView
                dataGridView2.DataSource = dataTable2;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}


