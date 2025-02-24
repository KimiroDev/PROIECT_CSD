using PROIECT_CSD.Interfata_Utilizator;

namespace PROIECT_CSD
{
    public partial class Overview_Form : Form, IOverview
    {
        public Overview_Form()
        {
            InitializeComponent();
        }

        private void Overview_Form_Load(object sender, EventArgs e)
        {
            // amandoua apele ale functiei 'CenterToScreen' sunt necesare, deoarece pe mai
            // multe monitoare Screen.GetBounds nu este initial setat pe monitorul corect
            CenterToScreen();

            // Centram fereastra
            Rectangle screen = Screen.GetBounds(this);
            Size = new((int)(screen.Width * 0.9d), (int)(screen.Height * 0.8d));

            // amandoua apele ale functiei 'CenterToScreen' sunt necesare, deoarece pe mai
            // multe monitoare Screen.GetBounds nu este initial setat pe monitorul corect
            CenterToScreen();
        }

        void IOverview.Refresh()
        {
            MessageBox.Show("IOverview.Refresh()", "Not implemented");
        }
    }
}
