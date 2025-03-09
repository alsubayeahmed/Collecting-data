using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace EmployeesManagement
{
    public partial class Administrator : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GBO380T;Initial Catalog=Administratordb;Integrated Security=True;");

        public Administrator()
        {
            InitializeComponent();
          
            BindData();
        }
      
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GBO380T;Initial Catalog=Administratordb;Integrated Security=True;");
            con.Open();
            if (string.IsNullOrEmpty(textUserName.Text) ||
                        string.IsNullOrEmpty(textPhone.Text) ||
                        string.IsNullOrEmpty(role1.Text) 
                        )
            {
                MessageBox.Show("يرجى تعبئة جميع الحقول!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                 SqlCommand command = new SqlCommand("insert into empTable values('" + textUserName.Text + "','" + role1.Text + "','" + textPhone.Text + "', '"+dob1.Text+"')", con);
           
            command.ExecuteNonQuery();
            MessageBox.Show("Successfully Inserted ");
            BindData();
            ResetFields();
            con.Close();
        }
            void BindData()
            {
                SqlCommand command1 = new SqlCommand("select * from empTable", con);
                SqlDataAdapter sa = new SqlDataAdapter(command1);
                DataTable dt = new DataTable();
                sa.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            
            private void Administrator_Load(object sender, EventArgs e)
        {
            {
                BindData();
                ResetFields();
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من الحقول الفارغة
                if (string.IsNullOrEmpty(textUserName.Text) ||
                    string.IsNullOrEmpty(textPhone.Text) ||
                    string.IsNullOrEmpty(role1.Text)

                    ) // يجب أن يكون حقل ID موجودًا في الواجهة
                {
                    MessageBox.Show("يرجى تعبئة جميع الحقول!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }




                // إنشاء الاتصال بقاعدة البيانات
                {
                   
                    if (MessageBox.Show("هل أنت متأكد من تعديل هذا الموظف؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value); // الحصول على الـ ID من الصف المحدد

                        con.Open();
                        SqlCommand cmd = new SqlCommand("update  empTable set UserName =('" + textUserName.Text + "'), Role ='" + role1.Text + "',Phone ='" + textPhone.Text + "',DOB = '"+DateTime.Parse(dob1.Text) + "' WHERE Id = @id", con);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("تم التعديل بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BindData();
                        ResetFields();
                        
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"خطأ في قاعدة البيانات: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ غير متوقع: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    
    private void ResetFields()
        {
            textUserName.Clear();
            textPhone.Clear();
            role1.ResetText();
        

            dob1.Text = DateTime.Now.ToString();
            dob.Text = DateTime.Now.ToString();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to clear the fields?", "Confirmation message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                textUserName.Clear();
                textPhone.Clear();
                role1.ResetText();
                dob1.Text= DateTime.Now.ToString();
                dob.Text = DateTime.Now.ToString();

            }
          
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM empTable WHERE  1=1"; // استعلام ديناميكي
            if (!string.IsNullOrEmpty(textUserName.Text)) query += " AND UserName LIKE @name";
            if (!string.IsNullOrEmpty(textPhone.Text)) query += " AND Phone LIKE @phone";
            if (!string.IsNullOrEmpty(role1.Text)) query += " AND Role LIKE @role1";

            SqlCommand cmd = new SqlCommand(query, con);
            if (!string.IsNullOrEmpty(textUserName.Text)) cmd.Parameters.AddWithValue("@name", $"%{textUserName.Text}%");
            if (!string.IsNullOrEmpty(textPhone.Text)) cmd.Parameters.AddWithValue("@phone", $"%{textPhone.Text}%");
            if (!string.IsNullOrEmpty(role1.Text)) cmd.Parameters.AddWithValue("@role1", $"%{role1.Text}%");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("لا توجد بيانات مطابقة للبحث", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل أنت متأكد من تسجيل الخروج؟", "تأكيد الخروج", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close(); 
            }
        }

        private void EmployeeManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("يرجى تحديد موظف للحذف", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("هل أنت متأكد من حذف هذا الموظف؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value); // الحصول على الـ ID من الصف المحدد

                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM empTable WHERE ID = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("تم الحذف بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindData(); // إعادة تحميل البيانات
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void get_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select Role,Phone,DOB from empTable WHERE UserName='"+textUserName.Text+"'", con);
            SqlDataReader srd = cmd.ExecuteReader();
            while(srd.Read())
                {
                role1.Text = srd.GetValue(0).ToString();
                textPhone.Text = srd.GetValue(1).ToString();
                dob1.Text = srd.GetValue(2).ToString();

            }
            //كررها عليهم كلهم مش اسم المستخدم بس
          
            con.Close();
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            BindData();
            ResetFields();

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void insertto_Click(object sender, EventArgs e)
        {
         this.Close();
            Form f = new Form();
            f = Input.ActiveForm;
            f.Activate();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    }
    
    

