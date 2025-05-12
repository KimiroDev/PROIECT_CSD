using Microsoft.VisualBasic.ApplicationServices;
using PROIECT_CSD.Date;
using PROIECT_CSD.Interfata_Utilizator;
using System.Security.Cryptography;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;


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

                case "RSA":
                    DateTime rsaTime = DateTime.Now;

                    // Generate a new RSA key pair
                    using (RSA rsa = RSA.Create(2048))
                    {
                        RSAParameters publicKey = rsa.ExportParameters(false);
                        RSAParameters privateKey = rsa.ExportParameters(true);

                        string privateKeyBase64 = Convert.ToBase64String(rsa.ExportRSAPrivateKey());

                        byte[] fileBytes = File.ReadAllBytes(item.FileFullPath);
                        byte[] encryptedBytes;

                        try
                        {
                            encryptedBytes = rsa.Encrypt(fileBytes, RSAEncryptionPadding.OaepSHA256);
                        }
                        catch (CryptographicException)
                        {
                            MessageBox.Show("File too large to encrypt with RSA directly. Use hybrid encryption.", "RSA Error");
                            return -1;
                        }

                        string outputPathRSA = item.FileFullPath + ".encrypted";

                        File.WriteAllBytes(outputPathRSA, encryptedBytes);

                        EntryData rsaEntryData = new EntryData();
                        rsaEntryData.Encrypted = "true";
                        rsaEntryData.EncryptionAlgorithm = "RSA";
                        rsaEntryData.EncryptionKey = privateKeyBase64; 
                        rsaEntryData.FileFullPath = outputPathRSA;
                        rsaEntryData.FileName = Path.GetFileName(outputPathRSA);
                        rsaEntryData.Duration = (DateTime.Now - rsaTime).TotalSeconds.ToString();

                        AddNewFile(rsaEntryData, Overview_Form.getUser());

                        return 0;
                    }

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

                    DateTime time = DateTime.Now;
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


                    string decryptedPath = item.FileFullPath.Substring(0, item.FileFullPath.LastIndexOf(".encrypted")) + ".decrypted";

                    using (FileStream inputFileStream = new FileStream(item.FileFullPath, FileMode.Open, FileAccess.Read))
                    {
                        // Read the IV (first 16 bytes)
                        byte[] iv = new byte[16];
                        inputFileStream.Read(iv, 0, 16);
                        myAES.IV = iv;


                        using (FileStream outputFileStream = new FileStream(decryptedPath, FileMode.Create, FileAccess.Write))
                        using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, myAES.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            inputFileStream.CopyTo(cryptoStream);
                        }
                    }
                    EntryData decryptedEntryData = new EntryData();
                    decryptedEntryData.Encrypted = "false";
                    decryptedEntryData.EncryptionAlgorithm = "AES-128";
                    decryptedEntryData.EncryptionKey = "";
                    decryptedEntryData.FileFullPath = decryptedPath;
                    decryptedEntryData.FileName = Path.GetFileName(decryptedPath);
                    decryptedEntryData.Duration = (DateTime.Now - time).TotalSeconds.ToString();

                    AddNewFile(decryptedEntryData, Overview_Form.getUser());

                    return 0;

                case "RSA":
                    DateTime rsaDecryptTime = DateTime.Now;

                    byte[] encryptedData = File.ReadAllBytes(item.FileFullPath);
                    byte[] decryptedData;

                    try
                    {
                        using (RSA rsa = RSA.Create())
                        {
                            byte[] privateKeyBytes = Convert.FromBase64String(item.EncryptionKey);
                            rsa.ImportRSAPrivateKey(privateKeyBytes, out _);

                            decryptedData = rsa.Decrypt(encryptedData, RSAEncryptionPadding.OaepSHA256);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("RSA Decryption failed: " + ex.Message, "RSA Error");
                        return -1;
                    }

                    string rsaDecryptedPath = item.FileFullPath.Substring(0, item.FileFullPath.LastIndexOf(".encrypted")) + ".decrypted";
                    File.WriteAllBytes(rsaDecryptedPath, decryptedData);

                    EntryData rsaDecryptedEntryData = new EntryData();
                    rsaDecryptedEntryData.Encrypted = "false";
                    rsaDecryptedEntryData.EncryptionAlgorithm = "RSA";
                    rsaDecryptedEntryData.EncryptionKey = "";
                    rsaDecryptedEntryData.FileFullPath = rsaDecryptedPath;
                    rsaDecryptedEntryData.FileName = Path.GetFileName(rsaDecryptedPath);
                    rsaDecryptedEntryData.Duration = (DateTime.Now - rsaDecryptTime).TotalSeconds.ToString();

                    AddNewFile(rsaDecryptedEntryData, Overview_Form.getUser());

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
