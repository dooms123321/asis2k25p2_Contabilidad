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
        public Frm_CierreContable()
        {
            InitializeComponent();

            Cbo_periodo.Items.Add("Anual");
            Cbo_periodo.Items.Add("Mensual");
            Cbo_periodo.Items.Add("Diario");

    
            Cbo_periodo.DropDownStyle = ComboBoxStyle.DropDownList;
            Dtp_fecha_cierre.Enabled = false;
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
    }
}
