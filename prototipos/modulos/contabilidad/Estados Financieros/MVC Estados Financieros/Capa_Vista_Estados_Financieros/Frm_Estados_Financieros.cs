using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Vista_Estados_Financieros
{
    public partial class Frm_Estados_Financieros : Form
    {
        public Frm_Estados_Financieros()
        {
            InitializeComponent();
        }

        private void estadoDeResultadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Frm_Estado_de_Resultados frm = new Frm_Estado_de_Resultados();
            frm.FormClosed += (s, args) => this.Show();
            frm.Show();
        }

        // Estado de Cambios en el Patrimonio Neto
        private void estadosDeCambiosEnElPatrimonioNetoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Frm_Estado_de_Cambios_en_el_Patrimonio_Neto frm = new Frm_Estado_de_Cambios_en_el_Patrimonio_Neto();
            frm.FormClosed += (s, args) => this.Show();
            frm.Show();
        }

        // Balance de Situación General
        private void balanceDeSituaciónGeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Frm_Balance_de_Situacion_General frm = new Frm_Balance_de_Situacion_General();
            frm.FormClosed += (s, args) => this.Show();
            frm.Show();
        }

        // Estado de Flujo de Efectivo
        private void estadoDeFlujoDeEjectivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Frm_Estado_de_Flujo_de_Efectivo frm = new Frm_Estado_de_Flujo_de_Efectivo();
            frm.FormClosed += (s, args) => this.Show();
            frm.Show();
        }
    }
}
