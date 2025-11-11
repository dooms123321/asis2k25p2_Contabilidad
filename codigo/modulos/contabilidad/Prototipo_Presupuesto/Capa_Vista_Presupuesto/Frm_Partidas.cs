using System;
using System.Data;
using System.Windows.Forms;
using Capa_Modelo_Presupuesto;

namespace Capa_Vista_Presupuesto
{
    public partial class Frm_Partidas : Form
    {
        // Variables para el período activo
        private int anioActivo;
        private int mesActivo;

        // Mapeo de controles según tu diseño:
        // 'Area a Designar'
        private ComboBox Cmb_Cuenta => Cmb_1;
        // 'Cantidad'
        private TextBox Txt_Monto => Txt_1;
        // 'Descripción' (Asumo que Txt_2 es el campo de Descripción)
        private TextBox Txt_Descripcion => Txt_2;


        // ==========================================================
        // CONSTRUCTOR
        // ==========================================================
        public Frm_Partidas(int anio, int mes)
        {
            InitializeComponent();
            this.anioActivo = anio;
            this.mesActivo = mes;
            CargarCuentas();
        }

        // ==========================================================
        // LÓGICA DE CARGA DE CUENTAS (DEL CATÁLOGO)
        // ==========================================================
        private void CargarCuentas()
        {
            try
            {
                Cmb_Cuenta.Items.Clear();

                // Consulta para cargar TODAS las cuentas de Gasto/Costo
                string queryCuentas = $@"
                    SELECT 
                        Pk_Codigo_Cuenta,
                        CONCAT(Pk_Codigo_Cuenta, ' - ', Cmp_CtaNombre) AS NombreCompleto
                    FROM Tbl_Catalogo_Cuentas
                    WHERE Pk_Codigo_Cuenta LIKE '5.%' OR Pk_Codigo_Cuenta LIKE '6.%' 
                    ORDER BY Pk_Codigo_Cuenta;";

                DataTable dtCuentas = Conexion.EjecutarConsulta(queryCuentas);

                if (dtCuentas != null && dtCuentas.Rows.Count > 0)
                {
                    Cmb_Cuenta.DataSource = dtCuentas;
                    Cmb_Cuenta.DisplayMember = "NombreCompleto";
                    // Usamos el Código de la Cuenta como valor clave
                    Cmb_Cuenta.ValueMember = "Pk_Codigo_Cuenta";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar cuentas contables: {ex.Message}");
            }
        }

        // ==========================================================
        // EVENTOS Y LÓGICA DE REGISTRO (ASIGNACIÓN INICIAL)
        // ==========================================================

        private void Btn_2_Click(object sender, EventArgs e) // Botón Cancelar
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Btn_1_Click(object sender, EventArgs e) // Botón Confirmar
        {
            // 1. Validaciones
            if (Cmb_Cuenta.SelectedValue == null || string.IsNullOrEmpty(Txt_Monto.Text))
            {
                MessageBox.Show("Complete el campo Monto y seleccione una Cuenta.", "Advertencia");
                return;
            }

            if (!decimal.TryParse(Txt_Monto.Text, out decimal monto) || monto <= 0)
            {
                MessageBox.Show("Ingrese una cantidad (monto) válida y positiva.", "Advertencia");
                return;
            }

            string codigoCuenta = Cmb_Cuenta.SelectedValue.ToString();
            string descripcion = string.IsNullOrEmpty(Txt_Descripcion.Text) ?
                                 "Asignación de partida inicial." :
                                 Txt_Descripcion.Text;

            // 2. Ejecutar la lógica de asignación inicial en la base de datos
            // Esta función la crearemos en el paso 2
            if (Conexion.RegistrarPartidaInicial(codigoCuenta, anioActivo, mesActivo, monto, descripcion))
            {
                MessageBox.Show("Partida presupuestaria inicial registrada con éxito.", "Registro Completo");
                this.DialogResult = DialogResult.OK; // Indica al Frm_Principal que actualice
                this.Close();
            }
            else
            {
                MessageBox.Show("ERROR: No se pudo registrar la partida inicial. Verifique la conexión y el periodo.", "Error de Registro");
            }
        }

        // El método RegistrarEjecucion antiguo se elimina, la lógica se mueve a Conexion.cs
    }
}