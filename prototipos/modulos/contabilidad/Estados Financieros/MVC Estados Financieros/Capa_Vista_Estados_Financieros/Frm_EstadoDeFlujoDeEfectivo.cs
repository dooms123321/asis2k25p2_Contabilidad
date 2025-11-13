// =====================================================================================
// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   09/11/2025
// Descripción: Vista estandarizada para el Estado de Flujo de Efectivo (modo Actual / Histórico)
// Proyecto: QUANTUM S.A. - Módulo CTA (Contabilidad)
// =====================================================================================

using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Capa_Controlador_Estados_Financieros;

namespace Capa_Vista_Estados_Financieros
{
    public partial class Frm_Flujo_Efectivo : Form
    {
        private readonly Cls_FlujoEfectivo_Controlador gControlador = new Cls_FlujoEfectivo_Controlador();

        public Frm_Flujo_Efectivo()
        {
            InitializeComponent();
            Cbo_TipoOrigen.SelectedIndexChanged += Cbo_TipoOrigen_SelectedIndexChanged;

            groupBox2.Anchor = AnchorStyles.Top;
            Btn_Ver_Reporte.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // Configuración de ventana
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Rockwell", 10, FontStyle.Regular);
            this.Resize += new EventHandler(Frm_Flujo_Efectivo_Resize);

            // Configuración del DataGridView
            Dgv_FlujoEfectivo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Dgv_FlujoEfectivo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dgv_FlujoEfectivo.BackgroundColor = Color.White;

            // 🔹 Inicializa opciones del ComboBox
            Cbo_TipoOrigen.Items.Clear();
            Cbo_TipoOrigen.Items.AddRange(new string[] { "Actual", "Histórico" });
            Cbo_TipoOrigen.DropDownStyle = ComboBoxStyle.DropDownList;
            Cbo_TipoOrigen.SelectedIndex = 0; //  Muestra “Actual” al entrar

            // 🔹 Configura visibilidad inicial (oculta año/mes si es Actual)
            fun_visibilidad_controles();
        }



        private void Frm_Flujo_Efectivo_Load(object sender, EventArgs e)
        {
            fun_visibilidad_controles();
        }


        // ---------------------------------------------------------------------------------
        // Manejo de visibilidad de año/mes
        // ---------------------------------------------------------------------------------
        private void Cbo_TipoOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            fun_visibilidad_controles();
        }

        private void fun_visibilidad_controles()
        {
            bool esHistorico = Cbo_TipoOrigen.SelectedItem?.ToString() == "Histórico";

            // 🔹 Mostrar controles de año y mes solo si es histórico
            Lbl_Anio.Visible = esHistorico;
            Nud_Anio.Visible = esHistorico;
            Lbl_Mes.Visible = esHistorico;
            Nud_Mes.Visible = esHistorico;

        }


        // ---------------------------------------------------------------------------------
        // Evento: Generar Estado de Flujo de Efectivo
        // ---------------------------------------------------------------------------------
        private void Btn_Generar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!gControlador.fun_validar_estado())
                    throw new InvalidOperationException("El controlador no está disponible.");

                DataTable dts_Flujo;

                // ===============================================
                // Selección de modo: ACTUAL o HISTÓRICO
                // ===============================================
                if (Cbo_TipoOrigen.SelectedItem?.ToString() == "Histórico")
                {
                    int anio = (int)Nud_Anio.Value;
                    int mes = (int)Nud_Mes.Value;

                    dts_Flujo = gControlador.fun_obtener_flujo_efectivo_historico(anio, mes);

                    if (dts_Flujo == null || dts_Flujo.Rows.Count == 0)
                    {
                        MessageBox.Show($"No hay registros en el mes {mes} del año {anio}.",
                                        "Sin datos históricos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Dgv_FlujoEfectivo.DataSource = null;
                        Lbl_Resultado.Text = "";
                        return;
                    }
                }
                else
                {
                    dts_Flujo = gControlador.fun_obtener_flujo_efectivo();
                }

                // === Mostrar resultados ===
                Dgv_FlujoEfectivo.DataSource = dts_Flujo;

                // Forzar ajuste completo para la última fila (que suele tener texto largo)
                Dgv_FlujoEfectivo.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                fun_formatear_tabla();

                // === Resultado final ===
                Cls_ResultadoFlujo resultado;
                if (Cbo_TipoOrigen.SelectedItem?.ToString() == "Histórico")
                {
                    int anio = (int)Nud_Anio.Value;
                    int mes = (int)Nud_Mes.Value;
                    resultado = gControlador.fun_calcular_resultado_historico(anio, mes);
                }
                else
                {
                    resultado = gControlador.fun_calcular_resultado();
                }

                if (resultado.EsValido)
                {
                    Lbl_Resultado.Text = resultado.TextoResultado;
                    Lbl_Resultado.ForeColor = resultado.TipoResultado == "AUMENTO"
                        ? Color.Green
                        : resultado.TipoResultado == "DISMINUCIÓN"
                            ? Color.Red
                            : Color.Black;
                }
                else
                {
                    Lbl_Resultado.Text = "No hay registros para el periodo seleccionado.";
                    Lbl_Resultado.ForeColor = Color.DarkRed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el Estado de Flujo de Efectivo:\n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------------------------------------------------------------------------
        // Formateo del DataGridView
        // ---------------------------------------------------------------------------------
        private void fun_formatear_tabla()
        {
            foreach (DataGridViewColumn col in Dgv_FlujoEfectivo.Columns)
            {
                col.DefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Regular);
                col.HeaderCell.Style.Font = new Font("Rockwell", 10, FontStyle.Bold);

                if (col.HeaderText == "Entrada" || col.HeaderText == "Salida")
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                else
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            foreach (DataGridViewRow fila in Dgv_FlujoEfectivo.Rows)
            {

                string sNombre = fila.Cells["Nombre"].Value?.ToString() ?? "";
                if (sNombre.Contains("TOTAL FLUJO NETO"))
                {
                    fila.DefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Bold);
                    fila.DefaultCellStyle.BackColor = Color.Gainsboro;
                }
                else if (sNombre.Contains("AUMENTO") || sNombre.Contains("DISMINUCIÓN"))
                {
                    fila.DefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Bold | FontStyle.Underline);
                }
            }
            Dgv_FlujoEfectivo.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Dgv_FlujoEfectivo.AutoResizeRows();
        }

        // ---------------------------------------------------------------------------------
        // Botones secundarios
        // ---------------------------------------------------------------------------------
        private void Btn_Limpiar_Click(object sender, EventArgs e)
        {
            Dgv_FlujoEfectivo.DataSource = null;
            Dgv_FlujoEfectivo.Rows.Clear();
            Lbl_Resultado.Text = string.Empty;
            Lbl_Resultado.ForeColor = Color.Black;
        }

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // =====================================================================================
        // Autor: Arón Ricardo Esquit Silva
        // Carné: 0901-22-13036
        // Fecha: 11/11/2025
        // Descripción: Vista - Genera y guarda el reporte del Flujo de Efectivo (solo actual)
        // =====================================================================================
        private void Btn_Generar_Reportes_Click(object sender, EventArgs e)
        {
            try
            {
                // 🔹 Obtener datos del DataGridView actual (ya generado)
                if (Dgv_FlujoEfectivo.DataSource == null)
                {
                    MessageBox.Show("Primero debe generar el flujo de efectivo antes de guardarlo.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataTable dts_Flujo = (DataTable)Dgv_FlujoEfectivo.DataSource;

                if (dts_Flujo.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos disponibles para guardar el reporte.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                bool esHistorico = false; // Solo se permite actual

                Cls_Reporte_FlujoEfectivo_Controlador gControladorReporte = new Cls_Reporte_FlujoEfectivo_Controlador();
                string sResultado = gControladorReporte.Fun_Guardar_Reporte(dts_Flujo, esHistorico);

                MessageBox.Show(sResultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------------------------------------------------------------------------
        // Mantiene centrado el GroupBox al redimensionar la ventana
        // ---------------------------------------------------------------------------------
        private void Frm_Flujo_Efectivo_Resize(object sender, EventArgs e)
        {
            groupBox1.Left = (this.ClientSize.Width - groupBox1.Width) / 2;
        }

        // =====================================================================================
        // Autor: Arón Ricardo Esquit Silva
        // Carné: 0901-22-13036
        // Fecha: 11/11/2025
        // Descripción: Generación y visualización del reporte Crystal Report del Flujo de Efectivo
        // =====================================================================================

        // =====================================================================================
        // Autor: Arón Ricardo Esquit Silva
        // Carné: 0901-22-13036
        // Fecha: 11/11/2025
        // Descripción: Generación y visualización del reporte Crystal Report del Flujo de Efectivo
        // =====================================================================================
        private void Btn_Ver_Reporte_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dgv_FlujoEfectivo.Rows.Count == 0)
                {
                    MessageBox.Show("Primero genera el flujo de efectivo antes de ver el reporte.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Crear estructura igual al XSD
                DataTable dt = new DataTable("FlujoEfectivo");
                dt.Columns.Add("Cuenta");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Entrada");
                dt.Columns.Add("Salida");


                Func<decimal?, string> formatoQ = valor =>
                {
                    if (valor == null) return "";
                    return "Q " + valor.Value.ToString("#,##0.00");
                };


                // Llenar DataTable desde el DataGridView
                foreach (DataGridViewRow fila in Dgv_FlujoEfectivo.Rows)
                {
                    if (!fila.IsNewRow)
                    {
                        string cuenta = fila.Cells["Cuenta"].Value?.ToString() ?? "";
                        string nombre = fila.Cells["Nombre"].Value?.ToString() ?? "";

                        // 🔹 Evitar volver a agregar si ya es una fila de aumento/disminución
                        if (nombre.ToUpper().Contains("AUMENTO") || nombre.ToUpper().Contains("DISMINUCIÓN"))
                            continue;

                        string entradaTexto = fila.Cells["Entrada"].Value?.ToString()?.Replace("Q", "").Replace(",", "").Trim();
                        string salidaTexto = fila.Cells["Salida"].Value?.ToString()?.Replace("Q", "").Replace(",", "").Trim();

                        decimal? entradaVal = null;
                        decimal? salidaVal = null;

                        // Entrada
                        if (!string.IsNullOrWhiteSpace(entradaTexto))
                            entradaVal = Convert.ToDecimal(entradaTexto);

                        // Salida
                        if (!string.IsNullOrWhiteSpace(salidaTexto))
                            salidaVal = Convert.ToDecimal(salidaTexto);

                        // Formatos finales
                        string entradaFormateada = (entradaVal == null)
                            ? ""
                            : "Q " + entradaVal.Value.ToString("#,##0.00");

                        string salidaFormateada = (salidaVal == null)
                            ? ""
                            : "Q " + salidaVal.Value.ToString("#,##0.00");

                        // Agregar fila
                        dt.Rows.Add(
                            cuenta,
                            nombre,
                            entradaFormateada,
                            salidaFormateada
                        );

                    }
                }

                //  Agregar fila final con el mismo texto mostrado en el label
                if (!string.IsNullOrWhiteSpace(Lbl_Resultado.Text))
                {
                    DataRow filaResultado = dt.NewRow();
                    filaResultado["Cuenta"] = "";
                    filaResultado["Nombre"] = Lbl_Resultado.Text; // Ej: "Aumento neto de efectivo: Q1,000.00"
                    filaResultado["Entrada"] = DBNull.Value;
                    filaResultado["Salida"] = DBNull.Value;
                    dt.Rows.Add(filaResultado);
                }

                // Crear DataSet y enviar al reporte
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);

                Rpt_FlujoEfectivo rpt = new Rpt_FlujoEfectivo();
                rpt.SetDataSource(ds);
                rpt.SetParameterValue("TipoOrigen", Cbo_TipoOrigen.SelectedItem?.ToString() ?? "Actual");
                rpt.SetParameterValue("FechaActual", DateTime.Now);

                Frm_VisorReporte_FlujoEfectivo visor = new Frm_VisorReporte_FlujoEfectivo();
                visor.crystalReportViewer1.ReportSource = rpt;
                visor.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }







    }
}

// Fin de código de Arón Ricardo Esquit Silva
// =====================================================================================
