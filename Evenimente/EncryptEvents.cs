using Microsoft.VisualBasic.ApplicationServices;
using PROIECT_CSD.Date;
using PROIECT_CSD.Interfata_Utilizator;
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
                    DateTime time = DateTime.Now;
                    //trebuie iv generat 
                    //key si iv trebuie stocate in baza de date dar vazute doar de admini
                    if (key.Length!=16)
                    {
                        MessageBox.Show("Key length must be 16 characters.","Key length error!");
                        return -1;
                    }
                    Aes myAES = Aes.Create();
                    byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                    myAES.Key = keyBytes;
                    myAES.GenerateIV();
                    //RandomNumberGenerator rng = RandomNumberGenerator.Create();
                    //rng.GetBytes(myAES.IV);
                    string fileName = Path.GetFileName(item.FileFullPath);
                    string outputPath = fileName + ".encrypted";
                    string outputFullPath = item.FileFullPath + ".encrypted";
                    using (FileStream inputFileStream = new FileStream(item.FileFullPath, FileMode.Open, FileAccess.Read))
                    using (FileStream outputFileStream = new FileStream(outputFullPath, FileMode.Create, FileAccess.Write))
                    {
                        outputFileStream.Write(myAES.IV, 0, myAES.IV.Length);
                        using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, myAES.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            inputFileStream.CopyTo(cryptoStream);
                        }
                    }
                    
                    //MessageBox.Show("Cica criptez fisier cu aes-128 \nkey = " + key + " \nkey in bytes : " + keyBytes.ToString() + "\niv = " + myAES.IV.ToString(), "Treaba lui Victor");
                    item.EncryptionAlgorithm = "AES-128";
                    StringBuilder sb = new StringBuilder();
                    foreach (byte b in myAES.Key)
                    {
                        sb.Append(b.ToString("x2"));
                    }
                    item.EncryptionKey = sb.ToString();
                    EntryData encryptedEntryData = new EntryData();
                    encryptedEntryData.Encrypted = "true";
                    encryptedEntryData.EncryptionAlgorithm = "AES-128";
                    encryptedEntryData.EncryptionKey = sb.ToString();
                    encryptedEntryData.FileFullPath = outputFullPath;
                    encryptedEntryData.FileName = Path.GetFileName(outputPath);
                    encryptedEntryData.Duration = (DateTime.Now - time).TotalSeconds.ToString();
                    AddNewFile(encryptedEntryData,Overview_Form.getUser());


                    return 0;

                default:
                    return -1;
            }
        }

        /// <summary>
        /// S-a apasat butonul de adaugare din prompt-ul de adaugare entry.
        /// </summary>
        /// <param name="item">Entry- ul de adaugat in baza de date sau idk unde.</param>
        /// <returns>true daca s-a putut adauga, false altfel</returns>
        static public int DecryptButtonPressed(EntryData item)
        {
            switch(item.EncryptionAlgorithm)
            {
                case ("AES-128"):
                    Aes myAES = Aes.Create();
                    using (FileStream inputFileStream = new FileStream(item.FileFullPath + ".encrypted", FileMode.Open, FileAccess.Read))
                    {
                        // Read the IV from the file (first 16 bytes)
                        byte[] iv = new byte[16];
                        inputFileStream.Read(iv, 0, 16);
                        myAES.IV = iv;
                        using (FileStream outputFileStream = new FileStream(item.FileFullPath.Split('.')[0] + ".decrypted", FileMode.Create, FileAccess.Write))
                        using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, myAES.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            inputFileStream.CopyTo(cryptoStream);
                        }
                    }
                    return 0;
                    break;
                default:
                    return -1;
            }
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
