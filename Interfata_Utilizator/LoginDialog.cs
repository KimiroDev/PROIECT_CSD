using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROIECT_CSD.Interfata_Utilizator
{
    public partial class LoginDialog : Form
    {
        public string User;
        public string UserType;

        public LoginDialog()
        {
            User = "null";
            UserType = "null";
            InitializeComponent();
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\");
            dbPath = Path.Combine(dbPath, "hello.db");
            if (!Directory.Exists(dbPath))
            {
                Evenimente.Evenimente.GenTESTdatabase();
                Evenimente.Evenimente.TEST_AdaugaUtilizatoriRandom();
            }
            CenterToParent();
            AcceptButton = LoginButton;
            
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            (string uname, string utype) user = Evenimente.Evenimente.LoginUser(LoginTextBox.Text, PasswordTextBox.Text);

            if (user.Item2 == "null")
            {
                FailedLoginLabel.Visible = true;
            }
            else
            {
                User = user.uname;
                UserType = user.utype;

                LoginTextBox.Text = "";
                PasswordTextBox.Text = "";

                if (UserType == "regular")
                {
                    Hide();
                    Overview_Form newform = new(User, UserType);
                    newform.ShowDialog();

                    if (newform.DialogResult == DialogResult.Cancel)
                        Close();
                    else
                        Show();
                }
                else
                {
                    Hide();
                    AdminForm newform = new(User, UserType);
                    newform.ShowDialog();

                    if (newform.DialogResult == DialogResult.Cancel)
                        Close();
                    else 
                        Show();
                }
                //DialogResult = DialogResult.OK;
                //Close();
            }
        }
    }
}
