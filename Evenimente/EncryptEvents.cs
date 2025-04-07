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
                    //trebuie iv generat 
                    //key si iv trebuie stocate in baza de date dar vazute doar de admini
                    if(key.Length!=16)
                    {
                        MessageBox.Show("Key length must be 16 characters.","Key length error!");
                        return -1;
                    }
                    Aes myAES = Aes.Create();
                    byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                    myAES.Key = keyBytes;
                    RandomNumberGenerator rng = RandomNumberGenerator.Create();
                    rng.GetBytes(myAES.IV);
                    string outputPath = item.FileFullPath.Substring(item.FileFullPath.LastIndexOf('/')+1);
                    using (FileStream inputFileStream = new FileStream(item.FileFullPath, FileMode.Open, FileAccess.Read))
                    using (FileStream outputFileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, myAES.CreateEncryptor(), CryptoStreamMode.Write))
                            inputFileStream.CopyTo(cryptoStream);
                    MessageBox.Show("Cica criptez fisier cu aes-128 \nkey = " + key + " \nkey in bytes : " + keyBytes.ToString() + "\niv = " + myAES.IV.ToString(), "Treaba lui Victor");

                    return 0;
                    break;
            }
            return -1;
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

    }
}
