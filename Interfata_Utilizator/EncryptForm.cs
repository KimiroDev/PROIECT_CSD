using PROIECT_CSD.Date;
using System.Security.Cryptography;

namespace PROIECT_CSD.Interfata_Utilizator
{
    public partial class EncryptForm : Form
    {
        EntryData FileData;
        public EncryptForm(EntryData file)
        {
            InitializeComponent();
            CenterToParent();
            FileData = file;
        }

        /// <summary>
        /// Trimite datele despre fisier si cum trebuie sa fie criptat la
        /// functia care face criptarea.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EncryptButton_Click(object sender, EventArgs e)
        {
            if (EncryptButton.Text == "Close")
            {
                Close();
            }
            else
            {
                int result = Evenimente.Evenimente.EncryptButtonPressed(FileData, comboBox1.Text, textBox1.Text);
                if (result != 0)
                    MessageBox.Show($"Encryption was unsuccessful. Error code {result}", "Error");

                else
                {
                    Text = "Encrypt File - Success";
                    EncryptButton.Text = "Close";
                }
            }
        }

        private void EncryptForm_Load(object sender, EventArgs e)
        {
            ImageList imageList = new ImageList();
            imageList.Images.Add("ok", Properties.Resources.rand);
            button1.ImageList = imageList;
            button1.ImageKey = "ok";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is null)
            {
                MessageBox.Show("Selectati un algoritm de criptare.", "Error");
                return;
            }
            else
            if (comboBox1.SelectedItem.ToString() == "AES-128")
                textBox1.Text = GenerateRandomString(16);
            else if (comboBox1.SelectedItem.ToString() == "RSA")
                textBox1.Text = GenerateRandomString(256);
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] data = new byte[length];
                rng.GetBytes(data);
                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[data[i] % chars.Length];
                }
            }
            return new string(stringChars);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is not null) button1.Enabled = true;
            else button1.Enabled = false;
        }
    }
}
