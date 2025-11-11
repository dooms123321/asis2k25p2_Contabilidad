// =====================================================================================
// Inicio de código de Arón Ricardo Esquit Silva   0901-22-13036   09/11/2025
// Descripción: Controlador del Balance de Saldos con soporte para datos históricos
// =====================================================================================

using System;
using System.Data;
using System.Linq;
using Capa_Modelo_Estados_Financieros;

namespace Capa_Controlador_Estados_Financieros
{
    public class Cls_BalanceDeSaldos_Controlador
    {
        private readonly Cls_BalanceDeSaldos_Calculo gCalculo = new Cls_BalanceDeSaldos_Calculo();

        // ---------------------------------------------------------------------------------
        // Método: Fun_Obtener_Balance_Saldos
        // Obtiene el balance actual y lo prepara para mostrarse en la vista
        // ---------------------------------------------------------------------------------
        public DataTable Fun_Obtener_Balance_Saldos(int iNivel)
        {
            try
            {
                DataTable dts_Balance = gCalculo.Fun_Calcular_Balance_Saldos(iNivel);
                return Fun_Formatear_Balance(dts_Balance);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el balance actual: " + ex.Message);
                return new DataTable();
            }
        }

        // ---------------------------------------------------------------------------------
        // Método: Fun_Obtener_Balance_Saldos_Historico
        // Obtiene el balance histórico filtrado por año y mes
        // ---------------------------------------------------------------------------------
        public DataTable Fun_Obtener_Balance_Saldos_Historico(int iNivel, int iAnio, int iMes)
        {
            try
            {
                DataTable dts_Balance = gCalculo.Fun_Calcular_Balance_Saldos_Historico(iNivel, iAnio, iMes);
                return Fun_Formatear_Balance(dts_Balance);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el balance histórico: " + ex.Message);
                return new DataTable();
            }
        }

        // ---------------------------------------------------------------------------------
        // Método interno: Fun_Formatear_Balance
        // Unifica formato de montos, nombres y niveles para mostrar en vista
        // ---------------------------------------------------------------------------------
        private DataTable Fun_Formatear_Balance(DataTable dts_Balance)
        {
            if (dts_Balance == null || dts_Balance.Rows.Count == 0)
                return new DataTable();

            DataTable dts_Formateado = Fun_Crear_DataTable();

            foreach (DataRow filaOriginal in dts_Balance.Rows)
            {
                string codigo = filaOriginal["Codigo"].ToString();
                string nombre = filaOriginal["Nombre"].ToString();
                decimal debe = Convert.ToDecimal(filaOriginal["Debe"]);
                decimal haber = Convert.ToDecimal(filaOriginal["Haber"]);

                int naturaleza = Fun_Determinar_Naturaleza(codigo);
                bool esTotal = nombre == "TOTAL GENERAL";
                int nivel = Fun_Calcular_Nivel(codigo);
                bool esMovimiento = nivel >= 3;

                DataRow nuevaFila = dts_Formateado.NewRow();
                nuevaFila["Codigo"] = codigo;
                nuevaFila["Nombre"] = nombre;
                nuevaFila["Debe"] = Fun_Formatear_Monto(debe, esTotal, esMovimiento, nivel, naturaleza, "Debe");
                nuevaFila["Haber"] = Fun_Formatear_Monto(haber, esTotal, esMovimiento, nivel, naturaleza, "Haber");
                dts_Formateado.Rows.Add(nuevaFila);
            }

            return dts_Formateado;
        }

        // ---------------------------------------------------------------------------------
        // Estructura de salida para DataGridView
        // ---------------------------------------------------------------------------------
        private DataTable Fun_Crear_DataTable()
        {
            DataTable dts = new DataTable();
            dts.Columns.Add("Codigo", typeof(string));
            dts.Columns.Add("Nombre", typeof(string));
            dts.Columns.Add("Debe", typeof(string));
            dts.Columns.Add("Haber", typeof(string));
            return dts;
        }

        // ---------------------------------------------------------------------------------
        // Funciones auxiliares de formato
        // ---------------------------------------------------------------------------------
        private int Fun_Calcular_Nivel(string sCodigo)
        {
            if (string.IsNullOrEmpty(sCodigo)) return 0;
            return sCodigo.Count(c => c == '.') + 1;
        }

        private string Fun_Formatear_Monto(decimal deMonto, bool bEsTotal, bool bEsMovimiento, int iNivel, int iNaturaleza, string sColumna)
        {
            if (bEsTotal)
                return $"Q {deMonto:N2}";

            if (iNivel == 1)
                return deMonto == 0 ? "" : $"Q {deMonto:N2}";

            if (!bEsMovimiento)
            {
                if (deMonto == 0)
                {
                    if (iNaturaleza == 1 && sColumna == "Debe") return "Q0.00";
                    if (iNaturaleza == 0 && sColumna == "Haber") return "Q0.00";
                    return "";
                }
                else
                    return $"Q {deMonto:N2}";
            }

            if (bEsMovimiento)
            {
                if (deMonto == 0)
                {
                    if (iNaturaleza == 1 && sColumna == "Debe") return "Q0.00";
                    if (iNaturaleza == 0 && sColumna == "Haber") return "Q0.00";
                    return "";
                }

                return $"Q {deMonto:N2}";
            }

            return deMonto == 0 ? "" : $"Q {deMonto:N2}";
        }

        private int Fun_Determinar_Naturaleza(string sCodigo)
        {
            if (string.IsNullOrEmpty(sCodigo)) return 1;

            if (sCodigo.StartsWith("1") || sCodigo.StartsWith("5") || sCodigo.StartsWith("6") || sCodigo.StartsWith("8"))
                return 1; // Deudora

            if (sCodigo.StartsWith("2") || sCodigo.StartsWith("3") || sCodigo.StartsWith("4") || sCodigo.StartsWith("7"))
                return 0; // Acreedora

            return 1;
        }
    }
}

// Fin de código de Arón Ricardo Esquit Silva
// =====================================================================================
