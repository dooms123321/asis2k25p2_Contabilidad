using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
<<<<<<< HEAD
using System.Data.Odbc;
=======
>>>>>>> e42e22a9edf48ad572659449d0ea79beb894f129
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            //Dgv_Cuentas.Enabled = false;
        }

        private void Frm_CierreContable_Load(object sender, EventArgs e)
        {
<<<<<<< HEAD
            Dgv_Cuentas.AutoGenerateColumns = true;
            Dgv_Cuentas.DataSource = null;

            // Evita que el usuario agregue filas manualmente
            Dgv_Cuentas.AllowUserToAddRows = false;
=======
            
>>>>>>> e42e22a9edf48ad572659449d0ea79beb894f129
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Gbp_datos_Enter(object sender, EventArgs e)
        {

        }

        private void Cbo_periodo_SelectedIndexChanged(object sender, EventArgs e)
        {
<<<<<<< HEAD
            DateTime hoy = DateTime.Today;
            switch (Cbo_periodo.SelectedItem.ToString())
            {
                case "Mensual":
                    Dtp_fecha_desde.Value = new DateTime(hoy.Year, hoy.Month, 1);
                    Dtp_Fecha_Hasta.Value = Dtp_fecha_desde.Value.AddMonths(1).AddDays(-1);
                    break;
                case "Anual":
                    Dtp_fecha_desde.Value = new DateTime(hoy.Year, 1, 1);
                    Dtp_Fecha_Hasta.Value = new DateTime(hoy.Year, 12, 31);
                    break;
                case "Diario":
                    Dtp_fecha_desde.Value = hoy;
                    Dtp_Fecha_Hasta.Value = hoy;
                    break;
            }
        }

=======


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
>>>>>>> e42e22a9edf48ad572659449d0ea79beb894f129

        private void Lbl_fecha_desde_Click(object sender, EventArgs e)
        {

        }

        private void Dtp_fecha_cierre_ValueChanged(object sender, EventArgs e)
        {
            Dtp_fecha_cierre.Format = DateTimePickerFormat.Short;
            Dtp_fecha_cierre.Value = DateTime.Today;

            
        }

        private void Dgv_Cuentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



       

        private void Btn_cargar_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            Btn_cargar.Enabled = false;
            MessageBox.Show( "Cargando datos...");
            Application.DoEvents();

            // Ejecutar carga
            CargarPolizas();

            MessageBox.Show("Datos cargados correctamente.", "Cierre contable", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Btn_cargar.Enabled = true;
=======
            CargarPolizas();
>>>>>>> e42e22a9edf48ad572659449d0ea79beb894f129
        }


        private void CargarPolizas()
        {
            try
            {
<<<<<<< HEAD
                DateTime fechaDesde = Dtp_fecha_desde.Value.Date;
                DateTime fechaHasta = Dtp_Fecha_Hasta.Value.Date;

                DataTable dt;

                if (fechaDesde != DateTime.MinValue && fechaHasta != DateTime.MinValue)
                    dt = controlador.CargarPolizasPorFechas(fechaDesde, fechaHasta);
                else
                    dt = controlador.CargarPolizas();

                Dgv_Cuentas.DataSource = dt;

                // Configuración de columnas (coincidiendo exactamente con las devueltas)
                if (dt.Columns.Contains("Pk_EncCodigo_Poliza"))
                    Dgv_Cuentas.Columns["Pk_EncCodigo_Poliza"].HeaderText = "Código Póliza";
                if (dt.Columns.Contains("PkFk_Fecha_Poliza")) // ← ojo: PK mayúsculas
                    Dgv_Cuentas.Columns["PkFk_Fecha_Poliza"].HeaderText = "Fecha";
                if (dt.Columns.Contains("Pk_Codigo_Cuenta")) // ← “cuenta” en minúscula
                    Dgv_Cuentas.Columns["Pk_Codigo_Cuenta"].HeaderText = "Código Cuenta";
                if (dt.Columns.Contains("Cmp_CtaNombre"))
                    Dgv_Cuentas.Columns["Cmp_CtaNombre"].HeaderText = "Nombre de la Cuenta";
                if (dt.Columns.Contains("Cmp_Concepto_Poliza"))
                    Dgv_Cuentas.Columns["Cmp_Concepto_Poliza"].HeaderText = "Concepto";
                if (dt.Columns.Contains("Debe"))
                    Dgv_Cuentas.Columns["Debe"].HeaderText = "Debe";
                if (dt.Columns.Contains("Haber"))
=======
                DataTable dt = controlador.CargarPolizas();
                Dgv_Cuentas.DataSource = dt;

                Dgv_Cuentas.Columns["Pk_EncCodigo_Poliza"].HeaderText = "Código Póliza";
                Dgv_Cuentas.Columns["Pk_Fecha_Poliza"].HeaderText = "Fecha";
                Dgv_Cuentas.Columns["Pk_Codigo_Cuenta"].HeaderText = "Código Cuenta";
                Dgv_Cuentas.Columns["Cmp_CtaNombre"].HeaderText = "Nombre de la Cuenta";
                Dgv_Cuentas.Columns["Cmp_Concepto_Poliza"].HeaderText = "Concepto";            
                
                Dgv_Cuentas.Columns["Debe"].HeaderText = "Debe";
>>>>>>> e42e22a9edf48ad572659449d0ea79beb894f129
                Dgv_Cuentas.Columns["Haber"].HeaderText = "Haber";

                Dgv_Cuentas.AutoResizeColumns();

                CalcularTotales();
            }
<<<<<<< HEAD
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
=======
>>>>>>> e42e22a9edf48ad572659449d0ea79beb894f129
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pólizas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

<<<<<<< HEAD

=======
>>>>>>> e42e22a9edf48ad572659449d0ea79beb894f129
        private void Lbl_observaciones_Click(object sender, EventArgs e)
        {

        }

        private void Lbl_Debe_Click(object sender, EventArgs e)
        {

        }



<<<<<<< HEAD
=======

>>>>>>> e42e22a9edf48ad572659449d0ea79beb894f129
        private void CalcularTotales()
        {
            decimal totalDebe = 0;
            decimal totalHaber = 0;

<<<<<<< HEAD
            // ✅ Validar que existen las columnas antes de usarlas
            if (!Dgv_Cuentas.Columns.Contains("Debe") || !Dgv_Cuentas.Columns.Contains("Haber"))
            {
                MessageBox.Show("Las columnas 'Debe' y 'Haber' no existen en el DataGridView.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ✅ Recorrer solo filas con datos
            foreach (DataGridViewRow fila in Dgv_Cuentas.Rows)
            {
                if (fila.IsNewRow) // ← evita la última fila vacía
                    continue;

                // ✅ Sumar DEBE
                if (fila.Cells["Debe"].Value != null &&
                    decimal.TryParse(fila.Cells["Debe"].Value.ToString(), out decimal debe))
                {
                    totalDebe += debe;
                }

                // ✅ Sumar HABER
                if (fila.Cells["Haber"].Value != null &&
                    decimal.TryParse(fila.Cells["Haber"].Value.ToString(), out decimal haber))
                {
                    totalHaber += haber;
                }
            }

            // ✅ Mostrar totales
            Lbl_TotalDebe.Text = totalDebe.ToString("N2");
            Lbl_TotalHaber.Text = totalHaber.ToString("N2");

=======
            // Recorremos todas las filas del DataGridView

            // Verifica que las columnas existen
            if (!Dgv_Cuentas.Columns.Contains("Pk_Codigo_Cuenta"))
            {
                MessageBox.Show("La columna 'Pk_Codigo_Cuenta' no existe en el DataGridView.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (DataGridViewRow fila in Dgv_Cuentas.Rows)
            {
                if (fila.Cells["Debe"].Value != null && decimal.TryParse(fila.Cells["Debe"].Value.ToString(), out decimal debe))
                    totalDebe += debe;

                if (fila.Cells["Haber"].Value != null && decimal.TryParse(fila.Cells["Haber"].Value.ToString(), out decimal haber))
                    totalHaber += haber;
            }

            // Mostramos los resultados
            Lbl_TotalDebe.Text = totalDebe.ToString("N2");
            Lbl_TotalHaber.Text = totalHaber.ToString("N2");

            // Calculamos la diferencia (resultado del cierre)
>>>>>>> e42e22a9edf48ad572659449d0ea79beb894f129
            decimal totalCierre = totalDebe - totalHaber;
            Lbl_SaldosTotales.Text = totalCierre.ToString("N2");
        }

<<<<<<< HEAD

=======
>>>>>>> e42e22a9edf48ad572659449d0ea79beb894f129
        private void Lbl_TotalHaber_Click(object sender, EventArgs e)
        {

        }

<<<<<<< HEAD


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

                // ✅ Guardar usando una cuenta general
                controlador.InsertarCierreContable(
                    fechaCierre,
                    Cbo_periodo.Text,
                    fechaDesde,
                    fechaHasta,
                    debe,
                    haber,
                    0,                 // saldo anterior no lo calculas aún
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




=======
        private void Btn_Guardar_Click(object sender, EventArgs e)
        {
          
        }
>>>>>>> e42e22a9edf48ad572659449d0ea79beb894f129
    }
}
