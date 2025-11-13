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
                Nombre = "Dgv_Catalogo_De_Cuentas"
            };

            string[] columnas = {
                "tbl_catalogo_cuentas",
                "Pk_Codigo_Cuenta",
                "Cmp_CtaNombre",
                "Cmp_CtaMadre",
                "Cmp_CtaSaldoInicial",
                "Cmp_CtaCargoMes",
                "Cmp_CtaAbonoMes",
                "Cmp_CtaSaldoActual",
                "Cmp_CtaCargoActual",
                "Cmp_CtaAbonoActual",
                "Cmp_CtaTipo",
                "Cmp_CtaNaturaleza"
            };

            string[] sEtiquetas = {
                "Codigo cuenta",
                "Nombre cuenta",
                "Cuenta madre",
                "Saldo inicial",
                "Cargo Mes",
                "Abono Mes",
                "Saldo actual",
                "Cargo Actual",
                "Abono Actul",
                "Tipo Cuenta",
                "Naturaleza cuenta"
            };



            int id_aplicacion = 2413;
            int id_Modulo = 7;
            navegador1.IPkId_Aplicacion = id_aplicacion;
            navegador1.IPkId_Modulo = id_Modulo;
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
