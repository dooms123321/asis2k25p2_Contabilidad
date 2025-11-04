using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Modelo_CierreContable;
using System.Data;
namespace Capa_Controlador_CierreContable
{
    public class Cls_ControladorCierre
    {
        Cls_CierreContable modelo = new Cls_CierreContable();

        public DataTable CargarCuentas()
        {
            return modelo.ObtenerCuentas();
        }

        public void GuardarCierre(string codigoCuenta, DateTime fecha, string periodo, decimal debe, decimal haber, decimal saldoAnterior, decimal saldoFinal, string observaciones)
        {
            modelo.InsertarCierre(codigoCuenta, fecha, periodo, debe, haber, saldoAnterior, saldoFinal, observaciones);
            modelo.ActualizarSaldoCuenta(codigoCuenta, saldoFinal);
        }



        public DataTable CargarPolizas()
        {
            return modelo.ObtenerPolizas();
        }


       

        public void InsertarCierreContable(string codigoCuenta, DateTime fechaCierre, string periodo,
                                           DateTime fechaDesde, DateTime fechaHasta,
                                           decimal debe, decimal haber, string observaciones)
        {
            modelo.InsertarCierreContable(codigoCuenta, fechaCierre, periodo,
                                          fechaDesde, fechaHasta, debe, haber, observaciones);
        }


    }

}
