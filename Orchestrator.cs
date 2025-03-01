using PROIECT_CSD.Date;
using PROIECT_CSD.Interfata_Utilizator;

namespace PROIECT_CSD
{
    static public class Orchestrator
    {
        static public IOverview? _o;

        /// <summary>
        /// Apeleaza functia de refresh din formul 'Overview_Form'.
        /// </summary>
        static public void RefreshOverViewTable() => _o?.Refresh();

        /// <summary>
        /// Adauga un item cu mana. va fi sters la primul refresh, de asta ii spun 'temporar'.
        /// </summary>
        /// <param name="entry"></param>
        static public void AddTemporaryEntry(EntryData entry) => _o?.AddTempEntry(entry);
    }
}
