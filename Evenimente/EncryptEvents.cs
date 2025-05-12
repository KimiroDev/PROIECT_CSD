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
            switch (alg)
            {
                case "AES-128":
                    DateTime time = DateTime.Now;

                    if (key.Length != 16)
                    {
                        MessageBox.Show("Key length must be 16 characters.", "Key length error!");
                        return -1;
                    }

                    Aes myAES = Aes.Create();
                    byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                    myAES.Key = keyBytes;
                    myAES.GenerateIV();

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

                    string base64Key = Convert.ToBase64String(myAES.Key);

                    EntryData encryptedEntryData = new EntryData();
                    encryptedEntryData.Encrypted = "true";
                    encryptedEntryData.EncryptionAlgorithm = "AES-128";
                    encryptedEntryData.EncryptionKey = base64Key;
                    encryptedEntryData.FileFullPath = outputFullPath;
                    encryptedEntryData.FileName = Path.GetFileName(outputPath);
                    encryptedEntryData.Duration = (DateTime.Now - time).TotalSeconds.ToString();

                    AddNewFile(encryptedEntryData, Overview_Form.getUser());

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
            switch (item.EncryptionAlgorithm)
            {
                case "AES-128":
                    Aes myAES = Aes.Create();

                    byte[] keyBytes;
                    try
                    {
                        keyBytes = Convert.FromBase64String(item.EncryptionKey);
                    }
                    catch
                    {
                        MessageBox.Show("Invalid encryption key format (must be base64).", "Key error");
                        return -1;
                    }

                    myAES.Key = keyBytes;

                    using (FileStream inputFileStream = new FileStream(item.FileFullPath, FileMode.Open, FileAccess.Read))
                    {
                        // Read the IV (first 16 bytes)
                        byte[] iv = new byte[16];
                        inputFileStream.Read(iv, 0, 16);
                        myAES.IV = iv;

                        string decryptedPath = item.FileFullPath.Replace(".encrypted", ".decrypted");

                        using (FileStream outputFileStream = new FileStream(decryptedPath, FileMode.Create, FileAccess.Write))
                        using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, myAES.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            inputFileStream.CopyTo(cryptoStream);
                        }
                    }

                    return 0;

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
