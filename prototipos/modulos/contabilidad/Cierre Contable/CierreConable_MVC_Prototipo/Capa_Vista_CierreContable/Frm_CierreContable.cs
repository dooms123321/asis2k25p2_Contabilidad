using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

            Cbo_periodo.Items.AddRange(new[] { "Anual", "Mensual" });
            Cbo_periodo.DropDownStyle = ComboBoxStyle.DropDownList;


            // Modo de actualización
            Cbo_actualizacion.Items.Add("En línea"); //1
            Cbo_actualizacion.Items.Add("Batch");    // 0
            Cbo_actualizacion.DropDownStyle = ComboBoxStyle.DropDownList;
            Dtp_fecha_cierre.Enabled = true;

            //Dgv_Cuentas.Enabled = false;
        }

        private void Frm_CierreContable_Load(object sender, EventArgs e)
        {

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
            string sPeriodoSeleccionado = Cbo_periodo.SelectedItem.ToString();

            switch (sPeriodoSeleccionado)
            {
                case "Anual":
                    MessageBox.Show(
                        " CIERRE ANUAL SELECCIONADO\n\n" +
                        "El cierre anual tomará en cuenta TODAS las pólizas registradas durante el año seleccionado, " +
                        "aunque solo existan operaciones en algunos meses.\n\n" +
                        " Se guardará un único cierre para el año completo.\n" +
                        " No importa si algunos meses no tienen movimientos.\n",
                        "Cierre Contable - Modo Anual",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    break;

                case "Mensual":
                    MessageBox.Show(
                        " CIERRE MENSUAL SELECCIONADO\n\n" +
                        "El cierre procesará únicamente los movimientos del mes seleccionado.",
                        "Cierre Contable - Modo Mensual",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    break;

                case "Diario":
                    MessageBox.Show(
                        " CIERRE DIARIO SELECCIONADO\n\n" +
                        "Solo se tomarán en cuenta las pólizas del día seleccionado.",
                        "Cierre Contable - Modo Diario",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    break;
            }


        }

        private void Lbl_fecha_desde_Click(object sender, EventArgs e)
        {

        }

        private void Dtp_fecha_cierre_ValueChanged(object sender, EventArgs e)
        {
            Dtp_fecha_cierre.Format = DateTimePickerFormat.Short;



        }

        private void Dgv_Cuentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }








        private void Btn_cargar_Click(object sender, EventArgs e)
        {
            DateTime desde = Dtp_fecha_desde.Value.Date;
            DateTime hasta = Dtp_Fecha_Hasta.Value.Date;

            if (desde > hasta)
            {
                MessageBox.Show("La fecha inicial no puede ser mayor a la final");
                return;
            }

            DataTable dt = controlador.CargarPolizasPorFecha(desde, hasta);
            Dgv_Cuentas.DataSource = dt;
            Dgv_Cuentas.AutoResizeColumns();

            CalcularTotales();
        }



        private void Lbl_observaciones_Click(object sender, EventArgs e)
        {

        }

        private void Lbl_Debe_Click(object sender, EventArgs e)
        {

        }




        private void CalcularTotales()
        {
            decimal deTotalDebe = 0;
            decimal deTotalHaber = 0;

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
                    deTotalDebe += debe;

                if (fila.Cells["Haber"].Value != null && decimal.TryParse(fila.Cells["Haber"].Value.ToString(), out decimal haber))
                    deTotalHaber += haber;
            }

            // Mostramos los resultados
            Lbl_TotalDebe.Text = deTotalDebe.ToString("N2");
            Lbl_TotalHaber.Text = deTotalHaber.ToString("N2");

            // Calculamos la diferencia (resultado del cierre)
            decimal totalCierre = deTotalDebe - deTotalHaber;
            Lbl_SaldosTotales.Text = totalCierre.ToString("N2");
        }

        private void Lbl_TotalHaber_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Guardar_Click(object sender, EventArgs e)
        {
            int iAnio = Dtp_fecha_cierre.Value.Year;
            int iMes = Dtp_fecha_cierre.Value.Month;
            string periodo = Cbo_periodo.SelectedItem.ToString();

            if (controlador.ExisteHistoricoPeriodo(iAnio, iMes))
            {
                MessageBox.Show("Ya existe un cierre contable para este periodo.");
                return;
            }

            if (Cbo_actualizacion.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar el modo de actualización.");
                return;
            }

            List<string> codigos = new List<string>();
            foreach (DataGridViewRow fila in Dgv_Cuentas.Rows)
                if (!fila.IsNewRow && fila.Cells[1].Value != null)
                    codigos.Add(fila.Cells[1].Value.ToString().Trim());

            if (codigos.Count == 0)
            {
                MessageBox.Show("No hay cuentas cargadas.");
                return;
            }

            //  PRIMERO registrar PERÍODO (solo si no existe)
            DateTime dInicio = new DateTime(iAnio, iMes, 1);
            DateTime dFin = dInicio.AddMonths(1).AddDays(-1);
            int iEstado = 1;
            int iModo = (Cbo_actualizacion.SelectedItem.ToString() == "En línea") ? 1 : 0;

            controlador.CerrarPeriodosAnteriores(iAnio, iMes);

            if (!controlador.ExistePeriodoContable(iAnio, iMes))
            {
                controlador.RegistrarPeriodo(iAnio, iMes, dInicio, dFin, iEstado, iModo);
            }

            //  AHORA guardar HISTÓRICO
            if (!controlador.GuardarHistoricoDesdeLista(iAnio, iMes, codigos))
            {
                MessageBox.Show("Error al guardar el histórico.");
                return;
            }

            MessageBox.Show(" Cierre contable registrado correctamente.");
        }


        private void Dtp_Fecha_Hasta_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Cbo_actualizacion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Btn_salir_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void Btn_imprimir_Click(object sender, EventArgs e)
        {
            Frm_reporte frm = new Frm_reporte();
            frm.Show();
        }

    }
}
