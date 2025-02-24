namespace PROIECT_CSD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Centram fereastra
            Rectangle screen = Screen.GetBounds(this);
            Size = new();
        }
    }
}
