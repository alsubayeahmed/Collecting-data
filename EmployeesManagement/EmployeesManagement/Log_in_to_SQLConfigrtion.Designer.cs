namespace EmployeesManagement
{
    partial class Log_in_to_SQLConfigrtion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.userName = new System.Windows.Forms.TextBox();
            this.Pas = new System.Windows.Forms.TextBox();
            this.UserNameLable = new System.Windows.Forms.Label();
            this.PasswordLable = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userName
            // 
            this.userName.Font = new System.Drawing.Font("Tahoma", 12F);
            this.userName.Location = new System.Drawing.Point(36, 66);
            this.userName.Name = "userName";
            this.userName.PasswordChar = '*';
            this.userName.Size = new System.Drawing.Size(288, 32);
            this.userName.TabIndex = 0;
            // 
            // Pas
            // 
            this.Pas.Font = new System.Drawing.Font("Tahoma", 12F);
            this.Pas.Location = new System.Drawing.Point(36, 105);
            this.Pas.Name = "Pas";
            this.Pas.PasswordChar = '*';
            this.Pas.Size = new System.Drawing.Size(288, 32);
            this.Pas.TabIndex = 1;
            // 
            // UserNameLable
            // 
            this.UserNameLable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UserNameLable.AutoSize = true;
            this.UserNameLable.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserNameLable.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.UserNameLable.Location = new System.Drawing.Point(331, 67);
            this.UserNameLable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UserNameLable.Name = "UserNameLable";
            this.UserNameLable.Size = new System.Drawing.Size(156, 31);
            this.UserNameLable.TabIndex = 3;
            this.UserNameLable.Text = ":إسـم المـستخـدم";
            // 
            // PasswordLable
            // 
            this.PasswordLable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PasswordLable.AutoSize = true;
            this.PasswordLable.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordLable.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.PasswordLable.Location = new System.Drawing.Point(350, 106);
            this.PasswordLable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PasswordLable.Name = "PasswordLable";
            this.PasswordLable.Size = new System.Drawing.Size(137, 31);
            this.PasswordLable.TabIndex = 4;
            this.PasswordLable.Text = ":كلمـة المـرور";
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogin.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLogin.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.SystemColors.Control;
            this.btnLogin.Location = new System.Drawing.Point(149, 144);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(175, 44);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "تـسجيـل الـدخـول";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // Log_in_to_SQLConfigrtion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 201);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.PasswordLable);
            this.Controls.Add(this.UserNameLable);
            this.Controls.Add(this.Pas);
            this.Controls.Add(this.userName);
            this.Name = "Log_in_to_SQLConfigrtion";
            this.Text = "Log_in_to_SQLConfigrtion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.TextBox Pas;
        private System.Windows.Forms.Label UserNameLable;
        private System.Windows.Forms.Label PasswordLable;
        private System.Windows.Forms.Button btnLogin;
    }
}