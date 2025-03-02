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
    public partial class NewUserPrompt : Form
    {
        public NewUserPrompt()
        {
            InitializeComponent();
            CenterToParent();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            int result = Evenimente.Evenimente.AddUser(new Date.UserData()
            {
                id = 0,
                username = UsernameTextBox.Text,
                passwordhash = PasswordTextBox.Text,
                isAdmin = IsAdminCheckBox.Checked,
            });

            if (result != 0)
                MessageBox.Show($"Could not add user. Message error {result}.", "Error");

            Close();
        }
    }
}
