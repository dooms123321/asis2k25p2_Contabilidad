// =====================================================================================
// Autor: Arón Ricardo Esquit Silva
// Carné: 0901-22-13036
// Fecha: 11/11/2025
// Descripción: Cálculo del Flujo de Efectivo (envía datos al DAO)
// =====================================================================================
using System;
using System.Data;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_Reporte_FlujoEfectivo_Calculo
    {
        private readonly Cls_Reporte_FlujoEfectivo_Dao gDao = new Cls_Reporte_FlujoEfectivo_Dao();

        public string Fun_Guardar_Reporte(DataTable dtsFlujo, bool esHistorico)
        {
            try
            {
                string sPeriodo = gDao.Fun_Obtener_Periodo_Activo();
                return gDao.Fun_Guardar_Reporte(dtsFlujo, sPeriodo, esHistorico);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
