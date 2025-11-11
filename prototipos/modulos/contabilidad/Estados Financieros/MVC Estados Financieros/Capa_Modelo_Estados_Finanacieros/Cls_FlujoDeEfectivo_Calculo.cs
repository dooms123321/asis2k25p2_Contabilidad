// =====================================================================================
// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   09/11/2025
// Descripción: Clase de cálculo para el Estado de Flujo de Efectivo (método directo)
// Proyecto: QUANTUM S.A. - Módulo CTA (Contabilidad)
// =====================================================================================

using System;
using System.Data;
using System.Globalization;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_FlujoEfectivo_Calculo
    {
        private readonly Cls_FlujoEfectivo_Dao gDao = new Cls_FlujoEfectivo_Dao();

        // ---------------------------------------------------------------------------------
        // Método: fun_calcular_flujo_efectivo
        // Descripción:
        // Obtiene los datos del flujo desde el DAO y genera los totales y el resultado neto.
        // ---------------------------------------------------------------------------------
        public DataTable fun_calcular_flujo_efectivo()
        {
            DataTable dtsBase = gDao.fun_consultar_flujo_efectivo();

            // Crear tabla resultado con estructura uniforme
            DataTable dtsResultado = new DataTable();
            dtsResultado.Columns.Add("Cuenta");
            dtsResultado.Columns.Add("Nombre");
            dtsResultado.Columns.Add("Entrada", typeof(string));
            dtsResultado.Columns.Add("Salida", typeof(string));

            decimal totalEntradas = 0;
            decimal totalSalidas = 0;

            // Procesar filas base y calcular totales
            foreach (DataRow fila in dtsBase.Rows)
            {
                string cuenta = fila["Cuenta"].ToString();
                string nombre = fila["Nombre"].ToString();

                string entradaStr = fila["Entrada"]?.ToString()?.Replace("Q", "").Replace(",", "").Trim();
                string salidaStr = fila["Salida"]?.ToString()?.Replace("Q", "").Replace(",", "").Trim();

                decimal entrada = 0;
                decimal salida = 0;

                if (decimal.TryParse(entradaStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal ent))
                    entrada = ent;
                if (decimal.TryParse(salidaStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal sal))
                    salida = sal;

                totalEntradas += entrada;
                totalSalidas += salida;

                // Asignar formato correcto solo si hay monto o es cero válido
                string entradaFmt = entrada > 0 ? $"Q{entrada:N2}" :
                    (entradaStr == "0.00" ? "Q0.00" : "");
                string salidaFmt = salida > 0 ? $"Q{salida:N2}" :
                    (salidaStr == "0.00" ? "Q0.00" : "");

                dtsResultado.Rows.Add(cuenta, nombre, entradaFmt, salidaFmt);
            }

            // ---------------------------------------------------------------------------------
            // Agregar fila: TOTAL FLUJO NETO DE EFECTIVO
            // ---------------------------------------------------------------------------------
            DataRow filaTotal = dtsResultado.NewRow();
            filaTotal["Cuenta"] = "";
            filaTotal["Nombre"] = "TOTAL FLUJO NETO DE EFECTIVO";
            filaTotal["Entrada"] = $"Q{totalEntradas:N2}";
            filaTotal["Salida"] = $"Q{totalSalidas:N2}";
            dtsResultado.Rows.Add(filaTotal);

            // ---------------------------------------------------------------------------------
            // Agregar fila: AUMENTO / DISMINUCIÓN NETA DE EFECTIVO
            // ---------------------------------------------------------------------------------
            decimal diferencia = totalEntradas - totalSalidas;
            string tipo = diferencia >= 0 ? "AUMENTO" : "DISMINUCIÓN";
            string diferenciaFmt = $"Q{Math.Abs(diferencia):N2} ({tipo})";

            DataRow filaResultado = dtsResultado.NewRow();
            filaResultado["Cuenta"] = "";
            filaResultado["Nombre"] = "AUMENTO / DISMINUCIÓN NETA DE EFECTIVO";
            filaResultado["Entrada"] = "";
            filaResultado["Salida"] = diferenciaFmt;
            dtsResultado.Rows.Add(filaResultado);

            return dtsResultado;
        }


        // ---------------------------------------------------------------------------------
        // Método: fun_calcular_aumento_disminucion
        // Descripción: Calcula directamente la diferencia entre total de entradas y salidas,
        // sin depender de otra función del DAO (mismo cálculo que la tabla visible).
        // ---------------------------------------------------------------------------------
        public decimal fun_calcular_aumento_disminucion()
        {
            DataTable dtsBase = gDao.fun_consultar_flujo_efectivo();

            decimal totalEntradas = 0;
            decimal totalSalidas = 0;

            foreach (DataRow fila in dtsBase.Rows)
            {
                decimal entrada = 0;
                decimal salida = 0;

                if (decimal.TryParse(fila["Entrada"].ToString(), out decimal ent))
                    entrada = ent;

                if (decimal.TryParse(fila["Salida"].ToString(), out decimal sal))
                    salida = sal;

                totalEntradas += entrada;
                totalSalidas += salida;
            }

            return totalEntradas - totalSalidas;
        }


        // ---------------------------------------------------------------------------------
        // Método: fun_obtener_tipo_resultado
        // Descripción: Determina si el flujo neto fue aumento o disminución.
        // ---------------------------------------------------------------------------------
        public string fun_obtener_tipo_resultado(decimal diferencia)
        {
            if (diferencia > 0)
                return "AUMENTO";
            else if (diferencia < 0)
                return "DISMINUCIÓN";
            else
                return "NEUTRO";
        }

        // ---------------------------------------------------------------------------------
        // Método: fun_calcular_flujo_efectivo_historico
        // Descripción:
        // Obtiene los datos históricos del flujo de efectivo filtrados por año y mes.
        // Mantiene la misma estructura visual y de totales que el modo actual.
        // ---------------------------------------------------------------------------------
        public DataTable fun_calcular_flujo_efectivo_historico(int iAnio, int iMes)
        {
            DataTable dtsBase = gDao.fun_consultar_flujo_efectivo_historico(iAnio, iMes);

            // Crear tabla resultado con estructura uniforme
            DataTable dtsResultado = new DataTable();
            dtsResultado.Columns.Add("Cuenta");
            dtsResultado.Columns.Add("Nombre");
            dtsResultado.Columns.Add("Entrada", typeof(string));
            dtsResultado.Columns.Add("Salida", typeof(string));

            decimal totalEntradas = 0;
            decimal totalSalidas = 0;

            foreach (DataRow fila in dtsBase.Rows)
            {
                string cuenta = fila["Cuenta"].ToString();
                string nombre = fila["Nombre"].ToString();

                string entradaStr = fila["Entrada"]?.ToString()?.Replace("Q", "").Replace(",", "").Trim();
                string salidaStr = fila["Salida"]?.ToString()?.Replace("Q", "").Replace(",", "").Trim();

                decimal entrada = 0;
                decimal salida = 0;

                if (decimal.TryParse(entradaStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal ent))
                    entrada = ent;
                if (decimal.TryParse(salidaStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal sal))
                    salida = sal;

                totalEntradas += entrada;
                totalSalidas += salida;

                string entradaFmt = entrada > 0 ? $"Q{entrada:N2}" :
                    (entradaStr == "0.00" ? "Q0.00" : "");
                string salidaFmt = salida > 0 ? $"Q{salida:N2}" :
                    (salidaStr == "0.00" ? "Q0.00" : "");

                dtsResultado.Rows.Add(cuenta, nombre, entradaFmt, salidaFmt);
            }

            // ---------------------------------------------------------------------------------
            // Agregar fila: TOTAL FLUJO NETO DE EFECTIVO
            // ---------------------------------------------------------------------------------
            DataRow filaTotal = dtsResultado.NewRow();
            filaTotal["Cuenta"] = "";
            filaTotal["Nombre"] = "TOTAL FLUJO NETO DE EFECTIVO";
            filaTotal["Entrada"] = $"Q{totalEntradas:N2}";
            filaTotal["Salida"] = $"Q{totalSalidas:N2}";
            dtsResultado.Rows.Add(filaTotal);

            // ---------------------------------------------------------------------------------
            // Agregar fila: AUMENTO / DISMINUCIÓN NETA DE EFECTIVO
            // ---------------------------------------------------------------------------------
            decimal diferencia = totalEntradas - totalSalidas;
            string tipo = diferencia >= 0 ? "AUMENTO" : "DISMINUCIÓN";
            string diferenciaFmt = $"Q{Math.Abs(diferencia):N2} ({tipo})";

            DataRow filaResultado = dtsResultado.NewRow();
            filaResultado["Cuenta"] = "";
            filaResultado["Nombre"] = "AUMENTO / DISMINUCIÓN NETA DE EFECTIVO";
            filaResultado["Entrada"] = "";
            filaResultado["Salida"] = diferenciaFmt;
            dtsResultado.Rows.Add(filaResultado);

            return dtsResultado;
        }

    }
}

// Fin de código de Arón Ricardo Esquit Silva
// =====================================================================================
