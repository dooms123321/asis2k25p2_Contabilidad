// EN Capa_Vista_Presupuesto/Frm_Ejecucion.cs

using System;
using System.Data;
using System.Windows.Forms;
using Capa_Modelo_Presupuesto; // Referencia NECESARIA

namespace Capa_Vista_Presupuesto
{
    public partial class Frm_Ejecucion : Form
    {
        // Variables para el período activo
        private int anioActivo;
        private int mesActivo;
        private int idUsuarioActual; // <--- ¡MANTENEMOS ESTA VARIABLE!

        // Mapeo de controles (deben coincidir con tu diseño)
        private ComboBox Cmb_Cuenta => Cmb_1;        // Cuenta de Gasto
        private TextBox Txt_Monto => Txt_1;            // Monto a Ejecutar
        private TextBox Txt_Descripcion => Txt_2;    // Descripción del Gasto

        // CONSTRUCTOR CORREGIDO: Acepta el ID del usuario
        public Frm_Ejecucion(int anio, int mes, int idUsuario) // <--- ¡PARÁMETRO AÑADIDO!
        {
            InitializeComponent();
            this.anioActivo = anio;
            this.mesActivo = mes;
            this.idUsuarioActual = idUsuario; // <--- ¡GUARDANDO EL ID!
            CargarCuentas();
        }

        // ... (El resto del método CargarCuentas se mantiene igual) ...

        // Evento Click para el botón Confirmar (Btn_1)
        private void Btn_1_Click(object sender, EventArgs e)
        {
            // 1. Validaciones...
            if (Cmb_Cuenta.SelectedValue == null || string.IsNullOrEmpty(Txt_Monto.Text))
            {
                MessageBox.Show("Complete el campo Monto y seleccione la Cuenta de Gasto.", "Advertencia");
                return;
            }

            if (!decimal.TryParse(Txt_Monto.Text, out decimal monto) || monto <= 0)
            {
                MessageBox.Show("Ingrese una cantidad (monto) válida y positiva.", "Advertencia");
                return;
            }

            // 2. Extracción de datos
            string codigoCuenta = Cmb_Cuenta.SelectedValue.ToString();
            string descripcion = string.IsNullOrEmpty(Txt_Descripcion.Text) ?
                                     $"Ejecución presupuestaria de Q{monto:N2} en {codigoCuenta}" :
                                     Txt_Descripcion.Text;

            // 3. Ejecutar la lógica de gasto/ejecución (¡LLAMANDO DIRECTO AL MODELO!)

            // LA LLAMADA AHORA TIENE 6 PARÁMETROS:
            if (Conexion.RegistrarEjecucionPresupuestaria(
                codigoCuenta,
                anioActivo,
                mesActivo,
                monto,
                descripcion,
                this.idUsuarioActual)) // <--- ¡EL ID DE USUARIO ES EL PARÁMETRO FALTANTE!
            {
                MessageBox.Show($"Gasto de Q{monto:N2} registrado con éxito.", "Registro Completo");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("ERROR: No se pudo registrar la ejecución. Revise el saldo disponible en el período o la conexión a la DB.", "Error de Registro");
            }
        }

        // Evento Click para el botón Cancelar (Btn_2)
        private void Btn_2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}