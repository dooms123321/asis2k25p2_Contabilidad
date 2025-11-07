using System;
using System.Windows.Forms;
using Capa_controlador;
using System.Globalization;

namespace Capa_vista
{
    public partial class Frm_registro_activo : Form
    {
        private Cls_Depreciacion_Controlador controlador = new Cls_Depreciacion_Controlador();

        public Frm_registro_activo()
        {
            InitializeComponent();
            CargarGruposActivos();
            CargarCuentasContables();
            Rdb_activo.Checked = true;
            Txt_fecha_adquisicion.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        private void DiagnosticoIndiceFueraIntervalo()
        {
            try
            {
                Console.WriteLine("=== DIAGNÓSTICO ÍNDICE FUERA DE INTERVALO ===");

                // 1. Verificar combobox
                Console.WriteLine("1. VERIFICANDO COMBOBOX:");
                Console.WriteLine($"   comboBox1 SelectedIndex: {comboBox1.SelectedIndex}");
                Console.WriteLine($"   comboBox2 SelectedIndex: {comboBox2.SelectedIndex}");
                Console.WriteLine($"   comboBox3 SelectedIndex: {comboBox3.SelectedIndex}");

                Console.WriteLine($"   comboBox1 Items Count: {comboBox1.Items.Count}");
                Console.WriteLine($"   comboBox2 Items Count: {comboBox2.Items.Count}");
                Console.WriteLine($"   comboBox3 Items Count: {comboBox3.Items.Count}");

                if (comboBox1.SelectedItem != null)
                    Console.WriteLine($"   comboBox1 SelectedItem: {comboBox1.SelectedItem.ToString()}");
                if (comboBox2.SelectedItem != null)
                    Console.WriteLine($"   comboBox2 SelectedItem: {comboBox2.SelectedItem.ToString()}");
                if (comboBox3.SelectedItem != null)
                    Console.WriteLine($"   comboBox3 SelectedItem: {comboBox3.SelectedItem.ToString()}");

                // 2. Probar el split de cuentas
                Console.WriteLine("2. PROBANDO SPLIT DE CUENTAS:");
                if (comboBox1.SelectedItem != null)
                {
                    string cuentaCompleta = comboBox1.SelectedItem.ToString();
                    Console.WriteLine($"   Cuenta completa activo: '{cuentaCompleta}'");
                    string[] partes = cuentaCompleta.Split('-');
                    Console.WriteLine($"   Partes después del split: {partes.Length}");
                    for (int i = 0; i < partes.Length; i++)
                    {
                        Console.WriteLine($"     Parte[{i}]: '{partes[i]}'");
                    }
                    if (partes.Length > 0)
                    {
                        string cuentaActivo = partes[0]?.Trim();
                        Console.WriteLine($"   Cuenta extraída: '{cuentaActivo}'");
                    }
                }

                // 3. Probar con datos fijos
                Console.WriteLine("3. PROBANDO CON DATOS FIJOS:");
                try
                {
                    bool resultado = controlador.GuardarNuevoActivo(
                        "PRUEBA SEGURA",
                        "Prueba con datos fijos",
                        "Equipo de Cómputo",
                        DateTime.Now,
                        1000.00m,
                        100.00m,
                        5,
                        "1.5.2",  // Cuenta fija, sin split
                        "1.6.2",  // Cuenta fija, sin split
                        "6.1.5",  // Cuenta fija, sin split
                        true
                    );
                    Console.WriteLine($"✅ Prueba con datos fijos: {(resultado ? "EXITOSA" : "FALLIDA")}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error en prueba fija: {ex.Message}");
                    Console.WriteLine($"   StackTrace: {ex.StackTrace}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR EN DIAGNÓSTICO: {ex.Message}");
                Console.WriteLine($"   StackTrace: {ex.StackTrace}");
            }
        }
        private void CargarGruposActivos()
        {
            try
            {
                string[] grupos = {
                    "Mobiliario",
                    "Equipo de Cómputo",
                    "Vehículos",
                    "Maquinaria",
                    "Equipo de Comunicación",
                    "Herramientas",
                    "Instalaciones",
                    "Software",
                    "Equipo Médico",
                    "Otros"
                };

                Cbo_grupo.Items.Clear();
                Cbo_grupo.Items.AddRange(grupos);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar grupos: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarCuentasContables()
        {
            try
            {
                // Limpiar combobox
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();

                // Obtener cuentas SOLO a través del controlador
                var cuentasActivo = controlador.ObtenerCuentasActivo();
                var cuentasDepreciacion = controlador.ObtenerCuentasDepreciacion();
                var cuentasGasto = controlador.ObtenerCuentasGastoDepreciacion();

                // Cargar cuentas de activo
                foreach (string cuenta in cuentasActivo)
                {
                    comboBox1.Items.Add(cuenta);
                }

                // Cargar cuentas de depreciación
                foreach (string cuenta in cuentasDepreciacion)
                {
                    comboBox2.Items.Add(cuenta);
                }

                // Cargar cuentas de gasto
                foreach (string cuenta in cuentasGasto)
                {
                    comboBox3.Items.Add(cuenta);
                }

                // Seleccionar primeras opciones si hay datos
                if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
                if (comboBox2.Items.Count > 0) comboBox2.SelectedIndex = 0;
                if (comboBox3.Items.Count > 0) comboBox3.SelectedIndex = 0;

                // Si no hay cuentas, mostrar advertencia
                if (comboBox1.Items.Count == 0 || comboBox2.Items.Count == 0 || comboBox3.Items.Count == 0)
                {
                    MessageBox.Show("Advertencia: No se encontraron todas las cuentas contables necesarias en la base de datos.",
                                  "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar cuentas contables: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            // Validar nombre del activo
            if (string.IsNullOrEmpty(Txt_activo_fijo.Text.Trim()))
            {
                MessageBox.Show("El nombre del activo fijo es requerido", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                Txt_activo_fijo.Focus();
                return false;
            }

            // Validar grupo
            if (Cbo_grupo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un grupo", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cbo_grupo.Focus();
                return false;
            }

            // Validar fecha
            if (string.IsNullOrEmpty(Txt_fecha_adquisicion.Text.Trim()))
            {
                MessageBox.Show("La fecha de adquisición es requerida", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                Txt_fecha_adquisicion.Focus();
                return false;
            }

            // Validar cuentas contables
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una cuenta de activo", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox1.Focus();
                return false;
            }

            if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una cuenta de depreciación", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox2.Focus();
                return false;
            }

            if (comboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una cuenta de gasto", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox3.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            Txt_activo_fijo.Clear();
            Txt_descripcion.Clear();
            Txt_costo_adquisicion.Clear();
            Txt_adquisicion.Clear();
            Txt_vida_util.Clear();
            Cbo_grupo.SelectedIndex = -1;

            // Mantener las selecciones de cuentas (ya que vienen de BD)
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            if (comboBox2.Items.Count > 0) comboBox2.SelectedIndex = 0;
            if (comboBox3.Items.Count > 0) comboBox3.SelectedIndex = 0;

            Txt_fecha_adquisicion.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Rdb_activo.Checked = true;
            Txt_activo_fijo.Focus();
        }

        private void Btn_limpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void Txt_costo_adquisicion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void Txt_adquisicion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void Txt_vida_util_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Frm_registro_activo_Load(object sender, EventArgs e)
        {
            Txt_activo_fijo.Focus();
        }

        private void Btn_guardar_Click_1(object sender, EventArgs e)
        {
            // Extraer códigos de cuenta - VERSIÓN SEGURA
            string cuentaActivo = "";
            string cuentaDepreciacion = "";
            string cuentaGasto = "";

            try
            {
                // Para cuenta activo
                if (comboBox1.SelectedItem != null)
                {
                    string cuentaCompleta = comboBox1.SelectedItem.ToString();
                    Console.WriteLine($"Cuenta completa activo: '{cuentaCompleta}'");

                    string[] partes = cuentaCompleta.Split('-');
                    if (partes.Length > 0)
                    {
                        cuentaActivo = partes[0].Trim();
                        Console.WriteLine($"Cuenta activo extraída: '{cuentaActivo}'");
                    }
                    else
                    {
                        // Si no hay split, usar el texto completo
                        cuentaActivo = cuentaCompleta.Trim();
                        Console.WriteLine($"Usando cuenta completa (sin split): '{cuentaActivo}'");
                    }
                }

                // Para cuenta depreciación
                if (comboBox2.SelectedItem != null)
                {
                    string cuentaCompleta = comboBox2.SelectedItem.ToString();
                    Console.WriteLine($"Cuenta completa depreciación: '{cuentaCompleta}'");

                    string[] partes = cuentaCompleta.Split('-');
                    if (partes.Length > 0)
                    {
                        cuentaDepreciacion = partes[0].Trim();
                        Console.WriteLine($"Cuenta depreciación extraída: '{cuentaDepreciacion}'");
                    }
                    else
                    {
                        cuentaDepreciacion = cuentaCompleta.Trim();
                        Console.WriteLine($"Usando cuenta completa (sin split): '{cuentaDepreciacion}'");
                    }
                }

                // Para cuenta gasto
                if (comboBox3.SelectedItem != null)
                {
                    string cuentaCompleta = comboBox3.SelectedItem.ToString();
                    Console.WriteLine($"Cuenta completa gasto: '{cuentaCompleta}'");

                    string[] partes = cuentaCompleta.Split('-');
                    if (partes.Length > 0)
                    {
                        cuentaGasto = partes[0].Trim();
                        Console.WriteLine($"Cuenta gasto extraída: '{cuentaGasto}'");
                    }
                    else
                    {
                        cuentaGasto = cuentaCompleta.Trim();
                        Console.WriteLine($"Usando cuenta completa (sin split): '{cuentaGasto}'");
                    }
                }

                // Validar que tenemos todas las cuentas
                if (string.IsNullOrEmpty(cuentaActivo) || string.IsNullOrEmpty(cuentaDepreciacion) || string.IsNullOrEmpty(cuentaGasto))
                {
                    MessageBox.Show("Error: No se pudieron extraer los códigos de cuenta contable", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar cuentas contables: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"ERROR en split de cuentas: {ex.Message}");
                return;
            }
            DateTime fecha;
            try
            {
                fecha = DateTime.ParseExact(Txt_fecha_adquisicion.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                MessageBox.Show("Formato de fecha inválido. Use el formato AAAA-MM-DD.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                Txt_fecha_adquisicion.Focus();
                return;
            }

            // 👇 Ahora llamas a tu método de guardado usando la variable fecha 👇
            bool resultado = controlador.GuardarNuevoActivo(
                Txt_activo_fijo.Text.Trim(),
                Txt_descripcion.Text.Trim(),
                Cbo_grupo.SelectedItem?.ToString() ?? "",
                fecha, // ← Aquí se usa el DateTime ya validado
                decimal.Parse(Txt_costo_adquisicion.Text),
                decimal.Parse(Txt_adquisicion.Text),
                int.Parse(Txt_vida_util.Text),
                cuentaActivo,
                cuentaDepreciacion,
                cuentaGasto,
                Rdb_activo.Checked
            );

            if (resultado)
            {
                MessageBox.Show("Activo guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al guardar el activo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Txt_fecha_adquisicion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void Txt_fecha_adquisicion_TextChanged(object sender, EventArgs e)
        {
            string text = Txt_fecha_adquisicion.Text.Replace("-", "");

            if (text.Length >= 4)
            {
                string formatted = text.Substring(0, 4);
                if (text.Length > 4)
                {
                    formatted += "-" + text.Substring(4, Math.Min(2, text.Length - 4));
                }
                if (text.Length > 6)
                {
                    formatted += "-" + text.Substring(6, Math.Min(2, text.Length - 6));
                }

                if (Txt_fecha_adquisicion.Text != formatted)
                {
                    Txt_fecha_adquisicion.Text = formatted;
                    Txt_fecha_adquisicion.SelectionStart = Txt_fecha_adquisicion.Text.Length;
                }
            }
        }

        // Método temporal para diagnóstico
        private void ProbarConexion()
        {
            try
            {
                string resultado = controlador.ProbarSistema();
                MessageBox.Show($"Resultado de prueba:\n\n{resultado}", "Prueba del Sistema",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en prueba: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_diagnostico_Click(object sender, EventArgs e)
        {
            try
            {
                // Probar conexión básica
                string resultado = controlador.ProbarSistema();

                // Probar inserción con datos de prueba
                bool prueba = controlador.GuardarNuevoActivo(
                    "EQUIPO PRUEBA",
                    "Equipo de prueba diagnóstico",
                    "Equipo de Cómputo",
                    DateTime.Now,
                    1000.00m,
                    100.00m,
                    5,
                    "1.5.2",  // Asegúrate que esta cuenta existe
                    "1.6.2",  // Asegúrate que esta cuenta existe  
                    "6.1.5",  // Asegúrate que esta cuenta existe
                    true
                );

                MessageBox.Show($"✅ Prueba exitosa: {prueba}\n\nConexión:\n{resultado}",
                              "Diagnóstico", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string errorCompleto = $"Error: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorCompleto += $"\n\nInner Exception: {ex.InnerException.Message}";
                    if (ex.InnerException.InnerException != null)
                    {
                        errorCompleto += $"\n\nInner Inner: {ex.InnerException.InnerException.Message}";
                    }
                }

                MessageBox.Show(errorCompleto, "Error en Diagnóstico",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DiagnosticoODBCEspecifico()
        {
            try
            {
                Console.WriteLine("=== DIAGNÓSTICO ODBC ESPECÍFICO ===");

                // Probar con datos MUY simples
                bool resultado = controlador.GuardarNuevoActivo(
                    "TEST ODBC",
                    "Test",
                    "Equipo de Cómputo",
                    new DateTime(2025, 1, 1), // Fecha fija
                    100.00m,
                    10.00m,
                    3,
                    "1.5.1", // Probar con otra cuenta
                    "1.6.1", // Probar con otra cuenta  
                    "6.1.5",
                    true
                );

                Console.WriteLine($"✅ Resultado: {resultado}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR ODBC: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"🔍 INNER: {ex.InnerException.Message}");
                }
            }
        }

        private void Btn_diagnostico_indice_Click(object sender, EventArgs e)
        {

            try
            {
                Console.WriteLine("=== PRUEBA CORRECCIÓN BIT ===");

                bool resultado = controlador.GuardarNuevoActivo_SQLDirecto(
                    "PRUEBA BIT CORREGIDO",
                    "Prueba de corrección para BIT",
                    "Equipo de Cómputo",
                    new DateTime(2025, 1, 1),
                    500.00m,
                    50.00m,
                    4,
                    "1.5.1",
                    "1.6.1",
                    "6.1.5",
                    true
                );

                MessageBox.Show($"Prueba BIT: {(resultado ? "✅ EXITOSA" : "❌ FALLIDA")}",
                              "Prueba", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en prueba BIT: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"❌ ERROR PRUEBA BIT: {ex.Message}");
            }
        }
    }
}