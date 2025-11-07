using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Controlador_Polizas
{
    internal class prueba
    {
        // controlador
        //Cls_PolizaControlador polizaCtrl = new Cls_PolizaControlador();

        //envian la fecha desde la vista (importante, enviar este fecha como parametro en ObtenerSiguienteIdEncabezado)
       // DateTime fechaCompra = new DateTime(2025, 11, 6);

        // Solicitar el siguiente ID disponible para esa fecha (para su poliza)
        //int nuevoId = polizaCtrl.ObtenerSiguienteIdEncabezado(fechaCompra);

        // envian concepto desde la vista
        //string concepto = "Poliza de compras.";

        // Cuentas contables del detalle
        /*
        var detalles = new List<(string sCodigoCuenta, bool bTipo, decimal dValor)>
        {
            ("5101", true, 1200.00m),   // cargo (o 1 porque es bit, como lo manejen) y el valor lo calculan según lo que requieran
            ("2105", false, 1200.00m)   // Abono (o 0 porque es bit) y el valor lo envian según lo que requieran
        };
        */

        // Inserción de la póliza
        // polizaCtrl.InsertarPoliza(fechaCompra, concepto, detalles);

    }
}
