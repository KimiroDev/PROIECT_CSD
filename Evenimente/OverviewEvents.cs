using PROIECT_CSD.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROIECT_CSD.Evenimente
{
    static public partial class Evenimente
    {
        /// <summary>
        /// DE FACUT
        /// Ia datele despre fisiere din baza de date si le pune intr-o lista de `EntryData`.
        /// In principal, pentru a fi afisate pe tabelul vizibil.
        /// </summary>
        /// <returns></returns>
        static public List<EntryData> GetFilesFromDatabase(string user)
        {

            // ...

            List<EntryData> files = [];

            EntryData placeholder1 = new()
            {
                FileName = "fisier",
                Encrypted = "true",
                EncryptionAlgorithm = "SHA256",
                EncryptionKey = RandomString(10),
                Duration = "12",
                FileFullPath = "C:\\Users\\student\\Desktop\\fisier.enc",
            };

            EntryData placeholder2 = new()
            {
                FileName = "parole",
                Encrypted = "false",
                EncryptionAlgorithm = "-",
                EncryptionKey = "-",
                Duration = "-",
                FileFullPath = "C:\\Users\\student\\Desktop\\parole.txt",
            };

            EntryData placeholder3 = new()
            {
                FileName = "retetacrabbypatty",
                Encrypted = "true",
                EncryptionAlgorithm = "RA",
                EncryptionKey = RandomString(10),
                Duration = "409",
                FileFullPath = "C:\\Users\\student\\Desktop\\retetacrabbypatty.enc",
            };

            files.Add(placeholder1);
            files.Add(placeholder2);
            files.Add(placeholder3);

            return files;
        }

        /// <summary>
        /// DE FACUT logare.
        /// </summary>
        /// <returns>2 strings representing the name of the user and type 'regular' or 'admin' or 'null' if login fails.</returns>
        static public (string username, string usertype) LoginUser(string username, string passwd)
        {

            // ...

            (string, string) placeholder = ("Piratu", "null");
            if (passwd == "r") placeholder.Item2 = "regular";
            if (passwd == "a") placeholder.Item2 = "admin";
            return placeholder;
        }

        /// <summary>
        /// DE FACUT
        /// APELATI ORCHESTRATOR.REFRESH SA SE REFLECTE SCHIMBAREA PE FORM
        /// Functia apelata de butonul 'Add File'.
        /// </summary>
        static public void AddNewFile()
        {

            // ...

            Orchestrator.RefreshOverViewTable();
        }

        /// <summary>
        /// DE FACUT
        /// stergere fisier dat
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        static public bool DeleteFile(EntryData file)
        {
            
            // ...

            return false;
        }

        /// <summary>
        /// cheie random pentru teste DE SCOS DIN VERSIUNEA FINALA 
        /// FUNCTIA RANDOM E ASA BAD PENTRU SECURITATE
        /// </summary>
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
