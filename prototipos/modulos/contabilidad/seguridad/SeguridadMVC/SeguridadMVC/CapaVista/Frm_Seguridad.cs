using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Controlador_Seguridad;
using Capa_Vista_CierreContable;
using Capa_vista;//capa de activo fijo
using Capa_Vista_Estados_Financieros;
using Capa_Vista_Polizas;

namespace Capa_Vista_Seguridad
{
    public partial class Frm_Seguridad : Form
    {
        Cls_BitacoraControlador ctrlBitacora = new Cls_BitacoraControlador();
        private Cls_ControladorAsignacionUsuarioAplicacion controladorPermisos = new Cls_ControladorAsignacionUsuarioAplicacion();
        private Cls_Asignacion_Permiso_PerfilControlador controladorPermisosPerfil = new Cls_Asignacion_Permiso_PerfilControlador();
        private int iIChildFormNumber = 0;

        public enum MenuOpciones
        {
            Archivo,
            Catalogos,
            Procesos,
            Reportes,
            GestionFinanciera,
            Herramientas,
            Asignaciones,
            Modulos
        }

        private Dictionary<MenuOpciones, ToolStripMenuItem> menuItems;

        public Frm_Seguridad()
        {
            InitializeComponent();
            InicializarMenuItems();
            fun_inicializar_botones_por_defecto();

            this.Load += Frm_Seguridad_Load;

            fun_habilitar_botones_por_permisos_combinados(
                Capa_Controlador_Seguridad.Cls_Usuario_Conectado.iIdUsuario,
                Capa_Controlador_Seguridad.Cls_Usuario_Conectado.iIdPerfil
            );

            this.FormClosing += Frm_Seguridad_FormClosing;
        }
        private void Frm_Seguridad_Load(object sender, EventArgs e)
        {
            // Mostrar usuario conectado en StatusStrip
            toolStripStatusLabel.Text = $"Estado: Conectado | Usuario: {Capa_Controlador_Seguridad.Cls_Usuario_Conectado.sNombreUsuario}";

            // El resto de tu código de carga...
            fun_inicializar_botones_por_defecto();
            fun_habilitar_botones_por_permisos_combinados(
                Capa_Controlador_Seguridad.Cls_Usuario_Conectado.iIdUsuario,
                Capa_Controlador_Seguridad.Cls_Usuario_Conectado.iIdPerfil
            );
        }
        private void Frm_Seguridad_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void InicializarMenuItems()
        {
            menuItems = new Dictionary<MenuOpciones, ToolStripMenuItem>
            {
                { MenuOpciones.Archivo, archivoToolStripMenuItem },
                { MenuOpciones.Catalogos, catálogosToolStripMenuItem },
                { MenuOpciones.Procesos, procesosToolStripMenuItem },
                { MenuOpciones.GestionFinanciera, gestiónFinancieraToolStripMenuItem },
                { MenuOpciones.Herramientas, herramientasToolStripMenuItem },
            };
        }
        //Procesos
        private void cierrePolizasMesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarFormulariosHijos();
            Frm_CierreMes frmCerrarMes = new Frm_CierreMes();
            frmCerrarMes.MdiParent = this;
            frmCerrarMes.Show();
        }
        
        //cierre polizas anual
        private void cierrePolizasAnualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarFormulariosHijos();
            Frm_CierreAño frmCerrarAnio = new Frm_CierreAño();
            frmCerrarAnio.MdiParent = this;
            frmCerrarAnio.Show();
        }

        public void fun_inicializar_botones_por_defecto()
        {
            foreach (var opcion in menuItems.Keys)
            {
                switch (opcion)
                {
                    case MenuOpciones.Archivo:
                    case MenuOpciones.Herramientas:
                        menuItems[opcion].Enabled = true;
                        break;
                    default:
                        menuItems[opcion].Enabled = false;
                        break;
                }
            }
        }

        // 0901-20-4620 Ruben Lopez 12/10/25
        //0
        //901-22-9663 Brandon Hernandez 12/10/25
        public void fun_habilitar_botones_por_permisos_combinados(int iIdUsuario, int iIdPerfil)
        {
            // Diccionarios de idAplicacion -> submenú para Módulo 7 (Contabilidad)

            // CATÁLOGOS
            Dictionary<int, ToolStripMenuItem> mapaCatalogos = new Dictionary<int, ToolStripMenuItem>
    {
        {2414, cuentasToolStripMenuItem1} // Catalogo de Cuentas
    };

            // PROCESOS (solo estos tres)
            Dictionary<int, ToolStripMenuItem> mapaProcesos = new Dictionary<int, ToolStripMenuItem>
    {
        {2402, actualizarSaldosToolStripMenuItem},   // Actualizar Saldos
        {2403, cierrePolizasMesToolStripMenuItem},   // Cierre Mes
        {2404, cierrePolizasAnualToolStripMenuItem}  // Cierre Año
    };

            // GESTIÓN FINANCIERA (los demás van aquí)
            Dictionary<int, ToolStripMenuItem> mapaGestionFinanciera = new Dictionary<int, ToolStripMenuItem>
    {
            {2401, polizasLocalesToolStripMenuItem},     // Polizas Locales
            {2405, presupustoToolStripMenuItem},        // Presupuesto - CORREGIDO
            {2406, activosFijosToolStripMenuItem},       // Activos Fijos
            {2407, cierreContableToolStripMenuItem},     // Cierre Contable
            {2408, estadoDeResultadosToolStripMenuItem}, // Estado de Resultados
            {2409, estadoDeBalancesDeToolStripMenuItem}, // Balance de Saldos
            {2410, balanceGeneralToolStripMenuItem},     // Balance General
            {2411, flujoDeEfectivoToolStripMenuItem},    // Flujo de Efectivo
            {2412, estadosFinancierosToolStripMenuItem}, // Estados Financieros
            {2413, enlacesModulosToolStripMenuItem}      // Enlaces de Modulos
    };

            // 1. DESHABILITA TODOS LOS SUBMENÚS ANTES DE HABILITAR PERMISOS
            foreach (var sub in mapaCatalogos.Values) sub.Enabled = false;
            foreach (var sub in mapaProcesos.Values) sub.Enabled = false;
            foreach (var sub in mapaGestionFinanciera.Values) sub.Enabled = false;

            // 2. Permisos por perfil (primer filtro) - MÓDULO 7
            DataTable dtPermisosPerfil = controladorPermisosPerfil.datObtenerPermisosPorPerfil(iIdPerfil);
            foreach (DataRow row in dtPermisosPerfil.Rows)
            {
                int idModulo = Convert.ToInt32(row["iFk_id_modulo"]);
                int idAplicacion = Convert.ToInt32(row["iFk_id_aplicacion"]);

                // Solo módulo 7 (Contabilidad)
                if (idModulo == 7)
                {
                    if (mapaCatalogos.ContainsKey(idAplicacion))
                        mapaCatalogos[idAplicacion].Enabled = true;
                    if (mapaProcesos.ContainsKey(idAplicacion))
                        mapaProcesos[idAplicacion].Enabled = true;
                    if (mapaGestionFinanciera.ContainsKey(idAplicacion))
                        mapaGestionFinanciera[idAplicacion].Enabled = true;
                }
            }

            // 3. Permisos por usuario (segundo filtro - suma, nunca deshabilita) - MÓDULO 7
            DataTable dtPermisosUsuario = controladorPermisos.ObtenerPermisosPorUsuario(iIdUsuario);
            foreach (DataRow row in dtPermisosUsuario.Rows)
            {
                int idModulo = Convert.ToInt32(row["iFk_id_modulo"]);
                int idAplicacion = Convert.ToInt32(row["iFk_id_aplicacion"]);

                // Solo módulo 7 (Contabilidad)
                if (idModulo == 7)
                {
                    if (mapaCatalogos.ContainsKey(idAplicacion))
                        mapaCatalogos[idAplicacion].Enabled = true;
                    if (mapaProcesos.ContainsKey(idAplicacion))
                        mapaProcesos[idAplicacion].Enabled = true;
                    if (mapaGestionFinanciera.ContainsKey(idAplicacion))
                        mapaGestionFinanciera[idAplicacion].Enabled = true;
                }
            }

            // 4. Habilita menús principales solo si algún submenú está habilitado
            menuItems[MenuOpciones.Catalogos].Enabled = mapaCatalogos.Values.Any(m => m.Enabled);
            menuItems[MenuOpciones.Procesos].Enabled = mapaProcesos.Values.Any(m => m.Enabled);
            menuItems[MenuOpciones.GestionFinanciera].Enabled = mapaGestionFinanciera.Values.Any(m => m.Enabled);

            // Habilita el menú de Gestión Financiera si tiene algún submenú habilitado
            gestiónFinancieraToolStripMenuItem.Enabled = mapaGestionFinanciera.Values.Any(m => m.Enabled);
        }
        //public void fun_habilitar_botones_por_permisos_combinados(int iIdUsuario, int iIdPerfil)
        //{
        //    // Diccionarios de idAplicacion -> submenú
        //    Dictionary<int, ToolStripMenuItem> mapaCatalogos = new Dictionary<int, ToolStripMenuItem>
        //    {
        //        {2401, cuentasToolStripMenuItem1},

        //    };

        //    Dictionary<int, ToolStripMenuItem> mapaProcesos = new Dictionary<int, ToolStripMenuItem>
        //    {
        //        {309, procesosToolStripMenuItem }
        //    };

        //    Dictionary<int, ToolStripMenuItem> mapaAsignaciones = new Dictionary<int, ToolStripMenuItem>
        //    {

        //    };

        //    // 1. DESHABILITA TODOS LOS SUBMENÚS ANTES DE HABILITAR PERMISOS
        //    foreach (var sub in mapaCatalogos.Values) sub.Enabled = false;
        //    foreach (var sub in mapaProcesos.Values) sub.Enabled = false;
        //    foreach (var sub in mapaAsignaciones.Values) sub.Enabled = false;

        //    // 2. Permisos por perfil (primer filtro)
        //    DataTable dtPermisosPerfil = controladorPermisosPerfil.datObtenerPermisosPorPerfil(iIdPerfil);
        //    foreach (DataRow row in dtPermisosPerfil.Rows)
        //    {
        //        int idModulo = Convert.ToInt32(row["iFk_id_modulo"]);
        //        int idAplicacion = Convert.ToInt32(row["iFk_id_aplicacion"]);
        //        if (idModulo == 7 && idAplicacion >= 2401 )
        //        {
        //            if (mapaCatalogos.ContainsKey(idAplicacion))
        //                mapaCatalogos[idAplicacion].Enabled = true;
        //            if (mapaProcesos.ContainsKey(idAplicacion))
        //                mapaProcesos[idAplicacion].Enabled = true;
        //            if (mapaAsignaciones.ContainsKey(idAplicacion))
        //                mapaAsignaciones[idAplicacion].Enabled = true;
        //        }
        //    }

        //    // 3. Permisos por usuario (segundo filtro - suma, nunca deshabilita)
        //    DataTable dtPermisosUsuario = controladorPermisos.ObtenerPermisosPorUsuario(iIdUsuario);
        //    foreach (DataRow row in dtPermisosUsuario.Rows)
        //    {
        //        int idModulo = Convert.ToInt32(row["iFk_id_modulo"]);
        //        int idAplicacion = Convert.ToInt32(row["iFk_id_aplicacion"]);
        //        if (idModulo == 7 && idAplicacion >= 2401)

        //        {
        //            if (mapaCatalogos.ContainsKey(idAplicacion))
        //                mapaCatalogos[idAplicacion].Enabled = true;
        //            if (mapaProcesos.ContainsKey(idAplicacion))
        //                mapaProcesos[idAplicacion].Enabled = true;
        //            //if (mapaAsignaciones.ContainsKey(idAplicacion))
        //              //  mapaAsignaciones[idAplicacion].Enabled = true;
        //        }
        //    }

        //    // 4. Habilita menús principales solo si algún submenú está habilitado
        //    menuItems[MenuOpciones.Catalogos].Enabled = mapaCatalogos.Values.Any(m => m.Enabled);
        //    menuItems[MenuOpciones.Procesos].Enabled = mapaProcesos.Values.Any(m => m.Enabled);
        //    //menuItems[MenuOpciones.Asignaciones].Enabled = mapaAsignaciones.Values.Any(m => m.Enabled);

        //    // Modulos siempre habilitar si tiene algún permiso del módulo 4
        //    //menuItems[MenuOpciones.Modulos].Enabled = mapaCatalogos.ContainsKey(304) && mapaCatalogos[304].Enabled;
        //}

        // --- El resto de tus métodos siguen igual ---
        private void CerrarFormulariosHijos()
        {
            foreach (Form childForm in this.MdiChildren)
            {
                childForm.Close();
            }
        }
        private void ShowNewForm(object sender, EventArgs e)
        {
            CerrarFormulariosHijos();
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + iIChildFormNumber++;
            childForm.Show();
        }



        
        private void cambiarContraseñaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_cambiar_contrasena ventana = new Frm_cambiar_contrasena(Capa_Controlador_Seguridad.Cls_Usuario_Conectado.iIdUsuario);
            ventana.Show();
        }
        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CutToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void CopyToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }
        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }
        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }
        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }
        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Frm_Principal ventanaPrincipal = new Frm_Principal();
            ventanaPrincipal.ShowDialog();
            this.Close();
        }
        private void btn_aplicacion_Click(object sender, EventArgs e) { }
        private void asignacionesToolStripMenuItem_Click(object sender, EventArgs e) { }


        private void Pic_Cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void catálogosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Pnl_Superior_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Frm_Seguridad_Load_1(object sender, EventArgs e)
        {

            
        }
        //Cierre contable
        private void cierreContableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarFormulariosHijos();
            Frm_CierreContable frmCierre = new Frm_CierreContable();
            frmCierre.MdiParent = this;
            frmCierre.Show();
        }
        //activos fijos
        private void activosFijosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarFormulariosHijos();
            Frm_registro_activo frmCierre = new Frm_registro_activo();
            frmCierre.MdiParent = this;
            frmCierre.Show();
        }

        private void estadosFinancierosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        

        private void polizasLocalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarFormulariosHijos();
            Frm_PolizasLocales frmCierre = new Frm_PolizasLocales();
            frmCierre.MdiParent = this;
            frmCierre.Show();
        }
        //estado de resultados
        private void estadoDeResultadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarFormulariosHijos();
            Frm_EstadoDeResultados frmCierre = new Frm_EstadoDeResultados();
            frmCierre.MdiParent = this;
            frmCierre.Show();
        }
        //estado de balances
        private void estadoDeBalancesDeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            CerrarFormulariosHijos();
            Frm_EstadoDeBalanceDeSaldos frmCierre = new Frm_EstadoDeBalanceDeSaldos();
            frmCierre.MdiParent = this;
            frmCierre.Show();

        }
         //balance general
        private void balanceGeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {

            CerrarFormulariosHijos();
            Frm_EstadoBalanceGeneral frmCierre = new Frm_EstadoBalanceGeneral();
            frmCierre.MdiParent = this;
            frmCierre.Show();
        }
        //flujo de efectivo
        private void flujoDeEfectivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarFormulariosHijos();
            Frm_Flujo_Efectivo frmCierre = new Frm_Flujo_Efectivo();
            frmCierre.MdiParent = this;
            frmCierre.Show();
        }
        //actualizacion de saldos
        private void actualizarSaldosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarFormulariosHijos();
            Frm_ActualizarSaldos frmActualizarSaldos = new Frm_ActualizarSaldos();
            frmActualizarSaldos.MdiParent = this;
            frmActualizarSaldos.Show();
        }

        //catalogo de cuentas
        private void cuentasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CerrarFormulariosHijos();
            Frm_Catalogo frm_Catalogo = new Frm_Catalogo();
            frm_Catalogo.MdiParent = this;
            frm_Catalogo.Show();
        }
        //polizas locales
        private void polizasLocalesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CerrarFormulariosHijos();
            Frm_PolizasLocales frmCierre = new Frm_PolizasLocales();
            frmCierre.MdiParent = this;
            frmCierre.Show();
        }
        //actualizar saldos
        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarFormulariosHijos();
            Frm_ActualizarSaldos frmCierre = new Frm_ActualizarSaldos();
            frmCierre.MdiParent = this;
            frmCierre.Show();
        }
        //cierre de mes
        private void cierreDeMesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarFormulariosHijos();
            Frm_CierreMes frmCierre = new Frm_CierreMes();
            frmCierre.MdiParent = this;
            frmCierre.Show();
        }
        //cierre año
        private void cierreAñoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            CerrarFormulariosHijos();
            Frm_CierreAño frmCierre = new Frm_CierreAño();
            frmCierre.MdiParent = this;
            frmCierre.Show();
        }

        private void estadoDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarFormulariosHijos();
            Frm_EstadosFinancieros frmCierre = new Frm_EstadosFinancieros();
            frmCierre.MdiParent = this;
            frmCierre.Show();
        }

        private void pruebaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarFormulariosHijos();
            Frm_Catalogo frm_Catalogo = new Frm_Catalogo();
            frm_Catalogo.MdiParent = this;
            frm_Catalogo.Show();
        }
    }
}
