// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   29/10/2025

using System;
using System.Data;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_BalanceDeSaldos_Calculo
    {
        private readonly Cls_BalanceDeSaldos_Dao gDao = new Cls_BalanceDeSaldos_Dao();

        // Método principal para obtener el balance con totales calculados
        public DataTable fun_calcular_balance_saldos()
        {
            // Obtener los registros desde la base de datos
            DataTable dts_Balance = gDao.fun_consultar_balance_saldos();

            // Verificar que existan registros
            if (dts_Balance.Rows.Count == 0)
                return dts_Balance;

            // Crear las variables para acumular los totales
            decimal deTotalDebe = 0;
            decimal deTotalHaber = 0;

            // Recorrer todas las filas del balance
            foreach (DataRow fila in dts_Balance.Rows)
            {
                decimal deDebe = Convert.ToDecimal(fila["Debe"]);
                decimal deHaber = Convert.ToDecimal(fila["Haber"]);

                // Calcular saldo por fila (como en el trigger)
                decimal deSaldo = deDebe - deHaber;
                fila["Saldo"] = deSaldo;

                // Acumular totales
                deTotalDebe += deDebe;
                deTotalHaber += deHaber;
            }

            // Crear una nueva fila para mostrar los totales al final
            DataRow filaTotal = dts_Balance.NewRow();
            filaTotal["Nombre"] = "TOTAL GENERAL";
            filaTotal["Debe"] = deTotalDebe;
            filaTotal["Haber"] = deTotalHaber;
            filaTotal["Saldo"] = deTotalDebe - deTotalHaber;

            dts_Balance.Rows.Add(filaTotal);

            return dts_Balance;
        }
    }
}
