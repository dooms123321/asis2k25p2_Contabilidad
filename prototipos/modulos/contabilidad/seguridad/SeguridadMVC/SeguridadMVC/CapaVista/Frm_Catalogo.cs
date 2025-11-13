using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Controlador_Navegador;
namespace Capa_Vista_Seguridad
{
    public partial class Frm_Catalogo : Form
    {
        public Frm_Catalogo()
        {
            InitializeComponent();
            //parametros para navegador
            Capa_Controlador_Navegador.Cls_ConfiguracionDataGridView config = new Capa_Controlador_Navegador.Cls_ConfiguracionDataGridView
            {
                Ancho = 1100,
                Alto = 200,
                PosX = 10,
                PosY = 300,
                ColorFondo = Color.AliceBlue,
                TipoScrollBars = ScrollBars.Both,
                Nombre = "dgv_empleados"
            };

            string[] columnas = {
                "tbl_usuario",
                "Pk_Id_Usuario",
                "Fk_Id_Empleado",
                "Cmp_Nombre_Usuario",
                "Cmp_Contrasena_Usuario",
                "Cmp_Intentos_Fallidos_Usuario",
                "Cmp_Estado_Usuario",
                "Cmp_FechaCreacion_Usuario",
                "Cmp_Ultimo_Cambio_Contrasenea",
                "Cmp_Pidio_Cambio_Contrasenea"
            };

            string[] sEtiquetas = {
                "Código Usuario",
                "Código Empleado",
                "Nombre de Usuario",
                "Contraseña",
                "Intentos Fallidos",
                "Estado del Usuario",
                "Fecha de Creación",
                "Último Cambio de Contraseña",
                "Solicitó Cambio de Contraseña"
            };



            int id_aplicacion = 100;
            navegador1.IPkId_Aplicacion = id_aplicacion;
            navegador1.configurarDataGridView(config);
            navegador1.SNombreTabla = columnas[0];
            navegador1.SAlias = columnas;
            navegador1.SEtiquetas = sEtiquetas;
            navegador1.mostrarDatos();
        }
        void Cargardatos()
        {

        }

        private void navegador1_Load(object sender, EventArgs e)
        {

        }
    }
}
