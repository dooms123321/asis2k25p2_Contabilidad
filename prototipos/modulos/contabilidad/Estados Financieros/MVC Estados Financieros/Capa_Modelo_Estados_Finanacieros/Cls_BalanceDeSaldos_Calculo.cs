// =====================================================================================
// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   09/11/2025
// Descripción: Cálculo compatible con datos actuales e históricos del Balance de Saldos
// =====================================================================================

using System;
using System.Data;
using System.Linq;

namespace Capa_Modelo_Estados_Financieros
{
    public class Cls_BalanceDeSaldos_Calculo
    {
        private readonly Cls_BalanceDeSaldos_Dao gDao = new Cls_BalanceDeSaldos_Dao();

        // ---------------------------------------------------------------------------------
        // Método: Fun_Calcular_Balance_Saldos
        // Calcula el balance de saldos (niveles 1, 2, 3) desde catálogo actual
        // ---------------------------------------------------------------------------------
        public DataTable Fun_Calcular_Balance_Saldos(int iNivel)
        {
            DataTable dts_Balance = gDao.Fun_Consultar_Balance_Saldos(3);
            if (dts_Balance.Rows.Count == 0)
                return dts_Balance;

            return Fun_Procesar_Balance(dts_Balance, iNivel);
        }

        // ---------------------------------------------------------------------------------
        // Método: Fun_Calcular_Balance_Saldos_Historico
        // Calcula el balance de saldos desde tabla histórica filtrando por año y mes
        // ---------------------------------------------------------------------------------
        public DataTable Fun_Calcular_Balance_Saldos_Historico(int iNivel, int iAnio, int iMes)
        {
            DataTable dts_Balance = gDao.Fun_Consultar_Balance_Saldos_Historico(iNivel, iAnio, iMes);
            if (dts_Balance.Rows.Count == 0)
                return dts_Balance;

            return Fun_Procesar_Balance(dts_Balance, iNivel);
        }

        // ---------------------------------------------------------------------------------
        // Método interno: Fun_Procesar_Balance
        // Aplica la lógica de acumulación y filtrado por nivel (reutilizable)
        // ---------------------------------------------------------------------------------
        private DataTable Fun_Procesar_Balance(DataTable dts_Balance, int iNivel)
        {
            if (!dts_Balance.Columns.Contains("Naturaleza"))
                dts_Balance.Columns.Add("Naturaleza", typeof(int), "0");

            DataTable dts_Acumulado = dts_Balance.Copy();

            var cuentasOrdenadas = dts_Balance.AsEnumerable()
                .Select(r => r.Field<string>("Codigo"))
                .OrderByDescending(c => c.Split('.').Length)
                .ToList();

            foreach (string cuenta in cuentasOrdenadas)
            {
                string[] partes = cuenta.Split('.');
                for (int i = partes.Length - 1; i > 0; i--)
                {
                    string madre = string.Join(".", partes.Take(i));

                    DataRow sub = dts_Balance.AsEnumerable()
                        .FirstOrDefault(r => r.Field<string>("Codigo") == cuenta);

                    DataRow madreFila = dts_Acumulado.AsEnumerable()
                        .FirstOrDefault(r => r.Field<string>("Codigo") == madre);

                    if (sub != null && madreFila != null)
                    {
                        madreFila["Debe"] = Convert.ToDecimal(madreFila["Debe"]) + Convert.ToDecimal(sub["Debe"]);
                        madreFila["Haber"] = Convert.ToDecimal(madreFila["Haber"]) + Convert.ToDecimal(sub["Haber"]);
                    }
                }
            }

            decimal deTotalDebe = 0;
            decimal deTotalHaber = 0;

            foreach (DataRow fila in dts_Acumulado.Rows)
            {
                string codigo = fila.Field<string>("Codigo");
                bool tieneHijas = dts_Acumulado.AsEnumerable()
                    .Any(r => r.Field<string>("Codigo").StartsWith(codigo + "."));

                if (!tieneHijas)
                {
                    deTotalDebe += Convert.ToDecimal(fila["Debe"]);
                    deTotalHaber += Convert.ToDecimal(fila["Haber"]);
                }
            }

            DataTable dts_Filtrado = dts_Acumulado.Clone();
            foreach (DataRow fila in dts_Acumulado.Rows)
            {
                int nivelCuenta = fila.Field<string>("Codigo").Split('.').Length;
                if (nivelCuenta <= iNivel)
                    dts_Filtrado.ImportRow(fila);
            }

            DataRow drTotal = dts_Filtrado.NewRow();
            drTotal["Nombre"] = "TOTAL GENERAL";
            drTotal["Debe"] = deTotalDebe;
            drTotal["Haber"] = deTotalHaber;
            dts_Filtrado.Rows.Add(drTotal);

            return dts_Filtrado;
        }
    }
}

// Fin de código de Arón Ricardo Esquit Silva
// =====================================================================================
