// =====================================================================================
// Autor: Arón Ricardo Esquit Silva
// Carné: 0901-22-13036
// Fecha: 11/11/2025
// Descripción: Cálculo para reportes del Estado de Resultados (niveles 1, 2, 3)
// =====================================================================================
using System;
using System.Data;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_Reporte_EstadoResultados_Calculo
    {
        private readonly Cls_Reporte_EstadoResultados_Dao gDao = new Cls_Reporte_EstadoResultados_Dao();

        // ---------------------------------------------------------------------------------
        // Método: Fun_Guardar_Reporte
        // Descripción: Envía los datos del Estado de Resultados al DAO para guardado
        // ---------------------------------------------------------------------------------
        public string Fun_Guardar_Reporte(DataTable dtsEstado, bool esHistorico)
        {
            try
            {
                string sPeriodo = gDao.Fun_Obtener_Periodo_Activo();
                return gDao.Fun_Guardar_Reporte(dtsEstado, sPeriodo, esHistorico);
            }
            catch (Exception ex)
            {
                return "Error en cálculo: " + ex.Message;
            }
        }
    }
}
