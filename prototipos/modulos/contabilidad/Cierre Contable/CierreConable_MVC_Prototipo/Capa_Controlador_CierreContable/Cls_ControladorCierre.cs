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



        public DataTable CargarPolizasPorFecha(DateTime desde, DateTime hasta)
        {
            return modelo.ObtenerPolizasPorFechas(desde, hasta);
        }




        public DataTable CargarPolizas()
        {
            return modelo.ObtenerPolizas();
        }


        public bool GuardarHistorico(int anio, int mes)
        {
            return modelo.GuardarHistorico(anio, mes);
        }



        public bool ExisteCierre(int anio, int mes)
        {
            return modelo.ExisteCierre(anio, mes);
        }

        public bool GuardarCierre(int anio, int mes)
        {
            return modelo.GuardarHistorico(anio, mes);
        }

        public bool GuardarHistoricoDesdeLista(int anio, int mes, List<string> codigos)
        {
            return modelo.GuardarHistoricoDesdeLista(anio, mes, codigos);
        }

        

        public bool RegistrarPeriodo(int anio, int mes, DateTime inicio, DateTime fin, int estado, int modo)
        {
            return modelo.RegistrarPeriodo(anio, mes, inicio, fin, estado, modo);
        }

        public void CerrarPeriodosAnteriores(int anio, int mes)
        {
            modelo.CerrarPeriodosAnteriores(anio, mes);
        }

     public bool ExistePeriodoContable(int anio, int mes)
{
    return modelo.ExistePeriodoContable(anio, mes);
}

public bool ExisteHistoricoPeriodo(int anio, int mes)
{
    return modelo.ExisteHistoricoPeriodo(anio, mes);
}


        public void ActualizarSaldosDiarios()
        {
            
        }

        public void ActualizarSaldosMensuales()
        {
            modelo.ActualizarSaldosMensuales();
        }

        public void ActualizarSaldosAnuales()
        {
            modelo.ActualizarSaldosAnuales();
        }

        public void CrearPeriodoSiguiente(int anio, int mes, int estado, int modo)
        {
            int nuevoAnio = (mes == 12) ? anio + 1 : anio;
            int nuevoMes = (mes == 12) ? 1 : mes + 1;

            DateTime inicio = new DateTime(nuevoAnio, nuevoMes, 1);
            DateTime fin = inicio.AddMonths(1).AddDays(-1);

            modelo.RegistrarPeriodo(nuevoAnio, nuevoMes, inicio, fin, estado, modo);
        }






    }

}
