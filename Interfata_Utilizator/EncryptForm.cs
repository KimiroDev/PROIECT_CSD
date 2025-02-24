using PROIECT_CSD.Date;

namespace PROIECT_CSD.Interfata_Utilizator
{
    public partial class EncryptForm : Form
    {
        EntryData FileData;
        public EncryptForm()
        {
            InitializeComponent();
            CenterToParent();
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

            if (Evenimente.Evenimente.EncryptButtonPressed(FileData, AlgLabel.Text, comboBox1.Text))
            {
                Text = "Encrypt File - Success";
                EncryptButton.Text = "Close";
            }
            else MessageBox.Show("An error has ocurred.", "Error");
        }
    }
}
