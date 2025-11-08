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

      



        public DataTable CargarPolizas()
        {
            return modelo.ObtenerPolizas();
        }


       

    


        public DataTable CargarPolizasPorFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            // Validación lógica (NO visual)
            if (fechaDesde > fechaHasta)
                throw new ArgumentException("La fecha inicial no puede ser mayor que la fecha final.");

            // Llamada al modelo
            return modelo.ObtenerPolizasPorFechas(fechaDesde, fechaHasta);
        }



        public class Cls_ControladorCierreContable
        {
            private Cls_CierreContable modelo = new Cls_CierreContable();

            public void InsertarCierreContable(DateTime fechaCierre, string periodo,
                                               DateTime fechaDesde, DateTime fechaHasta,
                                               decimal totalDebe, decimal totalHaber,
                                               decimal saldoAnterior, decimal saldoFinal,
                                               string observaciones)
            {
                modelo.InsertarCierreContable(fechaCierre, periodo, fechaDesde, fechaHasta,
                                              totalDebe, totalHaber, saldoAnterior, saldoFinal, observaciones);
            }
        }


        public void InsertarCierreContable(DateTime fechaCierre, string periodo,
                                          DateTime fechaDesde, DateTime fechaHasta,
                                          decimal totalDebe, decimal totalHaber,
                                          decimal saldoAnterior, decimal saldoFinal,
                                          string observaciones)
        {
            modelo.InsertarCierreContable(fechaCierre, periodo, fechaDesde, fechaHasta,
                                          totalDebe, totalHaber, saldoAnterior, saldoFinal, observaciones);
        }





    }

}
