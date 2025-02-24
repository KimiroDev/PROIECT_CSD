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
        
    }
}
