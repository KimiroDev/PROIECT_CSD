using PROIECT_CSD.Date;
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
    public partial class UserEditForm : Form
    {
        UserData User;

        public UserEditForm(UserData user)
        {
            InitializeComponent();

            UsernameTextBox.Text = user.username;
            IsAdminCheckBox.Checked = user.isAdmin;

            User = user;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            int result = Evenimente.Evenimente.EditUser(new Date.UserData()
            {
                id = 0,
                username = UsernameTextBox.Text,
                passwordhash = PasswordTextBox.Text,
                isAdmin = IsAdminCheckBox.Checked,
            });

            if (result != 0)
                MessageBox.Show($"Could not edit user. Message error {result}.", "Error");

            Close();
        }
    }
}
