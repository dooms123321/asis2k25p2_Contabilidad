// =====================================================================================
// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   09/11/2025
// Descripción: Formulario para la visualización del Balance de Saldos (actual e histórico)
// =====================================================================================

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Capa_Controlador_Estados_Financieros;
using System.IO;


namespace Capa_Vista_Estados_Financieros
{
    public partial class Frm_EstadoDeBalanceDeSaldos : Form
    {
        // Controlador principal
        private readonly Cls_BalanceDeSaldos_Controlador gControlador = new Cls_BalanceDeSaldos_Controlador();




        public Frm_EstadoDeBalanceDeSaldos()
        {
            InitializeComponent();

            groupBox2.Anchor = AnchorStyles.Top;
            Btn_Ver_Reporte.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.WindowState = FormWindowState.Maximized;
            // Configuración de ventana
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Rockwell", 10, FontStyle.Regular);
            this.Resize += new EventHandler(Frm_EstadoDeBalanceDeSaldos_Resize);
            Dgv_EstadoBalanceDeSaldos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // Configuración del DataGridView
            Dgv_EstadoBalanceDeSaldos.AllowUserToAddRows = false;
            Dgv_EstadoBalanceDeSaldos.RowHeadersVisible = false;
            Dgv_EstadoBalanceDeSaldos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dgv_EstadoBalanceDeSaldos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_EstadoBalanceDeSaldos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_EstadoBalanceDeSaldos.BackgroundColor = Color.White;
            Dgv_EstadoBalanceDeSaldos.ReadOnly = true;
            Dgv_EstadoBalanceDeSaldos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Dgv_EstadoBalanceDeSaldos.MultiSelect = false;
            Dgv_EstadoBalanceDeSaldos.EnableHeadersVisualStyles = false;
            Dgv_EstadoBalanceDeSaldos.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            Dgv_EstadoBalanceDeSaldos.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

            // =====================================================================================
            // Configuración visual general y anclaje para mantener proporciones al redimensionar
            // =====================================================================================

            this.MinimumSize = new Size(850, 550);




            // Configuración de controles nuevos
            Fun_Configurar_Controles_Adicionales();

            // Eventos principales
            Cbo_TipoOrigen.SelectedIndexChanged += Cbo_TipoOrigen_SelectedIndexChanged;
            Btn_Limpiar.Click += Btn_Limpiar_Click;
            Btn_Salir.Click += Btn_Salir_Click;
        }

        // ---------------------------------------------------------------------------------
        // Configura los nuevos controles de origen, año y mes
        // ---------------------------------------------------------------------------------
        private void Fun_Configurar_Controles_Adicionales()
        {
            // ComboBox: selección de origen
            Cbo_TipoOrigen.Items.Clear();
            Cbo_TipoOrigen.Items.AddRange(new string[] { "Actual", "Histórico" });
            Cbo_TipoOrigen.DropDownStyle = ComboBoxStyle.DropDownList;
            Cbo_TipoOrigen.SelectedIndex = 0;

            // Año
            Lbl_Anio.Text = "Año:";
            Lbl_Anio.Visible = false;
            Nud_Anio.Minimum = 2000;
            Nud_Anio.Maximum = 2100;
            Nud_Anio.Value = DateTime.Now.Year;
            Nud_Anio.Visible = false;

            // Mes
            Lbl_Mes.Text = "Mes:";
            Lbl_Mes.Visible = false;
            Nud_Mes.Minimum = 1;
            Nud_Mes.Maximum = 12;
            Nud_Mes.Value = DateTime.Now.Month;
            Nud_Mes.Visible = false;
        }


        // ---------------------------------------------------------------------------------
        // Evento: Selección de origen de datos (Actual / Histórico)
        // ---------------------------------------------------------------------------------
        private void Cbo_TipoOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bEsHistorico = Cbo_TipoOrigen.SelectedItem.ToString() == "Histórico";

            // Mostrar controles de año y mes solo si es histórico
            Lbl_Anio.Visible = bEsHistorico;
            Nud_Anio.Visible = bEsHistorico;
            Lbl_Mes.Visible = bEsHistorico;
            Nud_Mes.Visible = bEsHistorico;

            // El nivel siempre habilitado
            Nud_Nivel.Enabled = true;


        }

        // ---------------------------------------------------------------------------------
        // Evento: Botón GENERAR
        // Obtiene el balance según el tipo de origen seleccionado
        // ---------------------------------------------------------------------------------
        private void Btn_Generar_Click(object sender, EventArgs e)
        {

            int iNivel = Convert.ToInt32(Nud_Nivel.Value);
            DataTable dts_Balance = new DataTable();

            bool bEsHistorico = Cbo_TipoOrigen.SelectedItem.ToString() == "Histórico";

            try
            {
                if (bEsHistorico)
                {
                    int iAnio = Convert.ToInt32(Nud_Anio.Value);
                    int iMes = Convert.ToInt32(Nud_Mes.Value);

                    dts_Balance = gControlador.Fun_Obtener_Balance_Saldos_Historico(iNivel, iAnio, iMes);

                    if (dts_Balance.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay registros históricos para el año y mes seleccionados.",
                                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    dts_Balance = gControlador.Fun_Obtener_Balance_Saldos(iNivel);

                    if (dts_Balance.Rows.Count == 0)
                    {
                        MessageBox.Show("No existen registros para generar el Balance de Saldos.",
                                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                // Asignar datos al DataGridView
                Dgv_EstadoBalanceDeSaldos.DataSource = dts_Balance;

                // Configurar encabezados
                if (Dgv_EstadoBalanceDeSaldos.Columns.Contains("Codigo"))
                    Dgv_EstadoBalanceDeSaldos.Columns["Codigo"].HeaderText = "Cuenta";

                if (Dgv_EstadoBalanceDeSaldos.Columns.Contains("Nombre"))
                    Dgv_EstadoBalanceDeSaldos.Columns["Nombre"].HeaderText = "Nombre";

                if (Dgv_EstadoBalanceDeSaldos.Columns.Contains("Debe"))
                    Dgv_EstadoBalanceDeSaldos.Columns["Debe"].HeaderText = "Debe (Q)";

                if (Dgv_EstadoBalanceDeSaldos.Columns.Contains("Haber"))
                    Dgv_EstadoBalanceDeSaldos.Columns["Haber"].HeaderText = "Haber (Q)";

                // Alinear columnas numéricas
                foreach (DataGridViewColumn col in Dgv_EstadoBalanceDeSaldos.Columns)
                {
                    if (col.HeaderText.Contains("(Q)"))
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                // Formato de filas y validación de totales
                Fun_Aplicar_Formato_Filas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el balance: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------------------------------------------------------------------------------
        // Aplica formato visual a las filas del DataGridView
        // ---------------------------------------------------------------------------------
        private void Fun_Aplicar_Formato_Filas()
        {
            foreach (DataGridViewRow fila in Dgv_EstadoBalanceDeSaldos.Rows)
            {
                fila.DefaultCellStyle.BackColor = Color.White;
                fila.DefaultCellStyle.ForeColor = Color.Black;
                fila.DefaultCellStyle.Font = new Font("Rockwell", 10, FontStyle.Regular);

                string sNombre = fila.Cells["Nombre"].Value?.ToString() ?? "";
                if (sNombre == "TOTAL GENERAL")
                {
                    fila.DefaultCellStyle.Font = new Font("Rockwell", 11, FontStyle.Bold);
                    fila.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    decimal deDebe = 0, deHaber = 0;
                    string sDebe = fila.Cells["Debe"].Value?.ToString() ?? "";
                    string sHaber = fila.Cells["Haber"].Value?.ToString() ?? "";

                    if (sDebe.StartsWith("Q ")) sDebe = sDebe.Substring(2).Trim();
                    if (sHaber.StartsWith("Q ")) sHaber = sHaber.Substring(2).Trim();

                    decimal.TryParse(sDebe, out deDebe);
                    decimal.TryParse(sHaber, out deHaber);

                    decimal deDiferencia = deDebe - deHaber;

                    if (deDiferencia == 0)
                    {
                        fila.DefaultCellStyle.BackColor = Color.LightGreen;
                        Lbl_Diferencia.Text = "El balance está cuadrado. Diferencia: Q 0.00";
                        Lbl_Diferencia.ForeColor = Color.FromArgb(0, 100, 0);     // Verde oscuro
                        Lbl_Diferencia.Font = new Font("Rockwell", 14, FontStyle.Bold);

                    }
                    else
                    {
                        fila.DefaultCellStyle.BackColor = Color.LightCoral;
                        Lbl_Diferencia.Text = "El balance no cuadra. Diferencia: Q " +
                            Math.Abs(deDiferencia).ToString("#,##0.00");
                        Lbl_Diferencia.ForeColor = Color.FromArgb(139, 0, 0);     // Rojo oscuro
                        Lbl_Diferencia.Font = new Font("Rockwell", 14, FontStyle.Bold);

                    }
                }
            }

            Dgv_EstadoBalanceDeSaldos.DefaultCellStyle.SelectionBackColor = Color.White;
            Dgv_EstadoBalanceDeSaldos.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        // ---------------------------------------------------------------------------------
        // Evento: Botón LIMPIAR
        // ---------------------------------------------------------------------------------
        private void Btn_Limpiar_Click(object sender, EventArgs e)
        {
            Dgv_EstadoBalanceDeSaldos.DataSource = null;
            Dgv_EstadoBalanceDeSaldos.Rows.Clear();
            Lbl_Diferencia.Text = "";
        }

        // ---------------------------------------------------------------------------------
        // Evento: Botón SALIR
        // ---------------------------------------------------------------------------------
        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // ---------------------------------------------------------------------------------
        // Mantiene centrado el GroupBox cuando se cambia el tamaño del formulario
        // ---------------------------------------------------------------------------------
        private void Frm_EstadoDeBalanceDeSaldos_Resize(object sender, EventArgs e)
        {
            groupBox1.Left = (this.ClientSize.Width - groupBox1.Width) / 2;
        }

        private void Btn_Ver_Reporte_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que el DataGridView tenga datos
                if (Dgv_EstadoBalanceDeSaldos.DataSource == null || Dgv_EstadoBalanceDeSaldos.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para mostrar en el reporte.");
                    return;
                }

                // Crear DataTable con los nombres correctos para Crystal Report
                DataTable dt = new DataTable();
                dt.Columns.Add("Cuenta");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Debe", typeof(string));
                dt.Columns.Add("Haber", typeof(string));

                Func<decimal?, string> formatoQ = valor =>
                {
                    if (valor == null) return "";
                    return "Q " + valor.Value.ToString("#,##0.00");
                };


                // Llenar DataTable manualmente desde el DGV
                foreach (DataGridViewRow fila in Dgv_EstadoBalanceDeSaldos.Rows)
                {
                    if (!fila.IsNewRow)
                    {
                        string cuenta = fila.Cells["Codigo"].Value?.ToString() ?? "";
                        string nombre = fila.Cells["Nombre"].Value?.ToString() ?? "";

                        // Limpiar símbolos y convertir a decimal
                        string debeStr = fila.Cells["Debe"].Value?.ToString().Replace("Q", "").Replace(",", "").Trim();
                        string haberStr = fila.Cells["Haber"].Value?.ToString().Replace("Q", "").Replace(",", "").Trim();


                        // Manejar vacío y valores reales
                        decimal? debeVal = null;
                        decimal? haberVal = null;

                        // Solo convertir si trae un número real
                        if (!string.IsNullOrWhiteSpace(debeStr))
                            debeVal = Convert.ToDecimal(debeStr);

                        if (!string.IsNullOrWhiteSpace(haberStr))
                            haberVal = Convert.ToDecimal(haberStr);

                        // Formato final:
                        // Si es null → vacío
                        // Si es 0 → Q 0.00
                        // Si no → Q x,xxx.xx
                        string debeFormateado =
                            (debeVal == null) ? "" : "Q " + debeVal.Value.ToString("#,##0.00");

                        string haberFormateado =
                            (haberVal == null) ? "" : "Q " + haberVal.Value.ToString("#,##0.00");


                        // Agregar fila ya formateada
                        dt.Rows.Add(
                            cuenta,
                            nombre,
                            debeFormateado,
                            haberFormateado
                        );


                    }
                }

                // Crear DataSet y asignar datos
                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                ds.Tables[0].TableName = "BalanceSaldos"; // Igual que en tu XSD

                // Crear el reporte y asignar fuente
                Rpt_BalanceSaldos rpt = new Rpt_BalanceSaldos();
                rpt.SetDataSource(ds);

                // Parámetros
                rpt.SetParameterValue("TipoOrigen", Cbo_TipoOrigen.SelectedItem?.ToString() ?? "Actual");
                rpt.SetParameterValue("Nivel", Convert.ToInt32(Nud_Nivel.Value));
                rpt.SetParameterValue("FechaActual", DateTime.Now);

                // Mostrar en visor
                Frm_VisorReporte_BalanceDeSaldos visor = new Frm_VisorReporte_BalanceDeSaldos();
                visor.crystalReportViewer1.ReportSource = rpt;
                visor.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message);
            }
        }

        private void Btn_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                // Ruta relativa donde está tu archivo CHM (igual que tu compañero)
                const string subRutaAyuda = @"ayuda\modulos\contabilidad\Ayudas\Ayuda Conta.chm";

                string rutaEncontrada = null;
                DirectoryInfo dir = new DirectoryInfo(Application.StartupPath);

                // Busca la carpeta hacia arriba (10 niveles)
                for (int i = 0; i < 10 && dir != null; i++, dir = dir.Parent)
                {
                    string candidata = Path.Combine(dir.FullName, subRutaAyuda);
                    if (File.Exists(candidata))
                    {
                        rutaEncontrada = candidata;
                        break;
                    }
                }

                // Ruta de respaldo (opcional)
                string rutaAbsolutaRespaldo =
                    @"C:\Users\arone\OneDrive\Escritorio\asis2k25p2_Contabilidad\ayuda\modulos\contabilidad\Ayudas\Ayuda Conta.chm";

                if (rutaEncontrada == null && File.Exists(rutaAbsolutaRespaldo))
                    rutaEncontrada = rutaAbsolutaRespaldo;

                if (rutaEncontrada != null)
                {
                    // Esta es la ruta INTERNA del archivo dentro del CHM
                    string rutaInterna = @"Balance de Saldos/Ayuda-Balance de Saldos.html";


                    Help.ShowHelp(this, rutaEncontrada, HelpNavigator.Topic, rutaInterna);
                }
                else
                {
                    MessageBox.Show("No se encontró el archivo de ayuda.", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir la ayuda:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}

// Fin de código de Arón Ricardo Esquit Silva
// =====================================================================================
