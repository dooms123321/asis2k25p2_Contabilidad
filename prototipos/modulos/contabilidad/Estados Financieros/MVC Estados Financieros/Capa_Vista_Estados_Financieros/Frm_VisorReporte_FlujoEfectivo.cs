using System;
using System.Windows.Forms;

namespace Capa_Vista_Estados_Financieros
{
    public partial class Frm_VisorReporte_FlujoEfectivo : Form
    {
        public Frm_VisorReporte_FlujoEfectivo()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new System.Drawing.Font("Rockwell", 10);
        }
    }
}
