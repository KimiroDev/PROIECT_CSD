using PROIECT_CSD.Date;
using PROIECT_CSD.Interfata_Utilizator;

namespace PROIECT_CSD
{
    public partial class Overview_Form : Form, IOverview
    {
        public Overview_Form()
        {
            InitializeComponent();
        }

        private string User;
        private string UserType;

        private void Overview_Form_Load(object sender, EventArgs e)
        {
            LoginDialog loginDiag = new LoginDialog();
            Orchestrator._o = this;

            // amandoua apele ale functiei 'CenterToScreen' sunt necesare, deoarece pe mai
            // multe monitoare Screen.GetBounds nu este initial setat pe monitorul corect
            CenterToScreen();

            // Centram fereastra
            Rectangle screen = Screen.GetBounds(this);
            Size = new((int)(screen.Width * 0.9d), (int)(screen.Height * 0.8d));

            // amandoua apele ale functiei 'CenterToScreen' sunt necesare, deoarece pe mai
            // multe monitoare Screen.GetBounds nu este initial setat pe monitorul corect
            CenterToScreen();

            // Do Login
            if (loginDiag.ShowDialog(this) != DialogResult.OK) Close();

            User = loginDiag.User;
            UserType = loginDiag.UserType;
            Text = User + "'s files";

            // Display user info
            UserNameLabel.Text = User;
            UserTypeLabel.Text = UserType == "admin" ? "Admin" : "Regular user";

            // Display entries (files)
            RefreshListItems();

            loginDiag.Dispose();


            // 
            Evenimente.Evenimente.GenTESTdatabase();
        }

        /// <summary>
        /// Ia date despre fisierele utilizatorului din baza de date si le afiseaza pe ecran.
        /// </summary>
        void IOverview.Refresh() => RefreshListItems();

        /// <summary>
        /// Ia date despre fisierele utilizatorului din baza de date si le afiseaza pe ecran.
        /// </summary>
        private void RefreshListItems()
        {
            // stergem itemele existente
            EntryList.Items.Clear();
            List<EntryData> files = Evenimente.Evenimente.GetFilesFromDatabase(User);
            List<ListViewItem> items = [];

            foreach (EntryData file in files)
            {
                items.Add(new ListViewItem(new string[] {
                    file.FileName, file.Encrypted, file.EncryptionKey,
                    file.EncryptionAlgorithm, file.Duration, file.FileFullPath
                }));
            }

            EntryList.Items.AddRange([.. items]);
        }

        /// <summary>
        /// Adaugare fisier nou.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddFileButton_Click(object sender, EventArgs e)
        {
            Evenimente.Evenimente.AddNewFile();
        }

        /// <summary>
        /// Aratare dialog de criptare.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EncryptButton_Click(object? sender, EventArgs e)
        {
            if (EntryList.SelectedItems.Count != 1) return;

            ListViewItem item = EntryList.SelectedItems[0];

            EntryData selectedfile = new()
            {
                FileName = item.SubItems[0].Text,
                Encrypted = item.SubItems[1].Text,
                EncryptionKey = item.SubItems[2].Text,
                EncryptionAlgorithm = item.SubItems[3].Text,
                Duration = item.SubItems[4].Text,
                FileFullPath = item.SubItems[5].Text,
            };

            EncryptForm encryptForm = new();
            if (encryptForm.ShowDialog() == DialogResult.OK)
            {
                Refresh();
            }
        }

        /// <summary>
        /// Apelare functie de decriptare.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecryptButton_Click(object? sender, EventArgs e)
        {
            if (EntryList.SelectedItems.Count != 1) return;

            ListViewItem item = EntryList.SelectedItems[0];

            EntryData selectedfile = new()
            {
                FileName = item.SubItems[0].Text,
                Encrypted = item.SubItems[1].Text,
                EncryptionKey = item.SubItems[2].Text,
                EncryptionAlgorithm = item.SubItems[3].Text,
                Duration = item.SubItems[4].Text,
                FileFullPath = item.SubItems[5].Text,
            };

            Evenimente.Evenimente.DecryptButtonPressed(selectedfile);

            Refresh();
        }

        /// <summary>
        /// Daca niciun item/mai multe iteme sunt selectate nu putem face actiuni.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EntryList.SelectedItems.Count != 1)
            {
                ModifyButton.Enabled = false;
                EncryptButton.Enabled = false;
                DeleteButton.Enabled = false;
            }
            else
            {
                ModifyButton.Enabled = true;
                EncryptButton.Enabled = true;
                DeleteButton.Enabled = true;

                if (EntryList.SelectedItems[0].SubItems[1].Text == "true")
                {
                    EncryptButton.Text = "Decrypt";
                    EncryptButton.Click -= EncryptButton_Click;
                    EncryptButton.Click += DecryptButton_Click;
                }
                else
                {
                    EncryptButton.Text = "Encrypt";
                    EncryptButton.Click += EncryptButton_Click;
                    EncryptButton.Click -= DecryptButton_Click;
                }
            }
        }

        /// <summary>
        /// Afisare dialofg de modificare entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModifyButton_Click(object sender, EventArgs e)
        { 
            if (EntryList.SelectedItems.Count != 1) return;

            ListViewItem item = EntryList.SelectedItems[0];

            EntryData selectedfile = new()
            {
                FileName = item.SubItems[0].Text,
                Encrypted = item.SubItems[1].Text,
                EncryptionKey = item.SubItems[2].Text,
                EncryptionAlgorithm = item.SubItems[3].Text,
                Duration = item.SubItems[4].Text,
                FileFullPath = item.SubItems[5].Text,
            };

            EntryEditForm editform = new(selectedfile);
            editform.ShowDialog();
            RefreshListItems();
        }

        /// <summary>
        /// Stergere de fisier.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            // item ul selectat
            ListViewItem item = EntryList.SelectedItems[0];

            // colectam datele despre fisierul pe care il vrem sters
            EntryData selectedfile = new()
            {
                FileName = item.SubItems[0].Text,
                Encrypted = item.SubItems[1].Text,
                EncryptionKey = item.SubItems[2].Text,
                EncryptionAlgorithm = item.SubItems[3].Text,
                Duration = item.SubItems[4].Text,
                FileFullPath = item.SubItems[5].Text,
            };

            // prompt pentru confirmare stergere
            DialogResult res = MessageBox.Show(
                "Are you sure you want to delete '" + selectedfile.FileName + "'?", "", MessageBoxButtons.YesNo
            );

            // incercam sa stergem fisierul
            if (res == DialogResult.Yes)
            {
                if (!Evenimente.Evenimente.DeleteFile(selectedfile))
                    MessageBox.Show("Could not delete file", "Error");
            }
        }

        void IOverview.AddTempEntry(EntryData entry)
        {
            ListViewItem item = new(entry.FileName);
            item.SubItems.Add(entry.Encrypted);
            item.SubItems.Add(entry.EncryptionKey);
            item.SubItems.Add(entry.EncryptionAlgorithm);
            item.SubItems.Add(entry.Duration);
            item.SubItems.Add(entry.FileFullPath);

            EntryList.Items.Add(item);
        }
    }
}
