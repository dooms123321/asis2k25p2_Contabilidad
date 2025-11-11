// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   29/10/2025

using System;
using System.Windows.Forms;
using Capa_Vista_Estados_Financieros;

namespace Capa_Vista_Estados_Financieros
{
    public partial class Frm_EstadosFinancieros : Form
    {
        public Frm_EstadosFinancieros()
        {
            InitializeComponent();
        }

        // Método general para abrir formularios sin cerrar la aplicación
        private void AbrirFormulario(Form nuevoFormulario)
        {
            this.Hide(); // oculta el principal
            nuevoFormulario.FormClosed += (s, args) => this.Show(); // al cerrar el nuevo, vuelve el principal
            nuevoFormulario.Show();
        }

        // Estado de Resultados
        private void Tsm_EstadoResultados_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new Frm_EstadoDeResultados());
        }

        // Estado de Balance de Saldos
        private void Tsm_EstadoBalanceDeSaldos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new Frm_EstadoDeBalanceDeSaldos());
        }

        // Balance General
        private void Tsm_BalanceGeneral_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new Frm_EstadoBalanceGeneral());
        }

        // Flujo de Efectivo
        private void Tsm_FlujoEfectivo_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new Frm_Flujo_Efectivo());

        }
    }
}
