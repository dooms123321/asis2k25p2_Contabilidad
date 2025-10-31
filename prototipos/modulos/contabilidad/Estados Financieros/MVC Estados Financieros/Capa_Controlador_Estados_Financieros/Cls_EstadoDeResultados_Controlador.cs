// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   30/10/2025

using System;
using System.Data;
using Capa_Modelo_Estados_Financieros;

namespace Capa_Controlador_Estados_Financieros
{
    public class Cls_EstadoDeResultados_Controlador
    {
        private readonly Cls_EstadoDeResultados_Calculo gCalculo = new Cls_EstadoDeResultados_Calculo();

        // Método para obtener el estado de resultados completo y estructurado
        public DataTable fun_obtener_estado_resultados()
        {
            DataTable dts_Estado = new DataTable();

            try
            {
                // Llama al cálculo jerárquico del modelo
                dts_Estado = gCalculo.fun_calcular_estado_resultados();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el estado de resultados: " + ex.Message);
            }

            return dts_Estado;
        }
    }
}
