using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace EmployeesManagement
{
    public partial class Input : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GBO380T;Initial Catalog=DIC2025;Integrated Security=True;");
        bool EmpContorls =true;
        public string LoggedInUserName { get; set; }
        public Input()
        {

            InitializeComponent();

            BindData();

        }
        void BindData()
        {
            string connectionString = @"Data Source=DESKTOP-GBO380T;Initial Catalog=DIC2025;Integrated Security=True;";

            try
            {
               

                string query = @"
        SELECT * 
        FROM iiinsertion 
        WHERE Confirm_User = @Confirm_User 
        AND CAST(Confirm_Date AS DATE) = CAST(GETDATE() AS DATE)";

                using (SqlDataAdapter da = new SqlDataAdapter(query, connectionString))
                {
                    da.SelectCommand.Parameters.AddWithValue("@Confirm_User", textConUser.Text); // اسم المستخدم المؤكد

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // عرض البيانات في DataGridView
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
                    }
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            List<string> authorizedUsers = new List<string> { "Admin", "Manager", "Supervisor" };

            // تحقق من اسم المستخدم المدخل في واجهة تسجيل الدخول
            if (!authorizedUsers.Contains(LoggedInUserName))
            {
                MessageBox.Show("ليس لديك صلاحية لإستعمال هذا الزر.", "صلاحية مرفوضة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Administrator into = new Administrator();
            into.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Com_Click(object sender, EventArgs e)
        {

        }

        private void textMn_TextChanged(object sender, EventArgs e)
        {

        }
        List<string> restrictedUsersForFields = new List<string> { "Ahm", "User2" }; // قائمة المستخدمين الممنوعين
        private void textEid_KeyDown(object sender, KeyEventArgs e)
        {
            if (restrictedUsersForFields.Contains(LoggedInUserName))
            {
                e.SuppressKeyPress = true; // منع الكتابة
                MessageBox.Show("ليس لديك صلاحية التعديل على هذا الحقل!", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        List<string> restrictedUsersForEvents = new List<string> { "Ahd", "User4" };
        private void comboAs_Click(object sender, EventArgs e)
        {
            if (restrictedUsersForEvents.Contains(LoggedInUserName))
            {
                MessageBox.Show("ليس لديك صلاحية التفاعل مع هذا الحقل!", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // منع الحدث
            }
        }

        private void comboDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (restrictedUsersForFields.Contains(LoggedInUserName))
            {
                e.SuppressKeyPress = true; // منع الكتابة
                MessageBox.Show("ليس لديك صلاحية التعديل على هذا الحقل!", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void PreventSelection(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection(); // إزالة أي تحديد
        }
        private void PreventCopy(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                e.SuppressKeyPress = true; // منع النسخ
            }
        }

        List<string> restrictedUsers = new List<string> { "Aed", "User2", "User3" };


        
        private void Form3_Load(object sender, EventArgs e)
        {
            
            ShowName.Text = LoggedInUserName;
            ShowTimeNow.Text = DateTime.Now.ToString("dd/MM/yyyy");



            /*  DataTable emptyTable = new DataTable();
            dataGridView1.DataSource = emptyTable;*/
            textConUser.Text = LoggedInUserName;

            LoadSuggestions(comboWcom, "jeha_name", "jeha_name$");
            LoadSuggestions(comboCom, "wazara_name", "Ministry$");
            LoadSuggestions(comboPlace, "OffName", "OffName$");
            LoadSuggestions(comboAs, "hala", "HALA$");
            LoadSuggestions(comboAT, "Hala", "ProcedureState$");
            LoadSuggestions(comboOn, "OffName", "OffName$");
            // كرر لكل ComboBox


            if (restrictedUsers.Contains(LoggedInUserName))
            {
                // منع التحديد
                dataGridView1.SelectionChanged += PreventSelection;
                dataGridView1.ContextMenuStrip = new ContextMenuStrip(); // تعطيل القائمة السياقية
                dataGridView1.KeyDown += PreventCopy;
            }
            Dictionary<Button, List<string>> buttonPermissions = new Dictionary<Button, List<string>>
    {
        { btnUpdate, new List<string> { "Admin", "Ahmed", "Supervisor" } },
        { btnDelete, new List<string> { "Admin", "Manager" } },
        { btnInsert, new List<string> { "Admin", "A" } },
        { Get, new List<string> { "Admin", "Ahmed" } }
    };

            // التحقق من صلاحيات المستخدم لكل زر
            foreach (var entry in buttonPermissions)
            {
                Button button = entry.Key;
                List<string> authorizedUsers = entry.Value;

                // إظهار أو إخفاء الزر بناءً على صلاحيات المستخدم
                button.Visible = authorizedUsers.Contains(LoggedInUserName);
            }

            // عرض اسم المستخدم في ComboBox
            if (!string.IsNullOrEmpty(LoggedInUserName))
            {
                comboEn.Items.Clear();
                comboEn.Items.Add(LoggedInUserName);
                comboEn.SelectedIndex = 0;
            }


            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GBO380T;Initial Catalog=DIC2025;Integrated Security=True;");

            string sql = "Select * From HALA$";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboAs.DataSource = dt;
            comboAs.DisplayMember = "hala";





            sql = "Select * From  jehaa_name$";
            da = new SqlDataAdapter(sql, con);
            dt = new DataTable();
            da.Fill(dt);
            comboWcom.DataSource = dt;
            comboWcom.DisplayMember = "jeha_name";
            comboWcom.ValueMember = "no_work_dir1";




            sql = "Select * From  Ministry$";
            da = new SqlDataAdapter(sql, con);
            dt = new DataTable();
            da.Fill(dt);
            comboCom.DataSource = dt;
            comboCom.DisplayMember = "wazara_name";
            comboCom.ValueMember = "no_wazara";


            // ✅ استرداد القيم المرجعية من الجداول الأخرى
            //string noWorkDir = GetSingleValueFromDB(connectionString,
            //    "SELECT no_work_dir FROM jehaa_name$ WHERE jeha_name = @jeha_name", "@jeha_name", comboWcom.Text);

            //string noWazara = GetSingleValueFromDB(connectionString,
            //    "SELECT no_wazara FROM Ministry$ WHERE wazara_name = @wazara_name", "@wazara_name", comboCom.Text);

            //string offNum = GetSingleValueFromDB(connectionString,
            //    "SELECT OffNum FROM OffName$ WHERE OffName = @OffName", "@OffName", comboOn.Text);



            sql = "Select * From  OffName$";
            da = new SqlDataAdapter(sql, con);
            dt = new DataTable();
            da.Fill(dt);
            comboOn.DataSource = dt;
            comboOn.DisplayMember = "OffName";
            comboOn.ValueMember = "OffNum";

            sql = "Select * From  OffName$";
            da = new SqlDataAdapter(sql, con);
            dt = new DataTable();
            da.Fill(dt);
            comboPlace.DataSource = dt;
            comboPlace.DisplayMember = "OffName";
            comboPlace.ValueMember = "Id";

            sql = "Select * From  ProcedureState$";
            da = new SqlDataAdapter(sql, con);
            dt = new DataTable();
            da.Fill(dt);
            comboAT.DataSource = dt;
            comboAT.DisplayMember = "Hala";
            comboAT.ValueMember = "HId";
            ResetFields();
        }
        private void ResetFields()
        {

            // مسح الحقول من النوع TextBox
            textname.Clear();
            textEid.Clear();
            textMn.Clear();
            textFcom.Clear();
            textRn.Clear();
            textMnn.Clear();
            textDef.Clear();
            textDofa.Clear(); // تأكد من أنه TextBox
         //   textConTa.Clear(); // تأكد من أنه TextBox
            textDefi2.Clear(); // تأكد من أنه TextBox

            // إعادة تعيين القيم في ComboBox
            comboCom.ResetText();
            comboWcom.ResetText();
            comboPlace.ResetText();
            comboAs.ResetText();
            comboAT.ResetText();
            comboOn.ResetText();

            // إعادة تعيين DateTimePicker
            comboDate.Value = DateTime.Now;
            dateTimePicker1.Value = DateTime.Now; // إذا كان الحقل DateTimePicker

        }


        private void btnInsert_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-GBO380T;Initial Catalog=DIC2025;Integrated Security=True;";

            // إزالة الفراغات المكررة واستبدالها بفراغ واحد لكل الحقول النصية
            textname.Text = System.Text.RegularExpressions.Regex.Replace(textname.Text.Trim(), @"\s+", " ");
            textEid.Text = textEid.Text.Replace(" ", ""); // إزالة الفراغات من الرقم الوطني
            textMn.Text = System.Text.RegularExpressions.Regex.Replace(textMn.Text.Trim(), @"\s+", " ");
            textFcom.Text = System.Text.RegularExpressions.Regex.Replace(textFcom.Text.Trim(), @"\s+", " ");
            textRn.Text = textRn.Text.Replace(" ", "");
            textMnn.Text = textMnn.Text.Replace(" ", "");
            textDef.Text = System.Text.RegularExpressions.Regex.Replace(textDef.Text.Trim(), @"\s+", " ");
            textDofa.Text = textDofa.Text.Replace(" ", "");
            textConTa.Text = textConTa.Text.Replace(" ", "");
            textDefi2.Text = System.Text.RegularExpressions.Regex.Replace(textDefi2.Text.Trim(), @"\s+", " ");

            // التحقق من أن جميع الحقول النصية تحتوي على أحرف عربية فقط
            string arabicPattern = @"^[\p{IsArabic}\s]+$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(textname.Text, arabicPattern))
            {
                MessageBox.Show("الرجاء إدخال الاسم باللغة العربية فقط.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(textMn.Text, arabicPattern))
            {
                MessageBox.Show("الرجاء إدخال اسم الأم باللغة العربية فقط.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(textDef.Text, arabicPattern))
            {
                MessageBox.Show("الرجاء إدخال تفاصيل المستند باللغة العربية فقط.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(textFcom.Text, arabicPattern))
            {
                MessageBox.Show("الرجاء إدخال اسم الشركة باللغة العربية فقط.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            

            if (!System.Text.RegularExpressions.Regex.IsMatch(textDefi2.Text, arabicPattern))
            {
                MessageBox.Show("الرجاء إدخال التفاصيل الثانية باللغة العربية فقط.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ✅ التحقق من تعبئة الحقول قبل الإدخال
            if (string.IsNullOrEmpty(textname.Text) || string.IsNullOrEmpty(textEid.Text) ||
                string.IsNullOrEmpty(textMn.Text) || string.IsNullOrEmpty(comboCom.Text) ||
                string.IsNullOrEmpty(comboWcom.Text) || string.IsNullOrEmpty(textFcom.Text) ||
                string.IsNullOrEmpty(comboPlace.Text) || string.IsNullOrEmpty(comboAT.Text) ||
                string.IsNullOrEmpty(comboAs.Text) || string.IsNullOrEmpty(textRn.Text) ||
                comboDate.Value == null || string.IsNullOrEmpty(comboOn.Text) ||
                string.IsNullOrEmpty(textMnn.Text) || string.IsNullOrEmpty(textDef.Text) ||
                string.IsNullOrEmpty(textDofa.Text) || string.IsNullOrEmpty(textConTa.Text) ||
                string.IsNullOrEmpty(textDefi2.Text))
            {
                MessageBox.Show("يرجى تعبئة جميع الحقول!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // ✅ التحقق من صحة الرقم الوطني (يبدأ بـ 1 أو 2، ويكون 12 رقمًا)
                if (!System.Text.RegularExpressions.Regex.IsMatch(textEid.Text, @"^[12]\d{11}$"))
                {
                    MessageBox.Show("الرجاء إدخال رقم وطني صحيح يبدأ بـ 1 أو 2 ويتكون من 12 رقمًا بدون فراغات.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //// ✅ التحقق إذا كان الرقم الوطني موجودًا في قاعدة البيانات
                //string checkQuery = "SELECT COUNT(*) FROM iiinsertion WHERE nId = @nId";
                //using (SqlConnection conn = new SqlConnection(connectionString))
                //{
                //    conn.Open();
                //    SqlCommand cmd = new SqlCommand(checkQuery, conn);
                //    cmd.Parameters.AddWithValue("@nId", long.Parse(textEid.Text));

                //    int count = (int)cmd.ExecuteScalar();

                //    if (count > 0)
                //    {
                //        // الرقم الوطني موجود بالفعل
                //        var result = MessageBox.Show("الرقم الوطني الذي قمت بإدخاله موجود بالفعل. هل تريد جلب بياناته؟",
                //                                     "الرقم الوطني موجود", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //        if (result == DialogResult.Yes)
                //        {
                //            // جلب البيانات وتعبئة الحقول
                //            string query = "SELECT * FROM iiinsertion WHERE nId = @nId";
                //            SqlDataAdapter da = new SqlDataAdapter(query, connectionString);
                //            da.SelectCommand.Parameters.AddWithValue("@nId", long.Parse(textEid.Text));

                //            DataTable dt = new DataTable();
                //            da.Fill(dt);

                //            if (dt.Rows.Count > 0)
                //            {
                //                DataRow row = dt.Rows[0];

                //                // تعبئة الحقول بالبيانات من قاعدة البيانات
                //                textname.Text = row["cus_name"].ToString();
                //                textMn.Text = row["mo_name"].ToString();
                //                comboCom.Text = row["ministry"].ToString();
                //                comboWcom.Text = row["old_work"].ToString();
                //                textFcom.Text = row["work_sub"].ToString();
                //                comboPlace.Text = row["region"].ToString();
                //                comboAs.Text = row["hala"].ToString();
                //                comboAT.Text = row["n_hala"].ToString();
                //                textRn.Text = row["re_num"].ToString();
                //                comboDate.Value = (DateTime)row["re_date"];
                //                comboOn.Text = row["makteb"].ToString();
                //                textMnn.Text = row["mo_num"].ToString();
                //                textDef.Text = row["def"].ToString();
                //                comboEn.Text = row["emp_name"].ToString();
                //                textdate.Value = (DateTime)row["En_date"];
                //                textDofa.Text = row["Dofa"].ToString();
                //                textConUser.Text = row["Confirm_User"].ToString();
                //                textDefi2.Text = row["Deficiencies"].ToString();
                //                textConTa.Text = row["Confirm_Status"].ToString();
                //            }
                //            return; // الخروج من الدالة إذا كان المستخدم قد اختار جلب البيانات
                //        }
                //        else
                //        {
                //            return; // إذا اختار المستخدم عدم جلب البيانات، نخرج من الدالة
                //        }
                //    }
                //}

                //// ✅ استرداد القيم المرجعية من الجداول الأخرى
                string noWorkDir = GetSingleValueFromDB(connectionString,
                    "SELECT no_work_dir FROM jehaa_name$ WHERE jeha_name = @jeha_name", "@jeha_name", comboWcom.Text);

                string noWazara = GetSingleValueFromDB(connectionString,
                    "SELECT no_wazara FROM Ministry$ WHERE wazara_name = @wazara_name", "@wazara_name", comboCom.Text);

                string offNum = GetSingleValueFromDB(connectionString,
                    "SELECT OffNum FROM OffName$ WHERE OffName = @OffName", "@OffName", comboOn.Text);

                // ✅ إدخال البيانات في قاعدة البيانات
                using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM iiinsertion WHERE 1=0", connectionString))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "iiinsertion");
                    DataTable dt = ds.Tables["iiinsertion"];

                    // ✅ إنشاء صف جديد وإضافة البيانات إليه
                    DataRow newRow = dt.NewRow();

                    newRow["nId"] = long.Parse(textEid.Text);
                    newRow["cus_name"] = textname.Text;
                    newRow["mo_name"] = textMn.Text;
                    newRow["ministry"] = comboCom.Text;
                    newRow["old_work"] = comboWcom.Text;
                    newRow["work_sub"] = textFcom.Text;
                    newRow["region"] = comboPlace.Text;
                    newRow["hala"] = comboAs.Text;
                    newRow["n_hala"] = comboAT.Text;
                    newRow["re_num"] = textRn.Text;
                    newRow["re_date"] = comboDate.Value;
                    newRow["makteb"] = comboOn.Text;
                    newRow["mo_num"] = long.Parse(textMnn.Text);
                    newRow["def"] = textDef.Text;
                    newRow["emp_name"] = comboEn.Text;
                    newRow["En_date"] = textdate.Value;
                    newRow["no_work_dir"] = noWorkDir;
                    newRow["no_wazara"] = noWazara;
                    newRow["OffNum"] = offNum;
                    newRow["Dofa"] = textDofa.Text;
                    newRow["Confirm_User"] = textConUser.Text;
                    newRow["Deficiencies"] = textDefi2.Text;
                    newRow["Confirm_Date"] = DateTime.Now;
                    newRow["Confirm_Status"] = textConTa.Text;

                    dt.Rows.Add(newRow);

                    // ✅ تحديث قاعدة البيانات
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                    adapter.Update(ds, "iiinsertion");

                    MessageBox.Show("تم الإدخال بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textEid.Enabled = true;
                }
                BindData();

                ResetFields();
            }
            catch (FormatException)
            {
                MessageBox.Show("تأكد من إدخال القيم الرقمية بشكل صحيح (مثل الحقول re_num, mo_num).", "خطأ في التنسيق", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء الإدخال: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// جلب قيمة مفردة من قاعدة البيانات باستخدام `SqlDataAdapter`
        /// </summary>
        private string GetSingleValueFromDB(string connectionString, string query, string paramName, string paramValue)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connectionString))
            {
                adapter.SelectCommand.Parameters.AddWithValue(paramName, paramValue);

                DataTable dt = new DataTable();
                adapter.Fill(dt); // تحميل البيانات إلى DataTable

                return dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : string.Empty;
            }
        }



        private void btnReset_Click(object sender, EventArgs e)
        {
            textEid.Enabled=true;
            if (MessageBox.Show("هل تريد حقًا مسح الحقول؟", "Confirmation message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // مسح الحقول من النوع TextBox
                textname.Clear();
                textEid.Clear();
                textMn.Clear();
                textFcom.Clear();
                textRn.Clear();
                textMnn.Clear();
                textDef.Clear();
                textDofa.Clear(); // تأكد من أنه TextBox
           //     textConTa.Clear(); // تأكد من أنه TextBox
                textDefi2.Clear(); // تأكد من أنه TextBox

                // إعادة تعيين القيم في ComboBox
                comboCom.ResetText();
                comboWcom.ResetText();
                comboPlace.ResetText();
                comboAs.ResetText();
                comboAT.ResetText();
                comboOn.ResetText();

                // إعادة تعيين DateTimePicker
                comboDate.Value = DateTime.Now;
                dateTimePicker1.Value = DateTime.Now; // إذا كان الحقل DateTimePicker
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-GBO380T;Initial Catalog=DIC2025;Integrated Security=True;";
            string loggedInUser = LoggedInUserName; // يجب جلب هذا من النظام الحالي

            // إزالة الفراغات الزائدة والتحقق من الحقول
            textname.Text = System.Text.RegularExpressions.Regex.Replace(textname.Text.Trim(), @"\s+", " ");
            textMn.Text = System.Text.RegularExpressions.Regex.Replace(textMn.Text.Trim(), @"\s+", " ");
            textFcom.Text = System.Text.RegularExpressions.Regex.Replace(textFcom.Text.Trim(), @"\s+", " ");
            textDef.Text = System.Text.RegularExpressions.Regex.Replace(textDef.Text.Trim(), @"\s+", " ");
            textDofa.Text = textDofa.Text.Replace(" ", "");
            textConTa.Text = textConTa.Text.Replace(" ", "");
            textDefi2.Text = System.Text.RegularExpressions.Regex.Replace(textDefi2.Text.Trim(), @"\s+", " ");

            // التحقق من صحة الرقم الوطني
            if (!System.Text.RegularExpressions.Regex.IsMatch(textEid.Text, "^[12]\\d{11}$"))
            {
                MessageBox.Show("الرجاء إدخال رقم وطني صحيح يبدأ بـ 1 أو 2 ويتكون من 12 رقمًا.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    string selectQuery = "SELECT * FROM iiinsertion WHERE nId = @nId";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@nId", long.Parse(textEid.Text));
                        adapter.Fill(dt);
                    }

                    if (dt.Rows.Count > 0)
                    {
                        string confirmUser = dt.Rows[0]["Confirm_User"].ToString();

                        if (string.IsNullOrEmpty(confirmUser) || confirmUser == LoggedInUserName)
                        {
                            // **إذا كان Confirm_User فارغًا أو يطابق المستخدم الحالي → تحديث البيانات في نفس الصف**
                            DataRow row = dt.Rows[0];
                         
                            row["cus_name"] = textname.Text;
                            row["mo_name"] = textMn.Text;
                            row["ministry"] = comboCom.Text;
                            row["old_work"] = comboWcom.Text;
                            row["work_sub"] = textFcom.Text;
                            row["region"] = comboPlace.Text;
                            row["hala"] = comboAs.Text;
                            row["n_hala"] = comboAT.Text;
                            row["re_num"] = textRn.Text;
                            row["re_date"] = comboDate.Value;
                            row["makteb"] = comboOn.Text;
                            row["mo_num"] = long.Parse(textMnn.Text);
                            row["def"] = textDef.Text;
                            row["emp_name"] = comboEn.Text;
                            row["En_date"] = textdate.Value;
                            row["Dofa"] = textDofa.Text;
                            row["Confirm_Status"] = (textConTa.Text == "مؤكد")?"1":"0";
                            row["Confirm_User"] = LoggedInUserName;
                            row["Deficiencies"] = textDefi2.Text;
                            row["Confirm_Date"] = DateTime.Now;

                            string updateQuery = @"UPDATE iiinsertion SET 
    cus_name = @cus_name, mo_name = @mo_name, ministry = @ministry, 
    old_work = @old_work, work_sub = @work_sub, region = @region, hala = @hala, 
    n_hala = @n_hala, re_num = @re_num, re_date = @re_date, makteb = @makteb, 
    mo_num = @mo_num, def = @def, emp_name = @emp_name, En_date = @En_date, 
    Dofa = @Dofa, Confirm_Status = @Confirm_Status, Confirm_User = @Confirm_User, 
    Deficiencies = @Deficiencies, Confirm_Date = @Confirm_Date 
    WHERE nId = @nId";

                            using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, con))
                            {
                                cmdUpdate.Parameters.AddWithValue("@nId", long.Parse(textEid.Text));
                                cmdUpdate.Parameters.AddWithValue("@cus_name", textname.Text);
                                cmdUpdate.Parameters.AddWithValue("@mo_name", textMn.Text);
                                cmdUpdate.Parameters.AddWithValue("@ministry", comboCom.Text);
                                cmdUpdate.Parameters.AddWithValue("@old_work", comboWcom.Text);
                                cmdUpdate.Parameters.AddWithValue("@work_sub", textFcom.Text);
                                cmdUpdate.Parameters.AddWithValue("@region", comboPlace.Text);
                                cmdUpdate.Parameters.AddWithValue("@hala", comboAs.Text);
                                cmdUpdate.Parameters.AddWithValue("@n_hala", comboAT.Text);
                                cmdUpdate.Parameters.AddWithValue("@re_num", textRn.Text);
                                cmdUpdate.Parameters.AddWithValue("@re_date", comboDate.Value);
                                cmdUpdate.Parameters.AddWithValue("@makteb", comboOn.Text);
                                cmdUpdate.Parameters.AddWithValue("@mo_num", long.Parse(textMnn.Text));
                                cmdUpdate.Parameters.AddWithValue("@def", textDef.Text);
                                cmdUpdate.Parameters.AddWithValue("@emp_name", comboEn.Text);
                                cmdUpdate.Parameters.AddWithValue("@En_date", textdate.Value);
                                cmdUpdate.Parameters.AddWithValue("@Dofa", textDofa.Text);
                                cmdUpdate.Parameters.AddWithValue("@Confirm_Status", (textConTa.Text == "مؤكد")?"1":"0");
                                cmdUpdate.Parameters.AddWithValue("@Confirm_User", loggedInUser);
                                cmdUpdate.Parameters.AddWithValue("@Deficiencies", textDefi2.Text);
                                cmdUpdate.Parameters.AddWithValue("@Confirm_Date", DateTime.Now);

                                cmdUpdate.ExecuteNonQuery();
                            }


                            MessageBox.Show("تم التحديث بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // **إذا كان Confirm_User مختلفًا عن LoggedInUserName → إدراج صف جديد**
                            string insertQuery = @"INSERT INTO iiinsertion 
                        (nId, cus_name, mo_name, ministry, old_work, work_sub, region, hala, n_hala, 
                        re_num, re_date, makteb, mo_num, def, emp_name, En_date, Dofa, Confirm_Status, 
                        Confirm_User, Deficiencies, Confirm_Date) 
                        VALUES 
                        (@nId, @cus_name, @mo_name, @ministry, @old_work, @work_sub, @region, @hala, @n_hala, 
                        @re_num, @re_date, @makteb, @mo_num, @def, @emp_name, @En_date, @Dofa, @Confirm_Status, 
                        @Confirm_User, @Deficiencies, @Confirm_Date)";

                            using (SqlCommand cmdInsert = new SqlCommand(insertQuery, con))
                            {
                                cmdInsert.Parameters.AddWithValue("@nId", long.Parse(textEid.Text));
                                cmdInsert.Parameters.AddWithValue("@cus_name", textname.Text);
                                cmdInsert.Parameters.AddWithValue("@mo_name", textMn.Text);
                                cmdInsert.Parameters.AddWithValue("@ministry", comboCom.Text);
                                cmdInsert.Parameters.AddWithValue("@old_work", comboWcom.Text);
                                cmdInsert.Parameters.AddWithValue("@work_sub", textFcom.Text);
                                cmdInsert.Parameters.AddWithValue("@region", comboPlace.Text);
                                cmdInsert.Parameters.AddWithValue("@hala", comboAs.Text);
                                cmdInsert.Parameters.AddWithValue("@n_hala", comboAT.Text);
                                cmdInsert.Parameters.AddWithValue("@re_num", textRn.Text);
                                cmdInsert.Parameters.AddWithValue("@re_date", comboDate.Value);
                                cmdInsert.Parameters.AddWithValue("@makteb", comboOn.Text);
                                cmdInsert.Parameters.AddWithValue("@mo_num", long.Parse(textMnn.Text));
                                cmdInsert.Parameters.AddWithValue("@def", textDef.Text);
                                cmdInsert.Parameters.AddWithValue("@emp_name", comboEn.Text);
                                cmdInsert.Parameters.AddWithValue("@En_date", textdate.Value);
                                cmdInsert.Parameters.AddWithValue("@Dofa", textDofa.Text);
                                cmdInsert.Parameters.AddWithValue("@Confirm_Status", textConTa.Text);
                                cmdInsert.Parameters.AddWithValue("@Confirm_User", LoggedInUserName);
                                cmdInsert.Parameters.AddWithValue("@Deficiencies", textDefi2.Text);
                                cmdInsert.Parameters.AddWithValue("@Confirm_Date", DateTime.Now);

                                cmdInsert.ExecuteNonQuery();
                            }

                            MessageBox.Show("تمت إضافة سجل جديد بسبب اختلاف المستخدم.", "إضافة ناجحة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        BindData();
                        ResetFields();
                        textEid.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("لم يتم العثور على سجل بهذا الرقم الوطني.", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("تأكد من إدخال القيم الرقمية بشكل صحيح.", "خطأ في التنسيق", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء التحديث: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                try
                {
                    if (!int.TryParse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), out int CID))
                    {
                        MessageBox.Show("رقم المعرف غير صالح!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                  

                    using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GBO380T;Initial Catalog=DIC2025;Integrated Security=True;"))
                    {
                        con.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        SqlCommand cmd = new SqlCommand("exec dbo.deletedpro @id=@Cid , @deleteduser = @delUser", con);
                        cmd.Parameters.AddWithValue("@Cid", CID);
                        cmd.Parameters.AddWithValue("@delUser", LoginForm.IdUser);
                        adapter.DeleteCommand = cmd;

                        int rowsAffected = adapter.DeleteCommand.ExecuteNonQuery(); // عدد الصفوف المتأثرة
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("تم الحذف بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index); // إزالة الصف من الجدول بدلاً من إعادة تحميل البيانات بالكامل
                        }
                        else
                        {
                            MessageBox.Show("لم يتم العثور على سجل بهذا المعرف!", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"حدث خطأ أثناء الحذف: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            textEid.Enabled = false;

            try
            {
                if (string.IsNullOrWhiteSpace(textname.Text) &&
                    string.IsNullOrWhiteSpace(textEid.Text) &&
                    string.IsNullOrWhiteSpace(textMn.Text) &&
                    string.IsNullOrWhiteSpace(comboCom.Text) &&
                    string.IsNullOrWhiteSpace(comboWcom.Text) &&
                    string.IsNullOrWhiteSpace(textFcom.Text) &&
                    string.IsNullOrWhiteSpace(comboPlace.Text) &&
                    string.IsNullOrWhiteSpace(comboAs.Text) &&
                    string.IsNullOrWhiteSpace(comboAT.Text) &&
                    string.IsNullOrWhiteSpace(textRn.Text) &&
                    string.IsNullOrWhiteSpace(comboOn.Text) &&
                    string.IsNullOrWhiteSpace(textMnn.Text) &&
                    string.IsNullOrWhiteSpace(textDef.Text) &&
                    string.IsNullOrWhiteSpace(textDofa.Text) &&
                    string.IsNullOrWhiteSpace(textConTa.Text))
                {
                    MessageBox.Show("يرجى إدخال معايير البحث!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                textEid.Enabled = true;

                // إنشاء الاتصال مع قاعدة البيانات
                string connectionString = @"Data Source=DESKTOP-GBO380T;Initial Catalog=DIC2025;Integrated Security=True;";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // إنشاء الاستعلام الأساسي
                    string query = "SELECT * FROM iiinsertion WHERE 1=1";
                    List<SqlParameter> parameters = new List<SqlParameter>();

                    // إضافة شروط البحث إلى الاستعلام
                    if (!string.IsNullOrWhiteSpace(textname.Text))
                    {
                        query += " AND cus_name LIKE @cus_name";
                        parameters.Add(new SqlParameter("@cus_name", $"%{textname.Text}%"));
                    }

                    if (!string.IsNullOrWhiteSpace(textEid.Text) && long.TryParse(textEid.Text, out long nId))
                    {
                        query += " AND nId = @nId";
                        parameters.Add(new SqlParameter("@nId", nId));
                    }

                    if (!string.IsNullOrWhiteSpace(textMn.Text))
                    {
                        query += " AND mo_name LIKE @mo_name";
                        parameters.Add(new SqlParameter("@mo_name", $"%{textMn.Text}%"));
                    }

                    if (!string.IsNullOrWhiteSpace(comboCom.Text))
                    {
                        query += " AND ministry LIKE @ministry";
                        parameters.Add(new SqlParameter("@ministry", $"%{comboCom.Text}%"));
                    }

                    if (!string.IsNullOrWhiteSpace(comboWcom.Text))
                    {
                        query += " AND old_work LIKE @old_work";
                        parameters.Add(new SqlParameter("@old_work", $"%{comboWcom.Text}%"));
                    }

                    if (!string.IsNullOrWhiteSpace(textFcom.Text))
                    {
                        query += " AND work_sub LIKE @work_sub";
                        parameters.Add(new SqlParameter("@work_sub", $"%{textFcom.Text}%"));
                    }

                    if (!string.IsNullOrWhiteSpace(comboPlace.Text))
                    {
                        query += " AND region LIKE @region";
                        parameters.Add(new SqlParameter("@region", $"%{comboPlace.Text}%"));
                    }

                    if (!string.IsNullOrWhiteSpace(comboAs.Text))
                    {
                        query += " AND hala LIKE @hala";
                        parameters.Add(new SqlParameter("@hala", $"%{comboAs.Text}%"));
                    }

                    if (!string.IsNullOrWhiteSpace(comboAT.Text))
                    {
                        query += " AND n_hala LIKE @n_hala";
                        parameters.Add(new SqlParameter("@n_hala", $"%{comboAT.Text}%"));
                    }

                    if (!string.IsNullOrWhiteSpace(textRn.Text))
                    {
                        query += " AND re_num LIKE @re_num";
                        parameters.Add(new SqlParameter("@re_num", $"%{textRn.Text}%"));
                    }

                    if (!string.IsNullOrWhiteSpace(comboOn.Text))
                    {
                        query += " AND makteb LIKE @makteb";
                        parameters.Add(new SqlParameter("@makteb", $"%{comboOn.Text}%"));
                    }

                    if (!string.IsNullOrWhiteSpace(textMnn.Text) && long.TryParse(textMnn.Text, out long mo_num))
                    {
                        query += " AND mo_num = @mo_num";
                        parameters.Add(new SqlParameter("@mo_num", mo_num));
                    }

                    if (!string.IsNullOrWhiteSpace(textDef.Text))
                    {
                        query += " AND def LIKE @def";
                        parameters.Add(new SqlParameter("@def", $"%{textDef.Text}%"));
                    }

                    if (!string.IsNullOrWhiteSpace(textDofa.Text))
                    {
                        query += " AND Dofa LIKE @Dofa";
                        parameters.Add(new SqlParameter("@Dofa", $"%{textDofa.Text}%"));
                    }

                    if (!string.IsNullOrWhiteSpace(textConTa.Text))
                    {
                        query += " AND Confirm_Status LIKE @Confirm_Status";
                        parameters.Add(new SqlParameter("@Confirm_Status", $"%{textConTa.Text}%"));
                    }

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // إضافة المعاملات
                        cmd.Parameters.AddRange(parameters.ToArray());

                        // إنشاء SqlDataAdapter لاستخدامه مع DataSet
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();

                        // فتح الاتصال وتنفيذ الاستعلام
                        con.Open();
                        da.Fill(ds, "SearchResults");

                        // تعيين البيانات في DataGridView
                        if (ds.Tables["SearchResults"].Rows.Count > 0)
                        {
                            dataGridView1.DataSource = ds.Tables["SearchResults"];
                        }
                        else
                        {
                            MessageBox.Show("لا توجد بيانات مطابقة للبحث", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        textEid.Enabled = true;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void comboWcom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void LoadSuggestions(ComboBox comboBox, string columnName, string tableName)
        {
            try
            {
                // سلسلة الاتصال
                string connectionString = @"Data Source=DESKTOP-GBO380T;Initial Catalog=DIC2025;Integrated Security=True;";

                // الاستعلام لجلب القيم الفريدة من العمود المطلوب
                string query = $"SELECT DISTINCT {columnName} FROM {tableName}";

                // إنشاء DataTable لتخزين البيانات
                DataTable dt = new DataTable();

                // استخدام SqlDataAdapter لجلب البيانات دون الحاجة إلى إبقاء الاتصال مفتوحًا
                using (SqlDataAdapter da = new SqlDataAdapter(query, connectionString))
                {
                    da.Fill(dt); // تحميل البيانات في DataTable
                }

                // استخدام HashSet لتجنب التكرارات
                HashSet<string> uniqueValues = new HashSet<string>();

                foreach (DataRow row in dt.Rows)
                {
                    string value = row[columnName].ToString().Trim();
                    uniqueValues.Add(value); // تجنب التكرارات
                }

                // إعداد AutoComplete وملء ComboBox
                AutoCompleteStringCollection suggestions = new AutoCompleteStringCollection();
                suggestions.AddRange(uniqueValues.ToArray());

                comboBox.AutoCompleteCustomSource = suggestions;
                comboBox.Items.Clear();
                comboBox.Items.AddRange(uniqueValues.ToArray());
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"خطأ في قاعدة البيانات أثناء تحميل {comboBox.Name}: {ex.Message}", "خطأ SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ غير متوقع أثناء تحميل {comboBox.Name}: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void get_Click(object sender, EventArgs e)
        {
            textEid.Enabled = false;

            // التحقق من إدخال nId
            if (string.IsNullOrEmpty(textEid.Text))
            {
                MessageBox.Show("يرجى إدخال الرقم الوطني (nId)!", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                //    using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-GBO380T;Initial Catalog=DIC2025;Integrated Security=True;"))
                //    {
                //        con.Open();

                //        // جملة SQL لجلب البيانات
                //        string query = @"
                //SELECT cus_name, mo_name, ministry, old_work, work_sub, region, hala, n_hala, re_num, re_date, makteb, mo_num, def, 
                //       emp_name, En_date, Confirm_Date, Dofa, Confirm_Status, Deficiencies
                //FROM iiinsertion 
                //WHERE nId = @nId";

                      
                //        Console.WriteLine(row.Cells[1].Value.ToString());

                //        SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                //        adapter.SelectCommand.Parameters.AddWithValue("@nId", long.Parse(row.Cells[1].Value.ToString())); // استخدام nId للبحث

                //        DataTable dt = new DataTable();
                //        adapter.Fill(dt);

                if (dataGridView1.Rows.Count > 0)
                {
                    DataGridViewRow row = dataGridView1.SelectedRows[0];
                    // تعبئة الحقول بالبيانات
                    textEid.Text = row.Cells["nId"].Value.ToString();
                    textname.Text = row.Cells["cus_name"].Value.ToString();
                    textMn.Text = row.Cells["mo_name"].Value.ToString();
                    comboCom.Text = row.Cells["ministry"].Value.ToString();
                    comboWcom.Text = row.Cells["old_work"].Value.ToString();
                    textFcom.Text = row.Cells["work_sub"].Value.ToString();
                    comboPlace.Text = row.Cells["region"].Value.ToString();
                    comboAs.Text = row.Cells["hala"].Value.ToString();
                    comboAT.Text = row.Cells["n_hala"].Value.ToString();
                    textRn.Text = row.Cells["re_num"].Value.ToString();
                    comboDate.Value = DateTime.Parse(row.Cells["re_date"].Value.ToString());
                    comboOn.Text = row.Cells["makteb"].Value.ToString();
                    textMnn.Text = row.Cells["mo_num"].Value.ToString();
                    textDef.Text = row.Cells["def"].Value.ToString();

                    // جلب أو تعيين القيم الافتراضية للحقول المطلوبة
                    comboEn.Text = string.IsNullOrEmpty(row.Cells["emp_name"].Value.ToString()) ? LoggedInUserName : row.Cells["emp_name"].Value.ToString();

                    textdate.Value = row.Cells["En_date"].Value == DBNull.Value ? DateTime.Now : DateTime.Parse(row.Cells["En_date"].Value.ToString());

                    textDofa.Text = row.Cells["Dofa"].Value.ToString();
                    textConTa.Text = row.Cells["Confirm_Status"].Value.ToString();
                    textDefi2.Text = row.Cells["Deficiencies"].Value.ToString();
                }
                else
                {
                    // تعيين القيم الافتراضية إذا لم يتم العثور على البيانات
                    MessageBox.Show("الرجاء الضغط على زر البحث أولا!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    comboEn.Text = LoggedInUserName;
                    textdate.Value = DateTime.Now;
                    dateTimePicker1.Value = DateTime.Now;
                    textDofa.Clear();
                  //  textConTa.Clear();
                    textDefi2.Clear();
                }
            //}
            }
            catch (FormatException)
            {
                MessageBox.Show("تأكد من إدخال الرقم التعريفي بشكل صحيح!", "خطأ في التنسيق", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء جلب البيانات: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // التأكد من أن النقر لم يكن على الهيدر
            {
                try
                {
                    // الحصول على nId من الصف المحدد
                    string selectedNId = dataGridView1.Rows[e.RowIndex].Cells["nId"].Value.ToString();

                    // إظهار رسالة تأكيد
                    DialogResult result = MessageBox.Show("هل تريد جلب بيانات هذا الصف؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        textEid.Text = selectedNId; // وضع الرقم في الحقل
                        get_Click(sender, e); // استدعاء دالة جلب البيانات
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"حدث خطأ أثناء تحديد الصف: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textEid_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
           
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // التأكد من أن النقر تم على صف وليس على الهيدر
            {
                try
                {
                    // الحصول على CID من الصف المحدد
                    int selectedCID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["CID"].Value);

                    // إظهار رسالة تأكيد
                    DialogResult result = MessageBox.Show("هل تريد جلب بيانات هذا الصف؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        string connectionString = @"Data Source=DESKTOP-GBO380T;Initial Catalog=DIC2025;Integrated Security=True;";
                        string query = @"
                SELECT nId, cus_name, mo_name, ministry, old_work, work_sub, region, hala, n_hala, re_num, re_date, makteb, mo_num, def, 
                       emp_name, En_date, Confirm_Date, Dofa, Confirm_Status, Deficiencies
                FROM iiinsertion 
                WHERE CID = @CID";

                        using (SqlDataAdapter adapter = new SqlDataAdapter(query, connectionString))
                        {
                            adapter.SelectCommand.Parameters.AddWithValue("@CID", selectedCID);

                            DataTable dt = new DataTable();
                            adapter.Fill(dt); // تحميل البيانات بدون الحاجة إلى فتح الاتصال يدوياً

                            if (dt.Rows.Count > 0)
                            {
                                // تعبئة الحقول بالبيانات من الصف الذي تم النقر عليه
                                DataRow row = dt.Rows[0];

                                textEid.Text = row["nId"].ToString();
                                textname.Text = row["cus_name"].ToString();
                                textMn.Text = row["mo_name"].ToString();
                                comboCom.Text = row["ministry"].ToString();
                                comboWcom.Text = row["old_work"].ToString();
                                textFcom.Text = row["work_sub"].ToString();
                                comboPlace.Text = row["region"].ToString();
                                comboAs.Text = row["hala"].ToString();
                                comboAT.Text = row["n_hala"].ToString();
                                textRn.Text = row["re_num"].ToString();
                                comboDate.Value = DateTime.Parse(row["re_date"].ToString());
                                comboOn.Text = row["makteb"].ToString();
                                textMnn.Text = row["mo_num"].ToString();
                                textDef.Text = row["def"].ToString();

                                comboEn.Text = string.IsNullOrEmpty(row["emp_name"].ToString())
                                    ? LoggedInUserName
                                    : row["emp_name"].ToString();

                                textdate.Value = row["En_date"] == DBNull.Value
                                    ? DateTime.Now
                                    : DateTime.Parse(row["En_date"].ToString());

                                textDofa.Text = row["Dofa"].ToString();
                                textConTa.Text = row["Confirm_Status"].ToString();
                                textDefi2.Text = row["Deficiencies"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("لم يتم العثور على بيانات لهذا الصف!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"حدث خطأ أثناء جلب البيانات: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        
        }
    }
}
