// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   30/10/2025

using System;
using System.Data;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_BalanceGeneral_Calculo
    {
        private readonly Cls_BalanceGeneral_Dao gDao = new Cls_BalanceGeneral_Dao();

        // Método principal para generar el balance general con cálculos
        public DataTable fun_calcular_balance_general()
        {
            DataTable dts_Base = gDao.fun_consultar_balance_general();
            DataTable dts_Result = new DataTable();

            // Definir columnas
            dts_Result.Columns.Add("Codigo");
            dts_Result.Columns.Add("Nombre");
            dts_Result.Columns.Add("Tipo");
            dts_Result.Columns.Add("Valor", typeof(decimal));

            // Acumuladores
            decimal totalActivo = 0;
            decimal totalPasivo = 0;
            decimal totalCapital = 0;

            // Sección: ACTIVO
            var activos = dts_Base.Select("Codigo LIKE '1%'");
            if (activos.Length > 0)
            {
                dts_Result.Rows.Add(null, "ACTIVO", "", 0);
                foreach (DataRow fila in activos)
                {
                    dts_Result.Rows.Add(fila["Codigo"], fila["Nombre"], "Activo", Convert.ToDecimal(fila["Valor"]));
                    totalActivo += Convert.ToDecimal(fila["Valor"]);
                }
                dts_Result.Rows.Add(null, "Total Activo", "", totalActivo);
            }

            // Sección: PASIVO
            var pasivos = dts_Base.Select("Codigo LIKE '2%'");
            if (pasivos.Length > 0)
            {
                dts_Result.Rows.Add(null, "PASIVO", "", 0);
                foreach (DataRow fila in pasivos)
                {
                    dts_Result.Rows.Add(fila["Codigo"], fila["Nombre"], "Pasivo", Convert.ToDecimal(fila["Valor"]));
                    totalPasivo += Convert.ToDecimal(fila["Valor"]);
                }
                dts_Result.Rows.Add(null, "Total Pasivo", "", totalPasivo);
            }

            // Sección: CAPITAL
            var capitales = dts_Base.Select("Codigo LIKE '3%'");
            if (capitales.Length > 0)
            {
                dts_Result.Rows.Add(null, "CAPITAL", "", 0);
                foreach (DataRow fila in capitales)
                {
                    dts_Result.Rows.Add(fila["Codigo"], fila["Nombre"], "Capital", Convert.ToDecimal(fila["Valor"]));
                    totalCapital += Convert.ToDecimal(fila["Valor"]);
                }
                dts_Result.Rows.Add(null, "Total Capital", "", totalCapital);
            }

            // Agregar la suma final del pasivo + capital
            decimal sumaPasivoCapital = totalPasivo + totalCapital;
            dts_Result.Rows.Add(null, "Suma del Pasivo + Capital", "", sumaPasivoCapital);

            // Agregar fila vacía para claridad visual
            dts_Result.Rows.Add(null, "", "", 0);

            // Evaluar ecuación contable
            decimal diferencia = totalActivo - sumaPasivoCapital;
            string estadoEcuacion = diferencia == 0 ? "BALANCE CUADRADO" : "DESCUADRE";

            // Agregar fila de resultado
            dts_Result.Rows.Add(null, estadoEcuacion, "Ecuación Contable", diferencia);

            return dts_Result;
        }
    }
}
