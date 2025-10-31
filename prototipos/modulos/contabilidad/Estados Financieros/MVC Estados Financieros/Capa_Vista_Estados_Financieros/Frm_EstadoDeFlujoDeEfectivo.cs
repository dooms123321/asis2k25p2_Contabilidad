// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   31/10/2025

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Capa_Controlador_Estados_Financieros;

namespace Capa_Vista_Estados_Financieros
{
    public partial class Frm_EstadoDeFlujoDeEfectivo : Form
    {
        private readonly Cls_FlujoDeEfectivo_Controlador gControlador = new Cls_FlujoDeEfectivo_Controlador();

        public Frm_EstadoDeFlujoDeEfectivo()
        {
            InitializeComponent();

            // Ajustar el formulario al tamaño de la pantalla
            this.WindowState = FormWindowState.Maximized;

            // Ajustar automáticamente las columnas
            Dgv_EstadoDeFlujoDeEfectivo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dgv_EstadoDeFlujoDeEfectivo.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            Dgv_EstadoDeFlujoDeEfectivo.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            Dgv_EstadoDeFlujoDeEfectivo.EnableHeadersVisualStyles = false;
        }

        // Botón GENERAR
        private void Btn_Generar_Click(object sender, EventArgs e)
        {
            try
            {
                Dgv_EstadoDeFlujoDeEfectivo.AutoGenerateColumns = true;
                DataTable dts_Flujo = gControlador.fun_obtener_flujo_efectivo();
                Dgv_EstadoDeFlujoDeEfectivo.DataSource = dts_Flujo;

                // Formateo de columnas numéricas
                foreach (DataGridViewColumn col in Dgv_EstadoDeFlujoDeEfectivo.Columns)
                {
                    if (col.HeaderText == "Entrada" || col.HeaderText == "Salida")
                    {
                        col.DefaultCellStyle.Format = "Q #,##0.00";
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }

                // Colorear filas según su tipo
                foreach (DataGridViewRow fila in Dgv_EstadoDeFlujoDeEfectivo.Rows)
                {
                    string nombre = fila.Cells["Nombre"].Value?.ToString() ?? "";

                    // Resaltar totales
                    if (nombre.StartsWith("Total Actividades"))
                    {
                        fila.DefaultCellStyle.BackColor = Color.LightYellow;
                        fila.DefaultCellStyle.Font = new Font(Dgv_EstadoDeFlujoDeEfectivo.Font, FontStyle.Bold);
                    }

                    // Resaltar totales globales
                    if (nombre == "Total Flujo Neto de Efectivo")
                    {
                        fila.DefaultCellStyle.BackColor = Color.FromArgb(255, 245, 180);
                        fila.DefaultCellStyle.Font = new Font(Dgv_EstadoDeFlujoDeEfectivo.Font, FontStyle.Bold);
                    }

                    // Resultado final
                    if (nombre.Contains("AUMENTO NETO") || nombre.Contains("DISMINUCIÓN NETA"))
                    {
                        bool esAumento = nombre.Contains("AUMENTO");

                        fila.DefaultCellStyle.BackColor = esAumento ? Color.LightGreen : Color.LightCoral;
                        fila.DefaultCellStyle.Font = new Font(Dgv_EstadoDeFlujoDeEfectivo.Font, FontStyle.Bold);
                        fila.DefaultCellStyle.ForeColor = Color.Black;
                    }

                    // Títulos de secciones
                    if (nombre.StartsWith("ACTIVIDADES") || nombre.StartsWith("CUENTAS NO CLASIFICADAS"))
                    {
                        fila.DefaultCellStyle.Font = new Font(Dgv_EstadoDeFlujoDeEfectivo.Font, FontStyle.Bold);
                        fila.DefaultCellStyle.BackColor = Color.FromArgb(235, 235, 255);
                    }
                }

                // Ajustar alto automático
                Dgv_EstadoDeFlujoDeEfectivo.AutoResizeRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el estado de flujo de efectivo:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Botón LIMPIAR
        private void Btn_Limpiar_Click(object sender, EventArgs e)
        {
            Dgv_EstadoDeFlujoDeEfectivo.DataSource = null;
            Dgv_EstadoDeFlujoDeEfectivo.Rows.Clear();
        }

        // Botón SALIR
        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Botón PDF
        private void Btn_Generar_PDF_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Función de exportar a PDF en desarrollo.",
                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
