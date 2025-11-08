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

        public void GuardarCierre(string codigoCuenta, DateTime fecha, string periodo, 
                                  decimal debe, decimal haber, decimal saldoAnterior, 
                                  decimal saldoFinal, string observaciones)
        {
            modelo.InsertarCierre(codigoCuenta, fecha, periodo, debe, haber, 
                                  saldoAnterior, saldoFinal, observaciones);

            modelo.ActualizarSaldoCuenta(codigoCuenta, saldoFinal);
        }

        public DataTable CargarPolizas()
        {
            return modelo.ObtenerPolizas();
        }

        public DataTable CargarPolizasPorFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            if (fechaDesde > fechaHasta)
                throw new ArgumentException("La fecha inicial no puede ser mayor que la fecha final.");

            return modelo.ObtenerPolizasPorFechas(fechaDesde, fechaHasta);
        }

        // Guarda cierre contable general
        public void InsertarCierreContable(DateTime fechaCierre, string periodo,
                                          DateTime fechaDesde, DateTime fechaHasta,
                                          decimal totalDebe, decimal totalHaber,
                                          decimal saldoAnterior, decimal saldoFinal,
                                          string observaciones)
        {
            modelo.InsertarCierreContable(fechaCierre, periodo, fechaDesde, fechaHasta,
                                          totalDebe, totalHaber, saldoAnterior, saldoFinal, observaciones);
        }

        // Guarda cierre por cuenta
        public void InsertarCierreContable(string codigoCuenta, DateTime fechaCierre, string periodo,
                                           DateTime fechaDesde, DateTime fechaHasta,
                                           decimal debe, decimal haber, string observaciones)
        {
            modelo.InsertarCierreContable(codigoCuenta, fechaCierre, periodo,
                                          fechaDesde, fechaHasta, debe, haber, observaciones);
        }
    }
}
