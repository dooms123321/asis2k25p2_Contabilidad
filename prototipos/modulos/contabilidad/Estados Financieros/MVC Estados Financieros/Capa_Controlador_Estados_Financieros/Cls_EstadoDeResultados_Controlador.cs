// =====================================================================================
// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   10/11/2025
// Descripción: Controlador del Estado de Resultados (actual e histórico)
// =====================================================================================

using System;
using System.Data;
using Capa_Modelo_Estados_Financieros;

namespace Capa_Controlador_Estados_Financieros
{
    public class Cls_EstadoResultados_Controlador
    {
        private readonly Cls_EstadoResultados_Calculo gCalculo = new Cls_EstadoResultados_Calculo();

        // ---------------------------------------------------------------------------------
        // Método: Fun_Obtener_Estado_Resultados
        // Devuelve el estado de resultados (modo actual)
        // ---------------------------------------------------------------------------------
        public DataTable Fun_Obtener_Estado_Resultados(int iNivel)
        {
            try
            {
                return gCalculo.fun_calcular_estado_resultados(iNivel);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el estado de resultados: " + ex.Message);
                return new DataTable();
            }
        }

        // ---------------------------------------------------------------------------------
        // Método: Fun_Obtener_Estado_Resultados_Historico
        // Devuelve el estado de resultados (modo histórico)
        // ---------------------------------------------------------------------------------
        public DataTable Fun_Obtener_Estado_Resultados_Historico(int iNivel, int iAnio, int iMes)
        {
            try
            {
                return gCalculo.fun_calcular_estado_resultados_historico(iNivel, iAnio, iMes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el estado de resultados histórico: " + ex.Message);
                return new DataTable();
            }
        }

        // ---------------------------------------------------------------------------------
        // Método: Fun_Calcular_Utilidad_Neta
        // Calcula la utilidad neta del ejercicio (modo actual)
        // ---------------------------------------------------------------------------------
        public decimal Fun_Calcular_Utilidad_Neta()
        {
            try
            {
                return gCalculo.fun_calcular_utilidad_neta();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al calcular utilidad neta: " + ex.Message);
                return 0;
            }
        }

        // ---------------------------------------------------------------------------------
        // Método: Fun_Calcular_Utilidad_Neta_Historico
        // Calcula la utilidad neta del ejercicio histórico
        // ---------------------------------------------------------------------------------
        public decimal Fun_Calcular_Utilidad_Neta_Historico(int iAnio, int iMes)
        {
            try
            {
                return gCalculo.fun_calcular_utilidad_neta_historico(iAnio, iMes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al calcular utilidad neta histórica: " + ex.Message);
                return 0;
            }
        }

        // ---------------------------------------------------------------------------------
        // Método: Fun_Obtener_Tipo_Resultado
        // Determina si el resultado es utilidad, pérdida o neutro
        // ---------------------------------------------------------------------------------
        public string Fun_Obtener_Tipo_Resultado(decimal deUtilidadNeta)
        {
            try
            {
                return gCalculo.fun_obtener_tipo_resultado(deUtilidadNeta);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al determinar tipo de resultado: " + ex.Message);
                return "ERROR";
            }
        }
    }
}

// Fin de código de Arón Ricardo Esquit Silva
// =====================================================================================
