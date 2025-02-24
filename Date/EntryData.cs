using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROIECT_CSD.Date
{
    /// <summary>
    /// Clasa ce encapsuleaza datele pentru un fisier. Toate campurile sunt stringuri
    /// pentru ca e mai usor de transmis de la/spre baza de date.
    /// </summary>
    public struct EntryData
    {
        public string FileName;
        public string FileFullPath;
        public string Encrypted;
        public string EncryptionKey;
        public string EncryptionAlgorithm;
        public string Duration;

        public EntryData()
        {
            FileName = "NULL";
            FileFullPath = "NULL";
            Encrypted = "false";
            EncryptionKey = "NULL";
            EncryptionAlgorithm = "NULL";
            Duration = "NULL";
        }
    }
}
