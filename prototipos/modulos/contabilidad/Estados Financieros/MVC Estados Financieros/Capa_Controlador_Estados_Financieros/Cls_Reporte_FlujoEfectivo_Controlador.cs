// =====================================================================================
// Autor: Arón Ricardo Esquit Silva
// Carné: 0901-22-13036
// Fecha: 11/11/2025
// Descripción: Controlador del Flujo de Efectivo (actual, sin histórico)
// =====================================================================================
using System;
using System.Data;
using Capa_Modelo_Estados_Financieros;

namespace Capa_Controlador_Estados_Financieros
{
    public class Cls_Reporte_FlujoEfectivo_Controlador
    {
        private readonly Cls_Reporte_FlujoEfectivo_Calculo gCalculo = new Cls_Reporte_FlujoEfectivo_Calculo();

        public string Fun_Guardar_Reporte(DataTable dtsFlujo, bool esHistorico)
        {
            try
            {
                return gCalculo.Fun_Guardar_Reporte(dtsFlujo, esHistorico);
            }
            catch (Exception ex)
            {
                return "Error en controlador: " + ex.Message;
            }
        }
    }
}
