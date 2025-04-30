using PROIECT_CSD.Date;
using PROIECT_CSD.Interfata_Utilizator;
using System.Diagnostics;

namespace PROIECT_CSD
{
    public partial class Overview_Form : Form
    {
        public Overview_Form(string username, string usertype)
        {
            InitializeComponent();

            // amandoua apele ale functiei 'CenterToScreen' sunt necesare, deoarece pe mai
            // multe monitoare Screen.GetBounds nu este initial setat pe monitorul corect
            CenterToScreen();

            // Centram fereastra
            Rectangle screen = Screen.GetBounds(this);
            Size = new((int)(screen.Width * 0.9d), (int)(screen.Height * 0.8d));

            // amandoua apele ale functiei 'CenterToScreen' sunt necesare, deoarece pe mai
            // multe monitoare Screen.GetBounds nu este initial setat pe monitorul corect
            CenterToScreen();

            // afis info utilizator
            User = username;
            UserType = usertype;
            UserNameLabel.Text = User;
            UserTypeLabel.Text = UserType == "admin" ? "Admin" : "Regular user";
            Text = User + "'s files";

            // genereaza si afiseaza fisiere exemplu
            // (mai incolo doar afisare)
            // Evenimente.Evenimente.GenTESTdatabase();
            RefreshListItems();

            DialogResult = DialogResult.Continue;
        }

        private static string User;
        private string UserType;
        
        /// <summary>
        /// Ofera numele utilizatorului
        /// </summary>
        /// <returns></returns>
        public static string getUser()
        {
             return User;
        }

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
            OpenFileDialog dialog = new();
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
            {
                Evenimente.Evenimente.AddNewFile(dialog.FileName, User);
                RefreshListItems();
            }
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

            EncryptForm encryptForm = new(selectedfile);
            Debug.WriteLine("new encrypt form based on " + selectedfile.FileName);
            
            if (encryptForm.ShowDialog() == DialogResult.OK)
            {
                Debug.WriteLine("am criptat un fisier");
                RefreshListItems();
            }
            
            if(!encryptForm.Visible)
            {
                Debug.WriteLine("gata encryptia");
                RefreshListItems();
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

            int result = Evenimente.Evenimente.DecryptButtonPressed(selectedfile);

            if (result != 0)
                MessageBox.Show($"Decryption was unsuccessful. Error code {result}", "Error");

            RefreshListItems();
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
                    EncryptButton.Click -= DecryptButton_Click;
                    EncryptButton.Click += DecryptButton_Click;
                }
                else
                {
                    EncryptButton.Text = "Encrypt";
                    EncryptButton.Click -= EncryptButton_Click;
                    EncryptButton.Click -= DecryptButton_Click;
                    EncryptButton.Click += EncryptButton_Click;
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
    }
}
