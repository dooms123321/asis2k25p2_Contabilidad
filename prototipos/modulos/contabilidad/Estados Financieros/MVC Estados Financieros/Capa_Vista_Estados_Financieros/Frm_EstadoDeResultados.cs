// =====================================================================================
// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   10/11/2025
// Descripción: Formulario del Estado de Resultados (actual e histórico)
// =====================================================================================

using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Capa_Controlador_Estados_Financieros;

namespace Capa_Vista_Estados_Financieros
{
    public partial class Frm_EstadoDeResultados : Form
    {
        private readonly Cls_EstadoResultados_Controlador gControlador = new Cls_EstadoResultados_Controlador();

        public Frm_EstadoDeResultados()
        {
            InitializeComponent();

            groupBox2.Anchor = AnchorStyles.Top;
            Btn_Ver_Reporte.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // Configuración general
            // Configuración general del formulario
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Rockwell", 10, FontStyle.Regular);
            this.Resize += new EventHandler(Frm_EstadoDeResultados_Resize);

            // Configuración del DataGridView
            Dgv_EstadoDeResultados.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Dgv_EstadoDeResultados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dgv_EstadoDeResultados.BackgroundColor = Color.White;
            Dgv_EstadoDeResultados.ReadOnly = true;
            Dgv_EstadoDeResultados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Dgv_EstadoDeResultados.MultiSelect = false;
            Dgv_EstadoDeResultados.EnableHeadersVisualStyles = false;

            // Configurar controles adicionales
            Fun_Configurar_Controles_Adicionales();


        }

        // ---------------------------------------------------------------------------------
        // Configura controles adicionales (Actual / Histórico, año y mes)
        // ---------------------------------------------------------------------------------
        private void Fun_Configurar_Controles_Adicionales()
        {
            Cbo_TipoOrigen.Items.Clear();
            Cbo_TipoOrigen.Items.AddRange(new string[] { "Actual", "Histórico" });
            Cbo_TipoOrigen.DropDownStyle = ComboBoxStyle.DropDownList;
            Cbo_TipoOrigen.SelectedIndex = 0;
            Cbo_TipoOrigen.SelectedIndexChanged += Cbo_TipoOrigen_SelectedIndexChanged;

            Lbl_Anio.Text = "Año:";
            Lbl_Anio.Visible = false;
            Nud_Anio.Minimum = 2000;
            Nud_Anio.Maximum = 2100;
            Nud_Anio.Value = DateTime.Now.Year;
            Nud_Anio.Visible = false;

            Lbl_Mes.Text = "Mes:";
            Lbl_Mes.Visible = false;
            Nud_Mes.Minimum = 1;
            Nud_Mes.Maximum = 12;
            Nud_Mes.Value = DateTime.Now.Month;
            Nud_Mes.Visible = false;
        }



        // ---------------------------------------------------------------------------------
        // Evento: Cambio de origen de datos
        // ---------------------------------------------------------------------------------
        private void Cbo_TipoOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool esHistorico = Cbo_TipoOrigen.SelectedItem.ToString() == "Histórico";
            Lbl_Anio.Visible = esHistorico;
            Nud_Anio.Visible = esHistorico;
            Lbl_Mes.Visible = esHistorico;
            Nud_Mes.Visible = esHistorico;
            Btn_Generar_Reportes.Enabled = !esHistorico;
        }

        // ---------------------------------------------------------------------------------
        // Evento: Botón GENERAR
        // ---------------------------------------------------------------------------------
        private void Btn_Generar_Click(object sender, EventArgs e)
        {
            try
            {
                int iNivel = (int)Nud_Nivel.Value;
                bool esHistorico = Cbo_TipoOrigen.SelectedItem.ToString() == "Histórico";
                DataTable dts_Estado;

                if (esHistorico)
                {
                    int iAnio = (int)Nud_Anio.Value;
                    int iMes = (int)Nud_Mes.Value;
                    dts_Estado = gControlador.Fun_Obtener_Estado_Resultados_Historico(iNivel, iAnio, iMes);

                    // 🔸 Validar si hay datos reales
                    bool sinDatosReales = dts_Estado == null || dts_Estado.Rows.Count == 0 ||
                        dts_Estado.AsEnumerable().All(r =>
                            r["Saldo"] == DBNull.Value ||
                            string.IsNullOrWhiteSpace(r["Saldo"].ToString()) ||
                            Convert.ToDecimal(r["Saldo"]) == 0);

                    if (sinDatosReales)
                    {
                        Dgv_EstadoDeResultados.DataSource = null;
                        Dgv_EstadoDeResultados.Rows.Clear();
                        Lbl_Resultado.Text = "";
                        MessageBox.Show($"No hay registros históricos para el mes {iMes:D2} del año {iAnio}.",
                                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    dts_Estado = gControlador.Fun_Obtener_Estado_Resultados(iNivel);

                    if (dts_Estado == null || dts_Estado.Rows.Count == 0)
                    {
                        MessageBox.Show("No existen registros para generar el Estado de Resultados.",
                                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                // Asignar datos al DataGridView
                Dgv_EstadoDeResultados.DataSource = dts_Estado;

                // Formatear la columna de saldo con símbolo Q y dos decimales
                if (Dgv_EstadoDeResultados.Columns.Contains("Saldo"))
                {
                    Dgv_EstadoDeResultados.Columns["Saldo"].DefaultCellStyle.Format = "Q #,##0.00";
                    Dgv_EstadoDeResultados.Columns["Saldo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }


                if (Dgv_EstadoDeResultados.Columns.Contains("Codigo"))
                    Dgv_EstadoDeResultados.Columns["Codigo"].HeaderText = "Cuenta";
                if (Dgv_EstadoDeResultados.Columns.Contains("Nombre"))
                    Dgv_EstadoDeResultados.Columns["Nombre"].HeaderText = "Nombre";
                if (Dgv_EstadoDeResultados.Columns.Contains("Tipo"))
                    Dgv_EstadoDeResultados.Columns["Tipo"].HeaderText = "Tipo";
                if (Dgv_EstadoDeResultados.Columns.Contains("Saldo"))
                    Dgv_EstadoDeResultados.Columns["Saldo"].HeaderText = "Saldo (Q)";

                foreach (DataGridViewColumn col in Dgv_EstadoDeResultados.Columns)
                {
                    if (col.HeaderText.Contains("(Q)"))
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                // 🔹 Calcular utilidad o pérdida
                decimal deUtilidad = esHistorico
                    ? gControlador.Fun_Calcular_Utilidad_Neta_Historico((int)Nud_Anio.Value, (int)Nud_Mes.Value)
                    : gControlador.Fun_Calcular_Utilidad_Neta();

                string sTipoResultado = gControlador.Fun_Obtener_Tipo_Resultado(deUtilidad);
                string sMonto = $"Q {Math.Abs(deUtilidad):N2}";

                // Mostrar resultado final
                if (sTipoResultado == "UTILIDAD NETA")
                {
                    Lbl_Resultado.Text = $"Utilidad Neta: {sMonto}";
                    Lbl_Resultado.ForeColor = Color.FromArgb(0, 100, 0); // Verde oscuro
                }
                else if (sTipoResultado == "PERDIDA NETA")
                {
                    Lbl_Resultado.Text = $"Pérdida Neta: {sMonto}";
                    Lbl_Resultado.ForeColor = Color.FromArgb(139, 0, 0);
                }
                else
                {
                    Lbl_Resultado.Text = "Sin variación en resultados.";
                    Lbl_Resultado.ForeColor = Color.Gray;
                }

                Lbl_Resultado.TextAlign = ContentAlignment.MiddleCenter;
                Lbl_Resultado.Left = (this.ClientSize.Width - Lbl_Resultado.Width) / 2;
                Dgv_EstadoDeResultados.ClearSelection();
                Dgv_EstadoDeResultados.CurrentCell = null;

                // === Agregar fila final al DataTable (no directamente al DataGridView) ===
                if (!string.IsNullOrWhiteSpace(Lbl_Resultado.Text) && Dgv_EstadoDeResultados.DataSource is DataTable dts)
                {
                    DataRow filaFinal = dts.NewRow();

                    // Solo mostrar el texto sin el monto
                    if (Lbl_Resultado.Text.Contains("Utilidad"))
                        filaFinal["Nombre"] = "UTILIDAD NETA";
                    else if (Lbl_Resultado.Text.Contains("Pérdida"))
                        filaFinal["Nombre"] = "PÉRDIDA NETA";
                    else
                        filaFinal["Nombre"] = "RESULTADO FINAL";


                    // 🔹 Guardamos el valor como decimal (no texto)
                    decimal monto = 0;
                    string texto = Lbl_Resultado.Text;
                    if (texto.Contains("Q"))
                    {
                        string numero = texto.Substring(texto.IndexOf("Q") + 1).Trim();
                        decimal.TryParse(numero, out monto);
                    }
                    filaFinal["Saldo"] = monto;

                    // Insertar en el DataTable
                    dts.Rows.Add(filaFinal);

                    // Refrescar la vista
                    Dgv_EstadoDeResultados.DataSource = dts;

                    // Aplicar estilo visual
                    int ultimaFila = Dgv_EstadoDeResultados.Rows.Count - 1;
                    var filaGrid = Dgv_EstadoDeResultados.Rows[ultimaFila];
                    filaGrid.DefaultCellStyle.Font = new Font("Rockwell", 11, FontStyle.Bold);
                    filaGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    filaGrid.DefaultCellStyle.BackColor = Color.White;

                    if (Lbl_Resultado.ForeColor == Color.FromArgb(0, 100, 0))
                        filaGrid.DefaultCellStyle.ForeColor = Color.FromArgb(0, 100, 0); // Verde oscuro
                    else if (Lbl_Resultado.ForeColor == Color.FromArgb(139, 0, 0))
                        filaGrid.DefaultCellStyle.ForeColor = Color.FromArgb(139, 0, 0); // Rojo vino oscuro
                     else
                        filaGrid.DefaultCellStyle.ForeColor = Color.Black;

                }





            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el Estado de Resultados:\n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------------------------------------------------------------------------
        // Botón LIMPIAR
        // ---------------------------------------------------------------------------------
        private void Btn_Limpiar_Click(object sender, EventArgs e)
        {
            Dgv_EstadoDeResultados.DataSource = null;
            Dgv_EstadoDeResultados.Rows.Clear();
            Lbl_Resultado.Text = "";
        }

        // ---------------------------------------------------------------------------------
        // Botón SALIR
        // ---------------------------------------------------------------------------------
        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ---------------------------------------------------------------------------------
        // Mantiene centrado el GroupBox al redimensionar la ventana
        // ---------------------------------------------------------------------------------
        private void Frm_EstadoDeResultados_Resize(object sender, EventArgs e)
        {
            groupBox1.Left = (this.ClientSize.Width - groupBox1.Width) / 2;
        }

        // =====================================================================================
        // Autor: Arón Ricardo Esquit Silva
        // Carné: 0901-22-13036
        // Fecha: 12/11/2025
        // Descripción: Generación del Crystal Report del Estado de Resultados (versión final)
        // =====================================================================================
        private void Btn_VerReporte_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que existan datos
                if (Dgv_EstadoDeResultados.DataSource == null || Dgv_EstadoDeResultados.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para mostrar en el reporte.");
                    return;
                }

                // Crear DataTable con nombres que Crystal Report espera
                DataTable dt = new DataTable("EstadoResultados");
                dt.Columns.Add("Cuenta");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Tipo");
                dt.Columns.Add("Saldo", typeof(string));

                // Tomar los valores existentes del DataGridView
                foreach (DataGridViewRow fila in Dgv_EstadoDeResultados.Rows)
                {
                    if (fila.IsNewRow) continue;

                    string cuenta = fila.Cells["Codigo"].Value?.ToString() ?? "";
                    string nombre = fila.Cells["Nombre"].Value?.ToString() ?? "";
                    string tipo = fila.Cells["Tipo"].Value?.ToString() ?? "";

                    string saldoStr = fila.Cells["Saldo"].Value?.ToString() ?? "0";
                    saldoStr = saldoStr.Replace("Q", "").Replace(",", "").Trim();
                    decimal.TryParse(saldoStr, out decimal saldo);
                    saldoStr = (saldo == 0) ? "" : "Q " + saldo.ToString("#,##0.00");

                    dt.Rows.Add(cuenta, nombre, tipo, saldoStr);
                }

                // Crear DataSet y asignar datos
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);

                // Crear y cargar el Crystal Report
                Rpt_EstadoResultados rpt = new Rpt_EstadoResultados();
                rpt.SetDataSource(ds);

                rpt.SetParameterValue("TipoOrigen", Cbo_TipoOrigen.SelectedItem?.ToString() ?? "Actual");
                rpt.SetParameterValue("Nivel", Convert.ToInt32(Nud_Nivel.Value));
                rpt.SetParameterValue("FechaActual", DateTime.Now);

                // Mostrar visor
                Frm_VisorReporte_EstadoResultados visor = new Frm_VisorReporte_EstadoResultados();
                visor.crystalReportViewer1.ReportSource = rpt;
                visor.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al generar el reporte:\n" +
                    ex.Message +
                    "\n\nLínea donde falló:\n" +
                    ex.StackTrace,
                    "Error detallado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }














    }
}

// Fin de código de Arón Ricardo Esquit Silva
// =====================================================================================
