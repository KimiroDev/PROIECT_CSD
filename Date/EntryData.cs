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
    public class EntryData
    {
        public string FileName = "NULL";
        public string FileFullPath = "NULL";
        public string Encrypted = "false";
        public string EncryptionKey = "NULL";
        public string EncryptionAlgorithm = "NULL";
    }
}
