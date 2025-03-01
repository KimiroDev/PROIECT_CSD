using PROIECT_CSD.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROIECT_CSD.Interfata_Utilizator
{
    public interface IOverview
    {
        public void Refresh();
        public void AddTempEntry(EntryData entry);
    }
}
