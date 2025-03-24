using PROIECT_CSD.Date;

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
}
