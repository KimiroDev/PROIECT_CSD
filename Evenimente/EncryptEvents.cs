using PROIECT_CSD.Date;
using System.Security.Cryptography;
using System.Text;


namespace PROIECT_CSD.Evenimente
{
    /// <summary>
    /// Evenimente care vin de la prompt ul de adaugare entry-uri (fisiere).
    /// </summary>
    static public partial class Evenimente
    {
        /// <summary>
        /// S-a apasat butonul de adaugare din prompt-ul de adaugare entry.
        /// </summary>
        /// <param name="item">Entry- ul de adaugat in baza de date sau idk unde.</param>
        /// <returns>true daca s-a putut adauga, false altfel</returns>
        static public int EncryptButtonPressed(EntryData item, string alg, string key)
        {
            //trb lui victor
            switch(alg)
            {
                case "AES-128":
                    Aes myAES = Aes.Create();
                    MessageBox.Show("Cica criptez fisier cu aes-128 si key = "+key, "Treaba lui Victor");
                    return 0;
                    break;
            }
            return -1;
        }

        static public string SHA256EncryptPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                //trebuie trecut din string in bytes pentru functia de hash
                //si dupa inapoi in string pentru punerea in baza de date
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }

        }

        /// <summary>
        /// S-a apasat butonul de adaugare din prompt-ul de adaugare entry.
        /// </summary>
        /// <param name="item">Entry- ul de adaugat in baza de date sau idk unde.</param>
        /// <returns>true daca s-a putut adauga, false altfel</returns>
        static public int DecryptButtonPressed(EntryData item)
        {
            return -1;
        }
    }
}
