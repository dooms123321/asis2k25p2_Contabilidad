// =============================================
//  Autor: Arón Ricardo Esquit Silva
//  Carné: 0901-22-13036
//  Fecha: 10/11/2025
//  Descripción: Balance General (actual e histórico) con filtrado por niveles
// =============================================

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Capa_Controlador_Estados_Financieros;
using System.Data;
using System.Linq;
using System.IO;




namespace Capa_Vista_Estados_Financieros
{
    public partial class Frm_EstadoBalanceGeneral : Form
    {
        private readonly Cls_BalanceGeneral_Controlador gControlador = new Cls_BalanceGeneral_Controlador();

        public Frm_EstadoBalanceGeneral()
        {
            InitializeComponent();

            groupBox2.Anchor = AnchorStyles.Top;
            Btn_Ver_Reporte.Anchor = AnchorStyles.Top | AnchorStyles.Right;




            // Configuración general del formulario
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Rockwell", 10, FontStyle.Regular);
            this.Resize += new EventHandler(Frm_EstadoBalanceGeneral_Resize);


            // Configuración del DataGridView
            Dgv_EstadoBalanceGeneral.AllowUserToAddRows = false;
            Dgv_EstadoBalanceGeneral.RowHeadersVisible = false;
            Dgv_EstadoBalanceGeneral.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Dgv_EstadoBalanceGeneral.BackgroundColor = Color.White;
            Dgv_EstadoBalanceGeneral.ReadOnly = true;
            Dgv_EstadoBalanceGeneral.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Dgv_EstadoBalanceGeneral.MultiSelect = false;
            Dgv_EstadoBalanceGeneral.EnableHeadersVisualStyles = false;
            // Permitir que el DataGridView se ajuste al tamaño del formulario
            Dgv_EstadoBalanceGeneral.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;


            Fun_ConfigurarControles_Adicionales();

        }

        // -------------------------------------------------------------------------
        // Configurar controles adicionales (Origen, Año, Mes)
        // -------------------------------------------------------------------------
        private void Fun_ConfigurarControles_Adicionales()
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




  

        // -------------------------------------------------------------------------
        // Evento: Cambio de tipo de origen (Actual / Histórico)
        // -------------------------------------------------------------------------
        private void Cbo_TipoOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool esHistorico = Cbo_TipoOrigen.SelectedItem.ToString() == "Histórico";
            Lbl_Anio.Visible = esHistorico;
            Nud_Anio.Visible = esHistorico;
            Lbl_Mes.Visible = esHistorico;
            Nud_Mes.Visible = esHistorico;

            // Bloquear o habilitar el botón de reporte
        }

        // -------------------------------------------------------------------------
        // Evento: Botón Generar
        // -------------------------------------------------------------------------
        private void Btn_Generar_Click(object sender, EventArgs e)
        {
            try
            {
                int iNivel = (int)Num_Nivel.Value;
                bool esHistorico = Cbo_TipoOrigen.SelectedItem.ToString() == "Histórico";
                List<Cls_BalanceGeneral_Controlador.Cls_CuentaVisual> lstBalance;
                decimal totalActivo = 0, totalPasivo = 0, totalCapital = 0;

                if (esHistorico)
                {
                    int iAnio = Convert.ToInt32(Nud_Anio.Value);
                    int iMes = Convert.ToInt32(Nud_Mes.Value);

                    // 🔹 Consultar datos históricos
                    lstBalance = gControlador.Fun_Obtener_BalanceVisual_Historico(iNivel, iAnio, iMes);

                    // 🔸 Validar si todos los saldos están vacíos o en cero
                    bool sinDatosReales = lstBalance == null
                        || lstBalance.Count == 0
                        || lstBalance.TrueForAll(c =>
                            (string.IsNullOrWhiteSpace(c.sDebe) || c.sDebe == "Q0.00" || c.sDebe == "Q 0.00") &&
                            (string.IsNullOrWhiteSpace(c.sHaber) || c.sHaber == "Q0.00" || c.sHaber == "Q 0.00"));

                    if (sinDatosReales)
                    {
                        Dgv_EstadoBalanceGeneral.DataSource = null;
                        Dgv_EstadoBalanceGeneral.Rows.Clear();
                        Lbl_Resultado.Text = "";
                        MessageBox.Show(
                            $"No hay registros históricos para el mes {iMes:D2} del año {iAnio}.",
                            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // 🔹 Obtener totales históricos
                    (totalActivo, totalPasivo, totalCapital) =
                        gControlador.Fun_Obtener_Totales_Balance_Historico(iAnio, iMes);
                }

                else
                {
                    // 🔹 Consultar datos actuales
                    lstBalance = gControlador.Fun_Obtener_BalanceVisual(iNivel);

                    if (lstBalance == null || lstBalance.Count == 0)
                    {
                        Dgv_EstadoBalanceGeneral.DataSource = null;
                        Dgv_EstadoBalanceGeneral.Rows.Clear();
                        Lbl_Resultado.Text = "";
                        MessageBox.Show(
                            "No existen registros para generar el Balance General.",
                            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // 🔹 Obtener totales actuales
                    (totalActivo, totalPasivo, totalCapital) =
                        gControlador.Fun_Obtener_Totales_Balance();
                }

                // Si llega aquí, sí hay datos — se llena la tabla normalmente
                Dgv_EstadoBalanceGeneral.Columns.Clear();
                Dgv_EstadoBalanceGeneral.Rows.Clear();
                Dgv_EstadoBalanceGeneral.Columns.Add("Cuenta", "Cuenta");
                Dgv_EstadoBalanceGeneral.Columns.Add("Nombre", "Nombre");
                Dgv_EstadoBalanceGeneral.Columns.Add("Debe", "Debe (Q)");
                Dgv_EstadoBalanceGeneral.Columns.Add("Haber", "Haber (Q)");
                Dgv_EstadoBalanceGeneral.Columns["Nombre"].Width = 400;

                decimal totalPasivoCapital = totalPasivo + totalCapital;
                int filaActivo = -1, filaPasivoCapital = -1;

                foreach (var cuenta in lstBalance)
                {
                    int fila = Dgv_EstadoBalanceGeneral.Rows.Add(
                        cuenta.sCuenta, cuenta.sNombre, cuenta.sDebe, cuenta.sHaber);

                    if (cuenta.sNombre.Trim().ToUpper() == "ACTIVO")
                        filaActivo = fila;

                    if (cuenta.EsVirtual && cuenta.sNombre.Contains("TOTAL"))
                        filaPasivoCapital = fila;
                }

                if (filaPasivoCapital >= 0)
                {
                    Dgv_EstadoBalanceGeneral.Rows[filaPasivoCapital].Cells["Haber"].Value =
                        totalPasivoCapital != 0 ? $"Q {totalPasivoCapital:N2}" : "";
                }

                bool estaCuadrado = totalActivo == totalPasivoCapital;
                decimal diferencia = Math.Abs(totalActivo - totalPasivoCapital);
                Color colorResultado = estaCuadrado
                    ? Color.FromArgb(0, 100, 0)   // Verde oscuro (fuerte contraste)
                    : Color.FromArgb(139, 0, 0);  // Rojo oscuro (vino)


                if (filaActivo >= 0)
                {
                    Dgv_EstadoBalanceGeneral["Debe", filaActivo].Style.Font = new Font("Rockwell", 12, FontStyle.Bold | FontStyle.Underline);
                    Dgv_EstadoBalanceGeneral["Debe", filaActivo].Style.ForeColor = colorResultado;
                }

                if (filaPasivoCapital >= 0)
                {
                    Dgv_EstadoBalanceGeneral["Haber", filaPasivoCapital].Style.Font = new Font("Rockwell", 10, FontStyle.Bold | FontStyle.Underline);
                    Dgv_EstadoBalanceGeneral["Haber", filaPasivoCapital].Style.ForeColor = colorResultado;
                }

                Lbl_Resultado.Text = estaCuadrado
                    ? "El balance general está cuadrado"
                    : $"El balance general NO está cuadrado. Diferencia: Q{diferencia:N2}";
                Lbl_Resultado.ForeColor = colorResultado;
                Lbl_Resultado.TextAlign = ContentAlignment.MiddleCenter;
                Lbl_Resultado.Left = (this.ClientSize.Width - Lbl.Width) / 2;

                Dgv_EstadoBalanceGeneral.ClearSelection();
                Dgv_EstadoBalanceGeneral.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el Balance General:\n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Btn_Limpiar_Click(object sender, EventArgs e)
        {
            Dgv_EstadoBalanceGeneral.DataSource = null;
            Dgv_EstadoBalanceGeneral.Rows.Clear();
            Lbl_Resultado.Text = "";
        }

        private void Btn_Salir_Click(object sender, EventArgs e) => this.Close();



        // Fecha: 11/11/2025
        // Descripción: Vista - Genera y guarda el reporte del Balance General (niveles 1, 2, 3)
        // =====================================================================================
        private void Btn_Generar_Reporte_Click(object sender, EventArgs e)
        {
            try
            {
                bool esHistorico = (Cbo_TipoOrigen.SelectedItem?.ToString() == "Histórico");

                // 🔹 Obtener los datos reales del controlador
                List<Cls_BalanceGeneral_Controlador.Cls_CuentaVisual> lstBalance;

                if (esHistorico)
                {
                    int iAnio = Convert.ToInt32(Nud_Anio.Value);
                    int iMes = Convert.ToInt32(Nud_Mes.Value);
                    lstBalance = gControlador.Fun_Obtener_BalanceVisual_Historico(3, iAnio, iMes);
                }
                else
                {
                    lstBalance = gControlador.Fun_Obtener_BalanceVisual(3);
                }

                // 🔹 Validar que existan datos
                if (lstBalance == null || lstBalance.Count == 0)
                {
                    MessageBox.Show("No hay datos disponibles para generar el reporte.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 🔹 Convertir la lista a DataTable (para el DAO)
                DataTable dts_Balance = new DataTable();
                dts_Balance.Columns.Add("Cuenta");
                dts_Balance.Columns.Add("Nombre");
                dts_Balance.Columns.Add("Debe");
                dts_Balance.Columns.Add("Haber");

                foreach (var cuenta in lstBalance)
                {
                    dts_Balance.Rows.Add(cuenta.sCuenta, cuenta.sNombre, cuenta.sDebe, cuenta.sHaber);
                }

                // 🔹 Guardar usando el nuevo controlador de reportes
                Cls_Reporte_BalanceGeneral_Controlador gControladorReporte = new Cls_Reporte_BalanceGeneral_Controlador();
                string sResultado = gControladorReporte.Fun_Guardar_Reporte(dts_Balance, esHistorico);

                MessageBox.Show(sResultado, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // 🔹 Método auxiliar para agregar filas
        private void AgregarFila(DataTable tabla, string cuenta, string nombre, string debe, string haber)
        {
            DataRow fila = tabla.NewRow();
            fila["Cuenta"] = cuenta;
            fila["Nombre"] = nombre;
            fila["Debe"] = debe;
            fila["Haber"] = haber;
            tabla.Rows.Add(fila);
        }

        // ---------------------------------------------------------------------------------
        // Mantiene centrado el GroupBox cuando se cambia el tamaño del formulario
        // ---------------------------------------------------------------------------------
        private void Frm_EstadoBalanceGeneral_Resize(object sender, EventArgs e)
        {
            groupBox1.Left = (this.ClientSize.Width - groupBox1.Width) / 2;
        }

        private void Btn_VerReporte_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar si hay filas cargadas
                if (Dgv_EstadoBalanceGeneral.Rows.Count == 0)
                {
                    MessageBox.Show("Primero genere los datos antes de ver el reporte.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Crear DataTable manualmente desde el contenido del DataGridView
                DataTable dt = new DataTable();
                dt.Columns.Add("Cuenta");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Debe", typeof(decimal));
                dt.Columns.Add("Haber", typeof(decimal));

                // Llenar el DataTable con los datos del grid
                foreach (DataGridViewRow fila in Dgv_EstadoBalanceGeneral.Rows)
                {
                    if (!fila.IsNewRow)
                    {
                        string cuenta = "";
                        string nombre = "";
                        string debeStr = "";
                        string haberStr = "";

                        if (fila.Cells["Cuenta"].Value != null)
                            cuenta = fila.Cells["Cuenta"].Value.ToString();
                        if (fila.Cells["Nombre"].Value != null)
                            nombre = fila.Cells["Nombre"].Value.ToString();

                        if (fila.Cells["Debe"].Value != null)
                            debeStr = fila.Cells["Debe"].Value.ToString().Replace("Q", "").Replace(",", "").Trim();
                        if (fila.Cells["Haber"].Value != null)
                            haberStr = fila.Cells["Haber"].Value.ToString().Replace("Q", "").Replace(",", "").Trim();

                        object debe, haber;

                        if (string.IsNullOrWhiteSpace(debeStr))
                            debe = DBNull.Value;
                        else
                            debe = Convert.ToDecimal(debeStr);

                        if (string.IsNullOrWhiteSpace(haberStr))
                            haber = DBNull.Value;
                        else
                            haber = Convert.ToDecimal(haberStr);

                        dt.Rows.Add(cuenta, nombre, debe, haber);
                    }
                }

                // Validar si hay datos
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para mostrar en el reporte.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Crear el DataSet y asignar datos
                DataSet ds = new DataSet();
                ds.Tables.Add(dt.Copy());
                ds.Tables[0].TableName = "BalanceGeneral"; // Nombre debe coincidir con el XSD

                // Crear el reporte y asignar fuente de datos
                Rpt_BalanceGeneral rpt = new Rpt_BalanceGeneral();
                rpt.SetDataSource(ds);

                // Asignar los parámetros
                string tipoOrigen = "Actual";
                if (Cbo_TipoOrigen.SelectedItem != null)
                    tipoOrigen = Cbo_TipoOrigen.SelectedItem.ToString();

                rpt.SetParameterValue("TipoOrigen", tipoOrigen);
                rpt.SetParameterValue("Nivel", Convert.ToInt32(Num_Nivel.Value));
                rpt.SetParameterValue("FechaActual", DateTime.Now);

                // Mostrar en visor
                Frm_VisorReporte_BalanceGeneral visor = new Frm_VisorReporte_BalanceGeneral();
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
                // Ruta RELATIVA dentro del proyecto
                const string subRutaAyuda = @"Ayuda\Ayuda Conta.chm";

                string rutaCHM = null;
                DirectoryInfo dir = new DirectoryInfo(Application.StartupPath);

                // Buscar hasta 10 niveles hacia arriba
                for (int i = 0; i < 10 && dir != null; i++, dir = dir.Parent)
                {
                    string intento = Path.Combine(dir.FullName, subRutaAyuda);
                    if (File.Exists(intento))
                    {
                        rutaCHM = intento;
                        break;
                    }
                }

                // Si NO lo encuentra → mostrar mensaje elegante
                if (rutaCHM == null)
                {
                    MessageBox.Show("No se encontró el archivo de ayuda (CHM).",
                        "Ayuda no disponible",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // RUTA INTERNA del archivo dentro del CHM
                string paginaInterna = "Balance General/Ayuda - Balance Genral.html";

                // ABRIR LA AYUDA
                Help.ShowHelp(this, rutaCHM, HelpNavigator.Topic, paginaInterna);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir la ayuda:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

// =============================================
// Fin del código de Arón Ricardo Esquit Silva
// =============================================
