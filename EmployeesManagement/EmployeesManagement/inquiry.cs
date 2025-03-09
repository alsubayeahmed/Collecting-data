using EmployeesManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;



namespace Employeesinquiry
{
    public partial class inquiry : Form
    {
        private string username;
        //  string connectionString = @"Data Source=DESKTOP-G;Initial Catalog=DWConfiguration;Integrated Security=True;";
        string query = "SELECT * FROM [373] WHERE text_id = @text_id";
        string ConenctTo104 = "";
        
        public inquiry(string loggedInUserName)
        {
            //  InitializeComponent();
            username = loggedInUserName;

        }
     //   SqlConnection con = new SqlConnection(zzSS01;Initial Catalog=DIC2025;Integrated Security=True;");

        public string LoggedInUserName { get; set; }
        public inquiry()
        {
            InitializeComponent();
            //  ResetFields();
            //BindData();
        }
        void BindData()
        {
            string query = "SELECT CID, nId, cus_name, mo_name, ministry, old_work, work_sub, region, hala, n_hala, re_num, makteb, mo_num, def " +
                           "FROM iiinsertion " +
                           "WHERE emp_name = @emp_name AND CAST(En_date AS DATE) = CAST(GETDATE() AS DATE)";

            using (SqlConnection con = new SqlConnection(ConenctTo104))
            using (SqlDataAdapter sa = new SqlDataAdapter(query, con))
            {
                sa.SelectCommand.Parameters.AddWithValue("@emp_name", comboEn.Text);

                DataTable dt = new DataTable();
                sa.Fill(dt);

                dataGridView1.DataSource = dt;
            }
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
        List<string> restrictedUsersForFields = new List<string> { "Ahmed", "User2" }; // قائمة المستخدمين الممنوعين
        private void textEid_KeyDown(object sender, KeyEventArgs e)
        {
            if (restrictedUsersForFields.Contains(username))
            {
                e.SuppressKeyPress = true; // منع الكتابة
                MessageBox.Show("ليس لديك صلاحية التعديل على هذا الحقل!", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        List<string> restrictedUsersForEvents = new List<string> { "Ahmed", "User4" };
        private void comboAs_Click(object sender, EventArgs e)
        {
            if (restrictedUsersForEvents.Contains(username))
            {
                MessageBox.Show("ليس لديك صلاحية التفاعل مع هذا الحقل!", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // منع الحدث
            }
        }

        private void comboDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (restrictedUsersForFields.Contains(username))
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

        List<string> restrictedUsers = new List<string> { "Ahmed", "User2", "User3" };

        // اجلب اسم المستخدم من واجهة تسجيل الدخول

        private void inquiry_Load(object sender, EventArgs e)
        {

            ColConClasss colConClasss = new ColConClasss();


            ConenctTo104 = $"Server={ColConClasss.ServerName104};Database={ColConClasss.DataBase104};User id={ColConClasss.User104};Password={ColConClasss.PasswordCol};Trusted_Connection= {ColConClasss.Trusted_Connection104};";


            
            ShowName.Text = LoggedInUserName;
            ShowTimeNow.Text = DateTime.Now.ToString("dd/MM/yyyy");


            try
            {
                // إعداد DataTable فارغ للـ DataGridView
                DataTable emptyTable = new DataTable();
                dataGridView1.DataSource = emptyTable;

                // إعداد اسم المستخدم في ComboBox (إذا كان مستخدمًا مسجلاً)
                comboEn.Text = LoggedInUserName;

                // ملء الاقتراحات في ComboBoxes
                LoadSuggestions(comboWcom, "jeha_name", "jehaa_name$");
                LoadSuggestions(comboCom, "wazara_name", "Ministry$");
                LoadSuggestions(comboPlace, "OffName", "OffName$");
                LoadSuggestions(comboAs, "hala", "HALA$");
                LoadSuggestions(comboAT, "Hala", "ProcedureState$");
                LoadSuggestions(comboOn, "OffName", "OffName$");
              //  LoadSuggestions(comboOn, "OffName", "OffName$");

                // منع التحديد والنسخ إذا كان المستخدم ضمن القائمة المحظورة
                if (restrictedUsers.Contains(username))
                {
                    dataGridView1.SelectionChanged += PreventSelection;
                    dataGridView1.ContextMenuStrip = new ContextMenuStrip(); // تعطيل القائمة السياقية
                    dataGridView1.KeyDown += PreventCopy;
                }

                // إنشاء اتصال بقاعدة البيانات
                SqlConnection con = new SqlConnection(ConenctTo104);

                // استعلامات ملء بيانات ComboBoxes
                FillComboBox(con, comboAs, "Select * From HALA$", "hala");
                FillComboBox(con, comboWcom, "Select * From jehaa_name$", "jeha_name", "no_work_dir1");
                FillComboBox(con, comboCom, "Select * From Ministry$", "wazara_name", "المعرف");
                FillComboBox(con, comboOn, "Select * From OffName$", "OffName", "Id");
                FillComboBox(con, comboPlace, "Select * From OffName$", "OffName", "Id");
                FillComboBox(con, comboAT, "Select * From ProcedureState$", "Hala", "HId");

                UpdateEntryCount();
                UpdateConfirmedEntryCount();

                // استعلام لعرض أسماء الأعمدة فقط عند تحميل الواجهة
                string query = "SELECT TOP 0  CID, nId, cus_name, mo_name, ministry, old_work, work_sub, region, hala, n_hala, re_num, makteb, mo_num, def  FROM iiinsertion";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // تعيين أسماء الأعمدة إلى DataGridView
                dataGridView1.DataSource = dt;

                // إعادة تعيين الحقول النصية بعد التحميل
                ResetFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء تحميل البيانات: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillComboBox(SqlConnection con, ComboBox comboBox, string query, string displayMember, string valueMember = null)
        {
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox.DataSource = dt;
            comboBox.DisplayMember = displayMember;
            if (valueMember != null)
                comboBox.ValueMember = valueMember;
        }
        private void ResetFields()
        {
            textname.Clear();
            textEid.Clear();
            textMn.Clear();
            textFcom.Clear();
            textRn.Clear();
            comboDate.ResetText();
            textMnn.Clear();
            textDef.Clear();


            comboCom.ResetText();
            comboWcom.ResetText();
            comboPlace.ResetText();
            comboAs.ResetText();
            comboAT.ResetText();
            comboOn.ResetText();



            textdate.Text = DateTime.Now.ToString();

        }


        private void UpdateEntryCount()
        {
          //  string connectionString = @"Data Source=DESKTOP-KKP05IN\SQLEXPRESS01;Initial Catalog=DIC2025;Integrated Security=True;";

            using (SqlConnection con = new SqlConnection(ConenctTo104))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    $"SELECT COUNT(*) FROM iiinsertion WHERE CAST(En_date AS DATE) = CAST(GETDATE() AS DATE) and userId = {LoginForm.IdUser} ", con);

                int count = (int)cmd.ExecuteScalar(); // استرجاع العدد كعدد صحيح
                lblCount.Text = $" {count}"; // تحديث التسمية
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
                string query = $"SELECT DISTINCT {columnName} FROM {tableName}";

                using (SqlConnection con = new SqlConnection(ConenctTo104))
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    AutoCompleteStringCollection suggestions = new AutoCompleteStringCollection();

                    foreach (DataRow row in dt.Rows)
                    {
                        string value = row[columnName].ToString();
                        suggestions.Add(value);
                    }

                    // تحديث بيانات الـ ComboBox
                    comboBox.Invoke((MethodInvoker)delegate
                    {
                        comboBox.AutoCompleteCustomSource = suggestions; // تعيين الاقتراحات
                        comboBox.Items.Clear(); // مسح العناصر السابقة
                        comboBox.Items.AddRange(suggestions.Cast<string>().ToArray()); // إضافة العناصر الجديدة
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء تحميل البيانات لـ {comboBox.Name}: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void textEid_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click_1(object sender, EventArgs e)
        {
            
            string noWorkDir = "0", noWazara = "0", offNum = "0";


            using (SqlConnection con = new SqlConnection(ConenctTo104))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT no_work_dir, no_wazara, OffNum FROM iiinsertion WHERE nId = @nId", con);
                cmd.Parameters.AddWithValue("@nId", textEid.Text.Trim());

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())

                {
                    noWorkDir = reader["no_work_dir"].ToString();
                    noWazara = reader["no_wazara"].ToString();
                    offNum = reader["OffNum"].ToString();
                }
                reader.Close();
            }



            try
            {

                if (!comboCom.Items.Contains(comboOn.Text))
                {
                    MessageBox.Show( "الرجاء كتابة إسم المكـتب بشكل صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; 
                }

                if (!comboCom.Items.Contains(comboCom.Text))
                {
                    MessageBox.Show( "الرجاء كتابة الـوزارة بشكل صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!comboCom.Items.Contains(comboWcom.Text))
                {
                    MessageBox.Show("الرجاء كتابة جهــة الـعمل بشكل صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!comboCom.Items.Contains(comboPlace.Text))
                {
                    MessageBox.Show("الرجاء كتابة المنطقة بشكل صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!comboCom.Items.Contains(comboAs.Text))
                {
                    MessageBox.Show("الرجاء كتابة حـالة الإجراء بشكل صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!comboCom.Items.Contains(comboAT.Text))
                {
                    MessageBox.Show("الرجاء كتابة نـوع الإجراء بشكل صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!checkBox1.Checked)
                    textDef.Text = "لا يوجد";

                // إزالة الفراغات الزائدة من جميع الحقول
                textname.Text = System.Text.RegularExpressions.Regex.Replace(textname.Text.Trim(), @"\s+", " ");
                textMn.Text = System.Text.RegularExpressions.Regex.Replace(textMn.Text.Trim(), @"\s+", " ");
                //  if(checkBox1.Checked) textDef.Text = System.Text.RegularExpressions.Regex.Replace(textDef.Text.Trim(), @"\s+", " ");
                textRn.Text = System.Text.RegularExpressions.Regex.Replace(textRn.Text.Trim(), @"\s+", " ");

                textEid.Text = textEid.Text.Replace(" ", "").Trim();
                textMnn.Text = textMnn.Text.Replace(" ", "").Trim();


                // التحقق من تعبئة الحقول
                if (string.IsNullOrEmpty(textname.Text) || string.IsNullOrEmpty(textEid.Text) ||
                    string.IsNullOrEmpty(textMn.Text) || string.IsNullOrEmpty(comboCom.Text) ||
                    string.IsNullOrEmpty(comboWcom.Text) || string.IsNullOrEmpty(textFcom.Text) ||
                    string.IsNullOrEmpty(comboPlace.Text) || string.IsNullOrEmpty(comboAT.Text) ||
                    string.IsNullOrEmpty(comboAs.Text) || string.IsNullOrEmpty(textRn.Text) ||
                    comboDate.Value == null || string.IsNullOrEmpty(comboOn.Text) ||
                    string.IsNullOrEmpty(textMnn.Text))
                {
                    MessageBox.Show("يرجى تعبئة جميع الحقول!", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nationalId = textEid.Text.Replace(" ", "");

                // التحقق من صحة الإدخال للرقم الوطني
                if (!System.Text.RegularExpressions.Regex.IsMatch(nationalId, @"^[12]\d{11}$"))
                {
                    MessageBox.Show("الرجاء إدخال رقم وطني صحيح يبدأ بـ 1 أو 2 ويتكون من 12 رقمًا بدون فراغات.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                if (!System.Text.RegularExpressions.Regex.IsMatch(textMnn.Text, @"^\d+$"))
                {
                    MessageBox.Show("الرجاء إدخال أرقام صحيحة في خانة رقم المستند.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // التحقق من الحقول النصية بحيث تحتوي على أحرف عربية فقط
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

                // تجهيز DataTable للإدخال
                DataTable dtInsert = new DataTable();
                using (SqlConnection con = new SqlConnection(ConenctTo104))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("exec insertdataiiisertion @nId=@NId , @cus_name=@Cus_name, @mo_name=@Mo_name, @ministry=@Ministry, " +
                        "@no_wazara=@No_wazara, @old_work=@Old_work, @no_work_dir=@No_work_dir, @work_sub=@Work_sub, @region=@Region, @hala=@Hala, @n_hala=@N_hala," +
                        " @re_num=@Re_num,  @makteb=@Makteb, @OffNum=@offNum, @mo_num=@Mo_num,@def=@Def, @emp_name=@Emp_name," +
                        "  @Dofa=@dofa, @userId=@UserId, @re_date=@Re_date", con);

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@NId", (textEid.Text));
                    cmd.Parameters.AddWithValue("@Cus_name", textname.Text);
                    cmd.Parameters.AddWithValue("@Mo_name", textMn.Text);
                    cmd.Parameters.AddWithValue("@Ministry", comboCom.Text);
                    cmd.Parameters.AddWithValue("@No_wazara", comboCom.Text);
                    cmd.Parameters.AddWithValue("@Old_work", comboWcom.Text);
                    cmd.Parameters.AddWithValue("@No_work_dir", comboWcom.Text);
                    cmd.Parameters.AddWithValue("@Work_sub", textFcom.Text);
                    cmd.Parameters.AddWithValue("@Region", comboPlace.Text);
                    cmd.Parameters.AddWithValue("@Hala", comboAs.Text);
                    cmd.Parameters.AddWithValue("@N_hala", comboAT.Text);
                    cmd.Parameters.AddWithValue("@Re_num", textRn.Text);
                    cmd.Parameters.AddWithValue("@Re_date", comboDate.Value.ToShortDateString());
                    cmd.Parameters.AddWithValue("@Makteb", comboOn.Text);
                    cmd.Parameters.AddWithValue("@OffNum", comboOn.Text);
                    cmd.Parameters.AddWithValue("@Mo_num", (textMnn.Text));
                    cmd.Parameters.AddWithValue("@Def", textDef.Text);
                    cmd.Parameters.AddWithValue("@Emp_name", comboEn.Text);
                    //cmd.Parameters.AddWithValue("@en_date", DateTime.Now.ToString("dd/MM/yyyy"));
                    cmd.Parameters.AddWithValue("@dofa", 0);
                    cmd.Parameters.AddWithValue("@UserId", LoginForm.IdUser);


                    cmd.ExecuteNonQuery();
                    con.Close();
                    //SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM iiinsertion", con);
                    //SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                    //adapter.Fill(dtInsert);


                    //// إضافة سجل جديد
                    //DataRow newRow = dtInsert.NewRow();
                    //newRow["nId"] = long.Parse(textEid.Text);
                    //newRow["cus_name"] = textname.Text;
                    //newRow["mo_name"] = textMn.Text;
                    //newRow["ministry"] = comboCom.Text;
                    //newRow["old_work"] = comboWcom.Text;
                    //newRow["work_sub"] = textFcom.Text;
                    //newRow["region"] = comboPlace.Text;
                    //newRow["hala"] = comboAs.Text;
                    //newRow["n_hala"] = comboAT.Text;
                    //newRow["re_num"] = textRn.Text;
                    //newRow["re_date"] = comboDate.Value;
                    //newRow["makteb"] = comboOn.Text;
                    //newRow["mo_num"] = long.Parse(textMnn.Text);
                    //newRow["def"] = textDef.Text;
                    //newRow["emp_name"] = comboEn.Text;
                    //newRow["En_date"] = DateTime.Now.ToString("dd/MM/yyyy");
                    //newRow["Dofa"] = 0;
                    //newRow["no_work_dir"] = noWorkDir;
                    //newRow["no_wazara"] = noWazara;
                    //newRow["OffNum"] = offNum;
                    //newRow["userId"] = LoginForm.IdUser;

                    //dtInsert.Rows.Add(newRow);

                    //// تحديث قاعدة البيانات
                    //adapter.Update(dtInsert);
                }

                MessageBox.Show("تم الإدخال بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // تحديث `DataGridView`
                BindData();
                UpdateEntryCount();
                UpdateConfirmedEntryCount();

                ResetFields();
            }
            catch (FormatException ef)
            {
                MessageBox.Show("تأكد من إدخال القيم الرقمية بشكل صحيح." + ef.Message, "خطأ في التنسيق", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"حدث خطأ أثناء الإدخال: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateConfirmedEntryCount()
        {
          
            int confirmedCount = 0;

            using (SqlConnection con = new SqlConnection(ConenctTo104))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    $"SELECT COUNT(*) FROM iiinsertion WHERE emp_name = @user AND Confirm_User IS NOT NULL AND CAST(En_date AS DATE) = CAST(GETDATE() AS DATE) and Confirm_Status=1  and userId = {LoginForm.IdUser}  ", con);

                cmd.Parameters.AddWithValue("@user", LoggedInUserName); // استبدل currentUser باسم المستخدم الحالي

                confirmedCount = (int)cmd.ExecuteScalar(); // استرجاع العدد كعدد صحيح
            }

            lblCountV.Text = $" {confirmedCount}";
        }




        private void SearchData()
        {
            /*string noWorkDir = "0";
            string noWazara = "0";
            string offNum = "0";
            if (!string.IsNullOrEmpty(comboWcom.Text))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT no_work_dir FROM jehaa_name$ WHERE jeha_name = @jeha_name", con))
                {
                    cmd.Parameters.AddWithValue("@jeha_name", comboWcom.Text);
                    object result = cmd.ExecuteScalar();
                    noWorkDir = result != null ? result.ToString() : "0";
                }
            }

            if (!string.IsNullOrEmpty(comboCom.Text))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT no_wazara FROM Ministry$ WHERE wazara_name = @wazara_name", con))
                {
                    cmd.Parameters.AddWithValue("@wazara_name", comboCom.Text);
                    object result = cmd.ExecuteScalar();
                    noWazara = result != null ? result.ToString() : "0";
                }
            }

            if (!string.IsNullOrEmpty(comboOn.Text))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT OffNum FROM OffName$ WHERE OffName = @OffName", con))
                {
                    cmd.Parameters.AddWithValue("@OffName", comboOn.Text);
                    object result = cmd.ExecuteScalar();
                    offNum = result != null ? result.ToString() : "0";
                }
            }*/
            // التحقق من أنه تم تعبئة حقل واحد على الأقل

            /* cmd.Parameters.AddWithValue("@no_work_dir", noWorkDir);
             cmd.Parameters.AddWithValue("@no_wazara", noWazara);
             cmd.Parameters.AddWithValue("@OffNum", offNum);*/



            // إضافة الشروط الخاصة بـ no_work_dir و no_wazara و OffNum
            /*    cmd.Parameters.AddWithValue("@no_work_dir", noWorkDir);
                cmd.Parameters.AddWithValue("@no_wazara", noWazara);
                cmd.Parameters.AddWithValue("@OffNum", offNum);*/

            // تنفيذ الاستعلام وملء البيانات


            // تحديث الواجهة مع النتائج
            // إعلان المتغيرات

        }
        private void btnReset_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد حقًا مسح الحقول؟?", "رسالة التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                textname.Text =
                textEid.Text =
                textMn.Text = "";
                textFcom.ResetText();
                textRn.Clear();
                comboDate.ResetText();
                textMnn.Clear();
                textDef.Clear();


                comboCom.ResetText();
                comboWcom.ResetText();
                comboPlace.ResetText();
                comboAs.ResetText();
                comboAT.ResetText();
                comboOn.ResetText();



                textdate.Text = DateTime.Now.ToString();
            }


        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {

         //   string connectionString = $@"Data Source={ColConClasss.ServerName104};Initial Catalog={ColConClasss.DataBase104};Integrated Security={ColConClasss.Trusted_Connection104};";

            try
            {
                // تحقق من أن أحد الحقول على الأقل تمت تعبئته
                if (string.IsNullOrEmpty(textEid.Text) && string.IsNullOrEmpty(textname.Text))
                {
                    MessageBox.Show("يرجى تعبئة حقل واحد على الأقل للبحث.", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // إيقاف تنفيذ الكود إذا لم يتم تعبئة أي حقل
                }

                // استعلام البحث الأساسي
                string query = "SELECT cId,nId, cus_name, mo_name, ministry, old_work, work_sub, region, hala, n_hala, re_num, makteb, mo_num, def FROM iiinsertion WHERE 1=1";

                // قائمة المعلمات لتجنب الحقن SQL
                List<SqlParameter> parameters = new List<SqlParameter>();

                // إضافة الشروط بناءً على الحقول المعبأة
                if (!string.IsNullOrEmpty(textname.Text)) { query += " AND cus_name LIKE @cus_name"; parameters.Add(new SqlParameter("@cus_name", $"%{textname.Text}%")); }
                if (!string.IsNullOrEmpty(textEid.Text)) { query += " AND nId LIKE @nId"; parameters.Add(new SqlParameter("@nId", $"%{textEid.Text}%")); }
                if (!string.IsNullOrEmpty(textMn.Text)) { query += " AND mo_name LIKE @mo_name"; parameters.Add(new SqlParameter("@mo_name", $"%{textMn.Text}%")); }
                if (!string.IsNullOrEmpty(comboCom.Text)) { query += " AND ministry LIKE @ministry"; parameters.Add(new SqlParameter("@ministry", $"%{comboCom.Text}%")); }
                if (!string.IsNullOrEmpty(comboWcom.Text)) { query += " AND old_work LIKE @old_work"; parameters.Add(new SqlParameter("@old_work", $"%{comboWcom.Text}%")); }
                if (!string.IsNullOrEmpty(textFcom.Text)) { query += " AND work_sub LIKE @work_sub"; parameters.Add(new SqlParameter("@work_sub", $"%{textFcom.Text}%")); }
                if (!string.IsNullOrEmpty(comboPlace.Text)) { query += " AND region LIKE @region"; parameters.Add(new SqlParameter("@region", $"%{comboPlace.Text}%")); }
                if (!string.IsNullOrEmpty(comboAs.Text)) { query += " AND hala LIKE @hala"; parameters.Add(new SqlParameter("@hala", $"%{comboAs.Text}%")); }
                if (!string.IsNullOrEmpty(comboAT.Text)) { query += " AND n_hala LIKE @n_hala"; parameters.Add(new SqlParameter("@n_hala", $"%{comboAT.Text}%")); }
                if (!string.IsNullOrEmpty(textRn.Text)) { query += " AND re_num LIKE @re_num"; parameters.Add(new SqlParameter("@re_num", $"%{textRn.Text}%")); }
                if (!string.IsNullOrEmpty(comboOn.Text)) { query += " AND makteb LIKE @makteb"; parameters.Add(new SqlParameter("@makteb", $"%{comboOn.Text}%")); }
                if (!string.IsNullOrEmpty(textMnn.Text)) { query += " AND mo_num LIKE @mo_num"; parameters.Add(new SqlParameter("@mo_num", $"%{textMnn.Text}%")); }
                if (!string.IsNullOrEmpty(textDef.Text)) { query += " AND def LIKE @def"; parameters.Add(new SqlParameter("@def", $"%{textDef.Text}%")); }


                // إنشاء `SqlDataAdapter` لجلب البيانات من قاعدة البيانات
                using (SqlDataAdapter da = new SqlDataAdapter(query, ConenctTo104))
                {
                    // إضافة المعاملات إلى `SqlDataAdapter`
                    foreach (var param in parameters)
                    {
                        da.SelectCommand.Parameters.Add(param);
                    }

                    // تحميل البيانات في `DataSet`
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // عرض البيانات في DataGridView
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("لا توجد بيانات مطابقة للبحث.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            /* SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-KKP05IN\SQLEXPRESS01;Initial Catalog=DIC2025;Integrated Security=True;");
             con.Open();


             // التحقق من أنه تم تعبئة حقل واحد على الأقل
             if (string.IsNullOrEmpty(comboWcom.Text) &&
                 string.IsNullOrEmpty(comboCom.Text) &&
                 string.IsNullOrEmpty(comboOn.Text) &&
                 string.IsNullOrEmpty(textname.Text) &&
                 string.IsNullOrEmpty(textEid.Text) &&
                 string.IsNullOrEmpty(textMn.Text) &&
                 string.IsNullOrEmpty(textFcom.Text) &&
                 string.IsNullOrEmpty(textRn.Text) &&
                 comboDate.Value == null &&
                 string.IsNullOrEmpty(comboAT.Text) &&
                 string.IsNullOrEmpty(comboAs.Text) &&
                 string.IsNullOrEmpty(textMnn.Text) &&
                 string.IsNullOrEmpty(textDef.Text)) // التأكد من وجود حقل مملوء
             {
                 MessageBox.Show("يرجى تعبئة حقل واحد على الأقل للبحث.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return;
             }
             // بناء جملة SQL بناءً على الحقول المعبأة فقط
             string sql = "SELECT * FROM iiinsertion WHERE 1 = 1";

             // إضافة شروط البحث للحقول المعبأة
             if (!string.IsNullOrEmpty(textname.Text))
                 sql += " AND cus_name LIKE @cus_name";

             if (!string.IsNullOrEmpty(textEid.Text))
                 sql += " AND nId = @nId";

             if (!string.IsNullOrEmpty(textMn.Text))
                 sql += " AND mo_name LIKE @mo_name";

             if (!string.IsNullOrEmpty(comboCom.Text))
                 sql += " AND ministry = @ministry";

             if (!string.IsNullOrEmpty(comboWcom.Text))
                 sql += " AND old_work = @old_work";

             if (!string.IsNullOrEmpty(textFcom.Text))
                 sql += " AND work_sub LIKE @work_sub";

             if (!string.IsNullOrEmpty(comboPlace.Text))
                 sql += " AND region = @region";

             if (!string.IsNullOrEmpty(comboAs.Text))
                 sql += " AND hala = @hala";

             if (!string.IsNullOrEmpty(comboAT.Text))
                 sql += " AND n_hala = @n_hala";

             if (!string.IsNullOrEmpty(textRn.Text))
                 sql += " AND re_num LIKE @re_num";

             if (comboDate.Value != null)
                 sql += " AND re_date = @re_date";

             if (!string.IsNullOrEmpty(comboOn.Text))
                 sql += " AND makteb = @makteb";

             if (!string.IsNullOrEmpty(textMnn.Text))
                 sql += " AND mo_num = @mo_num";

             if (!string.IsNullOrEmpty(textDef.Text))
                 sql += " AND def LIKE @def";

             // إضافة القيم المعبأة إلى المعاملات
             SqlCommand command = new SqlCommand(sql, con);

             if (!string.IsNullOrEmpty(textname.Text))
                 command.Parameters.AddWithValue("@cus_name", "%" + textname.Text + "%");

             if (!string.IsNullOrEmpty(textEid.Text))
                 command.Parameters.AddWithValue("@nId", long.Parse(textEid.Text));

             if (!string.IsNullOrEmpty(textMn.Text))
                 command.Parameters.AddWithValue("@mo_name", "%" + textMn.Text + "%");

             if (!string.IsNullOrEmpty(comboCom.Text))
                 command.Parameters.AddWithValue("@ministry", comboCom.Text);

             if (!string.IsNullOrEmpty(comboWcom.Text))
                 command.Parameters.AddWithValue("@old_work", comboWcom.Text);

             if (!string.IsNullOrEmpty(textFcom.Text))
                 command.Parameters.AddWithValue("@work_sub", "%" + textFcom.Text + "%");

             if (!string.IsNullOrEmpty(comboPlace.Text))
                 command.Parameters.AddWithValue("@region", comboPlace.Text);

             if (!string.IsNullOrEmpty(comboAs.Text))
                 command.Parameters.AddWithValue("@hala", comboAs.Text);

             if (!string.IsNullOrEmpty(comboAT.Text))
                 command.Parameters.AddWithValue("@n_hala", comboAT.Text);

             if (!string.IsNullOrEmpty(textRn.Text))
                 command.Parameters.AddWithValue("@re_num", "%" + textRn.Text + "%");

             if (comboDate.Value != null)
                 command.Parameters.AddWithValue("@re_date", comboDate.Value.Date); // التأكد من المقارنة بالتاريخ فقط

             if (!string.IsNullOrEmpty(comboOn.Text))
                 command.Parameters.AddWithValue("@makteb", comboOn.Text);

             if (!string.IsNullOrEmpty(textMnn.Text))
                 command.Parameters.AddWithValue("@mo_num", long.Parse(textMnn.Text));

             if (!string.IsNullOrEmpty(textDef.Text))
                 command.Parameters.AddWithValue("@def", "%" + textDef.Text + "%");

             // تنفيذ الاستعلام
             try
             {
                 SqlDataAdapter adapter = new SqlDataAdapter(command);
                 DataTable dt = new DataTable();
                 adapter.Fill(dt);
                 dataGridView1.DataSource = dt; // عرض النتائج في DataGridView
             }
             catch (Exception ex)
             {
                 MessageBox.Show($"حدث خطأ أثناء البحث: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
             finally
             {
                 con.Close();
             }
            */
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            //BindData();
            ResetFields();
        }

        private void comboEn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // التأكد من إدخال الرقم الوطني
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("يرجى إدخال الرقم الوطني.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = $"Server={ColConClasss.ServerNameCol};Database={ColConClasss.DataBaseCol};User id={ColConClasss.UserCol};Password={ColConClasss.PasswordCol};Trusted_Connection= {ColConClasss.Trusted_ConnectionCol}";
            string query = "SELECT * FROM [373] WHERE text_id = @text_id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@text_id", textBox1.Text.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt; // عرض البيانات في DataGridView
                    }
                    else
                    {
                        MessageBox.Show("الرقم الوطني غير موجود.", "معلومة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.DataSource = null; // إفراغ الجدول في حالة عدم العثور على بيانات
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // التأكد من إدخال الرقم الوطني
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("يرجى إدخال الرقم الوطني للبحث.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT * FROM [373] WHERE text_id = @text_id";

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection("connectionString"))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    da.SelectCommand.Parameters.AddWithValue("@text_id", textBox1.Text.Trim());
                    da.Fill(dt);
                }
            }

            if (dt.Rows.Count > 0)
            {
                //  text_id, empname, mo_name, ministry, work_dir, work_sub, Region, edited, ekres, makteb, dofa


                // تعبئة الحقول بالبيانات المسترجعة
                DataRow row = dt.Rows[0];
                textEid.Text = row["text_id"].ToString();
                textname.Text = row["empname"].ToString();
                textMn.Text = row["mo_name"].ToString();
                comboCom.Text = row["ministry"].ToString();
                comboWcom.Text = row["work_dir"].ToString();
                textFcom.Text = row["work_sub"].ToString();
                comboPlace.Text = row["Region"].ToString();
                comboAs.Text = row["edited"].ToString();
                comboAT.Text = row["ekres"].ToString();
                //textRn.Text = row["re_num"].ToString();
                // comboDate.Value = Convert.ToDateTime(row["re_date"]);
                comboOn.Text = row["Region"].ToString();
                // textMnn.Text = row["mo_num"].ToString();
                //   textDef.Text = row["def"].ToString();
                //  comboEn.Text = row["emp_name"].ToString();

                MessageBox.Show("تم جلب البيانات بنجاح.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("لم يتم العثور على بيانات للرقم الوطني المدخل.", "معلومة", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // تفريغ الحقول عند عدم العثور على بيانات
                ClearFields();
            }
        }
        private void ClearFields()
        {
            textname.Text = "";
            textMn.Text = "";
            comboCom.Text = "";
            comboWcom.Text = "";
            textFcom.Text = "";
            comboPlace.Text = "";
            comboAs.Text = "";
            comboAT.Text = "";
            textRn.Text = "";
            comboDate.Value = DateTime.Now;
            comboOn.Text = "";
            textMnn.Text = "";
            textDef.Text = "";
            comboEn.Text = "";
        }

        private void lblCount_Click(object sender, EventArgs e)
        {

        }

        private void lblCountV_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(ConenctTo104))
                {


                    using (SqlDataAdapter da = new SqlDataAdapter($"SELECT * FROM iiinsertion WHERE  Confirm_User IS NOT NULL AND CAST(En_date AS DATE) = CAST(GETDATE() AS DATE) and Confirm_Status=1  and userId = {LoginForm.IdUser}", con))
                    {


                        //  da.SelectCommand.Parameters.AddWithValue("@user", LoggedInUserName);
                        da.Fill(dt);
                    }
                }

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء تحميل البيانات: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

          getCid();

        }



        private void getCid()
        {
            if (dataGridView1.SelectedRows.Count > 0) // التأكد من أن هناك صفًا محددًا
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0]; // الحصول على الصف الأول المحدد

                // جلب البيانات من العمود المطلوب باستخدام اسم العمود أو فهرسه
                string cid = row.Cells[0].Value.ToString(); // استبدل "اسم_العمود" بالاسم الفعلي


                using (SqlConnection con = new SqlConnection(ConenctTo104))
                {
                    string query = "select Confirm_Status from iiinsertion where iiinsertion.cId = @cid";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@cid", cid);

                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            if (string.IsNullOrEmpty(dt.Rows[0]["Confirm_Status"].ToString()))
                            {
                                button3.Enabled = true;
                                button3.Visible = true;
                            }
                            else
                            {
                                button3.Enabled = false;
                                button3.Visible = false;
                            }
                        }
                    }
                }

                // يمكنك الآن استخدام المتغير cid
                //    MessageBox.Show($"Cid المستخرج: {cid}", "معلومة", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // MessageBox.Show($"القيمة المستخرجة: {cid}", "معلومة", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("الرجاء تحديد صف أولًا!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        string CID = "";

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // التأكد من أن النقر تم على صف وليس على الهيدر
            {
                try
                {
                    // الحصول على CID من الصف المحدد
                    int selectedCID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["cId"].Value);
                    
                    // إظهار رسالة تأكيد
                    DialogResult result = MessageBox.Show("هل تريد جلب بيانات هذا الصف؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        CID = selectedCID.ToString();

                     //   string connectionString = @"Data Source=DESKTOP-KKP05IN\SQLEXPRESS01;Initial Catalog=DIC2025;Integrated Security=True;";
                        string query = @"
                SELECT CID,nId, cus_name, mo_name, ministry, old_work, work_sub, region, hala, n_hala, re_num, re_date, makteb, mo_num, def, 
                       emp_name, En_date
                FROM iiinsertion 
                WHERE CID = @cId";

                        using (SqlDataAdapter adapter = new SqlDataAdapter(query, ConenctTo104))
                        {
                            adapter.SelectCommand.Parameters.AddWithValue("@cId", selectedCID);

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

        private void button3_Click(object sender, EventArgs e)
        {

          //  string connectionString = @"Data Source=DESKTOP-KKP05IN\SQLEXPRESS01;Initial Catalog=DIC2025;Integrated Security=True;";

            try
            {
                using (SqlConnection con = new SqlConnection(ConenctTo104))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE iiinsertion SET cus_name=@Cus_name, mo_name=@Mo_name, ministry=@Ministry, " +
                        "old_work=@Old_work, work_sub=@Work_sub, region=@Region, hala=@Hala, n_hala=@N_hala, re_num=@Re_num, " +
                        "re_date=@Re_date, makteb=@Makteb, mo_num=@Mo_num, def=@Def, emp_name=@Emp_name " +
                        "WHERE CID=@CID", con);

                    // إضافة القيم من الحقول إلى الاستعلام
                    cmd.Parameters.AddWithValue("@CID", CID);
                    cmd.Parameters.AddWithValue("@Cus_name", textname.Text.Trim());
                    cmd.Parameters.AddWithValue("@Mo_name", textMn.Text.Trim());
                    cmd.Parameters.AddWithValue("@Ministry", comboCom.Text.Trim());
                    cmd.Parameters.AddWithValue("@Old_work", comboWcom.Text.Trim());
                    cmd.Parameters.AddWithValue("@Work_sub", textFcom.Text.Trim());
                    cmd.Parameters.AddWithValue("@Region", comboPlace.Text.Trim());
                    cmd.Parameters.AddWithValue("@Hala", comboAs.Text.Trim());
                    cmd.Parameters.AddWithValue("@N_hala", comboAT.Text.Trim());
                    cmd.Parameters.AddWithValue("@Re_num", textRn.Text.Trim());
                    cmd.Parameters.AddWithValue("@Re_date", comboDate.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Makteb", comboOn.Text.Trim());
                    cmd.Parameters.AddWithValue("@Mo_num", textMnn.Text.Trim());
                    cmd.Parameters.AddWithValue("@Def", textDef.Text.Trim());
                    cmd.Parameters.AddWithValue("@Emp_name", comboEn.Text.Trim());

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("تم التعديل بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BindData(); // تحديث البيانات في DataGridView
                        ResetFields(); // مسح الحقول بعد التعديل
                        WhoIsUpdated();
                    }
                    else
                    {
                        MessageBox.Show("لم يتم العثور على السجل المطلوب!", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء التعديل: " + ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void WhoIsUpdated()
        {
            string query = "SELECT cId,nId, cus_name, mo_name, ministry, old_work, work_sub, region, hala, n_hala, re_num, makteb, mo_num, def FROM iiinsertion WHERE cId= @CID";
          //  string connectionString = @"Data Source=DESKTOP-KKP05IN\SQLEXPRESS01;Initial Catalog=DIC2025;Integrated Security=True;";

            try
            {
                using (SqlConnection con = new SqlConnection(ConenctTo104))
                {
                    con.Open();
                    SqlCommand com=new SqlCommand(query, con);
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@CID", CID);
                    SqlDataReader dr =com.ExecuteReader();
                    DataTable data = new DataTable();
                    data.Load(dr);
                    dataGridView1.DataSource = data;
                    con.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textDef.Enabled = checkBox1.Checked;
        }
    }
    }
  