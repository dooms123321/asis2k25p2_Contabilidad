using System;
using System.Data;
using System.Windows.Forms;
using Capa_Controlador_CierreContable;

namespace Capa_Vista_CierreContable
{
    public partial class Frm_CierreContable : Form
    {
        private Cls_ControladorCierre controlador = new Cls_ControladorCierre();

        public Frm_CierreContable()
        {
            InitializeComponent();

            Cbo_periodo.Items.AddRange(new[] { "Anual", "Mensual", "Diario" });
            Cbo_periodo.DropDownStyle = ComboBoxStyle.DropDownList;

            Dtp_fecha_cierre.Enabled = false;
        }

        private void Frm_CierreContable_Load(object sender, EventArgs e)
        {
            // Puedes cargar información inicial aquí si deseas
        }

        private void Cbo_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string periodoSeleccionado = Cbo_periodo.SelectedItem.ToString();

            switch (periodoSeleccionado)
            {
                case "Anual":
                    MessageBox.Show("Seleccionaste ANUAL");
                    break;
                case "Mensual":
                    MessageBox.Show("Seleccionaste MENSUAL");
                    break;
                case "Diario":
                    MessageBox.Show("Seleccionaste DIARIO");
                    break;
            }
        }

        private void Dtp_fecha_cierre_ValueChanged(object sender, EventArgs e)
        {
            Dtp_fecha_cierre.Format = DateTimePickerFormat.Short;
            Dtp_fecha_cierre.Value = DateTime.Today;
        }

        private void Btn_cargar_Click(object sender, EventArgs e)
        {
            Btn_cargar.Enabled = false;
            MessageBox.Show("Cargando datos...");
            Application.DoEvents();

            CargarPolizas();

            MessageBox.Show("Datos cargados correctamente.", 
                            "Cierre contable", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Btn_cargar.Enabled = true;
        }

        private void CargarPolizas()
        {
            try
            {
                DataTable dt = controlador.CargarPolizas();
                Dgv_Cuentas.DataSource = dt;

                Dgv_Cuentas.Columns["Pk_EncCodigo_Poliza"].HeaderText = "Código Póliza";
                Dgv_Cuentas.Columns["Pk_Fecha_Poliza"].HeaderText = "Fecha";
                Dgv_Cuentas.Columns["Pk_Codigo_Cuenta"].HeaderText = "Código Cuenta";
                Dgv_Cuentas.Columns["Cmp_CtaNombre"].HeaderText = "Nombre Cuenta";
                Dgv_Cuentas.Columns["Cmp_Concepto_Poliza"].HeaderText = "Concepto";
                Dgv_Cuentas.Columns["Debe"].HeaderText = "Debe";
                Dgv_Cuentas.Columns["Haber"].HeaderText = "Haber";

                Dgv_Cuentas.AutoResizeColumns();
                CalcularTotales();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pólizas:\n" + ex.Message, 
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcularTotales()
        {
            decimal totalDebe = 0;
            decimal totalHaber = 0;

            if (!Dgv_Cuentas.Columns.Contains("Debe") || !Dgv_Cuentas.Columns.Contains("Haber"))
            {
                MessageBox.Show("Las columnas 'Debe' y 'Haber' no existen en los resultados.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (DataGridViewRow fila in Dgv_Cuentas.Rows)
            {
                if (fila.IsNewRow)
                    continue;

                if (fila.Cells["Debe"].Value != null &&
                    decimal.TryParse(fila.Cells["Debe"].Value.ToString(), out decimal debe))
                {
                    totalDebe += debe;
                }

                if (fila.Cells["Haber"].Value != null &&
                    decimal.TryParse(fila.Cells["Haber"].Value.ToString(), out decimal haber))
                {
                    totalHaber += haber;
                }
            }

            Lbl_TotalDebe.Text = totalDebe.ToString("N2");
            Lbl_TotalHaber.Text = totalHaber.ToString("N2");

            decimal totalCierre = totalDebe - totalHaber;
            Lbl_SaldosTotales.Text = totalCierre.ToString("N2");
        }

        private void Btn_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Cbo_periodo.Text))
                {
                    MessageBox.Show("Debe seleccionar un periodo (Anual, Mensual o Diario).",
                                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime fechaCierre = Dtp_fecha_cierre.Value.Date;
                DateTime fechaDesde = Dtp_fecha_desde.Value.Date;
                DateTime fechaHasta = Dtp_Fecha_Hasta.Value.Date;

                decimal debe = decimal.Parse(Lbl_TotalDebe.Text);
                decimal haber = decimal.Parse(Lbl_TotalHaber.Text);
                decimal saldoFinal = decimal.Parse(Lbl_SaldosTotales.Text);

                string observaciones = Rtb_observaciones.Text;

                controlador.InsertarCierreContable(
                    fechaCierre,
                    Cbo_periodo.Text,
                    fechaDesde,
                    fechaHasta,
                    debe,
                    haber,
                    0,
                    saldoFinal,
                    observaciones
                );

                MessageBox.Show("✅ Cierre contable registrado correctamente.",
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "⚠️ ERROR EN EJECUCIÓN ⚠️\n\n" +
                    "Mensaje: " + ex.Message + "\n\n" +
                    "Tipo: " + ex.GetType().FullName + "\n\n" +
                    "Método: " + ex.TargetSite + "\n\n" +
                    "StackTrace:\n" + ex.StackTrace +
                    (ex.InnerException != null ? "\n\nInnerException: " + ex.InnerException.Message : ""),
                    "ERROR DETALLADO",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void Btn_salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
