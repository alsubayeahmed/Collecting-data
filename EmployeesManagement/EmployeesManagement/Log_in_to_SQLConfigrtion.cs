using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeesManagement
{
    public partial class Log_in_to_SQLConfigrtion : Form
    {
        public Log_in_to_SQLConfigrtion()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            List<string> users = new List<string>
            {
                "U1 asdf",
                "A1 fghj",
                "R2 fjddj"

            };

            for (int i = 0; i < users.Count; i++) { 
            string user = users[i].Split(' ')[0];
            string pas = users[i].Split(' ')[1];
            
            if(user ==userName.Text &&  pas == Pas.Text)
                {
                   DataBaseConfigrtion dataBaseConfigrtion = new DataBaseConfigrtion();
                    dataBaseConfigrtion.ShowDialog();
                }
            }

            
        }
    }
}
