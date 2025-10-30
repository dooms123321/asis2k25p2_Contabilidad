// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   31/10/2025

using System;
using System.Data;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_FlujoDeEfectivo_Calculo
    {
        private readonly Cls_FlujoDeEfectivo_Dao gDao = new Cls_FlujoDeEfectivo_Dao();

        public DataTable fun_calcular_flujo_efectivo()
        {
            DataTable dts_Base = gDao.fun_consultar_flujo_efectivo();
            DataTable dts_Resultado = new DataTable();

            // Columnas estándar
            dts_Resultado.Columns.Add("Codigo");
            dts_Resultado.Columns.Add("Nombre");
            dts_Resultado.Columns.Add("Actividad");
            dts_Resultado.Columns.Add("Entrada", typeof(decimal));
            dts_Resultado.Columns.Add("Salida", typeof(decimal));

            // Acumuladores
            decimal totalOperativaE = 0, totalOperativaS = 0;
            decimal totalInversionE = 0, totalInversionS = 0;
            decimal totalFinanciacionE = 0, totalFinanciacionS = 0;
            decimal totalNoClasificadaE = 0, totalNoClasificadaS = 0;

            // --- No clasificadas ---
            var noClasificadas = dts_Base.Select("TipoActividad = 'No clasificada'");
            if (noClasificadas.Length > 0)
            {
                dts_Resultado.Rows.Add(null, "CUENTAS NO CLASIFICADAS", null, 0, 0);

                foreach (DataRow fila in noClasificadas)
                {
                    decimal entrada = Convert.ToDecimal(fila["Entrada"]);
                    decimal salida = Convert.ToDecimal(fila["Salida"]);

                    dts_Resultado.Rows.Add(
                        fila["Codigo"],
                        fila["Nombre"],
                        "No clasificada",
                        entrada,
                        salida
                    );

                    totalNoClasificadaE += entrada;
                    totalNoClasificadaS += salida;
                }

                dts_Resultado.Rows.Add(null, "", null, 0, 0);
            }

            // --- Actividades Operativas ---
            var operativas = dts_Base.Select("TipoActividad = 'Operativa'");
            if (operativas.Length > 0)
            {
                dts_Resultado.Rows.Add(null, "ACTIVIDADES OPERATIVAS", null, 0, 0);

                foreach (DataRow fila in operativas)
                {
                    decimal entrada = Convert.ToDecimal(fila["Entrada"]);
                    decimal salida = Convert.ToDecimal(fila["Salida"]);

                    dts_Resultado.Rows.Add(
                        fila["Codigo"],
                        fila["Nombre"],
                        "Operativa",
                        entrada,
                        salida
                    );

                    totalOperativaE += entrada;
                    totalOperativaS += salida;
                }

                dts_Resultado.Rows.Add(null, "Total Actividades Operativas", null, totalOperativaE, totalOperativaS);
            }

            // --- Actividades de Inversión ---
            var inversion = dts_Base.Select("TipoActividad = 'Inversión'");
            if (inversion.Length > 0)
            {
                dts_Resultado.Rows.Add(null, "", null, 0, 0);
                dts_Resultado.Rows.Add(null, "ACTIVIDADES DE INVERSIÓN", null, 0, 0);

                foreach (DataRow fila in inversion)
                {
                    decimal entrada = Convert.ToDecimal(fila["Entrada"]);
                    decimal salida = Convert.ToDecimal(fila["Salida"]);

                    dts_Resultado.Rows.Add(
                        fila["Codigo"],
                        fila["Nombre"],
                        "Inversión",
                        entrada,
                        salida
                    );

                    totalInversionE += entrada;
                    totalInversionS += salida;
                }

                dts_Resultado.Rows.Add(null, "Total Actividades de Inversión", null, totalInversionE, totalInversionS);
            }

            // --- Actividades de Financiación ---
            var financiacion = dts_Base.Select("TipoActividad = 'Financiación'");
            if (financiacion.Length > 0)
            {
                dts_Resultado.Rows.Add(null, "", null, 0, 0);
                dts_Resultado.Rows.Add(null, "ACTIVIDADES DE FINANCIACIÓN", null, 0, 0);

                foreach (DataRow fila in financiacion)
                {
                    decimal entrada = Convert.ToDecimal(fila["Entrada"]);
                    decimal salida = Convert.ToDecimal(fila["Salida"]);

                    dts_Resultado.Rows.Add(
                        fila["Codigo"],
                        fila["Nombre"],
                        "Financiación",
                        entrada,
                        salida
                    );

                    totalFinanciacionE += entrada;
                    totalFinanciacionS += salida;
                }

                dts_Resultado.Rows.Add(null, "Total Actividades de Financiación", null, totalFinanciacionE, totalFinanciacionS);
            }

            // --- Totales finales ---
            decimal totalEntradas = totalOperativaE + totalInversionE + totalFinanciacionE + totalNoClasificadaE;
            decimal totalSalidas = totalOperativaS + totalInversionS + totalFinanciacionS + totalNoClasificadaS;
            decimal flujoNeto = totalEntradas - totalSalidas;

            dts_Resultado.Rows.Add(null, "", null, 0, 0);
            dts_Resultado.Rows.Add(null, "Total Flujo Neto de Efectivo", null, totalEntradas, totalSalidas);

            // --- Resultado final ---
            string tipoResultado = flujoNeto >= 0 ? "AUMENTO NETO DE EFECTIVO" : "DISMINUCIÓN NETA DE EFECTIVO";
            dts_Resultado.Rows.Add(null, tipoResultado, "Resultado", Math.Abs(flujoNeto), 0);

            return dts_Resultado;
        }
    }
}
