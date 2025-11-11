// =====================================================================================
// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   09/11/2025
// Descripción: Controlador estandarizado para el Estado de Flujo de Efectivo
// Proyecto: QUANTUM S.A. - Módulo CTA (Contabilidad)
// =====================================================================================

using System;
using System.Data;
using Capa_Modelo_Estados_Financieros;

namespace Capa_Controlador_Estados_Financieros
{
    public class Cls_FlujoEfectivo_Controlador
    {
        private readonly Cls_FlujoEfectivo_Calculo gCalculo;

        // ---------------------------------------------------------------------------------
        // Constructor por defecto
        // ---------------------------------------------------------------------------------
        public Cls_FlujoEfectivo_Controlador()
        {
            gCalculo = new Cls_FlujoEfectivo_Calculo();
        }

        // ---------------------------------------------------------------------------------
        // Método: fun_obtener_flujo_efectivo
        // Descripción: Devuelve el flujo de efectivo completo (sin niveles ni filtros)
        // ---------------------------------------------------------------------------------
        public DataTable fun_obtener_flujo_efectivo()
        {
            try
            {
                if (gCalculo == null)
                    throw new InvalidOperationException("El módulo de cálculo no está disponible.");

                DataTable dtsFlujo = gCalculo.fun_calcular_flujo_efectivo();

                if (dtsFlujo == null || dtsFlujo.Rows.Count == 0)
                    throw new InvalidOperationException("No se encontraron registros de flujo de efectivo.");

                return dtsFlujo;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en fun_obtener_flujo_efectivo: " + ex.Message);
                throw new ApplicationException("Error al obtener los datos del flujo de efectivo.", ex);
            }
        }

        // ---------------------------------------------------------------------------------
        // Método: fun_calcular_resultado
        // Descripción: Calcula el aumento o disminución neta de efectivo.
        // ---------------------------------------------------------------------------------
        public Cls_ResultadoFlujo fun_calcular_resultado()
        {
            try
            {
                if (gCalculo == null)
                    throw new InvalidOperationException("El módulo de cálculo no está disponible.");

                decimal diferencia = gCalculo.fun_calcular_aumento_disminucion();
                string tipo = gCalculo.fun_obtener_tipo_resultado(diferencia);

                string texto;

                if (tipo == "AUMENTO")
                {
                    texto = "Aumento neto de efectivo: Q" + Math.Abs(diferencia).ToString("N2");
                }
                else if (tipo == "DISMINUCIÓN")
                {
                    texto = "Disminución neta de efectivo: Q" + Math.Abs(diferencia).ToString("N2");
                }
                else
                {
                    texto = "Sin variación neta en el efectivo.";
                }

                return new Cls_ResultadoFlujo
                {
                    Diferencia = diferencia,
                    TipoResultado = tipo,
                    TextoResultado = texto,
                    EsValido = true,
                    FechaCalculo = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en fun_calcular_resultado: " + ex.Message);
                return new Cls_ResultadoFlujo
                {
                    Diferencia = 0,
                    TipoResultado = "ERROR",
                    TextoResultado = "Error al calcular el flujo de efectivo.",
                    EsValido = false,
                    FechaCalculo = DateTime.Now,
                    MensajeError = ex.Message
                };
            }
        }

        // ---------------------------------------------------------------------------------
        // Método: fun_validar_estado
        // Descripción: Comprueba si el controlador está correctamente inicializado
        // ---------------------------------------------------------------------------------
        public bool fun_validar_estado()
        {
            try
            {
                return gCalculo != null;
            }
            catch
            {
                return false;
            }
        }

        // =================================================================================
        // NUEVOS MÉTODOS HISTÓRICOS (modo Histórico)
        // =================================================================================

        // ---------------------------------------------------------------------------------
        // Método: fun_obtener_flujo_efectivo_historico
        // Descripción: Devuelve el flujo de efectivo histórico filtrado por año y mes.
        // ---------------------------------------------------------------------------------
        public DataTable fun_obtener_flujo_efectivo_historico(int iAnio, int iMes)
        {
            try
            {
                if (gCalculo == null)
                    throw new InvalidOperationException("El módulo de cálculo no está disponible.");

                DataTable dtsFlujoHist = gCalculo.fun_calcular_flujo_efectivo_historico(iAnio, iMes);

                if (dtsFlujoHist == null || dtsFlujoHist.Rows.Count == 0)
                    throw new InvalidOperationException($"No hay registros en el mes {iMes} del año {iAnio}.");

                return dtsFlujoHist;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en fun_obtener_flujo_efectivo_historico: " + ex.Message);
                throw new ApplicationException($"No hay registros en el mes {iMes} del año {iAnio}.", ex);
            }
        }

        // ---------------------------------------------------------------------------------
        // Método: fun_calcular_resultado_historico
        // Descripción: Calcula el resultado neto (aumento/disminución) en modo histórico.
        // ---------------------------------------------------------------------------------
        public Cls_ResultadoFlujo fun_calcular_resultado_historico(int iAnio, int iMes)
        {
            try
            {
                if (gCalculo == null)
                    throw new InvalidOperationException("El módulo de cálculo no está disponible.");

                DataTable dtsFlujoHist = gCalculo.fun_calcular_flujo_efectivo_historico(iAnio, iMes);

                if (dtsFlujoHist == null || dtsFlujoHist.Rows.Count == 0)
                    throw new InvalidOperationException($"No hay registros en el mes {iMes} del año {iAnio}.");

                decimal totalEntradas = 0;
                decimal totalSalidas = 0;

                foreach (DataRow fila in dtsFlujoHist.Rows)
                {
                    string nombre = fila["Nombre"]?.ToString()?.Trim().ToUpper() ?? "";

                    // Ignorar filas de totales que el cálculo ya agregó al DataTable
                    if (nombre.Contains("TOTAL FLUJO NETO") || nombre.Contains("AUMENTO / DISMINUCIÓN"))
                        continue;

                    if (decimal.TryParse(fila["Entrada"].ToString().Replace("Q", "").Replace(",", ""), out decimal ent))
                        totalEntradas += ent;

                    if (decimal.TryParse(fila["Salida"].ToString().Replace("Q", "").Replace(",", ""), out decimal sal))
                        totalSalidas += sal;
                }


                decimal diferencia = totalEntradas - totalSalidas;
                string tipo = gCalculo.fun_obtener_tipo_resultado(diferencia);
                string texto = tipo == "AUMENTO"
                    ? $"Aumento neto de efectivo: Q{Math.Abs(diferencia):N2}"
                    : tipo == "DISMINUCIÓN"
                        ? $"Disminución neta de efectivo: Q{Math.Abs(diferencia):N2}"
                        : "Sin variación neta en el efectivo.";


                return new Cls_ResultadoFlujo
                {
                    Diferencia = diferencia,
                    TipoResultado = tipo,
                    TextoResultado = texto,
                    EsValido = true,
                    FechaCalculo = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en fun_calcular_resultado_historico: " + ex.Message);
                return new Cls_ResultadoFlujo
                {
                    Diferencia = 0,
                    TipoResultado = "ERROR",
                    TextoResultado = $"No hay registros en el mes {iMes} del año {iAnio}.",
                    EsValido = false,
                    FechaCalculo = DateTime.Now,
                    MensajeError = ex.Message
                };
            }
        }
    }

    // ---------------------------------------------------------------------------------
    // Clase DTO: Cls_ResultadoFlujo
    // Descripción: Contenedor del resultado del cálculo (para el Label del formulario)
    // ---------------------------------------------------------------------------------
    public class Cls_ResultadoFlujo
    {
        public decimal Diferencia { get; set; }
        public string TipoResultado { get; set; }
        public string TextoResultado { get; set; }
        public DateTime FechaCalculo { get; set; }
        public bool EsValido { get; set; }
        public string MensajeError { get; set; }
    }
}

// Fin de código de Arón Ricardo Esquit Silva
// =====================================================================================
