// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   30/10/2025

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Capa_Controlador_Estados_Financieros;

namespace Capa_Vista_Estados_Financieros
{
    public partial class Frm_EstadoBalanceGeneral : Form
    {
        private readonly Cls_BalanceGeneral_Controlador gControlador = new Cls_BalanceGeneral_Controlador();

        public Frm_EstadoBalanceGeneral()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;
            this.Font = new Font("Rockwell", 10, FontStyle.Regular);

            Dgv_EstadoBalanceGeneral.AllowUserToAddRows = false;
            Dgv_EstadoBalanceGeneral.RowHeadersVisible = false;
            Dgv_EstadoBalanceGeneral.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dgv_EstadoBalanceGeneral.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_EstadoBalanceGeneral.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        // Evento del botón Generar
        private void Btn_Generar_Click(object sender, EventArgs e)
        {
            DataTable dts_Balance = gControlador.fun_obtener_balance_general();

            if (dts_Balance.Rows.Count == 0)
            {
                MessageBox.Show("No existen registros para generar el Balance General.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Dgv_EstadoBalanceGeneral.DataSource = dts_Balance;

            foreach (DataGridViewColumn col in Dgv_EstadoBalanceGeneral.Columns)
            {
                if (col.HeaderText == "Valor")
                {
                    col.DefaultCellStyle.Format = "Q #,##0.00";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            // Aplicar formato visual
            foreach (DataGridViewRow fila in Dgv_EstadoBalanceGeneral.Rows)
            {
                string nombre = fila.Cells["Nombre"].Value?.ToString() ?? "";

                // Secciones principales
                if (nombre == "ACTIVO" || nombre == "PASIVO" || nombre == "CAPITAL")
                {
                    fila.DefaultCellStyle.BackColor = Color.LightSteelBlue;
                    fila.DefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Bold);
                }

                // Totales (resaltados en amarillo)
                else if (nombre.StartsWith("Total") || nombre.StartsWith("Suma del Pasivo"))
                {
                    fila.DefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Bold);
                    fila.DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                }

                // Ecuación contable (última fila)
                else if (nombre == "BALANCE CUADRADO" || nombre == "DESCUADRE")
                {
                    fila.DefaultCellStyle.Font = new Font("Rockwell", 11, FontStyle.Bold);
                    fila.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    if (nombre == "BALANCE CUADRADO")
                    {
                        fila.DefaultCellStyle.BackColor = Color.LightGreen;
                        fila.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        fila.DefaultCellStyle.BackColor = Color.LightCoral;
                        fila.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }

            // Evaluar la ecuación contable para el Label
            decimal totalActivo = 0;
            decimal totalPasivo = 0;
            decimal totalCapital = 0;

            foreach (DataRow fila in dts_Balance.Rows)
            {
                string tipo = fila["Tipo"].ToString();
                decimal valor = 0;
                decimal.TryParse(fila["Valor"].ToString(), out valor);

                if (tipo == "Activo") totalActivo += valor;
                else if (tipo == "Pasivo") totalPasivo += valor;
                else if (tipo == "Capital") totalCapital += valor;
            }

            decimal diferencia = totalActivo - (totalPasivo + totalCapital);

            if (diferencia == 0)
            {
                Lbl_EcuacionContable.Text = "El balance general está cuadrado";
                Lbl_EcuacionContable.ForeColor = Color.Green;
            }
            else
            {
                Lbl_EcuacionContable.Text = "El balance general NO está cuadrado. Diferencia: Q " + diferencia.ToString("#,##0.00");
                Lbl_EcuacionContable.ForeColor = Color.Red;
            }

            // Centrar label debajo del DataGridView
            Lbl_EcuacionContable.Left = (this.ClientSize.Width - Lbl_EcuacionContable.Width) / 2;
            Lbl_EcuacionContable.Top = Dgv_EstadoBalanceGeneral.Bottom + 10;
            Lbl_EcuacionContable.TextAlign = ContentAlignment.MiddleCenter;
        }

        // Evento del botón Limpiar
        private void Btn_Limpiar_Click(object sender, EventArgs e)
        {
            Dgv_EstadoBalanceGeneral.DataSource = null;
            Dgv_EstadoBalanceGeneral.Rows.Clear();
            Lbl_EcuacionContable.Text = "";
        }

        // Evento del botón Generar PDF (en desarrollo)
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
