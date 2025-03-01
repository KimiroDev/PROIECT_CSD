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
    public partial class EntryEditForm : Form
    {
        public EntryEditForm(EntryData entry)
        {
            InitializeComponent();
            AcceptButton = btnApply;

            tbFilename.Text = entry.FileName;
            tbEncrypted.Text = entry.Encrypted;
            tbEncKey.Text = entry.EncryptionKey;
            tbDuration.Text = entry.Duration;
            tbAlg.Text = entry.EncryptionAlgorithm;
            tbPath.Text = entry.FileFullPath;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            // verificam daca datele sunt valide
            if (tbEncrypted.Text != "true" && tbEncrypted.Text != "false")
            {
                MessageBox.Show("'Encrypted' invalid value (expected true or false).", "Error");
                return;
            }
            if (!int.TryParse(tbDuration.Text, out int duration) && duration < 0)
            {
                MessageBox.Show("Duration is not a valid whole number.", "Error");
                return;
            }
            if (!IsValidFile(tbPath.Text))
            {
                MessageBox.Show("Path to file does not exist", "Error");
                return;
            }

            // preluam datele
            EntryData entry = new()
            {
                FileName = tbFilename.Text,
                Encrypted = tbEncrypted.Text,
                EncryptionKey = tbEncKey.Text,
                EncryptionAlgorithm = tbAlg.Text,
                Duration = tbDuration.Text,
                FileFullPath = tbPath.Text
            };

            // aplicam modificarea
            int result = Evenimente.Evenimente.EditEntry(entry);
            if (result != 0)
                MessageBox.Show($"File metadata could not be modified. Error code {result}.", "Error");
        }

        /// <summary>
        /// Verificam daca fisierul exista
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool IsValidFile(string path)
        {
            // vezi daca exista
            FileAttributes attributes;
            try
            {
                attributes = File.GetAttributes(path);
            }
            catch { return false; }

            switch (attributes)
            {
                // daca este folser, nu ne trebuie
                case FileAttributes.Directory:
                    return false;
                default:
                    if (File.Exists(path))
                        return true;
                    else
                        return false;
            }
        }
    }
}
