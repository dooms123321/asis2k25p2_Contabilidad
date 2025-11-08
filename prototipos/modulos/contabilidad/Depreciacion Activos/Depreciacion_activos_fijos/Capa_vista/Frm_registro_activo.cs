using System;
using System.Windows.Forms;
using System.Data;
using Capa_controlador;

namespace Capa_vista
{
    public partial class Frm_registro_activo : Form
    {
        private Cls_Depreciacion_Controlador controlador = new Cls_Depreciacion_Controlador();
        public int IdActivoSeleccionado { get; private set; }
        public string NombreActivoSeleccionado { get; private set; }

        public Frm_registro_activo()
        {
            InitializeComponent();
            CargarGruposActivos();
            CargarCuentasContables();
            Rdb_activo.Checked = true;
            Txt_fecha_adquisicion.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Cbo_seleccion_activo.SelectedIndexChanged += Cbo_seleccion_activo_SelectedIndexChanged;
            ConfigurarTabDepreciacion();
            ConfigurarGridDepreciacion();
        }

        private void ConfigurarGridDepreciacion()
        {
            try
            {
                Dgv_depreciacion_lineal.Columns.Clear();

                // Agregar columnas si no existen
                if (Dgv_depreciacion_lineal.Columns.Count == 0)
                {
                    Dgv_depreciacion_lineal.Columns.Add("Año", "Año");
                    Dgv_depreciacion_lineal.Columns.Add("ValorEnLibros", "Valor en Libros");
                    Dgv_depreciacion_lineal.Columns.Add("DepreciacionAnual", "Depreciación Anual");
                    Dgv_depreciacion_lineal.Columns.Add("DepreciacionAcumulada", "Depreciación Acumulada");

                    // Configurar anchos de columnas
                    Dgv_depreciacion_lineal.Columns["Año"].Width = 80;
                    Dgv_depreciacion_lineal.Columns["ValorEnLibros"].Width = 150;
                    Dgv_depreciacion_lineal.Columns["DepreciacionAnual"].Width = 150;
                    Dgv_depreciacion_lineal.Columns["DepreciacionAcumulada"].Width = 150;
                }

                Console.WriteLine("Grid de depreciación configurado correctamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error configurando grid: {ex.Message}");
            }
        }

        private void ConfigurarTabDepreciacion()
        {
            // Cargar activos en el ComboBox
            CargarActivosEnComboBox();

            // Configurar el grid de depreciación
            ConfigurarGridDepreciacion();

            // Configurar valores iniciales de labels
            Lbl_activo_gpb.Text = "---";
            Lbl_grupo_gpb.Text = "---";
            Lbl_adquisicion.Text = "---";
            Lbl_resultado.Text = "---";

            Tbc_calculo_activo_fijo.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
        }
        private void VerDatosActivoEnBD(int idActivo)
        {
            try
            {
                Console.WriteLine($"=== VERIFICANDO DATOS EN BD PARA ACTIVO {idActivo} ===");

                string[] datos = controlador.ObtenerDatosActivo(idActivo);
                if (datos != null && datos.Length >= 5)
                {
                    Console.WriteLine($"Nombre: {datos[0]}");
                    Console.WriteLine($"Grupo: {datos[1]}");
                    Console.WriteLine($"Costo: {datos[2]}");
                    Console.WriteLine($"Valor Residual: {datos[3]}");
                    Console.WriteLine($"Vida Útil: {datos[4]} años");

                    // Convertir a valores numéricos para verificación
                    if (decimal.TryParse(datos[2].Replace("Q", "").Replace("$", "").Replace(",", ""), out decimal costo) &&
                        decimal.TryParse(datos[3].Replace("Q", "").Replace("$", "").Replace(",", ""), out decimal residual) &&
                        int.TryParse(datos[4], out int vidaUtil))
                    {
                        Console.WriteLine($"Valores numéricos - Costo: {costo}, Residual: {residual}, Vida Útil: {vidaUtil}");

                        // Verificar que los datos sean válidos para el cálculo
                        if (costo <= 0)
                            Console.WriteLine("ERROR: El costo debe ser mayor a 0");
                        if (residual < 0)
                            Console.WriteLine("ERROR: El valor residual no puede ser negativo");
                        if (residual >= costo)
                            Console.WriteLine("ERROR: El valor residual no puede ser mayor o igual al costo");
                        if (vidaUtil <= 0)
                            Console.WriteLine("ERROR: La vida útil debe ser mayor a 0");
                    }
                }
                else
                {
                    Console.WriteLine("No se pudieron obtener los datos del activo");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al ver datos: {ex.Message}");
            }
        }

        private void CargarActivosEnComboBox()
        {
            try
            {
                DataTable dtActivos = controlador.ObtenerActivosParaCombo();

                Console.WriteLine($"Número de activos cargados: {dtActivos.Rows.Count}");

                if (dtActivos.Rows.Count > 0)
                {
                    Cbo_seleccion_activo.DataSource = dtActivos;
                    Cbo_seleccion_activo.DisplayMember = "Cmp_Nombre_Activo";
                    Cbo_seleccion_activo.ValueMember = "Pk_Activo_ID";

                    Console.WriteLine($"Primer activo: ID={dtActivos.Rows[0]["Pk_Activo_ID"]}, Nombre={dtActivos.Rows[0]["Cmp_Nombre_Activo"]}");
                }
                else
                {
                    MessageBox.Show("No hay activos registrados en la base de datos.", "Información",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar activos: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error detallado: {ex.ToString()}");
            }
        }

        private void Cbo_seleccion_activo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine($"=== SelectedIndexChanged ===");
            Console.WriteLine($"SelectedValue: {Cbo_seleccion_activo.SelectedValue}");
            Console.WriteLine($"SelectedValue tipo: {Cbo_seleccion_activo.SelectedValue?.GetType()}");
            Console.WriteLine($"SelectedIndex: {Cbo_seleccion_activo.SelectedIndex}");

            int idActivo = 0;

            // Manejar diferentes tipos de SelectedValue
            if (Cbo_seleccion_activo.SelectedValue != null)
            {
                if (Cbo_seleccion_activo.SelectedValue is DataRowView rowView)
                {
                    // Cuando SelectedValue es DataRowView
                    idActivo = Convert.ToInt32(rowView["Pk_Activo_ID"]);
                    Console.WriteLine($"Convertido desde DataRowView: {idActivo}");
                }
                else if (Cbo_seleccion_activo.SelectedValue is int)
                {
                    // Cuando SelectedValue es int directamente
                    idActivo = (int)Cbo_seleccion_activo.SelectedValue;
                    Console.WriteLine($"Convertido desde int: {idActivo}");
                }
                else
                {
                    // Intentar conversión desde string u otros tipos
                    if (int.TryParse(Cbo_seleccion_activo.SelectedValue.ToString(), out int tempId))
                    {
                        idActivo = tempId;
                        Console.WriteLine($"Convertido desde string: {idActivo}");
                    }
                    else
                    {
                        Console.WriteLine("No se pudo convertir SelectedValue a int");
                    }
                }
            }

            if (idActivo > 0)
            {
                Console.WriteLine($"ID Activo seleccionado: {idActivo}");
                MostrarDatosActivo(idActivo);
                CargarDepreciacionesExistentes(idActivo);
            }
            else
            {
                Console.WriteLine("ID Activo no válido (probablemente el elemento de selección)");
                LimpiarCamposDepreciacion();
            }
        }

        private void MostrarDatosActivo(int idActivo)
        {
            try
            {
                Console.WriteLine($"Buscando datos para activo ID: {idActivo}");

                string[] datos = controlador.ObtenerDatosActivo(idActivo);

                Console.WriteLine($"Datos recibidos: {datos != null}, Cantidad: {datos?.Length}");

                if (datos != null && datos.Length >= 3)
                {
                    // Mostrar en los GroupBox
                    Lbl_activo_gpb.Text = datos[0]; // Nombre del Activo
                    Lbl_grupo_gpb.Text = datos[1];  // Grupo
                    Lbl_adquisicion.Text = datos[2]; // Costo de Adquisición

                    Console.WriteLine($"Datos cargados en labels:");
                    Console.WriteLine($"  Activo: {datos[0]}");
                    Console.WriteLine($"  Grupo: {datos[1]}");
                    Console.WriteLine($"  Costo: {datos[2]}");

                    // Forzar actualización de la interfaz
                    Lbl_activo_gpb.Refresh();
                    Lbl_grupo_gpb.Refresh();
                    Lbl_adquisicion.Refresh();
                }
                else
                {
                    Console.WriteLine("Datos insuficientes o nulos");
                    LimpiarCamposDepreciacion();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en MostrarDatosActivo: {ex.Message}");
                MessageBox.Show($"Error al cargar datos del activo: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                LimpiarCamposDepreciacion();
            }
        }

        private void CargarDepreciacionesExistentes(int idActivo)
        {
            try
            {
                Console.WriteLine($"Cargando depreciaciones existentes para activo {idActivo}");

                DataTable dtDepreciaciones = controlador.ObtenerDepreciacionesExistentes(idActivo);

                Console.WriteLine($"Depreciaciones encontradas: {dtDepreciaciones.Rows.Count}");

                if (dtDepreciaciones.Rows.Count > 0)
                {
                    MostrarDepreciacionesEnGrid(dtDepreciaciones);
                    decimal depreciacionTotal = controlador.ObtenerDepreciacionTotal(idActivo);
                    Lbl_resultado.Text = depreciacionTotal.ToString("C2");

                    Console.WriteLine($"Depreciación total cargada: {depreciacionTotal:C2}");
                }
                else
                {
                    LimpiarGridDepreciacion();
                    Lbl_resultado.Text = "0.00";
                    Console.WriteLine("No hay depreciaciones guardadas para este activo");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar depreciaciones existentes: {ex.Message}");
                LimpiarGridDepreciacion();
                Lbl_resultado.Text = "0.00";
            }
        }

        private void MostrarDepreciacionesEnGrid(DataTable dtDepreciaciones)
        {
            try
            {
                Console.WriteLine($"Mostrando {dtDepreciaciones.Rows.Count} filas en el grid");

                Dgv_depreciacion_lineal.Rows.Clear();

                if (dtDepreciaciones.Rows.Count == 0)
                {
                    Console.WriteLine("No hay datos para mostrar en el grid");
                    return;
                }

                foreach (DataRow row in dtDepreciaciones.Rows)
                {
                    int index = Dgv_depreciacion_lineal.Rows.Add();
                    Dgv_depreciacion_lineal.Rows[index].Cells["Año"].Value = row["Año"];
                    Dgv_depreciacion_lineal.Rows[index].Cells["ValorEnLibros"].Value = row["ValorEnLibros"];
                    Dgv_depreciacion_lineal.Rows[index].Cells["DepreciacionAnual"].Value = row["DepreciacionAnual"];
                    Dgv_depreciacion_lineal.Rows[index].Cells["DepreciacionAcumulada"].Value = row["DepreciacionAcumulada"];
                }

                Console.WriteLine("Grid actualizado correctamente");

                // Forzar actualización visual
                Dgv_depreciacion_lineal.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al mostrar datos en grid: {ex.Message}");
                throw;
            }
        }

        private void LimpiarGridDepreciacion()
        {
            Dgv_depreciacion_lineal.Rows.Clear();
        }

        private void LimpiarCamposDepreciacion()
        {
            Lbl_activo_gpb.Text = "---";
            Lbl_grupo_gpb.Text = "---";
            Lbl_adquisicion.Text = "---";
            LimpiarGridDepreciacion();
            Lbl_resultado.Text = "---";
        }

        // Evento del botón Calcular
        // Evento del botón Calcular
        // Evento del botón Calcular
        private void Btn_calcular_activo_fijo_Click(object sender, EventArgs e)
        {
            int idActivo = 0;

            // Usar la misma lógica de conversión que en SelectedIndexChanged
            if (Cbo_seleccion_activo.SelectedValue != null)
            {
                if (Cbo_seleccion_activo.SelectedValue is DataRowView rowView)
                {
                    idActivo = Convert.ToInt32(rowView["Pk_Activo_ID"]);
                }
                else if (Cbo_seleccion_activo.SelectedValue is int)
                {
                    idActivo = (int)Cbo_seleccion_activo.SelectedValue;
                }
                else if (int.TryParse(Cbo_seleccion_activo.SelectedValue.ToString(), out int tempId))
                {
                    idActivo = tempId;
                }
            }

            if (idActivo == 0)
            {
                MessageBox.Show("Por favor seleccione un activo primero.", "Advertencia",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Console.WriteLine($"=== INICIANDO CÁLCULO DESDE INTERFAZ ===");
                Console.WriteLine($"Calculando depreciación para activo ID: {idActivo}");

                // Verificar datos del activo antes de calcular
                VerDatosActivoEnBD(idActivo);

                // Calcular depreciación
                DataTable dtDepreciaciones = controlador.CalcularDepreciacionLineal(idActivo);

                Console.WriteLine($"Cálculo completado. Filas generadas: {dtDepreciaciones.Rows.Count}");

                if (dtDepreciaciones.Rows.Count == 0)
                {
                    MessageBox.Show("No se generaron datos de depreciación. Verifique los datos del activo.", "Información",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Mostrar en el grid
                MostrarDepreciacionesEnGrid(dtDepreciaciones);

                // Calcular y mostrar depreciación total
                decimal depreciacionTotal = 0;
                if (dtDepreciaciones.Rows.Count > 0)
                {
                    // La depreciación total es la depreciación acumulada del último año
                    string ultimaDepreciacion = dtDepreciaciones.Rows[dtDepreciaciones.Rows.Count - 1]["DepreciacionAcumulada"].ToString();

                    // Limpiar el formato de moneda para convertir a decimal
                    string valorLimpio = ultimaDepreciacion.Replace("Q", "").Replace("$", "").Replace(",", "").Trim();
                    if (decimal.TryParse(valorLimpio, out decimal total))
                    {
                        depreciacionTotal = total;
                    }

                    Lbl_resultado.Text = depreciacionTotal.ToString("C2");

                    Console.WriteLine($"Depreciación total: {depreciacionTotal:C2}");
                }

                MessageBox.Show($"Cálculo de depreciación completado.\n\n" +
                               $"Activo: {Cbo_seleccion_activo.Text}\n" +
                               $"Depreciación total: {depreciacionTotal:C2}\n" +
                               $"Período: {dtDepreciaciones.Rows.Count} años",
                               "Cálculo Completado",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR en cálculo desde interfaz: {ex.Message}");
                MessageBox.Show($"Error al calcular depreciación: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento del botón Guardar Cálculo
        // Evento del botón Guardar Cálculo
        private void Btn_guardar_calculo_Click(object sender, EventArgs e)
        {
            int idActivo = 0;

            // Usar la misma lógica de conversión que en los otros métodos
            if (Cbo_seleccion_activo.SelectedValue != null)
            {
                if (Cbo_seleccion_activo.SelectedValue is DataRowView rowView)
                {
                    idActivo = Convert.ToInt32(rowView["Pk_Activo_ID"]);
                }
                else if (Cbo_seleccion_activo.SelectedValue is int)
                {
                    idActivo = (int)Cbo_seleccion_activo.SelectedValue;
                }
                else if (int.TryParse(Cbo_seleccion_activo.SelectedValue.ToString(), out int tempId))
                {
                    idActivo = tempId;
                }
            }

            if (idActivo == 0)
            {
                MessageBox.Show("No hay activo seleccionado.", "Advertencia",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Console.WriteLine($"=== INICIANDO GUARDADO DESDE INTERFAZ ===");

                // Verificar que hay datos calculados
                if (Dgv_depreciacion_lineal.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos de depreciación calculados. Por favor, calcule primero.", "Advertencia",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Preguntar confirmación
                DialogResult resultado = MessageBox.Show(
                    $"¿Está seguro de guardar el cálculo de depreciación para '{Cbo_seleccion_activo.Text}'?\n\n" +
                    "Esto reemplazará cualquier cálculo anterior para este activo.",
                    "Confirmar Guardado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado != DialogResult.Yes)
                {
                    Console.WriteLine("Guardado cancelado por el usuario");
                    return;
                }

                // Guardar el cálculo
                bool exito = controlador.GuardarCalculoDepreciacion(idActivo);

                if (exito)
                {
                    MessageBox.Show("Cálculo de depreciación guardado correctamente en la base de datos.", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Recargar las depreciaciones existentes para mostrar los datos guardados
                    CargarDepreciacionesExistentes(idActivo);

                    Console.WriteLine("Datos recargados después del guardado");
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el cálculo.", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR al guardar desde interfaz: {ex.Message}");
                MessageBox.Show($"Error al guardar cálculo: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento del botón Limpiar
        private void Btn_limpial_calculo_Click(object sender, EventArgs e)
        {
            LimpiarCamposDepreciacion();
            Cbo_seleccion_activo.SelectedIndex = 0;
        }

        // MÉTODOS ORIGINALES DE LA PESTAÑA DE REGISTRO (NO DUPLICAR)
        private void CargarGruposActivos()
        {
            try
            {
                string[] grupos = {
                    "Mobiliario", "Equipo de Cómputo", "Vehículos", "Maquinaria",
                    "Equipo de Comunicación", "Herramientas", "Instalaciones",
                    "Software", "Equipo Médico", "Otros"
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
                Cbo_cuenta_activo.Items.Clear();
                Cbo_cuenta_depreciacion.Items.Clear();
                Cbo_gastos_depreciacoin.Items.Clear();

                // Obtener cuentas a través del controlador
                var cuentasActivo = controlador.ObtenerCuentasActivo();
                var cuentasDepreciacion = controlador.ObtenerCuentasDepreciacion();
                var cuentasGasto = controlador.ObtenerCuentasGastoDepreciacion();

                // Cargar cuentas
                Cbo_cuenta_activo.Items.AddRange(cuentasActivo.ToArray());
                Cbo_cuenta_depreciacion.Items.AddRange(cuentasDepreciacion.ToArray());
                Cbo_gastos_depreciacoin.Items.AddRange(cuentasGasto.ToArray());

                // Seleccionar primeras opciones
                if (Cbo_cuenta_activo.Items.Count > 0) Cbo_cuenta_activo.SelectedIndex = 0;
                if (Cbo_cuenta_depreciacion.Items.Count > 0) Cbo_cuenta_depreciacion.SelectedIndex = 0;
                if (Cbo_gastos_depreciacoin.Items.Count > 0) Cbo_gastos_depreciacoin.SelectedIndex = 0;
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
            if (Cbo_cuenta_activo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una cuenta de activo", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cbo_cuenta_activo.Focus();
                return false;
            }

            if (Cbo_cuenta_depreciacion.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una cuenta de depreciación", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cbo_cuenta_depreciacion.Focus();
                return false;
            }

            if (Cbo_gastos_depreciacoin.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una cuenta de gasto", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cbo_gastos_depreciacoin.Focus();
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
            if (Cbo_cuenta_activo.Items.Count > 0) Cbo_cuenta_activo.SelectedIndex = 0;
            if (Cbo_cuenta_depreciacion.Items.Count > 0) Cbo_cuenta_depreciacion.SelectedIndex = 0;
            if (Cbo_gastos_depreciacoin.Items.Count > 0) Cbo_gastos_depreciacoin.SelectedIndex = 0;

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
            tabPage1.Text = "Datos del Activo";
            tabPage2.Text = "Calcular Depreciacion";
        }

        private void Btn_guardar_Click_1(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            // Extraer códigos de cuenta
            string cuentaActivo = "";
            string cuentaDepreciacion = "";
            string cuentaGasto = "";

            try
            {
                // Para cuenta activo
                if (Cbo_cuenta_activo.SelectedItem != null)
                {
                    string cuentaCompleta = Cbo_cuenta_activo.SelectedItem.ToString();
                    string[] partes = cuentaCompleta.Split('-');
                    cuentaActivo = partes.Length > 0 ? partes[0].Trim() : cuentaCompleta.Trim();
                }

                // Para cuenta depreciación
                if (Cbo_cuenta_depreciacion.SelectedItem != null)
                {
                    string cuentaCompleta = Cbo_cuenta_depreciacion.SelectedItem.ToString();
                    string[] partes = cuentaCompleta.Split('-');
                    cuentaDepreciacion = partes.Length > 0 ? partes[0].Trim() : cuentaCompleta.Trim();
                }

                // Para cuenta gasto
                if (Cbo_gastos_depreciacoin.SelectedItem != null)
                {
                    string cuentaCompleta = Cbo_gastos_depreciacoin.SelectedItem.ToString();
                    string[] partes = cuentaCompleta.Split('-');
                    cuentaGasto = partes.Length > 0 ? partes[0].Trim() : cuentaCompleta.Trim();
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
                return;
            }

            DateTime fecha;
            try
            {
                fecha = DateTime.ParseExact(Txt_fecha_adquisicion.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                MessageBox.Show("Formato de fecha inválido. Use el formato AAAA-MM-DD.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                Txt_fecha_adquisicion.Focus();
                return;
            }

            bool resultado = controlador.GuardarNuevoActivo(
                Txt_activo_fijo.Text.Trim(),
                Txt_descripcion.Text.Trim(),
                Cbo_grupo.SelectedItem?.ToString() ?? "",
                fecha,
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

                // ACTUALIZAR LISTAS DE ACTIVOS DESPUÉS DE GUARDAR
                ActualizarListasActivos();

                // Opcional: Cambiar a la pestaña de depreciación para ver el nuevo activo
                Tbc_calculo_activo_fijo.SelectedTab = tabPage2;
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
        
        private void Btn_buscar_activo_Click(object sender, EventArgs e)
        {
            // Simplemente actualiza la lista de activos en el ComboBox
            ActualizarListasActivos();
            MessageBox.Show("Lista de activos actualizada", "Información",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BuscarActivos()
        {
            
        }


        
        private void ActualizarListasActivos()
        {
            try
            {
                // Solo actualizar el ComboBox de selección
                CargarActivosEnComboBox();

                Console.WriteLine("Lista de activos actualizada correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar lista de activos: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Tbc_calculo_activo_fijo.SelectedTab == tabPage2)
            {
                // Actualizar listas cuando se cambie a la pestaña de depreciación
                ActualizarListasActivos();
            }
        }

        private void Btn_actualizar_Click(object sender, EventArgs e)
        {
            ActualizarListasActivos();
            MessageBox.Show("Listas de activos actualizadas correctamente", "Actualización",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Btn_prueba_Click(object sender, EventArgs e)
        {
            Console.WriteLine("=== PRUEBA MANUAL ===");

            if (Cbo_seleccion_activo.SelectedValue != null)
            {
                int idActivo = Convert.ToInt32(Cbo_seleccion_activo.SelectedValue);
                Console.WriteLine($"Probando con activo ID: {idActivo}");
                MostrarDatosActivo(idActivo);
            }
            else
            {
                Console.WriteLine("No hay activo seleccionado");
            }
        }
        private void ProbarCalculoDirecto()
        {
            try
            {
                // Probar con un ID fijo para debugging
                int idActivo = 1; // El ID de Toyota que viste en los logs
                Console.WriteLine("=== PRUEBA DIRECTA DE CÁLCULO ===");

                DataTable dt = controlador.CalcularDepreciacionLineal(idActivo);
                Console.WriteLine($"Resultado: {dt.Rows.Count} filas calculadas");

                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine($"Año {row["Año"]}: {row["ValorEnLibros"]} | {row["DepreciacionAnual"]} | {row["DepreciacionAcumulada"]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en prueba directa: {ex.Message}");
            }
        }
    }
}