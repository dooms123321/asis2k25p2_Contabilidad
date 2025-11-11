// =====================================================================================
// Autor: Arón Ricardo Esquit Silva
// Carné: 0901-22-13036
// Fecha: 11/11/2025
// Descripción: Controlador del Estado de Resultados (niveles 1, 2, 3)
// =====================================================================================
using System;
using System.Data;
using Capa_Modelo_Estados_Financieros;

namespace Capa_Controlador_Estados_Financieros
{
    public class Cls_Reporte_EstadoResultados_Controlador
    {
        private readonly Cls_Reporte_EstadoResultados_Calculo gCalculo = new Cls_Reporte_EstadoResultados_Calculo();

        // ---------------------------------------------------------------------------------
        // Método: Fun_Guardar_Reporte
        // Descripción: Envía la información del Estado de Resultados al cálculo
        // ---------------------------------------------------------------------------------
        public string Fun_Guardar_Reporte(DataTable dtsEstado, bool esHistorico)
        {
            try
            {
                if (esHistorico)
                    return "Modo Histórico: no se permite guardar";

                return gCalculo.Fun_Guardar_Reporte(dtsEstado, esHistorico);
            }
            catch (Exception ex)
            {
                return "Error en controlador: " + ex.Message;
            }
        }
    }
}
