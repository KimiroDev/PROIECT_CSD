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
        /// Ia datele despre fisiere din baza de date si le pune intr-o lista de `EntryData`.
        /// In principal, pentru a fi afisate pe tabelul vizibil.
        /// </summary>
        /// <returns></returns>
        static public List<EntryData> GetFilesFromDatabase()
        {
            List<EntryData> files = [];

            EntryData placeholder = new()
            {
                Encrypted = "true",
                EncryptionAlgorithm = "SHA256",
                EncryptionKey = "abcdefgh",
                FileFullPath = "C:\\Users\\student\\Desktop\\fisier.enc",
                FileName = "fisier"
            };

            files.Add(placeholder);

            return files;
        }
    }
}
