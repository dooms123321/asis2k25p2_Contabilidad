// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   29/10/2025

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Capa_Controlador_Estados_Financieros;

namespace Capa_Vista_Estados_Financieros
{
    public partial class Frm_EstadoDeBalanceDeSaldos : Form
    {
        private readonly Cls_BalanceDeSaldos_Controlador gControlador = new Cls_BalanceDeSaldos_Controlador();

        public Frm_EstadoDeBalanceDeSaldos()
        {
            InitializeComponent();

            // Configuración general del DataGridView
            Dgv_EstadoBalanceDeSaldos.AllowUserToAddRows = false;    // Quita la fila vacía final
            Dgv_EstadoBalanceDeSaldos.RowHeadersVisible = false;     // Oculta encabezados de fila
            Dgv_EstadoBalanceDeSaldos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dgv_EstadoBalanceDeSaldos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_EstadoBalanceDeSaldos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Fuente estandarizada (Rockwell)
            this.Font = new Font("Rockwell", 10, FontStyle.Regular);
        }

        // Evento del botón Generar
        private void Btn_Generar_Click(object sender, EventArgs e)
        {
            // Obtener balance
            Dgv_EstadoBalanceDeSaldos.AutoGenerateColumns = true;
            DataTable dts_Balance = gControlador.fun_obtener_balance_saldos();
            Dgv_EstadoBalanceDeSaldos.DataSource = dts_Balance;

            // Formato de moneda
            foreach (DataGridViewColumn col in Dgv_EstadoBalanceDeSaldos.Columns)
            {
                if (col.HeaderText == "Debe" || col.HeaderText == "Haber" || col.HeaderText == "Saldo")
                {
                    col.DefaultCellStyle.Format = "Q #,##0.00";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            // Verificar fila de totales
            if (dts_Balance.Rows.Count > 0)
            {
                DataRow ultimaFila = dts_Balance.Rows[dts_Balance.Rows.Count - 1];
                if (ultimaFila["Nombre"].ToString() == "TOTAL GENERAL")
                {
                    decimal deDiferencia = Convert.ToDecimal(ultimaFila["Saldo"]);
                    DataGridViewRow filaTotal = Dgv_EstadoBalanceDeSaldos.Rows[Dgv_EstadoBalanceDeSaldos.Rows.Count - 1];

                    // Colorear la fila y mostrar diferencia en el label
                    if (deDiferencia == 0)
                    {
                        filaTotal.DefaultCellStyle.BackColor = Color.LightGreen;
                        Lbl_Diferencia.Text = " El balance está cuadrado. Diferencia: Q 0.00";
                        Lbl_Diferencia.ForeColor = Color.Green;
                    }
                    else
                    {
                        filaTotal.DefaultCellStyle.BackColor = Color.LightCoral;
                        Lbl_Diferencia.Text = " El balance no cuadra. Diferencia: Q " + deDiferencia.ToString("#,##0.00");
                        Lbl_Diferencia.ForeColor = Color.Red;
                    }

                    // Centrar label debajo del DataGridView
                    Lbl_Diferencia.Left = (this.ClientSize.Width - Lbl_Diferencia.Width) / 2;
                    Lbl_Diferencia.Top = Dgv_EstadoBalanceDeSaldos.Bottom + 10;
                    Lbl_Diferencia.TextAlign = ContentAlignment.MiddleCenter;
                }
            }
        }

        // Evento del botón Limpiar
        private void Btn_Limpiar_Click(object sender, EventArgs e)
        {
            Dgv_EstadoBalanceDeSaldos.DataSource = null;
            Dgv_EstadoBalanceDeSaldos.Rows.Clear();
            Lbl_Diferencia.Text = "";
        }

        // Evento del botón Generar PDF (pendiente)
        private void Btn_Generar_PDF_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Función de exportar a PDF en desarrollo.",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        // Evento del botón Salir
        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
