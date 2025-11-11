// =====================================================================================
// Autor: Arón Ricardo Esquit Silva
// Carné: 0901-22-13036
// Fecha: 11/11/2025
// Descripción: Cálculo para reportes del Balance General (niveles 1, 2, 3)
// =====================================================================================
using System;
using System.Data;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_Reporte_BalanceGeneral_Calculo
    {
        private readonly Cls_Reporte_BalanceGeneral_Dao gDao = new Cls_Reporte_BalanceGeneral_Dao();

        // ---------------------------------------------------------------------------------
        // Método: Fun_Guardar_Reporte
        // Descripción: Envía los datos del Balance General al DAO para guardado
        // ---------------------------------------------------------------------------------
        public string Fun_Guardar_Reporte(DataTable dtsBalance, bool esHistorico)
        {
            try
            {
                string sPeriodo = gDao.Fun_Obtener_Periodo_Activo();
                return gDao.Fun_Guardar_Reporte(dtsBalance, sPeriodo, esHistorico);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
