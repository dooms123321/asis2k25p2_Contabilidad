﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Capa_Controlador_Seguridad;



//Brandon Alexander Hernandez Salguero - 0901-22-9663

namespace Capa_Vista_Seguridad
{
    public partial class Frm_Permisos_Perfiles : Form
    {
        
        Cls_Asignacion_Permiso_PerfilControlador controlador = new Cls_Asignacion_Permiso_PerfilControlador();
        Cls_Registrar_Permisos_Bitacora registrarBitacora = new Cls_Registrar_Permisos_Bitacora();  //Aron Esquit  0901-22-13036
        Cls_BitacoraControlador ctrlBitacora = new Cls_BitacoraControlador();  //Bitacora  Aron Esquit 0901-22-13036
        Cls_Registrar_Permisos_Bitacora ctrlPermisosBitacora = new Cls_Registrar_Permisos_Bitacora(); //Bitacora  Aron Esquit 0901-22-13036

        //Brandon Hernandez 0901-22-9663 15/10/2025
        private bool _canIngresar, _canConsultar, _canModificar, _canEliminar, _canImprimir;

        public Frm_Permisos_Perfiles()
        {
            InitializeComponent();
            Dgv_Permisos.AllowUserToAddRows = false;
            fun_AplicarPermisos();


        }
        //Brandon Hernandez 0901-22-9663 15/10/2025
        private void fun_AplicarPermisos()
        {
            int idUsuario = Capa_Controlador_Seguridad.Cls_Usuario_Conectado.iIdUsuario;
            var usuarioCtrl = new Cls_Usuario_Controlador();
            var permisoUsuario = new Cls_Permiso_Usuario_Controlador();

            int idAplicacion = permisoUsuario.ObtenerIdAplicacionPorNombre("Asig aplicacion Perfil");
            if (idAplicacion <= 0) idAplicacion = 307; // Cambia a tu ID real si aplica
            int idModulo = permisoUsuario.ObtenerIdModuloPorNombre("Seguridad");
            int idPerfil = usuarioCtrl.ObtenerIdPerfilDeUsuario(idUsuario);

            var permisos = Cls_Aplicacion_Permisos.ObtenerPermisosCombinados(idUsuario, idAplicacion, idModulo, idPerfil);

            _canIngresar = permisos.ingresar;
            _canConsultar = permisos.consultar;
            _canModificar = permisos.modificar;
            _canEliminar = permisos.eliminar;
            _canImprimir = permisos.imprimir;

            // Botones principales
            if (Btn_agregar != null) Btn_agregar.Enabled = _canIngresar;
            if (Btn_insertar != null) Btn_insertar.Enabled = _canIngresar || _canModificar;
            if (Btn_quitar != null) Btn_quitar.Enabled = _canConsultar;
            if (Btn_Buscar != null) Btn_Buscar.Enabled = _canConsultar;
            if (Btn_salir != null) Btn_salir.Enabled = true;

            // Combos y DataGridView
            Cbo_perfiles.Enabled = _canConsultar || _canIngresar || _canModificar;
            Cbo_Modulos.Enabled = _canConsultar || _canIngresar || _canModificar;
            Cbo_aplicaciones.Enabled = _canConsultar || _canIngresar || _canModificar;
            Dgv_Permisos.Enabled = _canConsultar || _canIngresar || _canModificar || _canEliminar;
        }
        private void InicializarDataGridView()
        {
            Dgv_Permisos.Columns.Clear();
            Dgv_Permisos.Rows.Clear();

            Dgv_Permisos.Columns.Add("Perfil", "Perfil");
            Dgv_Permisos.Columns.Add("Aplicacion", "Aplicación");
            Dgv_Permisos.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "Ingresar", HeaderText = "Ingresar" });
            Dgv_Permisos.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "Consultar", HeaderText = "Consultar" });
            Dgv_Permisos.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "Modificar", HeaderText = "Modificar" });
            Dgv_Permisos.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "Eliminar", HeaderText = "Eliminar" });
            Dgv_Permisos.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "Imprimir", HeaderText = "Imprimir" });

            // IDs ocultos
            Dgv_Permisos.Columns.Add("IdPerfil", "IdPerfil");
            Dgv_Permisos.Columns["IdPerfil"].Visible = false;
            Dgv_Permisos.Columns.Add("IdModulo", "IdModulo");
            Dgv_Permisos.Columns["IdModulo"].Visible = false;
            Dgv_Permisos.Columns.Add("IdAplicacion", "IdAplicacion");
            Dgv_Permisos.Columns["IdAplicacion"].Visible = false;
        }


        private void Btn_salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmPermisosPerfiles_Load(object sender, EventArgs e)
        {
            // Llenar ComboBox Perfiles
            DataTable dtPerfiles = controlador.datObtenerPerfiles();
            Cbo_perfiles.DataSource = dtPerfiles;
            Cbo_perfiles.DisplayMember = "Cmp_Puesto_Perfil";
            Cbo_perfiles.ValueMember = "Pk_Id_Perfil";
            Cbo_perfiles.SelectedIndex = -1;

            // Llenar ComboBox Modulos
            DataTable dtModulos = controlador.datObtenerModulos();
            Cbo_Modulos.DataSource = dtModulos;
            Cbo_Modulos.DisplayMember = "Cmp_Nombre_Modulo";
            Cbo_Modulos.ValueMember = "Pk_Id_Modulo";
            Cbo_Modulos.SelectedIndex = -1;

            Cbo_Modulos.SelectedIndexChanged += Cbo_Modulos_SelectedIndexChanged;
            Cbo_aplicaciones.DataSource = null;
            Cbo_aplicaciones.Items.Clear();
            InicializarDataGridView();

            Dgv_Permisos.CellBeginEdit += Dgv_Permisos_CellBeginEdit;
            Dgv_Permisos.CellClick += Dgv_Permisos_CellClick;
        }

        private void Cbo_Modulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cbo_aplicaciones.DataSource = null;
            Cbo_aplicaciones.Items.Clear();

            if (Cbo_Modulos.SelectedValue != null && !(Cbo_Modulos.SelectedValue is DataRowView))
            {
                int iIdModulo = Convert.ToInt32(Cbo_Modulos.SelectedValue);
                DataTable dtAplicacionFiltrada = controlador.datObtenerAplicacionesPorModulo(iIdModulo);

                Cbo_aplicaciones.DataSource = dtAplicacionFiltrada;
                Cbo_aplicaciones.DisplayMember = "Cmp_Nombre_Aplicacion";
                Cbo_aplicaciones.ValueMember = "Pk_Id_Aplicacion";
                Cbo_aplicaciones.SelectedIndex = -1;
            }
            else
            {
                Cbo_aplicaciones.DataSource = null;
                Cbo_aplicaciones.Items.Clear();
            }
        }

        private void Btn_agregar_Click(object sender, EventArgs e)
        {
            if (Cbo_perfiles.SelectedIndex == -1 || Cbo_Modulos.SelectedIndex == -1 || Cbo_aplicaciones.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione Perfil, Módulo y Aplicación.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sPerfilNombre = Cbo_perfiles.Text;
            string sAplicacionNombre = Cbo_aplicaciones.Text;
            int iIdPerfil = Convert.ToInt32(Cbo_perfiles.SelectedValue);
            int iIdAplicacion = Convert.ToInt32(Cbo_aplicaciones.SelectedValue);
            int iIdModulo = Convert.ToInt32(Cbo_Modulos.SelectedValue);

            // Verifica si ya existe la fila (evita duplicados)
            bool bExiste = false;
            foreach (DataGridViewRow row in Dgv_Permisos.Rows)
            {
                if (row.IsNewRow) continue;
                int iPerfil = Convert.ToInt32(row.Cells["IdPerfil"].Value);
                int iModulo = Convert.ToInt32(row.Cells["IdModulo"].Value);
                int iAplicacion = Convert.ToInt32(row.Cells["IdAplicacion"].Value);
                if (iPerfil == iIdPerfil && iModulo == iIdModulo && iAplicacion == iIdAplicacion)
                {
                    bExiste = true;
                    break;
                }
            }

            if (!bExiste)
            {
                Dgv_Permisos.Rows.Add(
                    sPerfilNombre,
                    sAplicacionNombre,
                    false, false, false, false, false, // Permisos iniciales
                    iIdPerfil,
                    iIdModulo,
                    iIdAplicacion
                );

            }
            else
            {
                MessageBox.Show("Este Perfil ya tiene la aplicación asignada, solo modifique los permisos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Btn_insertar_Click(object sender, EventArgs e)
        {
            if (Dgv_Permisos.Rows.Count == 0 ||
                (Dgv_Permisos.Rows.Count == 1 && Dgv_Permisos.AllowUserToAddRows && Dgv_Permisos.Rows[0].IsNewRow))
            {
                MessageBox.Show("Debe agregar al menos un registro de asignación para insertar o actualizar.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int iInsertados = 0;
            int iActualizados = 0;

            foreach (DataGridViewRow row in Dgv_Permisos.Rows)
            {
                if (row.IsNewRow) continue;

                int iPerfil = Convert.ToInt32(row.Cells["IdPerfil"].Value);
                int iModulo = Convert.ToInt32(row.Cells["IdModulo"].Value);
                int iAplicacion = Convert.ToInt32(row.Cells["IdAplicacion"].Value);
                bool bIngresar = Convert.ToBoolean(row.Cells["Ingresar"].Value ?? false);
                bool bConsultar = Convert.ToBoolean(row.Cells["Consultar"].Value ?? false);
                bool bModificar = Convert.ToBoolean(row.Cells["Modificar"].Value ?? false);
                bool bEliminar = Convert.ToBoolean(row.Cells["Eliminar"].Value ?? false);
                bool bImprimir = Convert.ToBoolean(row.Cells["Imprimir"].Value ?? false);

                // Verificar si ya existe el permiso en la base de datos
                bool bExiste = controlador.bExistePermisoPerfil(iPerfil, iModulo, iAplicacion);

                if (!bExiste)
                {
                    // Insertar nuevo permiso
                    int filas = controlador.iInsertarPermisoPerfilAplicacion(
                        iPerfil, iModulo, iAplicacion,
                        bIngresar, bConsultar, bModificar, bEliminar, bImprimir
                    );
                    iInsertados += filas;
                }
                else
                {
                    // Actualizar permiso existente
                    int filas = controlador.iActualizarPermisoPerfilAplicacion(
                        iPerfil, iModulo, iAplicacion,
                        bIngresar, bConsultar, bModificar, bEliminar, bImprimir
                    );
                    iActualizados += filas;
                }

                // Registrar cambios en bitácora
                registrarBitacora.fun_CompararYRegistrarPerfilManual_Puente(
                    Capa_Controlador_Seguridad.Cls_Usuario_Conectado.iIdUsuario,
                    iAplicacion,
                    row.Cells["Perfil"].Value.ToString(),
                    row.Cells["Aplicacion"].Value.ToString(),
                    bIngresar,
                    bConsultar,
                    bModificar,
                    bEliminar,
                    bImprimir
                );
            }

            MessageBox.Show($"Se insertaron {iInsertados} registros y se actualizaron {iActualizados} registros correctamente.",
                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpiar DataGridView
            Dgv_Permisos.Rows.Clear();
        }

        private void Btn_quitar_Click(object sender, EventArgs e)
        {
            // Validar que haya una fila seleccionada
            if (Dgv_Permisos.CurrentRow == null || Dgv_Permisos.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Seleccione una fila para quitar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mostrar advertencia de confirmación
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de quitar el registro seleccionado?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                // Capturar datos antes de eliminar (opcional)
                int idAplicacion = Convert.ToInt32(Dgv_Permisos.CurrentRow.Cells["IdAplicacion"].Value);
                int idUsuario = Capa_Controlador_Seguridad.Cls_Usuario_Conectado.iIdUsuario;
                string sPerfil = Dgv_Permisos.CurrentRow.Cells["Perfil"].Value.ToString();
                string sAplicacion = Dgv_Permisos.CurrentRow.Cells["Aplicacion"].Value.ToString();

                // Eliminar la fila del DataGridView
                Dgv_Permisos.Rows.Remove(Dgv_Permisos.CurrentRow);

                // Registrar acción en bitácora
                ctrlBitacora.RegistrarAccion(
                    idUsuario,
                    idAplicacion,
                    $"Se quitaron los permisos del perfil '{sPerfil}' en la aplicación '{sAplicacion}'.",
                    true
                );

                // Mostrar confirmación
                MessageBox.Show("Se ha quitado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void Dgv_Permisos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validar que no sea encabezado o fila nueva
            if (e.RowIndex < 0) return;
            var vRow = Dgv_Permisos.Rows[e.RowIndex];

            var iIdPerfil = vRow.Cells["IdPerfil"].Value?.ToString();
            var iIdAplicacion = vRow.Cells["IdAplicacion"].Value?.ToString();

            if (string.IsNullOrWhiteSpace(iIdPerfil) || string.IsNullOrWhiteSpace(iIdAplicacion))
            {
                MessageBox.Show("No tiene aplicación y perfil seleccionado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Dgv_Permisos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            string columnName = Dgv_Permisos.Columns[e.ColumnIndex].Name;

            if (columnName == "Perfil" || columnName == "Aplicacion")
            {
                e.Cancel = true;
                return;
            }

            if (Dgv_Permisos.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
            {
                var vRow = Dgv_Permisos.Rows[e.RowIndex];
                if (vRow.IsNewRow)
                {
                    e.Cancel = true;
                    MessageBox.Show("No tiene aplicación y perfil seleccionado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var vIdPerfil = vRow.Cells["IdPerfil"].Value?.ToString();
                var vIdAplicacion = vRow.Cells["IdAplicacion"].Value?.ToString();

                if (string.IsNullOrWhiteSpace(vIdPerfil) || string.IsNullOrWhiteSpace(vIdAplicacion))
                {
                    e.Cancel = true;
                    MessageBox.Show("No tiene aplicación y perfil seleccionado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //Panel Superior Brandon Hernandez 0901-22-96663
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void Pnl_Superior_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void Btn_Buscar_Click(object sender, EventArgs e)
        {
            if (Cbo_perfiles.SelectedIndex == -1 || Cbo_perfiles.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un perfil para buscar sus permisos.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            InicializarDataGridView();

            try
            {
                int iIdPerfil = Convert.ToInt32(Cbo_perfiles.SelectedValue);
                DataTable dtPermisos = controlador.datObtenerPermisosPorPerfil(iIdPerfil);

                if (dtPermisos.Rows.Count > 0)
                {
                    foreach (DataRow row in dtPermisos.Rows)
                    {
                        Dgv_Permisos.Rows.Add(
                            row["nombre_perfil"].ToString(),
                            row["nombre_aplicacion"].ToString(),
                            Convert.ToBoolean(row["bIngresar_permiso_aplicacion_perfil"]),
                            Convert.ToBoolean(row["bConsultar_permiso_aplicacion_perfil"]),
                            Convert.ToBoolean(row["bModificar_permiso_aplicacion_perfil"]),
                            Convert.ToBoolean(row["bEliminar_permiso_aplicacion_perfil"]),
                            Convert.ToBoolean(row["imprimir_permiso_aplicacion_perfil"]),
                            row["iFk_id_perfil"],
                            row["iFk_id_modulo"],
                            row["iFk_id_aplicacion"]
                        );
                    }
                    MessageBox.Show($"Permisos cargados correctamente. Se encontraron {dtPermisos.Rows.Count} registros.", "Búsqueda exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("El perfil seleccionado no tiene permisos asignados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar permisos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }






        }


     



        //Panel Superior Brandon Hernandez 0901-22-96663
        private void Pic_Cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}