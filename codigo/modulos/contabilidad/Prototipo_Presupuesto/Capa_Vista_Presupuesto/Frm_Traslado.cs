// Capa_Vista_Presupuesto/Frm_Traslado.cs

using System;
using System.Data;
using System.Windows.Forms;
using Capa_Modelo_Presupuesto;

namespace Capa_Vista_Presupuesto
{
    public partial class Frm_Traslado : Form
    {
        // Variables para el período activo
        private int anioActivo;
        private int mesActivo;

        // Mapeo de controles (Asumimos Cmb_1, Cmb_2, Txt_1, Txt_2 existen en el diseño)
        private ComboBox Cmb_Origen => Cmb_1; // Combobox de la cuenta que cede (Origen)
        private ComboBox Cmb_Destino => Cmb_2; // Combobox de la cuenta que recibe (Destino)
        private TextBox Txt_Monto => Txt_1;
        private TextBox Txt_Descripcion => Txt_2; // ESTE DEBE EXISTIR en el diseño

        public Frm_Traslado(int anio, int mes)
        {
            InitializeComponent();
            this.anioActivo = anio;
            this.mesActivo = mes;
            CargarCuentas();
        }

        private void CargarCuentas()
        {
            try
            {
                // Solo cargamos cuentas de Gasto/Costo que pueden tener presupuesto
                // Usamos el método que ya tienes en Capa_Modelo_Presupuesto/Conexion.cs
                DataTable dtCuentas = Conexion.ObtenerCuentasPresupuestarias();

                if (dtCuentas != null && dtCuentas.Rows.Count > 0)
                {
                    // Origen
                    Cmb_Origen.DataSource = dtCuentas.Copy(); // Importante: usar una copia
                    Cmb_Origen.DisplayMember = "NombreCompleto";
                    Cmb_Origen.ValueMember = "Pk_Codigo_Cuenta";

                    // Destino
                    Cmb_Destino.DataSource = dtCuentas.Copy(); // Otra copia
                    Cmb_Destino.DisplayMember = "NombreCompleto";
                    Cmb_Destino.ValueMember = "Pk_Codigo_Cuenta";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar cuentas para traslado: {ex.Message}");
            }
        }

        private void Btn_1_Click(object sender, EventArgs e) // Botón Confirmar
        {
            // 1. Validaciones
            if (Cmb_Origen.SelectedValue == null || Cmb_Destino.SelectedValue == null || string.IsNullOrEmpty(Txt_Monto.Text))
            {
                MessageBox.Show("Complete el monto y seleccione ambas Cuentas (Origen y Destino).", "Advertencia");
                return;
            }

            if (!decimal.TryParse(Txt_Monto.Text, out decimal monto) || monto <= 0)
            {
                MessageBox.Show("Ingrese una cantidad (monto) válida y positiva.", "Advertencia");
                return;
            }

            string codigoOrigen = Cmb_Origen.SelectedValue.ToString();
            string codigoDestino = Cmb_Destino.SelectedValue.ToString();

            if (codigoOrigen == codigoDestino)
            {
                MessageBox.Show("Las Cuentas de Origen y Destino deben ser diferentes.", "Advertencia");
                return;
            }

            // Usamos la descripción del TextBox, o un texto genérico si está vacío
            string descripcion = string.IsNullOrEmpty(Txt_Descripcion.Text) ? $"Traslado presupuestario de {codigoOrigen} a {codigoDestino}" : Txt_Descripcion.Text;

            // 2. Ejecutar la lógica de traslado en la base de datos (modelo)
            if (Conexion.TrasladarMontoPresupuestario(codigoOrigen, codigoDestino, anioActivo, mesActivo, monto, descripcion))
            {
                MessageBox.Show($"Traslado de Q{monto:N2} realizado con éxito.", "Registro Completo");
                this.DialogResult = DialogResult.OK; // Indica al Frm_Principal que actualice
                this.Close();
            }
            else
            {
                MessageBox.Show("ERROR: No se pudo realizar el traslado. Verifique que la Cuenta de Origen tenga saldo suficiente.", "Error de Registro");
            }
        }

        private void Btn_2_Click(object sender, EventArgs e) // Botón Cancelar
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}