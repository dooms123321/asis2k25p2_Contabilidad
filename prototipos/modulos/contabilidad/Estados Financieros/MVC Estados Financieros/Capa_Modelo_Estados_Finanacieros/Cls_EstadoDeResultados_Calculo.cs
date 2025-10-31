// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   30/10/2025

using System;
using System.Data;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_EstadoDeResultados_Calculo
    {
        private readonly Cls_EstadoDeResultados_Dao gDao = new Cls_EstadoDeResultados_Dao();

        public DataTable fun_calcular_estado_resultados()
        {
            DataTable dts_Base = gDao.fun_consultar_estado_resultados();
            DataTable dts_Resultado = new DataTable();

            // Columnas estándar
            dts_Resultado.Columns.Add("Codigo");
            dts_Resultado.Columns.Add("Nombre");
            dts_Resultado.Columns.Add("Tipo");
            dts_Resultado.Columns.Add("Valor", typeof(decimal));

            // Acumuladores
            decimal totalIngresos = 0, totalCostos = 0, totalGastosOp = 0, totalGastosFin = 0, totalISR = 0;

            // SECCIÓN 1: INGRESOS
            var ingresos = dts_Base.Select("Codigo LIKE '4%'");
            if (ingresos.Length > 0)
            {
                dts_Resultado.Rows.Add(null, "INGRESOS", "", 0);
                foreach (DataRow fila in ingresos)
                {
                    decimal valor = Convert.ToDecimal(fila["Valor"]);
                    dts_Resultado.Rows.Add(fila["Codigo"], fila["Nombre"], "Ingreso", valor);
                    totalIngresos += valor;
                }
                dts_Resultado.Rows.Add(null, "Total Ingresos", "", totalIngresos);
            }

            // SECCIÓN 2: COSTOS
            var costos = dts_Base.Select("Codigo LIKE '5%'");
            if (costos.Length > 0)
            {
                dts_Resultado.Rows.Add(null, "COSTOS", "", 0);
                foreach (DataRow fila in costos)
                {
                    decimal valor = Convert.ToDecimal(fila["Valor"]);
                    dts_Resultado.Rows.Add(fila["Codigo"], fila["Nombre"], "Costo", valor);
                    totalCostos += valor;
                }
                dts_Resultado.Rows.Add(null, "Total Costos", "", totalCostos);
            }

            // SECCIÓN 3: GASTOS OPERATIVOS (6.1.x)
            var gastosOp = dts_Base.Select("Codigo LIKE '6.1%'");
            if (gastosOp.Length > 0)
            {
                dts_Resultado.Rows.Add(null, "GASTOS OPERATIVOS", "", 0);
                foreach (DataRow fila in gastosOp)
                {
                    decimal valor = Convert.ToDecimal(fila["Valor"]);
                    dts_Resultado.Rows.Add(fila["Codigo"], fila["Nombre"], "Gasto Operativo", valor);
                    totalGastosOp += valor;
                }
                dts_Resultado.Rows.Add(null, "Total Gastos Operativos", "", totalGastosOp);
            }

            // SECCIÓN 4: GASTOS FINANCIEROS (6.2.x)
            var gastosFin = dts_Base.Select("Codigo LIKE '6.2%'");
            if (gastosFin.Length > 0)
            {
                dts_Resultado.Rows.Add(null, "GASTOS FINANCIEROS", "", 0);
                foreach (DataRow fila in gastosFin)
                {
                    decimal valor = Convert.ToDecimal(fila["Valor"]);
                    dts_Resultado.Rows.Add(fila["Codigo"], fila["Nombre"], "Gasto Financiero", valor);
                    totalGastosFin += valor;
                }
                dts_Resultado.Rows.Add(null, "Total Gastos Financieros", "", totalGastosFin);
            }

            // SECCIÓN 5: GASTO POR ISR (6.3.x)
            var gastoISR = dts_Base.Select("Codigo LIKE '6.3%'");
            if (gastoISR.Length > 0)
            {
                dts_Resultado.Rows.Add(null, "GASTO POR ISR", "", 0);
                foreach (DataRow fila in gastoISR)
                {
                    decimal valor = Convert.ToDecimal(fila["Valor"]);
                    dts_Resultado.Rows.Add(fila["Codigo"], fila["Nombre"], "Gasto ISR", valor);
                    totalISR += valor;
                }
                dts_Resultado.Rows.Add(null, "Total Gastos Financieros y Fiscales", "", totalISR);
            }

            // CÁLCULO FINAL: UTILIDAD O PÉRDIDA NETA
            decimal utilidadNeta = totalIngresos - (totalCostos + totalGastosOp + totalGastosFin + totalISR);
            string nombreResultado = utilidadNeta >= 0 ? "UTILIDAD NETA" : "PÉRDIDA NETA";

            dts_Resultado.Rows.Add(null, nombreResultado, "Resultado", utilidadNeta);

            return dts_Resultado;
        }
    }
}
