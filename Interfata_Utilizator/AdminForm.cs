using Microsoft.VisualBasic.ApplicationServices;
using PROIECT_CSD.Date;
using System.ComponentModel.Design;

namespace PROIECT_CSD.Interfata_Utilizator
{
    public partial class AdminForm : Form
    {
        private string UserType;
        private string UserName;

        public AdminForm(string username, string usertype)
        {
            InitializeComponent();

            // amandoua apele ale functiei 'CenterToScreen' sunt necesare, deoarece pe mai
            // multe monitoare Screen.GetBounds nu este initial setat pe monitorul corect
            CenterToScreen();


            // Centram fereastra
            Rectangle screen = Screen.GetBounds(this);
            Size = new((int)(screen.Width * 0.7d), (int)(screen.Height * 0.5d));

            // amandoua apele ale functiei 'CenterToScreen' sunt necesare, deoarece pe mai
            // multe monitoare Screen.GetBounds nu este initial setat pe monitorul corect
            CenterToScreen();

            // afis info utilizator
            UserName = username;
            UserType = usertype;
            UserNameLabel.Text = UserName;
            UserTypeLabel.Text = UserType == "admin" ? "Admin" : "Regular user";
            Text = UserName + "'s files";

            DialogResult = DialogResult.Continue;
            RefreshListItems();
        }

        /// <summary>
        /// Ia date despre fisierele utilizatorului din baza de date si le afiseaza pe ecran.
        /// </summary>
        private void RefreshListItems()
        {
            // stergem itemele existente
            UserList.Items.Clear();
            List<UserData> users = Evenimente.Evenimente.GetUsersFromDatabase();
            List<ListViewItem> items = [];

            foreach (UserData user in users)
            {
                items.Add(new ListViewItem([
                    user.id.ToString(), user.isAdmin.ToString(), user.username, user.passwordhash
                ]));
            }

            UserList.Items.AddRange([.. items]);
        }

        /// <summary>
        /// Logout event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Continue;
            Close();
        }

        private void BtnAddUser_Click(object sender, EventArgs e)
        {
            NewUserPrompt prompt = new();

            // adaugarea si mesajul de eroare se intampla in form
            prompt.ShowDialog();
            RefreshListItems();
        }

        private void UserList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UserList.SelectedItems.Count != 1)
            {
                btnEditUser.Enabled = false;
                btnRemoveUser.Enabled = false;
            }
            else
            {
                btnEditUser.Enabled = true;
                btnRemoveUser.Enabled = true;
            }
        }

        private void BtnEditUser_Click(object sender, EventArgs e)
        {
            UserEditForm editform = new(new UserData() { 
                //username = UserList.Items. DE ADAUGAT
            });
        }
    }
}
