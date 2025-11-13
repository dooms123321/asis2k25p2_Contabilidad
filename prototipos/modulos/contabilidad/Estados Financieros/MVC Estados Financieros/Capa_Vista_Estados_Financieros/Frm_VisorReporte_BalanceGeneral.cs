// =====================================================================================
// Autor: Arón Ricardo Esquit Silva
// Carné: 0901-22-13036
// Fecha: 11/11/2025
// Descripción: Visor de reportes Crystal Reports para el Balance General
// =====================================================================================

using System;
using System.Windows.Forms;

namespace Capa_Vista_Estados_Financieros
{
    public partial class Frm_VisorReporte_BalanceGeneral : Form
    {
        public Frm_VisorReporte_BalanceGeneral()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new System.Drawing.Font("Rockwell", 10);
        }
    }
}
