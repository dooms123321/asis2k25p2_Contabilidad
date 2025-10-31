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

            Cbo_periodo.Items.AddRange(new[] { "Anual", "Mensual", "Diario" });
            Cbo_periodo.DropDownStyle = ComboBoxStyle.DropDownList;

            Dtp_fecha_cierre.Enabled = false;

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
            CargarPolizas();
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
                Dgv_Cuentas.Columns["Cmp_CtaNombre"].HeaderText = "Nombre de la Cuenta";
                Dgv_Cuentas.Columns["Cmp_Concepto_Poliza"].HeaderText = "Concepto";            
                
                Dgv_Cuentas.Columns["Debe"].HeaderText = "Debe";
                Dgv_Cuentas.Columns["Haber"].HeaderText = "Haber";

                Dgv_Cuentas.AutoResizeColumns();

                CalcularTotales();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pólizas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Lbl_observaciones_Click(object sender, EventArgs e)
        {

        }

        private void Lbl_Debe_Click(object sender, EventArgs e)
        {

        }




        private void CalcularTotales()
        {
            decimal totalDebe = 0;
            decimal totalHaber = 0;

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
            decimal totalCierre = totalDebe - totalHaber;
            Lbl_SaldosTotales.Text = totalCierre.ToString("N2");
        }

        private void Lbl_TotalHaber_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Guardar_Click(object sender, EventArgs e)
        {
          
        }
    }
}
