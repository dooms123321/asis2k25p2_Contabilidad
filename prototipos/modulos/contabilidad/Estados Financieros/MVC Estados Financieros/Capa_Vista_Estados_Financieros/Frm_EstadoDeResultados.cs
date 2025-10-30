// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   30/10/2025

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Capa_Controlador_Estados_Financieros;

namespace Capa_Vista_Estados_Financieros
{
    public partial class Frm_EstadoDeResultados : Form
    {
        private readonly Cls_EstadoDeResultados_Controlador gControlador = new Cls_EstadoDeResultados_Controlador();

        public Frm_EstadoDeResultados()
        {
            InitializeComponent();

            // Configuración general del formulario
            this.WindowState = FormWindowState.Maximized;
            this.Font = new Font("Rockwell", 10, FontStyle.Regular);

            // Configuración del DataGridView
            Dgv_EstadoDeResultados.AllowUserToAddRows = false;
            Dgv_EstadoDeResultados.RowHeadersVisible = false;
            Dgv_EstadoDeResultados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dgv_EstadoDeResultados.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_EstadoDeResultados.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        // Evento del botón Generar
        private void Btn_Generar_Click(object sender, EventArgs e)
        {
            // Obtener los datos calculados desde el controlador
            DataTable dts_Estado = gControlador.fun_obtener_estado_resultados();

            if (dts_Estado.Rows.Count == 0)
            {
                MessageBox.Show("No existen registros para generar el Estado de Resultados.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Asignar los datos al DataGridView
            Dgv_EstadoDeResultados.DataSource = dts_Estado;

            // Formato de las columnas numéricas
            foreach (DataGridViewColumn col in Dgv_EstadoDeResultados.Columns)
            {
                if (col.HeaderText == "Valor")
                {
                    col.DefaultCellStyle.Format = "Q #,##0.00";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            // Aplicar formato visual según el tipo de fila
            foreach (DataGridViewRow fila in Dgv_EstadoDeResultados.Rows)
            {
                string nombre = fila.Cells["Nombre"].Value?.ToString() ?? "";

                // 🔹 Secciones principales (títulos)
                if (nombre == "INGRESOS" || nombre == "COSTOS" ||
                    nombre == "GASTOS OPERATIVOS" || nombre == "GASTOS FINANCIEROS" ||
                    nombre == "GASTO POR ISR")
                {
                    fila.DefaultCellStyle.BackColor = Color.LightSteelBlue;
                    fila.DefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Bold);
                }

                // 🔹 Totales parciales
                else if (nombre.StartsWith("Total"))
                {
                    fila.DefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Bold);
                    fila.DefaultCellStyle.BackColor = Color.Beige;
                }

                // 🔹 Resultado final (UTILIDAD o PÉRDIDA)
                else if (nombre == "UTILIDAD NETA" || nombre == "PÉRDIDA NETA")
                {
                    fila.DefaultCellStyle.Font = new Font("Rockwell", 11, FontStyle.Bold);
                    fila.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    if (nombre.Contains("UTILIDAD"))
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
        }

        // Evento del botón Limpiar
        private void Btn_Limpiar_Click(object sender, EventArgs e)
        {
            Dgv_EstadoDeResultados.DataSource = null;
            Dgv_EstadoDeResultados.Rows.Clear();
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
